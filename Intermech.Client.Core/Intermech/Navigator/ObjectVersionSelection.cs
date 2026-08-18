
// Type: Intermech.Navigator.ObjectVersionSelection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.DBObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TenTec.Windows.iGridLib;


namespace Intermech.Navigator;

/// <summary>
/// Форма, позволяющая выбирать версии определённого объекта
/// </summary>
public class ObjectVersionSelection : Form
{
  /// <summary>Коллекция изображений для разных категорий</summary>
  private ICategoryTypeIconService _objtypesIcons;
  /// <summary>Кэш графических объектов "Навигатора"</summary>
  private INavGraphicsCache _navGraphicsCache;
  /// <summary>Информация о текущем пользователе</summary>
  private ICurrentUserAndRole _currUser;
  /// <summary>Список изображений</summary>
  private ImageList _images;
  /// <summary>Индекс изображения "imgBaseVersion"</summary>
  internal static int _imgBaseVersion = -1;
  /// <summary>Индекс изображения "imgNonBaseVersion"</summary>
  internal static int _imgNonBaseVersion = -1;
  /// <summary>Шрифт</summary>
  internal static Font _boldFont;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableMain;
  private PictureBox picturePrompt;
  private Label labelPrompt;
  protected Panel panelBottom;
  protected Button btnCancel;
  protected Button btnApply;
  private iGrid gridVersions;
  private iGCellStyle gridVersionsCol0CellStyle;
  private iGColHdrStyle gridVersionsCol0ColHdrStyle;
  private iGCellStyle gridVersionsCol1CellStyle;
  private iGColHdrStyle gridVersionsCol1ColHdrStyle;
  private iGCellStyle gridVersionsCol2CellStyle;
  private iGColHdrStyle gridVersionsCol2ColHdrStyle;
  private iGCellStyle gridVersionsCol3CellStyle;
  private iGColHdrStyle gridVersionsCol3ColHdrStyle;
  private iGCellStyle gridVersionsCol4CellStyle;
  private iGColHdrStyle gridVersionsCol4ColHdrStyle;

  /// <summary>Конструктор</summary>
  public ObjectVersionSelection()
  {
    this.InitializeComponent();
    if (this.DesignMode)
      return;
    this.Init();
  }

  /// <summary>Метод для инициализации формы</summary>
  protected virtual void Init()
  {
    Rectangle primaryWorkingArea = MultiscreenHelper.PrimaryWorkingArea;
    this.Size = new Size(primaryWorkingArea.Width / 100 * 60, primaryWorkingArea.Height / 100 * 50);
    this.Location = new Point((primaryWorkingArea.Width - this.Size.Width) / 2 + primaryWorkingArea.Left, (primaryWorkingArea.Height - this.Size.Height) / 2 + primaryWorkingArea.Top);
    this._objtypesIcons = ServicesManager.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
    this._navGraphicsCache = ServicesManager.GetService(typeof (INavGraphicsCache)) as INavGraphicsCache;
    this._currUser = ServicesManager.GetService(typeof (ICurrentUserAndRole)) as ICurrentUserAndRole;
    this.gridVersions.ImageList = this._objtypesIcons != null ? this._objtypesIcons.ImageList : (ImageList) null;
    this.UpdateControls();
  }

