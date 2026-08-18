// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelApiHelper
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.IO;
using Intermech.Tools.Integrators.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

internal static class ExcelApiHelper
{
  public static OpenComDocument OpenDocument(
    object app,
    string fullPath,
    IMsoApiResourceTracker apiResourceTracker)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, IMsoApiResourceTracker, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "OpenFile", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0, typeof (ExcelApiHelper), app, fullPath, apiResourceTracker);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, string, object, OpenComDocument>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1, typeof (OpenComDocument), fullPath, obj);
  }

  public static OpenComDocument FindOpenDocument(object app, string fullPath)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (ExcelApiHelper), app, fullPath);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p2 = ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1, obj1, (object) null);
    if (!target((CallSite) p2, obj2))
      return (OpenComDocument) null;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, string, object, OpenComDocument>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3, typeof (OpenComDocument), fullPath, obj1);
  }

  public static object OpenFile(
    object app,
    string fullPath,
    IMsoApiResourceTracker apiResourceTracker)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__2, typeof (ExcelApiHelper), app, fullPath);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p4 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__3.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__3, obj2, (object) null);
    if (target2((CallSite) p4, obj3))
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Open", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target3 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p6 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Workbooks", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__5.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__5, app);
      string str = fullPath;
      obj2 = target3((CallSite) p6, obj4, str);
      apiResourceTracker?.TrackOpenFile(fullPath);
    }
    return obj2;
  }

  public static object FindOpenFile(object app, string fullPath)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p3 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target2 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p2 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Workbooks", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0, app);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, obj2, 0);
      if (target1((CallSite) p3, obj3))
        return (object) null;
      object fileName = (object) Path.GetFileName(fullPath);
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Item", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> target4 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> p5 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Workbooks", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4, app);
      object obj5 = fileName;
      object obj6 = target4((CallSite) p5, obj4, obj5);
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target5 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__8.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p8 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__8;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__7 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__7 = CallSite<Func<CallSite, Type, string, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsSamePath", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, string, object, object> target6 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__7.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, string, object, object>> p7 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__7;
      Type type = typeof (PathUtils);
      string str = fullPath;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6, obj6);
      object obj8 = target6((CallSite) p7, type, str, obj7);
      return target5((CallSite) p8, obj8) ? obj6 : (object) null;
    }
    catch (COMException ex)
    {
      return (object) null;
    }
  }

  public static MsoSavedState SaveState(object app)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    MsoSavedState msoSavedState1 = new MsoSavedState();
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveWorkbook", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2, app);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__8.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p8 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__8;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3, obj2, (object) null);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    object obj4;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__7.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__7, obj3))
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> target3 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> p6 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__6;
      object obj5 = obj3;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__5 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsPathRooted", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target4 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p5 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__5;
      Type type = typeof (Path);
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__4.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__4, obj2);
      object obj7 = target4((CallSite) p5, type, obj6);
      obj4 = target3((CallSite) p6, obj5, obj7);
    }
    else
      obj4 = obj3;
    if (target2((CallSite) p8, obj4))
    {
      MsoSavedState msoSavedState2 = msoSavedState1;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (ExcelApiHelper)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target5 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p10 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__10;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__9.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__9, obj2);
      string str = target5((CallSite) p10, obj8);
      msoSavedState2.ActiveDocument = str;
    }
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__17 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (ExcelApiHelper)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable> target6 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__17.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable>> p17 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__17;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__11 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Workbooks", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__11.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__11, app);
    foreach (object obj10 in target6((CallSite) p17, obj9))
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__14 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__14 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target7 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__14.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p14 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__14;
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__13 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsPathRooted", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target8 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p13 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__13;
      Type type = typeof (Path);
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj11 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__12.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__12, obj10);
      object obj12 = target8((CallSite) p13, type, obj11);
      if (target7((CallSite) p14, obj12))
      {
        // ISSUE: reference to a compiler-generated field
        if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__16 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__16 = CallSite<Action<CallSite, ICollection<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Action<CallSite, ICollection<string>, object> target9 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__16.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Action<CallSite, ICollection<string>, object>> p16 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__16;
        ICollection<string> openDocuments = msoSavedState1.OpenDocuments;
        // ISSUE: reference to a compiler-generated field
        if (ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj13 = ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__15.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__15, obj10);
        target9((CallSite) p16, openDocuments, obj13);
      }
    }
    return msoSavedState1;
  }

  public static void RestoreState(object app, MsoSavedState appState)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    if (appState == null)
      throw new ArgumentNullException(nameof (appState));
    foreach (string openDocument in (IEnumerable<string>) appState.OpenDocuments)
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Action<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "OpenFile", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2, typeof (ExcelApiHelper), app, openDocument, (object) null);
    }
    if (appState.ActiveDocument == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3, typeof (ExcelApiHelper), app, appState.ActiveDocument);
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p5 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4, obj2, (object) null);
    if (!target2((CallSite) p5, obj3))
      return;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<Type>) null, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6, obj2);
  }

  public static void ActivateApplication(object app)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p4 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, object> target3 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, object>> p3 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2, app);
    object obj3 = target3((CallSite) p3, obj2);
    if (target2((CallSite) p4, obj3))
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Visible", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5, app, true);
    }
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (int), typeof (ExcelApiHelper)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, int> target4 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__7.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, int>> p7 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__7;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowState", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj5 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6, app);
    int num = target4((CallSite) p7, obj5);
    if (num == -4140)
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__8 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__8.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__8, app, -4137);
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__9.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__9, app, -4140);
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__10.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__10, app, num);
    }
  }

  public static void EnsureApplicationWindowIsAvailableToUser(object app)
  {
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p2 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, object> target2 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, object>> p1 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0, app);
    object obj2 = target2((CallSite) p1, obj1);
    if (!target1((CallSite) p2, obj2))
      return;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target3 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p5 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, int, object> target4 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, int, object>> p4 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowState", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__3.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__3, app);
    object obj4 = target4((CallSite) p4, obj3, -4140);
    if (target3((CallSite) p5, obj4))
    {
      // ISSUE: reference to a compiler-generated field
      if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__6.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__6, app, -4140);
    }
    // ISSUE: reference to a compiler-generated field
    if (ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Visible", typeof (ExcelApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__7.Target((CallSite) ExcelApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__7, app, true);
  }
}
