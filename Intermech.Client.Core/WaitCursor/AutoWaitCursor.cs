
// Type: WaitCursor.AutoWaitCursor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;


namespace WaitCursor;

/// <summary>
/// This static utility class can be used to automatically show a wait cursor when the application
/// is busy (ie not responding to user input). The class automatically monitors the application
/// state, removing the need for manually changing the cursor.
/// </summary>
/// <example>
/// To use, simply insert the following line in your Application startup code
/// 
/// 	private void Form1_Load(object sender, System.EventArgs e)
/// 	{
/// 		AutoWaitCursor.Cursor = Cursors.WaitCursor;
/// 		AutoWaitCursor.Delay = new TimeSpan(0, 0, 0, 0, 25);
/// 		// Set the window handle to the handle of the main form in your application
/// 		AutoWaitCursor.MainWindowHandle = this.Handle;
/// 		AutoWaitCursor.Start();
/// 	}
/// 
/// This installs changes to cursor after 100ms of blocking work (ie. work carried out on the main application thread).
/// 
/// Note, the above code GLOBALLY replaces the following:
/// 
/// public void DoWork()
/// {
/// 	try
/// 	{
/// 		Screen.Cursor = Cursors.Wait;
/// 		GetResultsFromDatabase();
/// 	}
/// 	finally
/// 	{
/// 		Screen.Cursor = Cursors.Default;
/// 	}
/// }
/// </example>
[DebuggerStepThrough]
public class AutoWaitCursor
{
  private static readonly TimeSpan DEFAULT_DELAY = new TimeSpan(0, 0, 0, 0, 25);
  /// <summary>
  /// The application state monitor class (which monitors the application busy status).
  /// </summary>
  private static AutoWaitCursor.ApplicationStateMonitor _appStateMonitor = new AutoWaitCursor.ApplicationStateMonitor(Cursors.WaitCursor, AutoWaitCursor.DEFAULT_DELAY);

  /// <summary>Default Constructor.</summary>
  private AutoWaitCursor()
  {
  }

  /// <summary>
  /// Returns the amount of time the application has been idle.
  /// </summary>
  public TimeSpan ApplicationIdleTime => AutoWaitCursor._appStateMonitor.ApplicationIdleTime;

  /// <summary>
  /// Returns true if the auto wait cursor has been started.
  /// </summary>
  public static bool IsStarted => AutoWaitCursor._appStateMonitor.IsStarted;

  /// <summary>
  /// Gets or sets the Cursor to use during Application busy periods.
  /// </summary>
  public static Cursor Cursor
  {
    get => AutoWaitCursor._appStateMonitor.Cursor;
    set => AutoWaitCursor._appStateMonitor.Cursor = value;
  }

  /// <summary>Enables or disables the auto wait cursor.</summary>
  public static bool Enabled
  {
    get => AutoWaitCursor._appStateMonitor.Enabled;
    set => AutoWaitCursor._appStateMonitor.Enabled = value;
  }

  /// <summary>
  /// Gets or sets the period of Time to wait before showing the WaitCursor whilst Application is working
  /// </summary>
  public static TimeSpan Delay
  {
    get => AutoWaitCursor._appStateMonitor.Delay;
    set => AutoWaitCursor._appStateMonitor.Delay = value;
  }

  /// <summary>
  /// Gets or sets the main window handle of the application (ie the handle of an MDI form).
  /// This is the window handle monitored to detect when the application becomes busy.
  /// </summary>
  public static IntPtr MainWindowHandle
  {
    get => AutoWaitCursor._appStateMonitor.MainWindowHandle;
    set => AutoWaitCursor._appStateMonitor.MainWindowHandle = value;
  }

  /// <summary>
  /// Starts the auto wait cursor monitoring the application.
  /// </summary>
  public static void Start() => AutoWaitCursor._appStateMonitor.Start();

  /// <summary>
  /// Stops the auto wait cursor monitoring the application.
  /// </summary>
  public static void Stop() => AutoWaitCursor._appStateMonitor.Stop();

