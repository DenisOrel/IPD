
// Type: Intermech.PropertyEditors.RolesViewsForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.PropertyEditors;

/// <summary>
/// Форма "Настройка закладок" позволяет выполнять настройки видимости закладок "Навигатора"
/// </summary>
public sealed class RolesViewsForm : Form
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
  public int ParentMode;
  /// <summary>Были ли изменения в дополнительных настройках роли</summary>
  public bool IsChanged;
  /// <summary>ID выделенных объектов</summary>
  public ArrayList RoleObjectIDs = new ArrayList();
  /// <summary>Название выделенных объектов</summary>
  public string RoleObjectName = "";
  /// <summary>
  /// Название базовой роли (если выделено несколько ролей, то первая будет базовой,
  /// а её настройки будут загружены в редактор)
  /// </summary>
  public string BaseRoleObjectName = "";
  /// <summary>
  /// Выполняется ли работа внутри обработчиков событий, меняющих структуру дерева
  /// </summary>
  private bool _inEditor;
  /// <summary>Колонка "IMAGE" - значок закладки</summary>
  private const string ImageColumnKey = "IMAGE";
  /// <summary>Колонка "CHECK" - отметка</summary>
  private const string CheckBoxColumnKey = "CHECK";
  /// <summary>Колонка "MODULE" - плагин</summary>
  private const string ModuleColumnKey = "MODULE";
  /// <summary>Колонка "VIEW" - имя закладки</summary>
  private const string ViewColumnKey = "VIEW";
  /// <summary>Колонка "TYPE" - типы объектов</summary>
  private const string ObjectTypesColumnKey = "TYPE";
  /// <summary>Колонка "NOTE" - описание закладки</summary>
  private const string NoteColumnKey = "NOTE";
  private const string OrderIDColumnKey = "OrderID";
  /// <summary>Сервис именованных изображений</summary>
  private INamedImageList _namedImageList;
  /// <summary>Временный класс для работы редактора</summary>
  private AdjustableViews _views = new AdjustableViews();
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private Panel panelBottom;
  private Button _cancelButton;
  private Button _acceptButton;
  private Label lbTooltip;
  private ImageList imagesState;
  private ToolTip toolTip;
  private PictureBox imgTooltip;
  protected Panel panel1;
  private Button _setDefaultButton;
  private iGrid _grid;
  private iGCellStyle iGrid1DefaultCellStyle;
  private iGColHdrStyle iGrid1DefaultColHdrStyle;
  private iGCellStyle iGrid1RowTextColCellStyle;

  /// <summary>Создать экземпляр формы-редактора</summary>
  public RolesViewsForm()
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
      this.IsChanged = false;
      long RoleID = 0;
      if (this.RoleObjectIDs.Count > 0)
        RoleID = Convert.ToInt64(this.RoleObjectIDs[this.RoleObjectIDs.Count - 1]);
      if (RoleID == 0L)
      {
        this.CreateViewsGrid();
        this.UpdateControls();
      }
      else
      {
        ServicesManager.GetService(typeof (IFactory));
        if ((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService)
          this._views.SyncWithRoleSettings(RoleID);
        foreach (AdjustableView defaultAdjustableView in (List<AdjustableView>) AdjustableViewsHelper.GetDefaultAdjustableViews())
        {
          if (this._views.FindView(defaultAdjustableView.Name) == null)
            this._views.Add(defaultAdjustableView);
        }
        this.CreateViewsGrid();
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
    if (this.RoleObjectIDs.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (sessionKeeper.Session.GetCustomService(typeof (IVersionRulesCacheService)) is IVersionRulesCacheService customService)
      {
        for (int index = 0; index < this.RoleObjectIDs.Count; ++index)
        {
          this._views.SaveToRoleSettings(Convert.ToInt64(this.RoleObjectIDs[index]));
          customService.SaveRolesSettings((object) sessionKeeper.Session.SessionGUID, Convert.ToInt64(this.RoleObjectIDs[index]));
        }
      }
    }
    this.IsChanged = false;
    this.UpdateControls();
  }

  private void Grid_AfterCommitEdit(object sender, iGAfterCommitEditEventArgs e)
  {
    if (!(this._grid.Cols[e.ColIndex].Key == "OrderID"))
      return;
    AdjustableView tag = this._grid.Rows[e.RowIndex].Tag as AdjustableView;
    int result = 0;
    if (this._grid.Cells[e.RowIndex, e.ColIndex].Value != null && !int.TryParse(this._grid.Cells[e.RowIndex, e.ColIndex].Value.ToString(), out result))
      return;
    tag.OrderID = result;
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void Grid_CellMouseUp(object sender, iGCellMouseUpEventArgs e)
  {
    if (e.RowIndex >= this._grid.Rows.Count || e.ColIndex != 1 || e.Button != MouseButtons.Left)
      return;
    AdjustableView tag = this._grid.Rows[e.RowIndex].Tag as AdjustableView;
    int left = e.Bounds.Left;
    int width1 = e.Bounds.Width;
    Size imageSize = this._namedImageList.ImageList.ImageSize;
    int width2 = imageSize.Width;
    int num1 = (width1 - width2) / 2;
    int num2 = left + num1;
    Rectangle bounds = e.Bounds;
    int top = bounds.Top;
    bounds = e.Bounds;
    int height1 = bounds.Height;
    imageSize = this._namedImageList.ImageList.ImageSize;
    int height2 = imageSize.Height;
    int num3 = (height1 - height2) / 2;
    int num4 = top + num3;
    Rectangle rectangle;
    ref Rectangle local = ref rectangle;
    int x = num2;
    int y = num4;
    imageSize = this._namedImageList.ImageList.ImageSize;
    int width3 = imageSize.Width;
    imageSize = this._namedImageList.ImageList.ImageSize;
    int height3 = imageSize.Height;
    local = new Rectangle(x, y, width3, height3);
    if (!rectangle.Contains(e.MousePos))
      return;
    tag.Visible = !tag.Visible;
    this._grid.Invalidate(e.Bounds);
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void Grid_CustomDrawCellForeground(object sender, iGCustomDrawCellEventArgs e)
  {
    if (e.RowIndex >= this._grid.Rows.Count || e.ColIndex > 1 && e.ColIndex != 4)
      return;
    AdjustableView tag = this._grid.Rows[e.RowIndex].Tag as AdjustableView;
    Size imageSize;
    Rectangle bounds;
    if (e.ColIndex == 0)
    {
      int left = e.Bounds.Left;
      int width1 = e.Bounds.Width;
      imageSize = this._namedImageList.ImageList.ImageSize;
      int width2 = imageSize.Width;
      int num1 = (width1 - width2) / 2;
      int x = left + num1;
      int top = e.Bounds.Top;
      bounds = e.Bounds;
      int height1 = bounds.Height;
      imageSize = this._namedImageList.ImageList.ImageSize;
      int height2 = imageSize.Height;
      int num2 = (height1 - height2) / 2;
      int y = top + num2;
      int index = this._namedImageList.ImageIndex(tag.ImageName);
      if (index >= 0)
        this._namedImageList.ImageList.Draw(e.Graphics, new Point(x, y), index);
    }
    if (e.ColIndex == 1)
    {
      bounds = e.Bounds;
      int left = bounds.Left;
      bounds = e.Bounds;
      int width3 = bounds.Width;
      imageSize = this.imagesState.ImageSize;
      int width4 = imageSize.Width;
      int num3 = (width3 - width4) / 2;
      int x = left + num3;
      bounds = e.Bounds;
      int top = bounds.Top;
      bounds = e.Bounds;
      int height3 = bounds.Height;
      imageSize = this.imagesState.ImageSize;
      int height4 = imageSize.Height;
      int num4 = (height3 - height4) / 2;
      int y = top + num4;
      this.imagesState.Draw(e.Graphics, new Point(x, y), tag.Visible ? 1 : 0);
    }
    if (e.ColIndex != 4)
      return;
    ImageList imageList32x16 = Images32x16_Cache.GetImageList32x16();
    bounds = e.Bounds;
    int num5 = bounds.Left + 2;
    bounds = e.Bounds;
    int top1 = bounds.Top;
    bounds = e.Bounds;
    int height5 = bounds.Height;
    imageSize = imageList32x16.ImageSize;
    int height6 = imageSize.Height;
    int num6 = (height5 - height6) / 2;
    int num7 = top1 + num6;
    List<int> objectTypes = tag.ObjectTypes;
    if (objectTypes == null || objectTypes.Count == 0)
      return;
    for (int index1 = 0; index1 < objectTypes.Count; ++index1)
    {
      int num8 = num5 + 20;
      imageSize = imageList32x16.ImageSize;
      int num9 = imageSize.Width / 2;
      int num10 = num8 + num9;
      bounds = e.Bounds;
      int right = bounds.Right;
      if (num10 > right)
        break;
      int image32x16Index = Images32x16_Cache.GetImage32x16Index(4, objectTypes[index1], (NavigatorTreeNode) null);
      if (image32x16Index >= 0)
      {
        ImageList imageList = imageList32x16;
        Graphics graphics = e.Graphics;
        int x = num5;
        int y = num7;
        imageSize = imageList32x16.ImageSize;
        int width = imageSize.Width / 2;
        imageSize = imageList32x16.ImageSize;
        int height7 = imageSize.Height;
        int index2 = image32x16Index;
        imageList.Draw(graphics, x, y, width, height7, index2);
        int num11 = num5;
        imageSize = imageList32x16.ImageSize;
        int num12 = imageSize.Width / 2;
        num5 = num11 + num12 + 2;
      }
    }
  }

  private void Grid_EllipsisBtnClick(object sender, iGEllipsisBtnClickEventArgs e)
  {
    iGRow row = this._grid.Rows[e.RowIndex];
    AdjustableView tag = row != null ? row.Tag as AdjustableView : (AdjustableView) null;
    if (tag == null || SelectionListWindow.Execute((System.IServiceProvider) ServicesManager.ServiceContainer, tag.ObjectTypes) != DialogResult.OK)
      return;
    this._grid.Refresh();
    this.IsChanged = true;
    this.UpdateControls();
  }

  private void Grid_Resize(object sender, EventArgs e)
  {
    if (this._grid.Cols.Count < 4)
      return;
    int num = this._grid.ClientRectangle.Width - this._grid.Cols[0].Width - this._grid.Cols[1].Width - this._grid.Cols[2].Width - this._grid.Cols[3].Width - 30;
    if (num <= 0)
      return;
    this._grid.Cols[4].Width = num;
  }

  private void SetDefaultButton_Click(object sender, EventArgs e)
  {
    this._views = AdjustableViewsHelper.GetDefaultAdjustableViews();
    this.CreateViewsGrid();
    this.IsChanged = true;
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
      if (!this.IsChanged)
        return;
      this.SaveObjectData();
    }
  }

  private void CancelButton_Click(object sender, EventArgs e)
  {
    if (this.ParentMode == 1)
      return;
    if (this._editorMode == 1 && this.ParentMode == 0)
      this.DialogResult = DialogResult.Cancel;
    else if (this._editorMode == 0 && this.ParentMode == 0)
    {
      if (!this.IsChanged)
      {
        this.DialogResult = DialogResult.Cancel;
      }
      else
      {
        if (MessageBox.Show(RolesViewsForm.RolesViewsFormConsts.Dialog1, RolesViewsForm.RolesViewsFormConsts.Dialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
          return;
        this.DialogResult = DialogResult.Cancel;
      }
    }
    else
    {
      if (this._editorMode != 0 || this.ParentMode != 2 || !this.IsChanged || MessageBox.Show(RolesViewsForm.RolesViewsFormConsts.Dialog1, RolesViewsForm.RolesViewsFormConsts.Dialog2, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
        return;
      this.LoadObjectData(this._editorMode);
    }
  }

  /// <summary>Инициализация данных</summary>
  private void Init()
  {
    this._namedImageList = ServicesManager.GetService(typeof (INamedImageList)) as INamedImageList;
    this.PrepareGridsColumns();
    Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
    this.Size = new Size(workingArea.Width / 100 * 70, workingArea.Height / 100 * 60);
    this.Location = new Point((workingArea.Width - this.Size.Width) / 2, (workingArea.Height - this.Size.Height) / 2);
    this._grid.ImageList = this._namedImageList.ImageList;
    if (this._grid.SortObject.Count == 0)
    {
      this._grid.SortObject.Add(2);
      this._grid.SortObject.Add(3);
    }
    if (this._grid.GroupObject.Count == 0)
      this._grid.GroupObject.Add(2);
    this.CreateViewsGrid();
    this.UpdateControls();
  }

  /// <summary>Установить статус всех контролов формы</summary>
  private void UpdateControls()
  {
    this._acceptButton.Enabled = this.ParentMode != 1 && this._editorMode == 0 && this.IsChanged;
    this._acceptButton.Visible = this.ParentMode != 1 && this._editorMode == 0;
    if (this.ParentMode == 0)
      this._acceptButton.Text = RolesViewsForm.RolesViewsFormConsts.ApplyText2;
    if (this.ParentMode == 2)
      this._acceptButton.Text = RolesViewsForm.RolesViewsFormConsts.ApplyText1;
    this._cancelButton.Visible = this.ParentMode != 1;
    this._cancelButton.Enabled = this._cancelButton.Visible && this.IsChanged;
    if (this._editorMode == 0)
      this._cancelButton.Text = RolesViewsForm.RolesViewsFormConsts.CancelText1;
    if (this._editorMode == 1)
      this._cancelButton.Text = RolesViewsForm.RolesViewsFormConsts.CancelText2;
    this.imgTooltip.Visible = this.RoleObjectIDs != null && this.RoleObjectIDs.Count > 1;
    this.lbTooltip.Text = string.Format(RolesViewsForm.RolesViewsFormConsts.Tooltip1, (object) this.BaseRoleObjectName);
    this.lbTooltip.Visible = this.imgTooltip.Visible;
  }

  /// <summary>Добавить в список очередную закладку "Навигатора"</summary>
  /// <param name="view">Закладка "Навигатора"</param>
  /// <returns>Добавленная закладка в виде строки или null</returns>
  private void AddView(AdjustableView view)
  {
    if (view == null)
      return;
    iGRow iGrow = this._grid.Rows.Add();
    iGrow.Cells["MODULE"].Value = (object) view.Module;
    iGrow.Cells["VIEW"].Value = (object) view.Caption;
    iGrow.Cells["NOTE"].Value = (object) view.Hint;
    iGrow.Cells["OrderID"].Value = (object) view.OrderID;
    iGrow.Tag = (object) view;
  }

  /// <summary>Построить список закладок "Навигатора"</summary>
  private void CreateViewsGrid()
  {
    try
    {
      this._grid.BeginUpdate();
      this._grid.Rows.Clear();
      for (int index = 0; index < this._views.Count; ++index)
        this.AddView(this._views[index]);
    }
    finally
    {
      this._grid.Sort();
      this._grid.Group();
      this._grid.EndUpdate();
    }
  }

  /// <summary>Создать в гриде колонки</summary>
  private void PrepareGridsColumns()
  {
    iGCellStyle iGcellStyle1 = new iGCellStyle(true);
    iGcellStyle1.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    iGcellStyle1.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle1.SingleClickEdit = iGBool.False;
    iGcellStyle1.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle2 = new iGCellStyle(true);
    iGcellStyle2.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    iGcellStyle2.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle2.SingleClickEdit = iGBool.False;
    iGcellStyle2.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle3 = new iGCellStyle(true);
    iGcellStyle3.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle3.TextAlign = iGContentAlignment.MiddleLeft;
    iGcellStyle3.SingleClickEdit = iGBool.False;
    iGcellStyle3.ReadOnly = iGBool.True;
    iGCellStyle iGcellStyle4 = new iGCellStyle(true);
    iGcellStyle4.CustomDrawFlags = iGCustomDrawFlags.Foreground;
    iGcellStyle4.Flags = iGCellFlags.DisplayText | iGCellFlags.DisplayImage;
    iGcellStyle4.ReadOnly = iGBool.False;
    iGcellStyle4.SingleClickEdit = iGBool.False;
    iGcellStyle4.TypeFlags = iGCellTypeFlags.HasEllipsisBtn;
    iGcellStyle4.Type = iGCellType.Check;
    iGcellStyle4.ValueType = typeof (object);
    (this._grid.Cols["IMAGE"] ?? this._grid.Cols.Add(new iGColPattern(32 /*0x20*/, true, true, 32 /*0x20*/, 32 /*0x20*/, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) string.Empty, "IMAGE", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle1;
    (this._grid.Cols["CHECK"] ?? this._grid.Cols.Add(new iGColPattern(32 /*0x20*/, true, true, 32 /*0x20*/, 32 /*0x20*/, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) string.Empty, "CHECK", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle2;
    iGCol iGcol = this._grid.Cols["MODULE"] ?? this._grid.Cols.Add(new iGColPattern(256 /*0x0100*/, true, true, 128 /*0x80*/, -1, true, false, true, iGSortType.ByTextNoCase, iGSortOrder.Ascending, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1606"), "MODULE", -1, (object) null, (object) null, -1));
    iGcol.CellStyle = iGcellStyle3;
    iGcol.AllowGrouping = true;
    iGcol.AllowMoving = true;
    (this._grid.Cols["VIEW"] ?? this._grid.Cols.Add(new iGColPattern(256 /*0x0100*/, true, true, 128 /*0x80*/, -1, true, false, false, iGSortType.ByTextNoCase, iGSortOrder.Ascending, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1607"), "VIEW", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle3;
    (this._grid.Cols["TYPE"] ?? this._grid.Cols.Add(new iGColPattern(164, true, true, 164, 164, false, false, false, iGSortType.None, iGSortOrder.None, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1608"), "TYPE", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle4;
    (this._grid.Cols["OrderID"] ?? this._grid.Cols.Add(new iGColPattern(150, true, true, 150, 150, false, false, false, iGSortType.ByValue, iGSortOrder.Ascending, false, (object) null, (object) "Порядок", "OrderID", -1, (object) null, (object) null, -1))).CellStyle = new iGCellStyle(true)
    {
      Flags = iGCellFlags.DisplayText,
      ReadOnly = iGBool.False,
      SingleClickEdit = iGBool.True,
      Type = iGCellType.Text,
      ValueType = typeof (int)
    };
    (this._grid.Cols["NOTE"] ?? this._grid.Cols.Add(new iGColPattern(256 /*0x0100*/, true, true, 128 /*0x80*/, -1, true, false, false, iGSortType.ByTextNoCase, iGSortOrder.Ascending, false, (object) null, (object) LocalizationHolder.rm.GetString("Client.Core_1609"), "NOTE", -1, (object) null, (object) null, -1))).CellStyle = iGcellStyle3;
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (RolesViewsForm));
    this.panelBottom = new Panel();
    this.imgTooltip = new PictureBox();
    this.lbTooltip = new Label();
    this._cancelButton = new Button();
    this._acceptButton = new Button();
    this.imagesState = new ImageList(this.components);
    this.toolTip = new ToolTip(this.components);
    this._setDefaultButton = new Button();
    this.panel1 = new Panel();
    this._grid = new iGrid();
    this.iGrid1DefaultCellStyle = new iGCellStyle(true);
    this.iGrid1DefaultColHdrStyle = new iGColHdrStyle(true);
    this.iGrid1RowTextColCellStyle = new iGCellStyle(true);
    this.panelBottom.SuspendLayout();
    ((ISupportInitialize) this.imgTooltip).BeginInit();
    this.panel1.SuspendLayout();
    ((ISupportInitialize) this._grid).BeginInit();
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
    this.imagesState.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imagesState.ImageStream");
    this.imagesState.TransparentColor = Color.Transparent;
    this.imagesState.Images.SetKeyName(0, "unchecked.ico");
    this.imagesState.Images.SetKeyName(1, "checked.ico");
    this.imagesState.Images.SetKeyName(2, "grayed.ico");
    componentResourceManager.ApplyResources((object) this._setDefaultButton, "_setDefaultButton");
    this._setDefaultButton.Cursor = Cursors.Default;
    this._setDefaultButton.Name = "_setDefaultButton";
    this.toolTip.SetToolTip((Control) this._setDefaultButton, componentResourceManager.GetString("_setDefaultButton.ToolTip"));
    this._setDefaultButton.Click += new EventHandler(this.SetDefaultButton_Click);
    this.panel1.Controls.Add((Control) this._setDefaultButton);
    componentResourceManager.ApplyResources((object) this.panel1, "panel1");
    this.panel1.Name = "panel1";
    this._grid.BackColorEvenRows = Color.White;
    this._grid.DefaultAutoGroupRow.Height = 24;
    this._grid.DefaultCol.CellStyle = this.iGrid1DefaultCellStyle;
    this._grid.DefaultCol.ColHdrStyle = this.iGrid1DefaultColHdrStyle;
    this._grid.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this._grid.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    componentResourceManager.ApplyResources((object) this._grid, "_grid");
    this._grid.EllipsisBtnGlyph = (Image) componentResourceManager.GetObject("_grid.EllipsisBtnGlyph");
    this._grid.GroupBox.Text = componentResourceManager.GetString("_grid.GroupBox.Text");
    this._grid.GroupBox.Visible = true;
    this._grid.Header.Height = (int) componentResourceManager.GetObject("_grid.Header.Height");
    this._grid.Name = "_grid";
    this._grid.RowMode = true;
    this._grid.RowModeHasCurCell = true;
    this._grid.RowTextCol.CellStyle = this.iGrid1RowTextColCellStyle;
    this._grid.ShowControlsInAllCells = false;
    this._grid.SilentValidation = true;
    this._grid.UniqueKeys = true;
    this._grid.CellMouseUp += new iGCellMouseUpEventHandler(this.Grid_CellMouseUp);
    this._grid.EllipsisBtnClick += new iGEllipsisBtnClickEventHandler(this.Grid_EllipsisBtnClick);
    this._grid.CustomDrawCellForeground += new iGCustomDrawCellEventHandler(this.Grid_CustomDrawCellForeground);
    this._grid.AfterCommitEdit += new iGAfterCommitEditEventHandler(this.Grid_AfterCommitEdit);
    this._grid.Resize += new EventHandler(this.Grid_Resize);
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this._grid);
    this.Controls.Add((Control) this.panel1);
    this.Controls.Add((Control) this.panelBottom);
    this.Name = nameof (RolesViewsForm);
    this.ShowInTaskbar = false;
    this.panelBottom.ResumeLayout(false);
    ((ISupportInitialize) this.imgTooltip).EndInit();
    this.panel1.ResumeLayout(false);
    ((ISupportInitialize) this._grid).EndInit();
    this.ResumeLayout(false);
  }

  /// <summary>Свалка констант для формы-редактора контекстных меню</summary>
  private static class RolesViewsFormConsts
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
    /// <summary>Отмена изменений в настройках закладок</summary>
    public static readonly string Dialog2 = LocalizationHolder.rm.GetString("Client.Core_672");
    /// <summary>Базовая роль: \"{0}\"</summary>
    public static readonly string Tooltip1 = LocalizationHolder.rm.GetString("Client.Core_643");
  }
}
