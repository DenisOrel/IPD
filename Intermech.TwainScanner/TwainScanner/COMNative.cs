// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.COMNative
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

internal class COMNative
{
  /// <summary>Interface Id of IClassFactory</summary>
  public const string IID_IClassFactory = "00000001-0000-0000-C000-000000000046";
  /// <summary>Interface Id of IUnknown</summary>
  public const string IID_IUnknown = "00000000-0000-0000-C000-000000000046";
  /// <summary>Interface Id of IDispatch</summary>
  public const string IID_IDispatch = "00020400-0000-0000-C000-000000000046";
  /// <summary>
  /// Class does not support aggregation (or class object is remote)
  /// </summary>
  public const int CLASS_E_NOAGGREGATION = -2147221232;
  /// <summary>No such interface supported</summary>
  public const int E_NOINTERFACE = -2147467262 /*0x80004002*/;

  /// <summary>
  /// CoInitializeEx() can be used to set the apartment model of individual
  /// threads.
  /// </summary>
  /// <param name="pvReserved">Must be NULL</param>
  /// <param name="dwCoInit">
  /// The concurrency model and initialization options for the thread
  /// </param>
  /// <returns></returns>
  [DllImport("ole32.dll")]
  public static extern int CoInitializeEx(IntPtr pvReserved, uint dwCoInit);

  /// <summary>
  /// CoUninitialize() is used to uninitialize a COM thread.
  /// </summary>
  [DllImport("ole32.dll")]
  public static extern void CoUninitialize();

  /// <summary>
  /// Registers an EXE class object with OLE so other applications can
  /// connect to it. EXE object applications should call
  /// CoRegisterClassObject on startup. It can also be used to register
  /// internal objects for use by the same EXE or other code (such as DLLs)
  /// that the EXE uses.
  /// </summary>
  /// <param name="rclsid">CLSID to be registered</param>
  /// <param name="pUnk">
  /// Pointer to the IUnknown interface on the class object whose
  /// availability is being published.
  /// </param>
  /// <param name="dwClsContext">
  /// Context in which the executable code is to be run.
  /// </param>
  /// <param name="flags">
  /// How connections are made to the class object.
  /// </param>
  /// <param name="lpdwRegister">
  /// Pointer to a value that identifies the class object registered;
  /// </param>
  /// <returns></returns>
  /// <remarks>
  /// PInvoking CoRegisterClassObject to register COM objects is not
  /// supported.
  /// </remarks>
  [DllImport("ole32.dll")]
  public static extern int CoRegisterClassObject(
    ref Guid rclsid,
    [MarshalAs(UnmanagedType.Interface)] IClassFactory pUnk,
    CLSCTX dwClsContext,
    REGCLS flags,
    out uint lpdwRegister);

  /// <summary>
  /// Informs OLE that a class object, previously registered with the
  /// CoRegisterClassObject function, is no longer available for use.
  /// </summary>
  /// <param name="dwRegister">
  /// Token previously returned from the CoRegisterClassObject function
  /// </param>
  /// <returns></returns>
  [DllImport("ole32.dll")]
  public static extern uint CoRevokeClassObject(uint dwRegister);

  /// <summary>
  /// Called by a server that can register multiple class objects to inform
  /// the SCM about all registered classes, and permits activation requests
  /// for those class objects.
  /// </summary>
  /// <returns></returns>
  /// <remarks>
  /// Servers that can register multiple class objects call
  /// CoResumeClassObjects once, after having first called
  /// CoRegisterClassObject, specifying REGCLS_LOCAL_SERVER |
  /// REGCLS_SUSPENDED for each CLSID the server supports. This function
  /// causes OLE to inform the SCM about all the registered classes, and
  /// begins letting activation requests into the server process.
  /// 
  /// This reduces the overall registration time, and thus the server
  /// application startup time, by making a single call to the SCM, no
  /// matter how many CLSIDs are registered for the server. Another
  /// advantage is that if the server has multiple apartments with
  /// different CLSIDs registered in different apartments, or is a free-
  /// threaded server, no activation requests will come in until the server
  /// calls CoResumeClassObjects. This gives the server a chance to
  /// register all of its CLSIDs and get properly set up before having to
  /// deal with activation requests, and possibly shutdown requests.
  /// </remarks>
  [DllImport("ole32.dll")]
  public static extern int CoResumeClassObjects();
}
