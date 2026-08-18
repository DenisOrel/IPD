// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.EcoTreeViewDlg
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Docking;
using Intermech.Document.Model;
using Intermech.Document.UI;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.ECO.Client;

public class EcoTreeViewDlg : DockControl, ISkipTargetActivate
{
  public static Guid DockGuid = new Guid("{038EA19E-29DC-4ae4-A24A-B2E46124D629}");
  private string HiddedObjects = "Скрытые объекты";
  private ECOEditorForm form;
  private MenuBar menuBar1;
  private ContextMenuBarItem elementContextMenu;
  private Intermech.VirtualTreeView.VirtualTreeView treeList;
  private Column columnElements;
  private Column columnNames;
  private CellEditor cellEditorTBe;
  private TextBox textBox1;
  private CellEditor cellEditorCBe;
  private CheckBox checkBox1;
  public int BlockUpdateSelection;
  private List<DocumentTreeNode> prevSelection;
  public DocumentControl documentControl;
  public ECOEditorForm ECOEditorForm;
  private ECOTreeItem treeRoot;
  private IContainer components;
  public static bool TreeMenu;
  private Dictionary<int, Icon> imageTypeDict = new Dictionary<int, Icon>();
  private Rectangle dragBoxFromMouseDown;
  private int levelDrag;
  private bool needUpdateTree = true;
  private Icon[] treeIcons;

  public EcoTreeViewDlg()
  {
    this.InitializeComponent();
    this.HideOnClose = true;
    this.treeIcons = new Icon[14];
    Bitmap[] bitmapArray = new Bitmap[10]
    {
      (Bitmap) (ImDocument.Icon as Bitmap).Clone(),
      (Bitmap) (Page.Icon as Bitmap).Clone(),
      (Bitmap) (TextBoxCreator.Icon as Bitmap).Clone(),
      (Bitmap) (LabelCreator.Icon as Bitmap).Clone(),
      (Bitmap) (TableCreator.Icon as Bitmap).Clone(),
      (Bitmap) (PolylineCreator.Icon as Bitmap).Clone(),
      (Bitmap) (ContainerCreator.Icon as Bitmap).Clone(),
      (Bitmap) (TableCreator.RowIcon as Bitmap).Clone(),
      PageElementCreator.LoadImageFromResurcesStatic("Intermech.Document.Model.Resources.DocumentsComplect.png") as Bitmap,
      null
    };
    for (int index = 0; index < bitmapArray.Length - 1; ++index)
    {
      using (Bitmap bmp = new Bitmap(bitmapArray[index].Width, bitmapArray[index].Height))
      {
        using (Graphics graphics = Graphics.FromImage((Image) bmp))
        {
          graphics.DrawImage((Image) bitmapArray[index], 0, 0);
          this.treeIcons[index] = ImageHelper.BitmapToIcon(bmp);
        }
      }
    }
    this.Guid = EcoTreeViewDlg.DockGuid;
    this.treeList.AllowMultiSelect = false;
  }

  public ECOEditorForm Form
  {
    get => this.form;
    set
    {
      if (this.form != null)
        this.form.StructureChanged -= new ECOAncestorForm.StructureChanged_EventHandler(this.form_StructureChanged);
      this.form = value;
      if (this.form == null)
        return;
      this.form.StructureChanged += new ECOAncestorForm.StructureChanged_EventHandler(this.form_StructureChanged);
    }
  }

  private void form_StructureChanged(object sender, StructureChanged_EventArgs e)
  {
    this.RefreshTree();
  }

  public override void Activated()
  {
    base.Activated();
    this.ECOEditorForm.Activated();
    (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).LockEditingContextID = true;
  }

  public override void Deactivated()
  {
    (ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).LockEditingContextID = false;
    this.ECOEditorForm.Deactivated();
    base.Deactivated();
  }

