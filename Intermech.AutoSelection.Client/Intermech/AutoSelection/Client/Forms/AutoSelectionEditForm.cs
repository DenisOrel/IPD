// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Forms.AutoSelectionEditForm
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using ImSSP;
using Intermech.AutoSelection.Client.AutoSelectionNode;
using Intermech.AutoSelection.Client.Common;
using Intermech.Bars;
using Intermech.Expert;
using Intermech.Expert.Editor;
using Intermech.Expert.Table;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Protection;
using SourceGrid3;
using SourceGrid3.Cells;
using SourceGrid3.Cells.Controllers;
using SourceGrid3.Cells.Views;
using SourceGrid3.Styles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AutoSelection.Client.Forms;

public class AutoSelectionEditForm : Form
{
  private AS_ImTableView _tableView;
  private Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule _rule;
  private bool _imGridMouseDown;
  private ICell _currentCell;
  private readonly List<TabPage> _tabPages;
  private bool _isModified;
  private bool _tvCopyMode;
  private TreeNode _dragTargetNode;
  private TreeNode _dragSourceNode;
  private DragEventArgs _dragEvent;
  private static readonly string TblColumnObjId = -2.ToString();
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel;
  private ContextMenuStrip cmsSelectionItems;
  private ToolStripMenuItem tsmiSelItemCreate;
  private ToolStripMenuItem tsmiSelItemCreateNew;
  private ToolStripMenuItem tsmiSelItemCreateInComposition;
  private ToolStripSeparator tsmiSelItemSep1;
  private ToolStripMenuItem tsmiSelItemRename;
  private ToolStripSeparator tsmiSelItemSep2;
  private ToolStripMenuItem tsmiSelItemDelete;
  private ToolStripMenuItem tsmiSelItemCopy;
  private ToolStripMenuItem tsmiSelItemPaste;
  private ToolStripMenuItem tsmiSelItemPasteToRoot;
  private ToolStripMenuItem tsmiSelItemPasteToCurrentItem;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem tsmiSelItemTable;
  private ToolStripMenuItem tsmiSelItemTableRemove;
  private ToolStripMenuItem tsmiSelItemCondition;
  private ToolStripMenuItem tsmiSelItemMove;
  private ToolStripMenuItem tsmiSelItemMoveFirst;
  private ToolStripMenuItem tsmiSelItemMoveLast;
  private ToolStripMenuItem tsmiSelItemMoveUp;
  private ToolStripMenuItem tsmiSelItemMoveDown;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem tsmiSelItemMoveLevelUp;
  private ToolStripMenuItem tsmiSelItemMoveLevelRoot;
  private Panel pnlButtons;
  private System.Windows.Forms.Button btnCancel;
  private System.Windows.Forms.Button btnOk;
  private ToolStripMenuItem tsmiSelItemCopyCurrent;
  private ToolStripMenuItem tsmiSelItemCopyAll;
  private ContextMenuStrip cmsConditions;
  private ToolStripMenuItem tsmiCondCopy;
  private ToolStripMenuItem tsmiCondPaste;
  private ToolStripSeparator tsmiCondSep1;
  private ToolStripMenuItem tsmiCondDelete;
  private ContextMenuStrip cmsInfoTable;
  private ContextMenuStrip cmsTblConds;
  private ToolStripMenuItem tsmiInfoTableCondAdd;
  private ToolStripMenuItem tsmiInfoTableCondEdit;
  private ToolStripMenuItem tsmiInfoTableCondDelete;
  private ToolStripSeparator tsmiInfoTableSep1;
  private ToolStripMenuItem tsmiInfoTableModifNone;
  private ToolStripMenuItem tsmiInfoTableModifMin;
  private ToolStripMenuItem tsmiInfoTableModifMax;
  private ToolStripMenuItem tsmiTblCondEdit;
  private ToolStripMenuItem tsmiTblCondDelete;
  private ToolStripSeparator tsmiTblCondSep1;
  private ToolStripMenuItem tsmiTblCondModifNone;
  private ToolStripMenuItem tsmiTblCondModifMin;
  private ToolStripMenuItem tsmiTblCondModifMax;
  private ImageList ilSelTree;
  private SplitContainer splCntrMain;
  private SplitContainer splCntrTop;
  private TreeView tvSelectionItems;
  private PropertyGrid pgSelectionItem;
  private TabControl tcSelection;
  private TabPage tpSelCond;
  private TextBox tbxSelCond;
  private TabPage tpTblConds;
  private Grid gridTblCond;
  private TabPage tpTable;
  private ToolStripMenuItem tsmiCondEdit;
  private ToolStripSeparator tsmiCondSep2;
  private ToolStripSeparator tsmiInfoTableSep2;
  private ToolStripMenuItem tsmiInfoTableDefRow;
  private ToolStripMenuItem tsmiInfoTableDefRowAdd;
  private ToolStripMenuItem tsmiInfoTableDefRowRemove;
  private ToolStripSeparator tsmiInfoTableDefRowSep1;
  private ToolStripMenuItem tsmiInfoTableDefRowClear;
  private ToolStripMenuItem tsmiInfoTableDefRowInvert;
  private ContextMenuStrip cmsDragDropTarget;
  private ToolStripMenuItem tsmiTargetBefore;
  private ToolStripMenuItem tsmiTargetAfter;
  private ToolStripSeparator tsmiTargetSep1;
  private ToolStripMenuItem tsmiTargetCurrentInside;

  private void GetInfo(
    DataGridViewElement dataCell,
    out AutoSelectionNodeItemImbase nodeItem,
    out string attributeName,
    out Guid attributeGuid)
  {
    nodeItem = (AutoSelectionNodeItemImbase) null;
    attributeName = string.Empty;
    attributeGuid = Guid.Empty;
    if (dataCell == null)
      return;
    nodeItem = this.GetSelectedItem() as AutoSelectionNodeItemImbase;
    int index = -1;
    if (dataCell is DataGridViewCell dataGridViewCell)
      index = dataGridViewCell.ColumnIndex;
    else if (dataCell is DataGridViewColumn dataGridViewColumn)
      index = dataGridViewColumn.Index;
    if (index == -1)
      return;
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(Convert.ToInt32(dataCell.DataGridView.Columns[index].Tag));
    if (attributeType == null)
      return;
    attributeName = attributeType.Name;
    attributeGuid = attributeType.AttributeGuid;
  }

  private AutoSelectionNodeCondition GetCondition(DataGridViewElement dataCell)
  {
    AutoSelectionNodeItemImbase nodeItem;
    Guid attributeGuid;
    this.GetInfo(dataCell, out nodeItem, out string _, out attributeGuid);
    return nodeItem?.TableInfo.CondList.GetCondition(attributeGuid);
  }

  private AutoSelectionNodeCondition GetCondition(
    DataGridViewElement dataCell,
    out string attributeName)
  {
    AutoSelectionNodeItemImbase nodeItem;
    Guid attributeGuid;
    this.GetInfo(dataCell, out nodeItem, out attributeName, out attributeGuid);
    return nodeItem?.TableInfo.CondList.GetCondition(attributeGuid);
  }

  private void InitData()
  {
    ImageList ilTree = (ImageList) null;
    AutosSelectConsts.Images.LoadImages(ref ilTree);
    this.tvSelectionItems.ImageList = ilTree;
    AS_ImTableView asImTableView = new AS_ImTableView();
    asImTableView.Parent = (Control) this.tpTable;
    asImTableView.Dock = DockStyle.Fill;
    this._tableView = asImTableView;
    this._tableView.Grid.ContextMenuStrip = this.cmsInfoTable;
    this._tableView.Grid.CellPainting += new DataGridViewCellPaintingEventHandler(this.Grid_CellPainting);
    this._tableView.Grid.EnableHeadersVisualStyles = false;
    this.ResumeLayout(false);
    this.tvSelectionItems.TreeViewNodeSorter = (IComparer) new AutoSelectionNodeTreeComparer();
    this.tsmiSelItemRename.Visible = false;
    foreach (TabPage tabPage in this.tcSelection.TabPages)
      this._tabPages.Add(tabPage);
    this.tcSelection.TabPages.Remove(this.tpTable);
    this.tcSelection.TabPages.Remove(this.tpTblConds);
  }

  protected virtual void FillSelectionTree()
  {
    this.tvSelectionItems.BeginUpdate();
    try
    {
      this.tvSelectionItems.Nodes.Clear();
      foreach (AutoSelectionNodeCommon childsNode in (List<AutoSelectionNodeCommon>) this._rule.ChildsNodes)
        this.AddSelectionNode((TreeNode) null, childsNode);
    }
    finally
    {
      this.tvSelectionItems.EndUpdate();
      this.tvSelectionItems.Sort();
      if (this.tvSelectionItems.Nodes.Count > 0)
      {
        this.tvSelectionItems.SelectedNode = this.tvSelectionItems.Nodes[0];
        this.tvSelectionItems.SelectedNode.ExpandAll();
      }
    }
  }

  protected virtual TreeNode AddSelectionNode(TreeNode ownerNode, AutoSelectionNodeCommon selNode)
  {
    if (selNode == null)
      return (TreeNode) null;
    TreeNode treeNode = ownerNode == null ? this.tvSelectionItems.Nodes.Add(string.Empty) : ownerNode.Nodes.Add(string.Empty);
    SelectionTreeViewUtils.UpdateSelectionNode(treeNode, selNode, true);
    return treeNode;
  }

  protected virtual void UpdateSelectionNode()
  {
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    TreeNode selectedNode = this.tvSelectionItems.SelectedNode;
    if (selectedItem == null || selectedNode == null)
      return;
    SelectionTreeViewUtils.UpdateSelectionNode(selectedNode, selectedItem, false);
  }

  private AutoSelectionNodeCommon ItemCreate(AutoSelectionNodeCommon owner)
  {
    AutoSelectionNodeType selNodeType = AutoSelectionTypeSelectionForm.SelectSelectionItemType(owner);
    if (selNodeType.Equals((object) AutoSelectionNodeType.None))
      return (AutoSelectionNodeCommon) null;
    System.Type nodeObjectType = AutoSelectionUtils.Common.GetNodeObjectType(selNodeType);
    if (nodeObjectType == (System.Type) null)
      return (AutoSelectionNodeCommon) null;
    AutoSelectionNodeBase selectionNodeBase = (AutoSelectionNodeBase) owner ?? (AutoSelectionNodeBase) this._rule;
    string caption = EnumTypeHelper.GetCaption((Enum) selNodeType);
    if (!(Activator.CreateInstance(nodeObjectType, (object) selectionNodeBase, (object) caption) is AutoSelectionNodeCommon instance))
      return (AutoSelectionNodeCommon) null;
    if (owner != null)
      owner.ChildsNodes.Add(instance);
    else
      this._rule.ChildsNodes.Add(instance);
    TreeNode treeNode = (TreeNode) null;
    this.tvSelectionItems.BeginUpdate();
    try
    {
      TreeNode selectedNode = owner != null ? this.tvSelectionItems.SelectedNode : (TreeNode) null;
      treeNode = this.AddSelectionNode(selectedNode, instance);
      selectedNode?.Expand();
      this._isModified = true;
    }
    finally
    {
      this.tvSelectionItems.EndUpdate();
      this.tvSelectionItems.Sort();
      this.tvSelectionItems.SelectedNode = treeNode;
    }
    return instance;
  }

