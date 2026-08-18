
// Type: Intermech.Navigator.Parts.CompositeStatusesInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.Parts;

internal class CompositeStatusesInfo : INodeStatusesInfo
{
  private List<StatusesInfoSlot> slots;
  private static Image[] emptyImages = new Image[0];
  /// <summary>Ссылка на актуальный метод поиска слота информации о статусе по идентификатору ноды</summary>
  private CompositeStatusesInfo.delegateGetNodeStatusesInfoSlot GetNodeStatusesInfoSlot;

  public CompositeStatusesInfo(List<StatusesInfoSlot> slots)
  {
    if (slots.Count > 4)
    {
      slots.Sort(new Comparison<StatusesInfoSlot>(StatusesInfoSlot.CompareByUniqueId));
      this.GetNodeStatusesInfoSlot = new CompositeStatusesInfo.delegateGetNodeStatusesInfoSlot(this.GetNodeStatusesInfoSlotBinary);
    }
    else
      this.GetNodeStatusesInfoSlot = new CompositeStatusesInfo.delegateGetNodeStatusesInfoSlot(this.GetNodeStatusesInfoSlotLinary);
    this.slots = slots;
  }

  /// <summary>Бинарный алгоритм поиска слота статуса ноды по идентификатору ноды</summary>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <returns>Найденный слот статуса элемента</returns>
  private StatusesInfoSlot GetNodeStatusesInfoSlotBinary(INodeID nodeId)
  {
    if (nodeId == null)
      return (StatusesInfoSlot) null;
    int partId = ((PartCookie) nodeId.Cookie).PartId;
    return this.slots.BinaryGetAnyMatch<StatusesInfoSlot>((Func<StatusesInfoSlot, int>) (slot => slot.UniqueId.CompareTo(partId)));
  }

  /// <summary>Линейный алгоритм поиска слота статуса ноды по идентификатору ноды</summary>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <returns>Найденный слот статуса элемента</returns>
  private StatusesInfoSlot GetNodeStatusesInfoSlotLinary(INodeID nodeId)
  {
    if (nodeId == null)
      return (StatusesInfoSlot) null;
    int partId = ((PartCookie) nodeId.Cookie).PartId;
    for (int index = this.slots.Count - 1; index >= 0; --index)
    {
      if (this.slots[index].UniqueId == partId)
        return this.slots[index];
    }
    return (StatusesInfoSlot) null;
  }

  public Image[] GetIcons(INodeID nodeId, object columnValue)
  {
    StatusesInfoSlot statusesInfoSlot = this.GetNodeStatusesInfoSlot(nodeId);
    return statusesInfoSlot == null ? CompositeStatusesInfo.emptyImages : statusesInfoSlot.Object.GetIcons(nodeId, columnValue);
  }

  public string GetDescription(
    IServiceProvider services,
    INodeID nodeId,
    object columnValue,
    int iconIndex)
  {
    StatusesInfoSlot statusesInfoSlot = this.GetNodeStatusesInfoSlot(nodeId);
    return statusesInfoSlot == null ? string.Empty : statusesInfoSlot.Object.GetDescription(services, nodeId, columnValue, iconIndex);
  }

  /// <summary>
  /// Возвращает шрифт для указанной ячейки, если есть какие-то проблемы с её содержимым, или null
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="nodeId">Идентификатор элемента навигации</param>
  /// <param name="columnValue">Значение колонки</param>
  /// <param name="parentFont">Текущий шрифт</param>
  /// <returns>Шрифт или null, если не требуется выделение особым шрифтом</returns>
  public Font GetFont(
    IServiceProvider services,
    INodeID nodeId,
    object columnValue,
    Font parentFont)
  {
    if (services == null)
      return (Font) null;
    StatusesInfoSlot statusesInfoSlot = this.GetNodeStatusesInfoSlot(nodeId);
    return statusesInfoSlot == null ? parentFont : statusesInfoSlot.Object.GetFont(services, nodeId, columnValue, parentFont);
  }

  public void Reload()
  {
  }

  /// <summary>Метод поиска слота информации о статусе по идентификатору ноды</summary>
  /// <param name="nodeId">Идентификатор ноды</param>
  /// <returns>Найденный слот</returns>
  private delegate StatusesInfoSlot delegateGetNodeStatusesInfoSlot(INodeID nodeId);
}
