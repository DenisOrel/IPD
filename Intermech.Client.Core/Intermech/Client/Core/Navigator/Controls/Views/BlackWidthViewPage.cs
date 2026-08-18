
// Type: Intermech.Client.Core.Navigator.Controls.Views.BlackWidthViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core.Navigator.Controls.Views;

/// <summary>Закладка для окна "Настройки", управляющая цветом для DWG</summary>
public class BlackWidthViewPage : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _provider;
  /// <summary>использование цвета в чертеже</summary>
  private const string USED = "USED";
  /// <summary>номер цвета в чертеже</summary>
  private const string INDEX = "INDEX";
  /// <summary> цвет в чертеже</summary>
  private const string COLOR = "COLOR";
  /// <summary>толина для цвета в чертеже</summary>
  private const string WIDTH = "WIDTH";
  /// <summary>все цвета привести к чёрному</summary>
  private bool _allColorToBlack;
  /// <summary>
  /// Ширина колонки для определённого типа.
  /// [KEY колонки] =&gt; [Ширина колонки]
  /// </summary>
  private Dictionary<string, int> colWidths = new Dictionary<string, int>();
  /// <summary>изменились настйроки</summary>
  private bool isSettingsChanged;
  /// <summary></summary>
  private bool isChanged;
  /// <summary></summary>
  private bool loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private CheckBox cbBlackColor;
  private iGrid grid;
  private iGCellStyle iGrid1DefaultCellStyle1;
  private iGColHdrStyle iGrid1DefaultColHdrStyle1;
  private iGCellStyle iGrid1RowTextColCellStyle1;

  /// <summary>Путь к странице свойств в дереве навигации</summary>
  public static string GetPath()
  {
    return $"{LocalizationHolder.rm.GetString("Client.Core_1118")}\\{LocalizationHolder.rm.GetString("Client.Core_1693")}";
  }

  /// <summary>Событие изменения на закладке</summary>
  public event EventHandler Changed;

  /// <summary>Событие будет дёргаться при необходимости</summary>
  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.isChanged = true;
    this.Changed((object) this, new EventArgs());
  }

  /// <summary></summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>Контрол, который будет размещён на главной форме настроек</summary>
  public object Control => (object) this;

  /// <summary>Название странички в главной форме настроек</summary>
  public string PageName => BlackWidthViewPage.GetPath();

  /// <summary>Применить изменения редактора</summary>
  public void Apply()
  {
    if (this.isChanged)
    {
      IBlackWidthService service = (IBlackWidthService) ServicesManager.GetService(typeof (IBlackWidthService));
      if (service != null)
      {
        service.AllColorToBlack = this._allColorToBlack = this.cbBlackColor.Checked;
        if (this.isSettingsChanged)
        {
          foreach (iGRow row in (IEnumerable) this.grid.Rows)
          {
            ColorWidth colorWidth = service[(byte) row.Cells["INDEX"].Value];
            colorWidth.Used = (bool) row.Cells["USED"].Value;
            colorWidth.Width = (float) row.Cells["WIDTH"].Value;
          }
        }
        service.SaveSettings();
      }
    }
    this.isSettingsChanged = this.isChanged = false;
  }

  /// <summary>Отменить изменения редактора</summary>
  public void Cancel()
  {
    this.isChanged = this.isSettingsChanged = false;
    this.LoadExtensionsSettgins();
    this.UpdateControls();
  }

  /// <summary></summary>
  public string HelpTopicID => string.Empty;

  /// <summary>Текст заголовка (пустое значение - заголовок не отображается)</summary>
  public string HeaderText => LocalizationHolder.rm.GetString("Client.Core_1693");

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>обновить состояние контролов</summary>
  private void UpdateControls()
  {
  }

  public static BlackWidthViewPage Instance { get; set; }

  /// <summary></summary>
  /// <param name="provider">Контейнер сервисов</param>
  public BlackWidthViewPage(System.IServiceProvider provider)
  {
    BlackWidthViewPage.Instance = this;
    this.InitializeComponent();
    if (!(ServicesManager.GetService(typeof (IBlackWidthService)) is IBlackWidthService))
      ServicesManager.AddService(typeof (IBlackWidthService), (object) new BlackWidthService());
    this._provider = provider;
    this.CreateGridsColumns();
    if (this._provider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service)
      service.AddPage(BlackWidthViewPage.GetPath(), (IPropertyPage) this);
    this.LoadExtensionsSettgins();
    this.isChanged = this.isSettingsChanged = false;
    this.UpdateControls();
    this.loaded = true;
    this.grid.Rows.AutoHeight();
    this.CorrectAutoColsWidth();
  }

  /// <summary>заполнить грид информацией о файлах</summary>
  private void LoadExtensionsSettgins()
  {
    this.isSettingsChanged = false;
    IBlackWidthService service = (IBlackWidthService) ServicesManager.GetService(typeof (IBlackWidthService));
    this._allColorToBlack = service.AllColorToBlack;
    this.grid.Rows.Clear();
    this.cbBlackColor.Checked = this._allColorToBlack;
    DwgColor dwgColor = new DwgColor((byte) 0);
    for (int index = 1; index < 256 /*0x0100*/; ++index)
    {
      ColorWidth colorWidth = service[(byte) index];
      if (colorWidth.AcadIndex != (byte) 0)
      {
        iGRow iGrow = this.grid.Rows.Add();
        iGrow.Cells["USED"].Value = (object) colorWidth.Used;
        iGrow.Cells["INDEX"].Value = (object) colorWidth.AcadIndex;
        iGrow.Cells["COLOR"].Value = (object) "";
        dwgColor.AcadIndex = (uint) colorWidth.AcadIndex;
        iGrow.Cells["COLOR"].BackColor = dwgColor.GdiColor;
        iGrow.Cells["COLOR"].ForeColor = dwgColor.GdiColor;
        iGrow.Cells["WIDTH"].Value = (object) colorWidth.Width;
        iGrow.Cells["WIDTH"].TextFormatFlags = iGStringFormatFlags.DisplayFormatControl;
      }
    }
    if (this.grid.Rows.Count > 0)
      this.grid.CurRow = this.grid.Rows[0];
    this.CorrectAutoColsWidth();
  }

  /// <summary></summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void BlackWidthViewPage_VisibleChanged(object sender, EventArgs e)
  {
    IBlackWidthService service = (IBlackWidthService) ServicesManager.GetService(typeof (IBlackWidthService));
    if (service == null)
      return;
    this.loaded = false;
    this.cbBlackColor.Checked = this._allColorToBlack = service.AllColorToBlack;
    this.cbBlackColor.CheckState = this._allColorToBlack ? CheckState.Checked : CheckState.Unchecked;
    foreach (iGRow row in (IEnumerable) this.grid.Rows)
    {
      int int32 = Convert.ToInt32(row.Cells["INDEX"].Value);
      ColorWidth colorWidth = service[(byte) int32];
      row.Cells["USED"].Value = (object) colorWidth.Used;
      row.Cells["WIDTH"].Value = (object) colorWidth.Width;
    }
    this.loaded = true;
  }

  /// <summary>создаём колокни в гриде</summary>
  private void CreateGridsColumns()
  {
    iGCellStyle iGcellStyle1 = new iGCellStyle(true);
    iGcellStyle1.TextAlign = iGContentAlignment.TopLeft;
    iGcellStyle1.ReadOnly = iGBool.True;
    iGcellStyle1.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGCellStyle iGcellStyle2 = new iGCellStyle(true);
    iGcellStyle2.TextAlign = iGContentAlignment.TopLeft;
    iGcellStyle2.ReadOnly = iGBool.False;
    iGcellStyle2.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGcellStyle2.Type = iGCellType.Text;
    iGCellStyle iGcellStyle3 = new iGCellStyle(true);
    iGColHdrStyle iGcolHdrStyle = new iGColHdrStyle(true);
    iGcellStyle3.Flags = iGCellFlags.DisplayImage;
    iGcellStyle3.ImageAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle3.SingleClickEdit = iGBool.True;
    iGcellStyle3.TextAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle3.Type = iGCellType.Check;
    iGcellStyle3.ValueType = typeof (bool);
    if (this.colWidths.Count == 0)
    {
      this.colWidths.Add("USED", 50);
      this.colWidths.Add("INDEX", 50);
      this.colWidths.Add("COLOR", 50);
      this.colWidths.Add("WIDTH", 50);
    }
    iGCol col1 = this.grid.Cols["USED"];
    iGCol iGcol1 = this.grid.Cols["USED"] ?? this.grid.Cols.Add(new iGColPattern(this.colWidths["USED"], true, true, this.colWidths["USED"], -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Используется", "USED", -1, (object) null, (object) null, -1));
    iGcol1.CellStyle = iGcellStyle3;
    iGcol1.ColHdrStyle = iGcolHdrStyle;
    iGCol col2 = this.grid.Cols["INDEX"];
    iGCol iGcol2 = this.grid.Cols["INDEX"] ?? this.grid.Cols.Add(new iGColPattern(this.colWidths["INDEX"], true, true, this.colWidths["INDEX"], -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Индекс", "INDEX", -1, (object) null, (object) null, -1));
    iGcol2.Width = this.colWidths["INDEX"];
    iGcol2.CellStyle = iGcellStyle1;
    iGCol col3 = this.grid.Cols["COLOR"];
    iGCol iGcol3 = this.grid.Cols["COLOR"] ?? this.grid.Cols.Add(new iGColPattern(this.colWidths["COLOR"], true, true, this.colWidths["COLOR"], -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Цвет", "COLOR", -1, (object) null, (object) null, -1));
    iGcol3.Width = this.colWidths["COLOR"];
    iGcol3.CellStyle = iGcellStyle1;
    iGCol col4 = this.grid.Cols["WIDTH"];
    iGCol iGcol4 = this.grid.Cols["WIDTH"] ?? this.grid.Cols.Add(new iGColPattern(this.colWidths["WIDTH"], true, true, this.colWidths["WIDTH"], -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Толщина", "WIDTH", -1, (object) null, (object) null, -1));
    iGcol4.Width = this.colWidths["WIDTH"];
    iGcol4.CellStyle = iGcellStyle2;
    iGcol4.CellStyle.ReadOnly = iGBool.False;
    iGcol4.CellStyle.Type = iGCellType.Text;
    iGcol4.CellStyle.FormatProvider = (IFormatProvider) CultureInfo.CurrentCulture.NumberFormat;
    this.CorrectColsWidth();
  }

  /// <summary>Откорректировать ширину колонок в гриде</summary>
  private void CorrectAutoColsWidth()
  {
    if (this.colWidths == null || this.colWidths.Count <= 0)
      return;
    this.grid.Cols.AutoWidth();
    this.colWidths["USED"] = this.grid.Cols["USED"].Width;
    this.colWidths["INDEX"] = this.grid.Cols["INDEX"].Width;
    this.colWidths["COLOR"] = this.grid.Cols["COLOR"].Width;
    this.colWidths["WIDTH"] = this.grid.Cols["WIDTH"].Width;
  }

  /// <summary>Откорректировать ширину колонок в гриде</summary>
  private void CorrectColsWidth()
  {
    if (this.colWidths == null || this.colWidths.Count <= 0)
      return;
    int num = this.grid.ClientRectangle.Width - 30 - this.grid.Cols["USED"].Width - this.colWidths["INDEX"] - this.colWidths["COLOR"];
    this.grid.Cols["USED"].Width = this.colWidths["USED"];
    this.grid.Cols["INDEX"].Width = this.colWidths["INDEX"];
    this.grid.Cols["COLOR"].Width = this.colWidths["COLOR"];
    if (num > 50)
      this.colWidths["WIDTH"] = num;
    this.grid.Cols["WIDTH"].Width = this.colWidths["WIDTH"];
  }

  /// <summary>Изменение размера грида</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void igAcadColors_Resize(object sender, EventArgs e) => this.CorrectColsWidth();

  /// <summary>Изменение ширины колонок в гриде</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void igAcadColors_ColWidthChanging(object sender, iGColWidthEventArgs e)
  {
    this.colWidths[this.grid.Cols[e.ColIndex].Key] = e.Width;
  }

  /// <summary>Завершение изменение ширины колонок в гриде</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void igAcadColors_ColWidthEndChange(object sender, iGColWidthEventArgs e)
  {
    this.colWidths[this.grid.Cols[e.ColIndex].Key] = e.Width;
  }

  /// <summary>Изменилась выделенная строка</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void igAcadColors_CurRowChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Нажата кнопка [...] в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (e.RowIndex >= this.grid.Rows.Count || e.ColIndex != 0 || e.Button != MouseButtons.Left)
      return;
    this.isSettingsChanged = true;
    this.OnChanged();
    this.UpdateControls();
  }

  /// <summary>Завершается редактирование в ячейке грида</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_BeforeCommitEdit(object sender, iGBeforeCommitEditEventArgs e)
  {
    iGCol col = this.grid.Cols[e.ColIndex];
    iGRow row = this.grid.Rows[e.RowIndex];
    if (col.Key != "WIDTH")
      return;
    if (e.NewValue == null || (double) (float) e.NewValue < 0.0)
    {
      e.Result = iGEditResult.Cancel;
    }
    else
    {
      float newValue = (float) e.NewValue;
      row.Cells["WIDTH"].Value = (object) newValue;
      this.isSettingsChanged = true;
      this.OnChanged();
      this.UpdateControls();
      e.Result = iGEditResult.Commit;
    }
  }

  private void grid_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != ' ')
      return;
    iGRow row = this.grid.SelectedCells.Count > 0 ? this.grid.SelectedCells[0].Row : (iGRow) null;
    if (row == null)
      return;
    row.Cells["USED"].Value = (object) !(bool) row.Cells["USED"].Value;
    this.isSettingsChanged = true;
    this.OnChanged();
    this.UpdateControls();
    e.Handled = true;
  }

  /// <summary>Изменение опции </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbBlackColor_CheckedChanged(object sender, EventArgs e)
  {
    if (!this.loaded)
      return;
    this.isSettingsChanged = true;
    this.OnChanged();
    this.UpdateControls();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (BlackWidthViewPage));
    this.cbBlackColor = new CheckBox();
    this.grid = new iGrid();
    this.iGrid1DefaultCellStyle1 = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle1 = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle1 = new iGCellStyle(true);
    ((ISupportInitialize) this.grid).BeginInit();
    this.SuspendLayout();
    componentResourceManager.ApplyResources((object) this.cbBlackColor, "cbBlackColor");
    this.cbBlackColor.Checked = true;
    this.cbBlackColor.CheckState = CheckState.Checked;
    this.cbBlackColor.Name = "cbBlackColor";
    this.cbBlackColor.UseVisualStyleBackColor = true;
    this.cbBlackColor.CheckedChanged += new EventHandler(this.cbBlackColor_CheckedChanged);
    componentResourceManager.ApplyResources((object) this.grid, "grid");
    this.grid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle1;
    this.grid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle1;
    this.grid.DefaultCol.Key = componentResourceManager.GetString("resource.Key");
    this.grid.DefaultCol.MaxWidth = (int) componentResourceManager.GetObject("resource.MaxWidth");
    this.grid.DefaultCol.MinWidth = (int) componentResourceManager.GetObject("resource.MinWidth");
    this.grid.DefaultCol.Text = componentResourceManager.GetObject("resource.Text");
    this.grid.DefaultCol.Width = (int) componentResourceManager.GetObject("resource.Width");
    this.grid.Header.Height = (int) componentResourceManager.GetObject("grid.Header.Height");
    this.grid.HotTracking = false;
    this.grid.Name = "grid";
    this.grid.ProcessTab = false;
    this.grid.RowMode = true;
    this.grid.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle1;
    this.grid.RowTextCol.Key = componentResourceManager.GetString("grid.RowTextCol.Key");
    this.grid.RowTextCol.MaxWidth = (int) componentResourceManager.GetObject("grid.RowTextCol.MaxWidth");
    this.grid.RowTextCol.MinWidth = (int) componentResourceManager.GetObject("grid.RowTextCol.MinWidth");
    this.grid.RowTextCol.Text = componentResourceManager.GetObject("grid.RowTextCol.Text");
    this.grid.RowTextCol.Width = (int) componentResourceManager.GetObject("grid.RowTextCol.Width");
    this.grid.SilentValidation = true;
    this.grid.SingleClickEdit = true;
    this.grid.CellMouseUp += new iGCellMouseUpEventHandler(this.grid_CellMouseUp);
    this.grid.ColWidthEndChange += new iGColWidthEventHandler(this.igAcadColors_ColWidthEndChange);
    this.grid.ColWidthChanging += new iGColWidthEventHandler(this.igAcadColors_ColWidthChanging);
    this.grid.CurRowChanged += new EventHandler(this.igAcadColors_CurRowChanged);
    this.grid.BeforeCommitEdit += new iGBeforeCommitEditEventHandler(this.grid_BeforeCommitEdit);
    this.grid.KeyPress += new KeyPressEventHandler(this.grid_KeyPress);
    this.grid.Resize += new EventHandler(this.igAcadColors_Resize);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.cbBlackColor);
    this.Controls.Add((System.Windows.Forms.Control) this.grid);
    this.Name = nameof (BlackWidthViewPage);
    this.VisibleChanged += new EventHandler(this.BlackWidthViewPage_VisibleChanged);
    ((ISupportInitialize) this.grid).EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void grid_CellMouseDown(object sender, iGCellMouseDownEventArgs e)
  {
    this.grid.SetCurCell(e.RowIndex, e.ColIndex);
    this.grid.RequestEditCurCell();
    this.grid.CommitEditCurCell();
  }
}
