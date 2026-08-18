
// Type: Intermech.PropertyEditors.CustomTypes4AttrForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using DevExpress.IM.XtraGrid.Views.Grid.ViewInfo;
using Intermech.Client.Core;
using Intermech.Holders;
using Intermech.Interfaces;
using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// Общая форма отображения привязанных к атрибутам типов объектов и связей
/// </summary>
public class CustomTypes4AttrForm : TabPageForm
{
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;
  private GridControl gridControl;
  private GridView gridView;
  private int _processedCategory;
  private Guid configKey = Guid.Empty;

  private CustomTypes4AttrForm()
    : this(Guid.Empty)
  {
  }

  private CustomTypes4AttrForm(Guid aInstGuid)
    : this(aInstGuid, 0)
  {
  }

  public CustomTypes4AttrForm(Guid aInstGuid, int processedCategory)
    : base(aInstGuid)
  {
    this.InitializeComponent();
    this._processedCategory = processedCategory;
    if (this._processedCategory == 4)
    {
      this.configKey = ConfigCache.ObjTypes4AttrConfigKey;
    }
    else
    {
      if (this._processedCategory != 6)
        return;
      this.configKey = ConfigCache.RelTypes4AttrConfigKey;
    }
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this.gridView != null)
        this.gridView.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (CustomTypes4AttrForm));
    this.gridControl = new GridControl();
    this.gridView = new GridView();
    this.gridControl.BeginInit();
    this.gridView.BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.gridControl, "gridControl");
    this.gridControl.EmbeddedNavigator.Name = "";
    this.gridControl.MainView = (BaseView) this.gridView;
    this.gridControl.Name = "gridControl";
    this.gridView.GridControl = this.gridControl;
    componentResourceManager.ApplyResources((object) this.gridView, "gridView");
    this.gridView.Name = "gridView";
    this.gridView.OptionsBehavior.Editable = false;
    this.gridView.OptionsView.ColumnAutoWidth = false;
    this.gridView.KeyPress += new KeyPressEventHandler(this.gridView_KeyPress);
    this.gridView.DoubleClick += new EventHandler(this.gridView_DoubleClick);
    this.Controls.Add((Control) this.gridControl);
    this.Name = nameof (CustomTypes4AttrForm);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Tag = (object) "  ";
    this.gridControl.EndInit();
    this.gridView.EndInit();
    this.ResumeLayout(false);
  }

  protected virtual BaseTabPage ActualTabPage
  {
    get
    {
      AbortException.Abort("ActualTabPage must be overvriten");
      return (BaseTabPage) null;
    }
  }

  public override void FillForm(IFolder folder)
  {
    this._folder = folder as CustomFolder;
    if (StatesController.GetLoadState((object) this.ActualTabPage))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable data = (DataTable) null;
      if (this._processedCategory == 4)
      {
        IDBObjectTypeCollection objectTypeCollection = sessionKeeper.Session.GetObjectTypeCollection(-2, CoreConsts.FilterRecords);
        if (objectTypeCollection != null)
          data = objectTypeCollection.GetUsedByAttribute((int) this._folder.Id);
      }
      else if (this._processedCategory == 6)
      {
        IDBRelationTypeCollection relationTypeCollection = sessionKeeper.Session.GetRelationTypeCollection(CoreConsts.FilterRecords);
        if (relationTypeCollection != null)
          data = relationTypeCollection.GetUsedByAttribute((int) this._folder.Id);
      }
      if (data != null)
      {
        MemoryStream config = ConfigCache.GetConfig(this.configKey);
        DataTableConverter.ApplyToGridControl(DataTableConverter.ConvertDataTable(data, this._processedCategory), this.gridControl, config);
      }
      StatesController.SetLoadState((object) this.ActualTabPage, true);
    }
  }

  public override void FormLostFocus(IFolder folder)
  {
    if (this._folder != folder as CustomFolder)
      return;
    this.SaveLayout();
  }

  private void SaveLayout()
  {
    MemoryStream ms = new MemoryStream();
    this.gridView.SaveLayoutToStream((Stream) ms);
    ConfigCache.SetConfig(this.configKey, ms);
  }

  private void gridView_DoubleClick(object sender, EventArgs e)
  {
    if (this.gridView.CalcHitInfo(this.gridView.GridControl.PointToClient(Control.MousePosition)).HitTest != GridHitTest.RowCell)
      return;
    this.DblClick(sender);
  }

  private void gridView_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != '\r')
      return;
    this.DblClick(sender);
  }

  private void DblClick(object sender)
  {
    string fieldName = Consts.KeyFieldByCategory(this._processedCategory);
    if (fieldName == string.Empty || this.gridView.GetChildRowCount(this.gridView.FocusedRowHandle) != 0)
      return;
    object rowCellValue = this.gridView.GetRowCellValue(this.gridView.FocusedRowHandle, this.gridView.Columns.ColumnByFieldName(fieldName));
    EventsHolder.FireJumpToAttribute4CustomType(sender, this.instGuid, new EventsHolder.JumpToAttribute4CustomTypeArgs(this._processedCategory, Convert.ToInt32(rowCellValue), (int) this._folder.Id));
  }

  /// <summary>id раздела справки</summary>
  public override string HelpTopicID => this._folder == null ? base.HelpTopicID : "1009";
}
