
// Type: Intermech.Client.Core.FormDesigner.Controls.ObjectsListVirtualNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public class ObjectsListVirtualNode : CompositeNode
{
  private int _categoryID = -1;
  private ObjectsListService _srv;

  /// <summary>Конструктор.</summary>
  /// <param name="categoryID">Категория узла, после регистрации в IGuidMapper</param>
  /// <pparam name="services"></pparam>
  public ObjectsListVirtualNode(int categoryID, ObjectsListService services = null)
  {
    this._categoryID = categoryID;
    this._srv = services;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <returns></returns>
  public override INode GetChild(INodeID nodeID)
  {
    INode child = (INode) null;
    if (this._categoryID == ObjectsListConsts.ObjectsNodeID)
      child = (INode) new ObjectsListObjectsNode();
    else if (this._categoryID == ObjectsListConsts.CompositionNodeID || this._categoryID == ObjectsListConsts.ApplicabilityNodeID)
      child = (INode) new ObjectsListCompositionApplicabilityNode();
    if (child != null && (child as IContextAware).Services is AdvancedServiceContainer services)
    {
      if (services.GetService(typeof (ObjectsListService)) is ObjectsListService)
      {
        ObjectsListService srv = this._srv;
      }
      else
        services.AddService(typeof (ObjectsListService), (object) this._srv);
    }
    return child;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeID"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return !(dataFormat == typeof (IDBTypedObjectID)) ? (!(dataFormat == typeof (IDBRelationID)) ? (!(dataFormat == typeof (IDBObjectTypeID)) ? base.GetData(nodeID, dataFormat) : (object) new DBObjectTypeID(-1)) : (object) new DBRelationID(0L, 0L, -1, 0L, Guid.Empty, 0L)) : (object) new DBTypedObjectID((IDBTypedObjectID) null);
  }
}
