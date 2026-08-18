// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Output.UserControlSetupOutput
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.AVS.Properties;
using Intermech.Bars;
using Intermech.Controls;
using Intermech.Docking;
using Intermech.Docking.Rendering;
using Intermech.Document.Client;
using Intermech.Document.Model;
using Intermech.Document.RtfEditor;
using Intermech.Document.UI;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Document;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using Intermech.VirtualTreeView;
using NJFLib.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS.Output;

/// <summary> Описание класса UserControlSetupOutput </summary>
public class UserControlSetupOutput : ExtUserControl
{
  private bool preventReentering;
  private readonly SynchronizationContext synchronizationContext;
  private DockControl dockableControl;
  private DocumentContainer docContainer;
  private DockManager dockMan;
  private DockContainer leftDock;
  private DockContainer rightDock;
  private DockContainer bottomDock;
  private DockContainer topDock;
  private DocumentTreeViewDlg documentTreeViewDlg;
  private bool isFormB;
  private static List<int> objectTypeIds;
  private Column columnOutput;
  private static List<int> relTypeIds;
  private TreeNode treeModel = new TreeNode("root");
  private Style boldStyle;
  private CellEditor cellEditor;
  private TextBox txtCellEditorTextBox;
  private List<int> availableTypeList;
  private ToolTipController _editModeToolTip;
  public Button buttonReset;
  private Panel panelMainBody;
  private Panel panelMainBottom;
  private SplitContainer splitContainerMain;
  private SplitContainer splitContainerLeft;
  private Panel panelSysAttrs;
  private Label lblSysAttsHeader;
  private SelectAvsAttributeControl selectAttributeControl;
  protected ImageList imagesToolbars;
  protected Intermech.Bars.ToolBar toolBarLeft;
  protected ButtonItem btAdd;
  protected ButtonItem btAddAll;
  protected ButtonItem btDelete;
  protected ButtonItem btDeleteAll;
  private Panel panelMiddleBottom;
  private Panel panelMiddleTop;
  protected Intermech.Bars.ToolBar toolBarRight;
  protected ButtonItem btMoveTop;
  protected ButtonItem btMoveUp;
  protected ButtonItem btMoveDown;
  protected ButtonItem btMoveBottom;
  protected ButtonItem btInsert;
  protected ButtonItem btEdit;
  protected ButtonItem btRemove;
  private Panel panelOutputMappingTreeView;
  private EditableVirtualTreeView treeOutput;
  private Label labelOutputMapping;
  private Panel panelRight;
  private Panel panelRightBody;
  private DocumentControl docControl;
  private Panel panelRightTop;
  private Label labelDocumentHeader;
  private CollapsibleSplitter collapsibleSplitter;
  private Button buttonCancel;
  private Button buttonApply;
  private ContextMenuStrip contextMenuMain;
  private ToolStripMenuItem miDelimiterAdd;
  private ToolStripMenuItem miObjectTypeAdd;
  private ToolStripMenuItem miObjectTypeDel;
  private ContextMenuStrip contextMenuDelimiters;
  private ToolStripMenuItem toolStripMenuItem2;
  private ToolStripMenuItem toolStripMenuItem3;
  private ToolStripMenuItem toolStripMenuItem4;
  private ToolStripMenuItem miDelimiterEdit;
  private ToolStripMenuItem miAttributeOrDelimiterDel;
  private Panel panelPreview;
  private RichTextBox richTexPreview;
  private Panel panelPreviewLabel;
  private Label lblPreviewOutput;
  private ToolTipController _readModeToolTip;
  private IContainer components;
  private OutputAttributeMappingScheme _outputAttributeMappingScheme;
  private OutputAttributeMappingScheme _defaultOutputAttributeMappingScheme;
  private ImDocumentData document;
  private long templateObjectId = -1;
  private List<SpecificationSectionInfo> sections;
  private bool isCommonTemplateMode;

  public UserControlSetupOutput()
  {
    this.InitializeComponent();
    this.treeOutput.Visible = true;
    this.boldStyle = new Style(this.treeOutput.RowOddStyle, new StyleDelta()
    {
      Font = new Font(this.treeOutput.RowOddStyle.Font, FontStyle.Bold)
    });
    this.treeOutput.GetCellData += new GetCellDataHandler(this.treeOutput_GetCellData);
    this.treeOutput.GetChildren += new GetChildrenHandler(this.treeOutput_GetChildren);
    this.treeOutput.GetChildPolicy += new GetChildPolicyHandler(this.treeOutput_GetChildPolicy);
    this.treeOutput.MouseClick += new MouseEventHandler(this.HandleMappingTreeViewMouseActions);
    this.treeOutput.SelectionChanged += (EventHandler) ((s, e) => this.UpdateControls());
    this.cellEditor.InitializeControl += new CellEditorInitializeHandler(this.treeOutputTextEditor_InitializeControl);
    this.cellEditor.SetControlValue += new CellEditorSetValueHandler(this.treeOutputTextEditor_SetControlValue);
    this.treeOutput.BeforeShowCellEdit += new BeforeShowCellEditHandler(this.treeOutput_BeforeShowCellEdit);
    this.selectAttributeControl.IsCached = true;
    this.synchronizationContext = SynchronizationContext.Current;
    this.selectAttributeControl.AttributesTreeClicked += (MouseEventHandler) ((s, e) => this.UpdateControls());
    this.selectAttributeControl.AttributesTreeDoubleClicked += (EventHandler) ((s, e) => this.AddAttributeNode());
    this.miObjectTypeAdd.Click += new EventHandler(this.DoAddNewObjectTypeAction);
    this.miObjectTypeDel.Click += (EventHandler) ((s, e) => this.RemoveObjectType());
    this.miAttributeOrDelimiterDel.Click += (EventHandler) ((s, e) => this.RemoveAttributeOrDelimiterNode());
    this.BuildDelimiterMenus();
  }

  private void treeOutput_BeforeShowCellEdit(object sender, BeforeShowCellEditEventArgs e)
  {
    if (this.treeOutput.IsEditModeOn)
      return;
    e.Cancel = true;
  }

  private void treeOutputTextEditor_SetControlValue(object sender, CellEditorSetValueEventArgs e)
  {
    if (this.preventReentering || !this.treeOutput.IsEditModeOn)
      return;
    this.preventReentering = true;
    try
    {
      string text = (e.Control as TextBox).Text;
      string str = e.Value.ToString();
      if ((this.treeOutput.IsEditCommitRequested ? 0 : (!this.treeOutput.IsEditCancelRequested ? 1 : 0)) != 0 || !(e.CellWidget.Row.Item is DelimiterNode delimiterNode))
        return;
      Row row = e.CellWidget.Row;
      Row parentRow = row.ParentRow;
      e.CellWidget.CompleteEdit();
      if (str == "(пробел)" && this.treeOutput.IsEditCancelRequested)
      {
        this.treeOutput.IsEditModeOn = false;
        delimiterNode.Remove();
        this.treeOutput.SelectedRow = parentRow;
      }
      else
      {
        bool editCancelRequested = this.treeOutput.IsEditCancelRequested;
        this.treeOutput.IsEditModeOn = false;
        if (text != str)
        {
          delimiterNode.SetDelimiter(editCancelRequested ? delimiterNode.Text : text);
          if (delimiterNode.Parent is CellNode parent)
            parent.IsOverriden = true;
          this.treeOutput.SelectedRow = row;
          this.Changed = true;
        }
      }
      this.treeOutput.UpdateRowData(parentRow);
      parentRow.UpdateChildren(true, true);
      this.UpdatePreviewBox();
    }
    finally
    {
      this.preventReentering = false;
    }
  }

  private void treeOutputTextEditor_InitializeControl(
    object sender,
    CellEditorInitializeEventArgs e)
  {
    e.Control.Tag = (object) e.CellWidget;
    if (e.NewControl)
      e.Control.Validating += new CancelEventHandler(this.Control_Validating);
    (e.Control as TextBox).Text = "";
  }

  private void Control_Validating(object sender, CancelEventArgs e)
  {
    if (!this.treeOutput.IsEditModeOn)
      return;
    if (this.treeOutput.IsEditCancelRequested)
    {
      e.Cancel = false;
    }
    else
    {
      TextBox textBox = sender as TextBox;
      CellWidget tag = textBox.Tag as CellWidget;
      if (tag.Column != this.columnOutput || !(tag.Row.Item is DelimiterNode))
        return;
      if (!string.IsNullOrWhiteSpace(textBox.Text))
      {
        if (!((IEnumerable<string>) new string[9]
        {
          "\\_",
          "\\line",
          "\\~",
          "",
          " ",
          "-",
          "*",
          ".",
          ","
        }).Contains<string>(textBox.Text))
          e.Cancel = false;
        else
          e.Cancel = true;
      }
      else
      {
        if (!(textBox.Text == ""))
          return;
        e.Cancel = true;
        int num = (int) MessageBox.Show("Разделитель не может быть пустым.", "Неверный разделитель");
        textBox.Text = " ";
        tag.Row.ParentRow.UpdateChildren(true, true);
      }
    }
  }

  /// <summary> Инициализация формы </summary>
  internal void BuildTrees()
  {
    this.SuspendLayout();
    this.LoadMappingFromSchemeToVTree();
    Application.DoEvents();
    this.FillAvailableAttributes();
  }

  public void LoadMappingFromSchemeToVTree()
  {
    this.CreateMappingTreeModelFromScheme();
    this.treeOutput.DataSource = (object) this.treeModel;
    if (this.treeModel.Nodes.Count <= 0)
      return;
    this.treeOutput.RootRow.ChildRowByIndex(0).Selected = true;
    this.ExpandAllSectionNodesInVirtualTree();
  }

  public void LoadMappingTreeContent(IEnumerable<TreeNode> content)
  {
    this.treeModel.Nodes.Clear();
    this.treeModel.Nodes.AddRange(content.ToArray<TreeNode>());
    this.treeOutput.DataSource = (object) this.treeModel;
    this.treeOutput.RootRow.ChildRowByIndex(0).Selected = true;
    this.ExpandAllSectionNodesInVirtualTree();
  }

  public void ExpandMappingTreeFirstNode()
  {
    if (this.treeOutput.RootRow.ChildItems.Count <= 0)
      return;
    this.treeOutput.RootRow.ChildRowByIndex(0).Expanded = true;
  }

