
// Type: Intermech.Navigator.DBObjectTypes.Helper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjectTypes.Implementation;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;


namespace Intermech.Navigator.DBObjectTypes;

public sealed class Helper
{
  private const string PropObjTypeID = "ObjTypeId";

  public static object MapColumnToField(NodeColumn column)
  {
    if (column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_CAPTION"))
      return (object) "F_OBJ_TYPE_NAME";
    return column.SchemeGuid == Intermech.Navigator.Consts.ObjectTypeColumnSchemeGuid ? column.ID : (object) null;
  }

  public static INode GetChild(INodeID nodeID)
  {
    NodeID nodeId = nodeID as NodeID;
    SelectionNodeID nodeID1 = nodeID as SelectionNodeID;
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    if (nodeID1 != null)
      return service.GetNode((INodeID) nodeID1, (object) nodeID1.TypeID, (object) nodeID1.HandSelection);
    if (nodeId == null)
      return (INode) null;
    return service.GetNode(nodeID, (object) nodeId.TypeID, (object) nodeId.AccessRights);
  }

  public static ContentAttributes GetAttributesOf(INodeID nodeID) => ContentAttributes.HasChildren;

  public static string GetObjectTypeName(int objTypeID)
  {
    return MetaDataHelper.GetObjectTypeName(objTypeID);
  }

  public static string GetAddress(INodeID nodeID) => Helper.GetObjectTypeName(nodeID.TypeID);

  public static object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new Descriptor(nodeID.TypeID);
    if (dataFormat == typeof (ICanOpenInNewWindow))
      return (object) new CanOpenInNewWindow();
    return dataFormat == typeof (IDBObjectTypeID) ? (object) new DBObjectTypeID(nodeID.TypeID) : (object) null;
  }

  public static void AddAllColumns(NodeColumnCollection columns)
  {
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectTypeColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) "F_OBJECT_TYPE"));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) "F_OBJ_TYPE_NAME"));
    columns.Add(service.CreateColumn(columnSchemeGuid, (object) "F_OBJ_NAME"));
  }

  public static PersistentState Serialize(INodeID nodeID)
  {
    PersistentState persistentState = new PersistentState();
    persistentState.AddValue("ObjTypeId", (object) nodeID.TypeID);
    return persistentState;
  }

  public static INodeID Deserialize(PersistentState persistentNodeID)
  {
    object obj = persistentNodeID.GetValue("ObjTypeId");
    return obj != null && obj is int objTypeID ? (INodeID) new NodeID(objTypeID, AccessRights.NotDefined) : (INodeID) null;
  }
}
