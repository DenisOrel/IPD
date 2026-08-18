
// Type: Intermech.Navigator.Nodes.FullObjectApplicabilityNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Nodes;

/// <summary>
/// Узел объекта с полной применяемостью. Полная применяемость включает применяемость по связям, по ссылкам и по выборкам.
/// </summary>
/// <summary>Конструктор</summary>
/// <param name="objectVersionID">Идентификатор версии объекта</param>
/// <param name="objectTypeID">Идентификатор типа объекта</param>
public sealed class FullObjectApplicabilityNode(long objectVersionID, int objectTypeID) : 
  ObjectApplicabilityByRelationsNode(objectVersionID, objectTypeID)
{
  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = base.CreateFolderSlots();
    folderSlots.Add(new PartSlot(Guid.NewGuid(), (INodePart) new ObjectApplicabilityByLinksPart(this._objID, this.Services)));
    folderSlots.Add(new PartSlot(Guid.NewGuid(), (INodePart) new ObjectApplicabilityByClassifiersPart(this._objID, this.Services)));
    return folderSlots;
  }
}
