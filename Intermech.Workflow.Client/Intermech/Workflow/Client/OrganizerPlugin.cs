// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.OrganizerPlugin
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Client.Core.Organizer;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Specialized;

#nullable disable
namespace Intermech.Workflow.Client;

internal class OrganizerPlugin
{
  public static readonly Guid InboxOrganizerNodeGuid = new Guid("{5DC919F5-8F34-43b8-940C-11AD4AD6E7E3}");

  public static void Init()
  {
    if (!(ApplicationServices.Container.GetService(typeof (IOrganizerService)) is IOrganizerService service1) || MailNode.InboxDescriptor == null)
      return;
    NodeColumnCollection columns = new NodeColumnCollection();
    Guid columnSchemeGuid1 = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    Guid columnSchemeGuid2 = Intermech.Navigator.Consts.ObjectColumnSchemeGuid;
    IColumnSchemes service2 = (IColumnSchemes) ApplicationServices.Container.GetService(typeof (IColumnSchemes));
    columns.Add(service2.CreateColumn(columnSchemeGuid1, (object) ObligatoryObjectAttributes.CAPTION));
    columns.Add(service2.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrProcessID));
    NodeColumn column = service2.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrStartedID);
    columns.Add(column);
    columns.Add(service2.CreateColumn(columnSchemeGuid2, (object) wfConsts.AttrSenderID));
    column.SortOrder = NodeColumnSortOrder.Descending;
    column.SortIndex = 0;
    try
    {
      if (!(service1.RegisterNode(OrganizerPlugin.InboxOrganizerNodeGuid, wfConsts.ActivitiesTypeID, InboxNode.StaticConditions, columns, LocalizationHolder.rm.GetString("Workflow.Client_12"), BaseHolder.IconService.IndexOf(Intermech.Navigator.Consts.CategoryMailInbox, 0)) is OrganizerChildNodeDescriptor childNodeDescriptor))
        return;
      if (childNodeDescriptor.Tags == null)
        childNodeDescriptor.Tags = new HybridDictionary();
      childNodeDescriptor.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesList(wfConsts.TermActivityTypes);
    }
    catch
    {
    }
  }
}
