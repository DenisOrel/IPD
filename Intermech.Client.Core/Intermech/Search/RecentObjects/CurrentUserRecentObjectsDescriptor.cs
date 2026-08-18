
// Type: Intermech.Search.RecentObjects.CurrentUserRecentObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;


namespace Intermech.Search.RecentObjects;

public sealed class CurrentUserRecentObjectsDescriptor : HiveDescriptor
{
  public CurrentUserRecentObjectsDescriptor()
    : base(Intermech.Navigator.Consts.CategoryRecentObjectsNode, 0, LocalizationHolder.rm.GetString("Client.Core_296"))
  {
  }

  private CurrentUserRecentObjectsDescriptor(PersistentState state)
    : this()
  {
  }

  public override INode GetChild(INodeID nodeID) => (INode) new CurrentUserRecentObjectsNode();
}
