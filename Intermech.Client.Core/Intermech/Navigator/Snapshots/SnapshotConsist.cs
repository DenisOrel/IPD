
// Type: Intermech.Navigator.Snapshots.SnapshotConsist
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator.Snapshots;

public class SnapshotConsist : UserControl, IView
{
  /// <summary>id итерации</summary>
  private long snapshotID;
  /// <summary>
  /// id версии объекта, для которого показываем применяемость
  /// </summary>
  private long objectID;
  /// <summary>индекс  иконки</summary>
  private int _imageIndex = -1;
  private bool loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  protected internal iGrid grid;

  /// <summary>Порядковый номер закладки</summary>
  public int OrderID => 1;

  /// <summary>Заголовок закладки</summary>
  public string Caption => LocalizationHolder.rm.GetString("Client.Core_1406");

  /// <summary>индекс иконки</summary>
  public int ImageIndex
  {
    get
    {
      if (this._imageIndex < 0)
        this._imageIndex = Holder.NamedImageList.ImageIndex("imgContains");
      return this._imageIndex;
    }
  }

  public SnapshotConsist() => this.InitializeComponent();

  /// <summary>
  /// Выполняет инициализацию закладки после ее создания. Реализация
  /// этого метода должна работать быстро, т.е. все длительные операции
  /// желательно выполнять при первом вызове метода Activate.
  /// </summary>
  /// <param name="items">Коллекция выбранных пользователем элементов навигации.</param>
  /// <param name="provider">Контейнер сервисов, которыми может пользоваться закладка.</param>
  public void Initialize(ISelectedItems items, System.IServiceProvider provider)
  {
    this.loaded = false;
    SnapshotsNodeID itemData = (SnapshotsNodeID) items.GetItemData(0, typeof (SnapshotsNodeID));
    this.objectID = itemData.ObjectID;
    this.snapshotID = itemData.SnapshotID;
  }

  public void Activate(IView previousView)
  {
    if (this.loaded)
      return;
    this.LoadData();
    this.loaded = true;
  }

