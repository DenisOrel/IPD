// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SetupAdditionalChaptersPanel
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using DevExpress.IM.Utils;
using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.AVS.AVSProperties;
using Intermech.AVS.Properties;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using Intermech.UI.Winforms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.AVS;

/// <summary> Класс панели настроек частей конструкторского документа</summary>
public class SetupAdditionalChaptersPanel : UserControl
{
  private IContainer components;
  private ToolTipController _editModeToolTip;
  private Button bAdd;
  private Button bDelete;
  private Intermech.VirtualTreeView.VirtualTreeView tree;
  protected Column column;
  protected CellEditor editSeparator;
  private Button bAddSection;
  private Button bRemoveSection;
  private Button bSectionDown;
  private Button bSectionUp;
  private ToolTipController _readModeToolTip;
  private List<AdditionalChapterSettings> _originalAdditionalChapters;
  private bool _isUpdating;

  public SetupAdditionalChaptersPanel()
  {
    this.InitializeComponent();
    this.tree.SelectionChanged += new EventHandler(this.tree_SelectionChanged);
    this.Init();
  }

  /// <summary> Инициализация формы </summary>
  protected void Init() => this.LoadData();

  /// <summary> Очистка использованных ресурсов </summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this._editModeToolTip != null)
      {
        this._editModeToolTip.Dispose();
        this._editModeToolTip = (ToolTipController) null;
      }
      if (this._readModeToolTip != null)
      {
        this._readModeToolTip.Dispose();
        this._readModeToolTip = (ToolTipController) null;
      }
      if (this.column != null)
      {
        this.column.Dispose();
        this.column = (Column) null;
      }
      if (this.editSeparator != null)
      {
        this.editSeparator.Dispose();
        this.editSeparator = (CellEditor) null;
      }
    }
    base.Dispose(disposing);
  }

  /// <summary> Обязательный метод, требуемый дизайнеру формы - не модифицируйте данный код </summary>
  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this._editModeToolTip = new ToolTipController(this.components);
    this._readModeToolTip = new ToolTipController(this.components);
    this.bAdd = new Button();
    this.bDelete = new Button();
    this.tree = new Intermech.VirtualTreeView.VirtualTreeView();
    this.column = new Column();
    this.editSeparator = new CellEditor();
    this.bAddSection = new Button();
    this.bRemoveSection = new Button();
    this.bSectionDown = new Button();
    this.bSectionUp = new Button();
    this.tree.BeginInit();
    this.SuspendLayout();
    this._editModeToolTip.Active = false;
    this._editModeToolTip.Style = new ViewStyle("ToolTip style");
    this._readModeToolTip.Style = new ViewStyle("ToolTip style");
    this.bAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bAdd.Location = new Point(8, 456);
    this.bAdd.Name = "bAdd";
    this.bAdd.Size = new Size(121, 27);
    this.bAdd.TabIndex = 22;
    this.bAdd.Text = "Добавить";
    this.bAdd.UseVisualStyleBackColor = true;
    this.bAdd.Visible = false;
    this.bAdd.Click += new EventHandler(this.bAdd_Click);
    this.bDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.bDelete.Location = new Point(137, 456);
    this.bDelete.Name = "bDelete";
    this.bDelete.Size = new Size(121, 27);
    this.bDelete.TabIndex = 23;
    this.bDelete.Text = "Удалить";
    this.bDelete.UseVisualStyleBackColor = true;
    this.bDelete.Visible = false;
    this.bDelete.Click += new EventHandler(this.bDelete_Click);
    this.tree.AllowDrop = true;
    this.tree.AllowIndividualRowResize = false;
    this.tree.AllowRowResize = false;
    this.tree.AllowUserPinnedColumns = false;
    this.tree.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tree.AutoFitColumns = true;
    this.tree.Columns.Add(this.column);
    this.tree.DisableHeaderContextMenu = true;
    this.tree.Editors.Add(this.editSeparator);
    this.tree.ImageList = (ImageList) null;
    this.tree.IndentWidth = 0;
    this.tree.Location = new Point(0, 0);
    this.tree.MainColumn = this.column;
    this.tree.Margin = new Padding(0);
    this.tree.MinRowHeight = 21;
    this.tree.Name = "tree";
    this.tree.RowHeight = 21;
    this.tree.RowSelectedUnfocusedStyle.BackColor = SystemColors.Highlight;
    this.tree.RowSelectedUnfocusedStyle.ForeColor = SystemColors.HighlightText;
    this.tree.ShowColumnHeaders = false;
    this.tree.ShowRootRow = false;
    this.tree.Size = new Size(261, 490);
    this.tree.SuppressErrorMessages = true;
    this.tree.TabIndex = 25;
    this.tree.GetCellData += new GetCellDataHandler(this.tree_GetCellData);
    this.tree.SetCellValue += new SetCellValueHandler(this.tree_SetCellValue);
    this.column.Caption = "Части";
    this.column.CellEditor = this.editSeparator;
    this.column.CellStyle.BackColor = SystemColors.InactiveCaptionText;
    this.column.HeaderStyle.HorzAlignment = StringAlignment.Near;
    this.column.MinWidth = 100;
    this.column.Movable = false;
    this.column.Name = "column";
    this.column.ToolTip = "";
    this.column.Width = 257;
    this.editSeparator.CellAlignment = ContentAlignment.MiddleCenter;
    this.editSeparator.Control = (Control) null;
    this.bAddSection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bAddSection.Image = (Image) Resources.AddStandart;
    this.bAddSection.ImeMode = ImeMode.NoControl;
    this.bAddSection.Location = new Point(264, 9);
    this.bAddSection.Name = "bAddSection";
    this.bAddSection.Size = new Size(23, 23);
    this.bAddSection.TabIndex = 26;
    this.bAddSection.TextAlign = ContentAlignment.TopCenter;
    this.bAddSection.UseVisualStyleBackColor = true;
    this.bAddSection.Click += new EventHandler(this.bAdd_Click);
    this.bRemoveSection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bRemoveSection.Image = (Image) Resources.DeleteStandart;
    this.bRemoveSection.ImeMode = ImeMode.NoControl;
    this.bRemoveSection.Location = new Point(264, 38);
    this.bRemoveSection.Name = "bRemoveSection";
    this.bRemoveSection.Size = new Size(23, 23);
    this.bRemoveSection.TabIndex = 27;
    this.bRemoveSection.TextAlign = ContentAlignment.TopCenter;
    this.bRemoveSection.UseVisualStyleBackColor = true;
    this.bRemoveSection.Click += new EventHandler(this.bDelete_Click);
    this.bSectionDown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSectionDown.Image = (Image) Resources.arrow_down_blueStandart;
    this.bSectionDown.ImeMode = ImeMode.NoControl;
    this.bSectionDown.Location = new Point(264, 96 /*0x60*/);
    this.bSectionDown.Name = "bSectionDown";
    this.bSectionDown.Size = new Size(23, 23);
    this.bSectionDown.TabIndex = 28;
    this.bSectionDown.TextAlign = ContentAlignment.TopCenter;
    this.bSectionDown.UseVisualStyleBackColor = true;
    this.bSectionDown.Click += new EventHandler(this.bMoveDown_Click);
    this.bSectionUp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.bSectionUp.Image = (Image) Resources.ArrowUp;
    this.bSectionUp.ImeMode = ImeMode.NoControl;
    this.bSectionUp.Location = new Point(264, 67);
    this.bSectionUp.Name = "bSectionUp";
    this.bSectionUp.Size = new Size(23, 23);
    this.bSectionUp.TabIndex = 29;
    this.bSectionUp.TextAlign = ContentAlignment.TopCenter;
    this.bSectionUp.UseVisualStyleBackColor = true;
    this.bSectionUp.Click += new EventHandler(this.bMoveUp_Click);
    this.Controls.Add((Control) this.bAddSection);
    this.Controls.Add((Control) this.bRemoveSection);
    this.Controls.Add((Control) this.bSectionDown);
    this.Controls.Add((Control) this.bSectionUp);
    this.Controls.Add((Control) this.tree);
    this.Controls.Add((Control) this.bDelete);
    this.Controls.Add((Control) this.bAdd);
    this.Name = nameof (SetupAdditionalChaptersPanel);
    this.Size = new Size(290, 495);
    this.tree.EndInit();
    this.ResumeLayout(false);
  }

  public List<AdditionalChapterSettings> AdditionalChapters { get; set; }

  /// <summary> Обновление визуальных контролов </summary>
  protected void UpdateControls()
  {
    if (this._isUpdating)
      return;
    this._isUpdating = true;
    try
    {
      if (this.Parent is IStructualControlSupport parent)
        parent.UpdateControls();
      if (this._editModeToolTip != null)
      {
        if (this.ReadOnly)
        {
          if (this._editModeToolTip.Active)
          {
            this._editModeToolTip.Active = false;
            this._readModeToolTip.Active = true;
          }
        }
        else if (this._readModeToolTip.Active)
        {
          this._readModeToolTip.Active = false;
          this._editModeToolTip.Active = true;
        }
      }
      if (this.ReadOnly)
      {
        this.bAdd.Enabled = this.bAddSection.Enabled = false;
        this.bDelete.Enabled = this.bRemoveSection.Enabled = this.SelectedAdditionalChapter != null;
      }
      else
      {
        this.bAdd.Enabled = this.bAddSection.Enabled = true;
        this.bDelete.Enabled = this.bRemoveSection.Enabled = true;
        Row selectedRow = this.tree.SelectedRow;
        int num = selectedRow != null ? selectedRow.RowIndex : -1;
        this.bSectionUp.Enabled = this.SelectedAdditionalChapter != null && num > 1;
        this.bSectionDown.Enabled = this.SelectedAdditionalChapter != null && num < this.tree.NumVisibleRows;
      }
      this.tree.Enabled = !this.ReadOnly;
    }
    finally
    {
      this._isUpdating = false;
    }
  }

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [DefaultValue(false)]
  public bool ReadOnly { get; set; }

  public bool ControlsAreUpdating { get; private set; }

  public AdditionalChapterSettings SelectedAdditionalChapter
  {
    get => this.tree.SelectedItem as AdditionalChapterSettings;
  }

  private void RefreshGrid()
  {
    if (this.AdditionalChapters != null)
    {
      this.AdditionalChapters = this.AdditionalChapters.OrderBy<AdditionalChapterSettings, long>((Func<AdditionalChapterSettings, long>) (el => el.SortIndex)).ToList<AdditionalChapterSettings>();
      this.tree.DataSource = (object) this.AdditionalChapters;
      if (this.tree.SelectedRow != null)
        return;
      this.tree.SelectedRow = this.tree.GetRow(1);
    }
    else
      this.tree.DataSource = (object) null;
  }

  private bool BeforeUpDownEdit() => !this.ReadOnly && !this.ControlsAreUpdating;

  private void AfterUpDownEdit()
  {
    this.Changed = true;
    this.UpdateControls();
  }

  private void bAdd_Click(object sender, EventArgs e)
  {
    if (!(this.AdditionalChapters != null & this.BeforeUpDownEdit()))
      return;
    AdditionalChapterSettings additionalChapterSettings1 = new AdditionalChapterSettings(Guid.NewGuid(), -1L, "", 0L);
    AdditionalChapterSettings additionalChapterSettings2 = this.AdditionalChapters.LastOrDefault<AdditionalChapterSettings>();
    this.AdditionalChapters.Add(additionalChapterSettings1);
    additionalChapterSettings1.SortIndex = additionalChapterSettings2 != null ? additionalChapterSettings2.SortIndex + 1L : 0L;
    this.RefreshGrid();
    this.AfterUpDownEdit();
    Row row = this.tree.FindRow((object) additionalChapterSettings1);
    if (row == null)
      return;
    this.tree.SelectedRow = row;
    this.tree.FocusRow = row;
    this.tree.EditFirstCellInFocusRow();
  }

  private void bDelete_Click(object sender, EventArgs e)
  {
    if (this.AdditionalChapters == null || !this.BeforeUpDownEdit() || !(this.tree.SelectedItem is AdditionalChapterSettings))
      return;
    int index = this.tree.SelectedRow.RowIndex;
    if (index > 1)
      --index;
    else if (index > this.tree.NumVisibleRows - 1)
      index = -1;
    this.AdditionalChapters.Remove((AdditionalChapterSettings) this.tree.SelectedItem);
    this.RefreshGrid();
    if (index != -1)
      this.tree.SelectedRow = this.tree.GetRow(index);
    this.AfterUpDownEdit();
  }

  private void bMoveUp_Click(object sender, EventArgs e)
  {
    if (this.AdditionalChapters == null || !this.BeforeUpDownEdit() || !(this.tree.SelectedItem is AdditionalChapterSettings selectedItem))
      return;
    int index = this.tree.SelectedRow.RowIndex - 1;
    if (this.tree.GetRow(index)?.Item is AdditionalChapterSettings additionalChapterSettings)
    {
      long sortIndex = additionalChapterSettings.SortIndex;
      additionalChapterSettings.SortIndex = selectedItem.SortIndex;
      selectedItem.SortIndex = sortIndex;
      this.RefreshGrid();
      if (index != -1)
        this.tree.SelectedRow = this.tree.GetRow(index);
    }
    this.AfterUpDownEdit();
  }

  private void bMoveDown_Click(object sender, EventArgs e)
  {
    if (this.AdditionalChapters == null || !this.BeforeUpDownEdit() || !(this.tree.SelectedItem is AdditionalChapterSettings selectedItem))
      return;
    int index = this.tree.SelectedRow.RowIndex + 1;
    if (this.tree.GetRow(index)?.Item is AdditionalChapterSettings additionalChapterSettings)
    {
      long sortIndex = additionalChapterSettings.SortIndex;
      additionalChapterSettings.SortIndex = selectedItem.SortIndex;
      selectedItem.SortIndex = sortIndex;
      this.RefreshGrid();
      this.tree.SelectedRow = this.tree.GetRow(index);
    }
    this.AfterUpDownEdit();
  }

  private void tree_SetCellValue(object sender, SetCellValueEventArgs e)
  {
    if (!this.BeforeUpDownEdit())
    {
      e.Cancel = true;
    }
    else
    {
      if (e.Column != this.column)
        return;
      ((AdditionalChapterSettings) e.Row.Item).Caption = e.NewValue.ToString();
      this.AfterUpDownEdit();
      this.RefreshGrid();
    }
  }

  private void tree_GetCellData(object sender, GetCellDataEventArgs e)
  {
    if (e.Column != this.column)
      return;
    StyleDelta delta = new StyleDelta();
    e.CellData.OddStyle = new Style(e.Row.Tree.RowOddStyle, delta);
    e.CellData.EvenStyle = new Style(e.Row.Tree.RowEvenStyle, delta);
    if (e.Row.Item == null)
      return;
    e.CellData.Value = (object) ((AdditionalChapterSettings) e.Row.Item).Caption;
    e.CellData.Editor.Control = (Control) new TextBox();
  }

  private void tree_SelectionChanged(object sender, EventArgs e) => this.UpdateControls();

  public void LoadData()
  {
    if (this.IsDesignerHosted())
      return;
    this._originalAdditionalChapters = AVSCommonPropertiesSchema.LoadAdditionalChaptersSettingsFromDB();
    List<AdditionalChapterSettings> source = new List<AdditionalChapterSettings>(this._originalAdditionalChapters.Count);
    foreach (AdditionalChapterSettings additionalChapter in this._originalAdditionalChapters)
      source.Add(additionalChapter.Clone());
    if (source.Count > 1 && source.All<AdditionalChapterSettings>((Func<AdditionalChapterSettings, bool>) (ac => ac.SortIndex == 0L)))
    {
      long num = 0;
      foreach (AdditionalChapterSettings additionalChapterSettings in source)
      {
        additionalChapterSettings.SortIndex = num;
        ++num;
      }
    }
    this.AdditionalChapters = source.OrderBy<AdditionalChapterSettings, long>((Func<AdditionalChapterSettings, long>) (ac => ac.SortIndex)).ToList<AdditionalChapterSettings>();
    this.Changed = false;
    if (!this.ReadOnly)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (!sessionKeeper.Session.IsAdmin)
          this.ReadOnly = true;
      }
    }
    this.UpdateControls();
    this.RefreshGrid();
  }

  public bool Changed { get; set; }

  /// <summary> Сохранение изменений </summary>
  public void SaveChanges()
  {
    if (this.AdditionalChapters == null)
      return;
    List<DBObjectsExtendedEventArgs> changedObjects = new List<DBObjectsExtendedEventArgs>();
    List<long> newObjects = new List<long>();
    List<long> deletedObjects = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(AvsIDCache.ObjType_SpecificationChapter);
      long num = -1;
      foreach (AdditionalChapterSettings additionalChapter in this.AdditionalChapters)
      {
        AdditionalChapterSettings chapter = additionalChapter;
        ++num;
        if (chapter.SortIndex < num)
          chapter.SortIndex = num;
        else
          num = chapter.SortIndex;
        AdditionalChapterSettings additionalChapterSettings = this._originalAdditionalChapters.Find((Predicate<AdditionalChapterSettings>) (x => x.ChapterGuid == chapter.ChapterGuid));
        if (additionalChapterSettings != null)
        {
          if (additionalChapterSettings.Caption != chapter.Caption || additionalChapterSettings.SortIndex != chapter.SortIndex)
          {
            AVSCommonPropertiesSchema.SaveChapterSettingsToDbObject(sessionKeeper.Session, chapter, objectCollection);
            changedObjects.Add(new DBObjectsExtendedEventArgs("ObjectsChanged", chapter.ChapterID, AvsIDCache.ObjType_SpecificationChapter, new AttributeValues[2]
            {
              new AttributeValues(AvsIDCache.Attr_Name, (object) additionalChapterSettings.Caption),
              new AttributeValues(AvsIDCache.Attr_PartNum, (object) additionalChapterSettings.SortIndex)
            }, new AttributeValues[2]
            {
              new AttributeValues(AvsIDCache.Attr_Name, (object) chapter.Caption),
              new AttributeValues(AvsIDCache.Attr_PartNum, (object) chapter.SortIndex)
            }));
          }
          this._originalAdditionalChapters.Remove(additionalChapterSettings);
        }
        else
        {
          AVSCommonPropertiesSchema.SaveChapterSettingsToDbObject(sessionKeeper.Session, chapter, objectCollection);
          newObjects.Add(chapter.ChapterID);
        }
      }
      foreach (AdditionalChapterSettings additionalChapter in this._originalAdditionalChapters)
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(additionalChapter.ChapterGuid, false);
        if (dbObject != null)
        {
          dbObject.Delete(0L);
          deletedObjects.Add(additionalChapter.ChapterID);
        }
      }
    }
    this.Changed = false;
    this.UpdateControls();
    this.RefreshGrid();
    this.FireEventsForNotificationService(deletedObjects, newObjects, changedObjects);
  }

  private void FireEventsForNotificationService(
    List<long> deletedObjects,
    List<long> newObjects,
    List<DBObjectsExtendedEventArgs> changedObjects)
  {
    INotificationService service = (INotificationService) ServicesManager.GetService(typeof (INotificationService));
    if (service == null)
      return;
    if (deletedObjects.Count > 0)
    {
      List<int> objectTypeIDs = new List<int>(deletedObjects.Count);
      for (int index = 0; index < deletedObjects.Count; ++index)
        objectTypeIDs.Add(AvsIDCache.ObjType_SpecificationChapter);
      service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsRemoved", (IList<long>) deletedObjects, (IList<int>) objectTypeIDs));
    }
    if (newObjects.Count > 0)
    {
      List<int> objectTypeIDs = new List<int>(newObjects.Count);
      for (int index = 0; index < newObjects.Count; ++index)
        objectTypeIDs.Add(AvsIDCache.ObjType_SpecificationChapter);
      service.FireEvent((object) this, (NotificationEventArgs) new DBObjectsEventArgs("ObjectsCreated", (IList<long>) newObjects, (IList<int>) objectTypeIDs));
    }
    foreach (DBObjectsExtendedEventArgs changedObject in changedObjects)
      service.FireEvent((object) this, (NotificationEventArgs) changedObject);
  }
}
