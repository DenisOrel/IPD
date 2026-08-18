// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.AttributeLocalizerComAdapter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Interop.CADInterface;
using System;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Обертка для IAttributeLocalizer для использования через COM. Реализация использует потоковую модель MTA и является thread safe.
/// </summary>
/// <remarks>
/// Со стороны CAD-интерфейса имеется ошибка - для COM-объектов с интерфейсом IAttributeLocalizer неправильно считаются ссылки (вызовы AddRef/Release не сбалансированы).
/// Поэтому в этом классе не используется базовый класс FreeThreadedObject, это позволяет отключить отслеживание экземпляров этого типа,
/// что позволяет автоматически завершить работу клиента IPS при завершении работы CAD-системы.
/// </remarks>
public sealed class AttributeLocalizerComAdapter : AttributeLocalizer, IAttributeLocalizer
{
  private IAttributeLocalizer localizer;

  public AttributeLocalizerComAdapter(IAttributeLocalizer localizer)
  {
    this.localizer = localizer != null ? localizer : throw new ArgumentNullException(nameof (localizer));
  }

  public string GetAttributeNameByID(EAttributeID ID) => this.localizer.GetAttributeNameByID(ID);
}
