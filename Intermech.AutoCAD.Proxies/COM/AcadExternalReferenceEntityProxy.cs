// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.AcadExternalReferenceEntityProxy
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

/// <summary>
/// Прокси-объект для COM-объекта элемента документа типа external reference (XRef) для AutoCAD.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="rawEntity">исходный необернутый COM-объект элемента документа CAD-системы</param>
/// <param name="cadDocument">прокси-объект родительского документа</param>
/// <exception cref="T:System.ArgumentNullException">параметр <paramref name="rawEntity" /> содержит null; параметр <paramref name="cadDocument" /> содержит null</exception>
public sealed class AcadExternalReferenceEntityProxy(object rawEntity, CadDocumentProxy cadDocument) : 
  CadExternalReferenceEntityProxy(rawEntity, cadDocument)
{
  /// <summary>
  /// Проверяет наличие файла у элемента документа CAD-системы.
  /// </summary>
  /// <returns>Признак наличия файла у документа CAD-системы</returns>
  protected override bool DoTestIfFilePresent()
  {
    return base.DoTestIfFilePresent() && this.RawGetEntityType() == AcEntityName.acExternalReference;
  }

  /// <summary>
  /// Возвращает значение свойства IAcadEntity.EntityType.
  /// Это свойство присутствует только у AutoCAD.
  /// </summary>
  /// <returns>Значение свойства</returns>
  private AcEntityName RawGetEntityType()
  {
    try
    {
      // ISSUE: reference to a compiler-generated field
      if (AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, AcEntityName>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (AcEntityName), typeof (AcadExternalReferenceEntityProxy)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, AcEntityName> target = AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, AcEntityName>> p1 = AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "EntityType", typeof (AcadExternalReferenceEntityProxy), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj = AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) AcadExternalReferenceEntityProxy.\u003C\u003Eo__2.\u003C\u003Ep__0, this.RawObject);
      return target((CallSite) p1, obj);
    }
    catch (COMException ex)
    {
      throw this.WrapExternalPropertyCOMException(ex, this.CADDocument.CADSystem.ApplicationName, "IAcadEntity.EntityType");
    }
  }
}
