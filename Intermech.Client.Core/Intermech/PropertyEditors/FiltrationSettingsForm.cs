
// Type: Intermech.PropertyEditors.FiltrationSettingsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using ImSSP;
using Intermech.Bars;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>
/// Редактор настроек фильтрации (для работы независимо от тулбара "Фильтрация состава" главной формы)
/// </summary>
public class FiltrationSettingsForm : Form
{
  /// <summary>Контейнер компонентов формы</summary>
  private IContainer components;
  /// <summary>Именованный список значков</summary>
  private INamedImageList namedImageList;
  /// <summary>
  /// Уникальный ID текущего владельца настроек фильтрации,
  /// </summary>
  private string _FSOwnerID = string.Empty;
  /// <summary>
  /// Текущие настройки фильтрации состава, информация по которым отображена в тулбаре "Фильтрация состава"
  /// </summary>
  private FiltrationSettings _FSFiltration = new FiltrationSettings();
  /// <summary>
  /// Правило фильтрации состава, возвращающее последние версии объектов
  /// </summary>
  private VersionsRule _FSLatestVersionsRule = new VersionsRule();
  /// <summary>
  /// Правило фильтрации состава, возвращающее все версии объектов
  /// </summary>
  private VersionsRule _FSAllVersionsRule = new VersionsRule();
  /// <summary>
  /// Если выбранное правило является вариантом значений переменных (т.е. создано на базе родительского правила),
  /// то это поле отражает, совместимо ли правило с родительским вариантом (на случай, если были изменения
  /// в родительском правиле после создания вариантов его значений переменных)
  /// </summary>
  private bool _FSRuleCompatible;
  /// <summary>
  /// Валидно ли выбранное правило подбора версий
  /// (для проверки выполняется метод Valid правила, а также проверяется наличие у него переменных значений)
  /// Если _FSRuleValid = false, правило применять нельзя
  /// </summary>
  private bool _FSRuleValid;
  /// <summary>
  /// Код ошибки для текущего правила:
  /// 0  - правило не выбрано,
  /// 1  - настройки недействительны - правило было изменено,
  /// 2  - нет ошибок, правило настроено,
  /// 3  - нет вариантов значений переменных для правила,
  /// 4  - фильтрация состава выключена (obsolete),
  /// 5  - не указан основной вариант значений переменных,
  /// 6  - правило является некорректным
  /// 7  - выбрано правило "Все версии объектов", но его выбирать запрещено
  /// -1 - системное правило "Последние версии объектов"
  /// -2 - системное правило "Все версии объектов"
  /// </summary>
  private int _FSRuleErrorCode = -2;
  /// <summary>
  /// True, если указано неверное значение варианта значений переменных
  /// </summary>
  private bool _FSVarsOutOfRange;
  /// <summary>Коллекция индексов значков</summary>
  private int[] _FSImages = new int[6];
  /// <summary>
  /// Чтобы избежать рекурсивного вызова одного обработчика событий внутри другого
  /// </summary>
  private bool _FSIsLoading;
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareFunctionsHelper FCFunc = new CompareFunctionsHelper();
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareTypesHelper FCTypes = new CompareTypesHelper();
  /// <summary>Разрешить выбор правила "Все версии объектов"</summary>
  private bool FEnableAllVersions = true;
  private Intermech.Bars.ToolBar toolbarFilter;
  private ComboBoxItem cbCurrentVersionRule;
  private ButtonItem BtnBrowse;
  private ImageList imagesList;
  private ImageList imagesToolbar;
  private MenuBar menuBar1;
  private ContextMenuBarItem contextMenuBarItem;
  private MenuButtonItem mnpEditVars;
  private MenuButtonItem mnpSetAsMain;
  private MenuButtonItem mnpAddVars;
  private MenuButtonItem mnpDeleteVars;
  private MenuButtonItem mnpRefresh;
  private MenuButtonItem mnpBrowse;
  private Panel panelBottom;
  private Button BtnClose;
  private Label lbWarning;
  private PictureBox imgWarning;
  private Intermech.Bars.ToolBar toolBarAdv;
  private ButtonItem BtnAddVars;
  private ButtonItem BtnChangeVars;
  private ButtonItem BtnSetAsMain;
  private ButtonItem BtnDelVars;
  private ButtonItem BtnRefresh;
  private ListView ListVars;
  private ColumnHeader columnNumber;
  private ColumnHeader columnVariants;
  private ColumnHeader columnDateTime;

  /// <summary>Создать и инициализировать экземпляр формы</summary>
  public FiltrationSettingsForm()
  {
    this.InitializeComponent();
    HelpProvidersClass.SetHelpOptionForControl((Control) this, 1023 /*0x03FF*/);
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._FSImages[0] = this.namedImageList.ImageIndex("imgGreenBall");
    this._FSImages[1] = this.namedImageList.ImageIndex("imgYellowBall");
    this._FSImages[2] = this.namedImageList.ImageIndex("imgRedBall");
    this._FSImages[3] = this.namedImageList.ImageIndex("imgApplyBall");
    this._FSImages[4] = this.namedImageList.ImageIndex("imgInvalidRule");
    this._FSImages[5] = this.namedImageList.ImageIndex("imgCorruptedRule");
    this._FSIsLoading = false;
    this.cbCurrentVersionRule.ComboBox.SelectedIndexChanged += new EventHandler(this.cbFiltrationRule_SelectedIndexChanged);
    this.cbCurrentVersionRule.ComboBox.Cursor = Cursors.Hand;
    this.cbCurrentVersionRule.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCurrentVersionRule.ComboBox.MaxDropDownItems = 16 /*0x10*/;
    this.cbCurrentVersionRule.ComboBox.Sorted = true;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    int width1 = workingArea.Width;
    Size size = this.Size;
    int width2 = size.Width;
    int x = (width1 - width2) / 2;
    int height1 = workingArea.Height;
    size = this.Size;
    int height2 = size.Height;
    int y = (height1 - height2) / 2;
    this.Location = new Point(x, y);
    this.cbCurrentVersionRule.ComboBox.Items.Clear();
    this.LoadFilterData();
    this.UpdateControls();
  }