  private void RefreshTree()
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new MethodInvoker(this.RefreshTree));
    }
    else
    {
      Row row = this.treeList.FindRow((object) this.TreeRoot);
      if (row == null)
        return;
      if (this.treeList.InvokeRequired)
        return;
      try
      {
        ++this.BlockUpdateSelection;
        int topRowIndex = this.treeList.TopRowIndex;
        row.UpdateChildren(true, false);
        if (this.treeList.RootRow != null)
          this.treeList.RootRow.ExpandChildren(true);
        this.treeList.TopRowIndex = topRowIndex;
      }
      catch
      {
      }
      finally
      {
        --this.BlockUpdateSelection;
      }
      this.UpdateSelection();
    }
  }

  protected override void OnVisibleChanged(EventArgs e)
  {
    base.OnVisibleChanged(e);
    if (!this.Visible || this.form == null || !this.form.Visible || this.form.Parent == null)
      return;
    this.UpdateTree();
    this.UpdateSelection();
  }

  public ECOTreeItem TreeRoot
  {
    [DebuggerStepThrough] get => this.treeRoot;
    set
    {
      if (this.treeRoot == value)
        return;
      this.treeRoot = value;
      this.UpdateTree();
    }
  }

  public void UpdateTree()
  {
    int topRowIndex = this.treeList.TopRowIndex;
    if (this.treeList.DataSource != this.TreeRoot)
    {
      if (this.TreeRoot != null && this.TreeRoot.Node != null)
        this.TreeRoot.Node.Changed -= new Changed_EventHandler(this.Node_Changed);
      this.treeList.DataSource = (object) this.TreeRoot;
      if (this.TreeRoot != null && this.TreeRoot.Node != null)
        this.TreeRoot.Node.Changed += new Changed_EventHandler(this.Node_Changed);
    }
    this.treeList.UpdateRows(true);
    if (this.treeList.RootRow != null)
      this.treeList.RootRow.ExpandChildren(true);
    this.treeList.TopRowIndex = topRowIndex;
  }

  private void Node_Changed(object sender, Changed_EventArgs e)
  {
  }

  public Intermech.VirtualTreeView.VirtualTreeView DocumentTreeList
  {
    [DebuggerStepThrough] get => this.treeList;
  }

  public DocumentControl DocumentControl
  {
    [DebuggerStepThrough] get => this.documentControl;
    set
    {
      if (this.documentControl != null)
      {
        this.documentControl.ActiveElementChanged -= new ActiveElementChanged_EventHandler(this.ActiveElementChanged);
        this.documentControl.SelectionChanged -= new SelectionChanged_EventHandler(this.DocumentSelectionChanged);
        if (this.documentControl.Document != null)
          this.documentControl.Document.PageUnlocked -= new PageUnlocked_EventHandler(this.Document_PageUnlocked);
      }
      this.documentControl = value;
      if (this.documentControl == null)
        return;
      this.documentControl.SelectionChanged += new SelectionChanged_EventHandler(this.DocumentSelectionChanged);
      this.documentControl.ActiveElementChanged += new ActiveElementChanged_EventHandler(this.ActiveElementChanged);
      if (this.documentControl.Document == null)
        return;
      this.documentControl.Document.PageUnlocked += new PageUnlocked_EventHandler(this.Document_PageUnlocked);
    }
  }

  private void Document_PageUnlocked(object sender, PageUnlockedArgs e)
  {
    if (e.Page == null)
      return;
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new PageUnlocked_EventHandler(this.Document_PageUnlocked), sender, (object) e);
    }
    else
    {
      if (this.treeList.InvokeRequired)
        return;
      Row row = this.treeList.FindRow((object) e.Page);
      int topRowIndex = this.treeList.TopRowIndex;
      row?.UpdateChildren(true, true);
      this.treeList.TopRowIndex = topRowIndex;
    }
  }

  public virtual void UpdateSelection()
  {
    try
    {
      if (this.BlockUpdateSelection > 0 || this.DocumentControl == null)
        return;
      this.SetSelection(this.DocumentControl.SelectedNodes);
    }
    catch
    {
    }
  }

  public List<ECOTreeItem> Selected
  {
    get
    {
      List<ECOTreeItem> selected = new List<ECOTreeItem>();
      foreach (ECOTreeItem selectedItem in (IEnumerable) this.treeList.SelectedItems)
        selected.Add(selectedItem);
      return selected;
    }
  }

  private void SetSelection(List<DocumentTreeNode> docNodes)
  {
    ++this.BlockUpdateSelection;
    try
    {
      if (docNodes == null || docNodes.Count == 0)
      {
        this.treeList.SelectedItem = (object) null;
      }
      else
      {
        List<DocumentTreeNode> documentTreeNodeList1 = new List<DocumentTreeNode>();
        List<DocumentTreeNode> documentTreeNodeList2 = !docNodes[0].IsVirtualNode ? docNodes : docNodes[0].GetNodesFromVirtualNode();
        List<ECOTreeItem> ecoTreeItemList = new List<ECOTreeItem>();
        for (int index = 0; index < documentTreeNodeList2.Count; ++index)
        {
          DocumentTreeNode row = (DocumentTreeNode) this.FindRow(documentTreeNodeList2[index]);
          IList childItems = this.treeList.RootRow.ChildItems;
          if (childItems != null)
          {
            foreach (ECOTreeItem ecoTreeItem in (IEnumerable) childItems)
            {
              if (ecoTreeItem.Node != null && row != null)
              {
                if (row.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) != "")
                {
                  if (ecoTreeItem.Node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) == row.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true))
                    ecoTreeItemList.Add(ecoTreeItem);
                }
                else if (ecoTreeItem.Node == row)
                  ecoTreeItemList.Add(ecoTreeItem);
              }
            }
          }
        }
        this.treeList.SelectedItems = (IList) ecoTreeItemList;
        if (this.treeList.SelectedItems == null || this.treeList.SelectedItems.Count <= 0)
          return;
        Row row1 = this.treeList.FindRow(this.treeList.SelectedItems[0]);
        if (row1 == null)
          return;
        this.treeList.FocusRow = row1;
      }
    }
    finally
    {
      --this.BlockUpdateSelection;
    }
  }

  protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
  {
    if (keyData != Keys.Escape)
      return base.ProcessCmdKey(ref msg, keyData);
    this.DocumentControl.GotoParentElement();
    return true;
  }

  protected virtual void DocumentSelectionChanged(object sender, SelectionChanged_EventArgs e)
  {
    this.UpdateSelection();
  }

  protected virtual void ActiveElementChanged(object sender, ActiveElementChanged_EventArgs e)
  {
  }

  private void DocumentTreeViewDlg_KeyDown(object sender, KeyEventArgs e)
  {
  }

  private void DocumentTreeViewDlg_Closed(object sender, EventArgs e)
  {
    this.DocumentControl = (DocumentControl) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      this.TreeRoot = (ECOTreeItem) null;
      this.DocumentControl = (DocumentControl) null;
      this.Form = (ECOEditorForm) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (DocumentTreeViewDlg));
    this.menuBar1 = new MenuBar();
    this.elementContextMenu = new ContextMenuBarItem();
    this.treeList = new Intermech.VirtualTreeView.VirtualTreeView();
    this.columnElements = new Column();
    this.cellEditorTBe = new CellEditor();
    this.textBox1 = new TextBox();
    this.cellEditorCBe = new CellEditor();
    this.checkBox1 = new CheckBox();
    this.treeList.BeginInit();
    this.SuspendLayout();
    this.menuBar1.Guid = new Guid("eccc0fb4-9268-4b1b-bb4b-8e5bb1b01f00");
    this.menuBar1.Hidden = false;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.elementContextMenu
    });
    this.menuBar1.Location = new Point(0, 0);
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (System.Windows.Forms.Form) null;
    this.menuBar1.Size = new Size(240 /*0xF0*/, 22);
    this.menuBar1.TabIndex = 1;
    this.menuBar1.Text = "menuBar1";
    this.menuBar1.Visible = false;
    this.elementContextMenu.CommandName = "elementContextMenu";
    this.elementContextMenu.BeforePopup += new MenuItemBase.BeforePopupEventHandler(this.elementContextMenu_BeforePopup);
    this.elementContextMenu.AfterPopup += new EventHandler(this.elementContextMenu_AfterPopup);
    this.treeList.AllowDrop = true;
    this.treeList.AllowIndividualRowResize = false;
    this.treeList.AutoFitColumns = true;
    this.treeList.BackgroundImageMode = ImageDrawMode.Tile;
    this.treeList.BorderStyle = BorderStyle.Fixed3D;
    this.treeList.Columns.Add(this.columnElements);
    this.treeList.DisableHeaderContextMenu = false;
    this.treeList.Dock = DockStyle.Fill;
    this.treeList.Editors.Add(this.cellEditorTBe);
    this.treeList.Editors.Add(this.cellEditorCBe);
    this.treeList.LineStyle = LineStyle.Solid;
    this.treeList.Location = new Point(0, 22);
    this.treeList.MainColumn = this.columnElements;
    this.treeList.Name = "treeList";
    this.menuBar1.SetPopupMenu((Control) this.treeList, (MenuBarItem) this.elementContextMenu);
    this.treeList.RowStyle.ForeColor = SystemColors.WindowText;
    this.treeList.SelectBeforeEdit = true;
    this.treeList.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.treeList.ShowColumnHeaders = false;
    this.treeList.Size = new Size(240 /*0xF0*/, 271);
    this.treeList.TabIndex = 2;
    this.treeList.GetAllowedRowDropLocations += new GetAllowedRowDropLocationsHandler(this.treeList_GetAllowedRowDropLocations);
    this.treeList.DragEnter += new DragEventHandler(this.treeList_DragEnter);
    this.treeList.GetChildren += new GetChildrenHandler(this.treeList_GetChildren);
    this.treeList.GetCellData += new GetCellDataHandler(this.treeList_GetCellData);
    this.treeList.GetRowData += new GetRowDataHandler(this.treeList_GetRowData);
    this.treeList.RowDrop += new RowDropHandler(this.treeList_RowDrop);
    this.treeList.SelectionChanged += new EventHandler(this.treeList_SelectionChanged);
    this.treeList.SetCellValue += new SetCellValueHandler(this.treeList_SetCellValue);
    this.treeList.GetParent += new GetParentHandler(this.treeList_GetParent);
    this.treeList.MouseMove += new MouseEventHandler(this.treeList_MouseMove);
    this.treeList.GetRowDropEffect += new GetRowDropEffectHandler(this.treeList_GetRowDropEffect);
    this.treeList.MouseDown += new MouseEventHandler(this.treeList_MouseDown);
    this.treeList.DragOver += new DragEventHandler(this.treeList_DragOver);
    this.columnElements.AutoSizePolicy = ColumnAutoSizePolicy.Manual;
    this.columnElements.Caption = (string) null;
    this.columnElements.CellEvenStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.columnElements.CellOddStyle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.columnElements.CellStyle.ForeColor = SystemColors.WindowText;
    this.columnElements.Icon = (Icon) componentResourceManager.GetObject("columnElements.Icon");
    this.columnElements.Name = "columnElements";
    this.columnElements.SortDirection = ListSortDirection.Ascending;
    this.columnElements.Width = 236;
    this.cellEditorTBe.CellAlignment = ContentAlignment.MiddleLeft;
    this.cellEditorTBe.Control = (Control) this.textBox1;
    this.cellEditorTBe.DisplayMode = CellEditorDisplayMode.OnEdit;
    this.textBox1.Location = new Point(0, 0);
    this.textBox1.Name = "textBox1";
    this.textBox1.Size = new Size(100, 20);
    this.textBox1.TabIndex = 0;
    this.cellEditorCBe.CellAlignment = ContentAlignment.MiddleLeft;
    this.cellEditorCBe.Control = (Control) this.checkBox1;
    this.cellEditorCBe.DisplayMode = CellEditorDisplayMode.Always;
    this.cellEditorCBe.UseCellHeight = false;
    this.cellEditorCBe.UseCellWidth = false;
    this.checkBox1.FlatStyle = FlatStyle.System;
    this.checkBox1.Location = new Point(0, 0);
    this.checkBox1.Name = "checkBox1";
    this.checkBox1.Size = new Size(13, 13);
    this.checkBox1.TabIndex = 0;
    this.Controls.Add((Control) this.treeList);
    this.Controls.Add((Control) this.menuBar1);
    this.Name = "DocumentTreeViewDlg";
    this.Size = new Size(300, 293);
    this.Text = "Схема документа";
    this.Closed += new EventHandler(this.DocumentTreeViewDlg_Closed);
    this.KeyDown += new KeyEventHandler(this.DocumentTreeViewDlg_KeyDown);
    this.treeList.EndInit();
    this.ResumeLayout(false);
  }

  private void elementContextMenu_BeforePopup(object sender, MenuPopupEventArgs e)
  {
    if (this.treeList == null)
      return;
    DocumentTreeNode[] context = new DocumentTreeNode[0];
    EcoTreeViewDlg.TreeMenu = true;
    try
    {
      this.elementContextMenu.Items.Clear();
      NodeContextMenu.AddToContextMenu(this.elementContextMenu, this.DocumentControl.GetContexMenu(context));
    }
    finally
    {
      EcoTreeViewDlg.TreeMenu = false;
    }
  }

  private void elementContextMenu_AfterPopup(object sender, EventArgs e)
  {
    NodeContextMenu.ContextForContextMenu = (DocumentTreeNode[]) null;
    NodeContextMenu.ContextMenuCommand = false;
  }

  private TableData FindRow(DocumentTreeNode node)
  {
    for (DocumentTreeNode row = node; row != null; row = row.Parent)
    {
      if (row.ContainsAttribute(Intermech.ECO.Client.ECO.objectsAttr))
        return row as TableData;
    }
    return (TableData) null;
  }

  private void treeList_GetChildren(object sender, GetChildrenEventArgs e)
  {
    try
    {
      if (!this.Visible || e.Row.Item == null || !(e.Row.Item is ECOTreeItem))
        return;
      ECOTreeItem parentItem = e.Row.Item as ECOTreeItem;
      parentItem.ChildItems.Clear();
      List<ECOTreeItem> ecoTreeItemList = new List<ECOTreeItem>();
      if (parentItem.Node is ImDocumentData)
      {
        DocumentTreeNode documentTreeNode = parentItem.Node.FindFirstNodeFromTemplate("Содержание изменения") ?? parentItem.Node.FindFirstNodeByName("Содержание изменения");
        if (documentTreeNode is TableData)
        {
          foreach (RectangleElement node in documentTreeNode as TableData)
          {
            string attributeValue = node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true);
            string empty = string.Empty;
            string str1;
            if (attributeValue != string.Empty && this.ECOEditorForm.ECO != null)
            {
              str1 = this.ECOEditorForm.ECO.DesignListStr(this.ECOEditorForm.ECO._GetIdList(attributeValue));
            }
            else
            {
              string str2 = (string) null;
              if (node.FindFirstNodeFromTemplate_Recursive(Intermech.ECO.Client.ECO.idFldDesign) is TextData templateRecursive)
                str2 = templateRecursive.GetAttributeValue(Intermech.ECO.Client.ECO.textAttr, true);
              if (str2 == "" && templateRecursive.Text != "")
                str2 = templateRecursive.Text;
              str1 = str2;
            }
            if (str1 == null)
              str1 = "";
            string caption = str1.Replace("\u000E", " ");
            if (caption == "")
              caption = "Нет объекта";
            ECOTreeItem ecoTreeItem = new ECOTreeItem(0L, caption, (DocumentTreeNode) node);
            ecoTreeItem.ParentItem = parentItem;
            parentItem.ChildItems.Add(ecoTreeItem);
            ecoTreeItemList.Add(ecoTreeItem);
          }
          if (ECOPlugin.plugin.eps.Current.ShowHidden)
          {
            ECOTreeItem ecoTreeItem = new ECOTreeItem(0L, this.HiddedObjects, (DocumentTreeNode) null);
            ecoTreeItem.ParentItem = parentItem;
            parentItem.ChildItems.Add(ecoTreeItem);
            ecoTreeItemList.Add(ecoTreeItem);
          }
        }
      }
      if (parentItem.Caption == this.HiddedObjects && this.ECOEditorForm.ECO != null)
      {
        foreach (long hidingObject in this.ECOEditorForm.GetHidingObjects())
          ecoTreeItemList.Add(this.CreateECOTreeItem(hidingObject, parentItem));
      }
      if (parentItem.Node is TableData && this.ECOEditorForm.ECO != null)
      {
        foreach (long id in this.ECOEditorForm.ECO._GetIdList(parentItem.Node.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true)))
          ecoTreeItemList.Add(this.CreateECOTreeItem(id, parentItem));
      }
      e.Children = (IList) ecoTreeItemList;
    }
    catch
    {
    }
  }

  private ECOTreeItem CreateECOTreeItem(long id, ECOTreeItem parentItem)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      QuickObjectInfo quickObjectInfo = new QuickObjectInfo();
      quickObjectInfo.ObjectTypeID = -1;
      PendingLink anyLink = this.form.ECO.FindAnyLink(id);
      string caption;
      if (anyLink != null)
      {
        anyLink.UpdateObjType();
        quickObjectInfo.ObjectID = anyLink.verID;
        quickObjectInfo.ID = anyLink.ID;
        quickObjectInfo.ObjectTypeID = anyLink.objType;
        quickObjectInfo.Caption = anyLink.design;
        caption = quickObjectInfo.Caption;
      }
      else
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(id, false) ?? sessionKeeper.Session.GetObject(-id, false);
        if (dbObject != null)
        {
          caption = dbObject.Caption;
          if (caption == null || caption == string.Empty)
          {
            IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
            if (attributeByGuid != null)
              caption = attributeByGuid.AsString;
          }
        }
        else
          caption = "Объект не найден";
      }
      ECOTreeItem ecoTreeItem = new ECOTreeItem(id, caption, (DocumentTreeNode) null);
      ecoTreeItem.ParentItem = parentItem;
      ecoTreeItem.HidingType = this.ECOEditorForm.GetHidingType(quickObjectInfo.ObjectID);
      parentItem.ChildItems.Add(ecoTreeItem);
      ecoTreeItem.Info = quickObjectInfo;
      return ecoTreeItem;
    }
  }

  private void DocumentTreeViewDlg_ChildNodePositionChanged(
    object sender,
    ChildNodePositionChanged_EventArgs e)
  {
    if (sender is TableElement tableElement && tableElement.IsDistributing || !this.needUpdateTree)
      return;
    if (sender == null)
      throw new ArgumentNullException(nameof (sender));
    this.treeList.FindRow((object) (sender as DocumentTreeNode))?.UpdateChildren(true, false);
  }

  private void DocumentTreeViewDlg_VisibleChanged(object sender, VisibleChanged_EventArgs e)
  {
    if (sender is TableElement tableElement && tableElement.IsDistributing)
      return;
    Row row = sender != null ? this.treeList.FindRow((object) (sender as DocumentTreeNode)) : throw new ArgumentNullException(nameof (sender));
    if (row == null)
      return;
    this.treeList.UpdateRowData(row);
  }

  private void DocumentTreeViewDlg_NodeRemoved(object sender, Removed_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved), sender, (object) e);
    }
    else
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (e.Node == null)
        return;
      e.Node.ChildNodeAdded -= new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded);
      e.Node.ChildNodeRemoved -= new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved);
      e.Node.NameChanged -= new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged);
      e.Node.NodeRemoved -= new NodeRemoved_EventHandler(this.DocumentTreeViewDlg_NodeRemoved);
      e.Node.ChildNodePositionChanged -= new ChildNodePositionChanged_EventHandler(this.DocumentTreeViewDlg_ChildNodePositionChanged);
      if (!(e.Node is VisualNode))
        return;
      (e.Node as VisualNode).VisibleChanged -= new VisibleChanged_EventHandler(this.DocumentTreeViewDlg_VisibleChanged);
    }
  }

  private void DocumentTreeViewDlg_NameChanged(object sender, NameChanged_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new NameChanged_EventHandler(this.DocumentTreeViewDlg_NameChanged), sender, (object) e);
    }
    else
    {
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if ((sender as DocumentTreeNode).Parent == null)
        return;
      Row row = this.treeList.FindRow((object) (sender as DocumentTreeNode));
      if (row == null)
        return;
      this.treeList.UpdateRowData(row);
    }
  }

  private void DocumentTreeViewDlg_ChildNodeRemoved(object sender, ChildNode_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new ChildNodeRemoved_EventHandler(this.DocumentTreeViewDlg_ChildNodeRemoved), sender, (object) e);
    }
    else
    {
      switch (sender)
      {
        case null:
          throw new ArgumentNullException(nameof (sender));
        case DocumentTreeNode _:
          this.treeList.FindRow((object) (sender as DocumentTreeNode))?.UpdateChildren(true, true);
          break;
      }
    }
  }

  private void DocumentTreeViewDlg_ChildNodeAdded(object sender, ChildNode_EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new ChildNodeAdded_EventHandler(this.DocumentTreeViewDlg_ChildNodeAdded), sender, (object) e);
    }
    else
    {
      switch (sender)
      {
        case null:
          throw new ArgumentNullException(nameof (sender));
        case DocumentTreeNode _:
          Row row = this.treeList.FindRow((object) (sender as DocumentTreeNode));
          if (row == null || this.InvokeRequired || this.treeList.InvokeRequired)
            break;
          row.UpdateChildren(true, false);
          break;
      }
    }
  }

  private void treeList_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new GetCellDataHandler(this.treeList_GetCellData), sender, (object) e);
    }
    else
    {
      if (!this.Visible || e.Column != this.columnElements)
        return;
      ECOTreeItem ecoTreeItem = e.Row.Item as ECOTreeItem;
      if (ecoTreeItem.Id != 0L && ecoTreeItem.HidingType == HidingType.CanBeHidden)
      {
        e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle);
        e.CellData.EvenStyle.ForeColor = Color.Green;
        e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle);
        e.CellData.OddStyle.ForeColor = Color.Green;
      }
      e.CellData.Value = (object) ecoTreeItem.Caption;
    }
  }

  private void treeList_SelectionChanged(object sender, EventArgs e)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new EventHandler(this.treeList_SelectionChanged), sender, (object) e);
    }
    else
    {
      try
      {
        if (this.BlockUpdateSelection > 0 || this.DocumentControl == null)
          return;
        List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
        foreach (ECOTreeItem selectedItem in (IEnumerable) this.treeList.SelectedItems)
        {
          DocumentTreeNode documentTreeNode = (DocumentTreeNode) null;
          if (selectedItem.Node != null)
            documentTreeNode = selectedItem.Node;
          else if (selectedItem.ParentItem != null)
            documentTreeNode = selectedItem.ParentItem.Node;
          if (documentTreeNode != null)
          {
            foreach (DocumentTreeNode selectedNode in this.documentControl.SelectedNodes)
            {
              if (this.FindRow(selectedNode).GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, true) == documentTreeNode.GetAttributeValue(Intermech.ECO.Client.ECO.objectsAttr, false))
                documentTreeNode = (DocumentTreeNode) null;
            }
          }
          if (documentTreeNode != null)
            documentTreeNodeList.Add(documentTreeNode);
        }
        if (documentTreeNodeList.Count <= 0)
          return;
        ++this.BlockUpdateSelection;
        try
        {
          if ((this.DocumentControl.Document == null ? 0 : (this.DocumentControl.Document.HasLockedNodes((IList<DocumentTreeNode>) documentTreeNodeList) ? 1 : 0)) != 0)
          {
            if (documentTreeNodeList.Count == 1)
            {
              this.prevSelection = (List<DocumentTreeNode>) null;
              this.DocumentControl.SelectNode(documentTreeNodeList[0], false, Point.Empty);
            }
            else
            {
              this.SetSelection(this.prevSelection);
              this.DocumentControl.SetSelection(this.prevSelection, false, Point.Empty, true, false);
            }
          }
          else if (documentTreeNodeList.Count > 5)
            this.prevSelection = documentTreeNodeList;
          if (documentTreeNodeList != null && documentTreeNodeList.Count == 1)
            this.SetVisible(documentTreeNodeList[0]);
          this.DocumentControl.SetSelection(documentTreeNodeList, false, Point.Empty, true, false);
        }
        finally
        {
          --this.BlockUpdateSelection;
        }
      }
      catch
      {
      }
    }
  }

  private void SetVisible(DocumentTreeNode node)
  {
    if (node == null)
      return;
    if (node is TableData tableData && tableData.OwnerDocument != null && tableData.OwnerDocument.IsTemplate && !tableData.CloneByTemplateWithParent)
    {
      int num = tableData.OwnerDocument.Modified ? 1 : 0;
      tableData.SetVisible(true, false, true, true, false);
      if (num != 0)
        return;
      tableData.OwnerDocument.Modified = false;
    }
    else
      this.SetVisible(node.Parent);
  }

  private void treeList_GetParent(object sender, GetParentEventArgs e)
  {
    try
    {
      if (!this.Visible || e.Item == null || !(e.Item is ECOTreeItem))
        return;
      e.Parent = (object) (e.Item as ECOTreeItem).ParentItem;
    }
    catch
    {
    }
  }

  private void treeList_MouseDown(object sender, MouseEventArgs e)
  {
    Size dragSize = SystemInformation.DragSize;
    dragSize.Width += 5;
    dragSize.Height += 5;
    this.dragBoxFromMouseDown = new Rectangle(new Point(e.X - dragSize.Width / 2, e.Y - dragSize.Height / 2), dragSize);
  }

  private void treeList_GetRowDropEffect(object sender, GetRowDropEffectEventArgs e)
  {
    e.DropEffect = DragDropEffects.None;
    ECOTreeItem ecoTreeItem1 = e.Row.Item as ECOTreeItem;
    ECOTreeItem ecoTreeItem2 = (e.Data.GetData(typeof (RowSelectionList)) as RowSelectionList)[0].Item as ECOTreeItem;
    if (ecoTreeItem2.Info.Empty)
    {
      if (!ecoTreeItem1.Info.Empty || ecoTreeItem1.Node is ImDocumentData || ecoTreeItem1.Node == null)
        return;
    }
    else if (ecoTreeItem1.Node is ImDocumentData && ecoTreeItem2.HidingType != HidingType.Hidden || (ecoTreeItem1.Caption == this.HiddedObjects || ecoTreeItem1.ParentItem != null && ecoTreeItem1.ParentItem.Caption == this.HiddedObjects) && ecoTreeItem2.HidingType != HidingType.CanBeHidden)
      return;
    if (Control.ModifierKeys != Keys.Control)
      e.DropEffect = DragDropEffects.Move;
    else
      e.DropEffect = DragDropEffects.Copy;
  }

  private void treeList_DragOver(object sender, DragEventArgs e)
  {
    if (this.treeList.GetRowAt(e.X, e.Y) == null)
      e.Effect = DragDropEffects.None;
    else if (Control.ModifierKeys != Keys.Control)
      e.Effect = DragDropEffects.Move;
    else
      e.Effect = DragDropEffects.Copy;
  }

  private void treeList_GetAllowedRowDropLocations(
    object sender,
    GetAllowedRowDropLocationsEventArgs e)
  {
    e.AllowedDropLocations = RowDropLocation.OnRow;
  }

  public override Cursor Cursor
  {
    get => base.Cursor;
    set => base.Cursor = value;
  }

  private void treeList_MouseMove(object sender, MouseEventArgs e)
  {
    if (this.documentControl != null && this.documentControl.Document != null)
    {
      if (this.documentControl.Document.BackThreadIsActive && this.documentControl.HasSuspendedSelection)
      {
        if (this.Cursor != Cursors.AppStarting)
          this.Cursor = Cursors.AppStarting;
      }
      else if (this.Cursor != this.DefaultCursor)
        this.Cursor = this.DefaultCursor;
    }
    if (this.dragBoxFromMouseDown.Contains(e.Location.X, e.Location.Y) || this.form != null && this.form.ReadOnly || e.Button != MouseButtons.Left || this.treeList.SelectedItems == null)
      return;
    this.levelDrag = -1;
    Row[] rows = this.treeList.SelectedRows.GetRows();
    if (rows == null || rows.Length == 0)
      return;
    int level = rows[0].Level;
    HidingType hidingType = HidingType.CanBeHidden;
    if (rows[0].Item != null)
      hidingType = (rows[0].Item as ECOTreeItem).HidingType;
    for (int index = 0; index < rows.Length; ++index)
    {
      if (level != rows[index].Level)
        return;
      Row row = rows[index];
      if (!(rows[index].Item is ECOTreeItem ecoTreeItem) || ecoTreeItem.Node is ImDocumentData || ecoTreeItem.Caption == this.HiddedObjects || ecoTreeItem.HidingType != hidingType || ecoTreeItem.Id != 0L && ecoTreeItem.HidingType == HidingType.Disabled)
        return;
    }
    this.levelDrag = level;
    if (Control.ModifierKeys != Keys.Control)
    {
      int num1 = (int) this.treeList.DoDragDrop((object) this.treeList.SelectedRows, DragDropEffects.Move);
    }
    else
    {
      int num2 = (int) this.treeList.DoDragDrop((object) this.treeList.SelectedRows, DragDropEffects.Copy);
    }
  }

  private void treeList_RowDrop(object sender, RowDropEventArgs e)
  {
    if (!e.Data.GetDataPresent(typeof (RowSelectionList)))
      return;
    Row row = e.Row;
    if (row.Item == null)
      return;
    ECOTreeItem ecoTreeItem1 = row.Item as ECOTreeItem;
    RowSelectionList data = e.Data.GetData(typeof (RowSelectionList)) as RowSelectionList;
    this.needUpdateTree = false;
    for (int index1 = data.Count - 1; index1 >= 0; --index1)
    {
      if (data[index1].Item != null)
      {
        ECOTreeItem ecoTreeItem2 = data[index1].Item as ECOTreeItem;
        if (ecoTreeItem2.Info.Empty)
        {
          if (data[index1].Level == row.Level && ecoTreeItem1.Node != null)
          {
            int index2 = ecoTreeItem1.Node.Index;
            (ecoTreeItem2.Node as TableData).UniteTable();
            ecoTreeItem1.Node.Parent.InsertChildNode(index2, ecoTreeItem2.Node, false, true, false, false);
            ecoTreeItem1 = ecoTreeItem2;
          }
        }
        else
        {
          if ((ecoTreeItem1.Caption == this.HiddedObjects || ecoTreeItem1.ParentItem != null && ecoTreeItem1.ParentItem.Caption == this.HiddedObjects) && ecoTreeItem2.HidingType == HidingType.CanBeHidden && ecoTreeItem2.ParentItem != null && ecoTreeItem2.ParentItem.Node != null)
            this.ECOEditorForm.HideObject(ecoTreeItem2.Info.ObjectID, ecoTreeItem2.ParentItem.Node as TableElement);
          if (ecoTreeItem1.Node is ImDocumentData && ecoTreeItem2.HidingType == HidingType.Hidden)
            this.ECOEditorForm.UnhideObject(ecoTreeItem2.Info.ObjectID, (TableElement) null);
          if (!(ecoTreeItem1.Node is TableElement node) && ecoTreeItem1.ParentItem != null && ecoTreeItem1.ParentItem.Node is TableElement)
            node = ecoTreeItem1.ParentItem.Node as TableElement;
          if (node != null && ecoTreeItem2.HidingType == HidingType.Hidden)
            this.ECOEditorForm.UnhideObject(ecoTreeItem2.Info.ObjectID, node);
        }
      }
    }
    this.TreeRoot.Node.UpdateLayout(true);
    this.UpdateTree();
  }

  private void treeList_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!this.Visible || e.Column != this.columnElements)
      return;
    (e.Row.Item as DocumentTreeNode).Name = (string) e.NewValue;
  }

  private void treeList_DragEnter(object sender, DragEventArgs e)
  {
  }

  private void treeList_GetRowData(object sender, GetRowDataEventArgs e)
  {
    if (e.Row.Item == null || !(e.Row.Item is ECOTreeItem) || !(e.Row.Item is ECOTreeItem ecoTreeItem) || ecoTreeItem.Info.Empty)
      return;
    e.RowData.ImageSize = 32 /*0x20*/;
    e.RowData.Image = Images32x16_Cache.GetImage32x16(4, ecoTreeItem.Info.ObjectTypeID, (NavigatorTreeNode) null);
  }

  internal class IconHelper
  {
    public static Image GetSize(Icon icon)
    {
      Size size = new Size(16 /*0x10*/ * (icon.Width / icon.Height), 16 /*0x10*/);
      Image image = (Image) new Bitmap(32 /*0x20*/, 16 /*0x10*/);
      using (Graphics graphics = Graphics.FromImage(image))
      {
        graphics.DrawRectangle(Pens.Green, new Rectangle(0, 0, 31 /*0x1F*/, 15));
        graphics.DrawRectangle(Pens.Red, new Rectangle(0, 0, size.Width - 1, size.Height - 1));
        graphics.DrawIcon(icon, new Rectangle(0, 0, size.Width, size.Height));
      }
      return image;
    }
  }
}
