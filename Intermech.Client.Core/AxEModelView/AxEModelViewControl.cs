
// Type: AxEModelView.AxEModelViewControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;


namespace AxEModelView;

/// <summary>
/// 
/// </summary>
public class AxEModelViewControl(string clsid) : AxHost(clsid)
{
  private object ocx;

  public virtual bool OpenDoc(
    string fileName,
    bool isTemp,
    bool promptToSave,
    bool readOnly,
    string commandString)
  {
    // ISSUE: reference to a compiler-generated field
    if (AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (AxEModelViewControl), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (AxEModelViewControl), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__0, this.ocx, (object) null);
    if (target((CallSite) p1, obj))
      throw new AxHost.InvalidActiveXStateException(nameof (OpenDoc), AxHost.ActiveXInvokeKind.MethodInvoke);
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Action<CallSite, object, string, bool, bool, bool, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, nameof (OpenDoc), (IEnumerable<System.Type>) null, typeof (AxEModelViewControl), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[6]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) AxEModelViewControl.\u003C\u003Eo__2.\u003C\u003Ep__2, this.ocx, fileName, isTemp, promptToSave, readOnly, commandString);
      return true;
    }
    catch
    {
    }
    return false;
  }

  public virtual void CloseActiveDoc(string commandString)
  {
    // ISSUE: reference to a compiler-generated field
    if (AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (AxEModelViewControl), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (AxEModelViewControl), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__0, this.ocx, (object) null);
    if (target((CallSite) p1, obj))
      throw new AxHost.InvalidActiveXStateException(nameof (CloseActiveDoc), AxHost.ActiveXInvokeKind.MethodInvoke);
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Action<CallSite, object, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, nameof (CloseActiveDoc), (IEnumerable<System.Type>) null, typeof (AxEModelViewControl), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) AxEModelViewControl.\u003C\u003Eo__3.\u003C\u003Ep__2, this.ocx, commandString);
    }
    catch
    {
    }
  }

  protected override void AttachInterfaces() => this.ocx = this.GetOcx();
}
