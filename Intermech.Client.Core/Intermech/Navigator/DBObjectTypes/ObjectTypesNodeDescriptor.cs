
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.DBObjectTypes;

/// <summary>
/// Дескриптор, создающий корень дерева всех типов объектов - узел "Объекты"
/// </summary>
public class ObjectTypesNodeDescriptor : HiveDescriptor
{
  /// <summary>Создает дескриптор корня дерева типов объектов</summary>
  public ObjectTypesNodeDescriptor()
    : base(Intermech.Navigator.Consts.CategoryObjectTypes, 0, LocalizationHolder.rm.GetString("Client.Core_1099"))
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state"></param>
  protected ObjectTypesNodeDescriptor(PersistentState state)
    : this()
  {
  }

  /// <summary>Выполняет сериализацию дескриптора.</summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
  }

  /// <summary>Создать идентификатор корневого узла "Все объекты"</summary>
  /// <returns>Идентификатор корневого узла "Все объекты"</returns>
  public override INodeID GetRecordNodeID()
  {
    return (INodeID) new ObjectTypesNodeID(this._categoryID, this._typeID, AccessRights.NotDefined);
  }

  public override INode GetChild(INodeID nodeID)
  {
    if (!(nodeID is ObjectTypesNodeID objectTypesNodeId))
      return base.GetChild(nodeID);
    return ((INodesFactory) ServicesManager.GetService(typeof (IFactory))).GetNode(nodeID, (object) objectTypesNodeId.AccessRights);
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new ObjectTypesNodeDescriptor();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  public override bool Equals(object obj) => obj == this || obj is ObjectTypesNodeDescriptor;

  public override int GetHashCode() => base.GetHashCode() ^ this._caption.GetHashCode();
}
