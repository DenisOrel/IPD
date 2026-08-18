
// Type: Intermech.Search.RecentObjects.CurrentUserRecentObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Search.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Intermech.Search.RecentObjects;

public sealed class CurrentUserRecentObjectsNode : ObjectNode, INodeNotificaionSupport
{
  public CurrentUserRecentObjectsNode()
    : base(-1, 0L)
  {
  }

  public override IUpdateAnalyser GetAnalyser(
    NodeViewCapabilities capabilities,
    object sender,
    NotificationEventArgs e)
  {
    return e.EventName == "RecentObjectsChanged" && e is RecentObjectsChangedEventArgs recentObjectsChangedEventArgs ? (IUpdateAnalyser) new RecentObjectsChangedUpdateAnalyzer(this, recentObjectsChangedEventArgs) : base.GetAnalyser(capabilities, sender, e);
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    long[] array = ((IEnumerable<long>) ServiceLocator.Get<IRecentObjectsClientService>().GetCurrentUserRecentObjects()).Where<long>((Func<long, bool>) (o => !ObjectHelper.IsUnknownObjectVersionID(o))).ToArray<long>();
    if (array.Length != 0)
      folderSlots.Add(new PartSlot(new Guid("28429849-2607-4BF1-8038-9A6435DF3E09"), (INodePart) ObjectsNodePart.CreateForObjects(array, this.Services)));
    return folderSlots;
  }

  protected override List<PartSlot> CreateNonFolderSlots() => new List<PartSlot>(0);

  public override ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    if (e.EventName == "RecentObjectsCleared")
    {
      this.folderSlots = (List<PartSlot>) null;
      return ProcessResult.RefreshNode;
    }
    return e.EventName == "RecentObjectsChanged" && e is RecentObjectsChangedEventArgs changedEventArgs && changedEventArgs.AddedRecentObjects.Length != 0 ? ProcessResult.RefreshNode : ProcessResult.None;
  }

  public bool DisableFocusAfterNodeAdded => true;
}
