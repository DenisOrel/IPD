
// Type: Intermech.Client.Core.Navigator.Controls.Views.RedliningViewPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.Navigator.Controls.Windows;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Client.Core.Navigator.Controls.Views;

/// <summary>Закладка для настройки Красный карандаш</summary>
public class RedliningViewPage : UserControl, IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>"FOLDER" Папка для поиска</summary>
  private const string FOLDER = "FOLDER";
  /// <summary>"MASK" Маска для поиска</summary>
  private const string MASK = "MASK";
  /// <summary>"NAME" Наименование типа файла</summary>
  private const string NAME = "NAME";
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _provider;
  /// <summary>
  /// Ширина колонки для определённого типа.
  /// [KEY колонки] =&gt; [Ширина колонки]
  /// </summary>
  private Dictionary<string, int> colWidths = new Dictionary<string, int>();
  /// <summary>изменились настйроки для файлов замечаний?</summary>
  private bool isSettingsChanged;
  /// <summary>
  /// 
  /// </summary>
  private bool isChanged;
  /// <summary>
  /// 
  /// </summary>
  private bool loaded;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private ImageList imButtons;
  private iGrid igRedliningFiles;
  private iGCellStyle iGrid1DefaultCellStyle1;
  private iGColHdrStyle iGrid1DefaultColHdrStyle1;
  private iGCellStyle iGrid1RowTextColCellStyle1;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;
  private iGCellStyle iGrid1DefaultCellStyle;
  private Button btnAdd;
  private Button btnChange;
  private Button btnDelete;
  private ToolTip toolTip1;
  private SplitContainer splitContainer1;
  private ComboBox cbLevels;
  private CheckBox cbDeleteFiles;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="provider"></param>
  public RedliningViewPage(System.IServiceProvider provider)
  {
    this.InitializeComponent();
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service1) || !service1.IsAdmin)
      return;
    this._provider = provider;
    this.CreateGridsColumns();
    if (this._provider.GetService(typeof (IPropertyPagesService)) is IPropertyPagesService service2)
      service2.AddPage(this.PageName, (IPropertyPage) this);
    this.LoadLevels();
    this.LoadRedliningSettgins();
    this.UpdateControls();
    this.loaded = true;
    this.cbLevels.SelectedIndexChanged += new EventHandler(this.cbLevels_SelectedIndexChanged);
    this.igRedliningFiles.Rows.AutoHeight();
  }

  /// <summary>загружаем все уровни</summary>
  private void LoadLevels()
  {
    this.cbLevels.Items.Clear();
    List<IMSLifeCycleLevel> lcLevelsList = MetaDataHelper.GetLCLevelsList();
    lcLevelsList.Sort();
    foreach (IMSLifeCycleLevel tag in lcLevelsList)
      this.cbLevels.Items.Add((object) new MyElement((object) tag.LevelID, tag.Name, (object) tag));
    this.cbLevels.SelectedIndex = 0;
  }

  /// <summary>заполнить грид информацией о файлах замечаний</summary>
  private void LoadRedliningSettgins()
  {
    this.igRedliningFiles.Rows.Clear();
    this.isSettingsChanged = false;
    bool flag = false;
    int levelID = 0;
    List<RedliningFiles> redliningFilesList = new List<RedliningFiles>();
    if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IRedliningService)) is IRedliningService customService)
    {
      flag = customService.DeleteFiles;
      levelID = customService.LevelID;
      redliningFilesList = customService.RedliningFilesSettings;
    }
    this.cbDeleteFiles.Checked = flag;
    if (flag)
    {
      IMSLifeCycleLevel lcLevel = MetaDataHelper.GetLCLevel(levelID);
      if (lcLevel != null)
        this.cbLevels.SelectedItem = (object) new MyElement((object) levelID, lcLevel.Name, (object) lcLevel);
    }
    foreach (RedliningFiles redliningFiles in redliningFilesList)
    {
      iGRow iGrow = this.igRedliningFiles.Rows.Add();
      iGrow.Cells["NAME"].Value = (object) redliningFiles.Name;
      iGrow.Cells["FOLDER"].Value = (object) redliningFiles.Folder;
      iGrow.Cells["MASK"].Value = (object) redliningFiles.Mask;
    }
    if (this.igRedliningFiles.Rows.Count <= 0)
      return;
    this.igRedliningFiles.CurRow = this.igRedliningFiles.Rows[0];
  }

  /// <summary>создаём колокни в гриде</summary>
  private void CreateGridsColumns()
  {
    iGCellStyle iGcellStyle = new iGCellStyle(true);
    iGcellStyle.TextAlign = iGContentAlignment.TopLeft;
    iGcellStyle.ReadOnly = iGBool.True;
    iGcellStyle.EmptyStringAs = iGEmptyStringAs.EmptyString;
    if (this.colWidths.Count == 0)
    {
      this.colWidths.Add("NAME", 150);
      this.colWidths.Add("FOLDER", 100);
      this.colWidths.Add("MASK", 100);
    }
    iGCol col1 = this.igRedliningFiles.Cols["NAME"];
    iGCol iGcol1 = this.igRedliningFiles.Cols["NAME"] ?? this.igRedliningFiles.Cols.Add(new iGColPattern(this.colWidths["NAME"], true, true, 150, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1417"), "NAME", -1, (object) null, (object) null, -1));
    iGcol1.Width = this.colWidths["NAME"];
    iGcol1.CellStyle = iGcellStyle;
    iGCol col2 = this.igRedliningFiles.Cols["FOLDER"];
    iGCol iGcol2 = this.igRedliningFiles.Cols["FOLDER"] ?? this.igRedliningFiles.Cols.Add(new iGColPattern(this.colWidths["FOLDER"], true, true, 100, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1418"), "FOLDER", -1, (object) null, (object) null, -1));
    iGcol2.Width = this.colWidths["FOLDER"];
    iGcol2.CellStyle = iGcellStyle;
    iGCol col3 = this.igRedliningFiles.Cols["MASK"];
    iGCol iGcol3 = this.igRedliningFiles.Cols["MASK"] ?? this.igRedliningFiles.Cols.Add(new iGColPattern(this.colWidths["MASK"], true, true, 100, -1, true, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1419"), "MASK", -1, (object) null, (object) null, -1));
    iGcol3.Width = this.colWidths["MASK"];
    iGcol3.CellStyle = iGcellStyle;
    this.CorrectColsWidth();
  }

  /// <summary>Откорректировать ширину колонок в гриде</summary>
  private void CorrectColsWidth()
  {
    if (this.colWidths == null || this.colWidths.Count <= 0)
      return;
    int num = this.igRedliningFiles.ClientRectangle.Width - 30 - this.colWidths["FOLDER"] - this.colWidths["MASK"];
    this.igRedliningFiles.Cols["FOLDER"].Width = this.colWidths["FOLDER"];
    this.igRedliningFiles.Cols["MASK"].Width = this.colWidths["MASK"];
    if (num > 150)
      this.igRedliningFiles.Cols["NAME"].Width = this.colWidths["NAME"] = num;
    else
      this.igRedliningFiles.Cols["NAME"].Width = this.colWidths["NAME"];
  }

  /// <summary>Изменение размера грида</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igRedliningFiles_Resize(object sender, EventArgs e) => this.CorrectColsWidth();

  /// <summary>Изменение ширины колонок в гриде</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igRedliningFiles_ColWidthChanging(object sender, iGColWidthEventArgs e)
  {
    this.colWidths[this.igRedliningFiles.Cols[e.ColIndex].Key] = e.Width;
  }

  /// <summary>Завершение изменение ширины колонок в гриде</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igRedliningFiles_ColWidthEndChange(object sender, iGColWidthEventArgs e)
  {
    this.colWidths[this.igRedliningFiles.Cols[e.ColIndex].Key] = e.Width;
  }

  /// <summary>Изменилась выделенная строка</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void igRedliningFiles_CurRowChanged(object sender, EventArgs e) => this.UpdateControls();

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

  /// <summary>
  /// 
  /// </summary>
  public PropertyPageType Type => PropertyPageType.Control;

  /// <summary>
  ///  Контрол, который будет размещён на главной форме настроек
  /// </summary>
  public object Control => (object) this;

  /// <summary>Название странички в главной форме настроек</summary>
  public string PageName => LocalizationHolder.rm.GetString("Client.Core_1416");

  /// <summary>Применить изменения редактора</summary>
  public void Apply()
  {
    if (this.isChanged)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (sessionKeeper.Session.GetCustomService(typeof (IRedliningService)) is IRedliningService customService)
        {
          bool delete = this.cbDeleteFiles.Checked;
          int levelID = delete ? (int) (this.cbLevels.SelectedItem as MyElement).Value : 0;
          List<RedliningFiles> settings = (List<RedliningFiles>) null;
          if (this.isSettingsChanged)
          {
            settings = new List<RedliningFiles>();
            foreach (iGRow row in (IEnumerable) this.igRedliningFiles.Rows)
              settings.Add(new RedliningFiles(row.Cells["NAME"].Value.ToString(), row.Cells["MASK"].Value.ToString(), row.Cells["FOLDER"].Value.ToString()));
          }
          customService.ChangeRedliningSettings(settings, delete, levelID, (object) sessionKeeper.Session.SessionGUID);
        }
      }
    }
    this.isSettingsChanged = this.isChanged = false;
  }

  /// <summary>Отменить изменения редактора</summary>
  public void Cancel()
  {
    this.isChanged = this.isSettingsChanged = false;
    this.LoadRedliningSettgins();
    this.UpdateControls();
  }

  /// <summary>
  /// 
  /// </summary>
  public string HelpTopicID => string.Empty;

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText => LocalizationHolder.rm.GetString("Client.Core_1420");

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is System.Windows.Forms.Control control) ? new List<string>() : IPropertyPageHelper.GetOptionNames(control);
  }

  /// <summary>Добавить тип файла замечаний</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnAdd_Click(object sender, EventArgs e)
  {
    using (RedlinigEditForm redlinigEditForm = new RedlinigEditForm())
    {
      if (redlinigEditForm.ShowDialog() != DialogResult.OK)
        return;
      iGRow iGrow = this.igRedliningFiles.Rows.Add();
      iGrow.Cells["NAME"].Value = (object) redlinigEditForm.FileName;
      iGrow.Cells["FOLDER"].Value = (object) redlinigEditForm.Folder;
      iGrow.Cells["MASK"].Value = (object) redlinigEditForm.Mask;
      this.isSettingsChanged = true;
      this.OnChanged();
      this.UpdateControls();
    }
  }

  /// <summary>Изменить тип файла замечаний</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnChange_Click(object sender, EventArgs e)
  {
    iGRow curRow = this.igRedliningFiles.CurRow;
    using (RedlinigEditForm redlinigEditForm = new RedlinigEditForm(curRow.Cells["NAME"].Value.ToString(), curRow.Cells["FOLDER"].Value.ToString(), curRow.Cells["MASK"].Value.ToString()))
    {
      if (redlinigEditForm.ShowDialog() != DialogResult.OK)
        return;
      curRow.Cells["NAME"].Value = (object) redlinigEditForm.FileName;
      curRow.Cells["FOLDER"].Value = (object) redlinigEditForm.Folder;
      curRow.Cells["MASK"].Value = (object) redlinigEditForm.Mask;
      this.isSettingsChanged = true;
      this.OnChanged();
      this.UpdateControls();
    }
  }

  /// <summary>Удалить тип файла замечаний</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void btnDelete_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1421"), LocalizationHolder.rm.GetString("Client.Core_1422"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
      return;
    this.isSettingsChanged = true;
    this.igRedliningFiles.Rows.RemoveAt(this.igRedliningFiles.CurRow.Index);
    this.OnChanged();
    this.UpdateControls();
  }

  /// <summary>обновить состояние контролов</summary>
  private void UpdateControls()
  {
    this.btnChange.Enabled = this.btnDelete.Enabled = this.igRedliningFiles.CurRow != null;
  }

  /// <summary>Изменение опции Удалять файлы замечаний...</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbDeleteFiles_CheckedChanged(object sender, EventArgs e)
  {
    this.cbLevels.Enabled = this.cbDeleteFiles.Checked;
    if (!this.loaded)
      return;
    this.OnChanged();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void cbLevels_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (!this.loaded)
      return;
    this.OnChanged();
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RedliningViewPage));
    this.imButtons = new ImageList(this.components);
    this.btnAdd = new Button();
    this.btnChange = new Button();
    this.btnDelete = new Button();
    this.toolTip1 = new ToolTip(this.components);
    this.igRedliningFiles = new iGrid();
    this.iGrid1DefaultCellStyle1 = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle1 = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle1 = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.splitContainer1 = new SplitContainer();
    this.cbLevels = new ComboBox();
    this.cbDeleteFiles = new CheckBox();
    ((ISupportInitialize) this.igRedliningFiles).BeginInit();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this.SuspendLayout();
    this.imButtons.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imButtons.ImageStream");
    this.imButtons.TransparentColor = Color.Transparent;
    this.imButtons.Images.SetKeyName(0, "add.ico");
    this.imButtons.Images.SetKeyName(1, "edit.ico");
    this.imButtons.Images.SetKeyName(2, "delete.ico");
    this.imButtons.Images.SetKeyName(3, "add.png");
    this.imButtons.Images.SetKeyName(4, "delete.png");
    this.imButtons.Images.SetKeyName(5, "edit.png");
    componentResourceManager.ApplyResources((object) this.btnAdd, "btnAdd");
    this.btnAdd.Name = "btnAdd";
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnAdd, componentResourceManager.GetString("btnAdd.ToolTip"));
    this.btnAdd.UseVisualStyleBackColor = true;
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    componentResourceManager.ApplyResources((object) this.btnChange, "btnChange");
    this.btnChange.Name = "btnChange";
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnChange, componentResourceManager.GetString("btnChange.ToolTip"));
    this.btnChange.UseVisualStyleBackColor = true;
    this.btnChange.Click += new EventHandler(this.btnChange_Click);
    componentResourceManager.ApplyResources((object) this.btnDelete, "btnDelete");
    this.btnDelete.Name = "btnDelete";
    this.toolTip1.SetToolTip((System.Windows.Forms.Control) this.btnDelete, componentResourceManager.GetString("btnDelete.ToolTip"));
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    componentResourceManager.ApplyResources((object) this.igRedliningFiles, "igRedliningFiles");
    this.igRedliningFiles.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle1;
    this.igRedliningFiles.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle1;
    this.igRedliningFiles.Header.Height = (int) componentResourceManager.GetObject("igRedliningFiles.Header.Height");
    this.igRedliningFiles.HotTracking = false;
    this.igRedliningFiles.Name = "igRedliningFiles";
    this.igRedliningFiles.ProcessTab = false;
    this.igRedliningFiles.ReadOnly = true;
    this.igRedliningFiles.RowMode = true;
    this.igRedliningFiles.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle1;
    this.igRedliningFiles.SilentValidation = true;
    this.igRedliningFiles.SingleClickEdit = true;
    this.igRedliningFiles.ColWidthEndChange += new iGColWidthEventHandler(this.igRedliningFiles_ColWidthEndChange);
    this.igRedliningFiles.ColWidthChanging += new iGColWidthEventHandler(this.igRedliningFiles_ColWidthChanging);
    this.igRedliningFiles.CurRowChanged += new EventHandler(this.igRedliningFiles_CurRowChanged);
    this.igRedliningFiles.Resize += new EventHandler(this.igRedliningFiles_Resize);
    this.splitContainer1.BackColor = SystemColors.ControlLight;
    componentResourceManager.ApplyResources((object) this.splitContainer1, "splitContainer1");
    this.splitContainer1.FixedPanel = FixedPanel.Panel1;
    this.splitContainer1.Name = "splitContainer1";
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel1, "splitContainer1.Panel1");
    this.splitContainer1.Panel1.BackColor = SystemColors.Control;
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbLevels);
    this.splitContainer1.Panel1.Controls.Add((System.Windows.Forms.Control) this.cbDeleteFiles);
    componentResourceManager.ApplyResources((object) this.splitContainer1.Panel2, "splitContainer1.Panel2");
    this.splitContainer1.Panel2.BackColor = SystemColors.Control;
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.btnChange);
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.igRedliningFiles);
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.btnDelete);
    this.splitContainer1.Panel2.Controls.Add((System.Windows.Forms.Control) this.btnAdd);
    this.cbLevels.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbLevels.FormattingEnabled = true;
    componentResourceManager.ApplyResources((object) this.cbLevels, "cbLevels");
    this.cbLevels.Name = "cbLevels";
    componentResourceManager.ApplyResources((object) this.cbDeleteFiles, "cbDeleteFiles");
    this.cbDeleteFiles.Checked = true;
    this.cbDeleteFiles.CheckState = CheckState.Checked;
    this.cbDeleteFiles.Name = "cbDeleteFiles";
    this.cbDeleteFiles.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((System.Windows.Forms.Control) this.splitContainer1);
    this.Name = nameof (RedliningViewPage);
    ((ISupportInitialize) this.igRedliningFiles).EndInit();
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel1.PerformLayout();
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
