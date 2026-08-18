// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.Subsystems.Import_from_Excel.ComExcelReader
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Runtime.ComInterop;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.Tools.Client.Subsystems.Import_from_Excel;

internal class ComExcelReader
{
  public static Array GetData(string aFileName)
  {
    Array data = (Array) null;
    object obj1 = (object) null;
    object obj2 = (object) null;
    object obj3 = (object) null;
    object obj4 = (object) null;
    object obj5 = (object) null;
    try
    {
      obj1 = new ProgIdProvider("Excel.Application", false).CreateInstance();
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Workbooks", typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      obj2 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__0, obj1);
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Open", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__1.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__1, obj2, aFileName);
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveSheet", typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      obj3 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__2.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__2, obj6);
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "UsedRange", typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      obj4 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__3.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__3, obj3);
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p6 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.GreaterThan, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target2 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p5 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj7 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__4.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__4, obj4);
      object obj8 = target2((CallSite) p5, obj7, 0);
      if (target1((CallSite) p6, obj8))
      {
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Cells", typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        obj5 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__7.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__7, obj4);
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, Array>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (Array), typeof (ComExcelReader)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, Array> target3 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__9.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, Array>> p9 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__9;
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Value2", typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj9 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__8.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__8, obj5);
        data = target3((CallSite) p9, obj9);
      }
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__10 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Close", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__10.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__10, obj6, 0);
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__11 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__11 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Quit", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__11.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__11, obj1);
      return data;
    }
    finally
    {
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__13 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target4 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__13.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p13 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__13;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj10 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__12.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__12, obj5, (object) null);
      if (target4((CallSite) p13, obj10))
      {
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__14 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReleaseComObject", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__14.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__14, typeof (Marshal), obj5);
      }
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target5 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__16.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p16 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__16;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__15 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj11 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__15.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__15, obj4, (object) null);
      if (target5((CallSite) p16, obj11))
      {
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__17 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__17 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReleaseComObject", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__17.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__17, typeof (Marshal), obj4);
      }
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__19 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__19 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target6 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__19.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p19 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__19;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__18 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj12 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__18.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__18, obj3, (object) null);
      if (target6((CallSite) p19, obj12))
      {
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__20 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__20 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReleaseComObject", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__20.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__20, typeof (Marshal), obj3);
      }
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__22 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__22 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target7 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__22.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p22 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__22;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__21 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__21 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj13 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__21.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__21, obj2, (object) null);
      if (target7((CallSite) p22, obj13))
      {
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__23 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__23 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReleaseComObject", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__23.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__23, typeof (Marshal), obj2);
      }
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__25 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__25 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target8 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__25.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p25 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__25;
      // ISSUE: reference to a compiler-generated field
      if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__24 == null)
      {
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__24 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj14 = ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__24.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__24, obj1, (object) null);
      if (target8((CallSite) p25, obj14))
      {
        // ISSUE: reference to a compiler-generated field
        if (ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__26 == null)
        {
          // ISSUE: reference to a compiler-generated field
          ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__26 = CallSite<Action<CallSite, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ReleaseComObject", (IEnumerable<Type>) null, typeof (ComExcelReader), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__26.Target((CallSite) ComExcelReader.\u003C\u003Eo__0.\u003C\u003Ep__26, typeof (Marshal), obj1);
      }
      GC.Collect();
    }
  }
}
