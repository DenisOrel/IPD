
// Type: Intermech.PropertyEditors.VersionRulesViewForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

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

/// <summary>Форма для панели "Фильтрация состава"</summary>
public class VersionRulesViewForm : Form
{
  /// <summary>
  /// Ссылка на интерфейс IFiltrationClass окна-владельца, для того, чтобы получать настройки фильтрации состава
  /// </summary>
  public IFiltrationClass FiltrationClass;
  /// <summary>
  /// Уведомлять тулбар "Фильтрация состава" при изменениях в редактируемых настройках фильтрации состава
  /// </summary>
  public bool NotifyFiltrationToolbar = true;
  /// <summary>
  /// Режим работы формы
  /// 0 -	полноценный редактор фильтриции состава (admin режим)
  /// 1 - хрен знает какой этот режим (user режим)
  /// 2 - просмотр фильтрации состава (read-only режим)
  /// </summary>
  public int EditorMode;
  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по дефолту)
  /// 1 - встроена внутри какой-либо формы
  /// 2 - на вьюшке "Навигатора"
  /// </summary>
  public int ParentMode;
  /// <summary>Текущие настройки фильтрации состава</summary>
  public FiltrationSettings Filtration;
  /// <summary>
  /// Экземпляр класса, инкапсулирующий в себя правило отбора версий указанного объекта
  /// </summary>
  public VersionsRule RuleClass = new VersionsRule();
  /// <summary>
  /// Является ли данный класс валидным
  /// Переменная введена для того, чтобы при каждом вызове UpdateControls не дёргать сессии
  /// </summary>
  public bool RuleClassValid;
  /// <summary>
  /// Это чтобы всякие обработчики событий не делали гнусные вещи	типа рекурсивных вызовов
  /// и падения системы по краху стека :-(
  /// </summary>
  private bool IsLoadingNow;
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareFunctionsHelper FCFunc = new CompareFunctionsHelper();
  /// <summary>Сугубо для внутреннего применения</summary>
  private CompareTypesHelper FCTypes = new CompareTypesHelper();
  private IContainer components;
  private ToolTip toolTip;
  private ComboBoxItem cbCurrentVersionRule;
  private ButtonItem BtnBrowse;
  private ButtonItem BtnAddVars;
  private ButtonItem BtnChangeVars;
  private ButtonItem BtnDelVars;
  private ButtonItem BtnRefresh;
  private ListView ListVars;
  private ColumnHeader columnNumber;
  private ColumnHeader columnVariants;
  private ColumnHeader columnDateTime;
  private Intermech.Bars.ToolBar tbMain;
  private Panel panelBottom;
  private ButtonItem BtnSetAsMain;
  private PictureBox imgWarning;
  private Label lbWarning;
  private ImageList imagesTips;
  private ImageList imagesList;
  private ImageList imagesToolbar;
  private MenuBar menuBar1;
  private ContextMenuBarItem contextMenuBarItem1;
  private MenuButtonItem mnpEditVars;
  private MenuButtonItem mnpSetAsMain;
  private MenuButtonItem mnpAddVars;
  private MenuButtonItem mnpDeleteVars;
  private MenuButtonItem mnpRefresh;
  private Button BtnClose;
  private MenuButtonItem mnpBrowse;

  /// <summary>Вернуть уникальный ID текущих настроек фильтрации</summary>
  public string CurrentFiltrationID
  {
    get
    {
      string empty = string.Empty;
      if (this.FiltrationClass != null)
        return this.FiltrationClass.FiltrationOwnerID;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return Convert.ToString(sessionKeeper.Session.UserID);
    }
  }

  /// <summary>Создать экземпляр формы</summary>
  public VersionRulesViewForm()
  {
    this.InitializeComponent();
    if (ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      service.RendererChanged += new EventHandler(this.ToolbarRendererChanged);
      this.ToolbarRendererChanged((object) service, EventArgs.Empty);
    }
    this.IsLoadingNow = false;
    this.cbCurrentVersionRule.ComboBox.SelectedIndexChanged += new EventHandler(this.cbCurrentVersionRule_SelectedIndexChanged);
    this.cbCurrentVersionRule.ComboBox.Cursor = Cursors.Hand;
    this.cbCurrentVersionRule.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCurrentVersionRule.ComboBox.MaxDropDownItems = 16 /*0x10*/;
    this.cbCurrentVersionRule.ComboBox.Sorted = true;
    this.toolTip.SetToolTip((Control) this.cbCurrentVersionRule.ComboBox, VersionRulesViewForm.VersionRulesViewFormConsts.Ctrls1);
    this.cbCurrentVersionRule.DefaultText = VersionRulesViewForm.VersionRulesViewFormConsts.Ctrls1;
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this.CreateContextMenu();
    this.cbCurrentVersionRule.ComboBox.Items.Clear();
    this.LoadFilterData(this.EditorMode);
    this.UpdateControls();
  }

  /// <summary>Почистить за собой мусор</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing && ServicesManager.GetService(typeof (BarManager)) is BarManager service)
    {
      this.tbMain.Renderer = (IToolBarRenderer) new EmptyToolbarRenderer();
      service.RendererChanged -= new EventHandler(this.ToolbarRendererChanged);
    }
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (VersionRulesViewForm));
    this.toolTip = new ToolTip(this.components);
    this.tbMain = new Intermech.Bars.ToolBar();
    this.imagesToolbar = new ImageList(this.components);
    this.cbCurrentVersionRule = new ComboBoxItem();
    this.BtnBrowse = new ButtonItem();
    this.BtnRefresh = new ButtonItem();
    this.BtnAddVars = new ButtonItem();
    this.BtnChangeVars = new ButtonItem();
    this.BtnSetAsMain = new ButtonItem();
    this.BtnDelVars = new ButtonItem();
    this.ListVars = new ListView();
    this.columnNumber = new ColumnHeader();
    this.columnVariants = new ColumnHeader();
    this.columnDateTime = new ColumnHeader();
    this.imagesList = new ImageList(this.components);
    this.panelBottom = new Panel();
    this.BtnClose = new Button();
    this.lbWarning = new Label();
    this.imgWarning = new PictureBox();
    this.menuBar1 = new MenuBar();
    this.contextMenuBarItem1 = new ContextMenuBarItem();
    this.mnpEditVars = new MenuButtonItem();
    this.mnpSetAsMain = new MenuButtonItem();
    this.mnpAddVars = new MenuButtonItem();
    this.mnpDeleteVars = new MenuButtonItem();
    this.mnpRefresh = new MenuButtonItem();
    this.mnpBrowse = new MenuButtonItem();
    this.imagesTips = new ImageList(this.components);
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.imgWarning).BeginInit();
    this.SuspendLayout();
    this.tbMain.FullMenus = true;
    this.tbMain.Guid = new Guid("6c45c99f-ce47-4612-803c-25ad580f2dfe");
    this.tbMain.Hidden = false;
    this.tbMain.ImageList = this.imagesToolbar;
    this.tbMain.Items.AddRange(new ToolbarItemBase[7]
    {
      (ToolbarItemBase) this.cbCurrentVersionRule,
      (ToolbarItemBase) this.BtnBrowse,
      (ToolbarItemBase) this.BtnRefresh,
      (ToolbarItemBase) this.BtnAddVars,
      (ToolbarItemBase) this.BtnChangeVars,
      (ToolbarItemBase) this.BtnSetAsMain,
      (ToolbarItemBase) this.BtnDelVars
    });
    componentResourceManager.ApplyResources((object) this.tbMain, "tbMain");
    this.tbMain.Name = "tbMain";
    this.tbMain.Overflow = ToolBarOverflow.Wrap;
    this.tbMain.Stretch = true;
    this.tbMain.StretchItem = (ToolbarItemBase) this.cbCurrentVersionRule;
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
    this.cbCurrentVersionRule.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbCurrentVersionRule.Importance = ToolBarItemImportance.Highest;
    this.cbCurrentVersionRule.MinimumControlWidth = 100;
    this.cbCurrentVersionRule.Padding.Bottom = 0;
    this.cbCurrentVersionRule.Padding.Left = 1;
    this.cbCurrentVersionRule.Padding.Right = 1;
    this.cbCurrentVersionRule.Padding.Top = 0;
    this.cbCurrentVersionRule.Stretch = true;
    componentResourceManager.ApplyResources((object) this.BtnBrowse, "BtnBrowse");
    this.BtnBrowse.ImageIndex = 1;
    this.BtnBrowse.Importance = ToolBarItemImportance.Highest;
    this.BtnBrowse.Click += new EventHandler(this.BtnBrowse_Click);
    this.BtnRefresh.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnRefresh, "BtnRefresh");
    this.BtnRefresh.ImageIndex = 2;
    this.BtnRefresh.Click += new EventHandler(this.BtnRefresh_Click);
    this.BtnAddVars.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnAddVars, "BtnAddVars");
    this.BtnAddVars.ImageIndex = 3;
    this.BtnAddVars.Click += new EventHandler(this.BtnAddVars_Click);
    componentResourceManager.ApplyResources((object) this.BtnChangeVars, "BtnChangeVars");
    this.BtnChangeVars.ImageIndex = 4;
    this.BtnChangeVars.Click += new EventHandler(this.DoEditVars);
    this.BtnSetAsMain.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnSetAsMain, "BtnSetAsMain");
    this.BtnSetAsMain.ImageIndex = 5;
    this.BtnSetAsMain.Click += new EventHandler(this.DoSetAsMain);
    this.BtnDelVars.BeginGroup = true;
    componentResourceManager.ApplyResources((object) this.BtnDelVars, "BtnDelVars");
    this.BtnDelVars.ImageIndex = 6;
    this.BtnDelVars.Click += new EventHandler(this.BtnDelVars_Click);
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
    this.menuBar1.SetPopupMenu((Control) this.ListVars, (MenuBarItem) this.contextMenuBarItem1);
    this.ListVars.SmallImageList = this.imagesList;
    this.ListVars.UseCompatibleStateImageBehavior = false;
    this.ListVars.View = View.Details;
    this.ListVars.Resize += new EventHandler(this.ListVars_Resize);
    this.ListVars.SelectedIndexChanged += new EventHandler(this.ListVars_SelectedIndexChanged);
    this.ListVars.DoubleClick += new EventHandler(this.ListVars_DoubleClick);
    componentResourceManager.ApplyResources((object) this.columnNumber, "columnNumber");
    componentResourceManager.ApplyResources((object) this.columnVariants, "columnVariants");
    componentResourceManager.ApplyResources((object) this.columnDateTime, "columnDateTime");
    this.imagesList.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesList.ImageStream");
    this.imagesList.TransparentColor = Color.Transparent;
    this.imagesList.Images.SetKeyName(0, "");
    this.imagesList.Images.SetKeyName(1, "");
    this.imagesList.Images.SetKeyName(2, "");
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
    componentResourceManager.ApplyResources((object) this.menuBar1, "menuBar1");
    this.menuBar1.Guid = new Guid("0909a734-928b-4c5d-9a6d-05be64690c06");
    this.menuBar1.Hidden = false;
    this.menuBar1.ImageList = this.imagesToolbar;
    this.menuBar1.Items.AddRange(new ToolbarItemBase[1]
    {
      (ToolbarItemBase) this.contextMenuBarItem1
    });
    this.menuBar1.Name = "menuBar1";
    this.menuBar1.OwnerForm = (Form) this;
    componentResourceManager.ApplyResources((object) this.contextMenuBarItem1, "contextMenuBarItem1");
    this.contextMenuBarItem1.Items.AddRange(new ToolbarItemBase[6]
    {
      (ToolbarItemBase) this.mnpEditVars,
      (ToolbarItemBase) this.mnpSetAsMain,
      (ToolbarItemBase) this.mnpAddVars,
      (ToolbarItemBase) this.mnpDeleteVars,
      (ToolbarItemBase) this.mnpRefresh,
      (ToolbarItemBase) this.mnpBrowse
    });
    this.contextMenuBarItem1.ShowText = true;
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
    this.imagesTips.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesTips.ImageStream");
    this.imagesTips.TransparentColor = Color.Transparent;
    this.imagesTips.Images.SetKeyName(0, "");
    this.imagesTips.Images.SetKeyName(1, "");
    this.imagesTips.Images.SetKeyName(2, "");
    this.imagesTips.Images.SetKeyName(3, "");
    this.imagesTips.Images.SetKeyName(4, "");
    this.imagesTips.Images.SetKeyName(5, "");
    this.imagesTips.Images.SetKeyName(6, "");
    this.AcceptButton = (IButtonControl) this.BtnClose;
    this.CancelButton = (IButtonControl) this.BtnClose;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this.panelBottom);
    this.Controls.Add((Control) this.ListVars);
    this.Controls.Add((Control) this.tbMain);
    this.Controls.Add((Control) this.menuBar1);
    this.Name = nameof (VersionRulesViewForm);
    this.ShowInTaskbar = false;
    this.Load += new EventHandler(this.VersionRulesViewForm_Load);
    this.FormClosed += new FormClosedEventHandler(this.VersionRulesViewForm_FormClosed);
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.imgWarning).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Установить статус всех контролов формы</summary>
  public void UpdateControls()
  {
    ListViewItem listViewItem1 = (ListViewItem) null;
    if (this.ListVars.SelectedItems.Count > 0)
      listViewItem1 = this.ListVars.SelectedItems[0];
    this.cbCurrentVersionRule.Enabled = this.Filtration != null;
    this.BtnBrowse.Enabled = this.cbCurrentVersionRule.Enabled;
    this.mnpBrowse.Enabled = this.BtnBrowse.Enabled;
    this.mnpBrowse.Visible = this.mnpBrowse.Enabled;
    this.ListVars.Enabled = this.cbCurrentVersionRule.Enabled && this.Filtration != null && this.Filtration.CurrentRule != null && this.Filtration.CurrentRule.RuleObjectID != 0L;
    bool flag = this.ListVars.Enabled && this.EditorMode == 0 && listViewItem1 != null && this.ListVars.Items.Count > 0 && (!this.RuleClassValid || !this.RuleClass.HasVariableValues());
    this.BtnDelVars.Enabled = ((!this.ListVars.Enabled || this.EditorMode != 0 || listViewItem1 == null || listViewItem1.ImageIndex == 1 ? 0 : (this.ListVars.Items.Count > 0 ? 1 : 0)) | (flag ? 1 : 0)) != 0;
    this.mnpDeleteVars.Enabled = this.BtnDelVars.Enabled;
    this.mnpDeleteVars.Visible = this.BtnDelVars.Enabled;
    this.BtnAddVars.Enabled = this.RuleClassValid && this.ListVars.Enabled && this.EditorMode == 0 && this.RuleClass.HasVariableValues();
    this.mnpAddVars.Enabled = this.BtnAddVars.Enabled;
    this.mnpAddVars.Visible = this.BtnAddVars.Enabled;
    this.BtnChangeVars.Enabled = this.BtnAddVars.Enabled && listViewItem1 != null && listViewItem1.ImageIndex < 2;
    this.mnpEditVars.Enabled = this.BtnChangeVars.Enabled;
    this.mnpEditVars.Visible = this.BtnChangeVars.Enabled;
    this.BtnSetAsMain.Enabled = this.BtnAddVars.Enabled && listViewItem1 != null && listViewItem1.ImageIndex == 0;
    this.mnpSetAsMain.Enabled = this.BtnSetAsMain.Enabled;
    this.mnpSetAsMain.Visible = this.BtnSetAsMain.Enabled;
    if (this.Filtration != null && this.Filtration.CurrentRule != null && this.Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this.Filtration.CurrentRule.RuleObjectID == 0L)
    {
      this.lbWarning.Text = VersionRulesViewForm.VersionRulesViewFormConsts.Tip0;
      this.imgWarning.Image = this.imagesTips.Images[0];
      this.lbWarning.Visible = true;
      this.imgWarning.Visible = true;
    }
    else if (this.Filtration != null && this.Filtration.CurrentRule != null && this.Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && this.Filtration.CurrentRule.RuleObjectID != 0L && this.RuleClass != null && !this.RuleClassValid)
    {
      this.lbWarning.Text = VersionRulesViewForm.VersionRulesViewFormConsts.Tip6;
      this.imgWarning.Image = this.imagesTips.Images[6];
      this.lbWarning.Visible = true;
      this.imgWarning.Visible = true;
    }
    else
    {
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      lock (this.ListVars)
      {
        if (this.ListVars.Items.Count > 0)
        {
          for (int index = 0; index < this.ListVars.Items.Count; ++index)
          {
            ListViewItem listViewItem2 = this.ListVars.Items[index];
            if (listViewItem2.ImageIndex == 0)
              ++num3;
            if (listViewItem2.ImageIndex == 1)
              ++num2;
            if (listViewItem2.ImageIndex == 2)
              ++num1;
          }
        }
      }
      if (this.Filtration == null || this.Filtration.CurrentRule != null && this.Filtration.CurrentRule.RuleObjectID == 0L && this.Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule || this.RuleClass == null)
      {
        this.lbWarning.Text = "";
        this.imgWarning.Image = this.imagesTips.Images[4];
        this.lbWarning.Visible = true;
        this.imgWarning.Visible = true;
      }
      else if (num1 > 0)
      {
        this.lbWarning.Text = VersionRulesViewForm.VersionRulesViewFormConsts.Tip1;
        this.lbWarning.Visible = true;
        this.imgWarning.Image = this.imagesTips.Images[1];
        this.imgWarning.Visible = true;
      }
      else if (this.RuleClass.HasVariableValues() && num3 <= 0 && num2 <= 0)
      {
        this.lbWarning.Text = VersionRulesViewForm.VersionRulesViewFormConsts.Tip3;
        this.lbWarning.Visible = true;
        this.imgWarning.Image = this.imagesTips.Images[3];
        this.imgWarning.Visible = true;
      }
      else if (this.RuleClass.HasVariableValues() & num2 <= 0)
      {
        this.lbWarning.Text = VersionRulesViewForm.VersionRulesViewFormConsts.Tip5;
        this.lbWarning.Visible = true;
        this.imgWarning.Image = this.imagesTips.Images[5];
        this.imgWarning.Visible = true;
      }
      else if (!this.RuleClass.HasVariableValues() || num1 <= 0 && num2 == 1)
      {
        this.lbWarning.Text = VersionRulesViewForm.VersionRulesViewFormConsts.Tip2;
        this.lbWarning.Visible = true;
        this.imgWarning.Image = this.imagesTips.Images[2];
        this.imgWarning.Visible = true;
      }
      else
      {
        this.lbWarning.Visible = false;
        this.imgWarning.Visible = false;
      }
    }
  }

  /// <summary>Очистка внутренних структур</summary>
  public void Clear()
  {
    this.RuleClass.Clear();
    this.RuleClassValid = false;
    this.FillList();
  }

  private void CreateContextMenu()
  {
  }

  public void SetParent(Control aParent)
  {
    if (aParent == null)
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = true;
      this.Dock = DockStyle.None;
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.Visible = false;
    }
    else
    {
      this.AutoScaleMode = AutoScaleMode.Inherit;
      this.TopLevel = false;
      this.Dock = DockStyle.Fill;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Visible = true;
    }
    this.Parent = aParent;
    this.UpdateControls();
  }

  /// <summary>Вернуть настройки фильтрации для указанного владельца</summary>
  /// <param name="OwnerID">OBJECT_ID правила подбора версий, для которого надо загрузить настройки фильтрации</param>
  /// <param name="Filtration">В эту переменную будут считаны настройки фильтрацииs</param>
  /// <param name="Rule">В это поле будет возвращено правило подбора версий с указанным RuleID</param>
  /// <param name="IsRuleValid">В это поле будет возвращён результат проверки корректности правила</param>
  /// <returns>true, если всё ок</returns>
  private bool GetFiltrationSettings(
    string OwnerID,
    ref FiltrationSettings Filtration,
    ref VersionsRule Rule,
    ref bool IsRuleValid)
  {
    if (Filtration == null)
      Filtration = new FiltrationSettings();
    if (Rule != null)
      Rule.Clear();
    string OwnerID1 = OwnerID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (OwnerID1.Length <= 0)
        OwnerID1 = this.CurrentFiltrationID;
      Filtration.OwnerID = OwnerID1;
      IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, OwnerID1, false);
      Filtration.Assign(filtrationSettings);
      if (filtrationSettings != null && filtrationSettings.CurrentRule != null)
      {
        Rule = customService[(object) sessionKeeper.Session.SessionGUID, filtrationSettings.CurrentRule.RuleObjectID];
      }
      else
      {
        Filtration.Clear();
        Filtration.CurrentRule = (VersionsRule) null;
      }
      if (Filtration.CurrentRule != null && Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtStandardRule && Rule == null)
        Rule = new VersionsRule();
      if (Filtration.CurrentRule == null || Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtLatestVersionsRule)
        Rule = customService.LatestVersionsRule;
      if (Filtration.CurrentRule != null && Filtration.CurrentRule.CurrentRuleType == VersionsRuleType.vrtAllVersionsRule)
        Rule = customService.AllVersionsRule;
      IsRuleValid = Rule.Valid(sessionKeeper.Session);
    }
    return true;
  }

  /// <summary>Загрузить настройки фильтрации состава в форму</summary>
  /// <param name="AEditorMode">Режим редактирования (0 - полноценный редактор фильтрации состава (admin режим), 1 - хрен знает какой этот режим (user режим), 2 - просмотр фильтрации состава (read-only режим))</param>
  public void LoadFilterData(int AEditorMode)
  {
    if (this.IsLoadingNow)
      return;
    try
    {
      this.Clear();
      this.IsLoadingNow = true;
      this.EditorMode = AEditorMode;
      if (this.EditorMode < 0)
        this.EditorMode = 0;
      if (this.EditorMode >= 3)
        this.EditorMode = 0;
      this.GetFiltrationSettings(this.CurrentFiltrationID, ref this.Filtration, ref this.RuleClass, ref this.RuleClassValid);
      if (this.RuleClass.RuleObjectID != 0L)
      {
        string caption = $"{this.RuleClass.RuleObjectCaption} [{this.RuleClass.RuleObjectID}]";
        MyElement myElement1 = (MyElement) null;
        if (this.cbCurrentVersionRule.ComboBox.Items.Count > 0)
        {
          foreach (MyElement myElement2 in this.cbCurrentVersionRule.ComboBox.Items)
          {
            if (myElement2 != null && Convert.ToInt64(myElement2.Tag) == this.RuleClass.RuleObjectID)
            {
              myElement1 = myElement2;
              break;
            }
          }
        }
        if (myElement1 == null)
        {
          myElement1 = new MyElement((object) this.RuleClass, caption, (object) this.RuleClass.RuleObjectID);
          this.cbCurrentVersionRule.ComboBox.Items.Add((object) myElement1);
        }
        this.cbCurrentVersionRule.ComboBox.Items.IndexOf((object) myElement1);
        this.cbCurrentVersionRule.ComboBox.SelectedItem = (object) myElement1;
      }
      else
        this.cbCurrentVersionRule.ComboBox.SelectedIndex = -1;
    }
    finally
    {
      this.IsLoadingNow = false;
    }
    this.FillList();
    this.UpdateControls();
  }

  /// <summary>Сохранить настройки фильтрации состава</summary>
  public void SaveFilterData()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      try
      {
        IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
        customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, this.CurrentFiltrationID, this.Filtration);
        string OwnerID = sessionKeeper.Session.UserID.ToString();
        FiltrationSettings filtrationSettings = customService.GetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, OwnerID, false);
        if (filtrationSettings != null)
        {
          filtrationSettings.Assign(this.Filtration);
          filtrationSettings.OwnerID = OwnerID;
          customService.SetFiltrationSettings((object) sessionKeeper.Session.SessionGUID, OwnerID, filtrationSettings);
        }
      }
      catch
      {
      }
    }
    this.UpdateControls();
    if (!this.NotifyFiltrationToolbar)
      return;
    ((IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService)))?.FiltrationUpdate(true);
  }

  private void VersionRulesViewForm_Load(object sender, EventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.LoadLayout((Control) this);
  }

  private void VersionRulesViewForm_FormClosed(object sender, FormClosedEventArgs e)
  {
    if (this.ParentMode != 0)
      return;
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>
  /// Изменить ширину колонки (колонок) в списке при изменении размеров формы
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void ListVars_Resize(object sender, EventArgs e)
  {
    int num = this.ListVars.ClientRectangle.Width - this.ListVars.Columns[0].Width - this.ListVars.Columns[2].Width - 30;
    if (num <= 0)
      return;
    this.ListVars.Columns[1].Width = num;
  }

  /// <summary>Выбрать из базы данных другое правило отбора версий</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void BtnBrowse_Click(object sender, EventArgs e)
  {
    long[] numArray = SelectionWindow.SelectObjects(VersionRulesViewForm.VersionRulesViewFormConsts.Dialog1, VersionRulesViewForm.VersionRulesViewFormConsts.Dialog2, ObjectTypesHelper.GetObjTypeID("cad001b3-306c-11d8-b4e9-00304f19f545"), SelectionOptions.Default);
    if (numArray == null)
      return;
    long num = numArray[0];
    if (this.Filtration == null)
      this.GetFiltrationSettings(string.Empty, ref this.Filtration, ref this.RuleClass, ref this.RuleClassValid);
    this.Filtration.CurrentRule = this.RuleClass;
    this.SaveFilterData();
    this.LoadFilterData(this.EditorMode);
  }

  /// <summary>Изменился индекс в ComboBox</summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void cbCurrentVersionRule_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.IsLoadingNow)
      return;
    long num = 0;
    if (this.Filtration == null)
      this.GetFiltrationSettings(string.Empty, ref this.Filtration, ref this.RuleClass, ref this.RuleClassValid);
    if (this.cbCurrentVersionRule.ComboBox.SelectedItem != null)
    {
      if (!(this.cbCurrentVersionRule.ComboBox.SelectedItem is MyElement selectedItem))
        return;
      num = Convert.ToInt64(selectedItem.Tag);
    }
    if (this.Filtration.CurrentRule != null && this.Filtration.CurrentRule.RuleObjectID == num)
      return;
    this.Filtration.CurrentRule = this.RuleClass;
    this.SaveFilterData();
    this.LoadFilterData(this.EditorMode);
  }

  /// <summary>
  /// Добавить в список новый элемент с указанными правилами
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="Vars">Вариант значения переменных для текущего правила подбора версий</param>
  /// <returns>Элемент списка или null</returns>
  private ListViewItem AddListItem(IUserSession session, VersionsRule Vars)
  {
    if (Vars == null || this.Filtration == null)
      return (ListViewItem) null;
    ListViewItem listViewItem = this.ListVars.Items.Add($"{this.ListVars.Items.Count + 1}", 0);
    int num = 0;
    if (listViewItem.Index == this.Filtration.CurrentRuleVars)
      num = 1;
    if (!Vars.Valid(session) || !this.RuleClass.IsCompatible(Vars))
      num = 2;
    listViewItem.ImageIndex = num;
    listViewItem.SubItems.Add(Vars.GetDisplayValue(2).ToString());
    listViewItem.SubItems.Add(Convert.ToString(Vars.RuleObjectModified + session.TimeZoneOffset));
    if (listViewItem != null)
      listViewItem.Tag = (object) Vars;
    return listViewItem;
  }

  /// <summary>
  /// Заполнить список вариантами значений переменных текущего правила
  /// </summary>
  public void FillList()
  {
    if (this.IsLoadingNow)
      return;
    try
    {
      this.ListVars.BeginUpdate();
      this.ListVars.Items.Clear();
      if (this.Filtration == null || this.Filtration.CurrentRule == null || this.Filtration.CurrentRule.CurrentRuleType != VersionsRuleType.vrtStandardRule || this.Filtration.CurrentRule.RuleObjectID == 0L)
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
        int num = this.Filtration.CurrentRule != null ? rulesCacheService.RuleVarsCount(session.UserID, this.Filtration.CurrentRule.RuleObjectID) : -1;
        if (num <= 0)
          return;
        for (int index = 0; index < num; ++index)
          this.AddListItem(session, rulesCacheService.GetRuleVars(session.UserID, index, this.Filtration.CurrentRule.RuleObjectID));
      }
    }
    finally
    {
      this.ListVars.EndUpdate();
    }
  }

  /// <summary>
  /// Добавить новый вариант значений переменных в текущее правило
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void BtnAddVars_Click(object sender, EventArgs e)
  {
    if (!this.ListVars.Enabled || this.EditorMode != 0)
      return;
    VersionsRule Vars = this.RuleClass.Clone() as VersionsRule;
    if (!this.RuleClass.IsCompatible(Vars))
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
      if (rulesCacheService == null || this.Filtration == null || this.Filtration.CurrentRule == null || rulesCacheService.RuleVarsCount(sessionKeeper.Session.UserID, this.Filtration.CurrentRule.RuleObjectID) >= 9999)
        return;
      rulesCacheService.RuleVarsAdd((object) sessionKeeper.Session.SessionGUID, Vars, this.Filtration.CurrentRule.RuleObjectID);
    }
    this.LoadFilterData(this.EditorMode);
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
  private void BtnRefresh_Click(object sender, EventArgs e) => this.LoadFilterData(this.EditorMode);

  /// <summary>
  /// Изменить вариант значений переменных для текущего правила
  /// </summary>
  /// <param name="sender">Засланец</param>
  /// <param name="e">Параметры</param>
  private void DoEditVars(object sender, EventArgs e)
  {
    if (this.Filtration == null || this.Filtration.CurrentRule == null)
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
      ruleVars = ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).GetRuleVars((ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole).UserID, listViewItem1.Index, this.Filtration.CurrentRule.RuleObjectID);
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
        (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService).SetRuleVars((object) sessionKeeper.Session.SessionGUID, this.Filtration.CurrentRule.RuleObjectID, index, ruleVars);
        ((IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService)))?.FiltrationUpdate(true);
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
    if (this.Filtration == null)
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
      int currentRuleVars = this.Filtration.CurrentRuleVars;
      if (currentRuleVars == listViewItem1.Index)
        return;
      IVersionRulesCacheService customService;
      try
      {
        customService = (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      }
      catch
      {
        return;
      }
      if (customService == null)
        return;
      this.Filtration.CurrentRuleVars = listViewItem1.Index;
      this.SaveFilterData();
      ((IFiltrationService) ServicesManager.GetService(typeof (IFiltrationService)))?.FiltrationUpdate(true);
      listViewItem1.ImageIndex = 1;
      ListViewItem listViewItem2;
      try
      {
        listViewItem2 = currentRuleVars < 0 ? (ListViewItem) null : this.ListVars.Items[currentRuleVars];
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
    if ((this.Filtration == null || this.Filtration.CurrentRule == null || this.Filtration.CurrentRule.RuleObjectID == 0L || this.EditorMode != 0 || listViewItem1 == null ? 0 : (listViewItem1.ImageIndex != 1 ? 1 : 0)) == 0)
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
      rulesCacheService?.RuleVarsDel((object) sessionKeeper.Session.SessionGUID, this.Filtration.CurrentRule.RuleObjectID, listViewItem1.Index);
    }
    this.LoadFilterData(this.EditorMode);
    this.SaveFilterData();
    if (this.Filtration.CurrentRuleVars >= this.ListVars.Items.Count && this.ListVars.Items.Count > 0)
    {
      ListViewItem listViewItem2 = this.ListVars.Items[this.ListVars.Items.Count - 1];
      if (listViewItem2 == null)
        return;
      listViewItem2.Selected = true;
      this.DoSetAsMain(sender, e);
    }
    else
    {
      if (index >= this.Filtration.CurrentRuleVars || this.Filtration.CurrentRuleVars - 1 >= this.ListVars.Items.Count - 1)
        return;
      ListViewItem listViewItem3 = this.ListVars.Items[this.Filtration.CurrentRuleVars - 1];
      if (listViewItem3 == null)
        return;
      listViewItem3.Selected = true;
      this.DoSetAsMain(sender, e);
    }
  }

  private void ListVars_DoubleClick(object sender, EventArgs e)
  {
    if (this.Filtration == null)
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

  /// <summary>Закрыть форму, сохранив все изменения в базе</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void BtnClose_Click(object sender, EventArgs e)
  {
    this.SaveFilterData();
    this.UpdateControls();
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>
  /// Пришло событие "Изменился рендерер панелей инструментов"
  /// </summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  protected virtual void ToolbarRendererChanged(object sender, EventArgs e)
  {
    this.tbMain.Renderer = (sender as BarManager).Renderer;
  }

  /// <summary>Свалка констант для формы</summary>
  internal abstract class VersionRulesViewFormConsts
  {
    /// <summary>[Выберите правило подбора версий]</summary>
    internal static readonly string Ctrls1 = LocalizationHolder.rm.GetString("Client.Core_835");
    /// <summary>Выбор текущего правила подбора версий</summary>
    internal static readonly string Dialog1 = LocalizationHolder.rm.GetString("Client.Core_782");
    /// <summary>
    /// Выберите правило, по которому будет выполняться фильтрация состава объектов
    /// </summary>
    internal static readonly string Dialog2 = LocalizationHolder.rm.GetString("Client.Core_783");
    /// <summary>Вы действительно хотите отменить все изменения?</summary>
    internal static readonly string Dialog3 = LocalizationHolder.rm.GetString("Client.Core_641");
    /// <summary>Отмена изменений в фильтрации состава</summary>
    internal static readonly string Dialog4 = LocalizationHolder.rm.GetString("Client.Core_836");
    /// <summary>
    /// В указанном правиле подбора версий есть переменные, значения которых Вам необходимо задать вручную.
    /// Добавьте новый вариант значений переменных в список и заполните эти значения.
    /// </summary>
    internal static readonly string Dialog5 = LocalizationHolder.rm.GetString("Client.Core_837") + LocalizationHolder.rm.GetString("Client.Core_838");
    /// <summary>
    /// В указанном правиле подбора версий есть переменные, значения которых Вам необходимо задать вручную.
    /// Выберите любой вариант значений переменных из списка и назначьте его основным вариантом.
    /// </summary>
    internal static readonly string Dialog6 = LocalizationHolder.rm.GetString("Client.Core_837") + LocalizationHolder.rm.GetString("Client.Core_839");
    /// <summary>Выберите правило для фильтрации состава объектов</summary>
    internal static readonly string Tip0 = LocalizationHolder.rm.GetString("Client.Core_786");
    /// <summary>
    /// Вариант значений недействителен, поскольку его правило подбора было изменено
    /// </summary>
    internal static readonly string Tip1 = LocalizationHolder.rm.GetString("Client.Core_787");
    /// <summary>Правило успешно подготовлено для фильтрации состава</summary>
    internal static readonly string Tip2 = LocalizationHolder.rm.GetString("Client.Core_788");
    /// <summary>Требуется хотя бы один вариант значений</summary>
    internal static readonly string Tip3 = LocalizationHolder.rm.GetString("Client.Core_789");
    /// <summary>Фильтрация состава не задействована</summary>
    internal const string Tip4 = "";
    /// <summary>Требуется указать основной вариант значений</summary>
    internal static readonly string Tip5 = LocalizationHolder.rm.GetString("Client.Core_791");
    /// <summary>Указанное правило является некорректным</summary>
    internal static readonly string Tip6 = LocalizationHolder.rm.GetString("Client.Core_792");
    internal const int MaxVariants = 9999;
  }
}
