
// Type: Intermech.Navigator.DBObjects.AdvObjectsListNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Дочерний узел, содержащий в своём составе объекты из указанного списка
/// </summary>
public class AdvObjectsListNode : ObjectNode
{
  public Dictionary<long, List<long>> ObjectLists;

  /// <summary>Создать экземпляр узла</summary>
  /// <param name="objTypeID">Тип</param>
  /// <param name="objID">Идентификатор версии объекта</param>
  /// <param name="objectLists"></param>
  public AdvObjectsListNode(int objTypeID, long objID, Dictionary<long, List<long>> objectLists)
    : base(objTypeID, objID)
  {
    this.ObjectLists = objectLists;
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-не-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.ObjectLists == null || !this.ObjectLists.ContainsKey(this._objID) ? (List<PartSlot>) null : this.SlotsFromSinglePart((INodePart) new ObjectsListPart((IList) this.ObjectLists[this._objID], this.Services));
  }
}
