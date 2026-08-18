// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.MailBoxNode
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Workflow.Client;

public abstract class MailBoxNode : CompositeNode, IContextAware
{
  private MailType _mailType;
  private IServiceProvider services;

  public MailBoxNode(MailType type) => this._mailType = type;

  public MailType MailType => this._mailType;

  public virtual IServiceProvider Services
  {
    [DebuggerStepThrough] get => this.services;
    set => this.services = value;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart(this.GetPart((IConditionsProvider) null));
  }

  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection specialDescriptors = this.GetSpecialDescriptors(true, false);
    List<PartSlot> folderSlots = new List<PartSlot>();
    folderSlots.Insert(0, new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(specialDescriptors)));
    return folderSlots;
  }

  protected override ITopBinding GetBinding(BindingType bindingType)
  {
    return (ITopBinding) new MailNodeBinding(this, bindingType);
  }

  public abstract ConditionStructure[] Conditions { get; }

  public virtual int ElementsTypeID => wfConsts.ProcessAtomsTypeID;

  public abstract INodePart GetPart(IConditionsProvider conditionProvider);
}
