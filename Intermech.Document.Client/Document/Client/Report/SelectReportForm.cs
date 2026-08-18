// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.SelectReportForm
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using DevExpress.IM.Utils;
using DevExpress.IM.XtraTreeList;
using DevExpress.IM.XtraTreeList.Columns;
using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.Client.Report;

public class SelectReportForm : Form
{
  internal static long LastReportID;
  private ISelectedItems _selectedItems;
  private INodeQuery _nodeQuery;
  private System.IServiceProvider _viewServices;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panel1;
  private Button bNew;
  private Button bPreview;
  private Button bPrint;
  private Button bDelete;
  private Button bEdit;
  private Panel panel2;
  private CheckBox cbItems;
  private TreeList treeList1;
  private TreeListColumn colName;
  private Button bInWindow;
  private ImageList imageList1;

  internal static void SaveLayout(SelectReportForm form)
  {
    if (form == null)
      return;
    SelectReportForm.LastReportID = form.FocusedReportID;
  }

  public SelectReportForm(System.IServiceProvider viewServices, ISelectedItems selectedItems)
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 850);
    this._selectedItems = selectedItems;
    this._viewServices = viewServices;
    this._nodeQuery = ((IReportView) this._viewServices.GetService(typeof (IReportView))).NodeQuery;
    this.cbItems.Checked = selectedItems != null && selectedItems.Count > 1;
  }

  public DialogResult Execute()
  {
    if (ServicesManager.GetService(typeof (ICategoryTypeIconService)) is ICategoryTypeIconService service)
    {
      ImageList imageList = new ImageList();
      imageList.ColorDepth = ColorDepth.Depth24Bit;
      Icon ico1 = service.GetIcon(4, MetaDataHelper.GetObjectTypeID(new Guid("cad00289-306c-11d8-b4e9-00304f19f545")));
      int width = ico1.Width;
      int height = ico1.Height;
      Icon ico2 = service.GetIcon(4, MetaDataHelper.GetObjectTypeID(new Guid("cad0028a-306c-11d8-b4e9-00304f19f545")));
      if (ico2.Width < width)
        width = ico2.Width;
      if (ico2.Height < height)
        height = ico2.Height;
      Size size = new Size(width, height);
      if (width == 32 /*0x20*/)
      {
        if (ico1.Size != size)
          ico1 = ImagesResizeHelper.ResizeIconTo32x16(ico1, this.treeList1.BackColor);
        if (ico2.Size != size)
          ico2 = ImagesResizeHelper.ResizeIconTo32x16(ico2, this.treeList1.BackColor);
      }
      imageList.ImageSize = size;
      imageList.Images.Add(ico1);
      imageList.Images.Add(ico2);
      this.treeList1.StateImageList = imageList;
    }
    this.LoadReports();
    if (SelectReportForm.LastReportID != 0L)
      this.SelectNode(SelectReportForm.LastReportID);
    this.SetButtons();
    return this.ShowDialog();
  }

  /// <summary>Загрузим все табличные отчеты в дерево</summary>
  private void LoadReports()
  {
    this.treeList1.ClearNodes();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this.LoadTypedReports(sessionKeeper.Session, new Guid("cad00289-306c-11d8-b4e9-00304f19f545"), this.treeList1.StateImageList != null ? 0 : -1);
      this.LoadTypedReports(sessionKeeper.Session, new Guid("cad0028a-306c-11d8-b4e9-00304f19f545"), this.treeList1.StateImageList != null ? 1 : -1);
    }
  }

  public long FocusedReportID
  {
    get
    {
      return this.treeList1.FocusedNode == null ? 0L : Convert.ToInt64(this.treeList1.FocusedNode.Tag);
    }
  }

  /// <summary>Закрузка в треелист отчетов одного типа</summary>
  /// <param name="session"></param>
  /// <param name="typeGuid"></param>
  /// <param name="iconIndex"></param>
  private void LoadTypedReports(IUserSession session, Guid typeGuid, int iconIndex)
  {
    IDBObjectType objectType = session.GetObjectType(typeGuid, false);
    if (objectType == null)
      return;
    TreeListNode treeListNode = this.treeList1.AppendNode((object) new object[1]
    {
      (object) objectType.ObjectTypeName
    }, (TreeListNode) null);
    treeListNode.StateImageIndex = iconIndex;
    treeListNode.Tag = (object) 0L;
    treeListNode.Expanded = true;
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectCollection(objectType.ObjectType).Select(new DBRecordSetParams((ConditionStructure[]) null, new ColumnDescriptor[3]
    {
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
      new ColumnDescriptor((object) session.IdentHelper.NameID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
    })).Rows)
      this.treeList1.AppendNode((object) new object[1]
      {
        (object) Convert.ToString(row[0])
      }, treeListNode.Id, treeListNode.ImageIndex, treeListNode.SelectImageIndex, treeListNode.StateImageIndex).Tag = (object) Convert.ToInt64(row[1]);
  }

  /// <summary>Установим кнопки</summary>
  private void SetButtons()
  {
    if (this.treeList1.FocusedNode != null && Convert.ToInt64(this.treeList1.FocusedNode.Tag) != 0L)
    {
      this.bInWindow.Enabled = true;
      this.bPrint.Enabled = true;
      this.bPreview.Enabled = true;
      this.bNew.Enabled = true;
      this.bEdit.Enabled = true;
      this.bDelete.Enabled = true;
    }
    else
    {
      this.bInWindow.Enabled = false;
      this.bPrint.Enabled = false;
      this.bPreview.Enabled = false;
      this.bNew.Enabled = true;
      this.bEdit.Enabled = false;
      this.bDelete.Enabled = false;
    }
  }

  private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
  {
    this.SetButtons();
  }

  private void bCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
  }

  private void bNew_Click(object sender, EventArgs e)
  {
    long aTemplateObjectID = 0;
    List<Guid> guidList = new List<Guid>();
    if (this.treeList1.FocusedNode != null && Convert.ToInt64(this.treeList1.FocusedNode.Tag) == 0L)
    {
      if (this.treeList1.FocusedNode.StateImageIndex == 0)
        guidList.Add(new Guid("cad00289-306c-11d8-b4e9-00304f19f545"));
      if (this.treeList1.FocusedNode.StateImageIndex == 1)
        guidList.Add(new Guid("cad0028a-306c-11d8-b4e9-00304f19f545"));
    }
    else
    {
      guidList.Add(new Guid("cad00289-306c-11d8-b4e9-00304f19f545"));
      guidList.Add(new Guid("cad0028a-306c-11d8-b4e9-00304f19f545"));
    }
    if (this.treeList1.FocusedNode != null && Convert.ToInt64(this.treeList1.FocusedNode.Tag) != 0L)
    {
      switch (IMMessageBox.Show(MessageDialogs.msgConfirmAction, $"{LocalizationHolder.rm.GetString("Document.Client_1")}\"{this.treeList1.FocusedNode.GetDisplayText((object) 0)}\" ?", MessageBoxButtons.YesNoCancel, IMMessageBoxImage.Question))
      {
        case DialogResult.Cancel:
          return;
        case DialogResult.Yes:
          aTemplateObjectID = Convert.ToInt64(this.treeList1.FocusedNode.Tag);
          break;
      }
    }
    IObjectCreatorService service = ServicesManager.GetService(typeof (IObjectCreatorService)) as IObjectCreatorService;
    long ReportID = aTemplateObjectID == 0L ? service.CreateObjectByTypeDialog(guidList.ToArray()) : service.CreateObjectByTemplateDialog(aTemplateObjectID);
    if (ReportID <= 0L)
      return;
    this.LoadReports();
    this.SelectNode(ReportID);
    this.SetButtons();
  }

  private void SelectNode(long ReportID)
  {
    foreach (TreeListNode node1 in this.treeList1.Nodes)
    {
      if (node1.Nodes.Count > 0)
      {
        foreach (TreeListNode node2 in node1.Nodes)
        {
          if (Convert.ToInt64(node2.Tag) == ReportID)
          {
            node1.Expanded = true;
            this.treeList1.FocusedNode = node2;
            this.treeList1.MakeNodeVisible(node2);
            return;
          }
        }
      }
    }
  }

  private void bEdit_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TableReportEditor tableReportEditor = new TableReportEditor();
      tableReportEditor.ParentMode = 0;
      tableReportEditor.LoadObjectData(Convert.ToInt64(this.treeList1.FocusedNode.Tag));
      if (tableReportEditor.ShowDialog() != DialogResult.OK)
        return;
      IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(this.treeList1.FocusedNode.Tag));
      this.treeList1.BeginUpdate();
      this.treeList1.FocusedNode.SetValue((object) 0, (object) dbObject.Caption);
      this.treeList1.EndUpdate();
    }
  }

  private void bDelete_Click(object sender, EventArgs e)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      TreeListNode focusedNode = this.treeList1.FocusedNode;
      IDBObject dbObject = sessionKeeper.Session.GetObject(Convert.ToInt64(focusedNode.Tag));
      long objectId = dbObject.ObjectID;
      IMMessageBoxButton messageBoxButton1 = new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_2"), DialogResult.Yes);
      IMMessageBoxButton messageBoxButton2 = new IMMessageBoxButton(LocalizationHolder.rm.GetString("Document.Client_3"), DialogResult.No);
      if (IMMessageBox.Show(LocalizationHolder.rm.GetString("Document.Client_4"), string.Format(LocalizationHolder.rm.GetString("Document.Client_5"), (object) dbObject.Caption), new IMMessageBoxButton[2]
      {
        messageBoxButton1,
        messageBoxButton2
      }, IMMessageBoxImage.Question) != DialogResult.Yes)
        return;
      dbObject.Delete(0L);
      ((INotificationService) ServicesManager.GetService(typeof (INotificationService))).FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", objectId));
      this.treeList1.BeginUpdate();
      this.treeList1.Nodes.Remove(focusedNode);
      this.treeList1.EndUpdate();
    }
  }

  private void bPreview_Click(object sender, EventArgs e)
  {
    try
    {
      new ReportFormer(!this.cbItems.Checked ? new ReportParameters(Convert.ToInt64(this.treeList1.FocusedNode.Tag), this._nodeQuery, this._viewServices) : new ReportParameters(Convert.ToInt64(this.treeList1.FocusedNode.Tag), this._selectedItems, this._nodeQuery, this._viewServices)).Execute(ShowReport.InPreviewWindow);
    }
    finally
    {
      this._nodeQuery = ((IReportView) this._viewServices.GetService(typeof (IReportView))).NodeQuery;
    }
  }

  private void bPrint_Click(object sender, EventArgs e)
  {
    try
    {
      new ReportFormer(!this.cbItems.Checked ? new ReportParameters(Convert.ToInt64(this.treeList1.FocusedNode.Tag), this._nodeQuery, this._viewServices) : new ReportParameters(Convert.ToInt64(this.treeList1.FocusedNode.Tag), this._selectedItems, this._nodeQuery, this._viewServices)).Execute(ShowReport.Print);
    }
    finally
    {
      this._nodeQuery = ((IReportView) this._viewServices.GetService(typeof (IReportView))).NodeQuery;
    }
  }

  private void bInWindow_Click(object sender, EventArgs e)
  {
    try
    {
      new ReportFormer(!this.cbItems.Checked ? new ReportParameters(Convert.ToInt64(this.treeList1.FocusedNode.Tag), this._nodeQuery, this._viewServices) : new ReportParameters(Convert.ToInt64(this.treeList1.FocusedNode.Tag), this._selectedItems, this._nodeQuery, this._viewServices)).Execute(ShowReport.InDoc);
    }
    finally
    {
      this._nodeQuery = ((IReportView) this._viewServices.GetService(typeof (IReportView))).NodeQuery;
    }
  }

  /// <summary>Загрузка формы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectReportForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохранение закрытии</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void SelectReportForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    SelectReportForm.SaveLayout(this);
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SelectReportForm));
    this.panel1 = new Panel();
    this.panel2 = new Panel();
    this.cbItems = new CheckBox();
    this.treeList1 = new TreeList();
    this.imageList1 = new ImageList();
    this.bPreview = new Button();
    this.colName = new TreeListColumn();
    this.bInWindow = new Button();
    this.bNew = new Button();
    this.bPrint = new Button();
    this.bDelete = new Button();
    this.bEdit = new Button();
    this.panel1.SuspendLayout();
    this.panel2.SuspendLayout();
    this.treeList1.BeginInit();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.bInWindow);
    this.panel1.Controls.Add((Control) this.bNew);
    this.panel1.Controls.Add((Control) this.bPreview);
    this.panel1.Controls.Add((Control) this.bPrint);
    this.panel1.Controls.Add((Control) this.bDelete);
    this.panel1.Controls.Add((Control) this.bEdit);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this.panel2.Controls.Add((Control) this.cbItems);
    componentResourceManager.ApplyResources((object) this.panel2, "panel2");
    this.panel2.Name = "panel2";
    this.cbItems.Checked = true;
    this.cbItems.CheckState = CheckState.Checked;
    componentResourceManager.ApplyResources((object) this.cbItems, "cbItems");
    this.cbItems.Name = "cbItems";
    this.cbItems.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.treeList1, "treeList1");
    this.treeList1.Columns.AddRange(new TreeListColumn[1]
    {
      this.colName
    });
    this.treeList1.Name = "treeList1";
    this.treeList1.Styles.AddReplace("FocusedCell", (object) new ViewStyle("FocusedCell", (string) null, new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), "", StyleOptions.StyleEnabled | StyleOptions.UseBackColor | StyleOptions.UseDrawFocusRect | StyleOptions.UseFont | StyleOptions.UseForeColor | StyleOptions.UseImage, true, false, false, HorzAlignment.Default, VertAlignment.Center, (Image) null, SystemColors.Highlight, SystemColors.HighlightText));
    this.treeList1.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
    this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
    componentResourceManager.ApplyResources((object) this.imageList1, "imageList1");
    this.imageList1.TransparentColor = Color.Transparent;
    componentResourceManager.ApplyResources((object) this.bPreview, "bPreview");
    this.bPreview.DialogResult = DialogResult.OK;
    this.bPreview.Image = (Image) Intermech.Document.Client.Properties.Resources.view_t;
    this.bPreview.Name = "bPreview";
    this.bPreview.UseVisualStyleBackColor = true;
    this.bPreview.Click += new EventHandler(this.bPreview_Click);
    componentResourceManager.ApplyResources((object) this.colName, "colName");
    this.colName.Name = "colName";
    componentResourceManager.ApplyResources((object) this.bInWindow, "bInWindow");
    this.bInWindow.DialogResult = DialogResult.OK;
    this.bInWindow.Image = (Image) Intermech.Document.Client.Properties.Resources.open_t;
    this.bInWindow.Name = "bInWindow";
    this.bInWindow.UseVisualStyleBackColor = true;
    this.bInWindow.Click += new EventHandler(this.bInWindow_Click);
    componentResourceManager.ApplyResources((object) this.bNew, "bNew");
    this.bNew.Image = (Image) Intermech.Document.Client.Properties.Resources.new_t;
    this.bNew.Name = "bNew";
    this.bNew.UseVisualStyleBackColor = true;
    this.bNew.Click += new EventHandler(this.bNew_Click);
    componentResourceManager.ApplyResources((object) this.bPrint, "bPrint");
    this.bPrint.Image = (Image) Intermech.Document.Client.Properties.Resources.print_t;
    this.bPrint.Name = "bPrint";
    this.bPrint.UseVisualStyleBackColor = true;
    this.bPrint.Click += new EventHandler(this.bPrint_Click);
    componentResourceManager.ApplyResources((object) this.bDelete, "bDelete");
    this.bDelete.Image = (Image) Intermech.Document.Client.Properties.Resources.del_t;
    this.bDelete.Name = "bDelete";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    componentResourceManager.ApplyResources((object) this.bEdit, "bEdit");
    this.bEdit.Image = (Image) Intermech.Document.Client.Properties.Resources.edit_t;
    this.bEdit.Name = "bEdit";
    this.bEdit.UseVisualStyleBackColor = true;
    this.bEdit.Click += new EventHandler(this.bEdit_Click);
    this.AcceptButton = (IButtonControl) this.bPreview;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.treeList1);
    this.Controls.Add((Control) this.panel2);
    this.Controls.Add((Control) this.panel1);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (SelectReportForm);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.FormClosed += new FormClosedEventHandler(this.SelectReportForm_FormClosed);
    this.Load += new EventHandler(this.SelectReportForm_Load);
    this.panel1.ResumeLayout(false);
    this.panel2.ResumeLayout(false);
    this.treeList1.EndInit();
    this.ResumeLayout(false);
  }
}
