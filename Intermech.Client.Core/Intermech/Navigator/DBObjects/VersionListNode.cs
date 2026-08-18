
// Type: Intermech.Navigator.DBObjects.VersionListNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

public class VersionListNode(int objTypeID, long objID) : ObjectNode(objTypeID, objID)
{
  public override INode GetChild(INodeID nodeID) => (INode) null;

  protected override List<PartSlot> CreateFolderSlots()
  {
    IViewState service = (IViewState) this.Services.GetService(typeof (IViewState));
    return service != null && (service.ViewState & ViewStateFlags.NodeInTree) == ViewStateFlags.NodeInTree ? (List<PartSlot>) null : base.CreateFolderSlots();
  }
}