  /// <summary>Заполнить форму списком версий</summary>
  /// <param name="descriptions"></param>
  /// <param name="colored">Список версий, которые требуется выделить цветом текста в списке</param>
  /// <param name="descriptions">Список с описаниями версий объектов (object совместим с ObjectVersionDescription)</param>
  /// <param name="excluded">Список версий, которые не требуется отображать в окене</param>
  protected virtual void Fill(
    List<object> descriptions,
    List<long> colored,
    params long[] excluded)
  {
    try
    {
      this._images = Holder.NamedImageList.ImageList;
      if (ObjectVersionSelection._imgBaseVersion < 0)
      {
        ObjectVersionSelection._imgNonBaseVersion = Holder.NamedImageList.ImageIndex("imgNonBaseVersion");
        ObjectVersionSelection._imgBaseVersion = Holder.NamedImageList.ImageIndex("imgBaseVersion");
        ObjectVersionSelection._boldFont = new Font(this.gridVersions.Font, FontStyle.Bold);
      }
      IUserNamesCache userNamesCache = CacheManager.Cache("UserNamesCache") as IUserNamesCache;
      this.gridVersions.BeginUpdate();
      this.gridVersions.Rows.Clear();
      if (descriptions == null || descriptions.Count == 0)
        return;
      List<long> longList = new List<long>((IEnumerable<long>) excluded);
      for (int index = 0; index < descriptions.Count; ++index)
      {
        if (descriptions[index] is ObjectVersionDescription description && !longList.Contains(description.F_OBJECT_ID))
        {
          iGRow iGrow = this.gridVersions.Rows.Add();
          iGrow.Key = index.ToString();
          int num = colored == null ? 0 : (colored.IndexOf(description.F_OBJECT_ID) >= 0 ? 1 : 0);
          iGrow.Cells[GridColumnKeys.columnIMAGE].ImageIndex = this._objtypesIcons.IndexOf(4, description.F_OBJECT_TYPE);
          iGrow.Cells[GridColumnKeys.columnF_OBJECT_ID].Value = (object) description.F_OBJECT_ID;
          iGrow.Cells[GridColumnKeys.columnF_OBJECT_ID].ImageList = this._images;
          iGrow.Cells[GridColumnKeys.columnF_OBJECT_ID].ImageIndex = (description.F_BASE_VERSION & 1L) != 0L ? ObjectVersionSelection._imgBaseVersion : ObjectVersionSelection._imgNonBaseVersion;
          iGrow.Cells[GridColumnKeys.columnF_VERSION_ID].Value = (object) description.F_VERSION_ID;
          iGrow.Cells[GridColumnKeys.columnCAPTION].Value = (object) CaptionTransform.GetCaption(description.CAPTION, description.F_VERSION_ID);
          iGrow.Cells[GridColumnKeys.columnF_CHKOUTBY_ID].Value = (object) userNamesCache.GetUserName(description.F_CHKOUT_BY);
          if (num != 0)
          {
            for (int colIndex = 0; colIndex < iGrow.Cells.Count; ++colIndex)
              iGrow.Cells[colIndex].Font = ObjectVersionSelection._boldFont;
          }
          iGrow.Tag = descriptions[index];
        }
      }
      this.gridVersions.SortObject.Clear();
      this.gridVersions.SortObject.Add(GridColumnKeys.columnF_VERSION_ID, iGSortOrder.Ascending);
      this.gridVersions.Sort();
    }
    finally
    {
      this.gridVersions.EndUpdate();
      this.UpdateControls();
    }
  }

