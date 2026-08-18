// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeExtendedBase
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public abstract class TreeNodeExtendedBase : TreeNode
{
  [CanBeNull]
  private TreeNodeExtendedBase.EmptyTreeNodeClass _emptyTreeNode;
  private bool _nestedNodesLoading;
  private bool _hasNestedNodes;

  protected TreeNodeExtendedBase()
  {
  }

  protected TreeNodeExtendedBase([NotNull] string text)
    : base(text)
  {
  }

  protected TreeNodeExtendedBase([NotNull] string text, [CanBeEmpty] int imageIndex)
    : base(text, imageIndex, imageIndex)
  {
  }

  protected TreeNodeExtendedBase([NotNull] SerializationInfo serializationInfo, StreamingContext context)
    : base(serializationInfo, context)
  {
  }

  public event TreeNodeExtendedEventHandler AfterCreate;

  internal virtual void OnAfterCreate([CanBeNull] object sender)
  {
    this.HasNestedNodes = this.GetHasNestedNodes();
    this.ImageIndex = this.GetImageIndex();
    this.SelectedImageIndex = this.ImageIndex;
    TreeNodeExtendedEventHandler afterCreate = this.AfterCreate;
    if (afterCreate == null)
      return;
    afterCreate(sender, new TreeNodeExtendedEventArgs(this));
  }

  public event TreeNodeExtendedCancelEventHandler BeforeExpand;

  internal virtual void OnBeforeExpand([CanBeNull] object sender, ref bool cancel)
  {
    if (!this.HasNestedNodes)
    {
      if (this.Nodes.Count > 0)
        this.Nodes.Clear();
      if (this._emptyTreeNode != null)
        this._emptyTreeNode = (TreeNodeExtendedBase.EmptyTreeNodeClass) null;
      cancel = true;
    }
    else
    {
      if (!this.NodesLoaded)
        this.LoadNestedNodes();
      if (this._emptyTreeNode != null)
      {
        this.Nodes.Remove((TreeNode) this._emptyTreeNode);
        this._emptyTreeNode = (TreeNodeExtendedBase.EmptyTreeNodeClass) null;
      }
    }
    TreeNodeExtendedCancelEventArgs e = new TreeNodeExtendedCancelEventArgs(this, cancel);
    TreeNodeExtendedCancelEventHandler beforeExpand = this.BeforeExpand;
    if (beforeExpand != null)
      beforeExpand(sender, e);
    cancel = e.Cancel;
  }

  protected void LoadNestedNodes()
  {
    this._nestedNodesLoading = true;
    try
    {
      IEnumerable<TreeNodeExtendedBase> nestedNodes = this.CreateNestedNodes();
      IReadOnlyCollection<TreeNodeExtendedBase> collection = nestedNodes != null ? nestedNodes.GetCollection<TreeNodeExtendedBase>() : (IReadOnlyCollection<TreeNodeExtendedBase>) null;
      if (collection != null)
      {
        foreach (TreeNodeExtendedBase nodeExtendedBase in (IEnumerable<TreeNodeExtendedBase>) collection)
          nodeExtendedBase.OnAfterCreate((object) this);
        if (this.TreeView != null)
        {
          int? count = collection.TryGetCount<TreeNodeExtendedBase>();
          int num = 1;
          if (count.GetValueOrDefault() > num & count.HasValue)
          {
            this.TreeView.BeginUpdate();
            try
            {
              this.Nodes.AddMany((IEnumerable<TreeNode>) collection);
              goto label_15;
            }
            finally
            {
              this.TreeView.EndUpdate();
            }
          }
        }
        this.Nodes.AddMany((IEnumerable<TreeNode>) collection);
      }
label_15:
      this.Nodes.Remove((TreeNode) this._emptyTreeNode);
      this._emptyTreeNode = (TreeNodeExtendedBase.EmptyTreeNodeClass) null;
      this.NodesLoaded = true;
    }
    finally
    {
      this._nestedNodesLoading = false;
    }
  }

  [CanBeNull]
  [ItemNotNull]
  protected virtual IEnumerable<TreeNodeExtendedBase> CreateNestedNodes()
  {
    return (IEnumerable<TreeNodeExtendedBase>) null;
  }

  protected virtual bool GetHasNestedNodes() => false;

  protected virtual int GetImageIndex() => -1;

  public bool NodesLoaded { get; set; }

  public bool HasNestedNodes
  {
    get => this._emptyTreeNode != null || this.Nodes.Count > 0;
    set
    {
      if (this._hasNestedNodes == value)
        return;
      this._hasNestedNodes = value;
      if (this._hasNestedNodes)
      {
        if (this._emptyTreeNode != null)
          return;
        this.Nodes.Add((TreeNode) (this._emptyTreeNode = new TreeNodeExtendedBase.EmptyTreeNodeClass()));
      }
      else
      {
        if (this.Nodes.Count > 0)
          this.Nodes.Clear();
        if (this._emptyTreeNode == null)
          return;
        this._emptyTreeNode = (TreeNodeExtendedBase.EmptyTreeNodeClass) null;
      }
    }
  }

  public event TreeNodeExtendedEventHandler AfterExpand;

  internal virtual void OnAfterExpand([CanBeNull] object sender)
  {
    TreeNodeExtendedEventHandler afterExpand = this.AfterExpand;
    if (afterExpand == null)
      return;
    afterExpand(sender, new TreeNodeExtendedEventArgs(this));
  }

  public event TreeNodeExtendedCancelEventHandler BeforeCollapse;

  internal virtual void OnBeforeCollapse([CanBeNull] object sender, ref bool cancel)
  {
    TreeNodeExtendedCancelEventArgs e = new TreeNodeExtendedCancelEventArgs(this, cancel);
    TreeNodeExtendedCancelEventHandler beforeCollapse = this.BeforeCollapse;
    if (beforeCollapse != null)
      beforeCollapse(sender, e);
    cancel = e.Cancel;
  }

  public event TreeNodeExtendedEventHandler AfterCollapse;

  internal virtual void OnAfterCollapse([CanBeNull] object sender)
  {
    TreeNodeExtendedEventHandler afterCollapse = this.AfterCollapse;
    if (afterCollapse == null)
      return;
    afterCollapse(sender, new TreeNodeExtendedEventArgs(this));
  }

  public event TreeNodeExtendedCancelEventHandler BeforeCheck;

  internal virtual void OnBeforeCheck([CanBeNull] object sender, ref bool cancel)
  {
    TreeNodeExtendedCancelEventArgs e = new TreeNodeExtendedCancelEventArgs(this, cancel);
    TreeNodeExtendedCancelEventHandler beforeCheck = this.BeforeCheck;
    if (beforeCheck != null)
      beforeCheck(sender, e);
    cancel = e.Cancel;
  }

  public event TreeNodeExtendedEventHandler AfterCheck;

  internal virtual void OnAfterCheck([CanBeNull] object sender)
  {
    TreeNodeExtendedEventHandler afterCheck = this.AfterCheck;
    if (afterCheck == null)
      return;
    afterCheck(sender, new TreeNodeExtendedEventArgs(this));
  }

  protected class EmptyTreeNodeClass : TreeNodeExtended
  {
  }
}