  /// <summary>Убрать за собой мусор</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.toolBarAdv.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      this.toolbarFilter.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>Вызвать форму как модальное окно</summary>
  /// <param name="FiltrationOwnerID">Уникальный ID редактируемых настроек фильтрации</param>
  /// <param name="EnableAllVersions">Разрешить выбор правила "Все версии объектов"</param>
  public static void Execute(string FiltrationOwnerID, bool EnableAllVersions)
  {
    using (FiltrationSettingsForm filtrationSettingsForm = new FiltrationSettingsForm())
    {
      filtrationSettingsForm._FSOwnerID = FiltrationOwnerID;
      filtrationSettingsForm.FEnableAllVersions = EnableAllVersions;
      filtrationSettingsForm.LoadFilterData();
      int num = (int) filtrationSettingsForm.ShowDialog();
    }
  }

  /// <summary>Очистка внутренних структур</summary>
  public void Clear()
  {
    this._FSFiltration.Clear();
    this._FSRuleCompatible = false;
    this._FSRuleErrorCode = 0;
    this._FSRuleValid = false;
    this._FSVarsOutOfRange = false;
    this.FillList();
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FiltrationSettingsForm));
    this.toolbarFilter = new Intermech.Bars.ToolBar();
    this.imagesToolbar = new ImageList(this.components);
    this.cbCurrentVersionRule = new ComboBoxItem();
    this.BtnBrowse = new ButtonItem();
    this.imagesList = new ImageList(this.components);
    this.menuBar1 = new MenuBar();
    this.contextMenuBarItem = new ContextMenuBarItem();
    this.mnpEditVars = new MenuButtonItem();
    this.mnpSetAsMain = new MenuButtonItem();
    this.mnpAddVars = new MenuButtonItem();
    this.mnpDeleteVars = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.mnpBrowse = new MenuButtonItem();
    this.ListVars = new ListView();
    this.columnNumber = new ColumnHeader();
    this.columnVariants = new ColumnHeader();
    this.columnDateTime = new ColumnHeader();
    this.panelBottom = new Panel();
    this.BtnClose = new Button();
    this.lbWarning = new Label();
    this.imgWarning = new PictureBox();
    this.toolBarAdv = new Intermech.Bars.ToolBar();
    this.BtnAddVars = new ButtonItem();
    this.BtnChangeVars = new ButtonItem();
    this.BtnSetAsMain = new ButtonItem();
    this.BtnRefresh = new ButtonItem();
    this.BtnDelVars = new ButtonItem();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.imgWarning).BeginInit();
    this.SuspendLayout();
    this.toolbarFilter.AllowVerticalDock = false;
    this.toolbarFilter.DockLine = 3;
    this.toolbarFilter.FullMenus = true;
    this.toolbarFilter.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolbarFilter.Hidden = false;
    this.toolbarFilter.ImageList = this.imagesToolbar;
    this.toolbarFilter.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this.cbCurrentVersionRule,
      (ToolbarItemBase) this.BtnBrowse
    });
    componentResourceManager.ApplyResources((object) this.toolbarFilter, "toolbarFilter");
    this.toolbarFilter.MinimumFloatingSize = new Size(250, 30);
    this.toolbarFilter.Name = "toolbarFilter";
    this.toolbarFilter.Stretch = true;
    this.toolbarFilter.StretchItem = (ToolbarItemBase) this.cbCurrentVersionRule;
    this.imagesToolbar.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesToolbar.ImageStream");
    this.imagesToolbar.TransparentColor = Color.Transparent;
    this.imagesToolbar.Images.SetKeyName(0, "");
    this.imagesToolbar.Images.SetKeyName(1, "");
    this.imagesToolbar.Images.SetKeyName(2, "");
    this.imagesToolbar.Images.SetKeyName(3, "");
    this.imagesToolbar.Images.SetKeyName(4, "");
    this.imagesToolbar.Images.SetKeyName(5, "");
    this.imagesToolbar.Images.SetKeyName(6, "");
    componentResourceManager.ApplyResources((object) this.cbCurrentVersionRule, "cbCurrentVersionRule");
    this.cbCurrentVersionRule.ControlText = "<Выберите правило подбора версий>";
    this.cbCurrentVersionRule.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCurrentVersionRule.MinimumControlWidth = 100;
    this.cbCurrentVersionRule.Padding.Bottom = 0;
    this.cbCurrentVersionRule.Padding.Left = 1;
    this.cbCurrentVersionRule.Padding.Right = 1;
    this.cbCurrentVersionRule.Padding.Top = 0;
    this.cbCurrentVersionRule.Stretch = true;
    componentResourceManager.ApplyResources((object) this.BtnBrowse, "BtnBrowse");
    this.BtnBrowse.ImageIndex = 1;
    this.BtnBrowse.ShowText = true;
    this.BtnBrowse.Click += new EventHandler(this.BtnBrowse_Click);
    this.imagesList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesList.ImageStream");
    this.imagesList.TransparentColor = Color.Transparent;
    this.imagesList.Images.SetKeyName(0, "");
    this.imagesList.Images.SetKeyName(1, "");
    this.imagesList.Images.SetKeyName(2, "");
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuBar1.Hidden = false;
    this.menuBar1.ImageList = this.imagesToolbar;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem
    });
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem, "contextMenuBarItem");
    this.contextMenuBarItem.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpEditVars,
      (ToolbarItemBase) this.mnpSetAsMain,
      (ToolbarItemBase) this.mnpAddVars,
      (ToolbarItemBase) this.mnpDeleteVars,
      (ToolbarItemBase) this.mnpRefresh,
      (ToolbarItemBase) this.mnpBrowse
    });
    this.contextMenuBarItem.ShowText = true;
    componentResourceManager.ApplyResources((object) this.mnpEditVars, "mnpEditVars");
    this.mnpEditVars.ImageIndex = 4;
    this.mnpEditVars.ShowText = true;
    this.mnpEditVars.Click += new EventHandler(this.DoEditVars);
    componentResourceManager.ApplyResources((object) this.mnpSetAsMain, "mnpSetAsMain");
    this.mnpSetAsMain.ImageIndex = 5;
    this.mnpSetAsMain.ShowText = true;
    this.mnpSetAsMain.Click += new EventHandler(this.DoSetAsMain);
    componentResourceManager.ApplyResources((object) this.mnpAddVars, "mnpAddVars");
    this.mnpAddVars.ImageIndex = 3;
    this.mnpAddVars.ShowText = true;
    this.mnpAddVars.Click += new EventHandler(this.BtnAddVars_Click);
    componentResourceManager.ApplyResources((object) this.mnpDeleteVars, "mnpDeleteVars");
    this.mnpDeleteVars.ImageIndex = 6;
    this.mnpDeleteVars.ShowText = true;
    this.mnpDeleteVars.Click += new EventHandler(this.BtnDelVars_Click);
    this.mnpRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.mnpRefresh, "mnpRefresh");
    this.mnpRefresh.ImageIndex = 2;
    this.mnpRefresh.ShowText = true;
    this.mnpRefresh.Click += new EventHandler(this.BtnRefresh_Click);
    componentResourceManager.ApplyResources((object) this.mnpBrowse, "mnpBrowse");
    this.mnpBrowse.ImageIndex = 1;
    this.mnpBrowse.ShowText = true;
    this.mnpBrowse.Click += new EventHandler(this.BtnBrowse_Click);
    this.ListVars.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnNumber,
      this.columnVariants,
      this.columnDateTime
    });
    componentResourceManager.ApplyResources((object) this.ListVars, "ListVars");
    this.ListVars.FullRowSelect = true;
    this.ListVars.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.ListVars.LargeImageList = this.imagesList;
    this.ListVars.MultiSelect = false;
    this.ListVars.Name = "ListVars";
    this.menuBar1.SetPopupMenu((Control) this.ListVars, (MenuBarItem) this.contextMenuBarItem);
    this.ListVars.SmallImageList = this.imagesList;
    this.ListVars.UseCompatibleStateImageBehavior = false;
    this.ListVars.View = View.Details;
    this.ListVars.Resize += new EventHandler(this.ListVars_Resize);
    this.ListVars.SelectedIndexChanged += new EventHandler(this.ListVars_SelectedIndexChanged);
    this.ListVars.DoubleClick += new EventHandler(this.ListVars_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnNumber, "columnNumber");
    componentResourceManager.ApplyResources((object) this.columnVariants, "columnVariants");
    componentResourceManager.ApplyResources((object) this.columnDateTime, "columnDateTime");
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.BtnClose);
    this.panelBottom.Controls.Add((Control) this.lbWarning);
    this.panelBottom.Controls.Add((Control) this.imgWarning);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.BtnClose, "BtnClose");
    this.BtnClose.Cursor = Cursors.Hand;
    this.BtnClose.DialogResult = DialogResult.Cancel;
    this.BtnClose.Name = "BtnClose";
    this.BtnClose.UseVisualStyleBackColor = true;
    componentResourceManager.ApplyResources((object) this.lbWarning, "lbWarning");
    this.lbWarning.Name = "lbWarning";
    componentResourceManager.ApplyResources((object) this.imgWarning, "imgWarning");
    this.imgWarning.Name = "imgWarning";
    this.imgWarning.TabStop = false;
    this.toolBarAdv.AllowVerticalDock = false;
    this.toolBarAdv.DockLine = 3;
    this.toolBarAdv.FullMenus = true;
    this.toolBarAdv.Guid = new Guid("ba855ba6-35ae-4775-b979-b76ac70a54e0");
    this.toolBarAdv.Hidden = false;
    this.toolBarAdv.ImageList = this.imagesToolbar;
    this.toolBarAdv.Items.AddRange(new ToolbarItemBase[5]
    {
      (ToolbarItemBase) this.BtnAddVars,
      (ToolbarItemBase) this.BtnChangeVars,
      (ToolbarItemBase) this.BtnSetAsMain,
      (ToolbarItemBase) this.BtnRefresh,
      (ToolbarItemBase) this.BtnDelVars
    });
    componentResourceManager.ApplyResources((object) this.toolBarAdv, "toolBarAdv");
    this.toolBarAdv.MinimumFloatingSize = new Size(250, 30);
    this.toolBarAdv.Name = "toolBarAdv";
    this.toolBarAdv.Stretch = true;
    this.BtnAddVars.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnAddVars, "BtnAddVars");
    this.BtnAddVars.ImageIndex = 3;
    this.BtnAddVars.ShowText = true;
    this.BtnAddVars.Click += new EventHandler(this.BtnAddVars_Click);
    componentResourceManager.ApplyResources((object) this.BtnChangeVars, "BtnChangeVars");
    this.BtnChangeVars.ImageIndex = 4;
    this.BtnChangeVars.ShowText = true;
    this.BtnChangeVars.Click += new EventHandler(this.DoEditVars);
    componentResourceManager.ApplyResources((object) this.BtnSetAsMain, "BtnSetAsMain");
    this.BtnSetAsMain.ImageIndex = 5;
    this.BtnSetAsMain.ShowText = true;
    this.BtnSetAsMain.Click += new EventHandler(this.DoSetAsMain);
    this.BtnRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnRefresh, "BtnRefresh");
    this.BtnRefresh.ImageIndex = 2;
    this.BtnRefresh.ShowText = true;
    this.BtnRefresh.Click += new EventHandler(this.BtnRefresh_Click);
    this.BtnDelVars.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnDelVars, "BtnDelVars");
    this.BtnDelVars.ImageIndex = 6;
    this.BtnDelVars.ShowText = true;
    this.BtnDelVars.Click += new EventHandler(this.BtnDelVars_Click);
    this.AcceptButton = (IButtonControl) this.BtnClose;
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.BtnClose;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.ListVars);
    this.Controls.Add((Control) this.toolBarAdv);
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.menuBar1);
    this.Controls.Add((Control) this.toolbarFilter);
    this.Name = nameof (FiltrationSettingsForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.FiltrationSettingsForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.FiltrationSettingsForm_FormClosed);
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.imgWarning).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void FiltrationSettingsForm_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void FiltrationSettingsForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public void UpdateControls()
  {
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.ListVars.SelectedItems.Count > 0)
      listViewItem = this.ListVars.SelectedItems[0];
    bool flag1 = this._FSFiltration != null && this._FSOwnerID.Length > 0;
    this.cbCurrentVersionRule.Enabled = flag1;
    this.BtnBrowse.Enabled = flag1;
    this.mnpBrowse.Enabled = this.BtnBrowse.Enabled;
    this.mnpBrowse.Visible = this.mnpBrowse.Enabled;
    this.ListVars.Enabled = this.cbCurrentVersionRule.Enabled && this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.RuleObjectID != 0L;
    bool flag2 = this.ListVars.Enabled && listViewItem != null && this.ListVars.Items.Count > 0 && (!this._FSRuleValid || this._FSFiltration.CurrentRule != null && !this._FSFiltration.CurrentRule.HasVariableValues());
    this.BtnDelVars.Enabled = ((!this.ListVars.Enabled ? 0 : (listViewItem != null ? 1 : 0)) | (flag2 ? 1 : 0)) != 0;
    this.mnpDeleteVars.Enabled = this.BtnDelVars.Enabled;
    this.mnpDeleteVars.Visible = this.BtnDelVars.Enabled;
    this.BtnAddVars.Enabled = this._FSRuleValid && this.ListVars.Enabled && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.HasVariableValues();
    this.mnpAddVars.Enabled = this.BtnAddVars.Enabled;
    this.mnpAddVars.Visible = this.BtnAddVars.Enabled;
    this.BtnChangeVars.Enabled = this.BtnAddVars.Enabled && listViewItem != null && listViewItem.ImageIndex < 2;
    this.mnpEditVars.Enabled = this.BtnChangeVars.Enabled;
    this.mnpEditVars.Visible = this.BtnChangeVars.Enabled;
    this.BtnSetAsMain.Enabled = this.BtnAddVars.Enabled && listViewItem != null && listViewItem.ImageIndex == 0;
    this.mnpSetAsMain.Enabled = this.BtnSetAsMain.Enabled;
    this.mnpSetAsMain.Visible = this.BtnSetAsMain.Enabled;
    this._FSRuleErrorCode = 2;
    string str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip2;
    int fsImage = this._FSImages[0];
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this._FSFiltration.CurrentRule.RuleObjectID == 0L)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip0;
      fsImage = this._FSImages[2];
      this._FSRuleErrorCode = 0;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this._FSFiltration.CurrentRule.RuleObjectID != 0L && !this._FSRuleValid)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip6;
      fsImage = this._FSImages[5];
      this._FSRuleErrorCode = 6;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this._FSFiltration.CurrentRule.RuleObjectID != 0L && this._FSRuleValid && this._FSFiltration.CurrentRule.HasVariableValues() && (this._FSFiltration.CurrentRuleVars < 0 || this._FSVarsOutOfRange))
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip5;
      fsImage = this._FSImages[1];
      this._FSRuleErrorCode = 5;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this._FSFiltration.CurrentRule.RuleObjectID != 0L && this._FSRuleValid && this._FSFiltration.CurrentRule.HasVariableValues() && this.ListVars.Items.Count <= 0)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip3;
      fsImage = this._FSImages[1];
      this._FSRuleErrorCode = 5;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this._FSFiltration.CurrentRule.RuleObjectID != 0L && this._FSRuleValid && !this._FSRuleCompatible)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip1;
      fsImage = this._FSImages[2];
      this._FSRuleErrorCode = 1;
    }
    if (this._FSOwnerID.Length <= 0)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip2;
      fsImage = this._FSImages[0];
      this._FSRuleErrorCode = 2;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this._FSFiltration.CurrentRule.RuleObjectID != 0L && this._FSRuleValid && !this._FSRuleCompatible)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip1;
      fsImage = this._FSImages[2];
      this._FSRuleErrorCode = 1;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType != VersionsRuleType.vrtStandardRule)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip2;
      fsImage = this._FSImages[0];
      this._FSRuleErrorCode = 2;
    }
    if (this._FSFiltration != null && this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule && !this.FEnableAllVersions)
    {
      str = FiltrationSettingsForm.FiltrationSettingsFormConsts.Tip7;
      fsImage = this._FSImages[2];
      this._FSRuleErrorCode = 7;
    }
    this.imgWarning.Image = this.namedImageList.ImageList.Images[fsImage];
    this.lbWarning.Text = str;
  }

  /// <summary>
  /// Добавить в список новый элемент с указанными правилами
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="Vars">Вариант значения переменных для текущего правила подбора версий</param>
  /// <returns>Элемент списка или null</returns>
  private ListViewItem AddListItem(IUserSession session, VersionsRule Vars)
  {
    if (Vars == null || this._FSFiltration == null)
      return (ListViewItem) null;
    ListViewItem listViewItem = this.ListVars.Items.Add($"{this.ListVars.Items.Count + 1}");
    int num = 0;
    if (listViewItem.Index == this._FSFiltration.CurrentRuleVars)
      num = 1;
    if (!Vars.Valid(session) || this._FSFiltration.CurrentRule != null && !this._FSFiltration.CurrentRule.IsCompatible(Vars))
      num = 2;
    listViewItem.ImageIndex = num;
    listViewItem.SubItems.Add(Vars.GetDisplayValue(2).ToString());
    if (Vars.ActualDate != DateTime.MinValue)
      listViewItem.SubItems.Add(Convert.ToString(Vars.ActualDate + session.TimeZoneOffset));
    else
      listViewItem.SubItems.Add(LocalizationHolder.rm.GetString("Client.Core_794"));
    if (listViewItem != null)
      listViewItem.Tag = (object) Vars;
    return listViewItem;
  }

  /// <summary>
  /// Заполнить список вариантами значений переменных текущего правила
  /// </summary>
  public void FillList()
  {
    if (this._FSIsLoading)
      return;
    try
    {
      this.ListVars.BeginUpdate();
      this.ListVars.Items.Clear();
      if (this._FSFiltration == null || this._FSFiltration.CurrentRule == null || this._FSFiltration.CurrentRule.CurrentRuleType != VersionsRuleType.vrtStandardRule || this._FSFiltration.CurrentRule.RuleObjectID == 0L)
        return;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IUserSession session = sessionKeeper.Session;
        IVersionRulesCacheService rulesCacheService;
        try
        {
          rulesCacheService = session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        }
        catch
        {
          rulesCacheService = (IVersionRulesCacheService) null;
        }
        if (rulesCacheService == null)
          return;
        int num = rulesCacheService.RuleVarsCount(session.UserID, this._FSFiltration.CurrentRule.RuleObjectID);
        if (num <= 0)
          return;
        for (int index = 0; index < num; ++index)
          this.AddListItem(session, rulesCacheService.GetRuleVars(session.UserID, index, this._FSFiltration.CurrentRule.RuleObjectID));
      }
    }
    finally
    {
      this.ListVars.EndUpdate();
    }
  }

  /// <summary>Вернуть настройки фильтрации для текущего владельца</summary>
  private void ReloadFiltrationSettings()
  {
    if (this._FSFiltration == null)
      this._FSFiltration = new FiltrationSettings();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      this._FSFiltration.Assign(customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this._FSOwnerID, false));
      this._FSFiltration.CurrentRule = customService.GetFiltrationRule((object) sessionKeeper.Session.SessionGUID, (IFiltrationSettings) this._FSFiltration, ref this._FSRuleCompatible, ref this._FSRuleValid, ref this._FSVarsOutOfRange);
      if (this._FSLatestVersionsRule.Empty())
        this._FSLatestVersionsRule.Assign((object) customService.LatestVersionsRule);
      if (!this._FSAllVersionsRule.Empty())
        return;
      this._FSAllVersionsRule.Assign((object) customService.AllVersionsRule);
    }
  }

  /// <summary>Загрузить настройки фильтрации состава в форму</summary>
  public void LoadFilterData()
  {
    if (this._FSIsLoading)
      return;
    bool fsIsLoading = this._FSIsLoading;
    try
    {
      this.Clear();
      this._FSIsLoading = true;
      this.ReloadFiltrationSettings();
      this.FillFilterCombobox();
    }
    finally
    {
      this._FSIsLoading = fsIsLoading;
      this.FillList();
      this.UpdateControls();
    }
  }

  /// <summary>Сохранить настройки фильтрации состава</summary>
  public void SaveFilterData()
  {
    if (this._FSOwnerID.Length <= 0 || this._FSFiltration.OwnerID != this._FSOwnerID)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      if (this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType != VersionsRuleType.vrtStandardRule)
      {
        this._FSRuleCompatible = true;
        this._FSRuleValid = true;
        this._FSVarsOutOfRange = false;
        if (this._FSFiltration.CurrentRule == null || this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
          this._FSFiltration.CurrentRule.Assign((object) this._FSLatestVersionsRule);
        if (this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
          this._FSFiltration.CurrentRule.Assign((object) this._FSAllVersionsRule);
      }
      customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this._FSOwnerID, this._FSFiltration);
      string OwnerID = sessionKeeper.Session.UserID.ToString();
      FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, OwnerID, false) ?? new FiltrationSettings();
      filtrationSettings.Assign(this._FSFiltration);
      filtrationSettings.OwnerID = OwnerID;
      customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, OwnerID, filtrationSettings);
    }
  }

  /// <summary>
  /// Изменить ширину колонки (колонок) в списке при изменении размеров формы
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ListVars_Resize(object sender, EventArgs e)
  {
    int num = this.ListVars.ClientRectangle.Width - this.ListVars.Columns[0].Width - this.ListVars.Columns[2].Width - 35;
    if (num <= 0)
      return;
    this.ListVars.Columns[1].Width = num;
  }

  /// <summary>Выбрать из базы данных другое правило отбора версий</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void BtnBrowse_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(FiltrationSettingsForm.FiltrationSettingsFormConsts.Dialog1, FiltrationSettingsForm.FiltrationSettingsFormConsts.Dialog2, ObjectTypesHelper.GetObjTypeID("cad001b3-306c-11d8-b4e9-00304f19f545"), SelectionOptions.Default);
    if (numArray == null)
      return;
    long Object_ID = numArray[0];
    if (!this.FEnableAllVersions)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        if (customService.RuleType((object) sessionKeeper.Session.SessionGUID, Object_ID) == VersionsRuleType.vrtAllVersionsRule)
        {
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString(sc_4594.ssp_imclient_4595()), FiltrationSettingsForm.FiltrationSettingsFormConsts.Dialog3, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return;
        }
        this._FSFiltration.CurrentRule = customService[Object_ID];
      }
    }
    if (this._FSFiltration == null)
      this.ReloadFiltrationSettings();
    this.SaveFilterData();
    this.LoadFilterData();
  }

  /// <summary>
  /// Найти в ComboBox элемент с системным правилом указанного типа
  /// </summary>
  /// <param name="RuleType">Допустимые типы системных правил - vrtLatestVersionsRule и vrtAllVersionsRule</param>
  /// <returns>Элемент, содержащий ссылку на системное правило указанного типа, или null</returns>
  private MyElement GetComboItem(VersionsRuleType RuleType)
  {
    if (this.cbCurrentVersionRule.ComboBox.Items.Count > 0)
    {
      foreach (MyElement comboItem in this.cbCurrentVersionRule.ComboBox.Items)
      {
        if (comboItem != null && comboItem.Value != null && comboItem.Value is VersionsRule versionsRule && versionsRule.CurrentRuleType == RuleType)
          return comboItem;
      }
    }
    return (MyElement) null;
  }

  /// <summary>Заполнить и настроить ComboBox тулбара</summary>
  private void FillFilterCombobox()
  {
    bool fsIsLoading = this._FSIsLoading;
    try
    {
      this._FSIsLoading = true;
      MyElement myElement1 = (MyElement) null;
      MyElement myElement2 = this.GetComboItem(VersionsRuleType.vrtLatestVersionsRule);
      if (myElement2 == null)
      {
        myElement2 = new MyElement((object) this._FSLatestVersionsRule, $"{VersionsRuleConsts.ruleLatestVersions}", (object) 0);
        this.cbCurrentVersionRule.ComboBox.Items.Insert(0, (object) myElement2);
      }
      if (this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
        this.cbCurrentVersionRule.ComboBox.SelectedItem = (object) myElement2;
      else if (this._FSFiltration.CurrentRule != null && this._FSFiltration.CurrentRule.RuleObjectID == 0L)
      {
        this.cbCurrentVersionRule.ComboBox.SelectedIndex = -1;
      }
      else
      {
        if (this._FSFiltration.CurrentRule != null && this.cbCurrentVersionRule.ComboBox.Items.Count > 0)
        {
          foreach (MyElement myElement3 in this.cbCurrentVersionRule.ComboBox.Items)
          {
            if (myElement3 != null && Convert.ToInt64(myElement3.Tag) == this._FSFiltration.CurrentRule.RuleObjectID)
            {
              myElement1 = myElement3;
              break;
            }
          }
        }
        string caption = $"{this._FSFiltration.CurrentRule.RuleObjectCaption} [{this._FSFiltration.CurrentRule.RuleObjectID}]";
        if (this._FSFiltration.CurrentRuleVars >= 0 && this._FSRuleErrorCode != 5)
          caption = string.Format(LocalizationHolder.rm.GetString("Client.Core_795"), (object) this._FSFiltration.CurrentRule.RuleObjectCaption, (object) this._FSFiltration.CurrentRule.RuleObjectID, (object) (this._FSFiltration.CurrentRuleVars + 1));
        if (myElement1 == null)
        {
          myElement1 = new MyElement((object) null, caption, (object) this._FSFiltration.CurrentRule.RuleObjectID);
          this.cbCurrentVersionRule.ComboBox.Items.Add((object) myElement1);
        }
        else
          myElement1.Caption = caption;
        int index = this.cbCurrentVersionRule.ComboBox.Items.IndexOf((object) myElement1);
        if (index >= 0 && index < this.cbCurrentVersionRule.ComboBox.Items.Count)
          this.cbCurrentVersionRule.ComboBox.Items[index] = (object) myElement1;
        this.cbCurrentVersionRule.ComboBox.SelectedItem = (object) myElement1;
      }
    }
    finally
    {
      this._FSIsLoading = fsIsLoading;
    }
  }

  /// <summary>Изменился индекс в ComboBox</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void cbFiltrationRule_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this._FSOwnerID == string.Empty || this._FSIsLoading)
      return;
    bool fsIsLoading = this._FSIsLoading;
    try
    {
      this._FSIsLoading = true;
      VersionsRule versionsRule = (VersionsRule) null;
      if (this.cbCurrentVersionRule.ComboBox.SelectedItem != null)
      {
        if (!(this.cbCurrentVersionRule.ComboBox.SelectedItem is MyElement selectedItem))
          return;
        Convert.ToInt64(selectedItem.Tag);
        versionsRule = selectedItem.Value as VersionsRule;
      }
      if (versionsRule != null && versionsRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
      {
        this._FSFiltration.CurrentRule = versionsRule;
        this.SaveFilterData();
      }
      else
      {
        this._FSFiltration.CurrentRule = versionsRule;
        this.SaveFilterData();
      }
    }
    finally
    {
      this._FSIsLoading = fsIsLoading;
      this.LoadFilterData();
    }
  }

  /// <summary>
  /// Добавить новый вариант значений переменных в текущее правило
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void BtnAddVars_Click(object sender, EventArgs e)
  {
    if (!this.ListVars.Enabled || this._FSFiltration.CurrentRule == null)
      return;
    VersionsRule Vars = this._FSFiltration.CurrentRule.Clone() as VersionsRule;
    if (!this._FSFiltration.CurrentRule.IsCompatible(Vars))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService rulesCacheService;
      try
      {
        rulesCacheService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      }
      catch
      {
        rulesCacheService = (IVersionRulesCacheService) null;
      }
      if (rulesCacheService == null || this._FSFiltration == null || rulesCacheService.RuleVarsCount(sessionKeeper.Session.UserID, this._FSFiltration.CurrentRule.RuleObjectID) >= 9999)
        return;
      rulesCacheService.RuleVarsAdd((object) sessionKeeper.Session.SessionGUID, Vars, this._FSFiltration.CurrentRule.RuleObjectID);
    }
    this.LoadFilterData();
    this.SaveFilterData();
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.ListVars.Items.Count > 0)
      listViewItem = this.ListVars.Items[this.ListVars.Items.Count - 1];
    if (listViewItem == null)
      return;
    listViewItem.Selected = true;
    this.DoSetAsMain(sender, e);
    this.DoEditVars(sender, e);
  }

  /// <summary>Обновить список вариантов значений переменных</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void BtnRefresh_Click(object sender, EventArgs e) => this.LoadFilterData();

  /// <summary>
  /// Изменить вариант значений переменных для текущего правила
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoEditVars(object sender, EventArgs e)
  {
    if (this._FSFiltration == null || this._FSFiltration.CurrentRule == null)
      return;
    ListViewItem listViewItem1 = (ListViewItem) null;
    if (this.ListVars.SelectedItems.Count > 0)
      listViewItem1 = this.ListVars.SelectedItems[0];
    if (listViewItem1 == null)
      return;
    int index = listViewItem1.Index;
    VersionsRule ruleVars;
    try
    {
      ruleVars = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).GetRuleVars((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).UserID, listViewItem1.Index, this._FSFiltration.CurrentRule.RuleObjectID);
    }
    catch
    {
      return;
    }
    VersionRulesEditorForm versionRulesEditorForm = new VersionRulesEditorForm();
    versionRulesEditorForm._expandAllCheckBox.Checked = true;
    versionRulesEditorForm.LoadObjectData(ruleVars, 1);
    if (versionRulesEditorForm.ShowDialog() != DialogResult.OK)
      return;
    ruleVars.Assign((object) versionRulesEditorForm.RuleClass);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).SetRuleVars((object) sessionKeeper.Session.SessionGUID, this._FSFiltration.CurrentRule.RuleObjectID, index, ruleVars);
      }
      catch
      {
        return;
      }
    }
    this.FillList();
    try
    {
      ListViewItem listViewItem2 = this.ListVars.Items[index];
      if (listViewItem2 != null)
      {
        listViewItem2.Selected = true;
        listViewItem2.Focused = true;
      }
    }
    catch
    {
    }
    this.UpdateControls();
  }

  private void DoSetAsMain(object sender, EventArgs e)
  {
    if (this._FSFiltration == null)
      return;
    ListViewItem listViewItem1 = (ListViewItem) null;
    if (this.ListVars.SelectedItems.Count > 0)
      listViewItem1 = this.ListVars.SelectedItems[0];
    if (listViewItem1 == null)
    {
      this.UpdateControls();
    }
    else
    {
      int index = this._FSFiltration.CurrentRuleVars;
      if (index == listViewItem1.Index)
        return;
      if (index >= this.ListVars.Items.Count)
        index = -1;
      this._FSFiltration.CurrentRuleVars = listViewItem1.Index;
      this.SaveFilterData();
      listViewItem1.ImageIndex = 1;
      ListViewItem listViewItem2;
      try
      {
        listViewItem2 = index < 0 ? (ListViewItem) null : this.ListVars.Items[index];
      }
      catch
      {
        listViewItem2 = (ListViewItem) null;
      }
      if (listViewItem2 != null)
        listViewItem2.ImageIndex = 0;
      this.UpdateControls();
    }
  }

  private void ListVars_SelectedIndexChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>
  /// Удалить текущий элемент из списка вариантов значений переменных
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void BtnDelVars_Click(object sender, EventArgs e)
  {
    ListViewItem listViewItem1 = (ListViewItem) null;
    if (this.ListVars.SelectedItems.Count > 0)
      listViewItem1 = this.ListVars.SelectedItems[0];
    if ((this._FSFiltration == null || this._FSFiltration.CurrentRule == null || this._FSFiltration.CurrentRule.RuleObjectID == 0L ? 0 : (listViewItem1 != null ? 1 : 0)) == 0)
      return;
    int index = listViewItem1.Index;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService rulesCacheService;
      try
      {
        rulesCacheService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      }
      catch
      {
        rulesCacheService = (IVersionRulesCacheService) null;
      }
      rulesCacheService?.RuleVarsDel((object) sessionKeeper.Session.SessionGUID, this._FSFiltration.CurrentRule.RuleObjectID, listViewItem1.Index);
    }
    this.LoadFilterData();
    this.SaveFilterData();
    if (this._FSFiltration.CurrentRuleVars >= this.ListVars.Items.Count && this.ListVars.Items.Count > 0)
    {
      ListViewItem listViewItem2 = this.ListVars.Items[this.ListVars.Items.Count - 1];
      if (listViewItem2 == null)
        return;
      listViewItem2.Selected = true;
    }
    else
    {
      if (index >= this._FSFiltration.CurrentRuleVars || this._FSFiltration.CurrentRuleVars - 1 >= this.ListVars.Items.Count - 1)
        return;
      ListViewItem listViewItem3 = this.ListVars.Items[this._FSFiltration.CurrentRuleVars - 1];
      if (listViewItem3 == null)
        return;
      listViewItem3.Selected = true;
      this.DoSetAsMain(sender, e);
    }
  }

  private void ListVars_DoubleClick(object sender, EventArgs e)
  {
    if (this._FSFiltration == null)
      return;
    ListViewItem listViewItem = (ListViewItem) null;
    if (this.ListVars.SelectedItems.Count > 0)
      listViewItem = this.ListVars.SelectedItems[0];
    if (listViewItem == null)
      return;
    if (listViewItem.ImageIndex == 0)
    {
      this.DoSetAsMain(sender, e);
    }
    else
    {
      if (listViewItem.ImageIndex != 1)
        return;
      this.DoEditVars(sender, e);
    }
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    IToolBarRenderer renderer = (sender as BarManager).Renderer;
    this.toolBarAdv.Renderer = renderer;
    this.toolbarFilter.Renderer = renderer;
  }

  /// <summary>Коллекция констант для формы FiltrationSettingsForm</summary>
  private static class FiltrationSettingsFormConsts
  {
    /// <summary>Выбор текущего правила подбора версий</summary>
    internal static readonly string Dialog1 = LocalizationHolder.rm.GetString("Client.Core_782");
    /// <summary>
    /// Выберите правило, по которому будет выполняться фильтрация состава объектов
    /// </summary>
    internal static readonly string Dialog2 = LocalizationHolder.rm.GetString("Client.Core_783");
    /// <summary>Выбрано системное правило подбора версий</summary>
    internal static readonly string Dialog3 = LocalizationHolder.rm.GetString("Client.Core_784");
    /// <summary>
    /// Выбрано системное правило подбора версий \"Все версии объектов\".\nПрименять его в данном контексте нельзя.\nПожалуйста, выберите другое правило подбора версий.
    /// </summary>
    internal static readonly string Dialog4 = LocalizationHolder.rm.GetString("Client.Core_785");
    /// <summary>Выберите правило для фильтрации состава объектов</summary>
    public static readonly string Tip0 = LocalizationHolder.rm.GetString("Client.Core_786");
    /// <summary>
    /// Вариант значений недействителен, поскольку его правило подбора было изменено
    /// </summary>
    public static readonly string Tip1 = LocalizationHolder.rm.GetString("Client.Core_787");
    /// <summary>Правило успешно подготовлено для фильтрации состава</summary>
    public static readonly string Tip2 = LocalizationHolder.rm.GetString("Client.Core_788");
    /// <summary>Требуется хотя бы один вариант значений</summary>
    public static readonly string Tip3 = LocalizationHolder.rm.GetString("Client.Core_789");
    /// <summary>Фильтрация состава отключена</summary>
    public const string Tip4 = "";
    /// <summary>Требуется указать основной вариант значений</summary>
    public static readonly string Tip5 = LocalizationHolder.rm.GetString("Client.Core_791");
    /// <summary>Указанное правило является некорректным</summary>
    public static readonly string Tip6 = LocalizationHolder.rm.GetString("Client.Core_792");
    /// <summary>
    /// Системное правило \"Все версии объектов\" в данном контексте выбирать нельзя
    /// </summary>
    public static readonly string Tip7 = LocalizationHolder.rm.GetString("Client.Core_793");
    internal const int MaxVariants = 9999;
  }
}
