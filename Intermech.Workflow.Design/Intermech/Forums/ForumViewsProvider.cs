// Decompiled with JetBrains decompiler
// Type: Intermech.Forums.ForumViewsProvider
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Forums;

public class ForumViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count == 1 && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(itemData.ObjectType);
      if (objectType != null && (objectType.ObjectTypeID == ForumsConsts.forumObjectTypeID || (objectType.Options & ObjectTypeOptions.ForumEnabled) == ObjectTypeOptions.ForumEnabled))
      {
        ViewsInfo views = new ViewsInfo();
        views.Add("ForumView", new ViewInfo(0, 2630, typeof (ForumView)));
        return views;
      }
    }
    return ViewsInfo.Empty;
  }
}