  /// <summary>
  /// Private class that monitors the state of the application and automatically
  /// changes the cursor accordingly.
  /// </summary>
  private class ApplicationStateMonitor : IDisposable
  {
    /// <summary>The time the application became inactive.</summary>
    private DateTime _inactiveStart = DateTime.Now;
    /// <summary>If the monitor has been started.</summary>
    private bool _isStarted;
    /// <summary>Delay to wait before calling back</summary>
    private TimeSpan _delay;
    /// <summary>The windows handle to the main process window.</summary>
    private IntPtr _mainWindowHandle = IntPtr.Zero;
    /// <summary>Thread to perform the wait and callback</summary>
    private Thread _callbackThread;
    /// <summary>Stores if the class has been disposed of.</summary>
    private bool _isDisposed;
    /// <summary>Stores if the class is enabled or not.</summary>
    private bool _enabled = true;
    /// <summary>GUI Thread Id .</summary>
    private uint _mainThreadId;
    /// <summary>Callback Thread Id.</summary>
    private uint _callbackThreadId;
    /// <summary>Stores the old cursor.</summary>
    private Cursor _oldCursor;
    /// <summary>Stores the new cursor.</summary>
    private Cursor _waitCursor;
    private const int SMTO_NORMAL = 0;
    private const int SMTO_BLOCK = 1;
    private const int SMTO_NOTIMEOUTIFNOTHUNG = 8;
    private const int ERROR_TIMEOUT = 1460;
    private const int INFINITE = 2147483647 /*0x7FFFFFFF*/;
    private const int WM_NULL = 0;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern IntPtr SendMessageTimeout(
      HandleRef hWnd,
      int msg,
      IntPtr wParam,
      IntPtr lParam,
      int flags,
      int timeout,
      out IntPtr pdwResult);

    [DllImport("USER32.DLL")]
    private static extern uint AttachThreadInput(uint attachTo, uint attachFrom, bool attach);

    [DllImport("KERNEL32.DLL")]
    private static extern uint GetCurrentThreadId();

