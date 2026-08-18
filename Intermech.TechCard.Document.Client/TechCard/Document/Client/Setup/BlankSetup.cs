// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Setup.BlankSetup
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Diagnostics;
using Intermech.Docking;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using Intermech.TechCard.Document.Client.Configs.Visual;
using Intermech.TechCard.Document.Client.Configs.Visual.Interfaces;
using Intermech.TechCard.Document.Client.Configs.Visual.Services;
using Intermech.TechCard.Document.Interfaces.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Common;
using Intermech.TechCard.Document.Interfaces.Configs.Interfaces;
using Intermech.TechCard.Document.Interfaces.Configs.Structure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

#nullable disable
namespace Intermech.TechCard.Document.Client.Setup;

public class BlankSetup : DockControl, ICommandTarget
{
  private System.IServiceProvider _services;
  private IConfigViewService _configViewService;
  private Dictionary<DocumentConfigElementType, IConfigViewController> _viewControllers = new Dictionary<DocumentConfigElementType, IConfigViewController>();
  private (IConfigViewController controller, bool changed, bool configCreated) _currentConfigChangeState = ((IConfigViewController) null, false, false);
  private bool _wasSomethingChanged;
  private ICommandState _saveCommandState;
  private const string BTN_SAVE = "Save";
  private readonly string MODULE_NAME = "TECHCARD_DOC_CLIENT";
  private readonly string BLAN_SETUP_VIEW = nameof (BLAN_SETUP_VIEW);
  private readonly string DOC_TEMPLATE_TREE_WIDTH = "TEMPLATE_TREE_WIDTH";
  private readonly long DOC_TEMPLATE_TREE_WIDTH_DEFAULT = 200;
  private Rectangle _dragBoxFromMouseDown;
  private int _levelDrag;
  private Icon[] _treeIcons;
  private bool _needUpdateTree = true;
  private ImDocumentEditorForm _docTemplateEditor;
  private IContainer components;
  private Splitter splBlankConfigs;
  private Intermech.VirtualTreeView.VirtualTreeView vtvDocTemplate;
  private Column column1;
  private ContextMenuStrip cmsDocTemlate;
  private ToolStripMenuItem tsTreeItemExclude;
  private ToolStripMenuItem tsTreeItemCopy;
  private ToolStripMenuItem tsTreeItemPaste;
  private ToolStripSeparator tsTreeItemSep0;
  private ToolStripMenuItem tsTreeItemMove;
  private ToolStripMenuItem tsTreeItemMoveFirst;
  private ToolStripMenuItem tsTreeItemMoveUp;
  private ToolStripMenuItem tsTreeItemMoveDown;
  private ToolStripMenuItem tsTreeItemMoveLast;
  private ToolStripSeparator tsTreeItemSep1;
  private ToolStripMenuItem tsTreeItemReload;
  private System.Windows.Forms.TabControl tcMain;
  private System.Windows.Forms.TabPage tpBlankConfigs;
  private System.Windows.Forms.TabPage tpObjectsOrdersConfigs;
  private TableLayoutPanel tlpMain;
  private Column colObjectName;
  private Column _columnElements;
  private Panel pnlConfig;
  private ToolStripMenuItem tsShowDocTemplate;

  private void InitServices()
  {
    ServiceContainer serviceContainer = new ServiceContainer();
    this._configViewService = (IConfigViewService) new ConfigViewService();
    serviceContainer.AddService<IConfigViewService>(this._configViewService);
    this._services = (System.IServiceProvider) serviceContainer;
  }

  private void InitializeTreeIcons()
  {
    if (this._treeIcons != null)
      return;
    this._treeIcons = new Icon[14];
    Bitmap[] bitmapArray = new Bitmap[10]
    {
      ImDocument.Icon is Bitmap icon1 ? (Bitmap) icon1.Clone() : (Bitmap) (object) null,
      Page.Icon is Bitmap icon2 ? (Bitmap) icon2.Clone() : (Bitmap) (object) null,
      TextBoxCreator.Icon is Bitmap icon3 ? (Bitmap) icon3.Clone() : (Bitmap) (object) null,
      LabelCreator.Icon is Bitmap icon4 ? (Bitmap) icon4.Clone() : (Bitmap) (object) null,
      TableCreator.Icon is Bitmap icon5 ? (Bitmap) icon5.Clone() : (Bitmap) (object) null,
      PolylineCreator.Icon is Bitmap icon6 ? (Bitmap) icon6.Clone() : (Bitmap) (object) null,
      ContainerCreator.Icon is Bitmap icon7 ? (Bitmap) icon7.Clone() : (Bitmap) (object) null,
      TableCreator.RowIcon is Bitmap rowIcon ? (Bitmap) rowIcon.Clone() : (Bitmap) (object) null,
      PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.DocumentsComplect.png") as Bitmap,
      new Bitmap(this.GetType().Assembly.GetManifestResourceStream("Intermech.TechCard.Document.Client.Resources.VirtualGroup.png"))
    };
    for (int index = 0; index < bitmapArray.Length; ++index)
    {
      using (Bitmap bmp = new Bitmap(bitmapArray[index].Width, bitmapArray[index].Height))
      {
        using (Graphics graphics = Graphics.FromImage((Image) bmp))
        {
          graphics.DrawImage((Image) bitmapArray[index], 0, 0);
          this._treeIcons[index] = ImageHelper.BitmapToIcon(bmp);
        }
      }
    }
  }

  private void BuildObjectOrdersView()
  {
    IConfigViewController viewController = this._configViewService.CreateViewController(this.Rules.Properties.ObjectsConfigs.ElementType, this._services);
    if (viewController == null)
      return;
    this._viewControllers.Add(this.Rules.Properties.ObjectsConfigs.ElementType, viewController);
    viewController.Show((Control) this.tpObjectsOrdersConfigs, (IConfigViewSettings) new ConfigViewSettings(this._services)
    {
      ConfigElement = (IDocumentConfigElement) this.Rules.Properties.ObjectsConfigs,
      ReadOnly = this.ReadOnly,
      OnDataChanged = new Action<IConfigViewController, bool>(this.OnConfigViewChanged)
    });
  }

  private void BuildTemplateTree()
  {
    DocumentTreeNode dataSource = this.vtvDocTemplate.DataSource as DocumentTreeNode;
    if (dataSource != this.Rules.Template)
    {
      if (dataSource != null)
        this.TreeNodeUnsubscribeHandlers(dataSource);
      this.vtvDocTemplate.DataSource = (object) this.Rules.Template;
      this.TreeNodeSubscribeHandlers((DocumentTreeNode) this.Rules.Template);
    }
    this.vtvDocTemplate.SelectedItem = this.vtvDocTemplate.DataSource;
  }

  private void OnConfigViewChanged(IConfigViewController viewController, bool changed)
  {
    if (!changed)
      return;
    this._wasSomethingChanged = true;
    this._currentConfigChangeState.controller = viewController;
    this._currentConfigChangeState.changed = true;
    this._saveCommandState.Enabled = true;
  }

  private bool CanTreeRowsCopy(RowSelectionList selectedRow)
  {
    // ISSUE: explicit non-virtual call
    return (selectedRow != null ? (__nonvirtual (selectedRow.Count) != 1 ? 1 : 0) : 1) == 0 && selectedRow.All<Row>((Func<Row, bool>) (row => (row.Item is DocumentTreeNode documentTreeNode ? documentTreeNode.Parent : (DocumentTreeNode) null) is TableData parent && parent.IsPageFlow));
  }

