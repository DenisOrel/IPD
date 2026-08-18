
// Type: Intermech.Navigator.DBObjectTypes.ObjectTypesItems
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Reflection;


namespace Intermech.Navigator.DBObjectTypes;

public class ObjectTypesItems : INodeItems
{
  public ContentAttributes GetAttributesOf(INodeID nodeID) => Helper.GetAttributesOf(nodeID);

  public virtual INode GetChild(INodeID nodeID)
  {
    return nodeID is NodeID nodeID1 ? this.CreateNode(nodeID1) : Helper.GetChild(nodeID);
  }

  public string GetAddress(INodeID nodeID) => Helper.GetAddress(nodeID);

  /// <summary>
  /// Возвращает идентификатор дочернего элемента по его адресу из адресной
  /// строки навигатора. Если преобразовать адрес не удается, то метод
  /// должен вернуть null.
  /// </summary>
  /// <param name="address">Адрес дочернего элемента</param>
  /// <returns>Идентификатор дочернего элемента</returns>
  public INodeID ParseAddress(string address)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        return (INodeID) new NodeID(sessionKeeper.Session.GetObjectType(address).ObjectType, AccessRights.NotDefined);
      }
      catch
      {
      }
    }
    return (INodeID) null;
  }

  public PersistentState Serialize(INodeID nodeID) => Helper.Serialize(nodeID);

  public INodeID Deserialize(PersistentState persistentNodeID)
  {
    return Helper.Deserialize(persistentNodeID);
  }

  public object GetData(INodeID nodeID, Type dataFormat) => Helper.GetData(nodeID, dataFormat);

  public object[] GetData(NodeIDCollection nodeIDs, Type dataFormat)
  {
    object[] data = new object[nodeIDs.Count];
    for (int index = 0; index < data.Length; ++index)
      data[index] = this.GetData(nodeIDs[index], dataFormat);
    return data;
  }

  public virtual IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    if (e is DBObjectTypesEventArgs objectTypesEventArgs)
    {
      switch (e.EventName)
      {
        case "ObjectTypesCreated":
          return !capabilities.CanAppend ? (IUpdateAnalyser) null : (IUpdateAnalyser) new ObjectTypesCreatedAnalyser(objectTypesEventArgs.ObjectTypeIDs);
        case "ObjectTypesChanged":
          return (IUpdateAnalyser) new ObjectTypesChangedAnalyser(objectTypesEventArgs.ObjectTypeIDs);
        case "ObjectTypesRemoved":
          return (IUpdateAnalyser) new ObjectTypesRemovedAnalyser(objectTypesEventArgs.ObjectTypeIDs);
      }
    }
    return (IUpdateAnalyser) null;
  }

  public object GetService(Type service) => (object) null;

  private INode CreateNode(NodeID nodeID)
  {
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    return service is Factory factory ? (factory.GetNodeType((INodeID) nodeID).GetConstructor(new Type[1]
    {
      typeof (NodeID)
    }) != (ConstructorInfo) null ? service.GetNode((INodeID) nodeID, (object) nodeID) : service.GetNode((INodeID) nodeID, (object) nodeID.TypeID, (object) nodeID.AccessRights)) : service.GetNode((INodeID) nodeID, (object) nodeID);
  }
}
