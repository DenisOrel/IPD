
// Type: WaitCursor.ApplicationWaitCursor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace WaitCursor;

/// <summary>
/// Singleton Utility class which is used to show a Wait Cursor when the Application is busy.
/// If the Application is busy then the Idle event will not be called during the busy period
/// and hence the Screen Cursor is automatically changed to a (by default) WaitCursor.
/// 
/// To use, simply insert the following line in your Application startup code
/// 
/// 	ApplicationWaitCursor.Cursor = Cursors.Wait;
/// 	ApplicationWaitCursor.Delay  = new TimeSpan(0, 0, 0, 0, 100);
/// 
/// This installs a StCursor to activate after 100ms of 'work' (Application.Idle not being called)
/// 
/// </summary>
public class ApplicationWaitCursor : IMessageFilter
{
  /// <summary>The Cursor to use during busy periods</summary>
  private static StCursor _cursor = new StCursor(Cursors.WaitCursor, false);
  private static int _cursorPos = 0;
  private static EventHandler _applicationIdleEventHandler = (EventHandler) null;
  private static ApplicationWaitCursor _singleton = (ApplicationWaitCursor) null;
  /// <summary>None Client Area Button Down Windows Message</summary>
  private const int WM_NCLBUTTONDOWN = 161;
  private const int WM_RBUTTONUP = 517;
  private const int WM_LBUTTONDOWN = 513;
  private const int WM_CONTEXTMENU = 123;
  private const int WM_SYSKEYUP = 261;
  private const int WM_SYSKEYDOWN = 260;
  private const int WM_RBUTTONDOWN = 516;
  private const int WM_MOUSEMOVE = 512 /*0x0200*/;

  /// <summary>Default Constructor.  Hidden</summary>
  private ApplicationWaitCursor()
  {
  }

  /// <summary>
  /// Static constructor which attaches to the Singleton Application instance
  /// </summary>
  static ApplicationWaitCursor()
  {
    ApplicationWaitCursor._singleton = new ApplicationWaitCursor();
    ApplicationWaitCursor._applicationIdleEventHandler = new EventHandler(ApplicationWaitCursor.OnApplicationIdle);
  }

  /// <summary>
  /// Gets and Sets the Cursor to use during Application busy periods.  Setting this to NULL will disable the
  /// monitoring of busy periods.
  /// </summary>
  public static Cursor Cursor
  {
    get => ApplicationWaitCursor._cursor.Cursor;
    set
    {
      if (value != (Cursor) null && !ApplicationWaitCursor._cursor.Enabled)
      {
        Application.Idle += ApplicationWaitCursor._applicationIdleEventHandler;
        Application.AddMessageFilter((IMessageFilter) ApplicationWaitCursor._singleton);
      }
      else if (value == (Cursor) null && ApplicationWaitCursor._cursor.Enabled)
      {
        Application.Idle -= ApplicationWaitCursor._applicationIdleEventHandler;
        Application.RemoveMessageFilter((IMessageFilter) ApplicationWaitCursor._singleton);
      }
      ApplicationWaitCursor._cursor.Cursor = value;
      ApplicationWaitCursor._cursor.Enabled = value != (Cursor) null;
    }
  }

  /// <summary>
  /// Get/Set the period of Time to wait before showing the WaitCursor whilst Application is working
  /// </summary>
  public static TimeSpan Delay
  {
    get => ApplicationWaitCursor._cursor.Delay;
    set => ApplicationWaitCursor._cursor.Delay = value;
  }

  /// <summary>
  /// Process the Idle event.  Simply reset the StWaitCursor
  /// </summary>
  private static void OnApplicationIdle(object sender, EventArgs e)
  {
    ApplicationWaitCursor._cursor.Reset();
  }

  /// <summary>
  /// Pre-Filters Windows messages.  During Window Moves/Resizes the Application Idle is not called (appears busy)
  /// so we filter for these events so we can temporarily turn off the WaitCursor
  /// </summary>
  bool IMessageFilter.PreFilterMessage(ref Message m)
  {
    if (m.Msg == 161 || m.Msg == 513 || m.Msg == 517 || m.Msg == 516 || m.Msg == 123 || m.Msg == 261 || m.Msg == 260)
    {
      ApplicationWaitCursor._cursor.Enabled = false;
    }
    else
    {
      ApplicationWaitCursor._cursor.Enabled = true;
      if (m.Msg == 512 /*0x0200*/)
      {
        if (ApplicationWaitCursor._cursorPos == (int) m.LParam)
        {
          ApplicationWaitCursor._cursorPos = 0;
          return true;
        }
        ApplicationWaitCursor._cursorPos = (int) m.LParam;
      }
    }
    return false;
  }
}
