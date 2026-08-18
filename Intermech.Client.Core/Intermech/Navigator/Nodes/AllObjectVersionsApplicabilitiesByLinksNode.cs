
// Type: Intermech.Navigator.Nodes.AllObjectVersionsApplicabilitiesByLinksNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Nodes;

/// <summary>Узел объекта с применяемостью по ссылкам</summary>
/// <summary>Конструктор</summary>
/// <param name="objectVersionID">Идентификатор версии объекта</param>
/// <param name="objectTypeID">Идентификатор типа объекта</param>
public sealed class AllObjectVersionsApplicabilitiesByLinksNode(
  long objectVersionID,
  int objectTypeID) : ObjectNode(objectTypeID, objectVersionID)
{
  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long objectIdVersion in sessionKeeper.Session.GetObjectIDVersions(this._objID))
        folderSlots.Add(new PartSlot(Guid.NewGuid(), (INodePart) new ObjectApplicabilityByLinksPart(objectIdVersion, this.Services)));
    }
    return folderSlots;
  }
}