  /// <summary>
  /// Выбрать одну из версий объекта с указанным идентификатором.
  /// Дополнительно задаётся список версий, которые не требуется показывать в окне
  /// </summary>
  /// <param name="F_ID">Идентификатор объекта</param>
  /// <param name="showAllModifications">Выключить фильтрацию контекстных версий, которые в данный момент невидимы в Навигаторе</param>
  /// <param name="colored">Список версий, которые требуется выделить цветом текста в списке</param>
  /// <param name="excluded">Список версий объектов, которые не требуется показывать в окне</param>
  /// <returns>Выбранная версия, либо Intermech.Consts.UnknownObjectId</returns>
  public static long SelectVersion(
    long F_ID,
    bool showAllModifications,
    List<long> colored,
    params long[] excluded)
  {
    long num1 = 0;
    if (F_ID == num1)
      return num1;
    using (ObjectVersionSelection versionSelection = new ObjectVersionSelection())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject objectById = sessionKeeper.Session.GetObjectByID(F_ID, false);
        int objectType = objectById != null ? objectById.ObjectType : -1;
        List<object> descriptions = ObjectVersionDescriptionsHelper.LoadDescriptions(sessionKeeper.Session, typeof (ObjectVersionDescription), F_ID, objectType, showAllModifications);
        versionSelection.Fill(descriptions, colored, excluded);
        if (versionSelection.gridVersions.Rows.Count == 0)
        {
          int num2 = (int) MessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1438"), "Intermech Professional Solution", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          return num1;
        }
        versionSelection.AllowMultiSelect = false;
        return versionSelection.ShowDialog() != DialogResult.OK ? num1 : (versionSelection.gridVersions.SelectedCells[0].Row.Tag as ObjectVersionDescription).F_OBJECT_ID;
      }
    }
  }

  /// <summary>
  /// Можно ли использовать множественное выделение в списке версий
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual bool AllowMultiSelect
  {
    get => this.gridVersions.SelectionMode == iGSelectionMode.MultiSimple;
    set
    {
      this.gridVersions.SelectionMode = value ? iGSelectionMode.MultiSimple : iGSelectionMode.One;
    }
  }

  /// <summary>
  /// Идентификатор выделенной версии объекта (значение Intermech.Consts.UnknownObjectId -
  /// нет выделенной версии)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual long CurrentObjectID
  {
    get
    {
      long currentObjectId = 0;
      if (this.gridVersions.SelectedCells.Count == 0)
        return currentObjectId;
      iGRow row = this.gridVersions.SelectedCells[0].Row;
      ObjectVersionDescription tag = row != null ? row.Tag as ObjectVersionDescription : (ObjectVersionDescription) null;
      return tag == null ? currentObjectId : tag.F_OBJECT_ID;
    }
  }

  /// <summary>
  /// Идентификаторы выделенных версий объектов (пустой список, если нет ни одной
  /// выделенной версии)
  /// </summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public virtual List<long> CurrentObjectIDs
  {
    get
    {
      List<long> currentObjectIds = new List<long>();
      if (this.gridVersions.SelectedCells.Count == 0)
        return currentObjectIds;
      for (int index = 0; index < this.gridVersions.SelectedCells.Count; ++index)
      {
        iGRow row = this.gridVersions.SelectedCells[index].Row;
        ObjectVersionDescription tag = row != null ? row.Tag as ObjectVersionDescription : (ObjectVersionDescription) null;
        if (tag != null)
          currentObjectIds.Add(tag.F_OBJECT_ID);
      }
      return currentObjectIds;
    }
  }

  /// <summary>Обновить состояние контролов</summary>
  protected virtual void UpdateControls()
  {
    this.btnApply.Enabled = this.CurrentObjectID != 0L;
    this.btnCancel.Enabled = true;
  }

  /// <summary>Загрузим положение формы из настроек пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ObjectVersionSelection_Load(object sender, EventArgs e)
  {
    FormStorage.LoadLayout((Control) this);
  }

  /// <summary>Сохраним положение формы в настройках пользователя</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ObjectVersionSelection_FormClosed(object sender, FormClosedEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
  }

  /// <summary>Изменилась выделенная строка в гриде</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void gridVersions_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  /// <summary>Нажата кнопка "ОК"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void DoOK(object sender, EventArgs e)
  {
    if (this.CurrentObjectID == 0L)
      return;
    this.DialogResult = DialogResult.OK;
  }

  /// <summary>Пользовательская отрисовка фона в ячейках грида</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void gridVersions_CustomDrawCellBackground(object sender, iGCustomDrawCellEventArgs e)
  {
    ObjectVersionDescription tag = e.RowIndex >= 0 ? this.gridVersions.Rows[e.RowIndex].Tag as ObjectVersionDescription : (ObjectVersionDescription) null;
    if (tag == null || tag.F_CHKOUT_BY == 0L)
      return;
    NavGradientBrush navGradientBrush = (NavGradientBrush) null;
    Rectangle bounds = e.Bounds;
    if (tag.F_CHKOUT_BY != this._currUser.UserID && tag.F_CHKOUT_BY > 0L)
    {
      bool useGradient = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckedOutOther) == GradientUsing.CheckedOutOther;
      navGradientBrush = this._navGraphicsCache.GetNavGradientBrush(this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkStartColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherBkEndColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutOtherGradientMode, bounds, useGradient);
    }
    if (tag.F_OBJECT_ID < 0L && tag.F_CHKOUT_BY == this._currUser.UserID)
    {
      bool useGradient = (this._navGraphicsCache.CurrentColorsScheme.Gradient & GradientUsing.CheckOut) == GradientUsing.CheckOut;
      navGradientBrush = this._navGraphicsCache.GetNavGradientBrush(this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkStartColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutBkEndColor, this._navGraphicsCache.CurrentColorsScheme.CheckedOutGradientMode, bounds, useGradient);
    }
    if (navGradientBrush == null)
      return;
    try
    {
      e.Graphics.FillRectangle(navGradientBrush.Brush, bounds);
    }
    finally
    {
      navGradientBrush.Dispose();
    }
  }

  /// <summary>Двойной клик мышью в ячейке</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void gridVersions_CellDoubleClick(object sender, iGCellDoubleClickEventArgs e)
  {
    this.UpdateControls();
    if (!this.btnApply.Enabled)
      return;
    this.DoOK(sender, (EventArgs) e);
  }

  /// <summary>Требуется открытие справочной системы</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="hlpevent">Аргументы события</param>
  private void ObjectVersionSelection_HelpRequested(object sender, HelpEventArgs hlpevent)
  {
    HelpProvidersClass.ShowHelpTopic(774);
  }

  /// <summary>Нажата кнопка "Справка"</summary>
  /// <param name="sender">Отправитель</param>
  /// <param name="e">Аргументы события</param>
  private void ObjectVersionSelection_HelpButtonClicked(object sender, CancelEventArgs e)
  {
    HelpProvidersClass.ShowHelpTopic(774);
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
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (ObjectVersionSelection));
    iGColPattern iGcolPattern1 = new iGColPattern();
    iGColPattern iGcolPattern2 = new iGColPattern();
    iGColPattern iGcolPattern3 = new iGColPattern();
    iGColPattern iGcolPattern4 = new iGColPattern();
    iGColPattern iGcolPattern5 = new iGColPattern();
    this.gridVersionsCol3CellStyle = new iGCellStyle(true);
    this.gridVersionsCol3ColHdrStyle = new iGColHdrStyle(true);
    this.gridVersionsCol0CellStyle = new iGCellStyle(true);
    this.gridVersionsCol0ColHdrStyle = new iGColHdrStyle(true);
    this.gridVersionsCol1CellStyle = new iGCellStyle(true);
    this.gridVersionsCol1ColHdrStyle = new iGColHdrStyle(true);
    this.gridVersionsCol2CellStyle = new iGCellStyle(true);
    this.gridVersionsCol2ColHdrStyle = new iGColHdrStyle(true);
    this.gridVersionsCol4CellStyle = new iGCellStyle(true);
    this.gridVersionsCol4ColHdrStyle = new iGColHdrStyle(true);
    this.tableMain = new TableLayoutPanel();
    this.gridVersions = new iGrid();
    this.picturePrompt = new PictureBox();
    this.labelPrompt = new Label();
    this.panelBottom = new Panel();
    this.btnCancel = new Button();
    this.btnApply = new Button();
    this.tableMain.SuspendLayout();
    ((ISupportInitialize) this.gridVersions).BeginInit();
    ((ISupportInitialize) this.picturePrompt).BeginInit();
    this.panelBottom.SuspendLayout();
    this.SuspendLayout();
    this.gridVersionsCol3CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    this.gridVersionsCol3CellStyle.Flags = iGCellFlags.DisplayImage;
    this.gridVersionsCol3CellStyle.ImageAlign = iGContentAlignment.MiddleCenter;
    this.gridVersionsCol0CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    this.gridVersionsCol0CellStyle.ReadOnly = iGBool.True;
    this.gridVersionsCol0CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridVersionsCol0CellStyle.ValueType = typeof (long);
    this.gridVersionsCol0ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridVersionsCol1CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    this.gridVersionsCol1CellStyle.ReadOnly = iGBool.True;
    this.gridVersionsCol1CellStyle.TextAlign = iGContentAlignment.MiddleRight;
    this.gridVersionsCol1CellStyle.ValueType = typeof (long);
    this.gridVersionsCol1ColHdrStyle.TextAlign = iGContentAlignment.MiddleRight;
    this.gridVersionsCol2CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    this.gridVersionsCol2CellStyle.ReadOnly = iGBool.NotSet;
    this.gridVersionsCol2CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridVersionsCol2CellStyle.ValueType = typeof (string);
    this.gridVersionsCol2ColHdrStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridVersionsCol4CellStyle.CustomDrawFlags = iGCustomDrawFlags.Background;
    this.gridVersionsCol4CellStyle.ReadOnly = iGBool.NotSet;
    this.gridVersionsCol4CellStyle.TextAlign = iGContentAlignment.MiddleLeft;
    this.gridVersionsCol4CellStyle.ValueType = typeof (string);
    componentResourceManager.ApplyResources((object) this.tableMain, "tableMain");
    this.tableMain.Controls.Add((Control) this.gridVersions, 0, 1);
    this.tableMain.Controls.Add((Control) this.picturePrompt, 0, 0);
    this.tableMain.Controls.Add((Control) this.labelPrompt, 1, 0);
    this.tableMain.Name = "tableMain";
    componentResourceManager.ApplyResources((object) this.gridVersions, "gridVersions");
    this.gridVersions.AutoResizeCols = true;
    iGcolPattern1.AllowGrouping = false;
    iGcolPattern1.AllowMoving = false;
    iGcolPattern1.AllowSizing = false;
    iGcolPattern1.CellStyle = this.gridVersionsCol3CellStyle;
    iGcolPattern1.ColHdrStyle = this.gridVersionsCol3ColHdrStyle;
    componentResourceManager.ApplyResources((object) iGcolPattern1, "iGColPattern1");
    iGcolPattern1.SortOrder = iGSortOrder.None;
    iGcolPattern1.SortType = iGSortType.None;
    iGcolPattern2.AllowMoving = false;
    iGcolPattern2.CellStyle = this.gridVersionsCol0CellStyle;
    iGcolPattern2.ColHdrStyle = this.gridVersionsCol0ColHdrStyle;
    iGcolPattern2.CustomGrouping = true;
    componentResourceManager.ApplyResources((object) iGcolPattern2, "iGColPattern2");
    iGcolPattern3.AllowMoving = false;
    iGcolPattern3.CellStyle = this.gridVersionsCol1CellStyle;
    iGcolPattern3.ColHdrStyle = this.gridVersionsCol1ColHdrStyle;
    iGcolPattern3.CustomGrouping = true;
    componentResourceManager.ApplyResources((object) iGcolPattern3, "iGColPattern3");
    iGcolPattern4.AllowMoving = false;
    iGcolPattern4.CellStyle = this.gridVersionsCol2CellStyle;
    iGcolPattern4.ColHdrStyle = this.gridVersionsCol2ColHdrStyle;
    iGcolPattern4.CustomGrouping = true;
    componentResourceManager.ApplyResources((object) iGcolPattern4, "iGColPattern4");
    iGcolPattern5.AllowMoving = false;
    iGcolPattern5.CellStyle = this.gridVersionsCol4CellStyle;
    iGcolPattern5.ColHdrStyle = this.gridVersionsCol4ColHdrStyle;
    iGcolPattern5.CustomGrouping = true;
    componentResourceManager.ApplyResources((object) iGcolPattern5, "iGColPattern5");
    this.gridVersions.Cols.AddRange(new iGColPattern[5]
    {
      iGcolPattern1,
      iGcolPattern2,
      iGcolPattern3,
      iGcolPattern4,
      iGcolPattern5
    });
    this.tableMain.SetColumnSpan((Control) this.gridVersions, 2);
    this.gridVersions.DefaultRow.Height = (int) componentResourceManager.GetObject("resource.Height");
    this.gridVersions.DefaultRow.Key = componentResourceManager.GetString("resource.Key");
    this.gridVersions.DefaultRow.NormalCellHeight = (int) componentResourceManager.GetObject("resource.NormalCellHeight");
    this.gridVersions.Header.Height = (int) componentResourceManager.GetObject("gridVersions.Header.Height");
    this.gridVersions.HotTracking = false;
    this.gridVersions.Name = "gridVersions";
    this.gridVersions.ProcessTab = false;
    this.gridVersions.ReadOnly = true;
    this.gridVersions.RowMode = true;
    this.gridVersions.SilentValidation = true;
    this.gridVersions.CellDoubleClick += new iGCellDoubleClickEventHandler(this.gridVersions_CellDoubleClick);
    this.gridVersions.CustomDrawCellBackground += new iGCustomDrawCellEventHandler(this.gridVersions_CustomDrawCellBackground);
    this.gridVersions.SelectionChanged += new EventHandler(this.gridVersions_SelectionChanged);
    componentResourceManager.ApplyResources((object) this.picturePrompt, "picturePrompt");
    this.picturePrompt.MaximumSize = new Size(64 /*0x40*/, 64 /*0x40*/);
    this.picturePrompt.MinimumSize = new Size(16 /*0x10*/, 16 /*0x10*/);
    this.picturePrompt.Name = "picturePrompt";
    this.picturePrompt.TabStop = false;
    componentResourceManager.ApplyResources((object) this.labelPrompt, "labelPrompt");
    this.labelPrompt.Name = "labelPrompt";
    componentResourceManager.ApplyResources((object) this.panelBottom, "panelBottom");
    this.panelBottom.Controls.Add((Control) this.btnCancel);
    this.panelBottom.Controls.Add((Control) this.btnApply);
    this.panelBottom.Name = "panelBottom";
    componentResourceManager.ApplyResources((object) this.btnCancel, "btnCancel");
    this.btnCancel.Cursor = Cursors.Default;
    this.btnCancel.DialogResult = DialogResult.Cancel;
    this.btnCancel.Name = "btnCancel";
    componentResourceManager.ApplyResources((object) this.btnApply, "btnApply");
    this.btnApply.Cursor = Cursors.Default;
    this.btnApply.Name = "btnApply";
    this.btnApply.Click += new EventHandler(this.DoOK);
    this.AcceptButton = (IButtonControl) this.btnApply;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.CancelButton = (IButtonControl) this.btnCancel;
    this.Controls.Add((Control) this.tableMain);
    this.Controls.Add((Control) this.panelBottom);
    this.HelpButton = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (ObjectVersionSelection);
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.HelpButtonClicked += new CancelEventHandler(this.ObjectVersionSelection_HelpButtonClicked);
    this.FormClosed += new FormClosedEventHandler(this.ObjectVersionSelection_FormClosed);
    this.Load += new EventHandler(this.ObjectVersionSelection_Load);
    this.HelpRequested += new HelpEventHandler(this.ObjectVersionSelection_HelpRequested);
    this.tableMain.ResumeLayout(false);
    this.tableMain.PerformLayout();
    ((ISupportInitialize) this.gridVersions).EndInit();
    ((ISupportInitialize) this.picturePrompt).EndInit();
    this.panelBottom.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
