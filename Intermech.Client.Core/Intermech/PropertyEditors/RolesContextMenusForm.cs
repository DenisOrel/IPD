
// Type: Intermech.PropertyEditors.RolesContextMenusForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using DevExpress.IM.XtraTreeList.Nodes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.ContextMenu;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.PropertyEditors;

/// <summary>Настройка контекстных меню для ролей</summary>
public sealed class RolesContextMenusForm : Form
{
  /// <summary>
  /// Режим работы формы
  /// 0 -	редактирование
  /// 1 - просмотр
  /// </summary>
  private int _editorMode;
  /// <summary>
  /// Где размещена наша форма
  /// 0 - самостоятельная форма (по дефолту)
  /// 1 - на форме-создателе новых объектов
  /// 2 - на вьюшке "Навигатора"
  /// </summary>
  public int _parentMode;
  /// <summary>ID выделенных объектов</summary>
  public ArrayList _roleObjectIDs = new ArrayList();
  /// <summary>Название выделенных объектов</summary>
  public string _roleObjectName = "";
  /// <summary>
  /// Название базовой роли (если выделено несколько ролей, то первая будет базовой,
  /// а её настройки будут загружены в редактор)
  /// </summary>
  public string _baseRoleObjectName = "";
  /// <summary>
  /// Выполняется ли работа внутри обработчиков событий, меняющих структуру дерева
  /// </summary>
  private bool _inEditor;
  /// <summary>Сервис именованных изображений</summary>
  private INamedImageList _images;
  /// <summary>Для быстрого поиска узлов панелей управления</summary>
  private Dictionary<Intermech.Bars.ToolBar, TreeListNode> _toolbars = new Dictionary<Intermech.Bars.ToolBar, TreeListNode>(0);
  /// <summary>Сервис настраиваемых команд контекстных меню</summary>
  private AdjustableMenuCommands _menus;
  /// <summary>Для быстрого поиска узлов команд</summary>
  private Dictionary<AdjustableMenuCommand, TreeListNode> _commands = new Dictionary<AdjustableMenuCommand, TreeListNode>(0);
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button _cancelButton;
  private Button _acceptButton;
  protected Panel panelMenuCommands;
  private Button _setDefaultButton;
  private Label lbTooltip;
  private ToolTip toolTip;
  private PictureBox imgTooltip;
  private ContextMenuEditor _contextMenuEditor;

  /// <summary>Создать экземпляр формы-редактора</summary>
  public RolesContextMenusForm()
  {
    this.InitializeComponent();
    this.Init();
  }

  /// <summary>Корректно назначить контрол-предок для формы</summary>
  /// <param name="aParent">Родительский оконный объект</param>
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

  /// <summary>Есть ли изменения в редакторе</summary>
  [Category("Appearance")]
  [Browsable(true)]
  public bool IsChanged => this._contextMenuEditor.IsChanged;

