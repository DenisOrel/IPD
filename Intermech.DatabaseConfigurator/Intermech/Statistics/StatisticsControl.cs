// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.StatisticsControl
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using DevExpress.IM.Utils;
using DevExpress.IM.XtraGrid;
using DevExpress.IM.XtraGrid.Columns;
using DevExpress.IM.XtraGrid.Views.Base;
using DevExpress.IM.XtraGrid.Views.Grid;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.DatabaseConfigurator;
using Intermech.Docking;
using Intermech.Holders;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Statistics;

public class StatisticsControl : DockControl, ICommandTarget
{
  internal static Guid _statisticsControlGuid = new Guid("{bf58f27a-0dfd-4ed0-a95c-5fe977b399eb}");
  private ColumnFilterInfo allFilterInfo = new ColumnFilterInfo();
  private ColumnFilterInfo excludeObligatoryFilterInfo = new ColumnFilterInfo("[F_ATTRIBUTE_ID]>0", LocalizationHolder.rm.GetString("DatabaseConfigurator_111"));
  private DockManager _dockManager;
  private bool loaded;
  private DataTable dataTableLoaded;
  private bool showObligatory;
  private bool blockOnSwitch;
  private bool blockOnObligatory;
  private IContainer components;
  private GridControl gridControl;
  private GridView gridView;
  private Intermech.Bars.ToolBar toolBar;
  private ButtonItem reportButtonItem;
  private ButtonItem switchButtonItem;
  private ButtonItem clearButtonItem;
  private ButtonItem saveButtonItem;
  private ButtonItem viewButtonItem;
  private ButtonItem obligatoryButtonItem;
  private OpenFileDialog openFileDialog;
  private SaveFileDialog saveFileDialog;
  private ContextMenuStrip contextMenuStrip;
  private ToolStripMenuItem RefreshToolStripMenuItem;

  public StatisticsControl()
  {
    this.InitializeComponent();
    this.Guid = StatisticsControl._statisticsControlGuid;
    if (ServicesManager.GetService(typeof (INamedImageList)) is INamedImageList service)
    {
      this.TabImageIndex = service.ImageIndex("imgPerformance");
      this.toolBar.ImageList = service.ImageList;
      this.reportButtonItem.ImageIndex = service.ImageIndex("imgOutput");
      this.clearButtonItem.ImageIndex = service.ImageIndex("imgClearAll");
      this.viewButtonItem.ImageIndex = service.ImageIndex("imgView");
      this.saveButtonItem.ImageIndex = service.ImageIndex("imgSave");
    }
    this.AssignObligatoryButtonItem(this.showObligatory);
  }

  private void StatisticsControl_Load(object sender, EventArgs e)
  {
    this._dockManager = (DockManager) ServicesManager.GetService(typeof (DockManager));
    this._dockManager.DocumentContainer.ActiveDocumentChanged += new ActiveDocumentEventHandler(this.DocumentContainer_ActiveDocumentChanged);
  }

  private void StatisticsControl_Closing(object sender, CancelEventArgs e)
  {
    if (this._dockManager == null)
      return;
    this._dockManager.DocumentContainer.ActiveDocumentChanged -= new ActiveDocumentEventHandler(this.DocumentContainer_ActiveDocumentChanged);
  }

  private void DocumentContainer_ActiveDocumentChanged(object sender, ActiveDocumentEventArgs e)
  {
    if (e.NewActiveDocument.Equals((object) this))
    {
      this.SetMenuCommandsVisibility(true);
      if (this.loaded)
        return;
      this.LoadControlData((DataTable) null);
    }
    else
      this.SetMenuCommandsVisibility(false);
  }

  private void SetMenuCommandsVisibility(bool p)
  {
  }

  private void LoadControlData(DataTable dataTableToView)
  {
    this.LoadControlData(dataTableToView, false);
  }

