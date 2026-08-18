// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordApiHelper
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

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
namespace Intermech.Tools.MSOffice.Word;

internal static class MSWordApiHelper
{
  public static OpenComDocument OpenDocument(
    object app,
    string fullPath,
    IMsoApiResourceTracker apiResourceTracker)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, IMsoApiResourceTracker, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "OpenFile", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__0, typeof (MSWordApiHelper), app, fullPath, apiResourceTracker);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, string, object, OpenComDocument>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__0.\u003C\u003Ep__1, typeof (OpenComDocument), fullPath, obj);
  }

  public static OpenComDocument FindOpenDocument(object app, string fullPath)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (MSWordApiHelper), app, fullPath);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target = MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p2 = MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__1, obj1, (object) null);
    if (!target((CallSite) p2, obj2))
      return (OpenComDocument) null;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, string, object, OpenComDocument>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__1.\u003C\u003Ep__3, typeof (OpenComDocument), fullPath, obj1);
  }

  public static object OpenFile(
    object app,
    string fullPath,
    IMsoApiResourceTracker apiResourceTracker)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, bool, IMsoApiResourceTracker, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, nameof (OpenFile), (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return MSWordApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (MSWordApiHelper), app, fullPath, true, apiResourceTracker);
  }

  public static object OpenFile(
    object app,
    string fullPath,
    bool openVisible,
    IMsoApiResourceTracker apiResourceTracker)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__2, typeof (MSWordApiHelper), app, fullPath);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p4 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__3, obj2, (object) null);
    if (target2((CallSite) p4, obj3))
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, string, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Open", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.NamedArgument, "Visible")
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, bool, object> target3 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, bool, object>> p6 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__6;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__3.\u003C\u003Ep__5, app);
      string str = fullPath;
      int num = openVisible ? 1 : 0;
      obj2 = target3((CallSite) p6, obj4, str, num != 0);
      apiResourceTracker?.TrackOpenFile(fullPath);
    }
    return obj2;
  }

  public static object FindOpenFile(object app, string fullPath)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    try
    {
      object obj2 = (object) fullPath;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<\u003C\u003EF\u007B00000004\u007D<CallSite, object, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Item", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsRef, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: variable of a compiler-generated type
      \u003C\u003EF\u007B00000004\u007D<CallSite, object, object, object> target2 = MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<\u003C\u003EF\u007B00000004\u007D<CallSite, object, object, object>> p3 = MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__4.\u003C\u003Ep__2, app);
      ref object local = ref obj2;
      return target2((CallSite) p3, obj3, ref local);
    }
    catch (COMException ex)
    {
      return (object) null;
    }
  }

  public static MsoSavedState SaveState(object app)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    MsoSavedState msoSavedState1 = new MsoSavedState();
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "TryGetActiveDocument", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__2, typeof (MSWordApiHelper), app);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__8.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p8 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__8;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__3, obj2, (object) null);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    object obj4;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    if (!MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__7.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__7, obj3))
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object, object> target3 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object, object>> p6 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__6;
      object obj5 = obj3;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsPathRooted", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, Type, object, object> target4 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, Type, object, object>> p5 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__5;
      Type type = typeof (Path);
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj6 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__4, obj2);
      object obj7 = target4((CallSite) p5, type, obj6);
      obj4 = target3((CallSite) p6, obj5, obj7);
    }
    else
      obj4 = obj3;
    if (target2((CallSite) p8, obj4))
    {
      MsoSavedState msoSavedState2 = msoSavedState1;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__10 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (MSWordApiHelper)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target5 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__10.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p10 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__10;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__9 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj8 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__9.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__9, obj2);
      string str = target5((CallSite) p10, obj8);
      msoSavedState2.ActiveDocument = str;
    }
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__20 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__20 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (MSWordApiHelper)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, IEnumerable> target6 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__20.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, IEnumerable>> p20 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__20;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__11 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__11.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__11, app);
    foreach (object obj10 in target6((CallSite) p20, obj9))
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__17 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__17 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target7 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__17.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p17 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__17;
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__12 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__12 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "UserControl", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj11 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__12.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__12, obj10);
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__16 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__16 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      object obj12;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__16.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__16, obj11))
      {
        // ISSUE: reference to a compiler-generated field
        if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__15 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__15 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, object, object> target8 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__15.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, object, object>> p15 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__15;
        object obj13 = obj11;
        // ISSUE: reference to a compiler-generated field
        if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__14 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__14 = CallSite<Func<CallSite, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IsPathRooted", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, Type, object, object> target9 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__14.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, Type, object, object>> p14 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__14;
        Type type = typeof (Path);
        // ISSUE: reference to a compiler-generated field
        if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__13 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__13 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj14 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__13.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__13, obj10);
        object obj15 = target9((CallSite) p14, type, obj14);
        obj12 = target8((CallSite) p15, obj13, obj15);
      }
      else
        obj12 = obj11;
      if (target7((CallSite) p17, obj12))
      {
        // ISSUE: reference to a compiler-generated field
        if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__19 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__19 = CallSite<Action<CallSite, ICollection<string>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Action<CallSite, ICollection<string>, object> target10 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__19.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Action<CallSite, ICollection<string>, object>> p19 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__19;
        ICollection<string> openDocuments = msoSavedState1.OpenDocuments;
        // ISSUE: reference to a compiler-generated field
        if (MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__18 == null)
        {
          // ISSUE: reference to a compiler-generated field
          MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__18 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj16 = MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__18.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__5.\u003C\u003Ep__18, obj10);
        target10((CallSite) p19, openDocuments, obj16);
      }
    }
    return msoSavedState1;
  }

  public static void RestoreState(object app, MsoSavedState appState)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__0, app, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (app));
    if (appState == null)
      throw new ArgumentNullException(nameof (appState));
    foreach (string openDocument in (IEnumerable<string>) appState.OpenDocuments)
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2 = CallSite<Action<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "OpenFile", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__2, typeof (MSWordApiHelper), app, openDocument, (object) null);
    }
    if (appState.ActiveDocument == null)
      return;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3 = CallSite<Func<CallSite, Type, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "FindOpenFile", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__3, typeof (MSWordApiHelper), app, appState.ActiveDocument);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p5 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__4, obj2, (object) null);
    if (!target2((CallSite) p5, obj3))
      return;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Activate", (IEnumerable<Type>) null, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__6.\u003C\u003Ep__6, obj2);
  }

  private static object TryGetActiveDocument(object app)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ActiveDocument", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return MSWordApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__7.\u003C\u003Ep__0, app);
    }
    catch (COMException ex)
    {
      return (object) null;
    }
  }

  public static void EnsureApplicationWindowIsAvailableToUser(object app)
  {
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p2 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, object> target2 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, object>> p1 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Visible", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__0, app);
    object obj2 = target2((CallSite) p1, obj1);
    if (!target1((CallSite) p2, obj2))
      return;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target3 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p5 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, int, object> target4 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, int, object>> p4 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowState", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__3.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__3, app);
    object obj4 = target4((CallSite) p4, obj3, 2);
    if (target3((CallSite) p5, obj4))
    {
      // ISSUE: reference to a compiler-generated field
      if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj5 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__6.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__6, app, 2);
    }
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__7 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__7 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Visible", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj6 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__7.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__7, app, true);
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__10 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target5 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__10.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p10 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__10;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__9 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__9 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, int, object> target6 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__9.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, int, object>> p9 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__9;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__8 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__8 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "WindowState", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj7 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__8.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__8, app);
    object obj8 = target6((CallSite) p9, obj7, 2);
    if (!target5((CallSite) p10, obj8))
      return;
    // ISSUE: reference to a compiler-generated field
    if (MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__11 == null)
    {
      // ISSUE: reference to a compiler-generated field
      MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__11 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "WindowState", typeof (MSWordApiHelper), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj9 = MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__11.Target((CallSite) MSWordApiHelper.\u003C\u003Eo__8.\u003C\u003Ep__11, app, 2);
  }
}