  /// <summary>Загрузка информации о составе</summary>
  private void LoadData()
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable dataTable = sessionKeeper.Session.GetSnapshot(this.snapshotID).ConsistFrom("F_OBJECT_ID");
      dataTable.Columns.Add("obj_Type", typeof (string));
      dataTable.Columns.Add("step_id", typeof (string));
      dataTable.Columns.Add("level_id", typeof (string));
      dataTable.Columns.Add("owner_id", typeof (string));
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        row["obj_Type"] = (object) MetaDataHelper.GetObjectTypeName(Convert.ToInt32(row["F_OBJECT_TYPE"]));
        row["step_id"] = (object) MetaDataHelper.GetLCStepName(Convert.ToInt32(row["F_LC_STEP"]));
        row["level_id"] = (object) MetaDataHelper.GetLCLevelName(Convert.ToInt32(row["F_LEVEL_ID"]));
        long int64 = Convert.ToInt64(row["F_OWNER_ID"]);
        row["owner_id"] = (object) sessionKeeper.Session.GetObjectInfo(int64).Caption;
      }
      this.grid.FillWithData(dataTable);
      this.grid.Cols["F_OBJECT_ID"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJECT_ID);
      this.grid.Cols["F_PROJECT_ID"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_PROJECT_ID);
      this.grid.Cols["F_MODIFICATION_ID"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_MODIFICATION_ID);
      this.grid.Cols["CAPTION"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.CAPTION);
      this.grid.Cols["F_VERSION_ID"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_VERSION_ID);
      this.grid.Cols["F_ID"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_ID);
      this.grid.Cols["F_OBJECT_TYPE"].Visible = false;
      this.grid.Cols["obj_Type"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJECT_TYPE);
      this.grid.Cols["obj_Type"].Order = 2;
      this.grid.Cols["F_OWNER_ID"].Visible = false;
      this.grid.Cols["owner_id"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OWNER_ID);
      this.grid.Cols["F_LC_STEP"].Visible = false;
      this.grid.Cols["step_id"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_LC_STEP);
      this.grid.Cols["step_id"].Order = 5;
      this.grid.Cols["F_LEVEL_ID"].Visible = false;
      this.grid.Cols["level_id"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_LEVEL_ID);
      this.grid.Cols["level_id"].Order = 6;
      this.grid.Cols["F_OBJ_CREATE"].Text = (object) ObligatoryObjectAttributesHelper.GetCaption(ObligatoryObjectAttributes.F_OBJ_CREATE);
      this.grid.Cols["F_OBJ_CREATE"].CellStyle.ValueType = typeof (DateTime);
      this.grid.AutoWidthColMode = iGAutoWidthColMode.HeaderAndCells;
      for (int index = 0; index < this.grid.Cols.Count; ++index)
        this.grid.Cols[index].AutoWidth(true);
    }
  }

  public void Deactivate(IView nextView)
  {
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (SnapshotConsist));
    this.grid = new iGrid();
    ((ISupportInitialize) this.grid).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.AllowDrop = true;
    this.grid.AutoWidthColMode = iGAutoWidthColMode.Cells;
    this.grid.BackColorEvenRows = SystemColors.Window;
    this.grid.BackColorOddRows = SystemColors.Window;
    this.grid.Cursor = Cursors.Default;
    this.grid.DefaultAutoGroupRow.Height = 21;
    this.grid.DefaultCol.Key = componentResourceManager.GetString("resource.Key");
    this.grid.DefaultCol.MaxWidth = (int) componentResourceManager.GetObject("resource.MaxWidth");
    this.grid.DefaultCol.MinWidth = (int) componentResourceManager.GetObject("resource.MinWidth");
    this.grid.DefaultCol.Text = componentResourceManager.GetObject("resource.Text");
    this.grid.DefaultCol.Width = (int) componentResourceManager.GetObject("resource.Width");
    this.grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this.grid.DefaultRow.Key = componentResourceManager.GetString("resource.Key1");
    this.grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this.grid.FrozenArea.SortFrozenRows = true;
    this.grid.GroupBox.BackColor = SystemColors.AppWorkspace;
    this.grid.GroupBox.HintBackColor = SystemColors.AppWorkspace;
    this.grid.GroupBox.HintForeColor = SystemColors.ControlText;
    this.grid.GroupBox.Text = componentResourceManager.GetString("grid.GroupBox.Text");
    this.grid.GroupBox.Visible = true;
    this.grid.Header.AutoHeightFlags = iGHdrAutoHeightFlags.OnAddCol | iGHdrAutoHeightFlags.OnRemoveCol | iGHdrAutoHeightFlags.OnShowCol | iGHdrAutoHeightFlags.OnContentsChange | iGHdrAutoHeightFlags.OnThemeChange | iGHdrAutoHeightFlags.OnResizeCol;
    this.grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this.grid.HighlightBackColorNoFocus = SystemColors.Highlight;
    this.grid.HighlightForeColorNoFocus = SystemColors.HighlightText;
    this.grid.HotTracking = false;
    this.grid.LayoutObject.Flags = iGLayoutFlags.Grouping | iGLayoutFlags.Sorting | iGLayoutFlags.ColVisibility | iGLayoutFlags.ColWidth | iGLayoutFlags.ColOrder;
    this.grid.Name = "grid";
    this.grid.PageCapacity = 500;
    this.grid.PressedMouseMoveMode = iGPressedMouseMoveMode.Normal;
    this.grid.ProcessTab = false;
    this.grid.ReadOnly = true;
    this.grid.RowMode = true;
    this.grid.RowModeHasCurCell = true;
    this.grid.RowTextStartColNear = 211;
    this.grid.SelectionMode = iGSelectionMode.MultiExtended;
    this.grid.ShowControlsInAllCells = false;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.grid);
    this.Name = nameof (SnapshotConsist);
    ((ISupportInitialize) this.grid).EndInit();
    this.ResumeLayout(false);
  }
}
