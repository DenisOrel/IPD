// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.AcadProxy
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>Прокси-объект для COM-объекта приложения AutoCAD.</summary>
/// <summary>Создает объект.</summary>
/// <param name="rawCADSystem">Необернутый COM-объект приложения</param>
/// <param name="applicationName">Имя приложения в сообщениях</param>
/// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawCADSystem" /> содержит null; параметр <paramref name="applicationName" /> содержит null</exception>
public sealed class AcadProxy(object rawCADSystem, string applicationName) : CadProxy(rawCADSystem, applicationName)
{
  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта документа CAD-системы.
  /// </summary>
  /// <param name="rawDocument">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentName">Имя документа для сообщений об ошибках</param>
  /// <returns>Прокси-объект для необернутого COM-объекта документа CAD-системы</returns>
  protected override CadDocumentProxy DoCreateDocumentProxy(object rawDocument, string documentName)
  {
    // ISSUE: reference to a compiler-generated field
    if (AcadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AcadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, string, AcadProxy, AcadDocumentProxy>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (AcadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (CadDocumentProxy) AcadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0.Target((CallSite) AcadProxy.\u003C\u003Eo__1.\u003C\u003Ep__0, typeof (AcadDocumentProxy), rawDocument, documentName, this);
  }

  /// <summary>
  /// Создает прокси-объект для необернутого COM-объекта элемента документа типа external reference.
  /// </summary>
  /// <param name="rawEntity">Необернутый COM-объект документа CAD-системы</param>
  /// <param name="documentProxy">Прокси-объект родительского документа</param>
  /// <returns>Прокси-объект для необернутого COM-объекта элемента документа CAD-системы</returns>
  protected override CadExternalReferenceEntityProxy DoCreateExternalReferenceEntityProxy(
    object rawEntity,
    CadDocumentProxy documentProxy)
  {
    // ISSUE: reference to a compiler-generated field
    if (AcadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AcadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, Type, object, CadDocumentProxy, AcadExternalReferenceEntityProxy>>.Create(Binder.InvokeConstructor(CSharpBinderFlags.None, typeof (AcadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[3]
      {
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
        CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      }));
    }
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    return (CadExternalReferenceEntityProxy) AcadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) AcadProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, typeof (AcadExternalReferenceEntityProxy), rawEntity, documentProxy);
  }

  /// <summary>
  /// Возвращает признак, что загрузка внешних ссылок на другие DWG-файлы является "блокирующией".
  /// Если значение свойства равно true, то CAD-система не дает редактировать DWG-файлы после
  /// их косвенного открытия в качестве external reference (xref) из другого документа.
  /// </summary>
  /// <returns>true - если DWG-файлы, открытые как external reference (xref), блокируются CAD-системой и доступны только для чтения</returns>
  protected override bool DoTestIfXRefLoadingIsBlocking()
  {
    return this.RawGetXRefDemandLoad() == AcXRefDemandLoad.acDemandLoadEnabled;
  }

  /// <summary>
  /// Возвращает значение свойства IAcadPreferencesOpenSave.XrefDemandLoad COM-объекта настроек CAD-системы.
  /// </summary>
  /// <returns>Значение свойства</returns>
  private AcXRefDemandLoad RawGetXRefDemandLoad()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__3 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, AcXRefDemandLoad>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (AcXRefDemandLoad), typeof (AcadProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, AcXRefDemandLoad> target1 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__3.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, AcXRefDemandLoad>> p3 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__3;
      // ISSUE: reference to a compiler-generated field
      if (AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__2 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "XrefDemandLoad", typeof (AcadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target2 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__2.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p2 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__2;
      // ISSUE: reference to a compiler-generated field
      if (AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "OpenSave", typeof (AcadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, object> target3 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, object>> p1 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Preferences", typeof (AcadProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj1 = AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0.Target((CallSite) AcadProxy.\u003C\u003Eo__4.\u003C\u003Ep__0, this.RawObject);
      object obj2 = target3((CallSite) p1, obj1);
      object obj3 = target2((CallSite) p2, obj2);
      return target1((CallSite) p3, obj3);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.ApplicationName, "IAcadPreferencesOpenSave.XrefDemandLoad");
    }
  }
}
