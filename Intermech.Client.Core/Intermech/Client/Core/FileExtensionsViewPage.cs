
// Type: Intermech.Client.Core.FileExtensionsViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls.Windows;
using Intermech.Extensions.WinForms;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core;

/// <summary>Параметры внешнего просмотра документов</summary>
public class FileExtensionsViewPage : UserControl, IPropertyPageSearchOptionEvents, IPropertyPage
{
  /// <summary>Используется</summary>
  private const string Used = "USED";
  /// <summary>"NAME"
  /// Наименование типа файла
  /// </summary>
  private const string Name_ = "NAME";
  /// <summary>Программный идентификатор ProgID</summary>
  private const string Progid = "PROGID";
  /// <summary>Маски для расширений файлов</summary>
  private const string Extensions = "EXTENSIONS";
  /// <summary>Настройка для всех пользователей</summary>
  private const string Alluser = "ALLUSER";
  /// <summary>
  /// Признак того, что настройки в данный момент загружаются
  /// </summary>
  private bool _isLoadingSettings;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private SplitContainer splitContainer;
  private GroupBox groupBox1;
  private iGrid grid;
  private Panel pnlCommon;
  private CheckBox cbDebugMode;
  private TextBox tbProperties;
  private Label label2;
  private TextBox tbMethods;
  private Label label1;
  private ToolStrip tsViewTypes;
  private ToolStripButton btnAdd;
  private ToolStripButton btnChange;
  private ToolStripButton btnDelete;
  private ToolStripButton btnUpdate;
  private ToolStripSeparator toolStripSeparator1;
  private ToolStripButton btnRegistredOcx;
  private GroupBox gbPvaf;
  private ListView lvPvaf;
  private ColumnHeader colHeader;
  private ToolStrip tsPvaf;
  private ToolStripButton tsbtnAdd;
  private ToolStripButton tsbtnRemove;
  private CheckBox cbWriteSignsAndParams;

  /// <summary>
  /// Типа объектов, для которых приоритетный просмотр аутентичных файлов
  /// </summary>
  private List<int> PriorityViewAuthenticFileObjTypes
  {
    get
    {
      return this.lvPvaf.Items.Cast<ListViewItem>().Select<ListViewItem, int>((Func<ListViewItem, int>) (x => (int) x.Tag)).ToList<int>();
    }
  }

  /// <summary></summary>
  public FileExtensionsViewPage()
  {
    this.InitializeComponent();
    ICurrentUserAndRole service = ApplicationServices.Container.GetService<ICurrentUserAndRole>();
    this.gbPvaf.Enabled = service.IsAdmin;
    this.cbWriteSignsAndParams.Enabled = service.IsAdmin;
    this.lvPvaf.SmallImageList = ApplicationServices.Container.GetService<ICategoryTypeIconService>().ImageList;
    this.CreateGridsColumns();
    this.LoadExtensionsSettings();
    this.grid.Rows.AutoHeight();
    ApplicationServices.Container.GetService<IPropertyPagesService>().AddPage("Система\\Просмотр документов", (IPropertyPage) this);
  }

  /// <summary>Событие изменения на закладке</summary>
  public event EventHandler Changed;

  /// <summary>Тип страницы свойств</summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>
  /// Контрол, который будет размещён на главной форме настроек
  /// </summary>
  public object Control => (object) this;

  /// <summary>Название странички в главной форме настроек</summary>
  public string PageName => "Пользователи\\Просмотр документов";

  /// <summary>
  /// 
  /// </summary>
  public string HelpTopicID => string.Empty;

  /// <summary>Текст заголовка</summary>
  public string HeaderText => "Просмотр документов";

  /// <summary>Применить изменения редактора</summary>
  public void Apply()
  {
    FileExtensionsInfo[] array = this.grid.Rows.Cast<iGRow>().Select<iGRow, FileExtensionsInfo>((Func<iGRow, FileExtensionsInfo>) (row => row.Tag as FileExtensionsInfo)).Where<FileExtensionsInfo>((Func<FileExtensionsInfo, bool>) (x => x != null)).ToArray<FileExtensionsInfo>();
    ApplicationServices.Container.GetService<IExtensionsService>().ChangeSettings((IReadOnlyCollection<FileExtensionsInfo>) array, this.tbMethods.Text, this.tbProperties.Text, this.cbDebugMode.Checked, this.cbWriteSignsAndParams.Checked, (IReadOnlyCollection<int>) this.PriorityViewAuthenticFileObjTypes);
    this.LoadExtensionsSettings(true);
  }

