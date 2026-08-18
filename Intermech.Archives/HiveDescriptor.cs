// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.HiveDescriptor
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.DataFormats;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Дескриптор, создающий корень дерева всех типов объектов - узел "Архивы документов"
/// </summary>
public class HiveDescriptor : Intermech.Navigator.VirtualNodes.HiveDescriptor
{
  /// <summary>Создает дескриптор узла "Архивы документов"</summary>
  public HiveDescriptor()
    : base(Consts.CategoryArchivesNode, 0, ServiceHolder.rm.GetString("Archives_65"))
  {
  }

  /// <summary>Создает дескриптор узла "Архивы документов"</summary>
  /// <param name="caption">наименование</param>
  public HiveDescriptor(string caption)
    : base(Consts.CategoryArchivesNode, 0, caption)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора узла "Архивы документов"
  /// </summary>
  /// <param name="state"></param>
  protected HiveDescriptor(PersistentState state)
    : base(state)
  {
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
      return (object) new HiveDescriptor();
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    return dataFormat == typeof (IDBObjectTypeSelectionID) ? (object) new DBBindedObjectType(ConstsHolder.DocTypeID) : base.GetData(nodeID, dataFormat);
  }

  /// <summary>Создать идентификатор корневого узла "Все объекты"</summary>
  /// <returns>Идентификатор корневого узла "Все объекты"</returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new ArchievesNodeID(this._categoryID, this._typeID, AccessRights.NotDefined);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID)
  {
    if (!(nodeID is ArchievesNodeID archievesNodeId) || !(ServicesManager.GetService(typeof (IFactory)) is IFactory service))
      return base.GetChild(nodeID);
    INodeID nodeID1 = nodeID;
    object[] objArray = new object[1]
    {
      (object) archievesNodeId.AccessRights
    };
    return service.GetNode(nodeID1, objArray);
  }
}