  private void ItemRename()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.tvSelectionItems.SelectedNode == null || this.tvSelectionItems.LabelEdit)
      return;
    this.tvSelectionItems.LabelEdit = true;
    this.tvSelectionItems.SelectedNode.BeginEdit();
    this.UpdateSelectionNode();
    this._isModified = true;
  }

  private void ItemDelete()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem == null || MessageBox.Show(string.Format(LocalizationHolder.rm.GetString(sc_708.ssp_automatch_709()), (object) selectedItem), LocalizationHolder.rm.GetString(sc_708.ssp_automatch_710()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    selectedItem.OwnerNode?.ChildsNodes.Remove(selectedItem, true);
    if (this.tvSelectionItems.SelectedNode.Tag == selectedItem)
      this.tvSelectionItems.SelectedNode.Remove();
    this._isModified = true;
  }

  private void ItemCopyCurrent()
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this.tvSelectionItems.SelectedNode == null)
      return;
    IClipboard service2 = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
    if (service2 == null)
      return;
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    service2.SetDataObject((object) new DataObject(selectedItem.Clone()));
  }

  private void ItemCopyAll()
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (this._rule.ChildsNodes.Count == 0)
      return;
    IClipboard service2 = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
    if (service2 == null)
      return;
    AutoSelNodeList childsNodes = this._rule.ChildsNodes;
    service2.SetDataObject((object) new DataObject(childsNodes.Clone()));
  }

  private void ItemPaste(AutoSelectionNodeBase owner)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (owner == null || !(ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false)?.GetDataObject() is DataObject dataObject))
      return;
    bool flag = false;
    bool dataPresent = dataObject.GetDataPresent(typeof (AutoSelNodeList));
    string format1 = string.Empty;
    foreach (string format2 in dataObject.GetFormats())
    {
      if (dataObject.GetData(format2) is AutoSelectionNodeCommon)
      {
        format1 = format2;
        flag = true;
        break;
      }
    }
    TreeNode selectedNode = owner is Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule ? (TreeNode) null : this.tvSelectionItems.SelectedNode;
    if (flag)
    {
      if (!((dataObject.GetData(format1) is AutoSelectionNodeCommon data ? data.Clone() : (object) null) is AutoSelectionNodeCommon selNode))
        return;
      selNode.Order = -1;
      selNode.OwnerNode = owner;
      owner.ChildsNodes.Add(selNode);
      this.AddSelectionNode(selectedNode, selNode);
    }
    else if (dataPresent)
    {
      if (!(dataObject.GetData(typeof (AutoSelNodeList)) is AutoSelNodeList data))
        return;
      this.tvSelectionItems.BeginUpdate();
      try
      {
        foreach (AutoSelectionNodeCommon selectionNodeCommon in (List<AutoSelectionNodeCommon>) data)
        {
          if (selectionNodeCommon.Clone() is AutoSelectionNodeCommon selNode)
          {
            selNode.Order = -1;
            selNode.OwnerNode = owner;
            owner.ChildsNodes.Add(selNode);
            this.AddSelectionNode(selectedNode, selNode);
          }
        }
      }
      finally
      {
        this.tvSelectionItems.EndUpdate();
      }
    }
    this._isModified = true;
  }

  private void ItemMove(AutoSelNodeList itemList, int oldIdx, int newIdx)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index1 = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index1];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num1 = service.Query(true, appId, queryData, response);
    if (!num1.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index1 + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num1));
    if (itemList == null || itemList.Count == 0 || oldIdx == newIdx)
      return;
    int num2 = Math.Sign(newIdx - oldIdx);
    try
    {
      for (int index2 = oldIdx; index2 != newIdx; index2 += num2)
      {
        int order = itemList[index2].Order;
        itemList[index2].Order = itemList[index2 + num2].Order;
        itemList[index2 + num2].Order = order;
        AutoSelectionNodeCommon selectionNodeCommon = itemList[index2];
        itemList[index2] = itemList[index2 + num2];
        itemList[index2 + num2] = selectionNodeCommon;
      }
    }
    finally
    {
      itemList.Sort((IComparer<AutoSelectionNodeCommon>) new AutoSelectionNodeCommonComparer());
      this.tvSelectionItems.Sort();
      this._isModified = true;
    }
  }

  private void ItemMoveFirst()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem?.OwnerNode == null)
      return;
    TreeNode selectedNode = this.tvSelectionItems.SelectedNode;
    int oldIdx = selectedItem.OwnerNode.ChildsNodes.IndexOf(selectedItem);
    this.ItemMove(selectedItem.OwnerNode.ChildsNodes, oldIdx, 0);
    this.tvSelectionItems.SelectedNode = selectedNode;
  }

  private void ItemMoveUp()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem?.OwnerNode == null)
      return;
    TreeNode selectedNode = this.tvSelectionItems.SelectedNode;
    int oldIdx = selectedItem.OwnerNode.ChildsNodes.IndexOf(selectedItem);
    this.ItemMove(selectedItem.OwnerNode.ChildsNodes, oldIdx, oldIdx - 1);
    this.tvSelectionItems.SelectedNode = selectedNode;
  }

  private void ItemMoveDown()
  {
    IProtectionKey service = ServicesManager.GetService(typeof (IProtectionKey)) as IProtectionKey;
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem?.OwnerNode == null)
      return;
    TreeNode selectedNode = this.tvSelectionItems.SelectedNode;
    int oldIdx = selectedItem.OwnerNode.ChildsNodes.IndexOf(selectedItem);
    this.ItemMove(selectedItem.OwnerNode.ChildsNodes, oldIdx, oldIdx + 1);
    this.tvSelectionItems.SelectedNode = selectedNode;
  }

  private void ItemMoveLast()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem?.OwnerNode == null)
      return;
    TreeNode selectedNode = this.tvSelectionItems.SelectedNode;
    int oldIdx = selectedItem.OwnerNode.ChildsNodes.IndexOf(selectedItem);
    this.ItemMove(selectedItem.OwnerNode.ChildsNodes, oldIdx, selectedItem.OwnerNode.ChildsNodes.Count - 1);
    this.tvSelectionItems.SelectedNode = selectedNode;
  }

  private void ItemMoveLevelUp()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    AutoSelectionNodeBase ownerNode = selectedItem?.OwnerNode;
    if (ownerNode?.OwnerNode == null)
      return;
    TreeNode parent = this.tvSelectionItems.SelectedNode.Parent;
    if (parent == null)
      return;
    this.tvSelectionItems.BeginUpdate();
    try
    {
      ownerNode.ChildsNodes.Remove(selectedItem, true);
      selectedItem.Order = -1;
      selectedItem.OwnerNode = ownerNode.OwnerNode;
      ownerNode.OwnerNode.ChildsNodes.Add(selectedItem);
      this.tvSelectionItems.SelectedNode.Remove();
      this.AddSelectionNode(parent.Parent, selectedItem);
    }
    finally
    {
      this.tvSelectionItems.EndUpdate();
      this.tvSelectionItems.Sort();
      this._isModified = true;
    }
  }

  private void ItemMoveLevelRoot()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    AutoSelectionNodeBase ownerNode = selectedItem?.OwnerNode;
    if (ownerNode?.OwnerNode == null)
      return;
    this.tvSelectionItems.BeginUpdate();
    try
    {
      ownerNode.ChildsNodes.Remove(selectedItem, true);
      selectedItem.Order = -1;
      selectedItem.OwnerNode = (AutoSelectionNodeBase) this._rule;
      this._rule.ChildsNodes.Add(selectedItem);
      this.tvSelectionItems.SelectedNode.Remove();
      this.AddSelectionNode((TreeNode) null, selectedItem);
    }
    finally
    {
      this.tvSelectionItems.EndUpdate();
      this.tvSelectionItems.Sort();
      this._isModified = true;
    }
  }

  private void ItemCondition() => this.ConditionEdit();

  private void TableCreate()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if (!(this.GetSelectedItem() is AutoSelectionNodeFolder selectedItem))
      return;
    eTable[] tables = selectedItem.ExpTables;
    if (tables == null)
    {
      using (TableSetup form = new TableSetup())
      {
        form.AllowAnyObjectType = true;
        Control[] controlArray1 = form.Controls.Find("cbType", true);
        if (controlArray1.Length != 0)
          controlArray1[0].Enabled = false;
        Control[] controlArray2 = form.Controls.Find("tbName", true);
        if (controlArray2.Length != 0)
          controlArray2[0].Enabled = false;
        Control[] controlArray3 = form.Controls.Find("lbResult", true);
        ListBox listBox = controlArray3.Length != 0 ? controlArray3[0] as ListBox : (ListBox) null;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          CommonTypeHolder commonTypeHolder = new CommonTypeHolder(-1, MetaDataHelper.GetAttributeID((object) AutosSelectConsts.ImbaseObjectLinkAttrGuid.ToString()), sessionKeeper.Session);
          if (listBox != null)
          {
            listBox.Items.Add((object) commonTypeHolder);
            listBox.Enabled = false;
          }
          if (form.ShowTopDialog().Equals((object) DialogResult.OK))
            tables = form.Tables;
        }
      }
    }
    if (tables != null)
    {
      using (AutoSelectionTableEditForm form = new AutoSelectionTableEditForm(tables))
      {
        Control[] controlArray = form.Controls.Find("menuBar1", true);
        MenuItemBase menuItem = (controlArray.Length != 0 ? controlArray[0] as MenuBar : (MenuBar) null)?.FindMenuItem("menu.menu_Setup");
        if (menuItem != null)
          menuItem.Visible = false;
        if (form.ShowTopDialog().Equals((object) DialogResult.OK))
        {
          selectedItem.ExpTables = form.Tables;
          this._isModified = true;
        }
      }
    }
    this.UpdateSelectionNode();
  }

  private void TableRemove()
  {
    if (!(this.GetSelectedItem() is AutoSelectionNodeFolder selectedItem))
      return;
    selectedItem.ExpTables = (eTable[]) null;
    this.UpdateSelectionItemProp((AutoSelectionNodeCommon) selectedItem);
  }

  private AutoSelectionNodeCommon GetSelectedItem()
  {
    return this.tvSelectionItems.SelectedNode?.Tag as AutoSelectionNodeCommon;
  }

  private void UpdateSelectionItemPages()
  {
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    this.UpdateSelectionItemCond(selectedItem);
    if (selectedItem is AutoSelectionNodeItemImbase)
    {
      if (!this.tcSelection.TabPages.Contains(this.tpTable))
        this.tcSelection.TabPages.Add(this.tpTable);
      if (!this.tcSelection.TabPages.Contains(this.tpTblConds))
        this.tcSelection.TabPages.Add(this.tpTblConds);
      this.UpdateSelectionItemTable(selectedItem);
      this.UpdateSelectionItemTableCond(selectedItem);
    }
    else
    {
      if (this.tcSelection.TabPages.Contains(this.tpTable))
        this.tcSelection.TabPages.Remove(this.tpTable);
      if (!this.tcSelection.TabPages.Contains(this.tpTblConds))
        return;
      this.tcSelection.TabPages.Remove(this.tpTblConds);
    }
  }

  private void UpdateSelectionItemProp(AutoSelectionNodeCommon selNode)
  {
    this.pgSelectionItem.SelectedObject = (object) selNode;
    this.pgSelectionItem.Enabled = !this.ReadOnly && selNode != null;
  }

  private void UpdateSelectionItemCond(AutoSelectionNodeCommon selNode)
  {
    if (this.tcSelection.SelectedTab != this.tpSelCond)
      return;
    if (selNode != null)
    {
      if (selNode.Condition == null || selNode.Condition.Count.Equals(0))
        this.tbxSelCond.Text = string.Empty;
      else
        this.tbxSelCond.Text = selNode.Condition.ToString();
    }
    else
      this.tbxSelCond.Text = string.Empty;
  }

  private void UpdateSelectionItemTable(AutoSelectionNodeCommon selNode)
  {
    if (this.tcSelection.SelectedTab != this.tpTable)
      return;
    AutoSelectionNodeItemImbase selectionNodeItemImbase = selNode as AutoSelectionNodeItemImbase;
    this._tableView.ObjectId = -1L;
    this._tableView.Grid.MouseDown -= new MouseEventHandler(this.Grid_MouseDown);
    this._tableView.Grid.MouseUp -= new MouseEventHandler(this.Grid_MouseUp);
    if (selectionNodeItemImbase == null)
      return;
    QuickObjectInfo objectInfo;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      objectInfo = sessionKeeper.Session.GetObjectInfo(selectionNodeItemImbase.ImbaseObjectID.Value);
    if (objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableTypeID && objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseTableRefTypeID)
      return;
    try
    {
      this._tableView.ObjectId = selectionNodeItemImbase.ImbaseObjectID.Value;
    }
    catch (ArgumentException ex)
    {
    }
    this._tableView.Grid.SelectionMode = DataGridViewSelectionMode.CellSelect;
    this._tableView.Grid.MouseDown += new MouseEventHandler(this.Grid_MouseDown);
    this._tableView.Grid.MouseUp += new MouseEventHandler(this.Grid_MouseUp);
  }

  private void UpdateSelectionItemTableCond(AutoSelectionNodeCommon selNode)
  {
    if (this.tcSelection.SelectedTab != this.tpTblConds)
      return;
    this.gridTblCond.RowsCount = 0;
    this.gridTblCond.ColumnsCount = 0;
    if (!(selNode is AutoSelectionNodeItemImbase selectionNodeItemImbase) || selectionNodeItemImbase.TableInfo.CondList.Count <= 0)
      return;
    this.gridTblCond.RowsCount = selectionNodeItemImbase.TableInfo.CondList.Count + 1;
    this.gridTblCond.ColumnsCount = 3;
    this.gridTblCond[0, 0] = (ICell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("AutoSelection.Client_49"));
    this.gridTblCond[0, 0].View = (IView) new GradientFlatHeader();
    this.gridTblCond[0, 1] = (ICell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString("AutoSelection.Client_50"));
    this.gridTblCond[0, 1].View = (IView) new GradientFlatHeader();
    this.gridTblCond[0, 2] = (ICell) new SourceGrid3.Cells.Real.Header((object) LocalizationHolder.rm.GetString(sc_708.ssp_automatch_711()));
    this.gridTblCond[0, 2].View = (IView) new GradientFlatHeader();
    int row = 1;
    CustomEvents model = new CustomEvents();
    foreach (AutoSelectionNodeCondition cond in (List<AutoSelectionNodeCondition>) selectionNodeItemImbase.TableInfo.CondList)
    {
      string name = LocalizationHolder.rm.GetString("AutoSelection.Client_52");
      IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(cond.AttributeGUID);
      if (attributeType != null)
        name = attributeType.Name;
      this.gridTblCond[row, 0] = (ICell) new SourceGrid3.Cells.Real.Cell((object) name);
      this.gridTblCond[row, 0].Tag = (object) cond.AttributeGUID;
      this.gridTblCond[row, 0].Controller.AddController((IController) model);
      this.gridTblCond[row, 1] = (ICell) new SourceGrid3.Cells.Real.Cell(cond.Condition != null ? (object) cond.Condition.ToString() : (object) string.Empty);
      this.gridTblCond[row, 1].Controller.AddController((IController) model);
      this.gridTblCond[row, 2] = (ICell) new SourceGrid3.Cells.Real.Cell((object) EnumTypeHelper.GetCaption((Enum) cond.Addon));
      this.gridTblCond[row, 2].Controller.AddController((IController) model);
      ++row;
    }
    this.gridTblCond.AutoSize();
  }

  private void ConditionEdit()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem == null || this.ReadOnly)
      return;
    if (selectedItem.Condition == null)
      selectedItem.Condition = new TempFormula();
    TempFormula tF = selectedItem.Condition.Clone() as TempFormula;
    bool flag;
    using (FormEditor formEditor = new FormEditor())
    {
      formEditor.CanReturnEmpty = true;
      string title = string.Format(LocalizationHolder.rm.GetString(sc_708.ssp_automatch_712()), (object) selectedItem);
      flag = formEditor.Execute(ref tF, title, true);
    }
    if (!flag)
      return;
    selectedItem.Condition = tF.Clone() as TempFormula;
    this.UpdateSelectionItemCond(selectedItem);
    this._isModified = true;
  }

  private void ConditionCopy()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] queryData = AutoSelectionProtectionKey.Key[index];
    byte[] numArray = new byte[queryData.Length];
    if (service != null)
    {
      int num = service.Query(true, AutoSelectionProtectionKey.appId, queryData, numArray);
      if (!num.Equals(0) || !Convert.ToBase64String(numArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
        throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    }
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem?.Condition == null || selectedItem.Condition.Count == 0)
      return;
    ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false)?.SetDataObject((object) new DataObject(selectedItem.Condition.Clone()));
  }

  private void ConditionPaste()
  {
    IProtectionKey service1 = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service1.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem == null || this.ReadOnly)
      return;
    IClipboard service2 = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
    if (service2 == null || (!(service2.GetDataObject() is DataObject dataObject) ? 0 : (dataObject.GetDataPresent(typeof (TempFormula)) ? 1 : 0)) == 0)
      return;
    selectedItem.Condition = dataObject.GetData(typeof (TempFormula)) as TempFormula;
    this.UpdateSelectionItemCond(selectedItem);
    this._isModified = true;
  }

  private void ConditionDelete()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem == null || this.ReadOnly)
      return;
    selectedItem.Condition = new TempFormula();
    this.UpdateSelectionItemCond(selectedItem);
    this._isModified = true;
  }

  private void TableRowAdd()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if ((this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem ? selectedItem.TableInfo?.RowList : (AutoSelectionDefRowList) null) == null)
      return;
    DataGridViewSelectedCellCollection selectedCells = this._tableView.Grid.SelectedCells;
    if (selectedCells.Count == 0)
      return;
    foreach (DataGridViewCell dataGridViewCell in (BaseCollection) selectedCells)
    {
      if (dataGridViewCell != null)
      {
        long int64 = Convert.ToInt64(dataGridViewCell.OwningRow.Cells[AutoSelectionEditForm.TblColumnObjId].Value);
        if (int64 != 0L && selectedItem.TableInfo.RowList.GetRow(int64) == null)
          selectedItem.TableInfo.RowList.Add(new AutoSelectionDefRow(int64));
      }
    }
    this._tableView.Grid.Refresh();
    this._isModified = true;
  }

  private void TableRowRemove()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if ((this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem ? selectedItem.TableInfo?.RowList : (AutoSelectionDefRowList) null) == null)
      return;
    DataGridViewSelectedCellCollection selectedCells = this._tableView.Grid.SelectedCells;
    if (selectedCells.Count == 0)
      return;
    foreach (DataGridViewCell dataGridViewCell in (BaseCollection) selectedCells)
    {
      if (dataGridViewCell != null)
      {
        long int64 = Convert.ToInt64(dataGridViewCell.OwningRow.Cells[AutoSelectionEditForm.TblColumnObjId].Value);
        AutoSelectionDefRow row = selectedItem.TableInfo.RowList.GetRow(int64);
        if (row != null)
          selectedItem.TableInfo.RowList.Remove(row);
      }
    }
    this._tableView.Grid.Refresh();
    this._isModified = true;
  }

  private void TableRowClear()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if ((this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem ? selectedItem.TableInfo?.RowList : (AutoSelectionDefRowList) null) == null)
      return;
    selectedItem.TableInfo.RowList.Clear();
    this._tableView.Grid.Refresh();
    this._isModified = true;
  }

  private void TableRowInvert()
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = AutoSelectionProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = AutoSelectionProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(AutoSelectionProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_75"), (object) LocalizationHolder.rm.GetString("AutoSelection.Client_67"), (object) num));
    if ((this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem ? selectedItem.TableInfo?.RowList : (AutoSelectionDefRowList) null) == null)
      return;
    List<AutoSelectionDefRow> collection = new List<AutoSelectionDefRow>();
    foreach (DataRow row in (InternalDataCollectionBase) this._tableView.Table.Rows)
    {
      if (row != null)
      {
        long int64 = Convert.ToInt64(row[AutoSelectionEditForm.TblColumnObjId]);
        if (int64 != 0L && selectedItem.TableInfo.RowList.GetRow(int64) == null)
          collection.Add(new AutoSelectionDefRow(int64));
      }
    }
    selectedItem.TableInfo.RowList.Clear();
    selectedItem.TableInfo.RowList.AddRange((IEnumerable<AutoSelectionDefRow>) collection);
    this._tableView.Grid.Refresh();
    this._isModified = true;
  }

  public AutoSelectionEditForm()
  {
    this.InitializeComponent();
    this._rule = new Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule();
    this._tabPages = new List<TabPage>();
    this.InitData();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1455);
  }

  private void cmsSelectionItems_Opening(object sender, CancelEventArgs e)
  {
    TreeNode selectedNode = this.tvSelectionItems.SelectedNode;
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    this.tsmiSelItemCreate.Enabled = this.tsmiSelItemCreateNew.Enabled = !this.ReadOnly;
    ToolStripMenuItem createInComposition = this.tsmiSelItemCreateInComposition;
    ToolStripMenuItem tsmiSelItemDelete = this.tsmiSelItemDelete;
    ToolStripMenuItem tsmiSelItemMove = this.tsmiSelItemMove;
    bool flag1;
    this.tsmiSelItemCondition.Enabled = flag1 = !this.ReadOnly && selectedItem != null;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    tsmiSelItemMove.Enabled = num1 != 0;
    int num2;
    bool flag3 = (num2 = flag2 ? 1 : 0) != 0;
    tsmiSelItemDelete.Enabled = num2 != 0;
    int num3 = flag3 ? 1 : 0;
    createInComposition.Enabled = num3 != 0;
    this.tsmiSelItemCopyCurrent.Enabled = selectedItem != null;
    this.tsmiSelItemCopyAll.Enabled = this.tvSelectionItems.Nodes.Count != 0;
    int num4 = selectedItem != null ? selectedItem.OwnerNode.ChildsNodes.IndexOf(selectedItem) : -1;
    this.tsmiSelItemMoveFirst.Enabled = this.tsmiSelItemMoveUp.Enabled = !this.ReadOnly && selectedItem != null && num4 != 0;
    this.tsmiSelItemMoveDown.Enabled = this.tsmiSelItemMoveLast.Enabled = !this.ReadOnly && selectedItem != null && num4 != selectedItem.OwnerNode.ChildsNodes.Count - 1;
    this.tsmiSelItemPaste.Enabled = false;
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false);
    if (service != null && !this.ReadOnly)
    {
      DataObject dataObject = service.GetDataObject() as DataObject;
      bool flag4 = dataObject != null;
      if (flag4)
      {
        flag4 = false;
        foreach (string format in dataObject.GetFormats())
        {
          switch (dataObject.GetData(format))
          {
            case AutoSelectionNodeCommon _:
            case AutoSelNodeList _:
              flag4 = true;
              goto label_7;
            default:
              continue;
          }
        }
      }
label_7:
      this.tsmiSelItemPaste.Enabled = this.tsmiSelItemPasteToRoot.Enabled = flag4;
      this.tsmiSelItemPasteToCurrentItem.Enabled = flag4 && selectedItem != null;
    }
    this.tsmiSelItemMoveLevelRoot.Enabled = this.tsmiSelItemMoveLevelUp.Enabled = !this.ReadOnly && selectedItem != null && selectedNode.Level != 0;
    bool flag5 = selectedItem is AutoSelectionNodeFolder selectionNodeFolder && selectionNodeFolder.FolderType.Equals((object) AutoSelectionFolderType.SelectFolder);
    this.tsmiSelItemTable.Enabled = flag5;
    this.tsmiSelItemTableRemove.Enabled = flag5 && (selectedItem as AutoSelectionNodeFolder).ExpTables != null;
  }

  private void tsmiSelItemCreateNew_Click(object sender, EventArgs e)
  {
    this.ItemCreate((AutoSelectionNodeCommon) null);
  }

  private void tsmiSelItemCreateInComposition_Click(object sender, EventArgs e)
  {
    this.ItemCreate(this.GetSelectedItem());
  }

  private void tvSelectionItems_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.UpdateSelectionItemProp(this.GetSelectedItem());
    this.UpdateSelectionItemPages();
  }

  private void tsmiSelItemMoveFirst_Click(object sender, EventArgs e) => this.ItemMoveFirst();

  private void tsmiSelItemMoveUp_Click(object sender, EventArgs e) => this.ItemMoveUp();

  private void tsmiSelItemMoveDown_Click(object sender, EventArgs e) => this.ItemMoveDown();

  private void tsmiSelItemMoveLast_Click(object sender, EventArgs e) => this.ItemMoveLast();

  private void tsmiSelItemMoveLevelUp_Click(object sender, EventArgs e) => this.ItemMoveLevelUp();

  private void tsmiSelItemMoveLevelRoot_Click(object sender, EventArgs e)
  {
    this.ItemMoveLevelRoot();
  }

  private void tsmiSelItemCopyCurrent_Click(object sender, EventArgs e) => this.ItemCopyCurrent();

  private void tsmiSelItemCopyAll_Click(object sender, EventArgs e) => this.ItemCopyAll();

  private void tsmiSelItemPasteToRoot_Click(object sender, EventArgs e)
  {
    this.ItemPaste((AutoSelectionNodeBase) this._rule);
  }

  private void tsmiSelItemPasteToCurrentItem_Click(object sender, EventArgs e)
  {
    this.ItemPaste((AutoSelectionNodeBase) this.GetSelectedItem());
  }

  private void tsmiSelItemTable_Click(object sender, EventArgs e) => this.TableCreate();

  private void tsmiSelItemTableRemove_Click(object sender, EventArgs e) => this.TableRemove();

  private void tsmiSelItemCondition_Click(object sender, EventArgs e) => this.ItemCondition();

  private void tcSelection_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateSelectionItemPages();
  }

  private void pgSelectionItem_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
  {
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    if (selectedItem != null && selectedItem.Name == e.ChangedItem.Value.ToString())
      this.UpdateSelectionNode();
    this._isModified = true;
  }

  private void tsmiSelItemRename_Click(object sender, EventArgs e) => this.ItemRename();

  private void tsmiSelItemDelete_Click(object sender, EventArgs e) => this.ItemDelete();

  private void cmsConditions_Opening(object sender, CancelEventArgs e)
  {
    AutoSelectionNodeCommon selectedItem = this.GetSelectedItem();
    this.tsmiCondEdit.Enabled = selectedItem != null & !this.ReadOnly;
    this.tsmiCondCopy.Enabled = selectedItem != null && selectedItem.Condition != null && selectedItem.Condition.Count != 0;
    this.tsmiCondDelete.Enabled = !this.ReadOnly && this.tsmiCondCopy.Enabled;
    bool flag = false;
    if (selectedItem != null && !this.ReadOnly)
      flag = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, false)?.GetDataObject() is DataObject dataObject && dataObject.GetDataPresent(typeof (TempFormula));
    this.tsmiCondPaste.Enabled = flag;
  }

  private void tsmiCondEdit_Click(object sender, EventArgs e) => this.ItemCondition();

  private void tsmiCondCopy_Click(object sender, EventArgs e) => this.ConditionCopy();

  private void tsmiCondPaste_Click(object sender, EventArgs e) => this.ConditionPaste();

  private void tsmiCondDelete_Click(object sender, EventArgs e) => this.ConditionDelete();

  private void tbxSelCond_DoubleClick(object sender, EventArgs e) => this.ConditionEdit();

  private void cmsInfoTable_Opening(object sender, CancelEventArgs e)
  {
    AutoSelectionNodeCondition condition = this.GetCondition((DataGridViewElement) this._tableView.Grid.CurrentCell);
    this.tsmiInfoTableCondAdd.Enabled = condition == null && !this.ReadOnly && this._tableView.ObjectId != -1L;
    this.tsmiInfoTableCondEdit.Enabled = this.tsmiInfoTableCondDelete.Enabled = condition != null && !this.ReadOnly;
    ToolStripMenuItem infoTableModifNone = this.tsmiInfoTableModifNone;
    ToolStripMenuItem infoTableModifMin = this.tsmiInfoTableModifMin;
    bool flag1;
    this.tsmiInfoTableModifMax.Enabled = flag1 = condition != null && !this.ReadOnly;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    infoTableModifMin.Enabled = num1 != 0;
    int num2 = flag2 ? 1 : 0;
    infoTableModifNone.Enabled = num2 != 0;
    this.tsmiInfoTableModifNone.Checked = true;
    this.tsmiInfoTableModifMin.Checked = this.tsmiInfoTableModifMax.Checked = false;
    DataGridViewSelectedCellCollection selectedCells = this._tableView.Grid.SelectedCells;
    this.tsmiInfoTableDefRow.Enabled = !this.ReadOnly && selectedCells.Count != 0;
    if (condition == null)
      return;
    switch (condition.Addon)
    {
      case AutoSelectionNodeCondRule.Min:
        this.tsmiInfoTableModifNone.Checked = false;
        this.tsmiInfoTableModifMin.Checked = true;
        break;
      case AutoSelectionNodeCondRule.Max:
        this.tsmiInfoTableModifNone.Checked = false;
        this.tsmiInfoTableModifMax.Checked = true;
        break;
    }
  }

  private void tsmiInfoTableCond_Click(object sender, EventArgs e)
  {
    AutoSelectionNodeItemImbase nodeItem;
    string attributeName;
    Guid attributeGuid;
    this.GetInfo((DataGridViewElement) this._tableView.Grid.CurrentCell, out nodeItem, out attributeName, out attributeGuid);
    bool flag = false;
    if (sender.Equals((object) this.tsmiInfoTableCondAdd))
    {
      using (FormEditor formEditor = new FormEditor())
      {
        TempFormula tF = new TempFormula();
        if (formEditor.Execute(ref tF, string.Format(LocalizationHolder.rm.GetString(sc_708.ssp_automatch_713()), (object) attributeName), true))
        {
          nodeItem.TableInfo.CondList.Add(new AutoSelectionNodeCondition(attributeGuid, tF, AutoSelectionNodeCondRule.None));
          flag = true;
        }
      }
    }
    else if (sender.Equals((object) this.tsmiInfoTableCondEdit))
    {
      AutoSelectionNodeCondition condition = nodeItem.TableInfo.CondList.GetCondition(attributeGuid);
      using (FormEditor formEditor = new FormEditor())
      {
        TempFormula tF = condition.Condition.Clone() as TempFormula;
        if (formEditor.Execute(ref tF, string.Format(LocalizationHolder.rm.GetString(sc_708.ssp_automatch_714()), (object) attributeName), true))
        {
          condition.Condition = tF;
          flag = true;
        }
      }
    }
    else if (sender.Equals((object) this.tsmiInfoTableCondDelete))
    {
      AutoSelectionNodeCondition condition = nodeItem.TableInfo.CondList.GetCondition(attributeGuid);
      nodeItem.TableInfo.CondList.Remove(condition);
      flag = true;
    }
    if (!flag)
      return;
    this._tableView.Grid.Invalidate();
    this._isModified = true;
  }

  private void tsmiInfoTableModify_Click(object sender, EventArgs e)
  {
    AutoSelectionNodeCondition condition = this.GetCondition((DataGridViewElement) this._tableView.Grid.CurrentCell);
    if (sender.Equals((object) this.tsmiInfoTableModifNone))
      condition.Addon = AutoSelectionNodeCondRule.None;
    else if (sender.Equals((object) this.tsmiInfoTableModifMin))
      condition.Addon = AutoSelectionNodeCondRule.Min;
    else if (sender.Equals((object) this.tsmiInfoTableModifMax))
      condition.Addon = AutoSelectionNodeCondRule.Max;
    this._isModified = true;
  }

  private void tsmiInfoTableDefRow_DropDownOpening(object sender, EventArgs e)
  {
    DataGridViewSelectedCellCollection selectedCells = this._tableView.Grid.SelectedCells;
    ToolStripMenuItem infoTableDefRowAdd = this.tsmiInfoTableDefRowAdd;
    ToolStripMenuItem tableDefRowRemove = this.tsmiInfoTableDefRowRemove;
    bool flag1;
    this.tsmiInfoTableDefRowInvert.Enabled = flag1 = !this.ReadOnly && selectedCells.Count != 0;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    tableDefRowRemove.Enabled = num1 != 0;
    int num2 = flag2 ? 1 : 0;
    infoTableDefRowAdd.Enabled = num2 != 0;
    this.tsmiInfoTableDefRowClear.Enabled = this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem && !this.ReadOnly && selectedItem.TableInfo.RowList.Count != 0;
  }

  private void tsmiInfoTableDefRowAdd_Click(object sender, EventArgs e) => this.TableRowAdd();

  private void tsmiInfoTableDefRowRemove_Click(object sender, EventArgs e) => this.TableRowRemove();

  private void tsmiInfoTableDefRowClear_Click(object sender, EventArgs e) => this.TableRowClear();

  private void tsmiInfoTableDefRowInvert_Click(object sender, EventArgs e) => this.TableRowInvert();

  private void tsmiTblCond_Click(object sender, EventArgs e)
  {
    if (!(this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem) || this._currentCell == null)
      return;
    Guid tag = (Guid) this._currentCell.Tag;
    AutoSelectionNodeCondition condition = selectedItem.TableInfo.CondList.GetCondition(tag);
    if (sender.Equals((object) this.tsmiTblCondEdit))
    {
      using (FormEditor formEditor = new FormEditor())
      {
        string name = LocalizationHolder.rm.GetString(sc_708.ssp_automatch_715());
        IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(condition.AttributeGUID);
        if (attributeType != null)
          name = attributeType.Name;
        TempFormula tF = condition.Condition.Clone() as TempFormula;
        if (!formEditor.Execute(ref tF, string.Format(LocalizationHolder.rm.GetString(sc_708.ssp_automatch_716()), (object) name), true))
          return;
        condition.Condition = tF;
        this.gridTblCond[this._currentCell.Row, 1].Value = (object) condition.Condition.ToString();
        this.gridTblCond.AutoSize();
        this._isModified = true;
      }
    }
    else
    {
      if (!sender.Equals((object) this.tsmiTblCondDelete))
        return;
      selectedItem.TableInfo.CondList.Remove(condition);
      this.gridTblCond.Rows.Remove(this._currentCell.Row);
      this._currentCell = (ICell) null;
      this._isModified = true;
    }
  }

  private void tsmiTblCondModify_Click(object sender, EventArgs e)
  {
    if (!(this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem) || this._currentCell == null)
      return;
    Guid tag = (Guid) this._currentCell.Tag;
    AutoSelectionNodeCondition condition = selectedItem.TableInfo.CondList.GetCondition(tag);
    if (sender.Equals((object) this.tsmiTblCondModifNone))
      condition.Addon = AutoSelectionNodeCondRule.None;
    else if (sender.Equals((object) this.tsmiTblCondModifMin))
      condition.Addon = AutoSelectionNodeCondRule.Min;
    else if (sender.Equals((object) this.tsmiTblCondModifMax))
      condition.Addon = AutoSelectionNodeCondRule.Max;
    this.gridTblCond[this._currentCell.Row, 2].Value = (object) EnumTypeHelper.GetCaption((Enum) condition.Addon);
    this._isModified = true;
  }

  private void cmsTblConds_Opening(object sender, CancelEventArgs e)
  {
    this.tsmiTblCondEdit.Enabled = this.tsmiTblCondDelete.Enabled = this.tsmiTblCondModifNone.Enabled = this.tsmiTblCondModifMin.Enabled = this.tsmiTblCondModifMax.Enabled = this.tsmiTblCondModifNone.Checked = this.tsmiTblCondModifMin.Checked = false;
    if (!(this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem))
      return;
    ICell mouseCell = this.gridTblCond.MouseCell;
    if (mouseCell == null)
      return;
    this._currentCell = this.gridTblCond[mouseCell.Row, 0];
    Guid tag = (Guid) this._currentCell.Tag;
    AutoSelectionNodeCondition condition = selectedItem.TableInfo.CondList.GetCondition(tag);
    this.tsmiTblCondEdit.Enabled = this.tsmiTblCondDelete.Enabled = condition != null;
    this.tsmiTblCondModifNone.Enabled = this.tsmiTblCondModifMin.Enabled = this.tsmiTblCondModifMax.Enabled = condition != null;
    this.tsmiTblCondModifNone.Checked = true;
    this.tsmiTblCondModifMin.Checked = this.tsmiTblCondModifMax.Checked = false;
    if (condition == null)
      return;
    switch (condition.Addon)
    {
      case AutoSelectionNodeCondRule.Min:
        this.tsmiTblCondModifNone.Checked = false;
        this.tsmiTblCondModifMin.Checked = true;
        break;
      case AutoSelectionNodeCondRule.Max:
        this.tsmiTblCondModifNone.Checked = false;
        this.tsmiTblCondModifMax.Checked = true;
        break;
    }
  }

  private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
  {
    if (sender == null || e == null || !(sender is DataGridView dataGridView))
      return;
    if (e.RowIndex == -1)
    {
      if (e.ColumnIndex == -1 || this.GetCondition((DataGridViewElement) dataGridView.Columns[e.ColumnIndex]) == null)
        return;
      e.CellStyle.BackColor = SystemColors.ActiveCaption;
    }
    else
    {
      if (e.ColumnIndex == -1 || (this.GetSelectedItem() is AutoSelectionNodeItemImbase selectedItem ? selectedItem.TableInfo?.RowList : (AutoSelectionDefRowList) null) == null)
        return;
      DataRowView dataRowView = this._tableView.DataView[e.RowIndex];
      if (dataRowView == null)
        return;
      long int64 = Convert.ToInt64(dataRowView[AutoSelectionEditForm.TblColumnObjId]);
      if (selectedItem.TableInfo.RowList.GetRow(int64) == null)
        return;
      e.CellStyle.BackColor = SystemColors.GradientInactiveCaption;
    }
  }

  private void Grid_MouseUp(object sender, MouseEventArgs e)
  {
    if (!e.Button.Equals((object) MouseButtons.Right))
      return;
    DataGridViewCell currentCell = this._tableView.Grid.CurrentCell;
    AutoSelectionNodeItemImbase selectedItem = this.GetSelectedItem() as AutoSelectionNodeItemImbase;
    if (currentCell == null || selectedItem == null || !this._imGridMouseDown)
      return;
    this._imGridMouseDown = false;
  }

  private void Grid_MouseDown(object sender, MouseEventArgs e)
  {
    this._imGridMouseDown = sender.Equals((object) this._tableView.Grid) && e.Button.Equals((object) MouseButtons.Right);
    if (!this._imGridMouseDown)
      return;
    DataGridView.HitTestInfo hitTestInfo = this._tableView.Grid.HitTest(e.X, e.Y);
    if (hitTestInfo == null)
      return;
    if (hitTestInfo.Type == DataGridViewHitTestType.Cell)
      this._tableView.Grid.CurrentCell = this._tableView.Grid.Rows[hitTestInfo.RowIndex].Cells[hitTestInfo.ColumnIndex];
  }

  private void pgSelectionItem_PropertyValueChanged_1(object s, PropertyValueChangedEventArgs e)
  {
    this.UpdateSelectionNode();
    this.UpdateSelectionItemPages();
    this._isModified = true;
  }

  private void AutoSelectionEditForm_Load(object sender, EventArgs e)
  {
    AutoSelectionUtils.Forms.LoadSettings((Form) this);
  }

  private void AutoSelectionEditForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    AutoSelectionUtils.Forms.SaveSettings((Form) this);
  }

  private void AutoSelectionEditForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    this._tableView.Detach();
    if (!this._isModified || MessageBox.Show(LocalizationHolder.rm.GetString(sc_708.ssp_automatch_717()), LocalizationHolder.rm.GetString(sc_708.ssp_automatch_718()), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.DialogResult = DialogResult.OK;
  }

  private void tvSelectionItems_ItemDrag(object sender, ItemDragEventArgs e)
  {
    if (this.ReadOnly || sender == null || e == null || e.Button != MouseButtons.Left)
      return;
    int num = (int) this.DoDragDrop(e.Item, this._tvCopyMode ? DragDropEffects.Copy : DragDropEffects.Move);
  }

  private void tvSelectionItems_DragOver(object sender, DragEventArgs e)
  {
    if (this.ReadOnly || sender == null || e == null)
      return;
    this.tvSelectionItems.SelectedNode = this.tvSelectionItems.GetNodeAt(this.tvSelectionItems.PointToClient(new Point(e.X, e.Y)));
  }

  private void tvSelectionItems_DragLeave(object sender, EventArgs e)
  {
  }

  private void tvSelectionItems_DragDrop(object sender, DragEventArgs e)
  {
    if (this.ReadOnly)
      return;
    Point client = this.tvSelectionItems.PointToClient(new Point(e.X, e.Y));
    this._dragEvent = e;
    this._dragTargetNode = this.tvSelectionItems.GetNodeAt(client);
    this._dragSourceNode = (TreeNode) e.Data.GetData(typeof (TreeNode));
    if (this._dragSourceNode == null || this._dragSourceNode.Tag == null || this._dragTargetNode.Equals((object) this._dragSourceNode))
      return;
    this.cmsDragDropTarget.Show(e.X, e.Y);
  }

  private void tvSelectionItems_DragEnter(object sender, DragEventArgs e)
  {
    e.Effect = e.AllowedEffect;
  }

  private bool ContainsNode(TreeNode childNode, TreeNode ownerNode)
  {
    if (childNode.Parent == null)
      return false;
    return childNode.Parent.Equals((object) ownerNode) || this.ContainsNode(childNode.Parent, ownerNode);
  }

  private void cmsDragDropTarget_Opening(object sender, CancelEventArgs e)
  {
    if (this.ReadOnly || this._dragSourceNode == null || this._dragTargetNode == null || this._dragTargetNode.Equals((object) this._dragSourceNode))
    {
      this.tsmiTargetBefore.Enabled = this.tsmiTargetAfter.Enabled = this.tsmiTargetCurrentInside.Enabled = false;
    }
    else
    {
      this.tsmiTargetBefore.Enabled = this.tsmiTargetAfter.Enabled = !this.ContainsNode(this._dragTargetNode, this._dragSourceNode);
      this.tsmiTargetCurrentInside.Enabled = !this.ContainsNode(this._dragTargetNode, this._dragSourceNode);
    }
  }

  private void DragDropExecute(AutoSelectionEditForm.DragAndDropMode mode)
  {
    try
    {
      if (this.ReadOnly || this._dragSourceNode == null || this._dragTargetNode == null || this._dragTargetNode.Equals((object) this._dragSourceNode))
        return;
      switch (mode)
      {
        case AutoSelectionEditForm.DragAndDropMode.InsertBefore:
        case AutoSelectionEditForm.DragAndDropMode.InsertAfter:
          if (this.ContainsNode(this._dragTargetNode, this._dragSourceNode))
            return;
          break;
        case AutoSelectionEditForm.DragAndDropMode.InsertInto:
          if (this.ContainsNode(this._dragTargetNode, this._dragSourceNode))
            return;
          break;
      }
      if (!(this._dragSourceNode.Tag is AutoSelectionNodeCommon selNode))
        return;
      switch (this._dragEvent.Effect)
      {
        case DragDropEffects.Copy:
          selNode = (AutoSelectionNodeCommon) selNode.Clone();
          break;
        case DragDropEffects.Move:
          if (this._dragSourceNode.Parent != null)
          {
            if (this._dragSourceNode.Parent.Tag is AutoSelectionNodeCommon tag)
              tag.ChildsNodes.Remove(selNode, true);
          }
          else
            this._rule.ChildsNodes.Remove(selNode, true);
          this._dragSourceNode.Remove();
          break;
      }
      TreeNode ownerNode = this._dragTargetNode;
      int index1 = -1;
      switch (mode)
      {
        case AutoSelectionEditForm.DragAndDropMode.InsertBefore:
        case AutoSelectionEditForm.DragAndDropMode.InsertAfter:
          ownerNode = ownerNode?.Parent;
          TreeNodeCollection treeNodeCollection = ownerNode != null ? ownerNode.Nodes : this._dragTargetNode.TreeView.Nodes;
          int num = treeNodeCollection.IndexOf(this._dragTargetNode);
          index1 = mode == AutoSelectionEditForm.DragAndDropMode.InsertBefore ? num : num + 1;
          if (index1 > treeNodeCollection.Count - 1)
          {
            index1 = -1;
            break;
          }
          break;
      }
      AutoSelectionNodeBase selectionNodeBase = (AutoSelectionNodeBase) (ownerNode?.Tag as AutoSelectionNodeCommon) ?? (AutoSelectionNodeBase) this._rule;
      selNode.Order = index1;
      selNode.OwnerNode = selectionNodeBase;
      if (index1 == -1)
      {
        selectionNodeBase.ChildsNodes.Add(selNode);
      }
      else
      {
        selectionNodeBase.ChildsNodes.Insert(index1, selNode);
        for (int index2 = index1; index2 < selectionNodeBase.ChildsNodes.Count; ++index2)
          selectionNodeBase.ChildsNodes[index2].Order = index2;
      }
      this.AddSelectionNode(ownerNode, selNode);
      ownerNode?.Expand();
      this.tvSelectionItems.Sort();
      this.tvSelectionItems.SelectedNode = ownerNode;
      this._isModified = true;
    }
    finally
    {
      this._dragEvent = (DragEventArgs) null;
      this._dragSourceNode = (TreeNode) null;
      this._dragTargetNode = (TreeNode) null;
    }
  }

  private void tsmiTargetBefore_Click(object sender, EventArgs e)
  {
    this.DragDropExecute(AutoSelectionEditForm.DragAndDropMode.InsertBefore);
  }

  private void tsmiTargetAfter_Click(object sender, EventArgs e)
  {
    this.DragDropExecute(AutoSelectionEditForm.DragAndDropMode.InsertAfter);
  }

  private void tsmiTargetCurrentInside_Click(object sender, EventArgs e)
  {
    this.DragDropExecute(AutoSelectionEditForm.DragAndDropMode.InsertInto);
  }

  private void tvSelectionItems_KeyDown(object sender, KeyEventArgs e)
  {
    this._tvCopyMode = (e.KeyData & Keys.Control) == Keys.Control;
  }

  private void tvSelectionItems_KeyUp(object sender, KeyEventArgs e) => this._tvCopyMode = false;

  public Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule Rule
  {
    get => this._rule;
    set
    {
      this._rule = value != null ? value.Clone() as Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule : new Intermech.AutoSelection.Client.AutoSelectionRule.AutoSelectionRule();
      this.Text = string.Format(LocalizationHolder.rm.GetString("AutoSelection.Client_17"), (object) this._rule.Name);
      this.FillSelectionTree();
      this._isModified = false;
    }
  }

  public bool ReadOnly { get; set; } = true;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AutoSelectionEditForm));
    this.tableLayoutPanel = new TableLayoutPanel();
    this.pnlButtons = new Panel();
    this.btnCancel = new System.Windows.Forms.Button();
    this.btnOk = new System.Windows.Forms.Button();
    this.splCntrMain = new SplitContainer();
    this.splCntrTop = new SplitContainer();
    this.tvSelectionItems = new TreeView();
    this.cmsSelectionItems = new ContextMenuStrip(this.components);
    this.tsmiSelItemCreate = new ToolStripMenuItem();
    this.tsmiSelItemCreateNew = new ToolStripMenuItem();
    this.tsmiSelItemCreateInComposition = new ToolStripMenuItem();
    this.tsmiSelItemRename = new ToolStripMenuItem();
    this.tsmiSelItemDelete = new ToolStripMenuItem();
    this.tsmiSelItemSep1 = new ToolStripSeparator();
    this.tsmiSelItemMove = new ToolStripMenuItem();
    this.tsmiSelItemMoveFirst = new ToolStripMenuItem();
    this.tsmiSelItemMoveUp = new ToolStripMenuItem();
    this.tsmiSelItemMoveDown = new ToolStripMenuItem();
    this.tsmiSelItemMoveLast = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.tsmiSelItemMoveLevelUp = new ToolStripMenuItem();
    this.tsmiSelItemMoveLevelRoot = new ToolStripMenuItem();
    this.tsmiSelItemSep2 = new ToolStripSeparator();
    this.tsmiSelItemCopy = new ToolStripMenuItem();
    this.tsmiSelItemCopyCurrent = new ToolStripMenuItem();
    this.tsmiSelItemCopyAll = new ToolStripMenuItem();
    this.tsmiSelItemPaste = new ToolStripMenuItem();
    this.tsmiSelItemPasteToRoot = new ToolStripMenuItem();
    this.tsmiSelItemPasteToCurrentItem = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.tsmiSelItemTable = new ToolStripMenuItem();
    this.tsmiSelItemTableRemove = new ToolStripMenuItem();
    this.tsmiSelItemCondition = new ToolStripMenuItem();
    this.ilSelTree = new ImageList(this.components);
    this.pgSelectionItem = new PropertyGrid();
    this.tcSelection = new TabControl();
    this.tpSelCond = new TabPage();
    this.tbxSelCond = new TextBox();
    this.cmsConditions = new ContextMenuStrip(this.components);
    this.tsmiCondEdit = new ToolStripMenuItem();
    this.tsmiCondSep2 = new ToolStripSeparator();
    this.tsmiCondCopy = new ToolStripMenuItem();
    this.tsmiCondPaste = new ToolStripMenuItem();
    this.tsmiCondSep1 = new ToolStripSeparator();
    this.tsmiCondDelete = new ToolStripMenuItem();
    this.tpTblConds = new TabPage();
    this.gridTblCond = new Grid();
    this.cmsTblConds = new ContextMenuStrip(this.components);
    this.tsmiTblCondEdit = new ToolStripMenuItem();
    this.tsmiTblCondDelete = new ToolStripMenuItem();
    this.tsmiTblCondSep1 = new ToolStripSeparator();
    this.tsmiTblCondModifNone = new ToolStripMenuItem();
    this.tsmiTblCondModifMin = new ToolStripMenuItem();
    this.tsmiTblCondModifMax = new ToolStripMenuItem();
    this.tpTable = new TabPage();
    this.cmsInfoTable = new ContextMenuStrip(this.components);
    this.tsmiInfoTableCondAdd = new ToolStripMenuItem();
    this.tsmiInfoTableCondEdit = new ToolStripMenuItem();
    this.tsmiInfoTableCondDelete = new ToolStripMenuItem();
    this.tsmiInfoTableSep1 = new ToolStripSeparator();
    this.tsmiInfoTableModifNone = new ToolStripMenuItem();
    this.tsmiInfoTableModifMin = new ToolStripMenuItem();
    this.tsmiInfoTableModifMax = new ToolStripMenuItem();
    this.tsmiInfoTableSep2 = new ToolStripSeparator();
    this.tsmiInfoTableDefRow = new ToolStripMenuItem();
    this.tsmiInfoTableDefRowAdd = new ToolStripMenuItem();
    this.tsmiInfoTableDefRowRemove = new ToolStripMenuItem();
    this.tsmiInfoTableDefRowSep1 = new ToolStripSeparator();
    this.tsmiInfoTableDefRowClear = new ToolStripMenuItem();
    this.tsmiInfoTableDefRowInvert = new ToolStripMenuItem();
    this.cmsDragDropTarget = new ContextMenuStrip(this.components);
    this.tsmiTargetBefore = new ToolStripMenuItem();
    this.tsmiTargetAfter = new ToolStripMenuItem();
    this.tsmiTargetSep1 = new ToolStripSeparator();
    this.tsmiTargetCurrentInside = new ToolStripMenuItem();
    this.tableLayoutPanel.SuspendLayout();
    this.pnlButtons.SuspendLayout();
    this.splCntrMain.BeginInit();
    this.splCntrMain.Panel1.SuspendLayout();
    this.splCntrMain.Panel2.SuspendLayout();
    this.splCntrMain.SuspendLayout();
    this.splCntrTop.BeginInit();
    this.splCntrTop.Panel1.SuspendLayout();
    this.splCntrTop.Panel2.SuspendLayout();
    this.splCntrTop.SuspendLayout();
    this.cmsSelectionItems.SuspendLayout();
    this.tcSelection.SuspendLayout();
    this.tpSelCond.SuspendLayout();
    this.cmsConditions.SuspendLayout();
    this.tpTblConds.SuspendLayout();
    this.cmsTblConds.SuspendLayout();
    this.cmsInfoTable.SuspendLayout();
    this.cmsDragDropTarget.SuspendLayout();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.tableLayoutPanel, "tableLayoutPanel");
    this.tableLayoutPanel.Controls.Add((Control) this.pnlButtons, 0, 2);
    this.tableLayoutPanel.Controls.Add((Control) this.splCntrMain, 0, 0);
    this.tableLayoutPanel.Name = "tableLayoutPanel";
    componentResourceManager.ApplyResources((object) this.pnlButtons, "pnlButtons");
    this.tableLayoutPanel.SetColumnSpan((Control) this.pnlButtons, 2);
    this.pnlButtons.Controls.Add((Control) this.btnCancel);
    this.pnlButtons.Controls.Add((Control) this.btnOk);
    this.pnlButtons.Name = "pnlButtons";
    this.btnCancel.DialogResult = DialogResult.Cancel;
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.UseVisualStyleBackColor = true;
    this.btnOk.DialogResult = DialogResult.OK;
    componentResourceManager.ApplyResources((object) this.btnOk, "btnOk");
    this.btnOk.Name = "btnOk";
    this.btnOk.UseVisualStyleBackColor = true;
    this.tableLayoutPanel.SetColumnSpan((Control) this.splCntrMain, 2);
    componentResourceManager.ApplyResources((object) this.splCntrMain, "splCntrMain");
    this.splCntrMain.Name = "splCntrMain";
    this.splCntrMain.Panel1.Controls.Add((Control) this.splCntrTop);
    this.splCntrMain.Panel2.Controls.Add((Control) this.tcSelection);
    componentResourceManager.ApplyResources((object) this.splCntrTop, "splCntrTop");
    this.splCntrTop.Name = "splCntrTop";
    this.splCntrTop.Panel1.Controls.Add((Control) this.tvSelectionItems);
    this.splCntrTop.Panel2.Controls.Add((Control) this.pgSelectionItem);
    this.tvSelectionItems.AllowDrop = true;
    this.tvSelectionItems.ContextMenuStrip = this.cmsSelectionItems;
    componentResourceManager.ApplyResources((object) this.tvSelectionItems, "tvSelectionItems");
    this.tvSelectionItems.HideSelection = false;
    this.tvSelectionItems.ImageList = this.ilSelTree;
    this.tvSelectionItems.Name = "tvSelectionItems";
    this.tvSelectionItems.ItemDrag += new ItemDragEventHandler(this.tvSelectionItems_ItemDrag);
    this.tvSelectionItems.AfterSelect += new TreeViewEventHandler(this.tvSelectionItems_AfterSelect);
    this.tvSelectionItems.DragDrop += new DragEventHandler(this.tvSelectionItems_DragDrop);
    this.tvSelectionItems.DragEnter += new DragEventHandler(this.tvSelectionItems_DragEnter);
    this.tvSelectionItems.DragOver += new DragEventHandler(this.tvSelectionItems_DragOver);
    this.tvSelectionItems.DragLeave += new EventHandler(this.tvSelectionItems_DragLeave);
    this.tvSelectionItems.KeyDown += new KeyEventHandler(this.tvSelectionItems_KeyDown);
    this.tvSelectionItems.KeyUp += new KeyEventHandler(this.tvSelectionItems_KeyUp);
    this.cmsSelectionItems.Items.AddRange(new ToolStripItem[12]
    {
      (ToolStripItem) this.tsmiSelItemCreate,
      (ToolStripItem) this.tsmiSelItemRename,
      (ToolStripItem) this.tsmiSelItemDelete,
      (ToolStripItem) this.tsmiSelItemSep1,
      (ToolStripItem) this.tsmiSelItemMove,
      (ToolStripItem) this.tsmiSelItemSep2,
      (ToolStripItem) this.tsmiSelItemCopy,
      (ToolStripItem) this.tsmiSelItemPaste,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.tsmiSelItemTable,
      (ToolStripItem) this.tsmiSelItemTableRemove,
      (ToolStripItem) this.tsmiSelItemCondition
    });
    this.cmsSelectionItems.Name = "contextMenuStrip1";
    componentResourceManager.ApplyResources((object) this.cmsSelectionItems, "cmsSelectionItems");
    this.cmsSelectionItems.Opening += new CancelEventHandler(this.cmsSelectionItems_Opening);
    this.tsmiSelItemCreate.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiSelItemCreateNew,
      (ToolStripItem) this.tsmiSelItemCreateInComposition
    });
    this.tsmiSelItemCreate.Name = "tsmiSelItemCreate";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCreate, "tsmiSelItemCreate");
    this.tsmiSelItemCreateNew.Name = "tsmiSelItemCreateNew";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCreateNew, "tsmiSelItemCreateNew");
    this.tsmiSelItemCreateNew.Click += new EventHandler(this.tsmiSelItemCreateNew_Click);
    this.tsmiSelItemCreateInComposition.Name = "tsmiSelItemCreateInComposition";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCreateInComposition, "tsmiSelItemCreateInComposition");
    this.tsmiSelItemCreateInComposition.Click += new EventHandler(this.tsmiSelItemCreateInComposition_Click);
    this.tsmiSelItemRename.Name = "tsmiSelItemRename";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemRename, "tsmiSelItemRename");
    this.tsmiSelItemRename.Click += new EventHandler(this.tsmiSelItemRename_Click);
    this.tsmiSelItemDelete.Name = "tsmiSelItemDelete";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemDelete, "tsmiSelItemDelete");
    this.tsmiSelItemDelete.Click += new EventHandler(this.tsmiSelItemDelete_Click);
    this.tsmiSelItemSep1.Name = "tsmiSelItemSep1";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemSep1, "tsmiSelItemSep1");
    this.tsmiSelItemMove.DropDownItems.AddRange(new ToolStripItem[7]
    {
      (ToolStripItem) this.tsmiSelItemMoveFirst,
      (ToolStripItem) this.tsmiSelItemMoveUp,
      (ToolStripItem) this.tsmiSelItemMoveDown,
      (ToolStripItem) this.tsmiSelItemMoveLast,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.tsmiSelItemMoveLevelUp,
      (ToolStripItem) this.tsmiSelItemMoveLevelRoot
    });
    this.tsmiSelItemMove.Name = "tsmiSelItemMove";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMove, "tsmiSelItemMove");
    this.tsmiSelItemMoveFirst.Name = "tsmiSelItemMoveFirst";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMoveFirst, "tsmiSelItemMoveFirst");
    this.tsmiSelItemMoveFirst.Click += new EventHandler(this.tsmiSelItemMoveFirst_Click);
    this.tsmiSelItemMoveUp.Name = "tsmiSelItemMoveUp";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMoveUp, "tsmiSelItemMoveUp");
    this.tsmiSelItemMoveUp.Click += new EventHandler(this.tsmiSelItemMoveUp_Click);
    this.tsmiSelItemMoveDown.Name = "tsmiSelItemMoveDown";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMoveDown, "tsmiSelItemMoveDown");
    this.tsmiSelItemMoveDown.Click += new EventHandler(this.tsmiSelItemMoveDown_Click);
    this.tsmiSelItemMoveLast.Name = "tsmiSelItemMoveLast";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMoveLast, "tsmiSelItemMoveLast");
    this.tsmiSelItemMoveLast.Click += new EventHandler(this.tsmiSelItemMoveLast_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem2, "toolStripMenuItem2");
    this.tsmiSelItemMoveLevelUp.Name = "tsmiSelItemMoveLevelUp";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMoveLevelUp, "tsmiSelItemMoveLevelUp");
    this.tsmiSelItemMoveLevelUp.Click += new EventHandler(this.tsmiSelItemMoveLevelUp_Click);
    this.tsmiSelItemMoveLevelRoot.Name = "tsmiSelItemMoveLevelRoot";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemMoveLevelRoot, "tsmiSelItemMoveLevelRoot");
    this.tsmiSelItemMoveLevelRoot.Click += new EventHandler(this.tsmiSelItemMoveLevelRoot_Click);
    this.tsmiSelItemSep2.Name = "tsmiSelItemSep2";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemSep2, "tsmiSelItemSep2");
    this.tsmiSelItemCopy.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiSelItemCopyCurrent,
      (ToolStripItem) this.tsmiSelItemCopyAll
    });
    this.tsmiSelItemCopy.Name = "tsmiSelItemCopy";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCopy, "tsmiSelItemCopy");
    this.tsmiSelItemCopyCurrent.Name = "tsmiSelItemCopyCurrent";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCopyCurrent, "tsmiSelItemCopyCurrent");
    this.tsmiSelItemCopyCurrent.Click += new EventHandler(this.tsmiSelItemCopyCurrent_Click);
    this.tsmiSelItemCopyAll.Name = "tsmiSelItemCopyAll";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCopyAll, "tsmiSelItemCopyAll");
    this.tsmiSelItemCopyAll.Click += new EventHandler(this.tsmiSelItemCopyAll_Click);
    this.tsmiSelItemPaste.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsmiSelItemPasteToRoot,
      (ToolStripItem) this.tsmiSelItemPasteToCurrentItem
    });
    this.tsmiSelItemPaste.Name = "tsmiSelItemPaste";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemPaste, "tsmiSelItemPaste");
    this.tsmiSelItemPasteToRoot.Name = "tsmiSelItemPasteToRoot";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemPasteToRoot, "tsmiSelItemPasteToRoot");
    this.tsmiSelItemPasteToRoot.Click += new EventHandler(this.tsmiSelItemPasteToRoot_Click);
    this.tsmiSelItemPasteToCurrentItem.Name = "tsmiSelItemPasteToCurrentItem";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemPasteToCurrentItem, "tsmiSelItemPasteToCurrentItem");
    this.tsmiSelItemPasteToCurrentItem.Click += new EventHandler(this.tsmiSelItemPasteToCurrentItem_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    componentResourceManager.ApplyResources((object) this.toolStripMenuItem1, "toolStripMenuItem1");
    this.tsmiSelItemTable.Name = "tsmiSelItemTable";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemTable, "tsmiSelItemTable");
    this.tsmiSelItemTable.Click += new EventHandler(this.tsmiSelItemTable_Click);
    this.tsmiSelItemTableRemove.Name = "tsmiSelItemTableRemove";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemTableRemove, "tsmiSelItemTableRemove");
    this.tsmiSelItemTableRemove.Click += new EventHandler(this.tsmiSelItemTableRemove_Click);
    this.tsmiSelItemCondition.Name = "tsmiSelItemCondition";
    componentResourceManager.ApplyResources((object) this.tsmiSelItemCondition, "tsmiSelItemCondition");
    this.tsmiSelItemCondition.Click += new EventHandler(this.tsmiSelItemCondition_Click);
    this.ilSelTree.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.ilSelTree, "ilSelTree");
    this.ilSelTree.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.pgSelectionItem, "pgSelectionItem");
    this.pgSelectionItem.Name = "pgSelectionItem";
    this.pgSelectionItem.PropertyValueChanged += new PropertyValueChangedEventHandler(this.pgSelectionItem_PropertyValueChanged_1);
    this.tcSelection.Controls.Add((Control) this.tpSelCond);
    this.tcSelection.Controls.Add((Control) this.tpTblConds);
    this.tcSelection.Controls.Add((Control) this.tpTable);
    componentResourceManager.ApplyResources((object) this.tcSelection, "tcSelection");
    this.tcSelection.Name = "tcSelection";
    this.tcSelection.SelectedIndex = 0;
    this.tcSelection.SelectedIndexChanged += new EventHandler(this.tcSelection_SelectedIndexChanged);
    this.tpSelCond.Controls.Add((Control) this.tbxSelCond);
    componentResourceManager.ApplyResources((object) this.tpSelCond, "tpSelCond");
    this.tpSelCond.Name = "tpSelCond";
    this.tpSelCond.UseVisualStyleBackColor = true;
    this.tbxSelCond.BackColor = SystemColors.Window;
    this.tbxSelCond.ContextMenuStrip = this.cmsConditions;
    componentResourceManager.ApplyResources((object) this.tbxSelCond, "tbxSelCond");
    this.tbxSelCond.Name = "tbxSelCond";
    this.tbxSelCond.ReadOnly = true;
    this.tbxSelCond.DoubleClick += new EventHandler(this.tbxSelCond_DoubleClick);
    this.cmsConditions.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.tsmiCondEdit,
      (ToolStripItem) this.tsmiCondSep2,
      (ToolStripItem) this.tsmiCondCopy,
      (ToolStripItem) this.tsmiCondPaste,
      (ToolStripItem) this.tsmiCondSep1,
      (ToolStripItem) this.tsmiCondDelete
    });
    this.cmsConditions.Name = "cmsConditions";
    componentResourceManager.ApplyResources((object) this.cmsConditions, "cmsConditions");
    this.cmsConditions.Opening += new CancelEventHandler(this.cmsConditions_Opening);
    this.tsmiCondEdit.Name = "tsmiCondEdit";
    componentResourceManager.ApplyResources((object) this.tsmiCondEdit, "tsmiCondEdit");
    this.tsmiCondEdit.Click += new EventHandler(this.tsmiCondEdit_Click);
    this.tsmiCondSep2.Name = "tsmiCondSep2";
    componentResourceManager.ApplyResources((object) this.tsmiCondSep2, "tsmiCondSep2");
    this.tsmiCondCopy.Name = "tsmiCondCopy";
    componentResourceManager.ApplyResources((object) this.tsmiCondCopy, "tsmiCondCopy");
    this.tsmiCondCopy.Click += new EventHandler(this.tsmiCondCopy_Click);
    this.tsmiCondPaste.Name = "tsmiCondPaste";
    componentResourceManager.ApplyResources((object) this.tsmiCondPaste, "tsmiCondPaste");
    this.tsmiCondPaste.Click += new EventHandler(this.tsmiCondPaste_Click);
    this.tsmiCondSep1.Name = "tsmiCondSep1";
    componentResourceManager.ApplyResources((object) this.tsmiCondSep1, "tsmiCondSep1");
    this.tsmiCondDelete.Name = "tsmiCondDelete";
    componentResourceManager.ApplyResources((object) this.tsmiCondDelete, "tsmiCondDelete");
    this.tsmiCondDelete.Click += new EventHandler(this.tsmiCondDelete_Click);
    this.tpTblConds.Controls.Add((Control) this.gridTblCond);
    componentResourceManager.ApplyResources((object) this.tpTblConds, "tpTblConds");
    this.tpTblConds.Name = "tpTblConds";
    this.tpTblConds.UseVisualStyleBackColor = true;
    this.gridTblCond.ContextMenuStrip = this.cmsTblConds;
    componentResourceManager.ApplyResources((object) this.gridTblCond, "gridTblCond");
    this.gridTblCond.GridToolTipActive = true;
    this.gridTblCond.Name = "gridTblCond";
    this.gridTblCond.SpecialKeys = GridSpecialKeys.Default;
    this.gridTblCond.StyleGrid = (StyleGrid) null;
    this.cmsTblConds.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.tsmiTblCondEdit,
      (ToolStripItem) this.tsmiTblCondDelete,
      (ToolStripItem) this.tsmiTblCondSep1,
      (ToolStripItem) this.tsmiTblCondModifNone,
      (ToolStripItem) this.tsmiTblCondModifMin,
      (ToolStripItem) this.tsmiTblCondModifMax
    });
    this.cmsTblConds.Name = "cmsTblConds";
    componentResourceManager.ApplyResources((object) this.cmsTblConds, "cmsTblConds");
    this.cmsTblConds.Opening += new CancelEventHandler(this.cmsTblConds_Opening);
    this.tsmiTblCondEdit.Name = "tsmiTblCondEdit";
    componentResourceManager.ApplyResources((object) this.tsmiTblCondEdit, "tsmiTblCondEdit");
    this.tsmiTblCondEdit.Click += new EventHandler(this.tsmiTblCond_Click);
    this.tsmiTblCondDelete.Name = "tsmiTblCondDelete";
    componentResourceManager.ApplyResources((object) this.tsmiTblCondDelete, "tsmiTblCondDelete");
    this.tsmiTblCondDelete.Click += new EventHandler(this.tsmiTblCond_Click);
    this.tsmiTblCondSep1.Name = "tsmiTblCondSep1";
    componentResourceManager.ApplyResources((object) this.tsmiTblCondSep1, "tsmiTblCondSep1");
    this.tsmiTblCondModifNone.Name = "tsmiTblCondModifNone";
    componentResourceManager.ApplyResources((object) this.tsmiTblCondModifNone, "tsmiTblCondModifNone");
    this.tsmiTblCondModifNone.Click += new EventHandler(this.tsmiTblCondModify_Click);
    this.tsmiTblCondModifMin.Name = "tsmiTblCondModifMin";
    componentResourceManager.ApplyResources((object) this.tsmiTblCondModifMin, "tsmiTblCondModifMin");
    this.tsmiTblCondModifMin.Click += new EventHandler(this.tsmiTblCondModify_Click);
    this.tsmiTblCondModifMax.Name = "tsmiTblCondModifMax";
    componentResourceManager.ApplyResources((object) this.tsmiTblCondModifMax, "tsmiTblCondModifMax");
    this.tsmiTblCondModifMax.Click += new EventHandler(this.tsmiTblCondModify_Click);
    componentResourceManager.ApplyResources((object) this.tpTable, "tpTable");
    this.tpTable.Name = "tpTable";
    this.tpTable.UseVisualStyleBackColor = true;
    this.cmsInfoTable.Items.AddRange(new ToolStripItem[9]
    {
      (ToolStripItem) this.tsmiInfoTableCondAdd,
      (ToolStripItem) this.tsmiInfoTableCondEdit,
      (ToolStripItem) this.tsmiInfoTableCondDelete,
      (ToolStripItem) this.tsmiInfoTableSep1,
      (ToolStripItem) this.tsmiInfoTableModifNone,
      (ToolStripItem) this.tsmiInfoTableModifMin,
      (ToolStripItem) this.tsmiInfoTableModifMax,
      (ToolStripItem) this.tsmiInfoTableSep2,
      (ToolStripItem) this.tsmiInfoTableDefRow
    });
    this.cmsInfoTable.Name = "cmsInfoTable";
    componentResourceManager.ApplyResources((object) this.cmsInfoTable, "cmsInfoTable");
    this.cmsInfoTable.Opening += new CancelEventHandler(this.cmsInfoTable_Opening);
    this.tsmiInfoTableCondAdd.Name = "tsmiInfoTableCondAdd";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableCondAdd, "tsmiInfoTableCondAdd");
    this.tsmiInfoTableCondAdd.Click += new EventHandler(this.tsmiInfoTableCond_Click);
    this.tsmiInfoTableCondEdit.Name = "tsmiInfoTableCondEdit";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableCondEdit, "tsmiInfoTableCondEdit");
    this.tsmiInfoTableCondEdit.Click += new EventHandler(this.tsmiInfoTableCond_Click);
    this.tsmiInfoTableCondDelete.Name = "tsmiInfoTableCondDelete";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableCondDelete, "tsmiInfoTableCondDelete");
    this.tsmiInfoTableCondDelete.Click += new EventHandler(this.tsmiInfoTableCond_Click);
    this.tsmiInfoTableSep1.Name = "tsmiInfoTableSep1";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableSep1, "tsmiInfoTableSep1");
    this.tsmiInfoTableModifNone.Name = "tsmiInfoTableModifNone";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableModifNone, "tsmiInfoTableModifNone");
    this.tsmiInfoTableModifNone.Click += new EventHandler(this.tsmiInfoTableModify_Click);
    this.tsmiInfoTableModifMin.Name = "tsmiInfoTableModifMin";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableModifMin, "tsmiInfoTableModifMin");
    this.tsmiInfoTableModifMin.Click += new EventHandler(this.tsmiInfoTableModify_Click);
    this.tsmiInfoTableModifMax.Name = "tsmiInfoTableModifMax";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableModifMax, "tsmiInfoTableModifMax");
    this.tsmiInfoTableModifMax.Click += new EventHandler(this.tsmiInfoTableModify_Click);
    this.tsmiInfoTableSep2.Name = "tsmiInfoTableSep2";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableSep2, "tsmiInfoTableSep2");
    this.tsmiInfoTableDefRow.DropDownItems.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.tsmiInfoTableDefRowAdd,
      (ToolStripItem) this.tsmiInfoTableDefRowRemove,
      (ToolStripItem) this.tsmiInfoTableDefRowSep1,
      (ToolStripItem) this.tsmiInfoTableDefRowClear,
      (ToolStripItem) this.tsmiInfoTableDefRowInvert
    });
    this.tsmiInfoTableDefRow.Name = "tsmiInfoTableDefRow";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableDefRow, "tsmiInfoTableDefRow");
    this.tsmiInfoTableDefRow.DropDownOpening += new EventHandler(this.tsmiInfoTableDefRow_DropDownOpening);
    this.tsmiInfoTableDefRowAdd.Name = "tsmiInfoTableDefRowAdd";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableDefRowAdd, "tsmiInfoTableDefRowAdd");
    this.tsmiInfoTableDefRowAdd.Click += new EventHandler(this.tsmiInfoTableDefRowAdd_Click);
    this.tsmiInfoTableDefRowRemove.Name = "tsmiInfoTableDefRowRemove";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableDefRowRemove, "tsmiInfoTableDefRowRemove");
    this.tsmiInfoTableDefRowRemove.Click += new EventHandler(this.tsmiInfoTableDefRowRemove_Click);
    this.tsmiInfoTableDefRowSep1.Name = "tsmiInfoTableDefRowSep1";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableDefRowSep1, "tsmiInfoTableDefRowSep1");
    this.tsmiInfoTableDefRowClear.Name = "tsmiInfoTableDefRowClear";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableDefRowClear, "tsmiInfoTableDefRowClear");
    this.tsmiInfoTableDefRowClear.Click += new EventHandler(this.tsmiInfoTableDefRowClear_Click);
    this.tsmiInfoTableDefRowInvert.Name = "tsmiInfoTableDefRowInvert";
    componentResourceManager.ApplyResources((object) this.tsmiInfoTableDefRowInvert, "tsmiInfoTableDefRowInvert");
    this.tsmiInfoTableDefRowInvert.Click += new EventHandler(this.tsmiInfoTableDefRowInvert_Click);
    this.cmsDragDropTarget.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsmiTargetBefore,
      (ToolStripItem) this.tsmiTargetAfter,
      (ToolStripItem) this.tsmiTargetSep1,
      (ToolStripItem) this.tsmiTargetCurrentInside
    });
    this.cmsDragDropTarget.Name = "cmsDragDropTarget";
    componentResourceManager.ApplyResources((object) this.cmsDragDropTarget, "cmsDragDropTarget");
    this.cmsDragDropTarget.Opening += new CancelEventHandler(this.cmsDragDropTarget_Opening);
    this.tsmiTargetBefore.Name = "tsmiTargetBefore";
    componentResourceManager.ApplyResources((object) this.tsmiTargetBefore, "tsmiTargetBefore");
    this.tsmiTargetBefore.Click += new EventHandler(this.tsmiTargetBefore_Click);
    this.tsmiTargetAfter.Name = "tsmiTargetAfter";
    componentResourceManager.ApplyResources((object) this.tsmiTargetAfter, "tsmiTargetAfter");
    this.tsmiTargetAfter.Click += new EventHandler(this.tsmiTargetAfter_Click);
    this.tsmiTargetSep1.Name = "tsmiTargetSep1";
    componentResourceManager.ApplyResources((object) this.tsmiTargetSep1, "tsmiTargetSep1");
    this.tsmiTargetCurrentInside.Name = "tsmiTargetCurrentInside";
    componentResourceManager.ApplyResources((object) this.tsmiTargetCurrentInside, "tsmiTargetCurrentInside");
    this.tsmiTargetCurrentInside.Click += new EventHandler(this.tsmiTargetCurrentInside_Click);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (AutoSelectionEditForm);
    this.ShowInTaskbar = false;
    this.FormClosing += new FormClosingEventHandler(this.AutoSelectionEditForm_FormClosing);
    this.FormClosed += new FormClosedEventHandler(this.AutoSelectionEditForm_FormClosed);
    this.Load += new EventHandler(this.AutoSelectionEditForm_Load);
    this.tableLayoutPanel.ResumeLayout(false);
    this.pnlButtons.ResumeLayout(false);
    this.splCntrMain.Panel1.ResumeLayout(false);
    this.splCntrMain.Panel2.ResumeLayout(false);
    this.splCntrMain.EndInit();
    this.splCntrMain.ResumeLayout(false);
    this.splCntrTop.Panel1.ResumeLayout(false);
    this.splCntrTop.Panel2.ResumeLayout(false);
    this.splCntrTop.EndInit();
    this.splCntrTop.ResumeLayout(false);
    this.cmsSelectionItems.ResumeLayout(false);
    this.tcSelection.ResumeLayout(false);
    this.tpSelCond.ResumeLayout(false);
    this.tpSelCond.PerformLayout();
    this.cmsConditions.ResumeLayout(false);
    this.tpTblConds.ResumeLayout(false);
    this.cmsTblConds.ResumeLayout(false);
    this.cmsInfoTable.ResumeLayout(false);
    this.cmsDragDropTarget.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private enum DragAndDropMode
  {
    InsertBefore,
    InsertAfter,
    InsertInto,
  }
}
