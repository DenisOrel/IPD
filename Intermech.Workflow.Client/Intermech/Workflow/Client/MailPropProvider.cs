// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailPropProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.DataFormats;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Design;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Workflow.Client;

public class MailPropProvider : IViewsProvider
{
  private static List<int> _allMailCategories;

  protected static List<int> AllMailCategories
  {
    get
    {
      if (MailPropProvider._allMailCategories == null)
        MailPropProvider._allMailCategories = new List<int>((IEnumerable<int>) new int[4]
        {
          Intermech.Navigator.Consts.CategoryMailInbox,
          Intermech.Navigator.Consts.CategoryMailOutbox,
          Intermech.Navigator.Consts.CategoryMailProcessed,
          Intermech.Navigator.Consts.CategoryMailTrash
        });
      return MailPropProvider._allMailCategories;
    }
  }

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("ObjectFiles", new ViewInfo(0));
    int num = MailItemsView.NodeCategoryID(items, services);
    if (MailPropProvider.AllMailCategories.Contains(num))
    {
      views.Add("ObjectEvents", new ViewInfo(0));
      views.Add("ContainsView", new ViewInfo(0));
      views.Add("PDM.ApplicabilityView", new ViewInfo(0));
      views.Add("PDM.ContainsView", new ViewInfo(0));
      views.Add("ChildrenView", new ViewInfo(0));
      views.Add("ObjectSecurity", new ViewInfo(0));
      views.Add("ObjectVisualizer", new ViewInfo(0));
      views.Add("ContextsSearchView", new ViewInfo(0));
      views.Add("ApplicabilityView", new ViewInfo(0));
      views.Add("MailMessages", new ViewInfo(0, 825, typeof (MessagesView)));
    }
    views.Add("MailAttachments", new ViewInfo(0, 825, typeof (MailAttachmentsView)));
    if (num == Intermech.Navigator.Consts.CategoryMailInbox && items.Count > 0 && items.GetItemData(0, typeof (IDBTypedObjectID)) is IDBTypedObjectID itemData && wfConsts.IsActivity(itemData.ObjectType))
      views.Add("MailAnswer", new ViewInfo(0, 825, typeof (AnswerView)));
    return views;
  }
}
