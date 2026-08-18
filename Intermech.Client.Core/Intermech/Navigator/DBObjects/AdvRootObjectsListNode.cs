
// Type: Intermech.Navigator.DBObjects.AdvRootObjectsListNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Parts;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Корневой узел, содержащий в своём составе объекты из указанного списка
/// </summary>
public class AdvRootObjectsListNode : ObjectsListNode
{
  /// <summary>
  /// Список идентификаторов объектов.
  /// Для каждого объекта (ключи в словарике) - свой список объектов.
  /// </summary>
  private Dictionary<long, List<long>> _objectLists;

  /// <summary>Создать экземпляр узла</summary>
  /// <param name="mainObjects">Список ключей в objectLists</param>
  /// <param name="objectLists">Список идентификаторов объектов.
  /// Для каждого объекта (ключи в словарике) - свой список объектов.</param>
  public AdvRootObjectsListNode(IList mainObjects, Dictionary<long, List<long>> objectLists)
    : base(mainObjects)
  {
    this._objectLists = objectLists;
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    if (this._objectLists == null || this._objectLists.Count == 0)
      return (List<PartSlot>) null;
    long[] numArray = new long[this._objectLists.Count];
    this._objectLists.Keys.CopyTo(numArray, 0);
    return this.SlotsFromSinglePart((INodePart) new AdvObjectsListPart((IList) numArray, this._objectLists, this.Services));
  }

  /// <summary>
  /// Создает и возвращает части, которые отвечают за элементы-не-папки.
  /// </summary>
  /// <returns>Коллекция частей</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    if (this._objectLists == null || this._objectLists.Count == 0)
      return (List<PartSlot>) null;
    List<long> objectIDs = new List<long>();
    foreach (KeyValuePair<long, List<long>> objectList in this._objectLists)
    {
      for (int index = 0; index < objectList.Value.Count; ++index)
      {
        if (!objectIDs.Contains(objectList.Value[index]))
          objectIDs.Add(objectList.Value[index]);
      }
    }
    return objectIDs.Count > 0 ? this.SlotsFromSinglePart((INodePart) new ObjectsListPart((IList) objectIDs, this.Services)) : (List<PartSlot>) null;
  }
}
