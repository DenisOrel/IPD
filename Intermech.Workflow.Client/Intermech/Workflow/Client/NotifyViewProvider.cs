// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.NotifyViewProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

internal class NotifyViewProvider : IViewsProvider
{
  private int _attributeNotices = -1;

  public NotifyViewProvider()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttributeType attributeType = sessionKeeper.Session.GetAttributeType(new Guid("cad001be-306c-11d8-b4e9-00304f19f545"));
      if (attributeType == null)
        return;
      this._attributeNotices = attributeType.AttributeID;
    }
  }

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items == null || items.Count <= 0 || !this.IsItemsEnabledForNotification(items))
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("Workflow.NotifyView", new ViewInfo(0, 709, typeof (NotifyView)));
    return views;
  }

  private bool IsItemsEnabledForNotification(ISelectedItems items)
  {
    for (int index = 0; index < items.Count; ++index)
    {
      if (!(items.GetItemData(index, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData))
        return false;
      IMSObjectType objectType = MetaDataHelper.GetObjectType(itemData.ObjectType);
      if (objectType == null || !objectType.Options.HasFlag((Enum) ObjectTypeOptions.NotificationsEnabled))
        return false;
    }
    return true;
  }
}
