// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.UsersTreeView
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Client.Core.FormDesigner.Navigator;
using Intermech.Interfaces;
using Intermech.Interfaces.Workflow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class UsersTreeView : 
  TreeView,
  IMultipleAttributeEditor,
  IBaseDesForm,
  IAttributeEditorModified,
  IValidateBeforeSave
{
  [NonSerialized]
  private ParticipantList _parts = new ParticipantList();
  private bool _loaded;
  private Guid _srcVariable = Guid.Empty;
  private Guid _dstVariable = Guid.Empty;
  private bool _groupsOnly;
  private EnhToolTip _tip;
  private bool _multiselect = true;
  private List<TreeNode> _selectedNodes = new List<TreeNode>();
  private TreeNode _deselectedNode;
  private bool _modified;
  private bool _emulateControl;
  private bool _selectNodesPending;
  [NonSerialized]
  private ParticipantList _selectedParticipants;
  private DesForm _desForm;
  private bool _requiresValue = true;
  private bool _manualCheck;

  public UsersTreeView()
  {
    this.ImageList = Holder.UsersImageList;
    this.ItemHeight += 2;
    this.TreeViewNodeSorter = (IComparer) new UsersTreeViewItemComparer();
    this.FullRowSelect = true;
    this.HideSelection = false;
    this.Width = 250;
    this.Height = 250;
    this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
  }

  protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
  {
    base.OnBeforeExpand(e);
    if (!(e.Node is BaseNode))
      return;
    ((BaseNode) e.Node).DoExpand();
  }

  protected override void OnHandleCreated(EventArgs e)
  {
    base.OnHandleCreated(e);
    if (this._selectNodesPending)
    {
      this._selectNodesPending = false;
      this.SelectNodes(this._selectedParticipants);
    }
    this._loaded = true;
  }

  private void CheckIsVarsNotEqual(Guid value, Guid othervar)
  {
    if (!value.Equals(Guid.Empty) && value.Equals(othervar))
      throw new Exception(LocalizationHolder.rm.GetString("Workflow.Design_93"));
  }

  public Guid SrcVariable
  {
    get => this._srcVariable;
    set
    {
      this.CheckIsVarsNotEqual(value, this.DstVariable);
      this._srcVariable = value;
    }
  }

  public Guid DstVariable
  {
    get => this._dstVariable;
    set
    {
      this.CheckIsVarsNotEqual(value, this.SrcVariable);
      this._dstVariable = value;
    }
  }

  public bool GroupsOnly
  {
    get => this._groupsOnly;
    set => this._groupsOnly = value;
  }

  public void UpdateToolTip()
  {
    if (this._tip == null)
      this._tip = new EnhToolTip();
    this._tip.SetToolTip((Control) this, $"{LocalizationHolder.rm.GetString("Workflow.Design_94")}{this.SelectedParticipants.Count.ToString()}\r\n{this.SelectedParticipants.ToUserString(30)}");
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this._tip != null)
      this._tip.Dispose();
    base.Dispose(disposing);
  }

  public bool Multiselect
  {
    get => this._multiselect;
    set => this._multiselect = value;
  }

  protected override void OnLostFocus(EventArgs e)
  {
    base.OnLostFocus(e);
    this.RepaintSelectedNodes();
  }

  protected override void OnGotFocus(EventArgs e)
  {
    base.OnGotFocus(e);
    this.RepaintSelectedNodes();
  }

  private void RepaintSelectedNodes()
  {
    if (this.CheckBoxes)
      return;
    this.BeginUpdate();
    try
    {
      Color color1 = SystemColors.HighlightText;
      Color color2 = SystemColors.Highlight;
      if (!this.Focused)
      {
        color1 = this.ForeColor;
        color2 = SystemColors.Control;
      }
      foreach (TreeNode selectedNode in this._selectedNodes)
      {
        selectedNode.ForeColor = color1;
        selectedNode.BackColor = color2;
      }
    }
    finally
    {
      this.EndUpdate();
    }
  }

  private void PaintDeselected(TreeNode node)
  {
    if (this.CheckBoxes)
      return;
    if (node.IsSelected)
      node.TreeView.SelectedNode = (TreeNode) null;
    node.ForeColor = this.ForeColor;
    node.BackColor = this.BackColor;
  }

  private void PaintSelected(TreeNode node)
  {
    if (node == null)
      return;
    Color color1 = SystemColors.HighlightText;
    Color color2 = SystemColors.Highlight;
    if (!this.Focused)
    {
      color1 = this.ForeColor;
      color2 = SystemColors.Control;
    }
    node.ForeColor = color1;
    node.BackColor = color2;
  }

  protected override void OnMouseDown(MouseEventArgs e)
  {
    base.OnMouseDown(e);
    this._deselectedNode = (TreeNode) null;
    TreeNode nodeAt = this.GetNodeAt(e.X, e.Y);
    if (nodeAt == null || !nodeAt.IsSelected || (Control.ModifierKeys & Keys.Control) == Keys.None)
      return;
    this.OnBeforeSelect(new TreeViewCancelEventArgs(nodeAt, false, TreeViewAction.ByMouse));
    this._deselectedNode = nodeAt;
  }

  protected override void OnClick(EventArgs e) => base.OnClick(e);

  protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
  {
    Keys modifierKeys = Control.ModifierKeys;
    base.OnBeforeSelect(e);
    if (this.CheckBoxes)
      return;
    TreeNode node1 = e.Node;
    if (node1 == null)
      return;
    if (e.Action == TreeViewAction.Unknown)
      e.Cancel = true;
    else if (node1 == this._deselectedNode)
    {
      e.Cancel = true;
    }
    else
    {
      if (!this.Multiselect)
      {
        TreeNode node2 = (TreeNode) null;
        if (this._selectedNodes.Count > 0)
          node2 = this._selectedNodes[0];
        if (node1 != node2)
        {
          this.BeginUpdate();
          try
          {
            this._selectedNodes.Clear();
            if (node2 != null)
              this.PaintDeselected(node2);
            this._selectedNodes.Add(node1);
            this.RepaintSelectedNodes();
          }
          finally
          {
            this.EndUpdate();
          }
        }
      }
      else
      {
        if (this._selectedNodes.Count == 0 && this.SelectedNode != null)
          this._selectedNodes.Add(this.SelectedNode);
        this.BeginUpdate();
        try
        {
          if (this._emulateControl || (modifierKeys & Keys.Control) != Keys.None)
          {
            if (this._selectedNodes.IndexOf(node1) != -1)
            {
              e.Cancel = true;
              this._selectedNodes.Remove(node1);
              this.PaintDeselected(node1);
            }
            else
              this._selectedNodes.Add(node1);
          }
          else if ((modifierKeys & Keys.Shift) != Keys.None)
          {
            int num = int.MinValue;
            TreeNode treeNode1 = (TreeNode) null;
            foreach (TreeNode selectedNode in this._selectedNodes)
            {
              if (selectedNode.Bounds.Top > num)
              {
                num = selectedNode.Bounds.Top;
                treeNode1 = selectedNode;
              }
            }
            if (treeNode1 != null)
            {
              foreach (TreeNode selectedNode in this._selectedNodes)
                this.PaintDeselected(selectedNode);
              this._selectedNodes.Clear();
              if (treeNode1.Bounds.Top <= node1.Bounds.Top)
              {
                TreeNode treeNode2 = node1;
                node1 = treeNode1;
                treeNode1 = treeNode2;
              }
              for (; node1 != null && node1 != treeNode1; node1 = !node1.IsExpanded || node1.FirstNode == null || node1.FirstNode is EmptyNode ? (node1.NextNode == null ? (node1.Parent == null ? (TreeNode) null : node1.Parent.NextNode) : node1.NextNode) : node1.FirstNode)
              {
                if (this._selectedNodes.IndexOf(node1) == -1)
                  this._selectedNodes.Add(node1);
              }
              if (this._selectedNodes.IndexOf(treeNode1) == -1)
                this._selectedNodes.Add(treeNode1);
            }
          }
          else
          {
            foreach (TreeNode selectedNode in this._selectedNodes)
              this.PaintDeselected(selectedNode);
            this._selectedNodes.Clear();
            this._selectedNodes.Add(node1);
          }
          this.RepaintSelectedNodes();
        }
        finally
        {
          this.EndUpdate();
        }
      }
      ParticipantList selectedParticipants = this._selectedParticipants;
      this._selectedParticipants = (ParticipantList) null;
      if (!this.SelectedParticipants.Equals((object) selectedParticipants))
        this.Modified = true;
      this.UpdateToolTip();
    }
  }

  private bool NodeInList(TreeNode node, ParticipantList pl)
  {
    foreach (Participant participant in pl)
    {
      if (participant.Kind == ParticipantKind.User && node is UserNode && participant.ID == ((UserNode) node).ID || participant.Kind == ParticipantKind.Rank && node is RankNode && participant.ID == ((GroupNode) node).ID || participant.Kind == ParticipantKind.Group && node is GroupNode && participant.ID == ((GroupNode) node).ID)
      {
        pl.Remove(participant);
        return true;
      }
    }
    return false;
  }

  private void SelectNodes(ParticipantList pl)
  {
    this.BeginUpdate();
    try
    {
      int count = pl.Count;
      for (TreeNode node = this.Nodes[0]; node != null; node = node.NextNode)
      {
        this.SelectNodes(node, pl);
        if (pl.Count == 0)
          break;
      }
      this.Modified = false;
      this.CollapseDeselected();
      if (this._selectedNodes.Count <= 0)
        return;
      this._selectedNodes[0].EnsureVisible();
    }
    finally
    {
      this.EndUpdate();
    }
  }

  private void SelectNodes(TreeNode node, ParticipantList pl, bool childrenOnly = false)
  {
    node.Expand();
    List<TreeNode> treeNodeList = new List<TreeNode>();
    if (!childrenOnly)
      treeNodeList.Add(node);
    foreach (TreeNode node1 in node.Nodes)
      treeNodeList.Add(node1);
    foreach (TreeNode node2 in treeNodeList)
    {
      if (this.NodeInList(node2, pl))
      {
        try
        {
          if (this.CheckBoxes)
          {
            node2.Checked = true;
          }
          else
          {
            this._emulateControl = true;
            this.OnBeforeSelect(new TreeViewCancelEventArgs(node2, false, TreeViewAction.ByKeyboard));
          }
        }
        finally
        {
          this._emulateControl = false;
        }
        if (pl.Count == 0)
          break;
      }
      else
      {
        if (pl.Count == 0)
          break;
        if (node2.FirstNode != null)
          this.SelectNodes(node2, pl, true);
      }
    }
  }

  private void ExpandNodes(TreeNodeCollection nodes)
  {
    foreach (TreeNode node in nodes)
    {
      node.Expand();
      this.ExpandNodes(node.Nodes);
    }
  }

  private void AddParticipant(TreeNode n)
  {
    long ID = 0;
    ParticipantKind Kind = ParticipantKind.User;
    switch (n)
    {
      case RankNode _:
        Kind = ParticipantKind.Rank;
        ID = ((GroupNode) n).ID;
        break;
      case GroupNode _:
        Kind = ParticipantKind.Group;
        ID = ((GroupNode) n).ID;
        break;
      case UserNode _:
        Kind = ParticipantKind.User;
        ID = ((UserNode) n).ID;
        break;
    }
    if (ID == 0L)
      return;
    this._selectedParticipants.AddParticipant(Kind, ID);
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ParticipantList SelectedParticipants
  {
    get
    {
      if (this._selectedParticipants == null)
      {
        this.BeginUpdate();
        try
        {
          this._selectedParticipants = new ParticipantList();
          foreach (TreeNode selectedNode in this._selectedNodes)
            this.AddParticipant(selectedNode);
        }
        finally
        {
          this.EndUpdate();
        }
      }
      return this._selectedParticipants;
    }
    set
    {
      if (this._selectedParticipants == null)
        this._selectedParticipants = new ParticipantList();
      this._selectedParticipants.Assign(value);
      this.SelectedNode = (TreeNode) null;
      if (this.IsHandleCreated)
        this.SelectNodes(this._selectedParticipants);
      else
        this._selectNodesPending = true;
    }
  }

  public void CollapseDeselected()
  {
    foreach (TreeNode node in this.Nodes)
      this.CollapseDeselected(node);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="root"></param>
  /// <returns>True if has selected items</returns>
  private bool CollapseDeselected(TreeNode root)
  {
    bool flag = false;
    if (this._selectedNodes.Count == 0)
      flag = root.Level == 0 && root.Index == 0;
    foreach (TreeNode node in root.Nodes)
    {
      if (this.CollapseDeselected(node))
        flag = true;
      if (!flag && this._selectedNodes.IndexOf(node) != -1)
      {
        flag = true;
        break;
      }
    }
    if (!flag)
      root.Collapse();
    return flag;
  }

  private long ObjectID
  {
    get
    {
      long elementIdentifier = this._desForm.Info.ElementIdentifier;
      if (this._desForm.PinExchange.ContainsKey(elementIdentifier))
        elementIdentifier = this._desForm.PinExchange[elementIdentifier];
      return elementIdentifier;
    }
  }

  public void Load()
  {
    this.Nodes.Clear();
    this._selectedNodes.Clear();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      bool flag1 = false;
      try
      {
        if (this._parts.Session == null)
        {
          this._parts.SetSession(sessionKeeper.Session);
          flag1 = true;
        }
        if (!this.SrcVariable.Equals(Guid.Empty))
        {
          IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.SrcVariable);
          if (objectAttributeByGuid != null)
            this._parts.AsString = objectAttributeByGuid.Value.ToString();
        }
        if (this._parts.Count == 0)
        {
          this.Init(true, true);
        }
        else
        {
          MiscFunx.ReplaceVariablesByParticipants(sessionKeeper.Session, this.ObjectID, this._parts);
          foreach (Participant part in this._parts)
          {
            string displayName = part.DisplayName;
            if (!string.IsNullOrEmpty(displayName))
            {
              switch (part.Kind)
              {
                case ParticipantKind.User:
                  this.Nodes.Add((TreeNode) new UserNode(displayName, part.ID));
                  continue;
                case ParticipantKind.Group:
                  this.Nodes.Add((TreeNode) new GroupNode(displayName, part.ID));
                  continue;
                case ParticipantKind.Rank:
                  this.Nodes.Add((TreeNode) new RankNode(displayName, part.ID));
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        if (!this.DstVariable.Equals(Guid.Empty))
        {
          IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.DstVariable);
          if (objectAttributeByGuid != null)
          {
            this._parts.AsString = objectAttributeByGuid.Value.ToString();
            MiscFunx.ReplaceVariablesByParticipants(sessionKeeper.Session, this.ObjectID, this._parts);
            this.SelectedParticipants = this._parts;
          }
        }
        if (this.Nodes.Count > 0)
        {
          bool flag2 = false;
          foreach (TreeNode node in this.Nodes)
          {
            if (node.IsExpanded)
            {
              flag2 = true;
              break;
            }
          }
          if (!flag2)
            this.Nodes[0].Expand();
        }
        this.UpdateToolTip();
      }
      finally
      {
        if (flag1)
          this._parts.SetSession((IUserSession) null);
      }
    }
  }

  public void Save()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.Modified = false;
      if (this.DstVariable.Equals(Guid.Empty))
        return;
      IDBAttribute objectAttributeByGuid = sessionKeeper.Session.GetObjectAttributeByGuid(this.ObjectID, this.DstVariable);
      if (objectAttributeByGuid == null)
        return;
      ParticipantList selectedParticipants = this.SelectedParticipants;
      sessionKeeper.Session.SetObjectAttributesValues(this.ObjectID, true, new AttributeValues[1]
      {
        new AttributeValues(objectAttributeByGuid.AttributeID, (object) selectedParticipants.AsString)
      });
    }
  }

  public bool Modified
  {
    get => this._modified;
    set
    {
      if (!this._loaded || this._modified == value)
        return;
      this._modified = value;
      EventHandler modifiedEvent = this.ModifiedEvent;
      if (modifiedEvent == null)
        return;
      modifiedEvent((object) this, new EventArgs());
    }
  }

  public DesForm DesForm
  {
    set => this._desForm = value;
  }

  public event EventHandler ModifiedEvent;

  public bool RequiresValue
  {
    get => this._requiresValue;
    set => this._requiresValue = value;
  }

  public void Init(bool showUsers, bool showRanks)
  {
    this.BeginUpdate();
    try
    {
      if (this.Nodes.Count > 0)
        this.Nodes.Clear();
      if (showUsers)
      {
        TreeNode node = (TreeNode) new AllUsersNode();
        this.Nodes.Add(node);
        node.Expand();
      }
      if (!showRanks)
        return;
      TreeNode node1 = (TreeNode) new RanksNode();
      this.Nodes.Add(node1);
      if (showUsers)
        return;
      node1.Expand();
    }
    finally
    {
      this.EndUpdate();
    }
  }

  public void Validate()
  {
    if (!this.RequiresValue)
      return;
    Control control = (Control) this;
    while (control.Parent != null)
      control = control.Parent;
    switch (control)
    {
      case FormDlg _:
      case NewProcessForm _:
      case FormDesignerView _:
        if (this.SelectedParticipants.Count != 0)
          break;
        this.Focus();
        throw new NotificationException(LocalizationHolder.rm.GetString("Workflow.Design_95"));
    }
  }

  protected override void WndProc(ref Message m)
  {
    if (20 == m.Msg)
      m.Msg = 0;
    base.WndProc(ref m);
  }

  protected override void OnAfterCheck(TreeViewEventArgs e)
  {
    base.OnAfterCheck(e);
    if (this._manualCheck)
      return;
    if (!this.Multiselect)
    {
      this._manualCheck = true;
      try
      {
        foreach (TreeNode selectedNode in this._selectedNodes)
          selectedNode.Checked = false;
      }
      finally
      {
        this._manualCheck = false;
      }
      this._selectedNodes.Clear();
    }
    if (e.Node.Checked)
    {
      if (!this._selectedNodes.Contains(e.Node))
        this._selectedNodes.Add(e.Node);
    }
    else
      this._selectedNodes.Remove(e.Node);
    ParticipantList selectedParticipants = this._selectedParticipants;
    this._selectedParticipants = (ParticipantList) null;
    if (this.SelectedParticipants.Equals((object) selectedParticipants))
      return;
    this.Modified = true;
    this.UpdateToolTip();
  }
}
