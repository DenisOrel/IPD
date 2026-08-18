
// Type: WaitCursor.StCursor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Windows.Forms;


namespace WaitCursor;

/// <summary>
/// Utility class to make showing (usually) a Wait Cursor much simpler and to remove the
/// possibility of the Cursor not being restored due to an uncaught exception or forgetfulness to restore
/// the cursor manually.
/// 
/// 2 Possible uses for this class :-
/// 
/// 1.  Single instance usage of the StCursor ..
/// Instead of
/// 
/// public void DoSomeLengthyWork()
/// {
/// 	try
/// 	{
/// 		Screen.Cursor = Cursors.Wait;
/// 
/// 		SlowlyCountToTenBillion();
/// 	}
/// 	finally
/// 	{
/// 		Screen.Cursor = Cursors.Default;
/// 	}
/// }
/// 
/// do this ..
/// 
/// public void DoSomeLengthyWork()
/// {
/// 	using (new StCursor(Cursors.Wait, new TimeSpan(0, 0, 0, 0, 100)))
/// 	{
/// 		SlowlyCountToTenBillion();
/// 	}
/// }
/// 
/// Above code will show the Wait cursor after 100ms of 'work'.
/// It makes use of the 'using' statement and IDispose to *make sure* the Cursor is always restored
/// 
/// 2.  Global usage of the StCursor (<see cref="T:WaitCursor.ApplicationWaitCursor" /> class for usage)
/// 
/// </summary>
public class StCursor : StThreadAttachedDelayedCallback, IDelayedCallbackHandler
{
  public static readonly TimeSpan DEFAULT_DELAY = new TimeSpan(0, 0, 0, 0, 500);
  private Cursor _oldCursor;
  private Cursor _newCursor;

  /// <summary>Member initialising Constructor</summary>
  /// <param name="newCursor">The Cursor to use</param>
  /// <param name="delay">Delay period before showing Cursor</param>
  /// <param name="enabled">Enable or Not</param>
  public StCursor(Cursor newCursor, TimeSpan delay, bool enabled)
    : base(delay, enabled)
  {
    this._newCursor = newCursor;
  }

  /// <summary>Member initialising Constructor</summary>
  /// <param name="newCursor">The Cursor to use</param>
  /// <param name="delay">Delay period before showing Cursor</param>
  public StCursor(Cursor newCursor, TimeSpan delay)
    : this(newCursor, delay, true)
  {
  }

  /// <summary>Member initialising Constructor</summary>
  /// <param name="newCursor">The Cursor to use</param>
  public StCursor(Cursor newCursor)
    : this(newCursor, StCursor.DEFAULT_DELAY)
  {
  }

  /// <summary>Member initialising Constructor</summary>
  /// <param name="newCursor">The Cursor to use</param>
  /// <param name="enabled">Enable or Not</param>
  public StCursor(Cursor newCursor, bool enabled)
    : this(newCursor, StCursor.DEFAULT_DELAY, enabled)
  {
  }

  /// <summary>Start showing the Cursor now</summary>
  public override void Start()
  {
    base.Start();
    if (this._newCursor != (Cursor) null && Cursor.Current != (Cursor) null && Cursor.Current.Handle == this._newCursor.Handle)
      return;
    this._oldCursor = Cursor.Current;
    Cursor.Current = this._newCursor;
  }

  /// <summary>
  /// Finish showing the Cursor (switch back to previous Cursor)
  /// </summary>
  public override void Finish()
  {
    if (Cursor.Current == (Cursor) null)
      return;
    int handle = (int) Cursor.Current.Handle;
    if (this._oldCursor != (Cursor) null && (int) this._oldCursor.Handle == handle)
    {
      base.Finish();
    }
    else
    {
      Cursor.Current = this._oldCursor;
      base.Finish();
    }
  }

  /// <summary>Get/Set the Cursor to show</summary>
  public Cursor Cursor
  {
    get => this._newCursor;
    set => this._newCursor = value;
  }
}