  /// <summary>Загрузить данные объектов в форму</summary>
  /// <param name="AEditorMode">Режим редактирования (0 - редактор, 1 - просмотр)</param>
  public void LoadObjectData(int AEditorMode)
  {
    this._editorMode = AEditorMode;
    bool inEditor = this._inEditor;
    try
    {
      this._inEditor = true;
      if (this._editorMode < 0)
        this._editorMode = 1;
      this._contextMenuEditor.IsChanged = false;
      long RoleID = 0;
      if (this._roleObjectIDs.Count > 0)
        RoleID = Convert.ToInt64(this._roleObjectIDs[this._roleObjectIDs.Count - 1]);
      if (RoleID == 0L)
      {
        this.UpdateControls();
      }
      else
      {
        IFactory service = ServicesManager.GetService(typeof (IFactory)) as IFactory;
        if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService)
        {
          this._menus = AdjustableMenusHelper.BuildFromMenuTemplate(service.ConfiguredContextMenuTemplate);
          this._menus.SyncWithRoleSettings(RoleID);
        }
        this._contextMenuEditor.AdjustableMenuCommands = this._menus;
      }
    }
    finally
    {
      this._inEditor = inEditor;
      this.UpdateControls();
    }
  }

  /// <summary>Сохранить данные в объект с ID = RoleObjectID</summary>
  public void SaveObjectData()
  {
    if (this._roleObjectIDs.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IVersionRulesCacheService customService = sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) as IVersionRulesCacheService;
      this._menus = this._contextMenuEditor.AdjustableMenuCommands;
      if (customService != null)
      {
        for (int index = 0; index < this._roleObjectIDs.Count; ++index)
        {
          this._menus.SaveToRoleSettings(Convert.ToInt64(this._roleObjectIDs[index]));
          customService.SaveRolesSettings((object) sessionKeeper.Session.SessionGUID, Convert.ToInt64(this._roleObjectIDs[index]));
        }
      }
    }
    this._contextMenuEditor.Fix();
    this.UpdateControls();
  }

  private void ContextMenuEditor_Changed(object sender, EventArgs e) => this.UpdateControls();

  private void SetDefaultButton_Click(object sender, EventArgs e)
  {
    this._menus = AdjustableMenusHelper.BuildFromMenuTemplate((ServicesManager.GetService(typeof (IFactory)) as IFactory).ContextMenuTemplate);
    this._menus.BatchPropertiesSet((object) true);
    this._contextMenuEditor.AdjustableMenuCommands = this._menus;
    this._contextMenuEditor.IsChanged = true;
    this.UpdateControls();
  }

  private void AcceptButton_Click(object sender, EventArgs e)
  {
    if (this._editorMode != 0)
    {
      this.DialogResult = DialogResult.OK;
    }
    else
    {
      if (!this._contextMenuEditor.IsChanged)
        return;
      this.SaveObjectData();
    }
  }

  private void CancelButton_Click(object sender, EventArgs e)
  {
    if (this._parentMode == 1)
      return;
    if (this._editorMode == 1 && this._parentMode == 0)
      this.DialogResult = DialogResult.Cancel;
    else if (this._editorMode == 0 && this._parentMode == 0)
    {
      if (!this._contextMenuEditor.IsChanged)
      {
        this.DialogResult = DialogResult.Cancel;
      }
      else
      {
        if (MessageBox.Show(RolesContextMenusForm.RolesContextMenusFormConsts.Dialog1, RolesContextMenusForm.RolesContextMenusFormConsts.Dialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
          return;
        this.DialogResult = DialogResult.Cancel;
      }
    }
    else
    {
      if (this._editorMode != 0 || this._parentMode != 2 || !this._contextMenuEditor.IsChanged || MessageBox.Show(RolesContextMenusForm.RolesContextMenusFormConsts.Dialog1, RolesContextMenusForm.RolesContextMenusFormConsts.Dialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.LoadObjectData(this._editorMode);
    }
  }

  /// <summary>Инициализация данных</summary>
  private void Init()
  {
    this._images = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this._menus = ServicesManager.GetService(typeof (AdjustableMenuCommands)) as AdjustableMenuCommands;
    if (this._menus != null)
      this._menus = this._menus.Clone() as AdjustableMenuCommands;
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
    this.UpdateControls();
  }

  /// <summary>Установить статус всех контролов формы</summary>
  private void UpdateControls()
  {
    this._acceptButton.Enabled = this._parentMode != 1 && this._editorMode == 0 && this._contextMenuEditor.IsChanged;
    this._acceptButton.Visible = this._parentMode != 1 && this._editorMode == 0;
    if (this._parentMode == 0)
      this._acceptButton.Text = RolesContextMenusForm.RolesContextMenusFormConsts.ApplyText2;
    if (this._parentMode == 2)
      this._acceptButton.Text = RolesContextMenusForm.RolesContextMenusFormConsts.ApplyText1;
    this._cancelButton.Visible = this._parentMode != 1;
    this._cancelButton.Enabled = this._cancelButton.Visible && this._contextMenuEditor.IsChanged;
    if (this._editorMode == 0)
      this._cancelButton.Text = RolesContextMenusForm.RolesContextMenusFormConsts.CancelText1;
    if (this._editorMode == 1)
      this._cancelButton.Text = RolesContextMenusForm.RolesContextMenusFormConsts.CancelText2;
    this.imgTooltip.Visible = this._roleObjectIDs != null && this._roleObjectIDs.Count > 1;
    this.lbTooltip.Text = string.Format(RolesContextMenusForm.RolesContextMenusFormConsts.Tooltip1, (object) this._baseRoleObjectName);
    this.lbTooltip.Visible = this.imgTooltip.Visible;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RolesContextMenusForm));
    this.panelBottom = new Panel();
    this.imgTooltip = new PictureBox();
    this.lbTooltip = new Label();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this.panelMenuCommands = new Panel();
    this._setDefaultButton = new Button();
    this.toolTip = new ToolTip(this.components);
    this._contextMenuEditor = new ContextMenuEditor();
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.imgTooltip).BeginInit();
    this.panelMenuCommands.SuspendLayout();
    ((ISupportInitialize) this._contextMenuEditor).BeginInit();
    this.SuspendLayout();
    this.panelBottom.BorderStyle = BorderStyle.Fixed3D;
    this.panelBottom.Controls.Add((Control) this.imgTooltip);
    this.panelBottom.Controls.Add((Control) this.lbTooltip);
    this.panelBottom.Controls.Add((Control) this._cancelButton);
    this.panelBottom.Controls.Add((Control) this._acceptButton);
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.imgTooltip, "imgTooltip");
    this.imgTooltip.Name = "imgTooltip";
    this.imgTooltip.TabStop = false;
    componentResourceManager.ApplyResources((object) this.lbTooltip, "lbTooltip");
    this.lbTooltip.Name = "lbTooltip";
    componentResourceManager.ApplyResources((object) this._cancelButton, "_cancelButton");
    this._cancelButton.Cursor = Cursors.Hand;
    this._cancelButton.DialogResult = DialogResult.Cancel;
    this._cancelButton.Name = "_cancelButton";
    this._cancelButton.Click += new EventHandler(this.CancelButton_Click);
    componentResourceManager.ApplyResources((object) this._acceptButton, "_acceptButton");
    this._acceptButton.Cursor = Cursors.Hand;
    this._acceptButton.Name = "_acceptButton";
    this._acceptButton.Click += new EventHandler(this.AcceptButton_Click);
    this.panelMenuCommands.Controls.Add((Control) this._setDefaultButton);
    componentResourceManager.ApplyResources((object) this.panelMenuCommands, "panelMenuCommands");
    this.panelMenuCommands.Name = "panelMenuCommands";
    componentResourceManager.ApplyResources((object) this._setDefaultButton, "_setDefaultButton");
    this._setDefaultButton.Cursor = Cursors.Default;
    this._setDefaultButton.Name = "_setDefaultButton";
    this.toolTip.SetToolTip((Control) this._setDefaultButton, componentResourceManager.GetString("_setDefaultButton.ToolTip"));
    this._setDefaultButton.Click += new EventHandler(this.SetDefaultButton_Click);
    componentResourceManager.ApplyResources((object) this._contextMenuEditor, "_contextMenuEditor");
    this._contextMenuEditor.IsChanged = false;
    this._contextMenuEditor.Name = "_contextMenuEditor";
    this._contextMenuEditor.Changed += new EventHandler(this.ContextMenuEditor_Changed);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.Controls.Add((Control) this._contextMenuEditor);
    this.Controls.Add((Control) this.panelMenuCommands);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (RolesContextMenusForm);
    this.ShowInTaskbar = false;
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.imgTooltip).EndInit();
    this.panelMenuCommands.ResumeLayout(false);
    ((ISupportInitialize) this._contextMenuEditor).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Свалка констант для формы-редактора контекстных меню</summary>
  private static class RolesContextMenusFormConsts
  {
    /// <summary>Применить</summary>
    public static readonly string ApplyText1 = LocalizationHolder.rm.GetString("Client.Core_167");
    /// <summary>ОК</summary>
    public static readonly string ApplyText2 = LocalizationHolder.rm.GetString("Client.Core_218");
    /// <summary>Отмена</summary>
    public static readonly string CancelText1 = LocalizationHolder.rm.GetString("Client.Core_166");
    /// <summary>Закрыть</summary>
    public static readonly string CancelText2 = LocalizationHolder.rm.GetString("Client.Core_217");
    /// <summary>Вы действительно хотите отменить все изменения?</summary>
    public static readonly string Dialog1 = LocalizationHolder.rm.GetString("Client.Core_641");
    /// <summary>Отмена изменений в настройках контекстных меню</summary>
    public static readonly string Dialog2 = LocalizationHolder.rm.GetString("Client.Core_642");
    /// <summary>Базовая роль: \"{0}\"</summary>
    public static readonly string Tooltip1 = LocalizationHolder.rm.GetString("Client.Core_643");
  }
}
