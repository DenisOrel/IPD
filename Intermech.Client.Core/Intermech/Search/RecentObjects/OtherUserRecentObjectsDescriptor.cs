
// Type: Intermech.Search.RecentObjects.OtherUserRecentObjectsDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.VirtualNodes;
using Intermech.Search.Utilities;
using System;


namespace Intermech.Search.RecentObjects;

public sealed class OtherUserRecentObjectsDescriptor : HiveDescriptor
{
  private long _userVersionID;

  public OtherUserRecentObjectsDescriptor(long userVersionID)
    : base(Intermech.Navigator.Consts.CategoryRecentObjectsNode, 1, $"Недавние объекты пользователя {OtherUserRecentObjectsDescriptor.GetUserName(userVersionID)}")
  {
    this._userVersionID = !ObjectHelper.IsUnknownObjectVersionID(userVersionID) ? userVersionID : throw new ArgumentException();
  }

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new OtherUserRecentObjectsNode(this._userVersionID);
  }

  private static string GetUserName(long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(objectVersionID).Caption;
  }
}
