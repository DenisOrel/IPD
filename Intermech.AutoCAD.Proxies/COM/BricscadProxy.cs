// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.BricscadProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>Прокси-объект для COM-объекта приложения BricsCAD.</summary>
/// <summary>Создает объект.</summary>
/// <param name="rawCADSystem">Необернутый COM-объект приложения</param>
/// <param name="applicationName">Имя приложения в сообщениях</param>
/// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawCADSystem" /> содержит null; параметр <paramref name="applicationName" /> содержит null</exception>
public sealed class BricscadProxy(object rawCADSystem, string applicationName) : CadProxy(rawCADSystem, applicationName)
{
  /// <summary>
  /// Создает построитель запросов по содержимому документа CAD-системы.
  /// </summary>
  /// <returns>Построитель запросов по содержимому документа CAD-системы</returns>
  protected override CadSelectionSetFilterBuilder DoCreateSelectionSetFilterBuilder()
  {
    return (CadSelectionSetFilterBuilder) new BricscadSelectionSetFilterBuilder();
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
    if (BricscadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      BricscadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, BricscadProxy, BricscadDocumentProxy>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (BricscadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (CadDocumentProxy) BricscadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) BricscadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (BricscadDocumentProxy), rawDocument, documentName, this);
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
        if (BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (BricscadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target1 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p1 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0 = CallSite<Func<CallSite, BricscadProxy, object, CadDocumentProxy, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateRasterImageEntityProxy", (IEnumerable<Type>) null, typeof (BricscadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj1 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0.Target((CallSite) BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__0, this, rawEntity, documentProxy);
        return target1((CallSite) p1, obj1);
      case "AcDbBlockReference":
        // ISSUE: reference to a compiler-generated field
        if (BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (BricscadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target2 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p3 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__3;
        // ISSUE: reference to a compiler-generated field
        if (BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2 = CallSite<Func<CallSite, BricscadProxy, object, CadDocumentProxy, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateExternalReferenceEntityProxy", (IEnumerable<Type>) null, typeof (BricscadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2.Target((CallSite) BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__2, this, rawEntity, documentProxy);
        return target2((CallSite) p3, obj2);
      case "AcDbPdfReference":
      case "AcDbDwfReference":
      case "AcDbDgnReference":
        // ISSUE: reference to a compiler-generated field
        if (BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__5 = CallSite<Func<CallSite, object, CadEntityProxy>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (CadEntityProxy), typeof (BricscadProxy)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, CadEntityProxy> target3 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__5.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, CadEntityProxy>> p5 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__5;
        // ISSUE: reference to a compiler-generated field
        if (BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__4 = CallSite<Func<CallSite, BricscadProxy, object, CadDocumentProxy, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.InvokeSimpleName, "DoCreateUnderlayEntityProxy", (IEnumerable<Type>) null, typeof (BricscadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__4.Target((CallSite) BricscadProxy.\u003C\u003Eo__3.\u003C\u003Ep__4, this, rawEntity, documentProxy);
        return target3((CallSite) p5, obj3);
      default:
        throw new NotSupportedException($"Не удалось создать прокси-объект для COM-объекта CAD-системы с IAcadObject.ObjectName={objectName}.");
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