    [DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
    public static extern uint GetLastError();

    /// <summary>Default member initialising Constructor</summary>
    /// <param name="waitCursor">The wait cursor to use.</param>
    /// <param name="delay">The delay before setting the cursor to the wait cursor.</param>
    public ApplicationStateMonitor(Cursor waitCursor, TimeSpan delay)
    {
      this._mainThreadId = AutoWaitCursor.ApplicationStateMonitor.GetCurrentThreadId();
      this._delay = delay;
      this._waitCursor = waitCursor;
      Application.ThreadExit += new EventHandler(this._OnApplicationThreadExit);
    }

    /// <summary>
    /// On Disposal terminates the Thread, calls Finish (on thread) if Start has been called
    /// </summary>
    public void Dispose()
    {
      if (this._isDisposed)
        return;
      this._isDisposed = true;
    }

    /// <summary>Starts the application state monitor.</summary>
    public void Start()
    {
      if (this._isStarted)
        return;
      this._isStarted = true;
      this._CreateMonitorThread();
    }

    /// <summary>Stops the application state monitor.</summary>
    public void Stop()
    {
      if (!this._isStarted)
        return;
      this._isStarted = false;
    }

    /// <summary>Set the Cursor to wait.</summary>
    public void SetWaitCursor()
    {
      this._callbackThreadId = AutoWaitCursor.ApplicationStateMonitor.GetCurrentThreadId();
      int num = (int) AutoWaitCursor.ApplicationStateMonitor.AttachThreadInput(this._callbackThreadId, this._mainThreadId, true);
      this._oldCursor = Cursor.Current;
      Cursor.Current = this._waitCursor;
    }

    /// <summary>
    /// Finish showing the Cursor (switch back to previous Cursor)
    /// </summary>
    public void RestoreCursor()
    {
      Cursor.Current = this._oldCursor;
      int num = (int) AutoWaitCursor.ApplicationStateMonitor.AttachThreadInput(this._callbackThreadId, this._mainThreadId, false);
    }

    /// <summary>
    /// Enable/Disable the call to Start (note, once Start is called it *always* calls the paired Finish)
    /// </summary>
    public bool Enabled
    {
      get => this._enabled;
      set => this._enabled = value;
    }

    /// <summary>
    /// Gets or sets the period of Time to wait before calling the Start method
    /// </summary>
    public TimeSpan Delay
    {
      get => this._delay;
      set => this._delay = value;
    }

    /// <summary>
    /// Returns true if the auto wait cursor has been started.
    /// </summary>
    public bool IsStarted => this._isStarted;

    /// <summary>
    /// Gets or sets the main window handle of the application (ie the handle of an MDI form).
    /// This is the window handle monitored to detect when the application becomes busy.
    /// </summary>
    public IntPtr MainWindowHandle
    {
      get => this._mainWindowHandle;
      set => this._mainWindowHandle = value;
    }

    /// <summary>Gets or sets the Cursor to show</summary>
    public Cursor Cursor
    {
      get => this._waitCursor;
      set => this._waitCursor = value;
    }

    /// <summary>
    /// Returns the amount of time the application has been idle.
    /// </summary>
    public TimeSpan ApplicationIdleTime => DateTime.Now.Subtract(this._inactiveStart);

    /// <summary>
    /// Prepares the class creating a Thread that monitors the main application state.
    /// </summary>
    private void _CreateMonitorThread()
    {
      this._callbackThread = new Thread(new ThreadStart(this._ThreadCallbackLoop));
      this._callbackThread.Name = "AutoWaitCursorCallback";
      this._callbackThread.IsBackground = true;
      this._callbackThread.Start();
    }

    /// <summary>
    /// Thread callback method.
    /// Loops calling SetWaitCursor and RestoreCursor until Disposed.
    /// </summary>
    private void _ThreadCallbackLoop()
    {
      try
      {
        do
        {
          if (!this._enabled || this._mainWindowHandle == IntPtr.Zero)
            Thread.Sleep(this._delay);
          else if (this._IsApplicationBusy(this._delay, this._mainWindowHandle))
          {
            if (this._enabled)
            {
              try
              {
                this.SetWaitCursor();
                this._WaitForIdle();
              }
              finally
              {
                this.RestoreCursor();
                this._inactiveStart = DateTime.Now;
              }
            }
          }
          else
            Thread.Sleep(25);
        }
        while (!this._isDisposed && this._isStarted);
      }
      catch (ThreadAbortException ex)
      {
        Thread.ResetAbort();
      }
    }

    /// <summary>
    /// Blocks until the application responds to a test message.
    /// If the application doesn't respond with the timespan, will return false,
    /// else returns true.
    /// </summary>
    private bool _IsApplicationBusy(TimeSpan delay, IntPtr windowHandle)
    {
      IntPtr pdwResult = IntPtr.Zero;
      if (delay == TimeSpan.MaxValue)
        AutoWaitCursor.ApplicationStateMonitor.SendMessageTimeout(new HandleRef((object) null, windowHandle), 0, IntPtr.Zero, IntPtr.Zero, 1, int.MaxValue, out pdwResult);
      else if (AutoWaitCursor.ApplicationStateMonitor.SendMessageTimeout(new HandleRef((object) null, windowHandle), 0, IntPtr.Zero, IntPtr.Zero, 1, Convert.ToInt32(delay.TotalMilliseconds), out pdwResult) == (IntPtr) 0 && AutoWaitCursor.ApplicationStateMonitor.GetLastError() == 1460U)
        pdwResult = (IntPtr) 1;
      return pdwResult != (IntPtr) 0;
    }

    /// <summary>
    /// Waits for the ResetEvent (set by Dispose and Reset),
    /// since Start has been called we *have* to call RestoreCursor once the thread is idle again.
    /// </summary>
    private void _WaitForIdle()
    {
      this._IsApplicationBusy(TimeSpan.MaxValue, this._mainWindowHandle);
    }

    /// <summary>
    /// The application is closing, shut the state monitor down.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void _OnApplicationThreadExit(object sender, EventArgs e) => this.Dispose();
  }
}
