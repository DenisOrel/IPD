// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.OpenFilesHandler
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
namespace Intermech.Tools.MSOffice.Excel;

internal sealed class OpenFilesHandler(IIntegrator integrator) : IntegratorOpenFilesHandler<object>((IServiceProvider) integrator)
{
  protected override bool IsAppFileOpen(string filePath)
  {
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
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
      object obj1 = OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (ExcelApiHelper), application, filePath);
      object obj2 = target2((CallSite) p1, obj1, (object) null);
      return target1((CallSite) p2, obj2);
    }
  }

  protected override bool IsAppFileDirty(string filePath)
  {
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
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
      object obj1 = OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (ExcelApiHelper), application, filePath);
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
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
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
      object obj1 = OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__3.\u003C\u003Ep__0, typeof (ExcelApiHelper), application, filePath);
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
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
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
      object obj1 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__0, typeof (ExcelApiHelper), application);
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
        object obj2 = OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__4.\u003C\u003Ep__2, typeof (ExcelApiHelper), application, applicationFile);
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
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
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
      object obj1 = OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__5.\u003C\u003Ep__0, typeof (ExcelApiHelper), application);
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
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
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
      OpenFilesHandler.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__6.\u003C\u003Ep__0, typeof (ExcelApiHelper), application, msoSavedState);
    }
  }

  protected override void SetAppReadOnlyFlag(string filePath, bool readOnlyFlag)
  {
    using (ExcelApiSession excelApiSession = new ExcelApiSession(this.apiSvc))
    {
      object application = excelApiSession.Application;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveWorkbook", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
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
      object obj2 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__1.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__1, typeof (ExcelApiHelper), application, filePath);
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
      Dictionary<int, List<int>> dictionary1 = new Dictionary<int, List<int>>();
      int num1 = 1;
      while (true)
      {
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, bool> target5 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, bool>> p14 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__14;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13 = CallSite<Func<CallSite, int, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.LessThan, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, int, object, object> target6 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, int, object, object>> p13 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__13;
        int num2 = num1;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target7 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p12 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__12;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object> target8 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object>> p11 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__11;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Windows", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__10, obj2);
        object obj10 = target8((CallSite) p11, obj9);
        object obj11 = target7((CallSite) p12, obj10, 1);
        object obj12 = target6((CallSite) p13, num2, obj11);
        if (target5((CallSite) p14, obj12))
        {
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, int, object> target9 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, int, object>> p17 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__17;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Item", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target10 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p16 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__16;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Windows", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj13 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__15, obj2);
          object obj14 = target10((CallSite) p16, obj13);
          int num3 = num1;
          object obj15 = target9((CallSite) p17, obj14, num3);
          Dictionary<int, List<int>> dictionary2 = dictionary1;
          int key = num1;
          List<int> intList1 = new List<int>();
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20 = CallSite<Action<CallSite, List<int>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, List<int>, object> target11 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, List<int>, object>> p20 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__20;
          List<int> intList2 = intList1;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Column", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target12 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p19 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__19;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Selection", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj16 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__18, obj15);
          object obj17 = target12((CallSite) p19, obj16);
          target11((CallSite) p20, intList2, obj17);
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__23 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__23 = CallSite<Action<CallSite, List<int>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, List<int>, object> target13 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__23.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, List<int>, object>> p23 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__23;
          List<int> intList3 = intList1;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Row", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target14 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p22 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__22;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Selection", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj18 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__21, obj15);
          object obj19 = target14((CallSite) p22, obj18);
          target13((CallSite) p23, intList3, obj19);
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__27 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__27 = CallSite<Action<CallSite, List<int>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, List<int>, object> target15 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__27.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, List<int>, object>> p27 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__27;
          List<int> intList4 = intList1;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__26 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__26 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target16 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__26.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p26 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__26;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__25 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Columns", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target17 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__25.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p25 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__25;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__24 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__24 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Selection", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj20 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__24.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__24, obj15);
          object obj21 = target17((CallSite) p25, obj20);
          object obj22 = target16((CallSite) p26, obj21);
          target15((CallSite) p27, intList4, obj22);
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__31 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__31 = CallSite<Action<CallSite, List<int>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, List<int>, object> target18 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__31.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, List<int>, object>> p31 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__31;
          List<int> intList5 = intList1;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__30 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__30 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target19 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__30.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p30 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__30;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__29 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__29 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Rows", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target20 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__29.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p29 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__29;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__28 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__28 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Selection", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj23 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__28.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__28, obj15);
          object obj24 = target20((CallSite) p29, obj23);
          object obj25 = target19((CallSite) p30, obj24);
          target18((CallSite) p31, intList5, obj25);
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__34 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__34 = CallSite<Action<CallSite, List<int>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Action<CallSite, List<int>, object> target21 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__34.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Action<CallSite, List<int>, object>> p34 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__34;
          List<int> intList6 = intList1;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__33 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__33 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Index", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, object> target22 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__33.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, object>> p33 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__33;
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__32 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__32 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveSheet", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj26 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__32.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__32, obj15);
          object obj27 = target22((CallSite) p33, obj26);
          target21((CallSite) p34, intList6, obj27);
          List<int> intList7 = intList1;
          dictionary2.Add(key, intList7);
          ++num1;
        }
        else
          break;
      }
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__35 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__35 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__35.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__35, obj2, 0);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__36 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__36 = CallSite<Action<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "OpenFile", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__36.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__36, typeof (ExcelApiHelper), application, filePath, (object) null);
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__37 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__37 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveWorkbook", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj28 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__37.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__37, application);
      object obj29 = (object) true;
      foreach (KeyValuePair<int, List<int>> keyValuePair in dictionary1)
      {
        int num4 = keyValuePair.Value[4];
        int num5 = keyValuePair.Value[0];
        int num6 = keyValuePair.Value[1];
        int num7 = keyValuePair.Value[2];
        int num8 = keyValuePair.Value[3];
        if (keyValuePair.Key != 1)
        {
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__38 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__38 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "NewWindow", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__38.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__38, obj28);
          // ISSUE: reference to a compiler-generated field
          if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__39 == null)
          {
            // ISSUE: reference to a compiler-generated field
            OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__39 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveWorkbook", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          obj28 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__39.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__39, application);
        }
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__41 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__41 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, object> target23 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__41.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, object>> p41 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__41;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__40 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__40 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Sheets", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj30 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__40.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__40, obj28);
        int num9 = num4;
        object obj31 = target23((CallSite) p41, obj30, num9);
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__42 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__42 = CallSite<Action<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Select", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__42.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__42, obj31, obj29);
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__49 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__49 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Select", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Action<CallSite, object> target24 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__49.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Action<CallSite, object>> p49 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__49;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__48 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__48 = CallSite<Func<CallSite, object, object, object, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object, object> target25 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__48.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object, object>> p48 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__48;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__43 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__43 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Range", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj32 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__43.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__43, obj31);
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__45 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__45 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, int, object> target26 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__45.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, int, object>> p45 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__45;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__44 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__44 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Cells", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj33 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__44.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__44, obj31);
        int num10 = num6;
        int num11 = num5;
        object obj34 = target26((CallSite) p45, obj33, num10, num11);
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__47 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__47 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, int, int, object> target27 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__47.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, int, int, object>> p47 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__47;
        // ISSUE: reference to a compiler-generated field
        if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__46 == null)
        {
          // ISSUE: reference to a compiler-generated field
          OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__46 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.ResultIndexed, "Cells", typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj35 = OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__46.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__46, obj31);
        int num12 = num6 + (num8 - 1);
        int num13 = num5 + (num7 - 1);
        object obj36 = target27((CallSite) p47, obj35, num12, num13);
        object obj37 = target25((CallSite) p48, obj32, obj34, obj36);
        target24((CallSite) p49, obj37);
      }
      if (flag)
        return;
      // ISSUE: reference to a compiler-generated field
      if (OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__50 == null)
      {
        // ISSUE: reference to a compiler-generated field
        OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__50 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<Type>) null, typeof (OpenFilesHandler), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__50.Target((CallSite) OpenFilesHandler.\u003C\u003Eo__7.\u003C\u003Ep__50, obj1);
    }
  }
}