  private void ExpandAllSectionNodesInVirtualTree()
  {
    for (int childIndex1 = 0; childIndex1 < this.treeOutput.RootRow.ChildItems.Count; ++childIndex1)
    {
      Row row = this.treeOutput.RootRow.ChildRowByIndex(childIndex1);
      if (row.Item is SectionNode)
      {
        row.ExpandAncestors();
      }
      else
      {
        for (int childIndex2 = 0; childIndex2 < row.ChildItems.Count; ++childIndex2)
        {
          if (row.ChildRowByIndex(childIndex2).Item is SectionNode)
            row.ExpandAncestors();
        }
      }
    }
  }

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
      this.components?.Dispose();
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модифицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (UserControlSetupOutput));
    this._editModeToolTip = new ToolTipController(this.components);
    this._readModeToolTip = new ToolTipController(this.components);
    this.buttonReset = new Button();
    this.panelMainBody = new Panel();
    this.splitContainerMain = new SplitContainer();
    this.splitContainerLeft = new SplitContainer();
    this.panelSysAttrs = new Panel();
    this.toolBarLeft = new Intermech.Bars.ToolBar();
    this.imagesToolbars = new ImageList(this.components);
    this.btAdd = new ButtonItem();
    this.btDelete = new ButtonItem();
    this.selectAttributeControl = new SelectAvsAttributeControl();
    this.lblSysAttsHeader = new Label();
    this.panelMiddleBottom = new Panel();
    this.panelPreview = new Panel();
    this.richTexPreview = new RichTextBox();
    this.panelPreviewLabel = new Panel();
    this.lblPreviewOutput = new Label();
    this.panelMiddleTop = new Panel();
    this.panelOutputMappingTreeView = new Panel();
    this.treeOutput = new EditableVirtualTreeView();
    this.columnOutput = new Column();
    this.cellEditor = new CellEditor();
    this.txtCellEditorTextBox = new TextBox();
    this.toolBarRight = new Intermech.Bars.ToolBar();
    this.btMoveTop = new ButtonItem();
    this.btMoveUp = new ButtonItem();
    this.btMoveDown = new ButtonItem();
    this.btMoveBottom = new ButtonItem();
    this.btInsert = new ButtonItem();
    this.btEdit = new ButtonItem();
    this.btRemove = new ButtonItem();
    this.labelOutputMapping = new Label();
    this.collapsibleSplitter = new CollapsibleSplitter();
    this.panelRight = new Panel();
    this.panelRightBody = new Panel();
    this.docContainer = new DocumentContainer();
    this.rightDock = new DockContainer();
    this.dockMan = new DockManager();
    this.panelRightTop = new Panel();
    this.labelDocumentHeader = new Label();
    this.btAddAll = new ButtonItem();
    this.btDeleteAll = new ButtonItem();
    this.docControl = new DocumentControl();
    this.panelMainBottom = new Panel();
    this.buttonCancel = new Button();
    this.buttonApply = new Button();
    this.contextMenuMain = new ContextMenuStrip(this.components);
    this.miDelimiterAdd = new ToolStripMenuItem();
    this.contextMenuDelimiters = new ContextMenuStrip(this.components);
    this.toolStripMenuItem2 = new ToolStripMenuItem();
    this.toolStripMenuItem3 = new ToolStripMenuItem();
    this.toolStripMenuItem4 = new ToolStripMenuItem();
    this.miDelimiterEdit = new ToolStripMenuItem();
    this.miAttributeOrDelimiterDel = new ToolStripMenuItem();
    this.miObjectTypeAdd = new ToolStripMenuItem();
    this.miObjectTypeDel = new ToolStripMenuItem();
    this.leftDock = new DockContainer();
    this.bottomDock = new DockContainer();
    this.topDock = new DockContainer();
    this.panelMainBody.SuspendLayout();
    this.splitContainerMain.BeginInit();
    this.splitContainerMain.Panel1.SuspendLayout();
    this.splitContainerMain.Panel2.SuspendLayout();
    this.splitContainerMain.SuspendLayout();
    this.splitContainerLeft.BeginInit();
    this.splitContainerLeft.Panel1.SuspendLayout();
    this.splitContainerLeft.Panel2.SuspendLayout();
    this.splitContainerLeft.SuspendLayout();
    this.panelSysAttrs.SuspendLayout();
    this.panelMiddleBottom.SuspendLayout();
    this.panelPreview.SuspendLayout();
    this.panelPreviewLabel.SuspendLayout();
    this.panelMiddleTop.SuspendLayout();
    this.panelOutputMappingTreeView.SuspendLayout();
    this.treeOutput.BeginInit();
    this.panelRight.SuspendLayout();
    this.panelRightBody.SuspendLayout();
    this.panelRightTop.SuspendLayout();
    this.panelMainBottom.SuspendLayout();
    this.contextMenuMain.SuspendLayout();
    this.contextMenuDelimiters.SuspendLayout();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this.buttonReset.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.buttonReset.Enabled = false;
    this.buttonReset.FlatStyle = FlatStyle.System;
    this.buttonReset.Location = new Point(3, 9);
    this.buttonReset.Name = "buttonReset";
    this.buttonReset.Size = new Size(121, 27);
    this.buttonReset.TabIndex = 39;
    this.buttonReset.Text = "По умолчанию";
    this.buttonReset.Click += new EventHandler(this.buttonReset_Click);
    this.panelMainBody.Controls.Add((Control) this.splitContainerMain);
    this.panelMainBody.Dock = DockStyle.Fill;
    this.panelMainBody.Location = new Point(0, 0);
    this.panelMainBody.Name = "panelMainBody";
    this.panelMainBody.Padding = new Padding(0, 0, 0, 40);
    this.panelMainBody.Size = new Size(976, 565);
    this.panelMainBody.TabIndex = 40;
    this.splitContainerMain.Dock = DockStyle.Fill;
    this.splitContainerMain.Location = new Point(0, 0);
    this.splitContainerMain.Name = "splitContainerMain";
    this.splitContainerMain.Panel1.Controls.Add((Control) this.splitContainerLeft);
    this.splitContainerMain.Panel1.Controls.Add((Control) this.collapsibleSplitter);
    this.splitContainerMain.Panel1MinSize = 560;
    this.splitContainerMain.Panel2.Controls.Add((Control) this.panelRight);
    this.splitContainerMain.Size = new Size(976, 525);
    this.splitContainerMain.SplitterDistance = 560;
    this.splitContainerMain.TabIndex = 0;
    this.splitContainerLeft.Dock = DockStyle.Fill;
    this.splitContainerLeft.Location = new Point(0, 0);
    this.splitContainerLeft.Name = "splitContainerLeft";
    this.splitContainerLeft.Panel1.Controls.Add((Control) this.panelSysAttrs);
    this.splitContainerLeft.Panel2.Controls.Add((Control) this.panelMiddleBottom);
    this.splitContainerLeft.Panel2.Controls.Add((Control) this.panelMiddleTop);
    this.splitContainerLeft.Panel2MinSize = 270;
    this.splitContainerLeft.Size = new Size(557, 525);
    this.splitContainerLeft.SplitterDistance = 282;
    this.splitContainerLeft.TabIndex = 0;
    this.panelSysAttrs.Controls.Add((Control) this.toolBarLeft);
    this.panelSysAttrs.Controls.Add((Control) this.selectAttributeControl);
    this.panelSysAttrs.Controls.Add((Control) this.lblSysAttsHeader);
    this.panelSysAttrs.Dock = DockStyle.Fill;
    this.panelSysAttrs.Location = new Point(0, 0);
    this.panelSysAttrs.Name = "panelSysAttrs";
    this.panelSysAttrs.Size = new Size(282, 525);
    this.panelSysAttrs.TabIndex = 0;
    this.toolBarLeft.AddRemoveButtonsVisible = false;
    this.toolBarLeft.AllowHorizontalDock = false;
    this.toolBarLeft.Dock = DockStyle.Right;
    this.toolBarLeft.DockLine = 3;
    this.toolBarLeft.DrawActionsButton = false;
    this.toolBarLeft.Flow = ToolBarLayout.Vertical;
    this.toolBarLeft.FullMenus = true;
    this.toolBarLeft.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarLeft.Hidden = false;
    this.toolBarLeft.ImageList = this.imagesToolbars;
    this.toolBarLeft.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.btAdd,
      (ToolbarItemBase) this.btDelete
    });
    this.toolBarLeft.Location = new Point(258, 23);
    this.toolBarLeft.MinimumFloatingSize = new Size(250, 30);
    this.toolBarLeft.Name = "toolBarLeft";
    this.toolBarLeft.Overflow = ToolBarOverflow.Wrap;
    this.toolBarLeft.Size = new Size(24, 502);
    this.toolBarLeft.Stretch = true;
    this.toolBarLeft.TabIndex = 2;
    this.toolBarLeft.Tearable = false;
    this.toolBarLeft.Text = "";
    this.imagesToolbars.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbars.ImageStream");
    this.imagesToolbars.TransparentColor = Color.Transparent;
    this.imagesToolbars.Images.SetKeyName(0, "arrow_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(1, "arrow_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(2, "arrow_all_right_blue.ico");
    this.imagesToolbars.Images.SetKeyName(3, "arrow_all_left_blue.ico");
    this.imagesToolbars.Images.SetKeyName(4, "arrow_up_blue.ico");
    this.imagesToolbars.Images.SetKeyName(5, "arrow_down_blue.ico");
    this.imagesToolbars.Images.SetKeyName(6, "");
    this.imagesToolbars.Images.SetKeyName(7, "");
    this.imagesToolbars.Images.SetKeyName(8, "");
    this.imagesToolbars.Images.SetKeyName(9, "");
    this.imagesToolbars.Images.SetKeyName(10, "");
    this.btAdd.CommandName = "btAdd";
    this.btAdd.ImageIndex = 0;
    this.btAdd.ToolTipText = "Добавить указанный атрибут";
    this.btAdd.Click += new EventHandler(this.btAdd_Click);
    this.btDelete.CommandName = "btDelete";
    this.btDelete.ImageIndex = 1;
    this.btDelete.ToolTipText = "Убрать указанный атрибут";
    this.btDelete.Click += new EventHandler(this.btDelete_Click);
    this.selectAttributeControl.Dock = DockStyle.Fill;
    this.selectAttributeControl.Font = new Font("Tahoma", 8.25f);
    this.selectAttributeControl.Location = new Point(0, 23);
    this.selectAttributeControl.Name = "selectAttributeControl";
    this.selectAttributeControl.Padding = new Padding(5, 0, 25, 5);
    this.selectAttributeControl.Size = new Size(282, 502);
    this.selectAttributeControl.TabIndex = 1;
    this.selectAttributeControl.ViewType = ViewType.All;
    this.lblSysAttsHeader.AutoSize = true;
    this.lblSysAttsHeader.Dock = DockStyle.Top;
    this.lblSysAttsHeader.Location = new Point(0, 0);
    this.lblSysAttsHeader.Name = "lblSysAttsHeader";
    this.lblSysAttsHeader.Padding = new Padding(2, 5, 0, 5);
    this.lblSysAttsHeader.Size = new Size(117, 23);
    this.lblSysAttsHeader.TabIndex = 0;
    this.lblSysAttsHeader.Text = "Системные атрибуты";
    this.panelMiddleBottom.Controls.Add((Control) this.panelPreview);
    this.panelMiddleBottom.Controls.Add((Control) this.panelPreviewLabel);
    this.panelMiddleBottom.Dock = DockStyle.Bottom;
    this.panelMiddleBottom.Location = new Point(0, 405);
    this.panelMiddleBottom.Name = "panelMiddleBottom";
    this.panelMiddleBottom.Padding = new Padding(1, 7, 1, 5);
    this.panelMiddleBottom.Size = new Size(271, 120);
    this.panelMiddleBottom.TabIndex = 1;
    this.panelPreview.Controls.Add((Control) this.richTexPreview);
    this.panelPreview.Dock = DockStyle.Fill;
    this.panelPreview.Location = new Point(1, 32 /*0x20*/);
    this.panelPreview.Name = "panelPreview";
    this.panelPreview.Size = new Size(269, 83);
    this.panelPreview.TabIndex = 6;
    this.richTexPreview.BackColor = SystemColors.Info;
    this.richTexPreview.Dock = DockStyle.Fill;
    this.richTexPreview.Location = new Point(0, 0);
    this.richTexPreview.Margin = new Padding(3, 15, 3, 3);
    this.richTexPreview.Name = "richTexPreview";
    this.richTexPreview.ReadOnly = true;
    this.richTexPreview.Size = new Size(269, 83);
    this.richTexPreview.TabIndex = 6;
    this.richTexPreview.Text = "";
    this.panelPreviewLabel.Controls.Add((Control) this.lblPreviewOutput);
    this.panelPreviewLabel.Dock = DockStyle.Top;
    this.panelPreviewLabel.Location = new Point(1, 7);
    this.panelPreviewLabel.Name = "panelPreviewLabel";
    this.panelPreviewLabel.Size = new Size(269, 25);
    this.panelPreviewLabel.TabIndex = 3;
    this.lblPreviewOutput.AutoSize = true;
    this.lblPreviewOutput.Location = new Point(3, 3);
    this.lblPreviewOutput.Margin = new Padding(3);
    this.lblPreviewOutput.Name = "lblPreviewOutput";
    this.lblPreviewOutput.Size = new Size(152, 13);
    this.lblPreviewOutput.TabIndex = 2;
    this.lblPreviewOutput.Text = "Предварительный просмотр";
    this.panelMiddleTop.Controls.Add((Control) this.panelOutputMappingTreeView);
    this.panelMiddleTop.Controls.Add((Control) this.labelOutputMapping);
    this.panelMiddleTop.Dock = DockStyle.Fill;
    this.panelMiddleTop.Location = new Point(0, 0);
    this.panelMiddleTop.Name = "panelMiddleTop";
    this.panelMiddleTop.Padding = new Padding(0, 0, 0, 120);
    this.panelMiddleTop.Size = new Size(271, 525);
    this.panelMiddleTop.TabIndex = 0;
    this.panelOutputMappingTreeView.Controls.Add((Control) this.treeOutput);
    this.panelOutputMappingTreeView.Controls.Add((Control) this.toolBarRight);
    this.panelOutputMappingTreeView.Dock = DockStyle.Fill;
    this.panelOutputMappingTreeView.Location = new Point(0, 23);
    this.panelOutputMappingTreeView.Name = "panelOutputMappingTreeView";
    this.panelOutputMappingTreeView.Size = new Size(271, 382);
    this.panelOutputMappingTreeView.TabIndex = 4;
    this.treeOutput.AllowDrop = true;
    this.treeOutput.AllowMultiSelect = false;
    this.treeOutput.AllowUserPinnedColumns = false;
    this.treeOutput.Columns.Add(this.columnOutput);
    this.treeOutput.DisableHeaderContextMenu = true;
    this.treeOutput.Dock = DockStyle.Fill;
    this.treeOutput.Editors.Add(this.cellEditor);
    this.treeOutput.EnableRowCaching = false;
    this.treeOutput.ImageList = (ImageList) null;
    this.treeOutput.LineStyle = LineStyle.Dot;
    this.treeOutput.Location = new Point(0, 0);
    this.treeOutput.MainColumn = this.columnOutput;
    this.treeOutput.Name = "treeOutput";
    this.treeOutput.RowSelectedStyle.WordWrap = false;
    this.treeOutput.RowStyle.BorderColor = SystemColors.Control;
    this.treeOutput.RowStyle.BorderStyle = Border3DStyle.Flat;
    this.treeOutput.RowStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.treeOutput.RowStyle.WordWrap = false;
    this.treeOutput.SelectBeforeEdit = true;
    this.treeOutput.ShowRootRow = false;
    this.treeOutput.Size = new Size(247, 382);
    this.treeOutput.TabIndex = 1;
    this.columnOutput.AutoSizePolicy = ColumnAutoSizePolicy.AutoSize;
    this.columnOutput.Caption = "Схема вывода";
    this.columnOutput.CellEditor = this.cellEditor;
    this.columnOutput.CellStyle.BorderStyle = Border3DStyle.Flat;
    this.columnOutput.CellStyle.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204);
    this.columnOutput.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.columnOutput.MinWidth = 128 /*0x80*/;
    this.columnOutput.Movable = false;
    this.columnOutput.Name = "columnOutput";
    this.columnOutput.Sortable = false;
    this.columnOutput.Width = 128 /*0x80*/;
    this.cellEditor.Control = (Control) this.txtCellEditorTextBox;
    this.txtCellEditorTextBox.Location = new Point(0, 0);
    this.txtCellEditorTextBox.Name = "txtCellEditorTextBox";
    this.txtCellEditorTextBox.Size = new Size(40, 20);
    this.txtCellEditorTextBox.TabIndex = 0;
    this.toolBarRight.AddRemoveButtonsVisible = false;
    this.toolBarRight.AllowHorizontalDock = false;
    this.toolBarRight.Dock = DockStyle.Right;
    this.toolBarRight.DockLine = 3;
    this.toolBarRight.DrawActionsButton = false;
    this.toolBarRight.Flow = ToolBarLayout.Vertical;
    this.toolBarRight.FullMenus = true;
    this.toolBarRight.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarRight.Hidden = false;
    this.toolBarRight.ImageList = this.imagesToolbars;
    this.toolBarRight.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.btMoveTop,
      (ToolbarItemBase) this.btMoveUp,
      (ToolbarItemBase) this.btMoveDown,
      (ToolbarItemBase) this.btMoveBottom,
      (ToolbarItemBase) this.btInsert,
      (ToolbarItemBase) this.btEdit,
      (ToolbarItemBase) this.btRemove
    });
    this.toolBarRight.Location = new Point(247, 0);
    this.toolBarRight.MinimumFloatingSize = new Size(250, 30);
    this.toolBarRight.Name = "toolBarRight";
    this.toolBarRight.Overflow = ToolBarOverflow.Wrap;
    this.toolBarRight.Size = new Size(24, 382);
    this.toolBarRight.Stretch = true;
    this.toolBarRight.TabIndex = 2;
    this.toolBarRight.Tearable = false;
    this.toolBarRight.Text = "";
    this.btMoveTop.CommandName = "btMoveTop";
    this.btMoveTop.Image = (Image) componentResourceManager.GetObject("btMoveTop.Image");
    this.btMoveTop.ToolTipText = "Переместить элемент в начало списка";
    this.btMoveTop.Click += new EventHandler(this.btMoveTop_Click);
    this.btMoveUp.CommandName = "btMoveUp";
    this.btMoveUp.ImageIndex = 4;
    this.btMoveUp.ToolTipText = "Переместить элемент вверх";
    this.btMoveUp.Click += new EventHandler(this.btMoveUp_Click);
    this.btMoveDown.BeginGroup = true;
    this.btMoveDown.CommandName = "btMoveDown";
    this.btMoveDown.ImageIndex = 5;
    this.btMoveDown.ToolTipText = "Переместить элемент вниз";
    this.btMoveDown.Click += new EventHandler(this.btMoveDown_Click);
    this.btMoveBottom.CommandName = "btMoveBottom";
    this.btMoveBottom.Image = (Image) componentResourceManager.GetObject("btMoveBottom.Image");
    this.btMoveBottom.ToolTipText = "Переместить элемент в конец списка";
    this.btMoveBottom.Click += new EventHandler(this.btMoveBottom_Click);
    this.btInsert.BeginGroup = true;
    this.btInsert.CommandName = "btInsert";
    this.btInsert.Image = (Image) Resources.AddStandart;
    this.btInsert.ToolTipText = "Добавить";
    this.btInsert.Click += new EventHandler(this.btInsert_Click);
    this.btEdit.CommandName = "btEdit";
    this.btEdit.Image = (Image) Resources.EditStandart;
    this.btEdit.ToolTipText = "Изменить на";
    this.btEdit.Click += new EventHandler(this.btEdit_Click);
    this.btRemove.CommandName = "btRemove";
    this.btRemove.Image = (Image) Resources.DeleteStandart;
    this.btRemove.ToolTipText = "Удалить";
    this.btRemove.Click += new EventHandler(this.btRemove_Click);
    this.labelOutputMapping.AutoSize = true;
    this.labelOutputMapping.Dock = DockStyle.Top;
    this.labelOutputMapping.Location = new Point(0, 0);
    this.labelOutputMapping.Name = "labelOutputMapping";
    this.labelOutputMapping.Padding = new Padding(2, 5, 0, 5);
    this.labelOutputMapping.Size = new Size(148, 23);
    this.labelOutputMapping.TabIndex = 3;
    this.labelOutputMapping.Text = "Правила вывода атрибутов";
    this.collapsibleSplitter.AnimationDelay = 20;
    this.collapsibleSplitter.AnimationStep = 20;
    this.collapsibleSplitter.BorderStyle3D = Border3DStyle.Flat;
    this.collapsibleSplitter.ControlToHide = (Control) this.splitContainerMain.Panel2;
    this.collapsibleSplitter.Dock = DockStyle.Right;
    this.collapsibleSplitter.ExpandParentForm = true;
    this.collapsibleSplitter.Location = new Point(557, 0);
    this.collapsibleSplitter.Name = "collapsibleSplitter";
    this.collapsibleSplitter.TabIndex = 2;
    this.collapsibleSplitter.TabStop = false;
    this.collapsibleSplitter.UseAnimations = false;
    this.collapsibleSplitter.VisualStyle = VisualStyles.Mozilla;
    this.collapsibleSplitter.Click += new EventHandler(this.collapsibleSplitter_Click);
    this.panelRight.Controls.Add((Control) this.panelRightBody);
    this.panelRight.Controls.Add((Control) this.panelRightTop);
    this.panelRight.Dock = DockStyle.Fill;
    this.panelRight.Location = new Point(0, 0);
    this.panelRight.Name = "panelRight";
    this.panelRight.Size = new Size(412, 525);
    this.panelRight.TabIndex = 0;
    this.panelRightBody.Controls.Add((Control) this.docContainer);
    this.panelRightBody.Controls.Add((Control) this.rightDock);
    this.panelRightBody.Dock = DockStyle.Fill;
    this.panelRightBody.Location = new Point(0, 23);
    this.panelRightBody.Name = "panelRightBody";
    this.panelRightBody.Padding = new Padding(0, 0, 0, 5);
    this.panelRightBody.Size = new Size(412, 502);
    this.panelRightBody.TabIndex = 3;
    this.docContainer.Guid = new Guid("4a6f9fe6-921e-4fe6-8008-47f0a82f3729");
    this.docContainer.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.docContainer.Location = new Point(0, 0);
    this.docContainer.Manager = (DockManager) null;
    this.docContainer.Name = "docContainer";
    this.docContainer.Renderer = (RendererBase) null;
    this.docContainer.Size = new Size(412, 497);
    this.docContainer.TabIndex = 0;
    this.rightDock.Dock = DockStyle.Right;
    this.rightDock.Guid = new Guid("37aaea0e-a487-48ec-9df7-cf4b3b6444a0");
    this.rightDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.rightDock.Location = new Point(412, 0);
    this.rightDock.Manager = this.dockMan;
    this.rightDock.Name = "rightDock";
    this.rightDock.Renderer = (RendererBase) null;
    this.rightDock.Size = new Size(0, 497);
    this.rightDock.TabIndex = 43;
    this.dockMan.DocumentContainer = (DocumentContainer) null;
    this.dockMan.OwnerForm = (Form) null;
    this.panelRightTop.Controls.Add((Control) this.labelDocumentHeader);
    this.panelRightTop.Dock = DockStyle.Top;
    this.panelRightTop.Location = new Point(0, 0);
    this.panelRightTop.Name = "panelRightTop";
    this.panelRightTop.Size = new Size(412, 23);
    this.panelRightTop.TabIndex = 2;
    this.labelDocumentHeader.AutoSize = true;
    this.labelDocumentHeader.Dock = DockStyle.Fill;
    this.labelDocumentHeader.Location = new Point(0, 0);
    this.labelDocumentHeader.Name = "labelDocumentHeader";
    this.labelDocumentHeader.Padding = new Padding(2, 5, 0, 5);
    this.labelDocumentHeader.Size = new Size(105, 23);
    this.labelDocumentHeader.TabIndex = 2;
    this.labelDocumentHeader.Text = "Шаблон документа";
    this.btAddAll.CommandName = "btAddAll";
    this.btAddAll.ImageIndex = 2;
    this.btAddAll.ToolTipText = "Добавить все атрибуты";
    this.btDeleteAll.CommandName = "btDeleteAll";
    this.btDeleteAll.ImageIndex = 3;
    this.btDeleteAll.ToolTipText = "Убрать все атрибуты";
    this.docControl.ActivePage = (Page) null;
    this.docControl.Document = (ImDocument) null;
    this.docControl.DocumentManager = (IImDocumentManager) null;
    this.docControl.DocumentsComplect = (DocumentsComplect) null;
    this.docControl.DocumentViewMode = DocumentViewMode.Normal;
    this.docControl.IsElementCreating = false;
    this.docControl.IsElementSelecting = true;
    this.docControl.Location = new Point(60, 64 /*0x40*/);
    this.docControl.Name = "docControl";
    this.docControl.QueryCache_HasLockedNodes = false;
    this.docControl.ReadOnly = false;
    this.docControl.ReadOnlyGeometry = false;
    this.docControl.ReadOnlyGeometryForDocument = false;
    this.docControl.RowSelection = false;
    this.docControl.SelectedElementCreator = (PageElementCreator) null;
    this.docControl.Size = new Size(285, 280);
    this.docControl.TabIndex = 1;
    this.docControl.TernEditorBuffer = (ImRtfEditor) null;
    this.panelMainBottom.Controls.Add((Control) this.buttonCancel);
    this.panelMainBottom.Controls.Add((Control) this.buttonApply);
    this.panelMainBottom.Controls.Add((Control) this.buttonReset);
    this.panelMainBottom.Dock = DockStyle.Bottom;
    this.panelMainBottom.Location = new Point(0, 525);
    this.panelMainBottom.MaximumSize = new Size(2000, 40);
    this.panelMainBottom.Name = "panelMainBottom";
    this.panelMainBottom.Padding = new Padding(0, 3, 0, 0);
    this.panelMainBottom.Size = new Size(976, 40);
    this.panelMainBottom.TabIndex = 41;
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Enabled = false;
    this.buttonCancel.Location = new Point(830, 8);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(121, 27);
    this.buttonCancel.TabIndex = 41;
    this.buttonCancel.Text = "Отмена";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonCancel.Visible = false;
    this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
    this.buttonApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonApply.DialogResult = DialogResult.OK;
    this.buttonApply.Enabled = false;
    this.buttonApply.Location = new Point(705, 8);
    this.buttonApply.Name = "buttonApply";
    this.buttonApply.Size = new Size(121, 27);
    this.buttonApply.TabIndex = 40;
    this.buttonApply.Text = "Применить";
    this.buttonApply.UseVisualStyleBackColor = true;
    this.buttonApply.Visible = false;
    this.buttonApply.Click += new EventHandler(this.buttonApply_Click);
    this.contextMenuMain.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.miDelimiterAdd,
      (ToolStripItem) this.miDelimiterEdit,
      (ToolStripItem) this.miAttributeOrDelimiterDel,
      (ToolStripItem) this.miObjectTypeAdd,
      (ToolStripItem) this.miObjectTypeDel
    });
    this.contextMenuMain.Name = "contextMenuStripOutputMapping";
    this.contextMenuMain.Size = new Size(196, 114);
    this.miDelimiterAdd.DropDown = (ToolStripDropDown) this.contextMenuDelimiters;
    this.miDelimiterAdd.Name = "miDelimiterAdd";
    this.miDelimiterAdd.Size = new Size(195, 22);
    this.miDelimiterAdd.Text = "Добавить";
    this.contextMenuDelimiters.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.toolStripMenuItem3,
      (ToolStripItem) this.toolStripMenuItem4
    });
    this.contextMenuDelimiters.Name = "contextMenuDelimiters";
    this.contextMenuDelimiters.OwnerItem = (ToolStripItem) this.miDelimiterAdd;
    this.contextMenuDelimiters.Size = new Size(112 /*0x70*/, 70);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(111, 22);
    this.toolStripMenuItem2.Text = "Delim2";
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    this.toolStripMenuItem3.Size = new Size(111, 22);
    this.toolStripMenuItem3.Text = "Delim3";
    this.toolStripMenuItem4.Name = "toolStripMenuItem4";
    this.toolStripMenuItem4.Size = new Size(111, 22);
    this.toolStripMenuItem4.Text = "Delim1";
    this.miDelimiterEdit.Name = "miDelimiterEdit";
    this.miDelimiterEdit.Size = new Size(195, 22);
    this.miDelimiterEdit.Text = "Изменить на";
    this.miAttributeOrDelimiterDel.Name = "miAttributeOrDelimiterDel";
    this.miAttributeOrDelimiterDel.Size = new Size(195, 22);
    this.miAttributeOrDelimiterDel.Text = "Удалить";
    this.miObjectTypeAdd.Name = "miObjectTypeAdd";
    this.miObjectTypeAdd.Size = new Size(195, 22);
    this.miObjectTypeAdd.Text = "Добавить тип объекта";
    this.miObjectTypeDel.Name = "miObjectTypeDel";
    this.miObjectTypeDel.Size = new Size(195, 22);
    this.miObjectTypeDel.Text = "Удалить тип объекта";
    this.leftDock.Dock = DockStyle.Left;
    this.leftDock.Guid = new Guid("4495bce3-9ef7-42aa-b59c-8c6866d2dc3c");
    this.leftDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.leftDock.Location = new Point(0, 0);
    this.leftDock.Manager = this.dockMan;
    this.leftDock.Name = "leftDock";
    this.leftDock.Renderer = (RendererBase) null;
    this.leftDock.Size = new Size(0, 565);
    this.leftDock.TabIndex = 42;
    this.bottomDock.Dock = DockStyle.Bottom;
    this.bottomDock.Guid = new Guid("2a0ea927-c38e-4fc9-b6fd-4fe7909c62f4");
    this.bottomDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.bottomDock.Location = new Point(0, 565);
    this.bottomDock.Manager = this.dockMan;
    this.bottomDock.Name = "bottomDock";
    this.bottomDock.Renderer = (RendererBase) null;
    this.bottomDock.Size = new Size(976, 0);
    this.bottomDock.TabIndex = 44;
    this.topDock.Dock = DockStyle.Top;
    this.topDock.Guid = new Guid("54c75bb3-1a02-412b-a417-54593b30db3b");
    this.topDock.LayoutSystem = new SplitLayoutSystem(new SizeF(250f, 400f), Orientation.Horizontal, new LayoutSystemBase[0]);
    this.topDock.Location = new Point(0, 0);
    this.topDock.Manager = this.dockMan;
    this.topDock.Name = "topDock";
    this.topDock.Renderer = (RendererBase) null;
    this.topDock.Size = new Size(976, 0);
    this.topDock.TabIndex = 45;
    this.Controls.Add((Control) this.panelMainBottom);
    this.Controls.Add((Control) this.panelMainBody);
    this.Controls.Add((Control) this.leftDock);
    this.Controls.Add((Control) this.bottomDock);
    this.Controls.Add((Control) this.topDock);
    this.MinimumSize = new Size(615, 365);
    this.Name = nameof (UserControlSetupOutput);
    this.Size = new Size(976, 565);
    this.panelMainBody.ResumeLayout(false);
    this.splitContainerMain.Panel1.ResumeLayout(false);
    this.splitContainerMain.Panel2.ResumeLayout(false);
    this.splitContainerMain.EndInit();
    this.splitContainerMain.ResumeLayout(false);
    this.splitContainerLeft.Panel1.ResumeLayout(false);
    this.splitContainerLeft.Panel2.ResumeLayout(false);
    this.splitContainerLeft.EndInit();
    this.splitContainerLeft.ResumeLayout(false);
    this.panelSysAttrs.ResumeLayout(false);
    this.panelSysAttrs.PerformLayout();
    this.panelMiddleBottom.ResumeLayout(false);
    this.panelPreview.ResumeLayout(false);
    this.panelPreviewLabel.ResumeLayout(false);
    this.panelPreviewLabel.PerformLayout();
    this.panelMiddleTop.ResumeLayout(false);
    this.panelMiddleTop.PerformLayout();
    this.panelOutputMappingTreeView.ResumeLayout(false);
    this.treeOutput.EndInit();
    this.panelRight.ResumeLayout(false);
    this.panelRightBody.ResumeLayout(false);
    this.panelRightTop.ResumeLayout(false);
    this.panelRightTop.PerformLayout();
    this.panelMainBottom.ResumeLayout(false);
    this.contextMenuMain.ResumeLayout(false);
    this.contextMenuDelimiters.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public event MouseEventHandler OnActionButtonClicked;

  public event EventHandler OnDocumentOutlineVisibleChanged;

  internal Form OwnerForm
  {
    get => this.dockMan.OwnerForm;
    set => this.dockMan.OwnerForm = value;
  }

  protected DocumentTreeViewDlg DocumentTreeViewDlg
  {
    get
    {
      return this.documentTreeViewDlg != null && !this.documentTreeViewDlg.IsDisposed ? this.documentTreeViewDlg : (DocumentTreeViewDlg) null;
    }
  }

  /// <summary> Схема вывода атрибутов </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public OutputAttributeMappingScheme OutputAttributeMappingScheme
  {
    get => this._outputAttributeMappingScheme;
    set
    {
      this.LockControls();
      try
      {
        this._outputAttributeMappingScheme = value;
        if (this.document != null && this._outputAttributeMappingScheme != null)
          this.PrepareDefaultOutputTreeData();
        this.Changed = false;
        this.RefreshReadOnly();
        this.UpdateControls(true);
        this.RaiseOnInitDataEvent((object) this._outputAttributeMappingScheme);
        this.buttonReset.Text = value?.Parent == null ? "По умолчанию" : "Наследовать";
      }
      finally
      {
        this.UnlockControls();
      }
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long TemplateObjectId
  {
    get => this.templateObjectId;
    set
    {
      if (this.templateObjectId == value)
        return;
      this.templateObjectId = value;
      this.Document = (ImDocumentData) DocumentEditorPlugin.LoadDocumentFromDBObject(Math.Abs(this.templateObjectId));
    }
  }

  public bool ShowActionButtons
  {
    get => this.buttonApply.Visible && this.buttonCancel.Visible;
    set => this.buttonApply.Visible = this.buttonCancel.Visible = value;
  }

  /// <summary> Конструкторский документ (шаблон) </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ImDocumentData Document
  {
    get => this.document;
    set
    {
      this.LockControls();
      try
      {
        this.document = value;
        this.Changed = false;
        this.isFormB = ((int) this.document?.DBObjectCaption?.Contains(" Б") ?? 0) != 0;
        DockControl[] documents = this.docContainer.Documents;
        if (documents != null)
          ((IEnumerable<DockControl>) documents).AsList<DockControl>().ForEach((Action<DockControl>) (d => d.Close()));
        this.docControl.Document = (ImDocument) this.document;
        double num = (double) this.docControl.SetZoom(DocZoomMode.FitPage, 0.0f);
        this.docControl.PageControl.OnePage = true;
        this.docControl.ReadOnly = true;
        this.dockableControl = new DockControl((Control) this.docControl, this.document?.DBObjectCaption ?? "");
        this.dockableControl.Show(this.dockMan, DockState.Document);
        this.dockableControl.Closable = false;
        this.ShowDocumentTreeView();
        this.docContainer.Focus();
      }
      finally
      {
        this.UnlockControls();
        this.UpdateControls();
      }
    }
  }

  public bool ShowDocumentOutline
  {
    get => !this.splitContainerMain.Panel2Collapsed;
    set
    {
      if (value != this.splitContainerMain.Panel2Collapsed)
        return;
      this.splitContainerMain.Panel2Collapsed = !value;
    }
  }

  private void PrepareDefaultOutputTreeData()
  {
    SectionNode[] outputMappingTree = this.GetDefaultOutputMappingTree(this.document);
    this.treeModel.Nodes.Clear();
    this.treeModel.Nodes.AddRange((TreeNode[]) outputMappingTree);
  }

  private SectionNode[] GetDefaultOutputMappingTree(ImDocumentData document)
  {
    TableData table = document != null ? AVSDocument.FindAvsDocRow(document) : throw new ArgumentNullException(nameof (document));
    SectionNode[] outputMappingTree;
    if (table == null)
    {
      this.isCommonTemplateMode = true;
      outputMappingTree = this.GetCommonTemplateOutputMappingTree(this.templateObjectId);
    }
    else
    {
      List<SpecificationSectionInfo> documentSections = this.GetDocumentSections(document);
      List<SectionNode> list1 = documentSections != null ? documentSections.Select<SpecificationSectionInfo, SectionNode>((Func<SpecificationSectionInfo, SectionNode>) (s => new SectionNode(s))).ToList<SectionNode>() : (List<SectionNode>) null;
      list1.Insert(0, new SectionNode("*Все разделы"));
      List<CellOutputMapping> list2 = this.OutputAttributeMappingScheme.GetOverallMappingList().Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (m => m.SectionGuid.Equals("00000000-0000-0000-0000-000000000000") && m.ObjTypeGuid.Equals("00000000-0000-0000-0000-000000000000"))).ToList<CellOutputMapping>();
      bool flag1 = false;
      foreach (TextData cell in (IEnumerable<TextData>) new TextCellEnumerator(table))
      {
        bool flag2 = AVSRow.IsCountFormBCell(this.isFormB, cell);
        if (!(flag2 & flag1))
        {
          foreach (SectionNode sectionNode in list1)
          {
            CellNode cellNode = new CellNode(flag2 ? AVSRow.DocAttr_Count : cell.Id, flag2 ? AVSRow.DocAttr_Count : cell.Name);
            CellOutputMapping cellOutputMapping = list2.FirstOrDefault<CellOutputMapping>((Func<CellOutputMapping, bool>) (m => m.CellId == cellNode.Id));
            if (cellOutputMapping != null)
            {
              TreeNode[] array = cellOutputMapping.Items.OrderBy<OutputMappingBase, int>((Func<OutputMappingBase, int>) (im => im.Order)).Select<OutputMappingBase, TreeNode>((Func<OutputMappingBase, TreeNode>) (it =>
              {
                switch (it)
                {
                  case AttributeMapping attributeMapping2:
                    return (TreeNode) new AttributeNode(attributeMapping2.AttributeInfo);
                  case DelimiterMapping delimiterMapping2:
                    return (TreeNode) new DelimiterNode(delimiterMapping2.Delimiter, delimiterMapping2.Description);
                  default:
                    return (TreeNode) null;
                }
              })).Where<TreeNode>((Func<TreeNode, bool>) (n => n != null)).ToArray<TreeNode>();
              cellNode.Nodes.AddRange(array);
            }
            sectionNode.Nodes.Add((TreeNode) cellNode);
          }
          if (flag2)
            flag1 = true;
        }
      }
      outputMappingTree = list1.ToArray();
    }
    return outputMappingTree;
  }

  private SectionNode[] GetCommonTemplateOutputMappingTree(long commonTemplateId)
  {
    if (commonTemplateId == -1L)
      return new SectionNode[0];
    List<SectionNode> list1 = OutputAttributeMappingScheme.SectionInfos.Select<SpecificationSectionInfo, SectionNode>((Func<SpecificationSectionInfo, SectionNode>) (si => new SectionNode(si))).ToList<SectionNode>();
    list1.Insert(0, new SectionNode("*Все разделы"));
    List<CellOutputMapping> list2 = this.OutputAttributeMappingScheme.GetOverallMappingList().Where<CellOutputMapping>((Func<CellOutputMapping, bool>) (m => m.SectionGuid.Equals("00000000-0000-0000-0000-000000000000") && m.ObjTypeGuid.Equals("00000000-0000-0000-0000-000000000000"))).ToList<CellOutputMapping>();
    if (list2.Count == 0)
      list2 = ((IEnumerable<CellOutputMapping>) OutputAttributeMappingScheme.DefaultSpecificationCells).ToList<CellOutputMapping>();
    for (int index = 0; index < OutputAttributeMappingScheme.DefaultSpecificationCells.Length; ++index)
    {
      foreach (SectionNode sectionNode in list1)
      {
        CellNode cellNode = new CellNode(OutputAttributeMappingScheme.DefaultSpecificationCells[index].CellId, OutputAttributeMappingScheme.DefaultSpecificationCells[index].CellId);
        TreeNode[] array = (list2.FirstOrDefault<CellOutputMapping>((Func<CellOutputMapping, bool>) (m => m.CellId == cellNode.Id))?.Items ?? new List<OutputMappingBase>()).OrderBy<OutputMappingBase, int>((Func<OutputMappingBase, int>) (im => im.Order)).Select<OutputMappingBase, TreeNode>((Func<OutputMappingBase, TreeNode>) (it =>
        {
          switch (it)
          {
            case AttributeMapping attributeMapping2:
              return (TreeNode) new AttributeNode(attributeMapping2.AttributeInfo);
            case DelimiterMapping delimiterMapping2:
              return (TreeNode) new DelimiterNode(delimiterMapping2.Delimiter, delimiterMapping2.Description);
            default:
              return (TreeNode) null;
          }
        })).Where<TreeNode>((Func<TreeNode, bool>) (n => n != null)).ToArray<TreeNode>();
        cellNode.Nodes.AddRange(array);
        sectionNode.Nodes.Add((TreeNode) cellNode);
      }
    }
    return list1.ToArray();
  }

  /// <summary>Получить список разделов текущего шаблона</summary>
  private List<SpecificationSectionInfo> GetDocumentSections(ImDocumentData document)
  {
    if (document == null)
      return (List<SpecificationSectionInfo>) null;
    if (this.sections == null)
    {
      long dbObjectId = document.DBObjectID;
      this.sections = SpecificationSectionInfo.GetAllowableSpecSections(dbObjectId);
      if (this.sections == null)
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          this.sections = SpecificationSectionInfo.CacheSpecSections(sessionKeeper.Session, dbObjectId, new AVSDocumentType?());
      }
    }
    return this.sections;
  }

  private void BuildDelimiterMenus()
  {
    this.contextMenuDelimiters.Items.Clear();
    int num = 0;
    foreach (DelimiterMapping predefinedDelimiter in DelimiterMapping.PredefinedDelimiters)
    {
      ToolStripItemCollection items = this.contextMenuDelimiters.Items;
      ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
      toolStripMenuItem.Name = $"delimiter_{num++}";
      toolStripMenuItem.Size = new Size(180, 22);
      toolStripMenuItem.Text = predefinedDelimiter.Description;
      toolStripMenuItem.Tag = (object) predefinedDelimiter;
      items.Add((ToolStripItem) toolStripMenuItem);
    }
    ToolStripItemCollection items1 = this.contextMenuDelimiters.Items;
    ToolStripMenuItem toolStripMenuItem1 = new ToolStripMenuItem();
    toolStripMenuItem1.Name = "delimiter_Custom";
    toolStripMenuItem1.Size = new Size(180, 22);
    toolStripMenuItem1.Text = "Другой...";
    items1.Add((ToolStripItem) toolStripMenuItem1);
    this.contextMenuDelimiters.ItemClicked += (ToolStripItemClickedEventHandler) ((s, e) => this.DoDelimiterAction(s, e.ClickedItem));
    this.miDelimiterAdd.DropDown = (ToolStripDropDown) this.contextMenuDelimiters;
    this.miDelimiterEdit.DropDown = (ToolStripDropDown) this.contextMenuDelimiters;
  }

  private void DoDelimiterAction(object s, ToolStripItem clickedItem)
  {
    if (!(s is ContextMenuStrip contextMenuStrip))
      return;
    if (contextMenuStrip.OwnerItem != null)
    {
      if (contextMenuStrip.OwnerItem.Name == "miDelimiterAdd")
      {
        this.AddDelimiterNode(clickedItem.Tag);
      }
      else
      {
        if (!(contextMenuStrip.OwnerItem.Name == "miDelimiterEdit"))
          return;
        this.ChangeDelimiterNode(clickedItem.Tag);
      }
    }
    else if (contextMenuStrip.Tag is "A")
    {
      this.AddDelimiterNode(clickedItem.Tag);
    }
    else
    {
      if (!(contextMenuStrip.Tag is string tag) || !(tag == "E"))
        return;
      this.ChangeDelimiterNode(clickedItem.Tag);
    }
  }

  /// <summary>Инициализация дерева доступных атрибутов</summary>
  private void FillAvailableAttributes()
  {
    this.selectAttributeControl.CustomColumnSchemes = new List<AVSColumnScheme>()
    {
      (AVSColumnScheme) new AvsVirtualAttributeColumnsScheme((IEnumerable<AttributeInfo>) AvsIDCache.VirtualAttributes)
    };
    this.selectAttributeControl.Select((NodeColumnCollection) null, (List<AVSColumnScheme>) null);
    this.ResumeLayout();
  }

  private TreeNode SelectedOutputMappingTreeNode => this.treeOutput.SelectedRow?.Item as TreeNode;

  /// <summary> Обновление визуальных контролов </summary>
  protected override void UpdateControls()
  {
    if (this._editModeToolTip != null)
    {
      if (this.ReadOnly)
      {
        if (this._editModeToolTip.Active)
        {
          this._editModeToolTip.Active = false;
          this._readModeToolTip.Active = true;
        }
      }
      else if (this._readModeToolTip.Active)
      {
        this._readModeToolTip.Active = false;
        this._editModeToolTip.Active = true;
      }
    }
    this.btAdd.Enabled = !this.ReadOnly && this.selectAttributeControl.SelectedAttribute != null && (this.SelectedOutputMappingTreeNode is CellNode || this.SelectedOutputMappingTreeNode is DelimiterNode || this.SelectedOutputMappingTreeNode is AttributeNode);
    this.btAddAll.Enabled = this.btAdd.Enabled;
    this.btDeleteAll.Enabled = !this.ReadOnly && this.OutputTreeHasAttributeNodes();
    ButtonItem btMoveBottom = this.btMoveBottom;
    ButtonItem btMoveTop = this.btMoveTop;
    ButtonItem btMoveUp = this.btMoveUp;
    ButtonItem btMoveDown = this.btMoveDown;
    bool flag1;
    this.btDelete.Enabled = flag1 = !this.ReadOnly && this.SelectedOutputMappingTreeNode is AttributeNode || this.SelectedOutputMappingTreeNode is DelimiterNode;
    int num1;
    bool flag2 = (num1 = flag1 ? 1 : 0) != 0;
    btMoveDown.Enabled = num1 != 0;
    int num2;
    bool flag3 = (num2 = flag2 ? 1 : 0) != 0;
    btMoveUp.Enabled = num2 != 0;
    int num3;
    bool flag4 = (num3 = flag3 ? 1 : 0) != 0;
    btMoveTop.Enabled = num3 != 0;
    int num4 = flag4 ? 1 : 0;
    btMoveBottom.Enabled = num4 != 0;
    this.btInsert.Enabled = !this.ReadOnly && this.SelectedOutputMappingTreeNode != null;
    this.btRemove.Enabled = !this.ReadOnly && (this.SelectedOutputMappingTreeNode is DelimiterNode || this.SelectedOutputMappingTreeNode is AttributeNode || this.SelectedOutputMappingTreeNode is ObjTypeNode);
    this.btEdit.Enabled = !this.ReadOnly && this.SelectedOutputMappingTreeNode is DelimiterNode;
    this.buttonReset.Enabled = !this.ReadOnly;
    this.buttonApply.Enabled = this.buttonCancel.Enabled = !this.ReadOnly && this.Changed;
    this.UpdatePreviewBox();
    this.UpdateDocumentControlSelection();
  }

  private void UpdatePreviewBox()
  {
    if (this.SelectedOutputMappingTreeNode == null || this.SelectedOutputMappingTreeNode is SectionNode || this.SelectedOutputMappingTreeNode is ObjTypeNode)
    {
      this.richTexPreview.Text = "";
    }
    else
    {
      string str = "";
      TreeNode treeNode = this.SelectedOutputMappingTreeNode is CellNode ? this.SelectedOutputMappingTreeNode : (this.SelectedOutputMappingTreeNode.Parent is CellNode ? this.SelectedOutputMappingTreeNode.Parent : (TreeNode) null);
      if (treeNode != null)
      {
        foreach (object node in treeNode.Nodes)
          str += (string) node;
      }
      this.richTexPreview.Text = str;
      this.richTexPreview.Rtf = this.richTexPreview.Rtf.Replace("\\\\_", "\\_").Replace("\\\\line", "\\line").Replace("\\\\~", "\\~");
    }
  }

  /// <summary> В окне шаблона установить курсор на элемент </summary>
  private void UpdateDocumentControlSelection()
  {
    string str = "";
    TreeNode outputMappingTreeNode = this.SelectedOutputMappingTreeNode;
    if (outputMappingTreeNode is CellNode cellNode)
      str = cellNode.Id;
    if (outputMappingTreeNode is AttributeNode || outputMappingTreeNode is DelimiterNode)
      str = (outputMappingTreeNode.Parent is CellNode parent ? parent.Id : (string) null) ?? "";
    if (!string.IsNullOrWhiteSpace(str) && this.document != null && this.docControl != null)
    {
      DocumentTreeNode selection = this.document.FindNode(str) ?? this.document.FindFirstNodeByName(str);
      if (selection != null)
      {
        List<DocumentTreeNode> documentTreeNodeList = new List<DocumentTreeNode>();
        this.document.FindNodes((FindCondition) ((n, cval) => n.Name == (string) cval || n.Id == (string) cval), (object) str, documentTreeNodeList);
        if (documentTreeNodeList.Count > 0)
          this.docControl.SetSelection(documentTreeNodeList, false, new Point(0, 0), true, false);
        else
          this.docControl.SetSelection(selection, false, new Point(0, 0), true, false);
        this.docControl.ResetTernBufer();
        this.documentTreeViewDlg.UpdateSelection();
      }
      else
      {
        if (selection != null || this.isCommonTemplateMode)
          return;
        int num = (int) MessageBox.Show($"В шаблоне не найдена графа '{str}'!", "Ошибка");
      }
    }
    else
    {
      if (this.document == null)
        return;
      this.docControl?.SetSelection((DocumentTreeNode) null, false, new Point(0, 0), true, false);
    }
  }

  private bool OutputTreeHasAttributeNodes()
  {
    foreach (TreeNode node1 in this.treeModel.Nodes)
    {
      if (node1.Nodes.Count > 0 && node1.Nodes[0] is CellNode)
      {
        foreach (TreeNode node2 in node1.Nodes)
        {
          foreach (TreeNode node3 in node2.Nodes)
          {
            if (node3 is AttributeNode)
              return true;
          }
        }
      }
      else
      {
        foreach (TreeNode node4 in node1.Nodes)
        {
          foreach (TreeNode node5 in node4.Nodes)
          {
            foreach (TreeNode node6 in node5.Nodes)
            {
              if (node6 is AttributeNode)
                return true;
            }
          }
        }
      }
    }
    return false;
  }

  /// <summary> Проверка, должно ли быть доступно редактирование </summary>
  public override bool GetIsReadOnly()
  {
    return this._outputAttributeMappingScheme == null || this._outputAttributeMappingScheme.ReadOnly;
  }

  /// <summary>
  /// Добавить выделенный в дереве доступных атрибут в схему вывода
  /// </summary>
  private void AddAttributeNode()
  {
    if (!this.CanChangeMapping() || this.selectAttributeControl.SelectedAttribute == null)
      return;
    AttributeInfo selectedAttribute = this.selectAttributeControl.SelectedAttribute;
    bool isDocRowAttribute = this.selectAttributeControl.SelectedScheme is DocFieldsColumnsScheme;
    Row selectedRow = this.treeOutput.SelectedRow;
    if (selectedAttribute == null || selectedRow == null)
      return;
    Row row = selectedRow.Item is CellNode ? selectedRow : (selectedRow.ParentRow?.Item is CellNode ? selectedRow?.ParentRow : (Row) null);
    if (!(row.Item is CellNode cellNode))
      return;
    try
    {
      AttributeNode node = new AttributeNode(selectedAttribute, isDocRowAttribute);
      cellNode.Nodes.Add((TreeNode) node);
      int index = node.Index;
      this.Changed = true;
      cellNode.IsOverriden = true;
      row.UpdateChildren(true, true);
      this.treeOutput.SelectedRow = row.ChildRowByIndex(index);
    }
    finally
    {
      this.UpdateControls();
      this.UpdatePreviewBox();
    }
  }

  private void RemoveAttributeOrDelimiterNode()
  {
    if (!this.CanChangeMapping())
      return;
    TreeNode outputMappingTreeNode = this.SelectedOutputMappingTreeNode;
    switch (outputMappingTreeNode)
    {
      case AttributeNode _:
      case DelimiterNode _:
        Row parentRow = this.treeOutput.SelectedRow.ParentRow;
        CellNode parent1 = outputMappingTreeNode.Parent as CellNode;
        try
        {
          outputMappingTreeNode.Remove();
          if (parent1.Nodes.Count > 0)
          {
            parent1.IsOverriden = true;
          }
          else
          {
            int num = (int) IMMessageBox.Show("Настройка вывода для ячейки.", "Список настроек вывода для ячейки пуст.\nВы хотите загрузить унаследованные настройки?", MessageBoxButtons.YesNo);
            SectionNode s = (parent1.Parent is ObjTypeNode parent2 ? parent2.Parent : parent1.Parent) as SectionNode;
            ObjTypeNode o = parent1.Parent is ObjTypeNode parent3 ? parent3 : ObjTypeNode.Default;
            if (num == 6)
            {
              parent1.IsOverriden = false;
              this.UpdateCellNodeFromScheme(s, parent1, o, true);
            }
            else
            {
              CellOutputMapping newCellMaping = new CellOutputMapping()
              {
                SectionGuid = s?.SectionGuid ?? Guid.Empty.ToString(),
                ObjTypeGuid = o.ObjectTypeGuid,
                CellId = parent1.Id
              };
              newCellMaping.Add((OutputMappingBase) DelimiterMapping.EmptyStub);
              this._outputAttributeMappingScheme.SetCellMapping(newCellMaping);
              this._outputAttributeMappingScheme.UpdateXml();
            }
          }
        }
        finally
        {
          this.treeOutput.UpdateRowData(parentRow);
          parentRow.UpdateChildren(true, true);
        }
        this.Changed = true;
        this.UpdatePreviewBox();
        break;
    }
  }

  private void AddDelimiterNode(object delimiterObject)
  {
    if (!this.CanChangeMapping() || this.SelectedOutputMappingTreeNode == null)
      return;
    string str1;
    switch (delimiterObject)
    {
      case string str2:
        str1 = str2;
        break;
      case DelimiterMapping delimiterMapping:
        str1 = delimiterMapping.Delimiter;
        break;
      default:
        str1 = (string) null;
        break;
    }
    string delimiter = str1;
    this.treeOutput.IsEditModeOn = delimiter == null;
    if (this.treeOutput.IsEditModeOn)
      delimiter = DelimiterMapping.Default.Delimiter;
    DelimiterNode node = new DelimiterNode(delimiter);
    Row row = (Row) null;
    if (this.SelectedOutputMappingTreeNode is CellNode outputMappingTreeNode)
    {
      outputMappingTreeNode.Nodes.Add((TreeNode) node);
      outputMappingTreeNode.IsOverriden = true;
      this.treeOutput.UpdateRowData(this.treeOutput.SelectedRow);
      this.treeOutput.SelectedRow.UpdateChildren(true, true);
      row = this.treeOutput.SelectedRow.ChildRowByIndex(node.Index);
    }
    else
    {
      Row parentRow = this.treeOutput.SelectedRow.ParentRow;
      int index = this.SelectedOutputMappingTreeNode.Index;
      if (parentRow.Item is CellNode cellNode)
      {
        if (index < cellNode.Nodes.Count - 1)
          cellNode.Nodes.Insert(this.SelectedOutputMappingTreeNode.Index + 1, (TreeNode) node);
        else
          cellNode.Nodes.Add((TreeNode) node);
        cellNode.IsOverriden = true;
        this.treeOutput.UpdateRowData(parentRow);
        parentRow.UpdateChildren(true, true);
        row = parentRow.ChildRowByIndex(node.Index);
      }
    }
    if (row != null)
      this.treeOutput.SelectedRow = row;
    this.Changed = true;
    this.UpdatePreviewBox();
    if (!this.treeOutput.IsEditModeOn)
      return;
    this.SelectedOutputMappingTreeNode.Text = this.SelectedOutputMappingTreeNode.Text.Replace("Разделитель: ", "");
    this.treeOutput.UpdateRowData(row);
    this.treeOutput.SelectBeforeEdit = false;
    this.treeOutput.EditFirstCellInFocusRow();
  }

  private void ChangeDelimiterNode(object delimiterObject)
  {
    if (!this.CanChangeMapping() || !(this.SelectedOutputMappingTreeNode is DelimiterNode outputMappingTreeNode))
      return;
    string str = delimiterObject as string;
    DelimiterMapping delimiterMapping = delimiterObject as DelimiterMapping;
    if (str != null || delimiterMapping != null)
    {
      string delimiter = delimiterMapping?.Delimiter ?? str;
      string description = delimiterMapping?.Description;
      outputMappingTreeNode.SetDelimiter(delimiter, description);
      this.OnCellOutputChanged((object) this.treeOutput.SelectedRow);
    }
    else
    {
      this.SelectedOutputMappingTreeNode.Text = this.SelectedOutputMappingTreeNode.Text.Replace("Разделитель: ", "");
      this.treeOutput.IsEditModeOn = true;
      this.treeOutput.EditFirstCellInFocusRow();
    }
  }

  private void OnCellOutputChanged(object treeObject)
  {
    if (treeObject is Row row1)
    {
      Row row = row1.Item is CellNode ? row1 : (row1.ParentRow.Item is CellNode ? row1.ParentRow : (Row) null);
      if (row != null)
      {
        (row.Item as CellNode).IsOverriden = true;
        this.treeOutput.UpdateRowData(row);
        row.UpdateChildren(true, true);
        this.treeOutput.SelectedRow = row1;
      }
    }
    this.Changed = true;
    this.UpdatePreviewBox();
  }

  /// <summary>
  /// Добавляет в дерево вывода новый узел по выбранному типу объекта
  /// </summary>
  private ObjTypeNode AddObjectType(string objTypeGuid = null, SectionNode parentNode = null, bool changeMapping = true)
  {
    if (changeMapping && !this.CanChangeMapping())
      return (ObjTypeNode) null;
    if (!changeMapping && (string.IsNullOrWhiteSpace(objTypeGuid) || parentNode == null))
      return (ObjTypeNode) null;
    if (parentNode == null)
      parentNode = this.SelectedOutputMappingTreeNode is SectionNode outputMappingTreeNode ? outputMappingTreeNode : this.SelectedOutputMappingTreeNode?.Parent as SectionNode;
    if (parentNode == null)
      return (ObjTypeNode) null;
    ObjTypeNode newNode = string.IsNullOrWhiteSpace(objTypeGuid) ? this.SelectObjectTypeFromList(parentNode.SectionGuid).FirstOrDefault<ObjTypeNode>() : new ObjTypeNode(Guid.Parse(objTypeGuid));
    if (newNode == null)
      return (ObjTypeNode) null;
    TreeNode treeNode = parentNode.Nodes.OfType<TreeNode>().FirstOrDefault<TreeNode>((Func<TreeNode, bool>) (n => n is ObjTypeNode objTypeNode && objTypeNode.ObjectTypeGuid.Equals(newNode.ObjectTypeGuid, StringComparison.Ordinal)));
    if (treeNode != null)
      return treeNode as ObjTypeNode;
    List<CellNode> typeSectionNodes = this.GetDefaultObjTypeSectionNodes(parentNode);
    if (typeSectionNodes != null)
    {
      foreach (CellNode cellNode in typeSectionNodes)
        newNode.Nodes.Add((TreeNode) cellNode.Clone());
    }
    this.AddDefaultObjectTypeNode(parentNode);
    if (parentNode.Nodes[0] is ObjTypeNode)
    {
      bool flag = false;
      foreach (TreeNode node in parentNode.Nodes)
      {
        if (newNode.CompareTo((object) node) <= 0)
        {
          int index = parentNode.Nodes.IndexOf(node);
          parentNode.Nodes.Insert(index, (TreeNode) newNode);
          flag = true;
        }
      }
      if (!flag)
        parentNode.Nodes.Add((TreeNode) newNode);
    }
    return newNode;
  }

  /// <summary>
  /// Сделать из "плоской" структуры подузлов ячеек узла раздела двумерную структру 'раздел/тип объекта/ячейка'
  /// </summary>
  private void AddDefaultObjectTypeNode(SectionNode rootNode)
  {
    if (!(rootNode?.Nodes[0] is CellNode))
      return;
    ObjTypeNode node1 = ObjTypeNode.Default;
    foreach (TreeNode node2 in rootNode.Nodes)
      node1.Nodes.Add((TreeNode) node2.Clone());
    rootNode.Nodes.Clear();
    rootNode.Nodes.Add((TreeNode) node1);
  }

  private List<CellNode> GetDefaultObjTypeSectionNodes(SectionNode rootNode)
  {
    if (rootNode.Nodes.Count == 0)
      return (List<CellNode>) null;
    if (rootNode.Nodes[0] is CellNode)
      return rootNode.Nodes.OfType<CellNode>().ToList<CellNode>();
    if (rootNode.Nodes[0] is ObjTypeNode)
    {
      ObjTypeNode objTypeNode = rootNode.Nodes.OfType<ObjTypeNode>().FirstOrDefault<ObjTypeNode>((Func<ObjTypeNode, bool>) (n => n.ObjectTypeGuid.Equals(Guid.Empty.ToString(), StringComparison.Ordinal)));
      if (objTypeNode != null)
        return objTypeNode.Nodes.OfType<CellNode>().ToList<CellNode>();
    }
    return (List<CellNode>) null;
  }

  private void RemoveObjectType(ObjTypeNode nodeToRemove = null)
  {
    if (!this.CanChangeMapping())
      return;
    nodeToRemove = nodeToRemove ?? this.SelectedOutputMappingTreeNode as ObjTypeNode;
    if (nodeToRemove == null || nodeToRemove.ObjectTypeGuid == ObjTypeNode.Default.ObjectTypeGuid || !(nodeToRemove.Parent is SectionNode parent))
      return;
    foreach (CellNode node in nodeToRemove.Nodes)
    {
      CellOutputMapping mapping = CellOutputMapping.FromNode(node);
      if (!this._outputAttributeMappingScheme.IsDefinedOnCurrentLevel(mapping))
        this._outputAttributeMappingScheme.SetCellMapping(mapping.Hide());
      else if (!mapping.IsHidden)
        this._outputAttributeMappingScheme.SetCellMapping(mapping.SectionGuid, mapping.ObjTypeGuid, mapping.CellId, (CellOutputMapping) null);
    }
    parent.Nodes.Remove((TreeNode) nodeToRemove);
    if (parent.Nodes.Count == 1)
      this.RemoveDefaultObjectTypeNode(parent);
    Row row = this.treeOutput.FindRow((object) parent);
    if (row != null)
    {
      this.treeOutput.UpdateRowData(row);
      row.UpdateChildren(true, true);
      this.treeOutput.SelectedRow = row;
    }
    this.Changed = true;
    this.UpdatePreviewBox();
  }

  private void RemoveDefaultObjectTypeNode(SectionNode rootNode)
  {
    if (!(rootNode?.Nodes[0] is ObjTypeNode node1))
      return;
    foreach (TreeNode node2 in node1.Nodes)
      rootNode.Nodes.Add(node2.Clone() as TreeNode);
    node1.Remove();
  }

  /// <summary>
  /// Загрузить данные из схемы в дерево - версия с VirtualTreeView
  /// </summary>
  private void CreateMappingTreeModelFromScheme()
  {
    if (this._outputAttributeMappingScheme == null)
      return;
    foreach (object node1 in this.treeModel.Nodes)
    {
      if (node1 is SectionNode sectionNode)
      {
        string[] objectTypesForSection = this._outputAttributeMappingScheme.GetObjectTypesForSection(sectionNode.SectionGuid);
        foreach (TreeNode treeNode in sectionNode.Nodes.OfType<TreeNode>().ToList<TreeNode>())
        {
          if (treeNode is ObjTypeNode objTypeNode)
          {
            if (!objTypeNode.IsDefault && !((IEnumerable<string>) objectTypesForSection).Contains<string>(objTypeNode.ObjectTypeGuid))
            {
              this.RemoveObjectType(objTypeNode);
            }
            else
            {
              foreach (object node2 in objTypeNode.Nodes)
              {
                if (node2 is CellNode c)
                  this.UpdateCellNodeFromScheme(sectionNode, c, objTypeNode);
              }
            }
          }
          else if (treeNode is CellNode c1)
            this.UpdateCellNodeFromScheme(sectionNode, c1, ObjTypeNode.Default);
        }
        if (objectTypesForSection.Length > 1)
        {
          foreach (string objTypeGuid in ((IEnumerable<string>) objectTypesForSection).Where<string>((Func<string, bool>) (id => !id.Equals(ObjTypeNode.Default.ObjectTypeGuid, StringComparison.Ordinal))))
          {
            ObjTypeNode o = this.AddObjectType(objTypeGuid, sectionNode, false);
            if (o != null)
            {
              foreach (object node3 in o.Nodes)
              {
                if (node3 is CellNode c)
                  this.UpdateCellNodeFromScheme(sectionNode, c, o);
              }
            }
          }
        }
      }
      else if (node1 is CellNode c2)
        this.UpdateCellNodeFromScheme(new SectionNode(""), c2, ObjTypeNode.Default);
    }
  }

  private void UpdateCellNodeFromScheme(
    SectionNode s,
    CellNode c,
    ObjTypeNode o,
    bool forceInherit = false)
  {
    if (c == null)
      return;
    Guid empty;
    string sectionGuid1;
    if (!forceInherit)
    {
      sectionGuid1 = s.SectionGuid;
    }
    else
    {
      empty = Guid.Empty;
      sectionGuid1 = empty.ToString();
    }
    string sectionGuid2 = sectionGuid1;
    string objectTypeGuid = o?.ObjectTypeGuid;
    if (objectTypeGuid == null)
    {
      empty = Guid.Empty;
      objectTypeGuid = empty.ToString();
    }
    string objTypeGuid = objectTypeGuid;
    CellOutputMapping cellMapping = this._outputAttributeMappingScheme.GetCellMapping(sectionGuid2, c.Id, objTypeGuid);
    bool flag = !forceInherit && this._outputAttributeMappingScheme.IsDefinedOnCurrentLevel(cellMapping);
    if (flag && s != null)
      s.IsOverriden = true;
    if (cellMapping == null)
      return;
    c.Nodes.Clear();
    if (!cellMapping.HasBlankOutput && !cellMapping.IsEmpty)
    {
      foreach (OutputMappingBase outputMappingBase in cellMapping.Items)
      {
        if (outputMappingBase is AttributeMapping attributeMapping)
        {
          AttributeNode node = new AttributeNode(attributeMapping.AttributeInfo);
          c.Nodes.Add((TreeNode) node);
        }
        else if (outputMappingBase is DelimiterMapping delimiterMapping)
        {
          DelimiterNode node = new DelimiterNode(delimiterMapping.Delimiter);
          c.Nodes.Add((TreeNode) node);
        }
      }
    }
    c.IsOverriden = flag;
  }

  /// <summary>Обновить (если нужно) схему данными из дерева</summary>
  public void UpdateScheme(bool forceUpdateXml = false)
  {
    bool flag = false;
    foreach (object node1 in this.treeModel.Nodes)
    {
      if (node1 is SectionNode s)
      {
        foreach (object node2 in s.Nodes)
        {
          if (node2 is CellNode c1 && this.OverwriteCellMapping(s, c1, ObjTypeNode.Default))
            flag = true;
          if (node2 is ObjTypeNode on)
          {
            foreach (object node3 in on.Nodes)
            {
              if (node3 is CellNode c2 && this.OverwriteCellMapping(s, c2, on))
                flag = true;
            }
          }
        }
      }
      else if (node1 is CellNode c)
        flag = flag || this.OverwriteCellMapping((SectionNode) null, c, ObjTypeNode.Default);
    }
    if (!(flag | forceUpdateXml))
      return;
    this._outputAttributeMappingScheme.UpdateXml();
  }

  private bool OverwriteCellMapping(SectionNode s, CellNode c, ObjTypeNode on)
  {
    bool flag1 = false;
    string sectionGuid = s?.SectionGuid;
    Guid empty;
    if (sectionGuid == null)
    {
      empty = Guid.Empty;
      sectionGuid = empty.ToString();
    }
    CellOutputMapping cellMapping = this._outputAttributeMappingScheme.GetCellMapping(sectionGuid, c.Id, on.ObjectTypeGuid);
    bool flag2 = cellMapping == null || cellMapping.Length != c.Nodes.Count;
    if (c.Nodes.Count == 0 && cellMapping != null && cellMapping.HasBlankOutput)
      return false;
    if (c.Nodes.Count == 0 && cellMapping != null && !cellMapping.HasBlankOutput)
      flag2 = true;
    if (!on.Name.Equals(ObjTypeNode.Default.Name, StringComparison.CurrentCultureIgnoreCase))
    {
      string objTypeGuid = cellMapping.ObjTypeGuid;
      empty = Guid.Empty;
      string str = empty.ToString();
      if (objTypeGuid == str)
        flag2 = true;
    }
    if (!flag2)
    {
      for (int index = 0; index < c.Nodes.Count; ++index)
      {
        flag2 = cellMapping.Items[index] is AttributeMapping && c.Nodes[index] is DelimiterNode || !(cellMapping.Items[index] is AttributeMapping) && c.Nodes[index] is AttributeNode || c.Nodes[index] is AttributeNode node1 && cellMapping.Items[index] is AttributeMapping attributeMapping && !attributeMapping.Equals((object) node1.AttributeInfo) || c.Nodes[index] is DelimiterNode node2 && cellMapping.Items[index] is DelimiterMapping delimiterMapping && !delimiterMapping.Delimiter.Equals(node2.Delimiter, StringComparison.CurrentCulture);
        if (flag2)
          break;
      }
    }
    if (flag2)
    {
      this._outputAttributeMappingScheme.SetCellMapping(CellOutputMapping.FromNode(c));
      flag1 = true;
    }
    return flag1;
  }

  private bool CanChangeMapping()
  {
    if (this.ReadOnly || this._outputAttributeMappingScheme == null || this.ControlsAreUpdating)
      return false;
    bool wasUpdated = false;
    return this.CheckCanEdit(ref wasUpdated);
  }

  private void ShowDocumentTreeView()
  {
    if (this.DocumentTreeViewDlg == null)
      this.documentTreeViewDlg = new DocumentTreeViewDlg();
    if (this.docControl != null)
    {
      this.documentTreeViewDlg.TreeRoot = (DocumentTreeNode) this.docControl.Document;
      this.documentTreeViewDlg.DocumentControl = this.docControl;
      this.documentTreeViewDlg.UpdateSelection();
    }
    this.documentTreeViewDlg.Collapsible = true;
    this.documentTreeViewDlg.Closable = false;
    this.documentTreeViewDlg.Show(this.dockMan, DockState.DockRight);
    this.documentTreeViewDlg.LayoutSystem.Collapsed = true;
  }

  /// <summary>Выбор типа объекта из списка допустимых в спецификации</summary>
  /// <param name="sections">Список разделов допустимые изделия которых можно выбирать</param>
  /// <param name="multiSelect">Использовать множественный выбор</param>
  /// <returns></returns>
  private List<ObjTypeNode> SelectObjectTypeFromList(string sectionGuid = null, bool selectMany = false)
  {
    List<int> availableObjectTypeIds = this.GetAvailableObjectTypeIDs(sectionGuid);
    if (availableObjectTypeIds == null)
      return (List<ObjTypeNode>) null;
    List<ObjTypeNode> objTypeNodeList = new List<ObjTypeNode>();
    SelectorForm selectorForm = new SelectorForm(typeof (ObjectTypesFolder), "Допустимые типы объектов", typeof (ObjectTypeFolder), selectMany);
    selectorForm.ExpandLevelsOnLoad = 2;
    selectorForm.SelectorFilter = (ISelectorFilter) new AVSSelectorFilter(availableObjectTypeIds, availableObjectTypeIds.ToArray(), true, true);
    selectorForm.NodeSelectorFilter = (INodeSelectorFilter) new AvsNodeSelectorFilter();
    if (selectorForm.ShowDialog() == DialogResult.OK && selectorForm.IDList.Count > 0)
    {
      foreach (int id in selectorForm.IDList)
      {
        string objectTypeName = MetaDataHelper.GetObjectTypeName(id);
        Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(id);
        objTypeNodeList.Add(new ObjTypeNode(objectTypeGuid, objectTypeName));
      }
    }
    return objTypeNodeList;
  }

  /// <summary>Собрать все допустимые типы из разделов спецификации</summary>
  private List<int> GetAvailableObjectTypeIDs(string sectionGuid = null)
  {
    this.availableTypeList = new List<int>();
    List<SpecificationSectionInfo> list = this.GetDocumentSections(this.document).Where<SpecificationSectionInfo>((Func<SpecificationSectionInfo, bool>) (ds => string.IsNullOrWhiteSpace(sectionGuid) || Guid.Empty.ToString().Equals(sectionGuid, StringComparison.InvariantCulture) || ds.SectionGuid.ToString().Equals(sectionGuid, StringComparison.InvariantCulture))).ToList<SpecificationSectionInfo>();
    if (list == null || list.Count == 0)
      return (List<int>) null;
    for (int index = 0; index < list.Count; ++index)
      this.availableTypeList.AddRange((IEnumerable<int>) AVSDocument.GetTypeIdListForSection(list[index]));
    if (this.availableTypeList.Count > 0)
      this.availableTypeList = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) this.availableTypeList);
    this.availableTypeList = this.availableTypeList.Distinct<int>().ToList<int>();
    return this.availableTypeList;
  }

  private void buttonReset_Click(object sender, EventArgs e)
  {
    bool wasUpdated = false;
    if (this.ReadOnly || !this.CheckCanEdit(ref wasUpdated) || MessageBox.Show("Сбросить изменения в схеме вывода к значениям по умолчанию?", "Схема вывода атрибутов", MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    this.LockControls();
    try
    {
      if (this.document != null && this._outputAttributeMappingScheme != null)
      {
        this._outputAttributeMappingScheme.ResetToDefaults();
        this.PrepareDefaultOutputTreeData();
      }
      this.LoadMappingFromSchemeToVTree();
      this.UpdateControls();
      this.Changed = true;
    }
    finally
    {
      this.UnlockControls();
    }
  }

  private void btAdd_Click(object sender, EventArgs e) => this.AddAttributeNode();

  private void btDelete_Click(object sender, EventArgs e)
  {
    if (this.SelectedOutputMappingTreeNode == null)
      return;
    this.RemoveAttributeOrDelimiterNode();
  }

  private void buttonApply_Click(object sender, EventArgs e)
  {
    this.Changed = false;
    this.UpdateControls();
    this.UpdateScheme(true);
    this.buttonApply.Tag = this.buttonApply.Tag ?? (object) "Apply";
    MouseEventHandler actionButtonClicked = this.OnActionButtonClicked;
    if (actionButtonClicked == null)
      return;
    actionButtonClicked((object) this.buttonApply, (MouseEventArgs) null);
  }

  private void buttonCancel_Click(object sender, EventArgs e)
  {
    this.Changed = false;
    this.UpdateControls();
    this.LoadMappingFromSchemeToVTree();
    this.buttonCancel.Tag = this.buttonCancel.Tag ?? (object) "Cancel";
    MouseEventHandler actionButtonClicked = this.OnActionButtonClicked;
    if (actionButtonClicked == null)
      return;
    actionButtonClicked((object) this.buttonCancel, (MouseEventArgs) null);
  }

  private void collapsibleSplitter_Click(object sender, EventArgs e)
  {
    this.splitContainerMain.Panel2Collapsed = !this.splitContainerMain.Panel2Collapsed;
    EventHandler outlineVisibleChanged = this.OnDocumentOutlineVisibleChanged;
    if (outlineVisibleChanged == null)
      return;
    outlineVisibleChanged(sender, e);
  }

  private void HandleMappingTreeViewMouseActions(object sender, MouseEventArgs args)
  {
    if (args.Button != MouseButtons.Right)
    {
      this.UpdateControls();
    }
    else
    {
      if (!(sender is Control control))
        return;
      TreeNode treeNode = (TreeNode) null;
      Row nodeAt = this.treeOutput.GetNodeAt(args.X, args.Y);
      if (nodeAt != null)
      {
        treeNode = nodeAt.Item as TreeNode;
        this.treeOutput.SelectedRow = nodeAt;
      }
      switch (treeNode)
      {
        case null:
          break;
        case DelimiterNode _:
        case AttributeNode _:
        case CellNode _:
          this.miObjectTypeAdd.Visible = this.miObjectTypeDel.Visible = false;
          this.miDelimiterAdd.Visible = this.miDelimiterEdit.Visible = this.miAttributeOrDelimiterDel.Visible = true;
          this.miDelimiterEdit.Enabled = treeNode is DelimiterNode;
          this.miAttributeOrDelimiterDel.Enabled = treeNode is DelimiterNode || treeNode is AttributeNode;
          this.contextMenuMain.Show(control, new Point(args.X, args.Y));
          break;
        default:
          this.miObjectTypeAdd.Visible = this.miObjectTypeDel.Visible = true;
          this.miDelimiterAdd.Visible = this.miDelimiterEdit.Visible = this.miAttributeOrDelimiterDel.Visible = false;
          this.miObjectTypeDel.Enabled = treeNode is ObjTypeNode objTypeNode && objTypeNode.ObjectTypeGuid != Guid.Empty.ToString();
          this.contextMenuMain.Show(control, new Point(args.X, args.Y));
          break;
      }
    }
  }

  private void btMoveUp_Click(object sender, EventArgs e)
  {
    if (!this.CanChangeMapping() || this.SelectedOutputMappingTreeNode == null)
      return;
    this.MoveSelectedTreeNodeUp();
  }

  private void MoveSelectedTreeNodeUp()
  {
    Row selectedRow;
    if ((selectedRow = this.treeOutput.SelectedRow) == null)
      return;
    TreeNode node = selectedRow.Item as TreeNode;
    Row parentRow = selectedRow.ParentRow;
    TreeNode parent = node.Parent;
    int index = node.Index;
    if (index <= 0)
      return;
    node.Remove();
    parent.Nodes.Insert(index - 1, node);
    this.OnCellOutputChanged((object) parentRow);
    this.treeOutput.SelectedRow = parentRow.ChildRowByIndex(index - 1);
  }

  private void btMoveDown_Click(object sender, EventArgs e)
  {
    if (!this.CanChangeMapping() || this.SelectedOutputMappingTreeNode == null)
      return;
    this.MoveSelectedNodeDown();
  }

  private void MoveSelectedNodeDown()
  {
    Row selectedRow;
    if ((selectedRow = this.treeOutput.SelectedRow) == null)
      return;
    TreeNode node = selectedRow.Item as TreeNode;
    Row parentRow = selectedRow.ParentRow;
    TreeNode parent = node.Parent;
    int index = node.Index;
    int count = parent.Nodes.Count;
    if (index >= count - 1)
      return;
    node.Remove();
    parent.Nodes.Insert(index + 1, node);
    this.OnCellOutputChanged((object) parentRow);
    this.treeOutput.SelectedRow = parentRow.ChildRowByIndex(index + 1);
  }

  private void btInsert_Click(object sender, EventArgs e)
  {
    if (!this.CanChangeMapping())
      return;
    TreeNode outputMappingTreeNode = this.SelectedOutputMappingTreeNode;
    switch (outputMappingTreeNode)
    {
      case ObjTypeNode _:
      case SectionNode _:
        this.Changed = this.AddObjectType() != null;
        if (!this.Changed)
          break;
        Row row = outputMappingTreeNode is SectionNode ? this.treeOutput.FindRow((object) outputMappingTreeNode) : this.treeOutput.FindRow((object) outputMappingTreeNode.Parent);
        if (row == null)
          break;
        this.treeOutput.UpdateRowData(row);
        row.UpdateChildren(true, true);
        break;
      default:
        int x1 = this.btInsert.ButtonBounds.Location.X;
        Rectangle buttonBounds = this.btInsert.ButtonBounds;
        int width = buttonBounds.Width;
        int x2 = x1 + width;
        buttonBounds = this.btInsert.ButtonBounds;
        int y1 = buttonBounds.Location.Y;
        buttonBounds = this.btInsert.ButtonBounds;
        int height = buttonBounds.Height;
        int y2 = y1 + height;
        this.contextMenuDelimiters.OwnerItem = (ToolStripItem) null;
        this.contextMenuDelimiters.Tag = (object) "A";
        this.contextMenuDelimiters.Show((Control) this.toolBarRight, new Point(x2, y2));
        break;
    }
  }

  private void btEdit_Click(object sender, EventArgs e)
  {
    if (!this.CanChangeMapping())
      return;
    Point location = this.btEdit.ButtonBounds.Location;
    int x = location.X + this.btEdit.ButtonBounds.Width;
    location = this.btEdit.ButtonBounds.Location;
    int y = location.Y + this.btEdit.ButtonBounds.Height;
    this.contextMenuDelimiters.OwnerItem = (ToolStripItem) null;
    this.contextMenuDelimiters.Tag = (object) "E";
    this.contextMenuDelimiters.Show((Control) this.toolBarRight, new Point(x, y));
  }

  private void btRemove_Click(object sender, EventArgs e)
  {
    TreeNode outputMappingTreeNode = this.SelectedOutputMappingTreeNode;
    switch (outputMappingTreeNode)
    {
      case DelimiterNode _:
      case AttributeNode _:
        this.RemoveAttributeOrDelimiterNode();
        break;
      default:
        this.RemoveObjectType(outputMappingTreeNode as ObjTypeNode);
        break;
    }
  }

  private void btMoveBottom_Click(object sender, EventArgs e)
  {
    if (!this.CanChangeMapping() || this.SelectedOutputMappingTreeNode == null)
      return;
    this.MoveSelectedNodeBottom();
  }

  private void MoveSelectedNodeBottom()
  {
    Row selectedRow;
    if ((selectedRow = this.treeOutput.SelectedRow) == null)
      return;
    TreeNode node = selectedRow.Item as TreeNode;
    Row parentRow = selectedRow.ParentRow;
    TreeNode parent = node.Parent;
    int index = node.Index;
    int count = parent.Nodes.Count;
    if (index >= count - 1)
      return;
    node.Remove();
    parent.Nodes.Add(node);
    this.OnCellOutputChanged((object) parentRow);
    this.treeOutput.SelectedRow = parentRow.ChildRowByIndex(count - 1);
  }

  private void btMoveTop_Click(object sender, EventArgs e)
  {
    if (!this.CanChangeMapping() || this.SelectedOutputMappingTreeNode == null)
      return;
    this.MoveSelectedNodeTop();
  }

  private void MoveSelectedNodeTop()
  {
    Row selectedRow;
    if ((selectedRow = this.treeOutput.SelectedRow) == null)
      return;
    TreeNode node = selectedRow.Item as TreeNode;
    Row parentRow = selectedRow.ParentRow;
    TreeNode parent = node.Parent;
    if (node.Index <= 0)
      return;
    node.Remove();
    parent.Nodes.Insert(0, node);
    this.OnCellOutputChanged((object) parentRow);
    this.treeOutput.SelectedRow = parentRow.ChildRowByIndex(0);
  }

  public void HideDocumentStructurePane()
  {
    this.splitContainerMain.Panel2Collapsed = true;
    this.splitContainerMain.Panel1.Controls.Remove((Control) this.collapsibleSplitter);
    this.collapsibleSplitter.Visible = false;
  }

  private void DoAddNewObjectTypeAction(object sender, EventArgs e)
  {
    ObjTypeNode objTypeNode = this.AddObjectType();
    if (objTypeNode == null)
      return;
    Row row = this.treeOutput.FindRow((object) objTypeNode)?.ParentRow;
    if (row == null && objTypeNode.Parent != null)
      row = this.treeOutput.FindRow((object) objTypeNode.Parent);
    if (row == null)
      return;
    this.treeOutput.UpdateRowData(row);
    row.UpdateChildren(true, true);
    this.treeOutput.SelectedRow = row.ChildRowByIndex(objTypeNode.Index);
    this.Changed = true;
    this.UpdatePreviewBox();
  }

  private void treeOutput_GetCellData(object sender, GetCellDataEventArgs e)
  {
    TreeNode treeNode1 = e.Row.Item as TreeNode;
    if (e.Row.Item is SectionNode sectionNode && sectionNode.IsOverriden || e.Row.Item is ObjTypeNode objTypeNode && objTypeNode.Nodes.OfType<CellNode>().Any<CellNode>((Func<CellNode, bool>) (n => n.IsOverriden)) || e.Row.Item is CellNode cellNode && cellNode.IsOverriden || e.Row.Item is TreeNode treeNode2 && treeNode2.Parent is CellNode parent && parent.IsOverriden)
      e.CellData.OddStyle = e.CellData.EvenStyle = this.boldStyle;
    e.CellData.Value = (object) treeNode1.Text;
  }

  /// <summary>Получить политику дочерних узлов</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeOutput_GetChildPolicy(object sender, GetChildPolicyEventArgs e)
  {
    e.ChildPolicy = RowChildPolicy.Normal;
  }

  /// <summary>Получить дочерние узлы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void treeOutput_GetChildren(object sender, GetChildrenEventArgs e)
  {
    e.Children = e.Row.Item is TreeNode treeNode ? (IList) treeNode.Nodes : (IList) null;
  }

  public override int CancelButtonRightEdge
  {
    get
    {
      return this.panelMainBottom.Width - (this.buttonCancel.Location.X + this.buttonCancel.Size.Width);
    }
  }
}
