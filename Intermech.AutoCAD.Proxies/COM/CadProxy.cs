// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Intermech.IO;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Win32;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Прокси-объект для COM-объекта приложения CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
public class CadProxy : CadObjectProxy, ICadProxy
{
  private readonly string applicationName;
  private readonly object rawCADSystem;
  private CadVisualStateBuilder visualStateBuilder;

  /// <summary>Создает объект.</summary>
  /// <param name="rawCADSystem">Необернутый COM-объект приложения</param>
  /// <param name="applicationName">Имя приложения в сообщениях</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawCADSystem" /> содержит null; параметр <paramref name="applicationName" /> содержит null</exception>
  public CadProxy(object rawCADSystem, string applicationName)
  {
    if (rawCADSystem == null)
      throw new ArgumentNullException(nameof (rawCADSystem));
    if (applicationName == null)
      throw new ArgumentNullException(nameof (applicationName));
    this.rawCADSystem = rawCADSystem;
    this.applicationName = applicationName;
  }

  /// <summary>
  /// Создает построитель для сохраненного состояния UI CAD-системы.
  /// </summary>
  /// <returns>Построитель для сохраненного состояния UI CAD-системы</returns>
  protected virtual CadVisualStateBuilder DoCreateVisualStateBuilder()
  {
    return new CadVisualStateBuilder();
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта документа CAD-системы.
  /// </summary>
  /// <param name="rawDocument">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentName">Имя документа для сообщений об ошибках</param>
  /// <returns>Прокси-объект для необернутого COM-объекта документа CAD-системы</returns>
  protected virtual CadDocumentProxy DoCreateDocumentProxy(object rawDocument, string documentName)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, CadProxy, CadDocumentProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (CadDocumentProxy), rawDocument, documentName, this);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта блока документа CAD-системы.
  /// </summary>
  /// <param name="rawBlock">Необернутый COM-объект блока документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта блока документа CAD-системы</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawBlock" /> содержит null; параметр <paramref name="documentProxy" /> содержит null</exception>
  internal CadBlockProxy CreateBlockProxy(object rawBlock, CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0, rawBlock, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (rawBlock));
    if (documentProxy == null)
      throw new ArgumentNullException(nameof (documentProxy));
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, CadBlockProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (CadBlockProxy), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, CadBlockProxy> target2 = CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, CadBlockProxy>> p3 = CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateBlockProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2, this, rawBlock, documentProxy);
    return target2((CallSite) p3, obj2);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта блока документа CAD-системы.
  /// </summary>
  /// <param name="rawBlock">Необернутый COM-объект блока документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта блока документа CAD-системы</returns>
  protected virtual CadBlockProxy DoCreateBlockProxy(
    object rawBlock,
    CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, CadDocumentProxy, CadBlockProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0, typeof (CadBlockProxy), rawBlock, documentProxy);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта состояния CAD-системы.
  /// </summary>
  /// <param name="rawState">Необернутый COM-объект состояния CAD-системы</param>
  /// <returns>Прокси-объект для необернутого COM-объекта состояния CAD-системы</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawBlock" /> содержит null; параметр <paramref name="documentProxy" /> содержит null</exception>
  internal CadStateProxy CreateStateProxy(object rawState)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0, rawState, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (rawState));
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, CadStateProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (CadStateProxy), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, CadStateProxy> target2 = CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, CadStateProxy>> p3 = CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, CadProxy, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateStateProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2, this, rawState);
    return target2((CallSite) p3, obj2);
  }

  protected virtual CadStateProxy DoCreateStateProxy(object rawState)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__6.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__6.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, CadProxy, CadStateProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CadProxy.\u003C\u003Eo__6.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__6.\u003C\u003Ep__0, typeof (CadStateProxy), rawState, this);
  }

  /// <summary>
  /// Создает построитель запросов по содержимому документа CAD-системы.
  /// </summary>
  /// <returns>Построитель запросов по содержимому документа CAD-системы</returns>
  internal CadSelectionSetFilterBuilder CreateSelectionSetFilterBuilder()
  {
    return this.DoCreateSelectionSetFilterBuilder();
  }

  /// <summary>
  /// Создает построитель запросов по содержимому документа CAD-системы.
  /// </summary>
  /// <returns>Построитель запросов по содержимому документа CAD-системы</returns>
  protected virtual CadSelectionSetFilterBuilder DoCreateSelectionSetFilterBuilder()
  {
    return new CadSelectionSetFilterBuilder();
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа CAD-системы.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawEntity" /> содержит null; параметр <paramref name="documentProxy" /> содержит null</exception>
  internal CadEntityProxy CreateEntityProxy(object rawEntity, CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__0, rawEntity, (object) null);
    if (target1((CallSite) p1, obj1))
      throw new ArgumentNullException(nameof (rawEntity));
    if (documentProxy == null)
      throw new ArgumentNullException(nameof (documentProxy));
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__2 = CallSite<Func<CallSite, CadProxy, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "RawGetObjectName", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__2, this, rawEntity);
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, CadEntityProxy> target2 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__4.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, CadEntityProxy>> p4 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__4;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__3 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateEntityProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__3.Target((CallSite) CadProxy.\u003C\u003Eo__9.\u003C\u003Ep__3, this, rawEntity, documentProxy, obj2);
    return target2((CallSite) p4, obj3);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа CAD-системы.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <param name="objectName">Значение свойства COM-объекта IAcadObject.ObjectName</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  protected virtual CadEntityProxy DoCreateEntityProxy(
    object rawEntity,
    CadDocumentProxy documentProxy,
    string objectName)
  {
    switch (objectName)
    {
      case "AcDbRasterImage":
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (CadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target1 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p1 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateRasterImageEntityProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__0, this, rawEntity, documentProxy);
        return target1((CallSite) p1, obj1);
      case "AcDbBlockReference":
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (CadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target2 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p3 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__2 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateExternalReferenceEntityProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__2, this, rawEntity, documentProxy);
        return target2((CallSite) p3, obj2);
      case "AcDbPdfReference":
      case "AcDbDwfReference":
      case "AcDbDgnReference":
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (CadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target3 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p5 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__5;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__4 = CallSite<Func<CallSite, CadProxy, object, CadDocumentProxy, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateUnderlayEntityProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__4.Target((CallSite) CadProxy.\u003C\u003Eo__10.\u003C\u003Ep__4, this, rawEntity, documentProxy);
        return target3((CallSite) p5, obj3);
      default:
        throw new NotSupportedException($"Не удалось создать прокси-объект для COM-объекта CAD-системы с IAcadObject.ObjectName={objectName}.");
    }
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа типа raster image.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  protected virtual CadRasterImageEntityProxy DoCreateRasterImageEntityProxy(
    object rawEntity,
    CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__11.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__11.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, CadDocumentProxy, CadRasterImageEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CadProxy.\u003C\u003Eo__11.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__11.\u003C\u003Ep__0, typeof (CadRasterImageEntityProxy), rawEntity, documentProxy);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа типа external reference.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  protected virtual CadExternalReferenceEntityProxy DoCreateExternalReferenceEntityProxy(
    object rawEntity,
    CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__12.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__12.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, CadDocumentProxy, CadExternalReferenceEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CadProxy.\u003C\u003Eo__12.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__12.\u003C\u003Ep__0, typeof (CadExternalReferenceEntityProxy), rawEntity, documentProxy);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа типа underlay.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  protected virtual CadUnderlayEntityProxy DoCreateUnderlayEntityProxy(
    object rawEntity,
    CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__13.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__13.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, CadDocumentProxy, CadUnderlayEntityProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return CadProxy.\u003C\u003Eo__13.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__13.\u003C\u003Ep__0, typeof (CadUnderlayEntityProxy), rawEntity, documentProxy);
  }

  protected virtual string RawGetObjectName(object rawObject)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ObjectName", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__14.\u003C\u003Ep__0, rawObject);
      return target((CallSite) p1, obj);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadDocument.ObjectName");
    }
  }

  /// <summary>Проверяет валидность подключения к CAD-системе.</summary>
  /// <exception cref="T:System.Exception">Подключение к CAD-системе нарушено и должно быть переустановлено</exception>
  public void KnockKnock()
  {
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__2.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p2 = CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__2;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, object, object> target2 = CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, object, object>> p1 = CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Version", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__15.\u003C\u003Ep__0, this.rawCADSystem);
    object obj2 = target2((CallSite) p1, obj1, (object) null);
    if (target1((CallSite) p2, obj2))
      throw new COMException($"{this.ApplicationName} com object is dead.");
  }

  /// <summary>
  /// Проверяет готовность CAD-системы к взаимодействию через COM API.
  /// </summary>
  /// <returns>true, если CAD-система готова к взаимодействию через COM API</returns>
  public bool IsReady() => this.DoIsReady();

  protected virtual bool DoIsReady() => !this.IsBusy();

  public ICadDocumentProxy CreateDocument()
  {
    Tuple<object, string> tuple = this.RawAddNewDocument();
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, ICadDocumentProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (ICadDocumentProxy), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, ICadDocumentProxy> target = CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, ICadDocumentProxy>> p1 = CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadProxy, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateDocumentProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__18.\u003C\u003Ep__0, this, tuple.Item1, tuple.Item2);
    return target((CallSite) p1, obj);
  }

  private Tuple<object, string> RawAddNewDocument()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__0, this.rawCADSystem);
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Add", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__1.Target((CallSite) CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__1, obj1);
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (string), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__2, obj2);
      string str = target((CallSite) p3, obj3);
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__4 = CallSite<Func<CallSite, Type, object, string, Tuple<object, string>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__4.Target((CallSite) CadProxy.\u003C\u003Eo__19.\u003C\u003Ep__4, typeof (Tuple<object, string>), obj2, str);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.ApplicationName, "IAcadDocuments.Add");
    }
  }

  public ICadDocumentProxy OpenDocument(string fullName)
  {
    this.CheckDocumentFullNameArgument(fullName);
    fullName = this.DoNormalizeDocumentFullName(fullName);
    object obj1 = File.Exists(fullName) ? this.TryGetOpenRawDocument(fullName) : throw new ApplicationProxyException($"При открытии документа {fullName} произошла внутренняя ошибка приложения: файл не найден.");
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__0, obj1, (object) null);
      if (target1((CallSite) p1, obj2))
      {
        bool flag = (File.GetAttributes(fullName) & FileAttributes.ReadOnly) != 0;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, bool, Missing, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "Open", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, string, bool, Missing, object> target2 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, string, bool, Missing, object>> p3 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__2, this.rawCADSystem);
        string str = fullName;
        int num = flag ? 1 : 0;
        Missing missing = Missing.Value;
        obj1 = target2((CallSite) p3, obj3, str, num != 0, missing);
      }
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__5 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, ICadDocumentProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (ICadDocumentProxy), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, ICadDocumentProxy> target3 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__5.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, ICadDocumentProxy>> p5 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__5;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__4 = CallSite<Func<CallSite, CadProxy, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateDocumentProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__4.Target((CallSite) CadProxy.\u003C\u003Eo__20.\u003C\u003Ep__4, this, obj1, Path.GetFileName(fullName));
      return target3((CallSite) p5, obj4);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.ApplicationName, "IAcadDocuments.Open()");
    }
  }

  public ICadDocumentProxy FindOpenDocument(string fullName)
  {
    this.CheckDocumentFullNameArgument(fullName);
    object openRawDocument = this.TryGetOpenRawDocument(fullName);
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, ICadDocumentProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (ICadDocumentProxy), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, ICadDocumentProxy> target1 = CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, ICadDocumentProxy>> p3 = CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target2 = CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__0, openRawDocument, (object) null);
    object obj2;
    if (!target2((CallSite) p1, obj1))
    {
      obj2 = (object) null;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__2 = CallSite<Func<CallSite, CadProxy, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateDocumentProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      obj2 = CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__21.\u003C\u003Ep__2, this, openRawDocument, Path.GetFileName(fullName));
    }
    return target1((CallSite) p3, obj2);
  }

  /// <summary>
  /// Проверяет корректность аргумента fullName.
  /// Значение аргумента должно быть не пустым и содержать абсолютный путь.
  /// </summary>
  /// <param name="fullName">Значение аргумента</param>
  /// <exception cref="T:System.ArgumentException">Значение аргумента некорректно</exception>
  private void CheckDocumentFullNameArgument(string fullName)
  {
    if (string.IsNullOrEmpty(fullName))
      throw new ArgumentException(AcadProxyResources.SR_DocumentFullNameNotSpecified, nameof (fullName));
    if (!Path.IsPathRooted(fullName))
      throw new ArgumentException(AcadProxyResources.SR_FullPathRequired, nameof (fullName));
  }

  /// <summary>
  /// Выполняет нормализацию абсолютного пути к файлу документа.
  /// Базовая реализация метода просто возвращает значение аргумента.
  /// </summary>
  /// <param name="fullName">Абсолютный путь к файлу документа</param>
  /// <returns>Нормализованный абсолютный путь к файлу документа</returns>
  protected virtual string DoNormalizeDocumentFullName(string fullName) => fullName;

  /// <summary>Возвращает список документов, открытых в CAD-системе.</summary>
  /// <param name="includeNew">Флаг, позволяющий включить в список еще не сохраненные на диск документы</param>
  /// <returns>Список документов, открытых в CAD-системе</returns>
  public List<ICadDocumentProxy> GetOpenDocuments(bool includeNew = true)
  {
    List<ICadDocumentProxy> openDocuments;
    try
    {
      List<Tuple<object, string>> documents = this.RawGetDocuments(CadProxy.GetDocumentsOptions.IncludeNames);
      openDocuments = new List<ICadDocumentProxy>(documents.Count);
      foreach (Tuple<object, string> tuple in documents)
      {
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__1 = CallSite<Action<CallSite, List<ICadDocumentProxy>, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        Action<CallSite, List<ICadDocumentProxy>, object> target = CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Action<CallSite, List<ICadDocumentProxy>, object>> p1 = CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__1;
        List<ICadDocumentProxy> cadDocumentProxyList = openDocuments;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__0 = CallSite<Func<CallSite, CadProxy, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateDocumentProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__24.\u003C\u003Ep__0, this, tuple.Item1, tuple.Item2);
        target((CallSite) p1, cadDocumentProxyList, obj);
      }
      if (!includeNew)
        openDocuments.RemoveAll((Predicate<ICadDocumentProxy>) (x => x.IsNew));
    }
    catch (COMException ex)
    {
      throw new ApplicationProxyException("При попытке получения открытых документов произошла внутренняя ошибка приложения: " + ex.Message);
    }
    return openDocuments;
  }

  /// <summary>
  /// Возвращает активный документ CAD-системы, если таковой имеется.
  /// Активного документа может не быть, если в CAD-системе нет открытых окон документов.
  /// </summary>
  /// <returns>Объект документа или null</returns>
  public ICadDocumentProxy TryGetActiveDocument()
  {
    object activeDocument = this.RawGetActiveDocument();
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__1;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__0, activeDocument, (object) null);
    if (target1((CallSite) p1, obj1))
      return (ICadDocumentProxy) null;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, string> target2 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, string>> p3 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__3;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__2 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj2 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__2.Target((CallSite) CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__2, activeDocument);
    string str = target2((CallSite) p3, obj2);
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__5 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, ICadDocumentProxy>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (ICadDocumentProxy), typeof (CadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, ICadDocumentProxy> target3 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__5.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, ICadDocumentProxy>> p5 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__5;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__4 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__4 = CallSite<Func<CallSite, CadProxy, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateDocumentProxy", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj3 = CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__4.Target((CallSite) CadProxy.\u003C\u003Eo__25.\u003C\u003Ep__4, this, activeDocument, str);
    return target3((CallSite) p5, obj3);
  }

  private object RawGetActiveDocument()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, bool> target1 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, bool>> p3 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, int, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, int, object> target2 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, int, object>> p2 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Count", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__0, this.rawCADSystem);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, obj2, 0);
      if (!target1((CallSite) p3, obj3))
        return (object) null;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__4 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ActiveDocument", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__4.Target((CallSite) CadProxy.\u003C\u003Eo__26.\u003C\u003Ep__4, this.rawCADSystem);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadApplication.ActiveDocument");
    }
  }

  /// <summary>
  /// Сохраняет сведения о визуальном состоянии CAD-системы: открытых файлах и активном файле.
  /// </summary>
  /// <param name="flags">Флаги, описывающие что необходимо сохранить</param>
  /// <returns>Объект-состояние</returns>
  public object SaveVisualState(CadVisualStateFlags flags)
  {
    return (object) this.VisualStateBuilder.SaveState((ICadProxy) this, flags);
  }

  /// <summary>
  /// Восстанавливает сохраненное ранее визуальное состояние CAD-системы.
  /// </summary>
  /// <param name="state">Объект-состояние</param>
  public void RestoreVisualState(object state)
  {
    if (state == null)
      throw new ArgumentNullException(nameof (state));
    ((ApplicationVisualState<ICadProxy>) state).RestoreState((ICadProxy) this);
  }

  private CadVisualStateBuilder VisualStateBuilder
  {
    [DebuggerStepThrough] get
    {
      if (this.visualStateBuilder == null)
        this.visualStateBuilder = this.DoCreateVisualStateBuilder();
      return this.visualStateBuilder;
    }
  }

  public void ShowWindow()
  {
    if (this.WindowState == AcWindowState.acMin)
      this.WindowState = AcWindowState.acMax;
    if (this.Visible)
      return;
    this.Visible = true;
  }

  public IntPtr SwitchToApp()
  {
    this.ShowWindow();
    IntPtr windowHandle1 = ForegroundWindowHelper.Default.TryGetWindowHandle();
    ForegroundWindowHelper foregroundWindowHelper = ForegroundWindowHelper.Default;
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__1 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__1 = CallSite<Func<CallSite, Type, object, IntPtr>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, Type, object, IntPtr> target = CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__1.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, Type, object, IntPtr>> p1 = CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__1;
    Type type = typeof (IntPtr);
    // ISSUE: reference to a compiler-generated field
    if (CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "HWND", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj = CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__32.\u003C\u003Ep__0, this.rawCADSystem);
    IntPtr windowHandle2 = target((CallSite) p1, type, obj);
    return foregroundWindowHelper.TrySetWindow(windowHandle2) ? windowHandle1 : IntPtr.Zero;
  }

  /// <summary>
  /// Возвращает исходный необернутый COM-объект CAD-системы.
  /// Это свойство должно использоваться только в тех случаях, когда
  /// COM-объект требуется передать в другое приложение.
  /// Внутри IPS должен использоваться только прокси-объект.
  /// </summary>
  public object RawObject
  {
    [DebuggerStepThrough] get => this.rawCADSystem;
  }

  /// <summary>
  /// Возвращает имя приложения, которое можно использовать в сообщениях и диалоговых окнах.
  /// </summary>
  public string ApplicationName
  {
    [DebuggerStepThrough] get => this.applicationName;
  }

  private bool IsBusy()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target1 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "ActiveProfile", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p2 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Profiles", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__37.\u003C\u003Ep__0, this.rawCADSystem);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, obj2);
      return target1((CallSite) p3, obj3) == null;
    }
    catch (COMException ex)
    {
      return true;
    }
  }

  public string ActiveProfile
  {
    get
    {
      try
      {
        if (CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__3 == null)
          CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
        Func<CallSite, object, string> target1 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__3.Target;
        CallSite<Func<CallSite, object, string>> p3 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__3;
        if (CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__2 == null)
          CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, nameof (ActiveProfile), typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__2.Target;
        CallSite<Func<CallSite, object, object>> p2 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__2;
        if (CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__1 == null)
          CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Profiles", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        Func<CallSite, object, object> target3 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__1;
        if (CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__0 == null)
          CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj1 = CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__39.\u003C\u003Ep__0, this.rawCADSystem);
        object obj2 = target3((CallSite) p1, obj1);
        object obj3 = target2((CallSite) p2, obj2);
        return target1((CallSite) p3, obj3);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesProfiles.ActiveProfile");
      }
    }
    set
    {
      try
      {
        if (CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__2 == null)
          CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, nameof (ActiveProfile), typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        Func<CallSite, object, string, object> target1 = CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__2.Target;
        CallSite<Func<CallSite, object, string, object>> p2 = CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__2;
        if (CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__1 == null)
          CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Profiles", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__1;
        if (CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__0 == null)
          CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj1 = CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__40.\u003C\u003Ep__0, this.rawCADSystem);
        object obj2 = target2((CallSite) p1, obj1);
        string str = value;
        object obj3 = target1((CallSite) p2, obj2, str);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesProfiles.ActiveProfile");
      }
    }
  }

  public string SupportPath
  {
    get => this.RawGetSupportPath();
    set => this.RawSetSupportPath(value);
  }

  protected virtual string RawGetSupportPath()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target1 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "SupportPath", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p2 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Files", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__44.\u003C\u003Ep__0, this.rawCADSystem);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, obj2);
      return target1((CallSite) p3, obj3);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesFiles.SupportPath");
    }
  }

  protected virtual void RawSetSupportPath(string value)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "SupportPath", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target1 = CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p2 = CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Files", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__45.\u003C\u003Ep__0, this.rawCADSystem);
      object obj2 = target2((CallSite) p1, obj1);
      string str = value;
      object obj3 = target1((CallSite) p2, obj2, str);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesFiles.SupportPath");
    }
  }

  public string WorkspacePath
  {
    get => this.RawGetWorkspacePath();
    set => this.RawSetWorkspacePath(value);
  }

  protected virtual string RawGetWorkspacePath()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target1 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "WorkspacePath", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p2 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Files", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__49.\u003C\u003Ep__0, this.rawCADSystem);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, obj2);
      return target1((CallSite) p3, obj3);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesFiles.WorkspacePath");
    }
  }

  protected virtual void RawSetWorkspacePath(string value)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "WorkspacePath", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target1 = CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p2 = CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Files", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__50.\u003C\u003Ep__0, this.rawCADSystem);
      object obj2 = target2((CallSite) p1, obj1);
      string str = value;
      object obj3 = target1((CallSite) p2, obj2, str);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesFiles.WorkspacePath");
    }
  }

  /// <summary>
  /// Возвращает признак, что загрузка внешних ссылок на другие DWG-файлы является "блокирующией".
  /// Если значение свойства равно true, то CAD-система не дает редактировать DWG-файлы после
  /// их косвенного открытия в качестве external reference (xref) из другого документа.
  /// </summary>
  public bool XRefLoadingIsBlocking
  {
    [DebuggerStepThrough] get => this.DoTestIfXRefLoadingIsBlocking();
  }

  /// <summary>
  /// Возвращает признак, что загрузка внешних ссылок на другие DWG-файлы является "блокирующией".
  /// Если значение свойства равно true, то CAD-система не дает редактировать DWG-файлы после
  /// их косвенного открытия в качестве external reference (xref) из другого документа.
  /// </summary>
  /// <returns>true - если DWG-файлы, открытые как external reference (xref), блокируются CAD-системой и доступны только для чтения</returns>
  protected virtual bool DoTestIfXRefLoadingIsBlocking() => false;

  protected virtual List<Tuple<object, string>> RawGetDocuments(
    CadProxy.GetDocumentsOptions getDocumentsOptions)
  {
    List<Tuple<object, string>> documents = new List<Tuple<object, string>>();
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Documents", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__0, this.rawCADSystem);
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__6 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__6 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (IEnumerable), typeof (CadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      foreach (object obj2 in CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__6.Target((CallSite) CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__6, obj1))
      {
        string str = (string) null;
        switch (getDocumentsOptions)
        {
          case CadProxy.GetDocumentsOptions.IncludeNames:
            // ISSUE: reference to a compiler-generated field
            if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__2 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string> target1 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__2.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string>> p2 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__2;
            // ISSUE: reference to a compiler-generated field
            if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__1 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "Name", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj3 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__1.Target((CallSite) CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__1, obj2);
            str = target1((CallSite) p2, obj3);
            break;
          case CadProxy.GetDocumentsOptions.IncludeFullNames:
            // ISSUE: reference to a compiler-generated field
            if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__4 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__4 = CallSite<Func<CallSite, object, string>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (CadProxy)));
            }
            // ISSUE: reference to a compiler-generated field
            Func<CallSite, object, string> target2 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__4.Target;
            // ISSUE: reference to a compiler-generated field
            CallSite<Func<CallSite, object, string>> p4 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__4;
            // ISSUE: reference to a compiler-generated field
            if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__3 == null)
            {
              // ISSUE: reference to a compiler-generated field
              CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, "FullName", typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
              {
                CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
              }));
            }
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            object obj4 = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__3.Target((CallSite) CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__3, obj2);
            str = target2((CallSite) p4, obj4);
            break;
        }
        List<Tuple<object, string>> tupleList = documents;
        // ISSUE: reference to a compiler-generated field
        if (CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__5 = CallSite<Func<CallSite, Type, object, string, Tuple<object, string>>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        Tuple<object, string> tuple = CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__5.Target((CallSite) CadProxy.\u003C\u003Eo__54.\u003C\u003Ep__5, typeof (Tuple<object, string>), obj2, str);
        tupleList.Add(tuple);
      }
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadApplication.Documents");
    }
    return documents;
  }

  protected virtual object RawGetAcadState()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (CadProxy.\u003C\u003Eo__55.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        CadProxy.\u003C\u003Eo__55.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.None, "GetAcadState", (IEnumerable<Type>) null, typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return CadProxy.\u003C\u003Eo__55.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__55.\u003C\u003Ep__0, this.rawCADSystem);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.ApplicationName, "IAcadApplication.GetAcadState()");
    }
  }

  protected bool Visible
  {
    get
    {
      try
      {
        if (CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__1 == null)
          CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (bool), typeof (CadProxy)));
        Func<CallSite, object, bool> target = CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, bool>> p1 = CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__1;
        if (CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__0 == null)
          CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, nameof (Visible), typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj = CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__57.\u003C\u003Ep__0, this.rawCADSystem);
        return target((CallSite) p1, obj);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadApplication.Visible");
      }
    }
    set
    {
      try
      {
        if (CadProxy.\u003C\u003Eo__58.\u003C\u003Ep__0 == null)
          CadProxy.\u003C\u003Eo__58.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, nameof (Visible), typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        object obj = CadProxy.\u003C\u003Eo__58.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__58.\u003C\u003Ep__0, this.rawCADSystem, value);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadApplication.Visible");
      }
    }
  }

  protected AcWindowState WindowState
  {
    get
    {
      try
      {
        if (CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__1 == null)
          CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, AcWindowState>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (AcWindowState), typeof (CadProxy)));
        Func<CallSite, object, AcWindowState> target = CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__1.Target;
        CallSite<Func<CallSite, object, AcWindowState>> p1 = CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__1;
        if (CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__0 == null)
          CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, nameof (WindowState), typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        object obj = CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__60.\u003C\u003Ep__0, this.rawCADSystem);
        return target((CallSite) p1, obj);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadApplication.WindowState");
      }
    }
    set
    {
      try
      {
        if (CadProxy.\u003C\u003Eo__61.\u003C\u003Ep__0 == null)
          CadProxy.\u003C\u003Eo__61.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, AcWindowState, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, nameof (WindowState), typeof (CadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        object obj = CadProxy.\u003C\u003Eo__61.\u003C\u003Ep__0.Target((CallSite) CadProxy.\u003C\u003Eo__61.\u003C\u003Ep__0, this.rawCADSystem, value);
      }
      catch (COMException ex)
      {
        throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadApplication.WindowState");
      }
    }
  }

  protected object TryGetOpenRawDocument(string fullName, bool throwIfNotFound = false)
  {
    List<Tuple<object, string>> documents = this.RawGetDocuments(CadProxy.GetDocumentsOptions.IncludeFullNames);
    try
    {
      foreach (Tuple<object, string> tuple in documents)
      {
        if (PathUtils.IsSamePath(tuple.Item2, fullName))
          return tuple.Item1;
      }
    }
    catch (COMException ex)
    {
      throw new ApplicationProxyException($"При поиске документа {fullName} произошла внутренняя ошибка: {ex.Message}.", (Exception) ex);
    }
    if (throwIfNotFound)
      throw new ApplicationProxyException($"Не удалось найти {fullName}. Возможно, он был закрыт.");
    return (object) null;
  }

  protected enum GetDocumentsOptions
  {
    IncludeNames,
    IncludeFullNames,
  }
}
