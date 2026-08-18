// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.OpenFilesHandler
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Integrators;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

internal sealed class OpenFilesHandler(IIntegrator integrator) : IntegratorOpenFilesHandler<object>((IServiceProvider) integrator)
{
  protected override bool IsAppFileOpen(string filePath)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenFilesHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p2 = OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> target2 = OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> p1 = OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (MSWordApiHelper), application, filePath);
      object obj2 = target2((CallSite) p1, obj1, (object) null);
      return target1((CallSite) p2, obj2);
    }
  }

  protected override bool IsAppFileDirty(string filePath)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (MSWordApiHelper), application, filePath);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenFilesHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p6 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__1.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__1, obj1, (object) null);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj3;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__5.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__5, obj2))
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target2 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p4 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__4;
        object obj4 = obj2;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object> target3 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object>> p3 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Saved", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__2, obj1);
        object obj6 = target3((CallSite) p3, obj5);
        obj3 = target2((CallSite) p4, obj4, obj6);
      }
      else
        obj3 = obj2;
      return target1((CallSite) p6, obj3);
    }
  }

  protected override void SaveAppFile(string filePath)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__0, typeof (MSWordApiHelper), application, filePath);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p10 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__10;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__1.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__1, obj1, (object) null);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj3;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__5.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__5, obj2))
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target2 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p4 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__4;
        object obj4 = obj2;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object> target3 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object>> p3 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Saved", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj5 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__2, obj1);
        object obj6 = target3((CallSite) p3, obj5);
        obj3 = target2((CallSite) p4, obj4, obj6);
      }
      else
        obj3 = obj2;
      object obj7 = obj3;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj8;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__9.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__9, obj7))
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target4 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__8.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p8 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__8;
        object obj9 = obj7;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object> target5 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object>> p7 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__7;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ReadOnly", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj10 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__6.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__6, obj1);
        object obj11 = target5((CallSite) p7, obj10);
        obj8 = target4((CallSite) p8, obj9, obj11);
      }
      else
        obj8 = obj7;
      if (!target1((CallSite) p10, obj8))
        return;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__11 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Save", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__11.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__11, obj1);
    }
  }

  protected override object UnloadAppFiles(ICollection<string> applicationFiles)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, MsoSavedState>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (MsoSavedState), typeof (OpenFilesHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, MsoSavedState> target1 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, MsoSavedState>> p1 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SaveState", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__0, typeof (MSWordApiHelper), application);
      MsoSavedState msoSavedState = target1((CallSite) p1, obj1);
      bool flag = false;
      foreach (string applicationFile in (IEnumerable<string>) applicationFiles)
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__2, typeof (MSWordApiHelper), application, applicationFile);
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target2 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__4.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p4 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__4;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__3.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__3, obj2, (object) null);
        if (target2((CallSite) p4, obj3))
        {
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__5 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__5 = CallSite<Action<CallSite, object, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__5.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__5, obj2, false);
          flag = ((flag ? 1 : 0) | 1) != 0;
        }
      }
      return flag ? (object) msoSavedState : (object) (MsoSavedState) null;
    }
  }

  protected override object UnloadAllAppFiles()
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, MsoSavedState>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (MsoSavedState), typeof (OpenFilesHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, MsoSavedState> target1 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, MsoSavedState>> p1 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "SaveState", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__0, typeof (MSWordApiHelper), application);
      MsoSavedState msoSavedState = target1((CallSite) p1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (OpenFilesHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, IEnumerable> target2 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__4.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, IEnumerable>> p4 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__4;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__2, application);
      foreach (object obj3 in target2((CallSite) p4, obj2))
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Action<CallSite, object, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__3, obj3, false);
      }
      return (object) msoSavedState;
    }
  }

  protected override void ReloadAppState(object reloadState)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      MsoSavedState msoSavedState = (MsoSavedState) reloadState;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Action<CallSite, Type, object, MsoSavedState>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RestoreState", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__6.\u003C\u003Ep__0, typeof (MSWordApiHelper), application, msoSavedState);
    }
  }

  protected override void SetAppReadOnlyFlag(string filePath, bool readOnlyFlag)
  {
    using (MSWordApiSession msWordApiSession = new MSWordApiSession(this.apiSvc))
    {
      object application = msWordApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveDocument", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__0, application);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__1, typeof (MSWordApiHelper), application, filePath);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (OpenFilesHandler)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p3 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Equals", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__2.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__2, obj1, obj2);
      bool flag = target1((CallSite) p3, obj3);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target2 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__9.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p9 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__9;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__4.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__4, obj2, (object) null);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj5;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__8.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__8, obj4))
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target3 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__7.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p7 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__7;
        object obj6 = obj4;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool, object> target4 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__6.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool, object>> p6 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__6;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ReadOnly", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj7 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__5.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__5, obj2);
        int num = readOnlyFlag ? 1 : 0;
        object obj8 = target4((CallSite) p6, obj7, num != 0);
        obj5 = target3((CallSite) p7, obj6, obj8);
      }
      else
        obj5 = obj4;
      if (!target2((CallSite) p9, obj5))
        return;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Start", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target5 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p13 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Selection", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target6 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p12 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target7 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p11 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Windows", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj9 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10, obj2);
      object obj10 = target7((CallSite) p11, obj9, 1);
      object obj11 = target6((CallSite) p12, obj10);
      object obj12 = target5((CallSite) p13, obj11);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "End", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target8 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p17 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Selection", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target9 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p16 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target10 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p15 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Windows", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj13 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14, obj2);
      object obj14 = target10((CallSite) p15, obj13, 1);
      object obj15 = target9((CallSite) p16, obj14);
      object obj16 = target8((CallSite) p17, obj15);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18, obj2, 0);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19 = CallSite<Func<CallSite, Type, object, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "OpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj17 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19, typeof (MSWordApiHelper), application, filePath, (object) null);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20 = CallSite<\u003C\u003EF\u007B0000000c\u007D<CallSite, object, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Range", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsRef, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsRef, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj18 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20, obj17, ref obj12, ref obj16);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Select", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21, obj18);
      if (flag)
        return;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22, obj1);
    }
  }
}