  /// <summary>Отменить изменения редактора</summary>
  public void Cancel() => this.LoadExtensionsSettings(true);

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return this.Control is System.Windows.Forms.Control control ? IPropertyPageHelper.GetOptionNames(control) : new List<string>();
  }

  /// <summary>создаём колокни в гриде</summary>
  private void CreateGridsColumns()
  {
    iGCellStyle iGcellStyle1 = new iGCellStyle(true);
    iGcellStyle1.TextAlign = iGContentAlignment.TopLeft;
    iGcellStyle1.ReadOnly = iGBool.True;
    iGcellStyle1.EmptyStringAs = iGEmptyStringAs.EmptyString;
    iGCellStyle iGcellStyle2 = iGcellStyle1;
    iGCellStyle iGcellStyle3 = new iGCellStyle(true);
    iGColHdrStyle iGcolHdrStyle = new iGColHdrStyle(true);
    iGcellStyle3.Flags = iGCellFlags.DisplayImage;
    iGcellStyle3.ImageAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle3.SingleClickEdit = iGBool.True;
    iGcellStyle3.TextAlign = iGContentAlignment.MiddleCenter;
    iGcellStyle3.Type = iGCellType.Check;
    iGcellStyle3.ValueType = typeof (bool);
    iGCol iGcol1 = this.grid.Cols["USED"] ?? this.grid.Cols.Add(new iGColPattern());
    iGcol1.Key = "USED";
    iGcol1.AllowGrouping = false;
    iGcol1.AllowMoving = false;
    iGcol1.AllowSizing = false;
    int num1;
    int num2 = num1 = 20;
    iGcol1.Width = num1;
    int num3;
    int num4 = num3 = num2;
    iGcol1.MinWidth = num3;
    iGcol1.MaxWidth = num4;
    iGcol1.CellStyle = iGcellStyle3;
    iGcol1.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol2 = this.grid.Cols["ALLUSER"] ?? this.grid.Cols.Add(new iGColPattern());
    iGcol2.Key = "ALLUSER";
    iGcol2.AllowGrouping = false;
    iGcol2.AllowMoving = false;
    iGcol2.AllowSizing = false;
    int num5;
    int num6 = num5 = 10;
    iGcol2.Width = num5;
    int num7;
    int num8 = num7 = num6;
    iGcol2.MinWidth = num7;
    iGcol2.MaxWidth = num8;
    iGcol2.Visible = false;
    iGcol2.CellStyle = iGcellStyle3;
    iGcol2.ColHdrStyle = iGcolHdrStyle;
    iGCol iGcol3 = this.grid.Cols["NAME"] ?? this.grid.Cols.Add(new iGColPattern(150, true, true, 150, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Наименование", "NAME", -1, (object) null, (object) null, -1));
    iGcol3.Width = 150;
    iGcol3.CellStyle = iGcellStyle2;
    iGCol iGcol4 = this.grid.Cols["PROGID"] ?? this.grid.Cols.Add(new iGColPattern(150, true, true, 150, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Программный идентификатор", "PROGID", -1, (object) null, (object) null, -1));
    iGcol4.Width = 150;
    iGcol4.CellStyle = iGcellStyle2;
    iGCol iGcol5 = this.grid.Cols["EXTENSIONS"] ?? this.grid.Cols.Add(new iGColPattern(150, true, true, 150, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Маски расширений файлов", "EXTENSIONS", -1, (object) null, (object) null, -1));
    iGcol5.Width = 150;
    iGcol5.CellStyle = iGcellStyle2;
  }

  /// <summary></summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void FileExtensionsViewPage_Enter(object sender, EventArgs e)
  {
    this.UpdateButtonsState();
  }

  /// <summary>Изменение размера грида</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void grid_Resize(object sender, EventArgs e) => this.grid.Cols.AutoWidth();

  /// <summary>Клик по галке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (e.RowIndex >= this.grid.Rows.Count || e.ColIndex != 0 || e.Button != MouseButtons.Left || !(this.grid.Rows[e.RowIndex]?.Tag is FileExtensionsInfo tag) || tag.IsUnknown)
      return;
    tag.Enabled = !tag.Enabled;
    this.OnChanged();
  }

  /// <summary> Нажата клавиша </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void grid_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar != ' ')
      return;
    iGRow row = this.grid.SelectedCells.Count > 0 ? this.grid.SelectedCells[0].Row : (iGRow) null;
    if (!(row?.Tag is FileExtensionsInfo tag) || tag.IsUnknown)
      return;
    row.Cells["USED"].Value = (object) !(bool) row.Cells["USED"].Value;
    tag.Enabled = !tag.Enabled;
    this.OnChanged();
    e.Handled = true;
  }

  /// <summary>Изменилась выделенная строка</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void grid_CurRowChanged(object sender, EventArgs e) => this.UpdateButtonsState();

  /// <summary>Заполнить строку грида</summary>
  /// <param name="row"></param>
  /// <param name="curObject"></param>
  private void FillRow(iGRow row, FileExtensionsInfo curObject)
  {
    row.Tag = (object) curObject;
    if (curObject.IsUnknown)
      curObject.Enabled = false;
    row.Cells["USED"].Value = (object) curObject.Enabled;
    row.Cells["ALLUSER"].Value = (object) curObject.IsAllUser;
    row.Cells["NAME"].Value = (object) curObject.Name;
    row.Cells["EXTENSIONS"].Value = (object) curObject.Extensions;
    string str = curObject.ProgId;
    if (curObject.Style == StyleView.CommandLine)
    {
      str = curObject.CommandLine;
      try
      {
        int startIndex = str.IndexOf(".exe\"");
        if (startIndex != -1)
        {
          int num = str.LastIndexOf("\\", startIndex);
          if (num > 5)
            str = str.Replace(str.Substring(4, num - 6), "...");
        }
      }
      catch (Exception ex)
      {
      }
    }
    row.Cells["PROGID"].Value = (object) str;
    iGBool iGbool = curObject.IsUnknown ? iGBool.False : iGBool.True;
    row.Cells["USED"].Enabled = iGbool;
    row.Cells["ALLUSER"].Enabled = iGbool;
    row.Cells["NAME"].Enabled = row.Cells["PROGID"].Enabled = row.Cells["EXTENSIONS"].Enabled = iGbool;
    if (curObject.IsAllUser)
    {
      row.Cells["NAME"].Enabled = row.Cells["PROGID"].Enabled = row.Cells["EXTENSIONS"].Enabled = iGBool.True;
      row.Cells["USED"].ForeColor = row.Cells["ALLUSER"].ForeColor = row.Cells["NAME"].ForeColor = row.Cells["PROGID"].ForeColor = row.Cells["EXTENSIONS"].ForeColor = Color.Blue;
    }
    else
      row.Cells["USED"].ForeColor = row.Cells["ALLUSER"].ForeColor = row.Cells["NAME"].ForeColor = row.Cells["PROGID"].ForeColor = row.Cells["EXTENSIONS"].ForeColor = Color.Black;
  }

  /// <summary>Добавить тип просмотра файла</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    using (ExtensionsEditForm form = new ExtensionsEditForm())
    {
      if (form.ShowTopDialog() != DialogResult.OK)
        return;
      this.FillRow(this.grid.SelectedCells.Count > 0 ? this.grid.Rows.Insert(this.grid.SelectedCells[0].Row.Index + 1) : this.grid.Rows.Add(), new FileExtensionsInfo(form.Used, form.NameViewer, form.ProgID, form.Extensions, form.CommandLine)
      {
        IsAllUser = form.IsAllUser
      });
      this.OnChanged();
    }
  }

  /// <summary>Изменить тип просмотра файла </summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void btnChange_Click(object sender, EventArgs e)
  {
    iGRow curRow = this.grid.CurRow;
    if (!(curRow?.Tag is FileExtensionsInfo tag))
      return;
    using (ExtensionsEditForm form = new ExtensionsEditForm(tag.Enabled, tag.IsAllUser, tag.Name, tag.ProgId, tag.Extensions, tag.CommandLine))
    {
      if (form.ShowTopDialog() != DialogResult.OK)
        return;
      FileExtensionsInfo curObject = new FileExtensionsInfo(form.Used, form.NameViewer, form.ProgID, form.Extensions, form.CommandLine)
      {
        IsAllUser = form.IsAllUser
      };
      this.FillRow(curRow, curObject);
      this.OnChanged();
    }
  }

  /// <summary>Удалить тип файла просмотра</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (!(this.grid.CurRow.Tag is FileExtensionsInfo) || MessageBox.Show("Удалить тип просмотра файла?", LocalizationHolder.rm.GetString("Client.Core_1422"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this.grid.Rows.RemoveAt(this.grid.CurRow.Index);
    this.OnChanged();
    this.UpdateButtonsState();
  }

  /// <summary>Зарегистрировать OCX</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnRegistredOcx_Click(object sender, EventArgs e)
  {
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
      openFileDialog.RestoreDirectory = true;
      openFileDialog.Title = "Регистрация в реестре";
      openFileDialog.Filter = "Регистрация (*.ocx)|*.ocx";
      if (openFileDialog.ShowDialog() != DialogResult.OK)
        return;
      using (Process process = new Process())
      {
        try
        {
          process.StartInfo.FileName = "regsvr32.exe";
          process.StartInfo.Arguments = $"\"{openFileDialog.FileName}\"";
          process.StartInfo.UseShellExecute = false;
          process.StartInfo.CreateNoWindow = true;
          process.StartInfo.RedirectStandardOutput = true;
          process.Start();
          process.WaitForExit();
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(ex.Message);
        }
      }
      int num1 = (int) MessageBox.Show($"Регистрация \"{Path.GetFileName(openFileDialog.FileName)}\" выполнена", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }
  }

  /// <summary>Обновить список типов файла</summary>
  /// <param name="sender">Отправитель события</param>
  /// <param name="e">Аргументы события</param>
  private void btnUpdate_Click(object sender, EventArgs e)
  {
    IExtensionsService service = ApplicationServices.Container.GetService<IExtensionsService>();
    service.CheckDefaultFileExtensions();
    FileExtensionsInfo[] array = this.grid.Rows.Cast<iGRow>().Select<iGRow, FileExtensionsInfo>((Func<iGRow, FileExtensionsInfo>) (row => row.Tag as FileExtensionsInfo)).Where<FileExtensionsInfo>((Func<FileExtensionsInfo, bool>) (x => x != null)).ToArray<FileExtensionsInfo>();
    List<FileExtensionsInfo> list = service.GetStoredFileExtensionsInfo().Except<FileExtensionsInfo>((IEnumerable<FileExtensionsInfo>) array).ToList<FileExtensionsInfo>();
    if (list.Count == 0)
      return;
    list.ForEach((Action<FileExtensionsInfo>) (x => this.FillRow(this.grid.Rows.Add(), x)));
    this.OnChanged();
  }

  /// <summary>
  /// Добавить тип объекта для приоритетного просмотра аутентичных
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsbtnAdd_Click(object sender, EventArgs e)
  {
    using (SelectorForm form = new SelectorForm("Выберите тип объекта", 4, true))
    {
      if (form.ShowTopDialog() != DialogResult.OK)
        return;
      this.FillLvObjTypes(this.lvPvaf, (IEnumerable<int>) Array.ConvertAll<object, int>(form.IDList.ToArray(), new Converter<object, int>(Convert.ToInt32)));
      this.OnChanged();
    }
  }

  /// <summary>
  ///  Удалить тип объекта из приоритетного просмотра аутентичных
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void tsbtnRemove_Click(object sender, EventArgs e)
  {
    if (this.lvPvaf.SelectedItems.Count <= 0)
      return;
    foreach (ListViewItem selectedItem in this.lvPvaf.SelectedItems)
      this.lvPvaf.Items.Remove(selectedItem);
    this.OnChanged();
  }

  /// <summary>Загрузить настройки</summary>
  /// <param name="needReload"></param>
  private void LoadExtensionsSettings(bool needReload = false)
  {
    this._isLoadingSettings = true;
    this.grid.Rows.Clear();
    IExtensionsService service = ApplicationServices.Container.GetService<IExtensionsService>();
    if (needReload)
      service.ReloadParams();
    this.tbMethods.Text = service.Methods;
    this.tbProperties.Text = service.Properties;
    this.cbDebugMode.Checked = service.DebugMode;
    this.cbWriteSignsAndParams.Checked = service.WriteSignsAndParams;
    service.GetStoredFileExtensionsInfo().ToList<FileExtensionsInfo>().ForEach((Action<FileExtensionsInfo>) (x => this.FillRow(this.grid.Rows.Add(), x)));
    this.grid.Cols.AutoWidth();
    this.grid.Sort();
    if (this.grid.Rows.Count > 0)
      this.grid.CurRow = this.grid.Rows[0];
    this.lvPvaf.Items.Clear();
    this.FillLvObjTypes(this.lvPvaf, (IEnumerable<int>) service.GetPriorityViewAuthenticFileObjTypes());
    this._isLoadingSettings = false;
  }

  /// <summary>Изменились настройки</summary>
  private void OnChanged()
  {
    if (this._isLoadingSettings)
      return;
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, new EventArgs());
  }

  /// <summary>Обновить состояние кнопок</summary>
  private void UpdateButtonsState()
  {
    if (this.grid.CurRow?.Tag is FileExtensionsInfo)
      this.btnChange.Enabled = this.btnDelete.Enabled = true;
    else
      this.btnChange.Enabled = this.btnDelete.Enabled = false;
  }

  /// <summary>Поменялись настройки</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void ChangedSettings(object sender, EventArgs e)
  {
    if (this._isLoadingSettings)
      return;
    this.OnChanged();
  }

  /// <summary>Заполнить ListView</summary>
  /// <param name="lv"></param>
  /// <param name="objTypeIds"></param>
  private void FillLvObjTypes(ListView lv, IEnumerable<int> objTypeIds)
  {
    List<int> list = objTypeIds.ToList<int>();
    if (!list.Any<int>())
      return;
    ICategoryTypeIconService service = ApplicationServices.Container.GetService<ICategoryTypeIconService>();
    foreach (int objTypeID in list)
    {
      IMSObjectType objectType = MetaDataHelper.GetObjectType(objTypeID);
      if (objectType != null)
        lv.Items.Add(objectType.ObjectTypeName, service.IndexOf(4, objectType.ObjectTypeID)).Tag = (object) objectType.ObjectTypeID;
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FileExtensionsViewPage));
    this.splitContainer = new SplitContainer();
    this.groupBox1 = new GroupBox();
    this.grid = new iGrid();
    this.tsViewTypes = new ToolStrip();
    this.btnAdd = new ToolStripButton();
    this.btnChange = new ToolStripButton();
    this.btnDelete = new ToolStripButton();
    this.btnUpdate = new ToolStripButton();
    this.toolStripSeparator1 = new ToolStripSeparator();
    this.btnRegistredOcx = new ToolStripButton();
    this.gbPvaf = new GroupBox();
    this.lvPvaf = new ListView();
    this.colHeader = new ColumnHeader();
    this.tsPvaf = new ToolStrip();
    this.tsbtnAdd = new ToolStripButton();
    this.tsbtnRemove = new ToolStripButton();
    this.pnlCommon = new Panel();
    this.cbWriteSignsAndParams = new CheckBox();
    this.cbDebugMode = new CheckBox();
    this.tbProperties = new TextBox();
    this.label2 = new Label();
    this.tbMethods = new TextBox();
    this.label1 = new Label();
    this.splitContainer.BeginInit();
    this.splitContainer.Panel1.SuspendLayout();
    this.splitContainer.Panel2.SuspendLayout();
    this.splitContainer.SuspendLayout();
    this.groupBox1.SuspendLayout();
    ((ISupportInitialize) this.grid).BeginInit();
    this.tsViewTypes.SuspendLayout();
    this.gbPvaf.SuspendLayout();
    this.tsPvaf.SuspendLayout();
    this.pnlCommon.SuspendLayout();
    this.SuspendLayout();
    this.splitContainer.Dock = DockStyle.Fill;
    this.splitContainer.Location = new Point(0, 141);
    this.splitContainer.Name = "splitContainer";
    this.splitContainer.Orientation = Orientation.Horizontal;
    this.splitContainer.Panel1.Controls.Add((System.Windows.Forms.Control) this.groupBox1);
    this.splitContainer.Panel2.Controls.Add((System.Windows.Forms.Control) this.gbPvaf);
    this.splitContainer.Size = new Size(619, 367);
    this.splitContainer.SplitterDistance = 175;
    this.splitContainer.TabIndex = 16 /*0x10*/;
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.grid);
    this.groupBox1.Controls.Add((System.Windows.Forms.Control) this.tsViewTypes);
    this.groupBox1.Dock = DockStyle.Fill;
    this.groupBox1.Location = new Point(0, 0);
    this.groupBox1.Name = "groupBox1";
    this.groupBox1.Size = new Size(619, 175);
    this.groupBox1.TabIndex = 16 /*0x10*/;
    this.groupBox1.TabStop = false;
    this.groupBox1.Text = "Параметры типов просмотра";
    this.grid.AutoResizeCols = true;
    this.grid.BorderStyle = iGBorderStyle.Flat;
    this.grid.Dock = DockStyle.Fill;
    this.grid.Header.Height = 19;
    this.grid.HotTracking = false;
    this.grid.Location = new Point(3, 41);
    this.grid.Name = "grid";
    this.grid.ProcessTab = false;
    this.grid.RowMode = true;
    this.grid.RowModeHasCurCell = true;
    this.grid.SilentValidation = true;
    this.grid.SingleClickEdit = true;
    this.grid.Size = new Size(613, 131);
    this.grid.TabIndex = 9;
    this.grid.CellMouseUp += new iGCellMouseUpEventHandler(this.grid_CellMouseUp);
    this.grid.CurRowChanged += new EventHandler(this.grid_CurRowChanged);
    this.grid.KeyPress += new KeyPressEventHandler(this.grid_KeyPress);
    this.grid.Resize += new EventHandler(this.grid_Resize);
    this.tsViewTypes.GripStyle = ToolStripGripStyle.Hidden;
    this.tsViewTypes.Items.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.btnAdd,
      (ToolStripItem) this.btnChange,
      (ToolStripItem) this.btnDelete,
      (ToolStripItem) this.btnUpdate,
      (ToolStripItem) this.toolStripSeparator1,
      (ToolStripItem) this.btnRegistredOcx
    });
    this.tsViewTypes.Location = new Point(3, 16 /*0x10*/);
    this.tsViewTypes.Name = "tsViewTypes";
    this.tsViewTypes.Size = new Size(613, 25);
    this.tsViewTypes.TabIndex = 15;
    this.tsViewTypes.Text = "toolStrip1";
    this.btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnAdd.Image = (Image) componentResourceManager.GetObject("btnAdd.Image");
    this.btnAdd.ImageTransparentColor = Color.Magenta;
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(23, 22);
    this.btnAdd.Text = "Добавить тип просмотра файла";
    this.btnAdd.ToolTipText = "Добавить тип просмотра файла";
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnChange.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnChange.Image = (Image) componentResourceManager.GetObject("btnChange.Image");
    this.btnChange.ImageTransparentColor = Color.Magenta;
    this.btnChange.Name = "btnChange";
    this.btnChange.Size = new Size(23, 22);
    this.btnChange.Text = "Изменить тип просмотра файла";
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    this.btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnDelete.Image = (Image) componentResourceManager.GetObject("btnDelete.Image");
    this.btnDelete.ImageTransparentColor = Color.Magenta;
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(23, 22);
    this.btnDelete.Text = "Удалить тип файла просмотра";
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.btnUpdate.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.btnUpdate.Image = (Image) componentResourceManager.GetObject("btnUpdate.Image");
    this.btnUpdate.ImageTransparentColor = Color.Magenta;
    this.btnUpdate.Name = "btnUpdate";
    this.btnUpdate.Size = new Size(23, 22);
    this.btnUpdate.Text = "Обновить список типов файлов";
    this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
    this.toolStripSeparator1.Name = "toolStripSeparator1";
    this.toolStripSeparator1.Size = new Size(6, 25);
    this.btnRegistredOcx.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.btnRegistredOcx.Image = (Image) componentResourceManager.GetObject("btnRegistredOcx.Image");
    this.btnRegistredOcx.ImageTransparentColor = Color.Magenta;
    this.btnRegistredOcx.Name = "btnRegistredOcx";
    this.btnRegistredOcx.Size = new Size(104, 22);
    this.btnRegistredOcx.Text = "Регистрация Ocx";
    this.btnRegistredOcx.Click += new EventHandler(this.btnRegistredOcx_Click);
    this.gbPvaf.Controls.Add((System.Windows.Forms.Control) this.lvPvaf);
    this.gbPvaf.Controls.Add((System.Windows.Forms.Control) this.tsPvaf);
    this.gbPvaf.Dock = DockStyle.Fill;
    this.gbPvaf.Location = new Point(0, 0);
    this.gbPvaf.Name = "gbPvaf";
    this.gbPvaf.Size = new Size(619, 188);
    this.gbPvaf.TabIndex = 18;
    this.gbPvaf.TabStop = false;
    this.gbPvaf.Text = "Приоритетный просмотр аутентичных файлов";
    this.lvPvaf.Alignment = ListViewAlignment.Left;
    this.lvPvaf.Columns.AddRange(new ColumnHeader[1]
    {
      this.colHeader
    });
    this.lvPvaf.Dock = DockStyle.Fill;
    this.lvPvaf.HeaderStyle = ColumnHeaderStyle.None;
    this.lvPvaf.HideSelection = false;
    this.lvPvaf.Location = new Point(3, 41);
    this.lvPvaf.Name = "lvPvaf";
    this.lvPvaf.Size = new Size(613, 144 /*0x90*/);
    this.lvPvaf.TabIndex = 14;
    this.lvPvaf.UseCompatibleStateImageBehavior = false;
    this.lvPvaf.View = View.Details;
    this.colHeader.Text = "";
    this.colHeader.Width = 597;
    this.tsPvaf.GripStyle = ToolStripGripStyle.Hidden;
    this.tsPvaf.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.tsbtnAdd,
      (ToolStripItem) this.tsbtnRemove
    });
    this.tsPvaf.Location = new Point(3, 16 /*0x10*/);
    this.tsPvaf.Name = "tsPvaf";
    this.tsPvaf.Size = new Size(613, 25);
    this.tsPvaf.TabIndex = 15;
    this.tsPvaf.Text = "toolStrip1";
    this.tsbtnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnAdd.Image = (Image) componentResourceManager.GetObject("tsbtnAdd.Image");
    this.tsbtnAdd.ImageTransparentColor = Color.Magenta;
    this.tsbtnAdd.Name = "tsbtnAdd";
    this.tsbtnAdd.Size = new Size(23, 22);
    this.tsbtnAdd.Text = "toolStripButton1";
    this.tsbtnAdd.ToolTipText = "Добавить тип объекта";
    this.tsbtnAdd.Click += new EventHandler(this.tsbtnAdd_Click);
    this.tsbtnRemove.DisplayStyle = ToolStripItemDisplayStyle.Image;
    this.tsbtnRemove.Image = (Image) componentResourceManager.GetObject("tsbtnRemove.Image");
    this.tsbtnRemove.ImageTransparentColor = Color.Magenta;
    this.tsbtnRemove.Name = "tsbtnRemove";
    this.tsbtnRemove.Size = new Size(23, 22);
    this.tsbtnRemove.Text = "toolStripButton2";
    this.tsbtnRemove.ToolTipText = "Удалить тип объекта";
    this.tsbtnRemove.Click += new EventHandler(this.tsbtnRemove_Click);
    this.pnlCommon.Controls.Add((System.Windows.Forms.Control) this.cbWriteSignsAndParams);
    this.pnlCommon.Controls.Add((System.Windows.Forms.Control) this.cbDebugMode);
    this.pnlCommon.Controls.Add((System.Windows.Forms.Control) this.tbProperties);
    this.pnlCommon.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.pnlCommon.Controls.Add((System.Windows.Forms.Control) this.tbMethods);
    this.pnlCommon.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.pnlCommon.Dock = DockStyle.Top;
    this.pnlCommon.Location = new Point(0, 0);
    this.pnlCommon.Name = "pnlCommon";
    this.pnlCommon.Size = new Size(619, 141);
    this.pnlCommon.TabIndex = 17;
    this.cbWriteSignsAndParams.AutoSize = true;
    this.cbWriteSignsAndParams.Location = new Point(6, 117);
    this.cbWriteSignsAndParams.Name = "cbWriteSignsAndParams";
    this.cbWriteSignsAndParams.Size = new Size(339, 17);
    this.cbWriteSignsAndParams.TabIndex = 14;
    this.cbWriteSignsAndParams.Text = "Записывать подписи и параметры в файл перед просмотром";
    this.cbWriteSignsAndParams.UseVisualStyleBackColor = true;
    this.cbWriteSignsAndParams.CheckedChanged += new EventHandler(this.ChangedSettings);
    this.cbDebugMode.AutoSize = true;
    this.cbDebugMode.Location = new Point(6, 94);
    this.cbDebugMode.Name = "cbDebugMode";
    this.cbDebugMode.Size = new Size(207, 17);
    this.cbDebugMode.TabIndex = 13;
    this.cbDebugMode.Text = "Выводить отладочную информацию";
    this.cbDebugMode.UseVisualStyleBackColor = true;
    this.cbDebugMode.CheckedChanged += new EventHandler(this.ChangedSettings);
    this.tbProperties.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbProperties.Location = new Point(6, 68);
    this.tbProperties.Name = "tbProperties";
    this.tbProperties.Size = new Size(607, 20);
    this.tbProperties.TabIndex = 10;
    this.tbProperties.TextChanged += new EventHandler(this.ChangedSettings);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(8, 52);
    this.label2.Name = "label2";
    this.label2.Size = new Size(185, 13);
    this.label2.TabIndex = 9;
    this.label2.Text = "Свойства для открытия просмотра";
    this.tbMethods.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.tbMethods.Location = new Point(6, 24);
    this.tbMethods.Name = "tbMethods";
    this.tbMethods.Size = new Size(607, 20);
    this.tbMethods.TabIndex = 8;
    this.tbMethods.TextChanged += new EventHandler(this.ChangedSettings);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(8, 8);
    this.label1.Name = "label1";
    this.label1.Size = new Size(177, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Методы для открытия просмотра";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer);
    this.Controls.Add((System.Windows.Forms.Control) this.pnlCommon);
    this.Name = nameof (FileExtensionsViewPage);
    this.Size = new Size(619, 508);
    this.Enter += new EventHandler(this.FileExtensionsViewPage_Enter);
    this.splitContainer.Panel1.ResumeLayout(false);
    this.splitContainer.Panel2.ResumeLayout(false);
    this.splitContainer.EndInit();
    this.splitContainer.ResumeLayout(false);
    this.groupBox1.ResumeLayout(false);
    this.groupBox1.PerformLayout();
    ((ISupportInitialize) this.grid).EndInit();
    this.tsViewTypes.ResumeLayout(false);
    this.tsViewTypes.PerformLayout();
    this.gbPvaf.ResumeLayout(false);
    this.gbPvaf.PerformLayout();
    this.tsPvaf.ResumeLayout(false);
    this.tsPvaf.PerformLayout();
    this.pnlCommon.ResumeLayout(false);
    this.pnlCommon.PerformLayout();
    this.ResumeLayout(false);
  }
}
