
// Type: WaitCursor.StThreadAttachedDelayedCallback
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Runtime.InteropServices;


namespace WaitCursor;

/// <summary>
/// Base class for StDelayedCallback classes that require ThreadInput from the Main thread
/// 
/// This class is a client of itself in that it implements the IDelayedCallbackHandler interface.
/// </summary>
public class StThreadAttachedDelayedCallback : StDelayedCallback, IDelayedCallbackHandler
{
  /// <summary>GUI Thread Id</summary>
  private uint _mainThreadId;
  /// <summary>Callback Thread Id</summary>
  private uint _callbackThreadId;

  [DllImport("USER32.DLL")]
  private static extern uint AttachThreadInput(uint attachTo, uint attachFrom, bool attach);

  [DllImport("KERNEL32.DLL")]
  private static extern uint GetCurrentThreadId();

  /// <summary>Member Initialising Constructor.</summary>
  /// <param name="delay">Delay to wait for</param>
  /// <param name="enabled">Enabled or not</param>
  public StThreadAttachedDelayedCallback(TimeSpan delay, bool enabled)
  {
    this._mainThreadId = StThreadAttachedDelayedCallback.GetCurrentThreadId();
    this.Init((IDelayedCallbackHandler) this, delay, enabled);
  }

  /// <summary>Member Initialising Constructor.</summary>
  /// <param name="delay">Delay to wait for</param>
  public StThreadAttachedDelayedCallback(TimeSpan delay)
    : this(delay, true)
  {
  }

  /// <summary>
  /// Start.  Called when the Delay has expired and operation is to begin.
  /// This implementation attaches this Thread to the Main Thread's Input.
  /// </summary>
  public virtual void Start()
  {
    this._callbackThreadId = StThreadAttachedDelayedCallback.GetCurrentThreadId();
    int num = (int) StThreadAttachedDelayedCallback.AttachThreadInput(this._callbackThreadId, this._mainThreadId, true);
  }

  /// <summary>
  /// Finish.  Called when the operation is to finish (usually IDispose)
  /// This implementation detaches this Thread from the Main Thread's Input.
  /// </summary>
  public virtual void Finish()
  {
    int num = (int) StThreadAttachedDelayedCallback.AttachThreadInput(this._callbackThreadId, this._mainThreadId, false);
  }
}
