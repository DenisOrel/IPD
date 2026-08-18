
// Type: Intermech.Navigator.DBObjects.AdvRootObjectsListNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.VirtualNodes;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Описание корневого виртуального узла со списками объектов
/// </summary>
public class AdvRootObjectsListNodeID : HiveNodeID
{
  /// <summary>
  /// Для каждого объекта (ключи в словарике) - свой список объектов
  /// </summary>
  public Dictionary<long, List<long>> ObjectLists;

  /// <summary>
  /// Создать описание корневого виртуального узла со списками объектов
  /// </summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="objectLists">Для каждого объекта (ключи в словарике) - свой список объектов</param>
  public AdvRootObjectsListNodeID(
    int categoryID,
    int typeID,
    Dictionary<long, List<long>> objectLists)
    : base(categoryID, typeID)
  {
    this.ObjectLists = objectLists;
  }
}
