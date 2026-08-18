// Decompiled with JetBrains decompiler
// Type: Intermech.TwainScanner.ImTwainScanner
// Assembly: Intermech.TwainScanner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0CEE3C76-D3AF-4F98-AB07-F18794839283
// Assembly location: D:\IPS\Client\Intermech.TwainScanner.exe
// XML documentation location: D:\IPS\Client\Intermech.TwainScanner.xml

using Intermech.Archives.ScanDocums;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.TwainScanner;

[ClassInterface(ClassInterfaceType.None)]
[ComSourceInterfaces(typeof (ICSSimpleObjectEvents))]
[Guid("3494789E-2865-4D27-9E07-92C39BD5AA40")]
[ComVisible(true)]
public class ImTwainScanner : ReferenceCountedObject, IImTwainScanner, IInitDone
{
  internal const string ClassId = "3494789E-2865-4D27-9E07-92C39BD5AA40";
  internal const string InterfaceId = "932F0738-6A3C-49D0-916D-E42CE41FE15B";
  internal const string EventsId = "2FAF539E-40B7-450B-92B8-2546AAB51DF4";
  private float fField;
  private static object iPSPlugin;
  private static object dimirObject;
  [NonSerialized]
  internal static OnImageTransferEventHandler onImageTransfer;
  private ScanerDocumentService scanerService;
  [NonSerialized]
  internal static OnEndScaningEventHandler onEndScaning;
  [NonSerialized]
  internal static ProgressChangedEventHandler progressChanged;

  [EditorBrowsable(EditorBrowsableState.Never)]
  [ComRegisterFunction]
  public static void Register(Type t)
  {
    try
    {
      COMHelper.RegasmRegisterLocalServer(t);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw ex;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  [ComUnregisterFunction]
  public static void Unregister(Type t)
  {
    try
    {
      COMHelper.RegasmUnregisterLocalServer(t);
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw ex;
    }
  }

  public float FloatProperty
  {
    get => this.fField;
    set
    {
      bool Cancel = false;
      if (this.FloatPropertyChanging != null)
        this.FloatPropertyChanging(value, ref Cancel);
      if (Cancel)
        return;
      this.fField = value;
    }
  }

  public object DimirObject
  {
    get => ImTwainScanner.dimirObject;
    set => ImTwainScanner.dimirObject = value;
  }

  public object IPSPlugin
  {
    get => ImTwainScanner.iPSPlugin;
    set => ImTwainScanner.iPSPlugin = value;
  }

  public string HelloWorld() => "HelloWorld222  " + Environment.Is64BitProcess.ToString();

  public void GetProcessThreadID(out uint processId, out uint threadId)
  {
    processId = NativeMethod.GetCurrentProcessId();
    threadId = NativeMethod.GetCurrentThreadId();
  }

  public void ChangeProgress(int val)
  {
    if (this.DimirObject == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (ImTwainScanner.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImTwainScanner.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, nameof (ChangeProgress), (IEnumerable<Type>) null, typeof (ImTwainScanner), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ImTwainScanner.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) ImTwainScanner.\u003C\u003Eo__19.\u003C\u003Ep__0, this.DimirObject, val);
  }

  public event FloatPropertyChangingEventHandler FloatPropertyChanging;

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event OnImageTransferEventHandler OnImageTransfer
  {
    add => ImTwainScanner.onImageTransfer += value;
    remove => ImTwainScanner.onImageTransfer -= value;
  }

  public void AcquireDoc(string fileExt)
  {
    if (this.scanerService == null)
    {
      this.scanerService = new ScanerDocumentService();
      this.scanerService.OnEndScaning += new EventHandler(this.scanerService_OnEndScaning);
      this.scanerService.OnImageTransfer += new EventHandler(this.scanerService_OnImageTransfer);
    }
    this.scanerService.AcquireDoc(fileExt);
  }

  private void scanerService_OnImageTransfer(object sender, EventArgs e)
  {
    if (!(sender is byte[] numArray) || ImTwainScanner.iPSPlugin == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (ImTwainScanner.\u003C\u003Eo__29.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImTwainScanner.\u003C\u003Eo__29.\u003C\u003Ep__0 = CallSite<Action<CallSite, object, byte[]>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "OnImageTransferMethod", (IEnumerable<Type>) null, typeof (ImTwainScanner), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ImTwainScanner.\u003C\u003Eo__29.\u003C\u003Ep__0.Target((CallSite) ImTwainScanner.\u003C\u003Eo__29.\u003C\u003Ep__0, ImTwainScanner.iPSPlugin, numArray);
  }

  private void scanerService_OnEndScaning(object sender, EventArgs e)
  {
    if (ImTwainScanner.iPSPlugin == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (ImTwainScanner.\u003C\u003Eo__30.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ImTwainScanner.\u003C\u003Eo__30.\u003C\u003Ep__0 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "OnEndScaningMethod", (IEnumerable<Type>) null, typeof (ImTwainScanner), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ImTwainScanner.\u003C\u003Eo__30.\u003C\u003Ep__0.Target((CallSite) ImTwainScanner.\u003C\u003Eo__30.\u003C\u003Ep__0, ImTwainScanner.iPSPlugin);
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event OnEndScaningEventHandler OnEndScaning
  {
    add => ImTwainScanner.onEndScaning += value;
    remove => ImTwainScanner.onEndScaning -= value;
  }

  /// <summary>Событие после деактивации редактора по месту</summary>
  public event ProgressChangedEventHandler ProgressChanged
  {
    add => ImTwainScanner.progressChanged += value;
    remove => ImTwainScanner.progressChanged -= value;
  }

  public void Init(object connection) => ImTwainScanner.iPSPlugin = connection;

  public void Done() => ImTwainScanner.iPSPlugin = (object) null;

  public void GetInfo(ref object connection) => connection = ImTwainScanner.iPSPlugin;

  public byte[] GetData(byte[] data) => data;
}
