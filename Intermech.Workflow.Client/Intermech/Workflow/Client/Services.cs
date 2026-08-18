// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.Services
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

#nullable disable
namespace Intermech.Workflow.Client;

public sealed class Services
{
  public static Intermech.Navigator.VirtualNodes.HiveDescriptor MailDescriptor;

  public static void Start()
  {
    Services.RegisterHandlers();
    Services.RegisterIcons();
    Services.MailDescriptor = (Intermech.Navigator.VirtualNodes.HiveDescriptor) new Intermech.Workflow.Client.MailDescriptor();
    BaseHolder.Factory.AddGlobalNode(new Guid("C3E28400-10B6-4ea4-AB0F-662F4AFC1F16"), (IDescriptor) Services.MailDescriptor, 20);
    QueryEvents.BeforeClientRecordsSelectEvent += new BeforeClientRecordsSelectHandler(Services.QueryEvents_BeforeClientRecordsSelectEvent);
  }

  private static void QueryEvents_BeforeClientRecordsSelectEvent(
    object sender,
    BeforeClientRecordsSelectEventArgs args)
  {
    if (!(sender is ObjectsQuery) || ((ObjectsQuery) sender).Services.GetService(typeof (MailObjectsPart)) == null)
      return;
    args.NewParameters = new DBRecordSetParams?(args.OldParameters);
    args.NewParameters.Value.Tags[(object) "LocalTypesSelector"] = (object) new LocalTypesList(wfConsts.MailObjectTypes.ToArray());
  }

  public static void Stop()
  {
  }

  private static void RegisterHandlers()
  {
    Intermech.Navigator.Consts.CategoryMailInbox = BaseHolder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryMailInboxGuid);
    BaseHolder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryMailInbox, typeof (InboxNode));
    BaseHolder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryMailInbox, (IViewsProvider) new MailViewsProviders());
    Intermech.Navigator.Consts.CategoryMailOutbox = BaseHolder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryMailOutboxGuid);
    BaseHolder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryMailOutbox, typeof (OutboxNode));
    BaseHolder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryMailOutbox, (IViewsProvider) new MailViewsProviders());
    Intermech.Navigator.Consts.CategoryMailProcessed = BaseHolder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryMailProcessedGuid);
    BaseHolder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryMailProcessed, typeof (CompletedNode));
    BaseHolder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryMailProcessed, (IViewsProvider) new MailViewsProviders());
    Intermech.Navigator.Consts.CategoryMailTrash = BaseHolder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryMailTrashGuid);
    BaseHolder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryMailTrash, typeof (TrashNode));
    BaseHolder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryMailTrash, (IViewsProvider) new MailViewsProviders());
    Intermech.Navigator.Consts.CategoryMail = BaseHolder.GuidMapper.Register(Intermech.Navigator.Consts.CategoryMailGuid);
    BaseHolder.Factory.AddNodeType(Intermech.Navigator.Consts.CategoryMail, typeof (MailNode));
    BaseHolder.Factory.AddViewsProvider(Intermech.Navigator.Consts.CategoryMail, (IViewsProvider) new MailViewProvider());
    BaseHolder.Factory.AddViewsProvider(1, Intermech.Navigator.Selections.Consts.SelectionsTypeID, (IViewsProvider) new MailViewsProviders());
    MailItemsView.MailNodeCategories.AddRange((IEnumerable<int>) new int[4]
    {
      Intermech.Navigator.Consts.CategoryMailInbox,
      Intermech.Navigator.Consts.CategoryMailOutbox,
      Intermech.Navigator.Consts.CategoryMailProcessed,
      Intermech.Navigator.Consts.CategoryMailTrash
    });
  }

  private static void RegisterIcons()
  {
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Mail.ico"))
    {
      Icon icon = new Icon(resourceStream);
      BaseHolder.IconService.AddIcon(icon, Intermech.Navigator.Consts.CategoryMail, 0, (object) null);
      Intermech.Workflow.Design.Holder.MailImageIndex = BaseHolder.NamedList.Add(icon, "wfMail");
    }
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Inbox.ico"))
      BaseHolder.IconService.AddIcon(new Icon(resourceStream), Intermech.Navigator.Consts.CategoryMailInbox, 0, (object) null);
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Outbox.ico"))
      BaseHolder.IconService.AddIcon(new Icon(resourceStream), Intermech.Navigator.Consts.CategoryMailOutbox, 0, (object) null);
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Processed.ico"))
      BaseHolder.IconService.AddIcon(new Icon(resourceStream), Intermech.Navigator.Consts.CategoryMailProcessed, 0, (object) null);
    using (Stream resourceStream = Intermech.Navigator.Services.GetResourceStream("Trash.ico"))
      BaseHolder.IconService.AddIcon(new Icon(resourceStream), Intermech.Navigator.Consts.CategoryMailTrash, 0, (object) null);
  }
}