  private bool CanVirtGroupsRowMove(Row selectedRow)
  {
    return !this.ReadOnly && selectedRow?.ParentRow?.Item is VirtualGroupNode;
  }

  private bool CanVirtGroupsRowExclude(Row selectedRow)
  {
    return !this.ReadOnly && selectedRow?.ParentRow?.Item is VirtualGroupNode;
  }

  private bool IsChildNodeOfParent(Row childCandidate, Row parentCandidate)
  {
    if (childCandidate == null)
      return false;
    for (Row parentRow = childCandidate.ParentRow; parentRow != null; parentRow = parentRow.ParentRow)
    {
      if (parentRow == parentCandidate)
        return true;
    }
    return false;
  }

  private bool CanVirtGroupsRowPaste(Row selectedRow)
  {
    if (this.ReadOnly || !(selectedRow?.Item is VirtualGroupNode))
      return false;
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
    if (service == null)
      return false;
    DataObject dataObject = service.GetDataObject() as DataObject;
    bool flag = dataObject != null;
    if (flag)
    {
      flag = false;
      foreach (string format in dataObject.GetFormats())
      {
        if (dataObject.GetData(format) is RowSelectionList data)
        {
          flag = true;
          using (IEnumerator<Row> enumerator = data.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              Row current = enumerator.Current;
              if (this.IsChildNodeOfParent(selectedRow, current))
              {
                flag = false;
                break;
              }
            }
            break;
          }
        }
      }
    }
    return flag;
  }

  private void VirtGroupsRowMove(Row selectedRow, int newIndex)
  {
    if (!this.CanVirtGroupsRowMove(selectedRow))
      return;
    DocumentTreeNode child = selectedRow.Item as DocumentTreeNode;
    if (!(selectedRow.ParentRow?.Item is VirtualGroupNode virtualGroupNode) || child == null)
      return;
    virtualGroupNode.InsertChildNode(newIndex, child, false, false, false, false, false);
    this.OnConfigViewChanged((IConfigViewController) null, !this.ReadOnly);
  }

  private void VirtGroupsRowCopy(RowSelectionList selectedRows)
  {
    if (!this.CanTreeRowsCopy(selectedRows))
      return;
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
    if (service == null)
      return;
    service.SetDataObject((object) new DataObject((object) new RowSelectionList()
    {
      (IEnumerable) selectedRows
    }));
  }

  private void VirtGroupsRowPaste(Row selectedRow)
  {
    if (!this.CanVirtGroupsRowPaste(selectedRow))
      return;
    RowSelectionList selRows = (RowSelectionList) null;
    IClipboard service = ServiceUtils.GetService<IClipboard>((object) ApplicationServices.Container, true);
    if (service != null && service.GetDataObject() is DataObject dataObject)
    {
      foreach (string format in dataObject.GetFormats())
      {
        if (dataObject.GetData(format) is RowSelectionList data)
        {
          selRows = data;
          break;
        }
      }
    }
    this.vtvDocTemlatePasteRows(selectedRow, selRows);
  }

  private void VirtGroupsRowExclude(Row selectedRow)
  {
    if (!this.CanVirtGroupsRowExclude(selectedRow) || !(selectedRow?.ParentRow?.Item is VirtualGroupNode virtualGroupNode))
      return;
    virtualGroupNode.RemoveChildNode(selectedRow.Item as DocumentTreeNode, false, false);
    this.OnConfigViewChanged((IConfigViewController) null, !this.ReadOnly);
  }

  private Row TreeNodeFindRow(object documentNode)
  {
    return !(documentNode is VirtualGroupNode virtualGroupNode) ? this.vtvDocTemplate.FindRow((object) (documentNode as DocumentTreeNode)) : virtualGroupNode.Data as Row;
  }

  private void TreeNodeSubscribeHandlers([NotNull] DocumentTreeNode documentTreeNode)
  {
    if (documentTreeNode is ImDocumentData imDocumentData)
      imDocumentData.PageUnlocked += new PageUnlocked_EventHandler(this.TreeNode_PageUnlocked);
    documentTreeNode.ChildNodeAdded += new ChildNodeAdded_EventHandler(this.TreeNode_ChildNodeAdded);
    documentTreeNode.ChildNodeRemoved += new ChildNodeRemoved_EventHandler(this.TreeNode_ChildNodeRemoved);
    documentTreeNode.NameChanged += new NameChanged_EventHandler(this.TreeNode_NameChanged);
    documentTreeNode.NodeRemoved += new NodeRemoved_EventHandler(this.TreeNode_NodeRemoved);
    documentTreeNode.ChildNodePositionChanged += new ChildNodePositionChanged_EventHandler(this.TreeNode_ChildNodePositionChanged);
    if (!(documentTreeNode is VisualNode visualNode))
      return;
    visualNode.VisibleChanged += new VisibleChanged_EventHandler(this.TreeNode_VisibleChanged);
  }

  private void TreeNodeUnsubscribeHandlers([NotNull] DocumentTreeNode documentTreeNode)
  {
    if (documentTreeNode is ImDocumentData imDocumentData)
      imDocumentData.PageUnlocked -= new PageUnlocked_EventHandler(this.TreeNode_PageUnlocked);
    documentTreeNode.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.TreeNode_ChildNodeAdded);
    documentTreeNode.ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.TreeNode_ChildNodeRemoved);
    documentTreeNode.NameChanged -= new NameChanged_EventHandler(this.TreeNode_NameChanged);
    documentTreeNode.NodeRemoved -= new NodeRemoved_EventHandler(this.TreeNode_NodeRemoved);
    documentTreeNode.ChildNodePositionChanged -= new ChildNodePositionChanged_EventHandler(this.TreeNode_ChildNodePositionChanged);
    if (!(documentTreeNode is VisualNode visualNode))
      return;
    visualNode.VisibleChanged -= new VisibleChanged_EventHandler(this.TreeNode_VisibleChanged);
  }

  private void vtvDocTemlatePasteRows(
    Row targetRow,
    RowSelectionList selRows,
    DragDropEffects mode = DragDropEffects.Copy)
  {
    // ISSUE: explicit non-virtual call
    if (targetRow.Item == null || selRows != null && __nonvirtual (selRows.Count) == 0)
      return;
    DocumentTreeNode documentTreeNode1 = targetRow.Item as DocumentTreeNode;
    DocumentTreeNode documentTreeNode2 = targetRow.ParentRow != null ? targetRow.ParentRow.Item as DocumentTreeNode : (DocumentTreeNode) null;
    if (documentTreeNode2 == null)
      return;
    Row row1 = ((IEnumerable<Row>) new Row[2]
    {
      targetRow,
      targetRow.ParentRow
    }).FirstOrDefault<Row>((Func<Row, bool>) (row => row?.Item is VirtualGroupNode));
    this._needUpdateTree = false;
    for (int index = selRows.Count - 1; index >= 0; --index)
    {
      Row selRow = selRows[index];
      if (selRow.Item is DocumentTreeNode documentTreeNode3)
      {
        if (row1 == null && mode == DragDropEffects.Copy)
          documentTreeNode3 = documentTreeNode3.Clone();
        if (selRow.Level == targetRow.Level || documentTreeNode2 is VirtualGroupNode)
        {
          int childIndex = targetRow.ChildIndex;
          if (selRow.ChildIndex < childIndex && documentTreeNode3.Index > -1 && documentTreeNode3.Parent == documentTreeNode1.Parent)
            --childIndex;
          if (index == 0)
          {
            this._needUpdateTree = true;
            documentTreeNode2.InsertChildNode(childIndex, documentTreeNode3, false, true, true, true);
          }
          else
            documentTreeNode2.InsertChildNode(childIndex, documentTreeNode3, false, true, false, false);
          documentTreeNode1 = documentTreeNode3;
        }
        else if (index == 0)
          documentTreeNode1.InsertChildNode(0, documentTreeNode3, false, true, true, true);
        else
          documentTreeNode1.InsertChildNode(0, documentTreeNode3, false, true, false, false);
        if (row1 != null)
        {
          Row parentRow = selRow.ParentRow;
          if (parentRow != null)
          {
            if (mode == DragDropEffects.Move && parentRow.Item is VirtualGroupNode virtualGroupNode && virtualGroupNode != documentTreeNode2)
              virtualGroupNode.RemoveChildNode(documentTreeNode3, false, false);
            parentRow.UpdateChildren(true, false);
          }
        }
      }
    }
    this.OnConfigViewChanged((IConfigViewController) null, !this.ReadOnly);
  }

  private void TreeNode_ChildNodeAdded(object sender, ChildNode_EventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.TreeNode_ChildNodeAdded), sender, (object) e);
    else if (this.vtvDocTemplate.InvokeRequired)
    {
      this.vtvDocTemplate.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.TreeNode_ChildNodeAdded), sender, (object) e);
    }
    else
    {
      if (sender is TableElement tableElement && tableElement.IsDistributing)
        return;
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      PageData pageData1 = (PageData) null;
      if (sender is PageElementNode pageElementNode)
        pageData1 = pageElementNode.Page;
      if (sender is PageData pageData2)
        pageData1 = pageData2;
      if (pageData1 != null && pageData1.IsLocked)
        return;
      if (sender is VirtualGroupNode virtualGroupNode)
      {
        IDocumentConfigElement element1 = this.Rules.Properties.FindElement(virtualGroupNode.Parent.Id);
        IDocumentConfigElement element2 = this.Rules.Properties.FindElement(e.Child.Id);
        if (element1 is VariantConfig variantConfig1 && element2 is VariantConfig variantConfig2)
        {
          int index = virtualGroupNode.Nodes.IndexOf(e.Child);
          variantConfig1.ChildsList.Remove(variantConfig2.Id);
          variantConfig1.ChildsList.Insert(index, variantConfig2.Id);
        }
        this.TreeNodeFindRow((object) virtualGroupNode.TopLevelTable)?.UpdateChildren(true, false);
      }
      if (!(sender is DocumentTreeNode documentNode))
        return;
      Row row = this.TreeNodeFindRow((object) documentNode);
      if (row == null || this.InvokeRequired || this.vtvDocTemplate.InvokeRequired)
        return;
      row.UpdateChildren(true, false);
    }
  }

  private void TreeNode_ChildNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new ChildNodeRemoved_EventHandler(this.TreeNode_ChildNodeRemoved), sender, (object) e);
    else if (this.vtvDocTemplate.InvokeRequired)
    {
      this.vtvDocTemplate.BeginInvoke((Delegate) new ChildNodeAdded_EventHandler(this.TreeNode_ChildNodeAdded), sender, (object) e);
    }
    else
    {
      PageData pageData1 = (PageData) null;
      if (sender is PageElementNode pageElementNode)
        pageData1 = pageElementNode.Page;
      if (sender is PageData pageData2)
        pageData1 = pageData2;
      if (pageData1 != null && pageData1.IsLocked || sender is TableElement tableElement && tableElement.IsDistributing)
        return;
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if (sender is VirtualGroupNode virtualGroupNode)
      {
        IDocumentConfigElement element1 = this.Rules.Properties.FindElement(virtualGroupNode.Parent.Id);
        IDocumentConfigElement element2 = this.Rules.Properties.FindElement(e.Child.Id);
        if (element1 is VariantConfig variantConfig1 && element2 is VariantConfig variantConfig2)
          variantConfig1.ChildsList.Remove(variantConfig2.Id);
        this.TreeNodeFindRow((object) virtualGroupNode.TopLevelTable)?.UpdateChildren(true, false);
      }
      if (!(sender is DocumentTreeNode documentNode))
        return;
      this.TreeNodeFindRow((object) documentNode)?.UpdateChildren(true, true);
    }
  }

  private void TreeNode_ChildNodePositionChanged(
    object sender,
    ChildNodePositionChanged_EventArgs e)
  {
    PageData pageData1 = (PageData) null;
    if (sender is PageElementNode pageElementNode)
      pageData1 = pageElementNode.Page;
    if (sender is PageData pageData2)
      pageData1 = pageData2;
    if (pageData1 != null && pageData1.IsLocked || sender is TableElement tableElement && tableElement.IsDistributing)
      return;
    if (sender is VirtualGroupNode virtualGroupNode)
    {
      IDocumentConfigElement element1 = this.Rules.Properties.FindElement(virtualGroupNode.Parent.Id);
      IDocumentConfigElement element2 = this.Rules.Properties.FindElement(e.Node.Id);
      if (element1 is VariantConfig variantConfig1 && element2 is VariantConfig variantConfig2)
      {
        variantConfig1.ChildsList.RemoveAt(e.OldIndex);
        variantConfig1.ChildsList.Insert(e.NewIndex, variantConfig2.Id);
      }
      this._needUpdateTree = true;
    }
    if (!this._needUpdateTree)
      return;
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    this.TreeNodeFindRow((object) (sender as DocumentTreeNode))?.UpdateChildren(true, false);
  }

  private void TreeNode_NameChanged(object sender, NameChanged_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new NameChanged_EventHandler(this.TreeNode_NameChanged), sender, (object) e);
    }
    else
    {
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if ((sender is DocumentTreeNode documentTreeNode ? documentTreeNode.Parent : (DocumentTreeNode) null) == null)
        return;
      Row row = this.TreeNodeFindRow(sender);
      if (row == null)
        return;
      this.vtvDocTemplate.UpdateRowData(row);
    }
  }

  private void TreeNode_NodeRemoved(object sender, Removed_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new NodeRemoved_EventHandler(this.TreeNode_NodeRemoved), sender, (object) e);
    }
    else
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (e.Node == null)
        return;
      this.TreeNodeUnsubscribeHandlers(e.Node);
    }
  }

  private void TreeNode_PageUnlocked(object sender, PageUnlockedArgs e)
  {
    if (this.InvokeRequired)
      this.BeginInvoke((Delegate) new PageUnlocked_EventHandler(this.TreeNode_PageUnlocked), sender, (object) e);
    else if (this.vtvDocTemplate.InvokeRequired)
      this.vtvDocTemplate.BeginInvoke((Delegate) new PageUnlocked_EventHandler(this.TreeNode_PageUnlocked), sender, (object) e);
    else
      this.TreeNodeFindRow((object) e.Page)?.UpdateChildren(true, false);
  }

  private void TreeNode_VisibleChanged(object sender, VisibleChanged_EventArgs e)
  {
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    if (sender is TableElement tableElement && tableElement.IsDistributing)
      return;
    Row row = this.TreeNodeFindRow(sender);
    if (row == null)
      return;
    this.vtvDocTemplate.UpdateRowData(row);
  }

  private void vtvDocTemlate_GetParent(object sender, GetParentEventArgs e)
  {
    try
    {
      if (!(e.Item is DocumentTreeNode documentTreeNode))
        return;
      e.Parent = (object) documentTreeNode.Parent;
    }
    catch (Exception ex)
    {
      string empty = string.Empty;
      ImDocumentData.ShowException(ex, empty);
    }
  }

  private void vtvDocTemlate_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    e.AllowedDropLocations = RowDropLocation.OnRow;
  }

  private void vtvDocTemlate_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.BeginInvoke((Delegate) new GetCellDataHandler(this.vtvDocTemlate_GetCellData), sender, (object) e);
    }
    else
    {
      if (e.Column != this._columnElements)
        return;
      StyleDelta delta = new StyleDelta()
      {
        ForeColor = SystemColors.WindowText
      };
      if (e.Row.Item is VisualNode visualNode)
      {
        if (!visualNode.Visible)
          delta.ForeColor = SystemColors.GrayText;
        else if (!visualNode.IsVisibleNow)
          delta.ForeColor = Color.FromArgb(80 /*0x50*/, 80 /*0x50*/, 80 /*0x50*/);
      }
      e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta);
      e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta);
      e.CellData.Value = e.Row.Item is DocumentTreeNode documentTreeNode ? (object) documentTreeNode.GetDefautCaption() : (object) (string) null;
    }
  }

  private void vtvDocTemlate_GetChildren(object sender, GetChildrenEventArgs e)
  {
    try
    {
      if (!(e.Row.Item is DocumentTreeNode ownerTreeNode))
        return;
      TableData tableData = ownerTreeNode as TableData;
      DocumentTreeNodeCollection nodes1 = ownerTreeNode.Nodes;
      List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
      e.Children = (IList) documentTreeNodeList;
      if (nodes1 != null)
      {
        foreach (DocumentTreeNode documentTreeNode in nodes1)
        {
          DocumentTreeNode documentSubNode = documentTreeNode;
          if (documentSubNode != null)
          {
            if (tableData != null && tableData.IsPageFlow)
            {
              IDocumentConfigElement documentConfigElement = this.Rules.Properties.Elements.FirstOrDefault<IDocumentConfigElement>((Func<IDocumentConfigElement, bool>) (item => item is VariantConfig variantConfig && variantConfig.ChildsList.Contains(documentSubNode.Id)));
              if (documentConfigElement != null && documentConfigElement.Id != documentSubNode.Id)
                continue;
            }
            documentTreeNodeList.Add(documentSubNode);
            this.TreeNodeUnsubscribeHandlers(documentSubNode);
            this.TreeNodeSubscribeHandlers(documentSubNode);
          }
        }
      }
      if (tableData == null || !tableData.TopLevelTable.IsPageFlow || tableData.IsPageFlow)
        return;
      if (!(this.Rules.Properties.FindElement(tableData.Id) is VariantConfig variantConfig1))
      {
        variantConfig1 = this.Rules.Properties.FindOrCreateElement(tableData.Id, DocumentConfigElementType.Variant) as VariantConfig;
        this.OnConfigViewChanged((IConfigViewController) null, !this.ReadOnly);
      }
      if (variantConfig1 == null)
        return;
      DocumentTreeNode documentTreeNode1 = (DocumentTreeNode) new VirtualGroupNode(ownerTreeNode);
      documentTreeNodeList.Add(documentTreeNode1);
      List<DocumentTreeNode> nodes2 = new List<DocumentTreeNode>();
      foreach (string childs in variantConfig1.ChildsList)
      {
        DocumentTreeNode node = this.Rules.Template.FindNode(childs);
        if (node != null)
          nodes2.Add(node);
      }
      documentTreeNode1.AddChildNodes(nodes2, false, false);
      documentTreeNode1.SetParent(ownerTreeNode, false, false);
      this.TreeNodeUnsubscribeHandlers(documentTreeNode1);
      this.TreeNodeSubscribeHandlers(documentTreeNode1);
    }
    catch (Exception ex)
    {
      string empty = string.Empty;
      ImDocumentData.ShowException(ex, empty);
    }
  }

  private void vtvDocTemlate_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (this._treeIcons == null)
      this.InitializeTreeIcons();
    if (e.Row.Item is ImDocument)
      e.RowData.Icon = this._treeIcons[0];
    else if (e.Row.Item is Page)
      e.RowData.Icon = this._treeIcons[1];
    else if (e.Row.Item is TextBoxElement)
      e.RowData.Icon = this._treeIcons[2];
    else if (e.Row.Item is LabelElement)
      e.RowData.Icon = this._treeIcons[3];
    else if (e.Row.Item is TableElement tableElement)
      e.RowData.Icon = tableElement.IsRow ? this._treeIcons[7] : this._treeIcons[4];
    else if (e.Row.Item is Polyline)
      e.RowData.Icon = this._treeIcons[5];
    else if (e.Row.Item is ContainerElement)
      e.RowData.Icon = this._treeIcons[6];
    else if (e.Row.Item is DocumentsComplect)
    {
      e.RowData.Icon = this._treeIcons[8];
    }
    else
    {
      if (!(e.Row.Item is VirtualGroupNode virtualGroupNode))
        return;
      virtualGroupNode.Data = (object) e.Row;
      e.RowData.Icon = this._treeIcons[9];
    }
  }

  private void vtvDocTemlate_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    e.DropEffect = DragDropEffects.None;
    Row row1 = e.Row;
    Row parentRow = e.Row?.ParentRow;
    DocumentTreeNode documentTreeNode1 = row1.Item as DocumentTreeNode;
    if (!(parentRow?.Item is DocumentTreeNode documentTreeNode2) || !documentTreeNode1.IsInWorkspace() || !(e.Data.GetData(typeof (RowSelectionList)) is RowSelectionList data) || data.Count <= 0)
      return;
    Row row2 = data[0];
    VirtualGroupNode virtualGroupNode1 = documentTreeNode1 as VirtualGroupNode;
    VirtualGroupNode virtualGroupNode2 = documentTreeNode2 as VirtualGroupNode;
    if (virtualGroupNode1 != null || virtualGroupNode2 != null)
    {
      for (Row row3 = row1; row3 != null; row3 = row3.ParentRow)
      {
        foreach (Row row4 in data)
        {
          if (row3.Item == row4.Item)
            return;
        }
      }
      if (!data.All<Row>((Func<Row, bool>) (item => item.Item is RectangleElement rectangleElement && rectangleElement.Parent is TableData parent && parent.IsPageFlow)))
        return;
    }
    DocumentTreeNode parent1 = (row2.Item as DocumentTreeNode).Parent;
    if (((row1.Item as DocumentTreeNode).Parent is Page || row1.Item is Page) && parent1 is Page)
    {
      if (row1 == null)
        return;
    }
    else if (row1 == null || virtualGroupNode1 == null && virtualGroupNode2 == null && (row1.Level != row2.Level || row1.ParentRow != row2.ParentRow))
      return;
    for (int index = 0; index < data.Count; ++index)
    {
      if (data[index].Item != null)
      {
        DocumentTreeNode child = data[index].Item as DocumentTreeNode;
        if (data[index].Level == row1.Level && virtualGroupNode1 == null && virtualGroupNode2 == null)
        {
          if (documentTreeNode2 != null && !documentTreeNode2.CanAddChildElement(child))
            return;
          if (child is TableData tableData && documentTreeNode2 is TableElement)
          {
            int headersCount = (documentTreeNode2 as TableElement).HeadersCount;
            if (tableData.IsRow && (tableData.TableCellType == CellType.Header && documentTreeNode1.Index > headersCount || tableData.TableCellType == CellType.DataCell && documentTreeNode1.Index < headersCount))
              return;
          }
        }
        else if (!documentTreeNode1.CanAddChildElement(child))
          return;
      }
    }
    if (Control.ModifierKeys != Keys.Control)
      e.DropEffect = DragDropEffects.Move;
    else
      e.DropEffect = DragDropEffects.Copy;
  }

  private void vtvDocTemlate_RowDrop(object sender, RowDropEventArgs e)
  {
    if (!e.Data.GetDataPresent(typeof (RowSelectionList)))
      return;
    Row row = e.Row;
    if (row.Item == null)
      return;
    RowSelectionList data = e.Data.GetData(typeof (RowSelectionList)) as RowSelectionList;
    this.vtvDocTemlatePasteRows(row, data, e.DropEffect);
  }

  private void vtvDocTemlate_SelectionChanged(object sender, EventArgs e)
  {
    if (!(this.vtvDocTemplate.SelectedItem is DocumentTreeNode selectedItem))
    {
      this.HideCurrentConfigControl();
    }
    else
    {
      this.SelectNodeInTemplateEditor(selectedItem);
      IDocumentConfigElement documentConfigElement = this.Rules.Properties.FindElement(selectedItem.Id, selectedItem.ToConfigElementType());
      if (documentConfigElement == null)
      {
        documentConfigElement = DocumentConfigElementFactory.CreateDocumentElementConfig(selectedItem.ToConfigElementType());
        if (documentConfigElement == null)
        {
          this.HideCurrentConfigControl();
          return;
        }
        (documentConfigElement as DocumentConfigElement).Id = selectedItem.Id;
        this._currentConfigChangeState.configCreated = true;
      }
      else
        this._currentConfigChangeState.configCreated = false;
      IConfigViewController viewController;
      if (!this._viewControllers.TryGetValue(documentConfigElement.ElementType, out viewController))
      {
        viewController = this._configViewService.CreateViewController(documentConfigElement.ElementType, this._services);
        if (viewController != null)
          this._viewControllers.Add(documentConfigElement.ElementType, viewController);
      }
      this._currentConfigChangeState.controller = viewController;
      this._currentConfigChangeState.changed = false;
      if (viewController == null)
        return;
      viewController.Show((Control) this.pnlConfig, (IConfigViewSettings) new ConfigViewSettings(this._services)
      {
        ConfigElement = documentConfigElement,
        ReadOnly = this.ReadOnly,
        OnDataChanged = new Action<IConfigViewController, bool>(this.OnConfigViewChanged)
      });
    }
  }

  private void HideCurrentConfigControl()
  {
    this.pnlConfig.SuspendLayout();
    foreach (object control1 in (ArrangedElementCollection) this.pnlConfig.Controls)
    {
      if (control1 is Control control2)
        control2.Hide();
    }
    this.pnlConfig.ResumeLayout();
  }

  private void vtvDocTemlate_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (e.Column != this._columnElements)
      return;
    (e.Row.Item as DocumentTreeNode).Name = (string) e.NewValue;
  }

  private void vtvDocTemlate_DragEnter(object sender, DragEventArgs e)
  {
  }

  private void vtvDocTemlate_DragOver(object sender, DragEventArgs e)
  {
    if (this.vtvDocTemplate.GetRowAt(e.X, e.Y) == null)
      e.Effect = DragDropEffects.None;
    else if (Control.ModifierKeys != Keys.Control)
      e.Effect = DragDropEffects.Move;
    else
      e.Effect = DragDropEffects.Copy;
  }

  private void vtvDocTemlate_MouseDown(object sender, MouseEventArgs e)
  {
    Size dragSize = SystemInformation.DragSize;
    dragSize.Width += 5;
    dragSize.Height += 5;
    this._dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
  }

  private void vtvDocTemlate_MouseMove(object sender, MouseEventArgs e)
  {
    ref Rectangle local = ref this._dragBoxFromMouseDown;
    Point location = e.Location;
    int x = location.X;
    location = e.Location;
    int y = location.Y;
    if (local.Contains(x, y) || this.ReadOnly || e.Button != MouseButtons.Left || this.vtvDocTemplate.SelectedItems == null)
      return;
    this._levelDrag = -1;
    Row[] rows = this.vtvDocTemplate.SelectedRows.GetRows();
    if (rows == null || rows.Length == 0)
      return;
    int level = rows[0].Level;
    for (int index = 0; index < rows.Length; ++index)
    {
      Row row = rows[index];
      if (level != row.Level || row.Item == null || !(row.Item is DocumentTreeNode documentTreeNode) || !documentTreeNode.CanRemove() || documentTreeNode is RectangleElement rectangleElement && rectangleElement.ParentCell != null && rectangleElement.ParentCell.IsRow)
        return;
    }
    this._levelDrag = level;
    if (Control.ModifierKeys != Keys.Control)
    {
      int num1 = (int) this.vtvDocTemplate.DoDragDrop((object) this.vtvDocTemplate.SelectedRows, DragDropEffects.Move);
    }
    else
    {
      int num2 = (int) this.vtvDocTemplate.DoDragDrop((object) this.vtvDocTemplate.SelectedRows, DragDropEffects.Copy);
    }
  }

  private void vtvDocTemlate_SelectionChanging(object sender, SelectionChangingEventArgs e)
  {
    if (this._currentConfigChangeState.controller == null || !this._currentConfigChangeState.changed)
      return;
    IDocumentConfigElement config;
    if (this._currentConfigChangeState.controller.ApplyChanges(out config))
    {
      if (this._currentConfigChangeState.configCreated)
        this.Rules.Properties.Elements.Add(config);
      this._currentConfigChangeState.changed = false;
      this._currentConfigChangeState.controller = (IConfigViewController) null;
    }
    else
      e.Cancel = true;
  }

  private void cmsDocTemlate_Opening(object sender, CancelEventArgs e)
  {
    Row selectedRow = this.vtvDocTemplate.SelectedRow;
    RectangleElement rectangleElement = selectedRow.Item as RectangleElement;
    ToolStripSeparator tsTreeItemSep0 = this.tsTreeItemSep0;
    ToolStripSeparator tsTreeItemSep1 = this.tsTreeItemSep1;
    ToolStripMenuItem tsTreeItemMove = this.tsTreeItemMove;
    ToolStripMenuItem tsTreeItemExclude = this.tsTreeItemExclude;
    ToolStripMenuItem tsTreeItemCopy = this.tsTreeItemCopy;
    ToolStripMenuItem tsTreeItemPaste1 = this.tsTreeItemPaste;
    ToolStripMenuItem tsTreeItemPaste2 = this.tsTreeItemPaste;
    bool? isPageFlow = rectangleElement?.TopLevelTable?.IsPageFlow;
    int num1;
    bool flag1 = (num1 = (int) isPageFlow ?? 0) != 0;
    tsTreeItemPaste2.Visible = num1 != 0;
    int num2;
    bool flag2 = (num2 = flag1 ? 1 : 0) != 0;
    tsTreeItemPaste1.Visible = num2 != 0;
    int num3;
    bool flag3 = (num3 = flag2 ? 1 : 0) != 0;
    tsTreeItemCopy.Visible = num3 != 0;
    int num4;
    bool flag4 = (num4 = flag3 ? 1 : 0) != 0;
    tsTreeItemExclude.Visible = num4 != 0;
    int num5;
    bool flag5 = (num5 = flag4 ? 1 : 0) != 0;
    tsTreeItemMove.Visible = num5 != 0;
    int num6;
    bool flag6 = (num6 = flag5 ? 1 : 0) != 0;
    tsTreeItemSep1.Visible = num6 != 0;
    int num7 = flag6 ? 1 : 0;
    tsTreeItemSep0.Visible = num7 != 0;
    bool flag7 = this.CanVirtGroupsRowMove(selectedRow);
    this.tsTreeItemMove.Enabled = flag7;
    this.tsTreeItemMoveFirst.Enabled = this.tsTreeItemMoveUp.Enabled = flag7 && selectedRow.ChildIndex != 0;
    this.tsTreeItemMoveDown.Enabled = this.tsTreeItemMoveLast.Enabled = flag7 && selectedRow.ChildIndex != selectedRow.ParentRow.ChildItems.Count - 1;
    this.tsTreeItemExclude.Enabled = this.CanVirtGroupsRowExclude(selectedRow);
    this.tsTreeItemCopy.Enabled = this.CanTreeRowsCopy(this.vtvDocTemplate.SelectedRows);
    this.tsTreeItemPaste.Enabled = this.CanVirtGroupsRowPaste(selectedRow);
  }

  private void tsTreeItemCopy_Click(object sender, EventArgs e)
  {
    this.VirtGroupsRowCopy(this.vtvDocTemplate.SelectedRows);
  }

  private void tsTreeItemPaste_Click(object sender, EventArgs e)
  {
    this.VirtGroupsRowPaste(this.vtvDocTemplate.SelectedRow);
  }

  private void tsTreeItemMoveFirst_Click(object sender, EventArgs e)
  {
    if (!this.CanVirtGroupsRowMove(this.vtvDocTemplate.SelectedRow))
      return;
    this.VirtGroupsRowMove(this.vtvDocTemplate.SelectedRow, 0);
  }

  private void tsTreeItemMoveUp_Click(object sender, EventArgs e)
  {
    if (!this.CanVirtGroupsRowMove(this.vtvDocTemplate.SelectedRow))
      return;
    this.VirtGroupsRowMove(this.vtvDocTemplate.SelectedRow, this.vtvDocTemplate.SelectedRow.ChildIndex - 1);
  }

  private void tsTreeItemMoveDown_Click(object sender, EventArgs e)
  {
    if (!this.CanVirtGroupsRowMove(this.vtvDocTemplate.SelectedRow))
      return;
    this.VirtGroupsRowMove(this.vtvDocTemplate.SelectedRow, this.vtvDocTemplate.SelectedRow.ChildIndex + 1);
  }

  private void tsTreeItemMoveEnd_Click(object sender, EventArgs e)
  {
    if (!this.CanVirtGroupsRowMove(this.vtvDocTemplate.SelectedRow))
      return;
    this.VirtGroupsRowMove(this.vtvDocTemplate.SelectedRow, this.vtvDocTemplate.SelectedRow.ParentRow.ChildItems.Count - 1);
  }

  private void tsTreeItemExclude_Click(object sender, EventArgs e)
  {
    this.VirtGroupsRowExclude(this.vtvDocTemplate.SelectedRow);
  }

  private void tsTreeItemReload_Click(object sender, EventArgs e) => this.BuildTemplateTree();

  private void ApplyCurrentChanges()
  {
    this._viewControllers.Values.ToList<IConfigViewController>().ForEach((Action<IConfigViewController>) (vc =>
    {
      IDocumentConfigElement config;
      vc.ApplyChanges(out config);
      if (vc != this._currentConfigChangeState.controller || !this._currentConfigChangeState.changed || !this._currentConfigChangeState.configCreated)
        return;
      this.Rules.Properties.Elements.Add(config);
    }));
  }

  private void SelectNodeInTemplateEditor(DocumentTreeNode nodeToSelect)
  {
    if (nodeToSelect == null)
      return;
    this._docTemplateEditor?.Activate();
    this._docTemplateEditor?.DocumentControl.SetSelection(nodeToSelect, true, true);
    if (this.vtvDocTemplate.Focused)
      return;
    this.vtvDocTemplate.Focus();
  }

  private void tsShowDocTemplate_Click(object sender, EventArgs e)
  {
    if (this._docTemplateEditor == null)
    {
      this._docTemplateEditor = DocumentEditorPlugin.Instance.OpenDocument((DocumentTreeNode) this.Rules.Template, this.ReadOnly, false);
      this._docTemplateEditor.DisposeDocumentOnClose = false;
      this.SubscribeToDocTemplateEditorEvents(this._docTemplateEditor);
      this._docTemplateEditor.Show(ApplicationServices.Container.GetService<DockManager>(), DockState.DockRight);
    }
    if (!(this.vtvDocTemplate.SelectedItem is DocumentTreeNode nodeToSelect))
      nodeToSelect = (DocumentTreeNode) this.Rules.Template;
    this.SelectNodeInTemplateEditor(nodeToSelect);
  }

  private void SubscribeToDocTemplateEditorEvents([CanBeNull] ImDocumentEditorForm docTemplateEditor)
  {
    if (docTemplateEditor == null)
      return;
    docTemplateEditor.DocumentControl.SelectionChanged += new SelectionChanged_EventHandler(this.DocTemplateEditorSelectionChanged);
    docTemplateEditor.Closed += new EventHandler(this.OnDocumentTemplateEditorClosed);
  }

  private void UnSubscribeFromDocTemplateEditorEvents([CanBeNull] ImDocumentEditorForm docTemplateEditor)
  {
    if (docTemplateEditor == null)
      return;
    docTemplateEditor.DocumentControl.SelectionChanged -= new SelectionChanged_EventHandler(this.DocTemplateEditorSelectionChanged);
    docTemplateEditor.Closed -= new EventHandler(this.OnDocumentTemplateEditorClosed);
  }

  private void DocTemplateEditorSelectionChanged(object sender, SelectionChanged_EventArgs e)
  {
    if (this.vtvDocTemplate.SelectedItem == this._docTemplateEditor.DocumentControl.SelectedNode)
      return;
    this.vtvDocTemlate_SelectionChanging((object) this.vtvDocTemplate, new SelectionChangingEventArgs(this.vtvDocTemplate.SelectedRow, this.vtvDocTemplate.SelectedRow, SelectionChange.Clear));
    this.vtvDocTemplate.SelectedItem = (object) this._docTemplateEditor.DocumentControl.SelectedNode;
  }

  private void OnDocumentTemplateEditorClosed(object sender, EventArgs e)
  {
    this.UnSubscribeFromDocTemplateEditorEvents(this._docTemplateEditor);
    this._docTemplateEditor = (ImDocumentEditorForm) null;
  }

  private void BlankSetup_BeforeFirstShown(object sender, EventArgs e)
  {
    if (ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service)
    {
      int index = service.IndexOf(4, BlankConsts.ObjectType.BlankSetupId);
      this.TabImage = service.ImageList.Images[index];
      this.ShowImageInDocumentTab = true;
    }
    this.LoadViewConfig();
    this.BuildObjectOrdersView();
    this.BuildTemplateTree();
  }

  private void BlankSetup_Closing(object sender, CancelEventArgs e)
  {
    this.SaveViewConfig();
    if (this._wasSomethingChanged && !this.ReadOnly)
    {
      this.ApplyCurrentChanges();
      Func<BlankSetup, bool, bool> onSaveChanges = this.OnSaveChanges;
      bool? nullable = onSaveChanges != null ? new bool?(onSaveChanges(this, true)) : new bool?();
      if (nullable.HasValue && !nullable.Value)
        e.Cancel = true;
    }
    if (e.Cancel || this._docTemplateEditor == null)
      return;
    if (MessageBox.Show(LocalizationHolder.rm.GetString("TechCard.Document_188"), LocalizationHolder.rm.GetString("TechCard.Document_187"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      this._docTemplateEditor.Close();
    this.UnSubscribeFromDocTemplateEditorEvents(this._docTemplateEditor);
  }

  private void LoadViewConfig()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.vtvDocTemplate.Width = (int) sessionKeeper.Session.Configurations.ReadInteger(this.MODULE_NAME, this.BLAN_SETUP_VIEW, this.DOC_TEMPLATE_TREE_WIDTH, this.DOC_TEMPLATE_TREE_WIDTH_DEFAULT, DBConfigMode.UserAndGlobal);
  }

  private void SaveViewConfig()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.Configurations.WriteInteger(this.MODULE_NAME, this.BLAN_SETUP_VIEW, this.DOC_TEMPLATE_TREE_WIDTH, (long) this.vtvDocTemplate.Width, sessionKeeper.Session.UserID);
  }

  public BlankSetup()
  {
    this.InitializeComponent();
    this.InitServices();
    this.InitializeTreeIcons();
    ICommandManager service = ServicesManager.GetService<ICommandManager>();
    service.AddTarget((ICommandTarget) this);
    this._saveCommandState = service.FindCommand("Save");
  }

  public bool Execute(ICommandState commandState)
  {
    if (commandState == null || !(commandState.CommandName == "Save"))
      return false;
    if (this._wasSomethingChanged && !this.ReadOnly)
    {
      this.ApplyCurrentChanges();
      Func<BlankSetup, bool, bool> onSaveChanges = this.OnSaveChanges;
      if (onSaveChanges != null)
      {
        int num = onSaveChanges(this, false) ? 1 : 0;
      }
      this._wasSomethingChanged = false;
      this._saveCommandState.Enabled = false;
    }
    return true;
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (commandState == null || !(commandState.CommandName == "Save"))
      return false;
    commandState.Enabled = this._wasSomethingChanged && !this.ReadOnly;
    return true;
  }

  [NotNull]
  public Rules Rules { get; set; }

  [CanBeNull]
  public Func<BlankSetup, bool, bool> OnSaveChanges { get; set; }

  public bool ReadOnly { get; set; }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.splBlankConfigs = new Splitter();
    this.vtvDocTemplate = new Intermech.VirtualTreeView.VirtualTreeView();
    this._columnElements = new Column();
    this.cmsDocTemlate = new ContextMenuStrip(this.components);
    this.tsTreeItemCopy = new ToolStripMenuItem();
    this.tsTreeItemPaste = new ToolStripMenuItem();
    this.tsTreeItemSep0 = new ToolStripSeparator();
    this.tsTreeItemMove = new ToolStripMenuItem();
    this.tsTreeItemMoveFirst = new ToolStripMenuItem();
    this.tsTreeItemMoveUp = new ToolStripMenuItem();
    this.tsTreeItemMoveDown = new ToolStripMenuItem();
    this.tsTreeItemMoveLast = new ToolStripMenuItem();
    this.tsTreeItemExclude = new ToolStripMenuItem();
    this.tsTreeItemSep1 = new ToolStripSeparator();
    this.tsTreeItemReload = new ToolStripMenuItem();
    this.tsShowDocTemplate = new ToolStripMenuItem();
    this.tcMain = new System.Windows.Forms.TabControl();
    this.tpBlankConfigs = new System.Windows.Forms.TabPage();
    this.pnlConfig = new Panel();
    this.tpObjectsOrdersConfigs = new System.Windows.Forms.TabPage();
    this.tlpMain = new TableLayoutPanel();
    this.vtvDocTemplate.BeginInit();
    this.cmsDocTemlate.SuspendLayout();
    this.tcMain.SuspendLayout();
    this.tpBlankConfigs.SuspendLayout();
    this.tlpMain.SuspendLayout();
    this.SuspendLayout();
    this.splBlankConfigs.Cursor = Cursors.VSplit;
    this.splBlankConfigs.Location = new Point(150, 0);
    this.splBlankConfigs.MinSize = 150;
    this.splBlankConfigs.Name = "splBlankConfigs";
    this.splBlankConfigs.Size = new Size(4, 354);
    this.splBlankConfigs.TabIndex = 8;
    this.splBlankConfigs.TabStop = false;
    this.vtvDocTemplate.AllowDrop = true;
    this.vtvDocTemplate.AllowIndividualRowResize = false;
    this.vtvDocTemplate.AutoFitColumns = true;
    this.vtvDocTemplate.Columns.Add(this._columnElements);
    this.vtvDocTemplate.ContextMenuStrip = this.cmsDocTemlate;
    this.vtvDocTemplate.DisableHeaderContextMenu = false;
    this.vtvDocTemplate.Dock = DockStyle.Left;
    this.vtvDocTemplate.ImageList = (ImageList) null;
    this.vtvDocTemplate.Location = new Point(0, 0);
    this.vtvDocTemplate.MainColumn = this._columnElements;
    this.vtvDocTemplate.Name = "treelist";
    this.vtvDocTemplate.ShowColumnHeaders = false;
    this.vtvDocTemplate.Size = new Size(150, 354);
    this.vtvDocTemplate.TabIndex = 0;
    this.vtvDocTemplate.UseThemedHeaders = false;
    this.vtvDocTemplate.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.vtvDocTemlate_GetAllowedRowDropLocations);
    this.vtvDocTemplate.GetCellData += new GetCellDataHandler(this.vtvDocTemlate_GetCellData);
    this.vtvDocTemplate.GetChildren += new GetChildrenHandler(this.vtvDocTemlate_GetChildren);
    this.vtvDocTemplate.GetParent += new GetParentHandler(this.vtvDocTemlate_GetParent);
    this.vtvDocTemplate.GetRowData += new GetRowDataHandler(this.vtvDocTemlate_GetRowData);
    this.vtvDocTemplate.GetRowDropEffect += new GetRowDropEffectHandler(this.vtvDocTemlate_GetRowDropEffect);
    this.vtvDocTemplate.RowDrop += new RowDropHandler(this.vtvDocTemlate_RowDrop);
    this.vtvDocTemplate.SelectionChanged += new EventHandler(this.vtvDocTemlate_SelectionChanged);
    this.vtvDocTemplate.SelectionChanging += new SelectionChangingHandler(this.vtvDocTemlate_SelectionChanging);
    this.vtvDocTemplate.SetCellValue += new SetCellValueHandler(this.vtvDocTemlate_SetCellValue);
    this.vtvDocTemplate.DragEnter += new DragEventHandler(this.vtvDocTemlate_DragEnter);
    this.vtvDocTemplate.DragOver += new DragEventHandler(this.vtvDocTemlate_DragOver);
    this.vtvDocTemplate.MouseDown += new MouseEventHandler(this.vtvDocTemlate_MouseDown);
    this.vtvDocTemplate.MouseMove += new MouseEventHandler(this.vtvDocTemlate_MouseMove);
    this._columnElements.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this._columnElements.Caption = (string) null;
    this._columnElements.Movable = false;
    this._columnElements.Name = "_columnElements";
    this._columnElements.Resizable = false;
    this._columnElements.Sortable = false;
    this._columnElements.Width = 30;
    this.cmsDocTemlate.Items.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.tsTreeItemCopy,
      (ToolStripItem) this.tsTreeItemPaste,
      (ToolStripItem) this.tsTreeItemSep0,
      (ToolStripItem) this.tsTreeItemMove,
      (ToolStripItem) this.tsTreeItemExclude,
      (ToolStripItem) this.tsTreeItemSep1,
      (ToolStripItem) this.tsTreeItemReload,
      (ToolStripItem) this.tsShowDocTemplate
    });
    this.cmsDocTemlate.Name = "contextMenuStrip1";
    this.cmsDocTemlate.Size = new Size(234, 148);
    this.cmsDocTemlate.Opening += new CancelEventHandler(this.cmsDocTemlate_Opening);
    this.tsTreeItemCopy.Name = "tsTreeItemCopy";
    this.tsTreeItemCopy.ShortcutKeys = Keys.C | Keys.Control;
    this.tsTreeItemCopy.Size = new Size(233, 22);
    this.tsTreeItemCopy.Text = "Копировать";
    this.tsTreeItemCopy.Click += new EventHandler(this.tsTreeItemCopy_Click);
    this.tsTreeItemPaste.Name = "tsTreeItemPaste";
    this.tsTreeItemPaste.ShortcutKeys = Keys.V | Keys.Control;
    this.tsTreeItemPaste.Size = new Size(233, 22);
    this.tsTreeItemPaste.Text = "Вставить";
    this.tsTreeItemPaste.Click += new EventHandler(this.tsTreeItemPaste_Click);
    this.tsTreeItemSep0.Name = "tsTreeItemSep0";
    this.tsTreeItemSep0.Size = new Size(230, 6);
    this.tsTreeItemMove.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.tsTreeItemMoveFirst,
      (ToolStripItem) this.tsTreeItemMoveUp,
      (ToolStripItem) this.tsTreeItemMoveDown,
      (ToolStripItem) this.tsTreeItemMoveLast
    });
    this.tsTreeItemMove.Name = "tsTreeItemMove";
    this.tsTreeItemMove.Size = new Size(233, 22);
    this.tsTreeItemMove.Text = "Переместить";
    this.tsTreeItemMoveFirst.Name = "tsTreeItemMoveFirst";
    this.tsTreeItemMoveFirst.ShortcutKeys = Keys.H | Keys.Control;
    this.tsTreeItemMoveFirst.Size = new Size(212, 22);
    this.tsTreeItemMoveFirst.Text = "В начало";
    this.tsTreeItemMoveFirst.Click += new EventHandler(this.tsTreeItemMoveFirst_Click);
    this.tsTreeItemMoveUp.Name = "tsTreeItemMoveUp";
    this.tsTreeItemMoveUp.ShortcutKeys = Keys.U | Keys.Control;
    this.tsTreeItemMoveUp.Size = new Size(212, 22);
    this.tsTreeItemMoveUp.Text = "На уровень вверх";
    this.tsTreeItemMoveUp.Click += new EventHandler(this.tsTreeItemMoveUp_Click);
    this.tsTreeItemMoveDown.Name = "tsTreeItemMoveDown";
    this.tsTreeItemMoveDown.ShortcutKeys = Keys.D | Keys.Control;
    this.tsTreeItemMoveDown.Size = new Size(212, 22);
    this.tsTreeItemMoveDown.Text = "На уровень вниз";
    this.tsTreeItemMoveDown.Click += new EventHandler(this.tsTreeItemMoveDown_Click);
    this.tsTreeItemMoveLast.Name = "tsTreeItemMoveLast";
    this.tsTreeItemMoveLast.ShortcutKeys = Keys.L | Keys.Control;
    this.tsTreeItemMoveLast.Size = new Size(212, 22);
    this.tsTreeItemMoveLast.Text = "В конец";
    this.tsTreeItemMoveLast.Click += new EventHandler(this.tsTreeItemMoveEnd_Click);
    this.tsTreeItemExclude.Name = "tsTreeItemExclude";
    this.tsTreeItemExclude.ShortcutKeys = Keys.Delete;
    this.tsTreeItemExclude.Size = new Size(233, 22);
    this.tsTreeItemExclude.Text = "Исключить";
    this.tsTreeItemExclude.Click += new EventHandler(this.tsTreeItemExclude_Click);
    this.tsTreeItemSep1.Name = "tsTreeItemSep1";
    this.tsTreeItemSep1.Size = new Size(230, 6);
    this.tsTreeItemReload.Name = "tsTreeItemReload";
    this.tsTreeItemReload.ShortcutKeys = Keys.R | Keys.Control;
    this.tsTreeItemReload.Size = new Size(233, 22);
    this.tsTreeItemReload.Text = "Обновить";
    this.tsTreeItemReload.Click += new EventHandler(this.tsTreeItemReload_Click);
    this.tsShowDocTemplate.Name = "tsShowDocTemplate";
    this.tsShowDocTemplate.Size = new Size(233, 22);
    this.tsShowDocTemplate.Text = "Показать шаблон документа";
    this.tsShowDocTemplate.Click += new EventHandler(this.tsShowDocTemplate_Click);
    this.tcMain.Controls.Add((Control) this.tpBlankConfigs);
    this.tcMain.Controls.Add((Control) this.tpObjectsOrdersConfigs);
    this.tcMain.Dock = DockStyle.Fill;
    this.tcMain.Location = new Point(0, 0);
    this.tcMain.Margin = new Padding(0);
    this.tcMain.Name = "tcMain";
    this.tcMain.SelectedIndex = 0;
    this.tcMain.Size = new Size(695, 380);
    this.tcMain.TabIndex = 9;
    this.tpBlankConfigs.Controls.Add((Control) this.splBlankConfigs);
    this.tpBlankConfigs.Controls.Add((Control) this.pnlConfig);
    this.tpBlankConfigs.Controls.Add((Control) this.vtvDocTemplate);
    this.tpBlankConfigs.Location = new Point(4, 22);
    this.tpBlankConfigs.Margin = new Padding(0);
    this.tpBlankConfigs.Name = "tpBlankConfigs";
    this.tpBlankConfigs.Size = new Size(687, 354);
    this.tpBlankConfigs.TabIndex = 0;
    this.tpBlankConfigs.Text = "Настройки бланка";
    this.tpBlankConfigs.UseVisualStyleBackColor = true;
    this.pnlConfig.Dock = DockStyle.Fill;
    this.pnlConfig.Location = new Point(150, 0);
    this.pnlConfig.Name = "pnlConfig";
    this.pnlConfig.Size = new Size(537, 354);
    this.pnlConfig.TabIndex = 9;
    this.tpObjectsOrdersConfigs.Location = new Point(4, 22);
    this.tpObjectsOrdersConfigs.Margin = new Padding(0);
    this.tpObjectsOrdersConfigs.Name = "tpObjectsOrdersConfigs";
    this.tpObjectsOrdersConfigs.Size = new Size(687, 354);
    this.tpObjectsOrdersConfigs.TabIndex = 1;
    this.tpObjectsOrdersConfigs.Text = "Настройки порядка вывода объектов";
    this.tpObjectsOrdersConfigs.UseVisualStyleBackColor = true;
    this.tlpMain.ColumnCount = 1;
    this.tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
    this.tlpMain.Controls.Add((Control) this.tcMain, 0, 0);
    this.tlpMain.Dock = DockStyle.Fill;
    this.tlpMain.Location = new Point(0, 0);
    this.tlpMain.Name = "tlpMain";
    this.tlpMain.RowCount = 1;
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
    this.tlpMain.Size = new Size(695, 380);
    this.tlpMain.TabIndex = 10;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tlpMain);
    this.MinimumSize = new Size(350, 350);
    this.Name = nameof (BlankSetup);
    this.Size = new Size(695, 380);
    this.Text = "Настройка генерации документа Techcard";
    this.BeforeFirstShown += new EventHandler(this.BlankSetup_BeforeFirstShown);
    this.Closing += new CancelEventHandler(this.BlankSetup_Closing);
    this.vtvDocTemplate.EndInit();
    this.cmsDocTemlate.ResumeLayout(false);
    this.tcMain.ResumeLayout(false);
    this.tpBlankConfigs.ResumeLayout(false);
    this.tlpMain.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
