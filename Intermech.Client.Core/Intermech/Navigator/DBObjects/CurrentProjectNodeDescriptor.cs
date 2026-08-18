
// Type: Intermech.Navigator.DBObjects.CurrentProjectNodeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.DBObjects;

/// <summary>Дескриптор для узла с текущим проектом</summary>
public class CurrentProjectNodeDescriptor : Descriptor
{
  /// <summary>
  /// Создать корневой элемент пространства навигации для данного дескриптора
  /// </summary>
  /// <returns>Корневой элемент пространства навигации для данного дескриптора</returns>
  public override INodeID GetRecordNodeID()
  {
    this.CorrectDescriptor((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).CachedProjectID);
    return base.GetRecordNodeID();
  }

  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    NodeID nodeId = (NodeID) nodeID;
    return dataFormat == typeof (ProjectObjectID) && MetaDataHelper.IsObjectTypeChildOf(nodeId.ObjectTypeID, MetaDataHelper.GetObjectTypeID("cad00812-306c-11d8-b4e9-00304f19f545")) ? (object) new ProjectObjectID(nodeId.ObjectID) : base.GetData(nodeID, dataFormat);
  }
}
