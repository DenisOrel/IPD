// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.ExeCOMServer
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Threading;

#nullable disable
namespace Intermech.TwainScanner;

internal sealed class ExeCOMServer
{
  private static ExeCOMServer _instance = new ExeCOMServer();
  private object syncRoot = new object();
  private bool _bRunning;
  private uint _nMainThreadID;
  private int _nLockCnt;
  private Timer _gcTimer;
  private uint _cookieSimpleObj;

  private ExeCOMServer()
  {
  }

  public static ExeCOMServer Instance => ExeCOMServer._instance;

  /// <summary>
  /// The method is call every 5 seconds to GC the managed heap after
  /// the COM server is started.
  /// </summary>
  /// <param name="stateInfo"></param>
  private static void GarbageCollect(object stateInfo) => GC.Collect();

  /// <summary>
  /// PreMessageLoop is responsible for registering the COM class
  /// factories for the COM classes to be exposed from the server, and
  /// initializing the key member variables of the COM server (e.g.
  /// _nMainThreadID and _nLockCnt).
  /// </summary>
  private void PreMessageLoop()
  {
    Guid rclsid = new Guid("3494789E-2865-4D27-9E07-92C39BD5AA40");
    int num1 = COMNative.CoRegisterClassObject(ref rclsid, (IClassFactory) new CSSimpleObjectClassFactory(), CLSCTX.LOCAL_SERVER, REGCLS.MULTIPLEUSE | REGCLS.SUSPENDED, out this._cookieSimpleObj);
    if (num1 != 0)
      throw new ApplicationException("CoRegisterClassObject failed w/err 0x" + num1.ToString("X"));
    int num2 = COMNative.CoResumeClassObjects();
    if (num2 != 0)
    {
      if (this._cookieSimpleObj != 0U)
      {
        int num3 = (int) COMNative.CoRevokeClassObject(this._cookieSimpleObj);
      }
      throw new ApplicationException("CoResumeClassObjects failed w/err 0x" + num2.ToString("X"));
    }
    this._nMainThreadID = NativeMethod.GetCurrentThreadId();
    this._nLockCnt = 0;
    this._gcTimer = new Timer(new TimerCallback(ExeCOMServer.GarbageCollect), (object) null, 5000, 5000);
  }

  /// <summary>
  /// RunMessageLoop runs the standard message loop. The message loop
  /// quits when it receives the WM_QUIT message.
  /// </summary>
  private void RunMessageLoop()
  {
    MSG lpMsg;
    while (NativeMethod.GetMessage(out lpMsg, IntPtr.Zero, 0U, 0U))
    {
      NativeMethod.TranslateMessage(ref lpMsg);
      NativeMethod.DispatchMessage(ref lpMsg);
    }
  }

  /// <summary>
  /// PostMessageLoop is called to revoke the registration of the COM
  /// classes exposed from the server, and perform the cleanups.
  /// </summary>
  private void PostMessageLoop()
  {
    if (this._cookieSimpleObj != 0U)
    {
      int num = (int) COMNative.CoRevokeClassObject(this._cookieSimpleObj);
    }
    if (this._gcTimer != null)
      this._gcTimer.Dispose();
    Thread.Sleep(1000);
  }

  /// <summary>
  /// Run the COM server. If the server is running, the function
  /// returns directly.
  /// </summary>
  /// <remarks>The method is thread-safe.</remarks>
  public void Run()
  {
    lock (this.syncRoot)
    {
      if (this._bRunning)
        return;
      this._bRunning = true;
    }
    try
    {
      this.PreMessageLoop();
      this.RunMessageLoop();
      this.PostMessageLoop();
    }
    finally
    {
      this._bRunning = false;
    }
  }

  /// <summary>Increase the lock count</summary>
  /// <returns>The new lock count after the increment</returns>
  /// <remarks>The method is thread-safe.</remarks>
  public int Lock() => Interlocked.Increment(ref this._nLockCnt);

  /// <summary>
  /// Decrease the lock count. When the lock count drops to zero, post
  /// the WM_QUIT message to the message loop in the main thread to
  /// shut down the COM server.
  /// </summary>
  /// <returns>The new lock count after the increment</returns>
  public int Unlock()
  {
    int num = Interlocked.Decrement(ref this._nLockCnt);
    if (num != 0)
      return num;
    NativeMethod.PostThreadMessage(this._nMainThreadID, 18U, UIntPtr.Zero, IntPtr.Zero);
    return num;
  }

  /// <summary>Get the current lock count.</summary>
  /// <returns></returns>
  public int GetLockCount() => this._nLockCnt;
}
