
// Type: WaitCursor.StDelayedCallback
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Threading;


namespace WaitCursor;

/// <summary>
/// This class manages a IDelayedCallbackHandler.  After a specified delay the
/// <see cref="M:WaitCursor.IDelayedCallbackHandler.Start" /> method is called.
/// If the <see cref="M:WaitCursor.IDelayedCallbackHandler.Start" /> is called then this guarantees that the
/// <see cref="M:WaitCursor.IDelayedCallbackHandler.Finish" /> method is called when this instance is Disposed.
/// <seealso cref="T:WaitCursor.StCursor" /> for an implementation
/// </summary>
public class StDelayedCallback : IDisposable
{
  /// <summary>The callback</summary>
  private IDelayedCallbackHandler _callbackHandler;
  /// <summary>Delay to wait before calling back</summary>
  private TimeSpan _delay;
  /// <summary>Thread to perform the wait and callback</summary>
  private Thread _callbackThread;
  /// <summary>Have we been Disposed or not ?</summary>
  private bool _disposed;
  /// <summary>Has callback Start been called ?</summary>
  private bool _startCalled;
  /// <summary>WaitHandle for notifications</summary>
  private ManualResetEvent _resetEvent = new ManualResetEvent(false);
  /// <summary>Enabled or not ?</summary>
  private bool _enabled = true;

  /// <summary>Default Constructor.  Hidden.</summary>
  protected StDelayedCallback()
  {
  }

  /// <summary>
  /// Creates a StDelayedCallback instance prepared with a <see cref="T:WaitCursor.IDelayedCallbackHandler" /> and the specified <see cref="T:System.TimeSpan" /> delay
  /// </summary>
  /// <param name="callbackHandler">The CallbackHandler to use</param>
  /// <param name="delay">Initial Delay value</param>
  /// <param name="enabled">Initial Enabled state</param>
  public StDelayedCallback(IDelayedCallbackHandler callbackHandler, TimeSpan delay, bool enabled)
  {
    this.Init(callbackHandler, delay, enabled);
  }

  /// <summary>
  /// Prepares the class.  Creates the Thread that will call Start - Finish
  /// </summary>
  /// <param name="callbackHandler"></param>
  /// <param name="delay"></param>
  /// <param name="enabled"></param>
  protected void Init(IDelayedCallbackHandler callbackHandler, TimeSpan delay, bool enabled)
  {
    this._callbackHandler = callbackHandler;
    this._delay = delay;
    this._enabled = enabled;
    this._callbackThread = new Thread(new ThreadStart(this.CallbackThread));
    this._callbackThread.Name = this.GetType().Name + " DelayedCallback Thread";
    this._callbackThread.IsBackground = true;
    this._callbackThread.Start();
  }

  /// <summary>
  /// Thread method.  Loops calling Start - Finish until Disposed, honours the Enabled flag
  /// </summary>
  private void CallbackThread()
  {
    do
    {
      this._startCalled = false;
      this.WaitToStart();
      if (this._startCalled)
        this.WaitForReset();
    }
    while (!this._disposed);
  }

  /// <summary>
  /// Waits for either the ResetEvent or the Wait period to expire.  If Wait period expires then Start is called
  /// </summary>
  private void WaitToStart()
  {
    int num = this._resetEvent.WaitOne(this._delay, false) ? 1 : 0;
    this._resetEvent.Reset();
    if (num != 0)
      return;
    if (!this._enabled)
      return;
    try
    {
      this._callbackHandler.Start();
    }
    finally
    {
      this._startCalled = true;
    }
  }

  /// <summary>
  /// Waits for the ResetEvent (set by Dispose - Reset), since Start has been called we *have* to call Finish
  /// </summary>
  private void WaitForReset()
  {
    this._resetEvent.WaitOne();
    this._resetEvent.Reset();
    this._callbackHandler.Finish();
  }

  /// <summary>Resets the Wait period to start Waiting again</summary>
  public void Reset() => this._resetEvent.Set();

  /// <summary>
  /// On Disposal terminates the Thread, calls Finish (on thread) if Start has been called
  /// </summary>
  public void Dispose()
  {
    if (this._disposed)
      return;
    this._disposed = true;
    this._resetEvent.Set();
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
  /// Get/Set the period of Time to wait before calling the Start method
  /// </summary>
  public TimeSpan Delay
  {
    get => this._delay;
    set => this._delay = value;
  }
}
