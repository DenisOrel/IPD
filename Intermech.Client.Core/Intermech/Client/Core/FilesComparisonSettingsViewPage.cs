
// Type: Intermech.Client.Core.FilesComparisonSettingsViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core;

public class FilesComparisonSettingsViewPage : UserControl, IPropertyPage
{
  private System.IServiceProvider _provider;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Button btnDelete;
  private iGrid grid;
  private Button btnUpdate;
  private Button btnChange;
  private Button btnAdd;
  private ToolTip toolTip1;

  /// <summary>Страница настроек файлов и аутентичных файлов</summary>
  /// <param name="provider"></param>
  public FilesComparisonSettingsViewPage()
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    ((IPropertyPagesService) ServicesManager.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("FilesComparisonSettings"), (IPropertyPage) this);
    this.InitializeComponent();
    this.InitializeToolTip();
    this.CreateGridsColumns();
    this.InitializeGridsColumns();
  }

  /// <summary>создаём колонки в гриде</summary>
  private void CreateGridsColumns()
  {
    iGCellStyle iGcellStyle1 = new iGCellStyle(true);
    iGcellStyle1.TextAlign = iGContentAlignment.TopLeft;
    iGcellStyle1.ReadOnly = iGBool.True;
    iGcellStyle1.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGCellStyle iGcellStyle2 = new iGCellStyle(true);
    iGColHdrStyle iGcolHdrStyle = new iGColHdrStyle(true);
    iGcellStyle2.Flags = iGCellFlags.DisplayImage;
    iGcellStyle2.ImageAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle2.SingleClickEdit = iGBool.True;
    iGcellStyle2.TextAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle2.Type = iGCellType.Check;
    iGcellStyle2.ValueType = typeof (bool);
    iGCol iGcol1 = this.grid.Cols["F_NAME"] ?? this.grid.Cols.Add(new iGColPattern(70, true, true, 70, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Наименование", "F_NAME", -1, (object) null, (object) null, -1));
    iGcol1.Width = 70;
    iGcol1.CellStyle = iGcellStyle1;
    iGCol iGcol2 = this.grid.Cols["F_EXT"] ?? this.grid.Cols.Add(new iGColPattern(70, true, true, 70, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Маски расширений файлов", "F_EXT", -1, (object) null, (object) null, -1));
    iGcol2.Width = 70;
    iGcol2.CellStyle = iGcellStyle1;
    iGCol iGcol3 = this.grid.Cols["F_ARGS"] ?? this.grid.Cols.Add(new iGColPattern(200, true, true, 200, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Аргументы командной строки", "F_ARGS", -1, (object) null, (object) null, -1));
    iGcol3.Width = 200;
    iGcol3.CellStyle = iGcellStyle1;
    this.grid.Cols.AutoWidth();
  }

  private void InitializeGridsColumns()
  {
    List<FilesComparisonSettings> comparisonSettings1 = ServiceUtils.GetService<ICompareFilesService>((object) ApplicationServices.Container, false).GetAllFilesComparisonSettings();
    this.grid.Rows.Clear();
    foreach (FilesComparisonSettings comparisonSettings2 in comparisonSettings1)
    {
      iGRow iGrow = this.grid.Rows.Add();
      iGrow.Cells["F_NAME"].Value = (object) comparisonSettings2.Name;
      iGrow.Cells["F_ARGS"].Value = (object) comparisonSettings2.Arguments;
      iGrow.Cells["F_EXT"].Value = (object) comparisonSettings2.ExtensionsAsString;
      iGrow.Tag = (object) comparisonSettings2;
    }
  }

  private void InitializeToolTip()
  {
    this.toolTip1.AutoPopDelay = 5000;
    this.toolTip1.InitialDelay = 700;
    this.toolTip1.ReshowDelay = 500;
    this.toolTip1.ShowAlways = true;
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnAdd, "Добавить настройку сравнения типов файлов");
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnChange, "Изменить настройку сравнения типов файлов");
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnDelete, "Удалить настройку сравнения типов файлов");
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnUpdate, "Обновить настройки");
  }

  public event EventHandler Changed;

  /// <summary>Добавляем в окно настроек контрол</summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>
  /// Контрол, который будет размещён на главной форме настроек
  /// </summary>
  public object Control => (object) this;

  /// <summary>Название странички в главной форме настроек</summary>
  public string PageName => LocalizationHolder.rm.GetString("FilesComparisonPageName");

  public void Apply()
  {
    ICompareFilesService service = ServiceUtils.GetService<ICompareFilesService>((object) ServicesManager.ServiceContainer, true);
    List<FilesComparisonSettings> settings = new List<FilesComparisonSettings>();
    for (int index = 0; index < this.grid.Rows.Count; ++index)
    {
      if (this.grid.Rows[index].Tag is FilesComparisonSettings tag)
        settings.Add(tag);
    }
    service.SaveFilesComparisonSettings(settings);
  }

  public void Cancel() => this.InitializeGridsColumns();

  public string HelpTopicID { get; }

  /// <summary>Текст заголовка</summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  /// <summary>Добавить настройку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    using (CompareSettingsEditForm settingsEditForm = new CompareSettingsEditForm())
    {
      int num = (int) settingsEditForm.ShowDialog();
      if (settingsEditForm.DialogResult != DialogResult.OK)
        return;
      this.FillRow(this.grid.SelectedCells.Count > 0 ? this.grid.Rows.Insert(this.grid.SelectedCells[0].Row.Index + 1) : this.grid.Rows.Add(), settingsEditForm.Settings);
      this.OnChanged();
      this.UpdateButtonsEnabling();
    }
  }

  /// <summary>Обновить видимость кнопок</summary>
  private void UpdateButtonsEnabling()
  {
    this.btnChange.Enabled = this.btnDelete.Enabled = this.grid.CurRow != null;
  }

  /// <summary>Заполнить строку настройками</summary>
  /// <param name="row"></param>
  /// <param name="settings"></param>
  private void FillRow(iGRow row, FilesComparisonSettings settings)
  {
    row.Tag = (object) settings;
    row.Cells["F_NAME"].Value = (object) settings.Name;
    row.Cells["F_ARGS"].Value = (object) settings.Arguments;
    row.Cells["F_EXT"].Value = (object) settings.ExtensionsAsString;
  }

  /// <summary>Изменить настройки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnChange_Click(object sender, EventArgs e)
  {
    iGRow curRow = this.grid.CurRow;
    if (curRow == null || !(curRow.Tag is FilesComparisonSettings tag))
      return;
    using (CompareSettingsEditForm settingsEditForm = new CompareSettingsEditForm())
    {
      settingsEditForm.Init(tag);
      if (settingsEditForm.ShowDialog() != DialogResult.OK)
        return;
      this.FillRow(curRow, settingsEditForm.Settings);
      this.OnChanged();
      this.UpdateButtonsEnabling();
    }
  }

  /// <summary>Удалить настройку</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show("Вы действительно хотите удалить настройку?", LocalizationHolder.rm.GetString("Client.Core_1422"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    if (this.grid.CurRow.Tag is FilesComparisonSettings)
      this.grid.Rows.RemoveAt(this.grid.CurRow.Index);
    this.OnChanged();
    this.UpdateButtonsEnabling();
  }

  /// <summary>Перечитать настройки из базы</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnUpdate_Click(object sender, EventArgs e)
  {
    this.InitializeGridsColumns();
    this.UpdateButtonsEnabling();
  }

  /// <summary>Сменилась выделенная строка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void grid_CurRowChanged(object sender, EventArgs e) => this.UpdateButtonsEnabling();

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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FilesComparisonSettingsViewPage));
    this.btnDelete = new Button();
    this.grid = new iGrid();
    this.btnUpdate = new Button();
    this.btnChange = new Button();
    this.btnAdd = new Button();
    this.toolTip1 = new ToolTip(this.components);
    ((ISupportInitialize) this.grid).BeginInit();
    this.SuspendLayout();
    this.btnDelete.Enabled = false;
    this.btnDelete.Image = (Image) componentResourceManager.GetObject("btnDelete.Image");
    this.btnDelete.Location = new Point(62, 3);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(28, 24);
    this.btnDelete.TabIndex = 14;
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.grid.AutoResizeCols = true;
    this.grid.Header.Height = 19;
    this.grid.HotTracking = false;
    this.grid.Location = new Point(2, 33);
    this.grid.Name = "grid";
    this.grid.ProcessTab = false;
    this.grid.RowMode = true;
    this.grid.RowModeHasCurCell = true;
    this.grid.SilentValidation = true;
    this.grid.SingleClickEdit = true;
    this.grid.Size = new Size(596, 147);
    this.grid.TabIndex = 15;
    this.grid.CurRowChanged += new EventHandler(this.grid_CurRowChanged);
    this.btnUpdate.Image = (Image) componentResourceManager.GetObject("btnUpdate.Image");
    this.btnUpdate.Location = new Point(92, 3);
    this.btnUpdate.Name = "btnUpdate";
    this.btnUpdate.Size = new Size(28, 24);
    this.btnUpdate.TabIndex = 16 /*0x10*/;
    this.btnUpdate.UseVisualStyleBackColor = true;
    this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
    this.btnChange.Enabled = false;
    this.btnChange.Image = (Image) componentResourceManager.GetObject("btnChange.Image");
    this.btnChange.Location = new Point(32 /*0x20*/, 3);
    this.btnChange.Name = "btnChange";
    this.btnChange.Size = new Size(28, 24);
    this.btnChange.TabIndex = 13;
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    this.btnAdd.Image = (Image) componentResourceManager.GetObject("btnAdd.Image");
    this.btnAdd.Location = new Point(2, 3);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(28, 24);
    this.btnAdd.TabIndex = 12;
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.btnDelete);
    this.Controls.Add((System.Windows.Forms.Control) this.grid);
    this.Controls.Add((System.Windows.Forms.Control) this.btnUpdate);
    this.Controls.Add((System.Windows.Forms.Control) this.btnChange);
    this.Controls.Add((System.Windows.Forms.Control) this.btnAdd);
    this.Name = nameof (FilesComparisonSettingsViewPage);
    this.Size = new Size(599, 183);
    ((ISupportInitialize) this.grid).EndInit();
    this.ResumeLayout(false);
  }
}
