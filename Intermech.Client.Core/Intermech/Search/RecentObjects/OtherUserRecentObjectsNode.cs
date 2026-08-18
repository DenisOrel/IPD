
// Type: Intermech.Search.RecentObjects.OtherUserRecentObjectsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Parts;
using Intermech.Search.Navigator;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;


namespace Intermech.Search.RecentObjects;

public sealed class OtherUserRecentObjectsNode : ObjectNode
{
  private long _userVersionID;

  public OtherUserRecentObjectsNode(long userVersionID)
    : base(-1, 0L)
  {
    this._userVersionID = !ObjectHelper.IsUnknownObjectVersionID(userVersionID) ? userVersionID : throw new ArgumentException();
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      long[] userRecentObjects = ((IRecentObjectsServerService) sessionKeeper.Session.GetCustomService(typeof (IRecentObjectsServerService))).GetOtherUserRecentObjects(sessionKeeper.Session.SessionGUID, this._userVersionID);
      if (userRecentObjects.Length != 0)
        folderSlots.Add(new PartSlot(new Guid("28429849-2607-4BF1-8038-9A6435DF3E09"), (INodePart) ObjectsNodePart.CreateForObjects(userRecentObjects, this.Services)));
    }
    return folderSlots;
  }

  protected override List<PartSlot> CreateNonFolderSlots() => new List<PartSlot>(0);
}
