// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailNode
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Workflow.Client;

public class EmailNode : CompositeNode, IContextAware, IEmailNode, INodeNotifications
{
  private string _accauntEmail = string.Empty;
  private IServiceProvider _services;

  public EmailNode()
  {
  }

  public EmailNode(string accauntEmail) => this._accauntEmail = accauntEmail;

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new EmailInboxPart(this.Services, this._accauntEmail));
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    List<PartSlot> folderSlots = new List<PartSlot>(1);
    ITopBinding binding = (ITopBinding) new EmailNodeBinding(this._accauntEmail);
    folderSlots.Insert(0, new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(new DescriptorCollection()
    {
      {
        Intermech.Navigator.Selections.Consts.SelectionsDescriptorGuid,
        (IDescriptor) new HiveDescriptor(MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545"), binding)
      }
    }, false)));
    return folderSlots;
  }

  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => this._services;
    set => this._services = value;
  }

  public string AccauntEmail => this._accauntEmail;

  public ProcessResult Process(NotificationEventArgs e, object AdditionalInfo)
  {
    return e.EventName == "EmailImported" ? ProcessResult.RefreshNode : ProcessResult.None;
  }
}