  private void LoadControlData(DataTable dataTableToView, bool afterRefresh)
  {
    this.loaded = true;
    if (!afterRefresh)
      this.showObligatory = false;
    this.AssignObligatoryButtonItem(this.showObligatory);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      MemoryStream ms;
      if (!afterRefresh)
      {
        ms = ConfigCache.GetConfig(Statics.CategoryStatisticsGUID);
      }
      else
      {
        ms = new MemoryStream();
        this.gridView.SaveLayoutToStream((Stream) ms);
      }
      try
      {
        this.dataTableLoaded = dataTableToView == null ? customService.GetOptimizerStatistics(sessionKeeper.Session.SessionGUID) : dataTableToView;
        if (dataTableToView == null)
          this.ExtendDataTable(this.dataTableLoaded);
        DataTableConverter.ApplyToGridControl(DataTableConverter.ConvertDataTable(this.dataTableLoaded, Statics.CategoryStatistics), this.gridControl, ms);
        this.HideIDFields((GridView) this.gridControl.MainView);
      }
      finally
      {
        if (afterRefresh)
          ms.Close();
      }
      this.showObligatory = this.obligatoryButtonItem.Checked;
      this.ApplyFilter();
      bool optimizerStatisticsFlag = customService.GetOptimizerStatisticsFlag();
      (this.gridControl.MainView as GridView).ColumnPanelRowHeight = -1;
      this.HeaderSize();
      this.blockOnSwitch = true;
      try
      {
        this.switchButtonItem.Checked = optimizerStatisticsFlag;
      }
      finally
      {
        this.blockOnSwitch = false;
      }
    }
  }

  private void HeaderSize()
  {
    GridView mainView = this.gridControl.MainView as GridView;
    bool flag = false;
    using (Graphics graphics = this.gridControl.CreateGraphics())
    {
      Font font = mainView.ViewStylesInfo.HeaderPanel.Font;
      for (int index = 0; index < mainView.VisibleColumns.Count; ++index)
      {
        if ((int) graphics.MeasureString(mainView.VisibleColumns[index].Caption, font).Width / (mainView.VisibleColumns[index].Width - SystemInformation.VerticalScrollBarWidth - 2 * SystemInformation.BorderSize.Width - 2 * SystemInformation.VerticalResizeBorderThickness) > 0)
        {
          flag = true;
          break;
        }
      }
    }
    if (flag)
      mainView.ColumnPanelRowHeight = 30;
    else
      mainView.ColumnPanelRowHeight = -1;
  }

  private void HideIDFields(GridView gridView)
  {
    if (gridView.Columns == null || gridView.Columns.Count == 0)
      return;
    int num = gridView.Columns["F_ATTRIBUTE_ID"].VisibleIndex != -1 || gridView.Columns["F_OBJECT_TYPE"].VisibleIndex != -1 ? 1 : (gridView.Columns["F_RELATION_TYPE"].VisibleIndex != -1 ? 1 : 0);
    gridView.Columns["F_ATTRIBUTE_ID"].VisibleIndex = -1;
    gridView.Columns["F_OBJECT_TYPE"].VisibleIndex = -1;
    gridView.Columns["F_RELATION_TYPE"].VisibleIndex = -1;
    if (num == 0)
      return;
    gridView.Columns["F_ATTRIBUTE_ID_STR"].VisibleIndex = 0;
    gridView.Columns["F_OBJECT_TYPE_STR"].VisibleIndex = 1;
    gridView.Columns["F_RELATION_TYPE_STR"].VisibleIndex = 2;
  }

  private void ExtendDataTable(DataTable dataTableLoaded)
  {
    dataTableLoaded.Columns.AddRange(new DataColumn[4]
    {
      new DataColumn("F_ATTRIBUTE_ID_STR", typeof (string)),
      new DataColumn("F_OBJECT_TYPE_STR", typeof (string)),
      new DataColumn("F_RELATION_TYPE_STR", typeof (string)),
      new DataColumn("F_RECOMMENDED", typeof (string))
    });
    dataTableLoaded.Columns["F_ATTRIBUTE_ID_STR"].Caption = dataTableLoaded.Columns["F_ATTRIBUTE_ID"].Caption;
    dataTableLoaded.Columns["F_OBJECT_TYPE_STR"].Caption = dataTableLoaded.Columns["F_OBJECT_TYPE"].Caption;
    dataTableLoaded.Columns["F_RELATION_TYPE_STR"].Caption = dataTableLoaded.Columns["F_RELATION_TYPE"].Caption;
    dataTableLoaded.Columns["F_RECOMMENDED"].Caption = LocalizationHolder.rm.GetString("DatabaseConfigurator_214");
    foreach (DataRow row in (InternalDataCollectionBase) dataTableLoaded.Rows)
    {
      int int32_1 = Convert.ToInt32(row["F_ATTRIBUTE_ID"]);
      row["F_ATTRIBUTE_ID_STR"] = (object) new AttributePropertyClass(int32_1).ToString();
      int int32_2 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      if (int32_2 != -1)
        row["F_OBJECT_TYPE_STR"] = (object) new ObjectTypePropertyClass(int32_2).ToString();
      int int32_3 = Convert.ToInt32(row["F_RELATION_TYPE"]);
      if (int32_3 != -1)
        row["F_RELATION_TYPE_STR"] = (object) new RelationTypePropertyClass(int32_3).ToString();
      row["F_RECOMMENDED"] = (object) this.RecommendedOptimiazation(row);
    }
  }

  private void StatisticsControl_Enter(object sender, EventArgs e)
  {
    if (!((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
      return;
    this.blockOnSwitch = true;
    try
    {
      this.switchButtonItem.Checked = customService.GetOptimizerStatisticsFlag();
    }
    finally
    {
      this.blockOnSwitch = false;
    }
  }

  private void StatisticsControl_Leave(object sender, EventArgs e)
  {
  }

  public bool QueryStatus(ICommandState commandState)
  {
    if (!(commandState.CommandName == "Refresh"))
      return false;
    commandState.Enabled = true;
    return true;
  }

  public bool Execute(ICommandState commandState)
  {
    if (!(commandState.CommandName == "Refresh"))
      return false;
    this.RefreshControlData();
    return true;
  }

  private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.RefreshControlData();
  }

  private void RefreshControlData() => this.LoadControlData((DataTable) null, true);

  private void reportButtonItem_Click(object sender, EventArgs e)
  {
    new StatisticsReportForm().ShowReport(this.CreateReport(this.dataTableLoaded));
  }

  private List<string> CreateReport(DataTable dataTableLoaded)
  {
    List<string> report = new List<string>();
    report.Add(LocalizationHolder.rm.GetString("DatabaseConfigurator_112"));
    report.Add("==== " + DateTime.Now.ToString());
    report.Add(string.Empty);
    int num = 0;
    foreach (DataRow dr in dataTableLoaded.Select("F_ATTRIBUTE_ID>0 AND NOT ( F_OBJECT_TYPE=-1 AND F_RELATION_TYPE=-1)", "F_ATTRIBUTE_ID_STR"))
    {
      if (Convert.ToInt32(dr["F_OBJECT_TYPE"]) != -1)
      {
        long int64_1 = Convert.ToInt64(dr["F_SEEK_DURATION"]);
        long int64_2 = Convert.ToInt64(dr["F_READ_DURATION"]);
        long int64_3 = Convert.ToInt64(dr["F_WRITE_DURATION"]);
        OptimizationModes int64_4 = (OptimizationModes) Convert.ToInt64(dr["F_OPTIMIZED"]);
        if (int64_1 > int64_2 && int64_1 > int64_3 && int64_1 - this.Max(int64_2, int64_3) > int64_1 * (long) CoreConsts.StatisticsSensitivity / 100L && int64_4 != OptimizationModes.Seek)
          this.AddToReport(++num, dr, OptimizationModes.Seek, report);
        else if (int64_2 > int64_1 && int64_2 > int64_3 && int64_2 - this.Max(int64_1, int64_3) > int64_2 * (long) CoreConsts.StatisticsSensitivity / 100L && int64_4 != OptimizationModes.Read)
          this.AddToReport(++num, dr, OptimizationModes.Read, report);
        else if (int64_3 > int64_1 && int64_3 > int64_2 && int64_3 - this.Max(int64_1, int64_2) > int64_3 * (long) CoreConsts.StatisticsSensitivity / 100L && int64_4 != OptimizationModes.Write)
          this.AddToReport(++num, dr, OptimizationModes.Write, report);
      }
    }
    return report;
  }

  private string RecommendedOptimiazation(DataRow dataRow)
  {
    string str = string.Empty;
    if (Convert.ToInt32(dataRow["F_ATTRIBUTE_ID"]) > 0 && (Convert.ToInt32(dataRow["F_OBJECT_TYPE"]) != -1 || Convert.ToInt32(dataRow["F_RELATION_TYPE"]) != -1))
    {
      long int64_1 = Convert.ToInt64(dataRow["F_SEEK_DURATION"]);
      long int64_2 = Convert.ToInt64(dataRow["F_READ_DURATION"]);
      long int64_3 = Convert.ToInt64(dataRow["F_WRITE_DURATION"]);
      OptimizationModes int64_4 = (OptimizationModes) Convert.ToInt64(dataRow["F_OPTIMIZED"]);
      if (int64_1 > int64_2 && int64_1 > int64_3 && int64_1 - this.Max(int64_2, int64_3) > int64_1 * (long) CoreConsts.StatisticsSensitivity / 100L && int64_4 != OptimizationModes.Seek)
        str = OptimizationModesHelper.GetCaption(OptimizationModes.Seek);
      else if (int64_2 > int64_1 && int64_2 > int64_3 && int64_2 - this.Max(int64_1, int64_3) > int64_2 * (long) CoreConsts.StatisticsSensitivity / 100L && int64_4 != OptimizationModes.Read)
        str = OptimizationModesHelper.GetCaption(OptimizationModes.Read);
      else if (int64_3 > int64_1 && int64_3 > int64_2 && int64_3 - this.Max(int64_1, int64_2) > int64_3 * (long) CoreConsts.StatisticsSensitivity / 100L && int64_4 != OptimizationModes.Write)
        str = OptimizationModesHelper.GetCaption(OptimizationModes.Write);
    }
    return str;
  }

  private long Max(long l1, long l2) => l1 > l2 ? l1 : l2;

  private void AddToReport(int ndx, DataRow dr, OptimizationModes opt, List<string> report)
  {
    string str1 = string.Format($"{ndx.ToString()}. {LocalizationHolder.rm.GetString("DatabaseConfigurator_113")}", (object) dr["F_ATTRIBUTE_ID_STR"].ToString());
    if (Convert.ToInt32(dr["F_OBJECT_TYPE"]) != -1)
      str1 += string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_114"), (object) dr["F_OBJECT_TYPE_STR"].ToString());
    if (Convert.ToInt32(dr["F_RELATION_TYPE"]) != -1)
      str1 += string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_115"), (object) dr["F_RELATION_TYPE_STR"].ToString());
    report.Add(str1);
    string str2 = ClientCommons.StrFormatTimeSpan(TimeSpan.FromMilliseconds((double) Convert.ToInt64(dr["F_SEEK_DURATION"])));
    string str3 = ClientCommons.StrFormatTimeSpan(TimeSpan.FromMilliseconds((double) Convert.ToInt64(dr["F_READ_DURATION"])));
    string str4 = ClientCommons.StrFormatTimeSpan(TimeSpan.FromMilliseconds((double) Convert.ToInt64(dr["F_WRITE_DURATION"])));
    string caption = OptimizationModesHelper.GetCaption((OptimizationModes) Convert.ToInt32(dr["F_OPTIMIZED"]));
    string str5 = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_116"), (object) str2, (object) str3, (object) str4, (object) caption);
    report.Add(str5);
    string str6 = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_117"), (object) OptimizationModesHelper.GetCaption(opt));
    report.Add(str6);
    report.Add(string.Empty);
  }

  private void switchButtonItem_Click(object sender, EventArgs e)
  {
    if (this.blockOnSwitch)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      try
      {
        bool optimizerStatisticsFlag = customService.GetOptimizerStatisticsFlag();
        if (optimizerStatisticsFlag != this.switchButtonItem.Checked && MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_ToggleStatFlag"), LocalizationHolder.rm.GetString("DatabaseConfigurator_Attention"), MessageBoxButtons.YesNo) == DialogResult.No)
        {
          this.blockOnSwitch = true;
          try
          {
            this.switchButtonItem.Checked = optimizerStatisticsFlag;
          }
          finally
          {
            this.blockOnSwitch = false;
          }
        }
        else
        {
          customService.SetOptimizerStatisticsFlag(this.switchButtonItem.Checked, sessionKeeper.Session.SessionGUID);
          this.blockOnSwitch = true;
          try
          {
            this.switchButtonItem.Checked = customService.GetOptimizerStatisticsFlag();
          }
          finally
          {
            this.blockOnSwitch = false;
          }
        }
      }
      catch (Exception ex)
      {
        ExceptionHelper.ExceptionService.ShowException(ex);
        this.blockOnSwitch = true;
        try
        {
          this.switchButtonItem.Checked = customService.GetOptimizerStatisticsFlag();
        }
        finally
        {
          this.blockOnSwitch = false;
        }
      }
    }
  }

  private void clearButtonItem_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_118"), MessageDialogs.msgConfirmDelete, MessageBoxButtons.YesNo) != DialogResult.Yes)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService))
        return;
      customService.ClearStatistics(sessionKeeper.Session.SessionGUID);
      this.LoadControlData((DataTable) null);
    }
  }

  private void gridView_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
  {
    if (!(e.Column.FieldName == "F_SEEK_DURATION") && !(e.Column.FieldName == "F_READ_DURATION") && !(e.Column.FieldName == "F_WRITE_DURATION"))
      return;
    string str = e.DisplayText;
    try
    {
      str = ClientCommons.StrFormatTimeSpan(TimeSpan.FromMilliseconds((double) Convert.ToInt64(e.CellValue)));
    }
    catch
    {
    }
    e.DisplayText = str;
  }

  private void gridView_LostFocus(object sender, EventArgs e)
  {
    if (!this.loaded)
      return;
    MemoryStream ms = new MemoryStream();
    this.gridView.SaveLayoutToStream((Stream) ms);
    ConfigCache.SetConfig(Statics.CategoryStatisticsGUID, ms);
  }

  private void obligatoryButtonItem_Click(object sender, EventArgs e)
  {
    if (this.blockOnObligatory)
      return;
    this.showObligatory = this.obligatoryButtonItem.Checked;
    this.AssignObligatoryButtonItem(this.showObligatory);
    this.ApplyFilter();
  }

  private void AssignObligatoryButtonItem(bool b)
  {
    this.blockOnObligatory = true;
    try
    {
      this.obligatoryButtonItem.Checked = b;
    }
    finally
    {
      this.blockOnObligatory = false;
    }
  }

  private void ApplyFilter()
  {
    if (this.gridView.Columns == null || this.gridView.Columns.Count <= 0)
      return;
    if (this.obligatoryButtonItem.Checked)
      this.gridView.Columns["F_ATTRIBUTE_ID"].FilterInfo = this.allFilterInfo;
    else
      this.gridView.Columns["F_ATTRIBUTE_ID"].FilterInfo = this.excludeObligatoryFilterInfo;
  }

  private void saveButtonItem_Click(object sender, EventArgs e)
  {
    if (!this.loaded || this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.dataTableLoaded.WriteXml(this.saveFileDialog.FileName);
    string fileName = this.saveFileDialog.FileName + ".xms";
    this.dataTableLoaded.WriteXmlSchema(fileName);
    int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_119") + this.saveFileDialog.FileName + LocalizationHolder.rm.GetString("DatabaseConfigurator_120") + LocalizationHolder.rm.GetString("DatabaseConfigurator_121") + fileName, MessageDialogs.msgInformation, MessageBoxButtons.OK);
  }

  private void viewButtonItem_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    DataTable dataTableToView = new DataTable();
    string str = this.openFileDialog.FileName + ".xms";
    if (!File.Exists(str))
    {
      int num1 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_122") + str, MessageDialogs.msgError, MessageBoxButtons.OK);
    }
    else
    {
      dataTableToView.ReadXmlSchema(str);
      int num2 = (int) dataTableToView.ReadXml(this.openFileDialog.FileName);
      this.LoadControlData(dataTableToView);
    }
  }

  private void gridView_ColumnFilterChanged(object sender, EventArgs e)
  {
    this.AssignObligatoryButtonItem(this.showObligatory);
  }

  private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
  {
    DataRow dataRow = this.gridView.GetDataRow(e.RowHandle);
    if (e.RowHandle == this.gridView.FocusedRowHandle || Convert.ToString(dataRow["F_RECOMMENDED"]).Equals(string.Empty) || !(Convert.ToString(dataRow["F_RECOMMENDED"]) != Convert.ToString(dataRow["F_OPTIMIZED"])))
      return;
    e.CellStyle = this.gridControl.Styles["Style1"];
  }

  private void gridControl_PaddingChanged(object sender, EventArgs e) => this.HeaderSize();

  private void gridControl_MarginChanged(object sender, EventArgs e) => this.HeaderSize();

  private void gridControl_MouseCaptureChanged(object sender, EventArgs e) => this.HeaderSize();

  public override string HelpID => "1131";

  private void gridView_DoubleClick(object sender, EventArgs e)
  {
    DockManager service = (DockManager) ServicesManager.GetService(typeof (DockManager));
    DatabaseConfiguratorControl configuratorControl = (DatabaseConfiguratorControl) null;
    if (service == null)
      return;
    DockControl[] dockControls = service.GetDockControls();
    bool flag = false;
    foreach (DockControl dockControl in dockControls)
    {
      if (dockControl is DatabaseConfiguratorControl)
      {
        configuratorControl = dockControl as DatabaseConfiguratorControl;
        flag = true;
        break;
      }
    }
    if (!flag)
      configuratorControl = new DatabaseConfiguratorControl();
    configuratorControl.Show(service);
    configuratorControl.Activate();
    int focusedRowHandle = this.gridView.FocusedRowHandle;
    GridColumn column1 = this.gridView.Columns["F_ATTRIBUTE_ID"];
    GridColumn column2 = this.gridView.Columns["F_OBJECT_TYPE"];
    GridColumn column3 = this.gridView.Columns["F_RELATION_TYPE"];
    int int32_1 = Convert.ToInt32(this.gridView.GetRowCellValue(focusedRowHandle, column1));
    int int32_2 = Convert.ToInt32(this.gridView.GetRowCellValue(focusedRowHandle, column2));
    int int32_3 = Convert.ToInt32(this.gridView.GetRowCellValue(focusedRowHandle, column3));
    IFolder iFolder;
    EventsHolder.FolderArgs e1;
    if (int32_3 != -1)
    {
      iFolder = (IFolder) configuratorControl.RootRelationTypesFolder;
      e1 = new EventsHolder.FolderArgs(6, (object) int32_3, iFolder);
    }
    else if (int32_2 != -1)
    {
      iFolder = this.GetParentFolder((IFolder) configuratorControl.RootObjectTypesFolder);
      e1 = new EventsHolder.FolderArgs(4, (object) int32_2, iFolder);
    }
    else
    {
      iFolder = this.GetParentFolder((IFolder) configuratorControl.RootAttributesFolder);
      e1 = new EventsHolder.FolderArgs(3, (object) int32_1, iFolder);
    }
    if (iFolder == null)
      return;
    EventsHolder.FireFolderDClick(sender, configuratorControl.InstGuid, e1);
    TreeNode focusedNode = configuratorControl.GetFocusedNode();
    if (focusedNode == null || focusedNode.Tag == null || !(focusedNode.Tag is IFolder tag))
      return;
    BaseTabPage baseTabPage1;
    switch (tag)
    {
      case RelationTypeFolder _:
        baseTabPage1 = (BaseTabPage) TabPagesHolder.TabPages(configuratorControl.InstGuid).Attr4RelTypeTabPage;
        break;
      case ObjectTypeFolder _:
        baseTabPage1 = (BaseTabPage) TabPagesHolder.TabPages(configuratorControl.InstGuid).Attr4ObjTypeTabPage;
        break;
      default:
        baseTabPage1 = (BaseTabPage) TabPagesHolder.TabPages(configuratorControl.InstGuid).PropertyTabPage;
        break;
    }
    BaseTabPage baseTabPage2 = baseTabPage1;
    if (tag.PropertiesForm is IConfigPage propertiesForm && propertiesForm.TabControl != null)
    {
      TabControlProcessor.BlockTabPageChangedEvent = true;
      try
      {
        propertiesForm.TabControl.SelectedTab = (System.Windows.Forms.TabPage) baseTabPage2;
      }
      finally
      {
        TabControlProcessor.BlockTabPageChangedEvent = false;
      }
      propertiesForm.OpenTabPage((System.Windows.Forms.TabPage) baseTabPage2);
    }
    ITabPageForm pageProcessingForm = baseTabPage2.TabPageProcessingForm;
    if (!(pageProcessingForm is IPositionAssigner))
      return;
    (pageProcessingForm as IPositionAssigner).SetPositionAt(3, (object) int32_1);
  }

  private IFolder GetParentFolder(IFolder rootFolder)
  {
    IFolder parentFolder = (IFolder) null;
    TreeNode node1 = rootFolder.Node;
    if (!node1.IsExpanded)
      rootFolder.Populate(false);
    foreach (TreeNode node2 in node1.Nodes)
    {
      if (node2.Tag is IFolder tag && Convert.ToInt32(tag.Id) == -1)
      {
        parentFolder = tag;
        break;
      }
    }
    return parentFolder;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (StatisticsControl));
    this.gridControl = new GridControl();
    this.contextMenuStrip = new ContextMenuStrip(this.components);
    this.RefreshToolStripMenuItem = new ToolStripMenuItem();
    this.gridView = new GridView();
    this.toolBar = new Intermech.Bars.ToolBar();
    this.reportButtonItem = new ButtonItem();
    this.clearButtonItem = new ButtonItem();
    this.saveButtonItem = new ButtonItem();
    this.viewButtonItem = new ButtonItem();
    this.switchButtonItem = new ButtonItem();
    this.obligatoryButtonItem = new ButtonItem();
    this.openFileDialog = new OpenFileDialog();
    this.saveFileDialog = new SaveFileDialog();
    this.gridControl.BeginInit();
    this.contextMenuStrip.SuspendLayout();
    this.gridView.BeginInit();
    this.SuspendLayout();
    this.gridControl.ContextMenuStrip = this.contextMenuStrip;
    componentResourceManager.ApplyResources((object) this.gridControl, "gridControl");
    this.gridControl.EmbeddedNavigator.Name = "";
    this.gridControl.MainView = (BaseView) this.gridView;
    this.gridControl.Name = "gridControl";
    this.gridControl.Styles.AddReplace("Style1", (object) new ViewStyleEx("Style1", "", new Font("Microsoft Sans Serif", 8.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 204), Color.NavajoWhite, SystemColors.WindowText, Color.FloralWhite, LinearGradientMode.Vertical));
    this.gridControl.Styles.AddReplace("HeaderPanel", (object) new ViewStyleEx("HeaderPanel", "Grid", "", true, true, false, HorzAlignment.Default, VertAlignment.Top, (Image) null, SystemColors.Control, SystemColors.ControlText, Color.Empty, LinearGradientMode.Horizontal));
    this.gridControl.MarginChanged += new EventHandler(this.gridControl_MarginChanged);
    this.gridControl.PaddingChanged += new EventHandler(this.gridControl_PaddingChanged);
    this.gridControl.MouseCaptureChanged += new EventHandler(this.gridControl_MouseCaptureChanged);
    this.contextMenuStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.RefreshToolStripMenuItem
    });
    this.contextMenuStrip.Name = "contextMenuStrip";
    componentResourceManager.ApplyResources((object) this.contextMenuStrip, "contextMenuStrip");
    this.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem";
    componentResourceManager.ApplyResources((object) this.RefreshToolStripMenuItem, "RefreshToolStripMenuItem");
    this.RefreshToolStripMenuItem.Click += new EventHandler(this.RefreshToolStripMenuItem_Click);
    this.gridView.GridControl = this.gridControl;
    componentResourceManager.ApplyResources((object) this.gridView, "gridView");
    this.gridView.Name = "gridView";
    this.gridView.OptionsBehavior.Editable = false;
    this.gridView.OptionsSelection.MultiSelect = true;
    this.gridView.OptionsView.ColumnAutoWidth = false;
    this.gridView.OptionsView.ShowFilterPanel = false;
    this.gridView.CustomDrawCell += new RowCellCustomDrawEventHandler(this.gridView_CustomDrawCell);
    this.gridView.DoubleClick += new EventHandler(this.gridView_DoubleClick);
    this.gridView.LostFocus += new EventHandler(this.gridView_LostFocus);
    this.gridView.RowCellStyle += new RowCellStyleEventHandler(this.gridView_RowCellStyle);
    this.gridView.ColumnFilterChanged += new EventHandler(this.gridView_ColumnFilterChanged);
    this.toolBar.FullMenus = true;
    this.toolBar.Guid = new Guid("d338a73f-1221-40d7-948c-fcc86f85edf2");
    this.toolBar.Hidden = false;
    this.toolBar.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.reportButtonItem,
      (ToolbarItemBase) this.clearButtonItem,
      (ToolbarItemBase) this.saveButtonItem,
      (ToolbarItemBase) this.viewButtonItem,
      (ToolbarItemBase) this.switchButtonItem,
      (ToolbarItemBase) this.obligatoryButtonItem
    });
    componentResourceManager.ApplyResources((object) this.toolBar, "toolBar");
    this.toolBar.Name = "toolBar";
    componentResourceManager.ApplyResources((object) this.reportButtonItem, "reportButtonItem");
    this.reportButtonItem.Visible = false;
    this.reportButtonItem.Click += new EventHandler(this.reportButtonItem_Click);
    this.clearButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.clearButtonItem, "clearButtonItem");
    this.clearButtonItem.Click += new EventHandler(this.clearButtonItem_Click);
    this.saveButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.saveButtonItem, "saveButtonItem");
    this.saveButtonItem.Click += new EventHandler(this.saveButtonItem_Click);
    componentResourceManager.ApplyResources((object) this.viewButtonItem, "viewButtonItem");
    this.viewButtonItem.Click += new EventHandler(this.viewButtonItem_Click);
    this.switchButtonItem.AutoToggle = AutoToggleType.Single;
    this.switchButtonItem.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.switchButtonItem, "switchButtonItem");
    this.switchButtonItem.ShowText = true;
    this.switchButtonItem.Click += new EventHandler(this.switchButtonItem_Click);
    this.obligatoryButtonItem.AutoToggle = AutoToggleType.Single;
    componentResourceManager.ApplyResources((object) this.obligatoryButtonItem, "obligatoryButtonItem");
    this.obligatoryButtonItem.ShowText = true;
    this.obligatoryButtonItem.Click += new EventHandler(this.obligatoryButtonItem_Click);
    this.openFileDialog.DefaultExt = "xml";
    componentResourceManager.ApplyResources((object) this.openFileDialog, "openFileDialog");
    this.openFileDialog.RestoreDirectory = true;
    this.saveFileDialog.DefaultExt = "xml";
    componentResourceManager.ApplyResources((object) this.saveFileDialog, "saveFileDialog");
    this.saveFileDialog.RestoreDirectory = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.gridControl);
    this.Controls.Add((Control) this.toolBar);
    this.HideOnClose = true;
    this.Name = nameof (StatisticsControl);
    this.PersistState = false;
    this.Load += new EventHandler(this.StatisticsControl_Load);
    this.Leave += new EventHandler(this.StatisticsControl_Leave);
    this.Enter += new EventHandler(this.StatisticsControl_Enter);
    this.Closing += new System.ComponentModel.CancelEventHandler(this.StatisticsControl_Closing);
    this.gridControl.EndInit();
    this.contextMenuStrip.ResumeLayout(false);
    this.gridView.EndInit();
    this.ResumeLayout(false);
  }
}
