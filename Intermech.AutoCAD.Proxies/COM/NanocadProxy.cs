// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.NanocadProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>Прокси-объект для COM-объекта приложения nanoCAD.</summary>
/// <summary>Создает объект.</summary>
/// <param name="rawCADSystem">Необернутый COM-объект приложения</param>
/// <param name="applicationName">Имя приложения в сообщениях</param>
/// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawCADSystem" /> содержит null; параметр <paramref name="applicationName" /> содержит null</exception>
public sealed class NanocadProxy(object rawCADSystem, string applicationName) : CadProxy(rawCADSystem, applicationName)
{
  private const string AllOpenFileFormatsSection = "IO\\AllOpenFileFormats";
  private const string OpenInitDirParameter = "OpenInitDir";
  private const string SaveInitDirParameter = "SaveInitDir";

  protected override bool DoIsReady()
  {
    // ISSUE: reference to a compiler-generated field
    if (NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, NanocadProxy, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "CreateStateProxy", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    object obj1 = NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0, this, this.RawGetAcadState());
    // ISSUE: reference to a compiler-generated field
    if (NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__3 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (bool), typeof (NanocadProxy)));
    }
    // ISSUE: reference to a compiler-generated field
    Func<CallSite, object, bool> target1 = NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__3.Target;
    // ISSUE: reference to a compiler-generated field
    CallSite<Func<CallSite, object, bool>> p3 = NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__3;
    bool flag = base.DoIsReady();
    object obj2;
    if (flag)
    {
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__2 = CallSite<Func<CallSite, bool, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, bool, object, object> target2 = NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, bool, object, object>> p2 = NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__2;
      int num = flag ? 1 : 0;
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "IsReady", typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__1.Target((CallSite) NanocadProxy.\u003C\u003Eo__1.\u003C\u003Ep__1, obj1);
      obj2 = target2((CallSite) p2, num != 0, obj3);
    }
    else
      obj2 = (object) flag;
    return target1((CallSite) p3, obj2);
  }

  protected override object RawGetAcadState()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GetState", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return NanocadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) NanocadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, this.RawObject);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.ApplicationName, "IAcadApplication.GetState()");
    }
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта документа CAD-системы.
  /// </summary>
  /// <param name="rawDocument">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentName">Имя документа для сообщений об ошибках</param>
  /// <returns>Прокси-объект для необернутого COM-объекта документа CAD-системы</returns>
  protected override CadDocumentProxy DoCreateDocumentProxy(object rawDocument, string documentName)
  {
    // ISSUE: reference to a compiler-generated field
    if (NanocadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      NanocadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, NanocadProxy, NanocadDocumentProxy>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (CadDocumentProxy) NanocadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) NanocadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0, typeof (NanocadDocumentProxy), rawDocument, documentName, this);
  }

  /// <summary>
  /// Создает построитель запросов по содержимому документа CAD-системы.
  /// </summary>
  /// <returns>Построитель запросов по содержимому документа CAD-системы</returns>
  protected override CadSelectionSetFilterBuilder DoCreateSelectionSetFilterBuilder()
  {
    return (CadSelectionSetFilterBuilder) new NanocadSelectionSetFilterBuilder();
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа CAD-системы.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <param name="objectName">Значение свойства COM-объекта IAcadObject.ObjectName</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  protected override CadEntityProxy DoCreateEntityProxy(
    object rawEntity,
    CadDocumentProxy documentProxy,
    string objectName)
  {
    switch (objectName)
    {
      case "AcDbRasterImage":
        // ISSUE: reference to a compiler-generated field
        if (NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (NanocadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target1 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p1 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0 = CallSite<Func<CallSite, NanocadProxy, object, CadDocumentProxy, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateRasterImageEntityProxy", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0.Target((CallSite) NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__0, this, rawEntity, documentProxy);
        return target1((CallSite) p1, obj1);
      case "AcDbBlockReference":
        // ISSUE: reference to a compiler-generated field
        if (NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (NanocadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target2 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p3 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2 = CallSite<Func<CallSite, NanocadProxy, object, CadDocumentProxy, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateExternalReferenceEntityProxy", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2.Target((CallSite) NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__2, this, rawEntity, documentProxy);
        return target2((CallSite) p3, obj2);
      case "AcDbPdfReference":
      case "AcDbDwfReference":
      case "AcDbDgnReference":
        // ISSUE: reference to a compiler-generated field
        if (NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (NanocadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target3 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p5 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__5;
        // ISSUE: reference to a compiler-generated field
        if (NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__4 = CallSite<Func<CallSite, NanocadProxy, object, CadDocumentProxy, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateUnderlayEntityProxy", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__4.Target((CallSite) NanocadProxy.\u003C\u003Eo__5.\u003C\u003Ep__4, this, rawEntity, documentProxy);
        return target3((CallSite) p5, obj3);
      default:
        throw new NotSupportedException($"Не удалось создать прокси-объект для COM-объекта CAD-системы с IAcadObject.ObjectName={objectName}.");
    }
  }

  /// <summary>
  /// Выполняет нормализацию абсолютного пути к файлу документа.
  /// </summary>
  /// <param name="fullName">Абсолютный путь к файлу документа</param>
  /// <returns>Нормализованный абсолютный путь к файлу документа</returns>
  protected override string DoNormalizeDocumentFullName(string fullName)
  {
    fullName = Path.GetFullPath(fullName);
    return base.DoNormalizeDocumentFullName(fullName);
  }

  protected override string RawGetWorkspacePath()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Section", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target1 = NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p1 = NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Profile", typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__0.Target((CallSite) NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__0, this.RawObject);
      object obj2 = target1((CallSite) p1, obj1, "IO\\AllOpenFileFormats");
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (NanocadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target2 = NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p3 = NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.GetIndex(CSharpBinderFlags.None, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__2.Target((CallSite) NanocadProxy.\u003C\u003Eo__7.\u003C\u003Ep__2, obj2, "OpenInitDir");
      return target2((CallSite) p3, obj3);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.ApplicationName, "IAcadApplication.Profile.Section()");
    }
  }

  protected override void RawSetWorkspacePath(string value)
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Section", (IEnumerable<Type>) null, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string, object> target = NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string, object>> p1 = NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Profile", typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__0.Target((CallSite) NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__0, this.RawObject);
      object obj2 = target((CallSite) p1, obj1, "IO\\AllOpenFileFormats");
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, string, string, object>>.Create(Binder.SetIndex(CSharpBinderFlags.None, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj3 = NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__2.Target((CallSite) NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__2, obj2, "OpenInitDir", value);
      // ISSUE: reference to a compiler-generated field
      if (NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, string, string, object>>.Create(Binder.SetIndex(CSharpBinderFlags.None, typeof (NanocadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, (string) null),
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj4 = NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__3.Target((CallSite) NanocadProxy.\u003C\u003Eo__8.\u003C\u003Ep__3, obj2, "SaveInitDir", value);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalMethodCOMException(ex, this.ApplicationName, "IAcadApplication.Profile.Section()");
    }
  }

  /// <summary>
  /// Возвращает признак, что загрузка внешних ссылок на другие DWG-файлы является "блокирующией".
  /// Если значение свойства равно true, то CAD-система не дает редактировать DWG-файлы после
  /// их косвенного открытия в качестве external reference (xref) из другого документа.
  /// </summary>
  /// <returns>true - если DWG-файлы, открытые как external reference (xref), блокируются CAD-системой и доступны только для чтения</returns>
  protected override bool DoTestIfXRefLoadingIsBlocking() => false;
}
