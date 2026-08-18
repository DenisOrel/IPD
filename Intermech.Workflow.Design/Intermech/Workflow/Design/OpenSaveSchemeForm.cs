// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.OpenSaveSchemeForm
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Workflow;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class OpenSaveSchemeForm : FormEx
{
  private long _categoryID;
  private long _schemeID;
  private bool _manualChanging;
  private ISelectedItemsHost _selhost;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel SchemesPanel;
  private PageViewsManager pageViewsManager;
  private Splitter schemesSplitter;
  private SchemesTreeView schemesView;
  private Panel panel1;
  private Button CancButton;
  private Button OkButton;
  private Label capLabel;
  private CheckBox readOnlyCheckBox;
  private TextBox nameCombo;
  private TreeViewsBridge treeViewsBridge;

  public OpenSaveSchemeForm()
  {
    this.InitializeComponent();
    this.nameCombo.TextChanged += new EventHandler(this.nameCombo_TextChanged);
  }

  private void nameCombo_TextChanged(object sender, EventArgs e)
  {
    if (this._manualChanging)
      return;
    this.SchemeID = 0L;
  }

  public long CategoryID => this._categoryID;

  private void schemesView_SelectedItemsChanged(object sender, EventArgs e)
  {
    this._categoryID = 0L;
    ISelectedItems selectedItems = this.schemesView.SelectedItems;
    if (selectedItems.Count > 0 && selectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      this._categoryID = itemData.Value;
    this.UpdateSelHost();
  }

  private void UpdateSelHost()
  {
    this._selhost = (ISelectedItemsHost) null;
    if (this.pageViewsManager.ActiveViewPage == null)
      return;
    IView view = this.pageViewsManager.ActiveViewPage.View;
    this._selhost = view as ISelectedItemsHost;
    if (this._selhost != null)
    {
      this._selhost.SelectedItemsChanged -= new EventHandler(this.SchemesSelectedItemsChanged);
      this._selhost.SelectedItemsChanged += new EventHandler(this.SchemesSelectedItemsChanged);
    }
    if (!(view is SchemesView))
      return;
    SchemesView schemesView = view as SchemesView;
    schemesView.DisableDoubleClicks = true;
    schemesView.Grid.DoubleClick -= new EventHandler(this.SchemesViewDoubleClick);
    schemesView.Grid.DoubleClick += new EventHandler(this.SchemesViewDoubleClick);
  }

  private void SchemesViewDoubleClick(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.OK;
  }

  public long SchemeID
  {
    get => this._schemeID;
    set
    {
      if (this._schemeID == value)
        return;
      this._schemeID = value;
      if (value == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(value);
        this._manualChanging = true;
        try
        {
          if (objectInfo.Empty)
            return;
          this.nameCombo.Text = objectInfo.Caption;
        }
        finally
        {
          this._manualChanging = false;
        }
      }
    }
  }

  private void SchemesSelectedItemsChanged(object sender, EventArgs e)
  {
    if (this._selhost != null)
    {
      ISelectedItems selectedItems = this._selhost.SelectedItems;
      if (selectedItems.Count > 0 && selectedItems.GetItemData(0, typeof (IDBObjectID)) is IDBObjectID itemData)
      {
        this.SchemeID = itemData.Value;
        return;
      }
    }
    this.SchemeID = 0L;
  }

  /// <summary>
  /// Returns -1 if cancelled, otherwise returns SchemeID (if save to exisitng) and fills the Name parameter
  /// </summary>
  /// <param name="Name"></param>
  /// <returns></returns>
  public static SaveDialogResult ExecuteSave()
  {
    SaveDialogResult saveDialogResult = new SaveDialogResult();
    using (OpenSaveSchemeForm openSaveSchemeForm = new OpenSaveSchemeForm())
    {
      saveDialogResult.DialogResult = openSaveSchemeForm.ShowDialog();
      if (saveDialogResult.DialogResult == DialogResult.OK)
      {
        saveDialogResult.Name = openSaveSchemeForm.nameCombo.Text.Trim();
        saveDialogResult.SchemeID = openSaveSchemeForm.SchemeID;
        saveDialogResult.CategoryID = openSaveSchemeForm.CategoryID;
      }
    }
    return saveDialogResult;
  }

  private void OpenSaveSchemeForm_Shown(object sender, EventArgs e)
  {
    IDescriptor rootDescriptor = (IDescriptor) new TopObjectsDescriptor(Holder.CategorySchemesID, 0, LocalizationHolder.rm.GetString("Workflow.Design_145"), wfConsts.SchemeCategoriesID);
    ServiceContainer serviceContainer = new ServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.InDialog));
    serviceContainer.AddService(typeof (INotificationService), (object) BaseHolder.NotificationService);
    serviceContainer.AddService(typeof (VersionsRule), (object) Holder.AllVersionsRule);
    this.schemesView.Services = (System.IServiceProvider) serviceContainer;
    this.pageViewsManager.Services = (System.IServiceProvider) serviceContainer;
    this.schemesView.SelectedItemsChanged += new EventHandler(this.schemesView_SelectedItemsChanged);
    this.schemesView.SetColumns(Utils.CaptionColumnOnly(NodeColumnSortOrder.Ascending));
    if (!wfFunx.RestoreTreePath((NavigatorTreeView) this.schemesView))
      this.schemesView.Build(rootDescriptor);
    this.UpdateSelHost();
    this.SchemesSelectedItemsChanged((object) null, (EventArgs) null);
  }

  private long FindScheme(string caption, long category)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[1]
    {
      new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID)
    };
    ConditionStructure[] conditionStructureArray = new ConditionStructure[0];
    ConditionStructure[] conditions;
    if (category == 0L)
      conditions = new ConditionStructure[2]
      {
        new ConditionStructure(0, RelationalOperators.NotEntersInType, (object) wfConsts.SchemeCategoriesID, LogicalOperators.AND, 0, false),
        new ConditionStructure(-50, RelationalOperators.Equal, (object) caption, LogicalOperators.NONE, 0, false)
      };
    else
      conditions = new ConditionStructure[2]
      {
        new ConditionStructure(0, RelationalOperators.EntersIn, (object) category, LogicalOperators.AND, 0, false),
        new ConditionStructure(-50, RelationalOperators.Equal, (object) caption, LogicalOperators.NONE, 0, false)
      };
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetObjectCollection(wfConsts.SchemesTypeID).Select(new DBRecordSetParams(conditions, columns, recordCount: 1));
      if (dataTable.Rows.Count > 0)
        return Convert.ToInt64(dataTable.Rows[0][0]);
    }
    return 0;
  }

  private void OpenSaveSchemeForm_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (this.DialogResult != DialogResult.OK)
      return;
    string caption = this.nameCombo.Text.Trim();
    if (caption == "")
      throw new NotificationException(LocalizationHolder.rm.GetString("Workflow.Design_83"));
    long num = this.SchemeID;
    if (num == 0L)
      num = this.FindScheme(caption, this._categoryID);
    if (num != 0L)
      throw new NotificationException($"Шаблон с именем \"{caption}\" уже существует! Назначте другое имя.");
  }

  private void OpenSaveSchemeForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  private void OpenSaveSchemeForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
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
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (OpenSaveSchemeForm));
    this.SchemesPanel = new Panel();
    this.pageViewsManager = new PageViewsManager();
    this.schemesSplitter = new Splitter();
    this.schemesView = new SchemesTreeView();
    this.panel1 = new Panel();
    this.readOnlyCheckBox = new CheckBox();
    this.nameCombo = new TextBox();
    this.capLabel = new Label();
    this.CancButton = new Button();
    this.OkButton = new Button();
    this.treeViewsBridge = new TreeViewsBridge(this.components);
    this.SchemesPanel.SuspendLayout();
    this.schemesView.BeginInit();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.SchemesPanel.BackColor = SystemColors.Control;
    this.SchemesPanel.Controls.Add((Control) this.pageViewsManager);
    this.SchemesPanel.Controls.Add((Control) this.schemesSplitter);
    this.SchemesPanel.Controls.Add((Control) this.schemesView);
    componentResourceManager.ApplyResources((object) this.SchemesPanel, "SchemesPanel");
    this.SchemesPanel.Name = "SchemesPanel";
    this.pageViewsManager.ActiveViewPage = (IViewPage) null;
    this.pageViewsManager.AllowedViews = new string[1]
    {
      "ChildrenView"
    };
    this.pageViewsManager.CausesValidation = false;
    componentResourceManager.ApplyResources((object) this.pageViewsManager, "pageViewsManager");
    this.pageViewsManager.Name = "pageViewsManager";
    componentResourceManager.ApplyResources((object) this.schemesSplitter, "schemesSplitter");
    this.schemesSplitter.Name = "schemesSplitter";
    this.schemesSplitter.TabStop = false;
    this.schemesView.AllowDrop = true;
    this.schemesView.AllowMultiSelect = false;
    this.schemesView.AllowUserPinnedColumns = false;
    this.schemesView.BackgroundImageMode = ImageDrawMode.Tile;
    this.schemesView.BorderStyle = BorderStyle.Fixed3D;
    this.schemesView.DisableCheckedOutColumn = true;
    this.schemesView.DisableKeyDownEvents = true;
    componentResourceManager.ApplyResources((object) this.schemesView, "schemesView");
    this.schemesView.HeaderStyle.HorzAlignment = (StringAlignment) componentResourceManager.GetObject("schemesView.HeaderStyle.HorzAlignment");
    this.schemesView.LineStyle = LineStyle.Dot;
    this.schemesView.Name = "schemesView";
    this.schemesView.RowEvenStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowEvenStyle.WordWrap");
    this.schemesView.RowOddStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowOddStyle.WordWrap");
    this.schemesView.RowSelectedStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowSelectedStyle.WordWrap");
    this.schemesView.RowStyle.BorderColor = SystemColors.Control;
    this.schemesView.RowStyle.BorderStyle = Border3DStyle.Adjust;
    this.schemesView.RowStyle.BorderWidth = 1;
    this.schemesView.RowStyle.WordWrap = (bool) componentResourceManager.GetObject("schemesView.RowStyle.WordWrap");
    this.schemesView.SelectBeforeEdit = true;
    this.schemesView.SelectionMode = Infralution.Controls.VirtualTree.SelectionMode.FullRow;
    this.schemesView.ShowRootRow = false;
    this.schemesView.SuppressErrorMessages = true;
    this.panel1.Controls.Add((Control) this.readOnlyCheckBox);
    this.panel1.Controls.Add((Control) this.nameCombo);
    this.panel1.Controls.Add((Control) this.capLabel);
    this.panel1.Controls.Add((Control) this.CancButton);
    this.panel1.Controls.Add((Control) this.OkButton);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    componentResourceManager.ApplyResources((object) this.readOnlyCheckBox, "readOnlyCheckBox");
    this.readOnlyCheckBox.Name = "readOnlyCheckBox";
    this.readOnlyCheckBox.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.nameCombo, "nameCombo");
    this.nameCombo.Name = "nameCombo";
    componentResourceManager.ApplyResources((object) this.capLabel, "capLabel");
    this.capLabel.Name = "capLabel";
    componentResourceManager.ApplyResources((object) this.CancButton, "CancButton");
    this.CancButton.DialogResult = DialogResult.Cancel;
    this.CancButton.Name = "CancButton";
    componentResourceManager.ApplyResources((object) this.OkButton, "OkButton");
    this.OkButton.DialogResult = DialogResult.OK;
    this.OkButton.Name = "OkButton";
    this.treeViewsBridge.NavTreeView = (NavigatorTreeView) this.schemesView;
    this.treeViewsBridge.UseDelay = false;
    this.treeViewsBridge.ViewsManager = (IViewsManager) this.pageViewsManager;
    this.AcceptButton = (IButtonControl) this.OkButton;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.CancButton;
    this.Controls.Add((Control) this.SchemesPanel);
    this.Controls.Add((Control) this.panel1);
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (OpenSaveSchemeForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.OpenSaveSchemeForm_Load);
    this.Shown += new EventHandler(this.OpenSaveSchemeForm_Shown);
    this.FormClosed += new FormClosedEventHandler(this.OpenSaveSchemeForm_FormClosed);
    this.FormClosing += new FormClosingEventHandler(this.OpenSaveSchemeForm_FormClosing);
    this.SchemesPanel.ResumeLayout(false);
    this.schemesView.EndInit();
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }
}
