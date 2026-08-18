
// Type: Intermech.Navigator.DBObjects.VirtualGrouingObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Виртуальный дескриптор для узла "Найденные группирующие объекты"
/// </summary>
public class VirtualGrouingObjectsDescriptor : ListDescriptor
{
  /// <summary>Создать экземпляр дескриптора</summary>
  public VirtualGrouingObjectsDescriptor()
    : this(Intermech.Navigator.Consts.CategoryGroupingObjectsNode, 0, LocalizationHolder.rm.GetString("Client.Core_1098"), (IList) new List<long>())
  {
  }

  /// <summary>Создать экземпляр дескриптора</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="caption">Заголовок</param>
  /// <param name="objectIDs">Список идентификаторов объектов</param>
  public VirtualGrouingObjectsDescriptor(
    int categoryID,
    int typeID,
    string caption,
    IList objectIDs)
    : base(categoryID, typeID, caption, objectIDs)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public VirtualGrouingObjectsDescriptor(PersistentState state)
    : base(state)
  {
    List<long> longList = new List<long>();
    this._objectIDs = (IList) longList;
    long result1 = 0;
    object obj1 = state.GetValue("Count");
    if (obj1 == null || !long.TryParse(obj1.ToString(), out result1))
      return;
    for (int index = 0; (long) index < result1; ++index)
    {
      long result2 = 0;
      object obj2 = state.GetValue("Item" + index.ToString());
      if (obj2 != null && long.TryParse(obj2.ToString(), out result2))
        longList.Add(result2);
    }
  }

  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("Count", (object) this._objectIDs.Count.ToString());
    for (int index = 0; index < this._objectIDs.Count; ++index)
      state.AddValue("Item" + index.ToString(), (object) this._objectIDs[index].ToString());
  }

  /// <summary>Десериализовать описание узла</summary>
  /// <param name="persistNodeID">Строка с сериализованным описанием узла</param>
  /// <returns>Описание узла</returns>
  public override INodeID Deserialize(PersistentState persistNodeID)
  {
    return (INodeID) new VirtualGrouingObjectsNodeID(this._caption);
  }

  /// <summary>
  /// Вернуть описание корневого узла для текущего дескриптора
  /// </summary>
  /// <returns>Описание корневого узла для текущего дескриптора</returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new VirtualGrouingObjectsNodeID(this._caption);
  }

  /// <summary>
  /// Вернуть данные определённого формата по указанному описанию узла
  /// </summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new VirtualGrouingObjectsDescriptor(this._categoryID, this._typeID, this._caption, this._objectIDs);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }
}
