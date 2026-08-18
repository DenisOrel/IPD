
// Type: Intermech.Navigator.Nodes.ObjectApplicabilityByLinksNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Nodes;

/// <summary>Узел объекта с применяемостью по ссылкам</summary>
/// <summary>Конструктор</summary>
/// <param name="objectVersionID">Идентификатор версии объекта</param>
/// <param name="objectTypeID">Идентификатор типа объекта</param>
public sealed class ObjectApplicabilityByLinksNode(long objectVersionID, int objectTypeID) : 
  ObjectNode(objectTypeID, objectVersionID)
{
  protected override List<PartSlot> CreateFolderSlots()
  {
    return new List<PartSlot>()
    {
      new PartSlot(Guid.NewGuid(), (INodePart) new ObjectApplicabilityByLinksPart(this._objID, this.Services))
    };
  }
}
