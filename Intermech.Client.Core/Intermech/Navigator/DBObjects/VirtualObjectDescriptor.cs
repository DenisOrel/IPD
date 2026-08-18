
// Type: Intermech.Navigator.DBObjects.VirtualObjectDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Виртуальный дескриптор для узла "Объект не существовал на указанную дату"
/// </summary>
public class VirtualObjectDescriptor : HiveDescriptor
{
  /// <summary>Создать экземпляр класса</summary>
  public VirtualObjectDescriptor()
    : base(Intermech.Navigator.Consts.CategoryVirtualObjectNode, 0, LocalizationHolder.rm.GetString("Client.Core_1097"))
  {
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="categoryID">Категория</param>
  /// <param name="typeID">Тип</param>
  /// <param name="caption">Заголовок</param>
  public VirtualObjectDescriptor(int categoryID, int typeID, string caption)
    : base(categoryID, typeID, caption)
  {
  }

  /// <summary>Десериализовать описание узла</summary>
  /// <param name="persistNodeID">Строка с сериализованным описанием узла</param>
  /// <returns>Описание узла</returns>
  public override INodeID Deserialize(PersistentState persistNodeID)
  {
    return (INodeID) new VirtualObjectNodeID(LocalizationHolder.rm.GetString("Client.Core_331"));
  }

  /// <summary>
  /// Вернуть описание корневого узла для текущего дескриптора
  /// </summary>
  /// <returns>Описание корневого узла для текущего дескриптора</returns>
  public override INodeID GetRecordNodeID() => (INodeID) new VirtualObjectNodeID(this._caption);

  /// <summary>
  /// Вернуть данные определённого формата по указанному описанию узла
  /// </summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new VirtualObjectDescriptor(this._categoryID, this._typeID, this._caption);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  /// <summary>Сравнить дескриптор с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    if (!(obj is VirtualObjectDescriptor objectDescriptor))
      return base.Equals(obj);
    return this._categoryID == objectDescriptor._categoryID && this._typeID == objectDescriptor._typeID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра класса</summary>
  /// <returns>32-битный хэш-код экземпляра класса</returns>
  public override int GetHashCode() => base.GetHashCode();
}
