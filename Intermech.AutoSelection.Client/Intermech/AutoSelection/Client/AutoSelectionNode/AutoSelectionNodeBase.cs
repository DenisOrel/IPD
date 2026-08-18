// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.AutoSelectionNodeBase
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.AutoSelection.Client.AutoSelectionLog;
using Intermech.AutoSelection.Client.AutoSelectionService;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

public abstract class AutoSelectionNodeBase : MarshalByRefObject, IImsGlobalsSupport
{
  protected string _name;
  protected int _order = -1;
  protected AutoSelectionLogRec _logRec;

  private void InitializeData()
  {
  }

  private void CollectChildNodes(bool recursive, List<AutoSelectionNodeBase> nodes)
  {
    if (nodes == null)
      return;
    foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
    {
      if (!nodes.Contains((AutoSelectionNodeBase) childsNode))
      {
        nodes.Add((AutoSelectionNodeBase) childsNode);
        if (recursive)
          childsNode.CollectChildNodes(true, nodes);
      }
    }
  }

  protected virtual string GetShortInfo() => this.Name;

  protected internal virtual void CollectLinks(
    Dictionary<long, int> id2Types,
    Dictionary<Guid, int> objGuid2Types)
  {
  }

  protected internal virtual void UpdateLinks(
    Dictionary<long, string> id2Caption,
    Dictionary<Guid, string> guid2Caption)
  {
  }

  protected AutoSelectionNodeBase(AutoSelectionNodeBase ownerNode, string name)
  {
    this.OwnerNode = ownerNode;
    this._name = name;
    this.ChildsNodes = new AutoSelNodeList();
    this.InitializeData();
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_3")]
  [CustomDescription("Attribute.AutoSelection.Client_4")]
  public virtual string Name
  {
    get => this._name;
    set => this._name = value;
  }

  [CustomCategory("Attribute.AutoSelection.Client_87")]
  [CustomDisplayName("Attribute.AutoSelection.Client_5")]
  [CustomDescription("Attribute.AutoSelection.Client_6")]
  [ReadOnly(true)]
  public virtual int Order
  {
    get => this._order;
    set => this._order = value;
  }

  [Browsable(false)]
  public virtual string ShortInfo => this.GetShortInfo();

  [Browsable(false)]
  public AutoSelectionNodeBase OwnerNode { get; set; }

  [Browsable(false)]
  public Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule Rule
  {
    get
    {
      for (AutoSelectionNodeBase selectionNodeBase = this; selectionNodeBase != null; selectionNodeBase = selectionNodeBase.OwnerNode)
      {
        if (selectionNodeBase is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule rule)
          return rule;
      }
      return (Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule) null;
    }
  }

  [Browsable(false)]
  public AutoSelNodeList ChildsNodes { get; }

  public override string ToString() => this.Name;

  public List<AutoSelectionNodeBase> CollectChildNodes(bool recursive)
  {
    List<AutoSelectionNodeBase> nodes = new List<AutoSelectionNodeBase>();
    this.CollectChildNodes(recursive, nodes);
    return nodes;
  }

  public abstract AutoSelExecuteStatus Execute(
    AutoSelectionSession asSession,
    AutoSelectionLogRec logRec);

  protected void DoExecuteCheckArgs(AutoSelectionSession asSession, AutoSelectionLogRec logRec)
  {
    if (asSession == null)
      throw new ArgumentNullException(nameof (asSession));
    if (logRec == null)
      throw new ArgumentNullException(nameof (logRec));
  }

  public IEnumerable<Guid> GetMetaDataGuids(IMSGlobals type)
  {
    return (IEnumerable<Guid>) this.CollectMetaDataGuids(type, (ICollection<Guid>) new List<Guid>());
  }

  public virtual ICollection<Guid> CollectMetaDataGuids(
    IMSGlobals type,
    ICollection<Guid> collector)
  {
    foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) this.ChildsNodes)
    {
      if (this != childsNode && childsNode != null)
        childsNode.CollectMetaDataGuids(type, collector);
    }
    return collector;
  }
}
