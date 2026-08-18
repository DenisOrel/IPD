
// Type: Intermech.Search.ObjectListFilters.ChildrenViewObjectListFiltersComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Search.Utilities;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Search.ObjectListFilters;

public sealed class ChildrenViewObjectListFiltersComponent : Component
{
  private IObjectListFiltersClientService _objectListFiltersClientService;
  private INavGraphicsCache _navGraphicsCache;
  private ICurrentUserAndRole _currentUserAndRole;
  private ICategoryTypeIconService _categoryTypeIconService;
  private INamedImageList _namedImageList;
  private ChildrenView _childrenView;
  private bool _isAttached = true;
  private bool _isEnabled = true;
  private int _parentObjectTypeID = -1;
  private ObjectListFilter _selectedFilter = ObjectListFilter.DefaultFilter;
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ChildrenViewObjectListFiltersComponent() => this.InitializeComponent();

  public ChildrenViewObjectListFiltersComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ICategoryTypeIconService CategoryTypeIconService
  {
    get => this._categoryTypeIconService;
    set
    {
      if (this._categoryTypeIconService == value)
        return;
      this._categoryTypeIconService = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ICurrentUserAndRole CurrentUserAndRole
  {
    get => this._currentUserAndRole;
    set
    {
      if (this._currentUserAndRole == value)
        return;
      this._currentUserAndRole = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public IObjectListFiltersClientService ObjectListFiltersClientService
  {
    get => this._objectListFiltersClientService;
    set
    {
      if (this._objectListFiltersClientService == value)
        return;
      this._objectListFiltersClientService = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INamedImageList NamedImageList
  {
    get => this._namedImageList;
    set
    {
      if (this._namedImageList == value)
        return;
      this._namedImageList = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public INavGraphicsCache NavGraphicsCache
  {
    get => this._navGraphicsCache;
    set
    {
      if (this._navGraphicsCache == value)
        return;
      this._navGraphicsCache = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ChildrenView ChildrenView
  {
    get => this._childrenView;
    set
    {
      if (this._childrenView == value)
        return;
      if (this._childrenView != null)
      {
        this._childrenView.FiltersComboBoxItem.ComboBox.SelectedIndexChanged -= new EventHandler(this.FiltersComboBox_SelectedIndexChanged);
        this._childrenView.FiltersComboBoxItem.ComboBox.DrawItem -= new DrawItemEventHandler(this.FiltersComboBox_DrawItem);
        this._childrenView.RefreshFiltersDropDownMenuItem.Click -= new EventHandler(this.RefreshFiltersDropDownMenuItem_Click);
        this._childrenView.CreateCommonFilterMenuButtonItem.Click -= new EventHandler(this.CreateCommonFilterMenuButtonItem_Click);
        this._childrenView.CreatePersonalFilterMenuButtonItem.Click -= new EventHandler(this.CreatePersonalFilterMenuButtonItem_Click);
        this._childrenView.FilterCardMenuButtonItem.Click -= new EventHandler(this.FilterCardMenuButtonItem_Click);
        this._childrenView.RemoveFilterMenuButtonItem.Click -= new EventHandler(this.RemoveFilterMenuButtonItem_Click);
      }
      this._childrenView = value;
      if (this._childrenView != null)
      {
        this._childrenView.FiltersComboBoxItem.ComboBox.SelectedIndexChanged += new EventHandler(this.FiltersComboBox_SelectedIndexChanged);
        this._childrenView.FiltersComboBoxItem.ComboBox.DrawItem += new DrawItemEventHandler(this.FiltersComboBox_DrawItem);
        this._childrenView.RefreshFiltersDropDownMenuItem.Click += new EventHandler(this.RefreshFiltersDropDownMenuItem_Click);
        this._childrenView.CreateCommonFilterMenuButtonItem.Click += new EventHandler(this.CreateCommonFilterMenuButtonItem_Click);
        this._childrenView.CreatePersonalFilterMenuButtonItem.Click += new EventHandler(this.CreatePersonalFilterMenuButtonItem_Click);
        this._childrenView.FilterCardMenuButtonItem.Click += new EventHandler(this.FilterCardMenuButtonItem_Click);
        this._childrenView.RemoveFilterMenuButtonItem.Click += new EventHandler(this.RemoveFilterMenuButtonItem_Click);
      }
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsAttached
  {
    get => this._isAttached;
    set
    {
      if (this._isAttached == value)
        return;
      this._isAttached = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public bool IsEnabled
  {
    get => this._isEnabled;
    set
    {
      if (this._isEnabled == value)
        return;
      this._isEnabled = value;
      this.UpdateControls();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int ParentObjectTypeID
  {
    get => this._parentObjectTypeID;
    set
    {
      if (this._parentObjectTypeID == value)
        return;
      this._parentObjectTypeID = value;
      this.Initialize();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ObjectListFilter SelectedFilter => this._selectedFilter;

  public void SelectFilter(Guid filterGuid)
  {
    this.SelectFilter(this.ChildrenView.FiltersComboBoxItem.Items.Cast<ObjectListFilter>().FirstOrDefault<ObjectListFilter>((Func<ObjectListFilter, bool>) (o => o.Guid == filterGuid)) ?? ObjectListFilter.DefaultFilter);
  }

  private void FiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.CheckReady();
    if (!this.IsAttached)
      objectListFilter = ObjectListFilter.DefaultFilter;
    else if (!(this.ChildrenView.FiltersComboBoxItem.ComboBox.SelectedItem is ObjectListFilter objectListFilter))
      objectListFilter = ObjectListFilter.DefaultFilter;
    this._selectedFilter = objectListFilter;
    UISettings.SelectedChildrenViewObjectFilter = new Guid?(this.SelectedFilter.Guid);
    this.UpdateControls();
    this.ChildrenView.ReloadItems();
  }

  private void FiltersComboBox_DrawItem(object sender, DrawItemEventArgs e)
  {
    this.CheckReady();
    ObjectListFilter objectListFilter = e.Index >= 0 ? this.ChildrenView.FiltersComboBoxItem.ComboBox.Items[e.Index] as ObjectListFilter : (ObjectListFilter) null;
    Brush brush1 = (Brush) null;
    Brush brush2 = (Brush) null;
    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
    {
      if (this.NavGraphicsCache != null)
        brush1 = this.NavGraphicsCache.GetNavGradientBrush(this.NavGraphicsCache.CurrentColorsScheme.ComboBoxBkStartColor, this.NavGraphicsCache.CurrentColorsScheme.ComboBoxBkEndColor, this.NavGraphicsCache.CurrentColorsScheme.ComboBoxGradientMode, e.Bounds).Brush;
      brush2 = SystemBrushes.HighlightText;
    }
    if (brush1 == null)
    {
      brush1 = SystemBrushes.Window;
      brush2 = objectListFilter == null || !objectListFilter.IsSystem ? SystemBrushes.WindowText : Brushes.DarkBlue;
    }
    e.Graphics.FillRectangle(brush1, e.Bounds);
    if (objectListFilter != null)
      e.Graphics.DrawString(objectListFilter.Name, e.Font, brush2, (RectangleF) e.Bounds, StringFormat.GenericDefault);
    if (!this.ChildrenView.FiltersComboBoxItem.ComboBox.Focused)
      return;
    e.DrawFocusRectangle();
  }

  private void RefreshFiltersDropDownMenuItem_Click(object sender, EventArgs e)
  {
    this.CheckReady();
    this.ObjectListFiltersClientService.RefreshCache();
    this.FillComboBox();
  }

  private void CreateCommonFilterMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.CheckReady();
    this.CreateFilter(ObjectListFilterType.Common);
  }

  private void CreatePersonalFilterMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.CheckReady();
    this.CreateFilter(ObjectListFilterType.Personal);
  }

  private void FilterCardMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.CheckReady();
    if (this.ShowFilterCard(this.SelectedFilter.ID) != DialogResult.OK)
      return;
    this.FillComboBox();
  }

  private void RemoveFilterMenuButtonItem_Click(object sender, EventArgs e)
  {
    this.CheckReady();
    if (MessageBox.Show("Выбранный фильтр списка объектов будет удален, продолжить?", "Удаление фильтра", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
      return;
    this.ObjectListFiltersClientService.RemoveFilter(this.SelectedFilter.ID);
    this.FillComboBox();
  }

  private void Initialize()
  {
    if (this.IsAttached)
    {
      if (!this.IsReady())
        return;
      this.ChildrenView.FiltersComboBoxItem.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
      this.ChildrenView.CreateCommonFilterMenuButtonItem.Image = this.CategoryTypeIconService.ImageList.Images[this.CategoryTypeIconService.IndexOf(4, MetaDataHelper.GetObjectTypeID("cad00122-306c-11d8-b4e9-00304f19f545"))];
      this.ChildrenView.CreatePersonalFilterMenuButtonItem.Image = this.CategoryTypeIconService.ImageList.Images[this.CategoryTypeIconService.IndexOf(4, MetaDataHelper.GetObjectTypeID("cad00123-306c-11d8-b4e9-00304f19f545"))];
      int index = this.NamedImageList.ImageIndex("imgCard");
      if (index > 0)
        this.ChildrenView.FilterCardMenuButtonItem.Image = this.NamedImageList.ImageList.Images[index];
      this.FillComboBox();
      Guid? selectedFilterGuid = new Guid?();
      if (UISettings.SaveSelectedChildrenViewObjectFilter)
        selectedFilterGuid = UISettings.SelectedChildrenViewObjectFilter;
      if (!selectedFilterGuid.HasValue)
        selectedFilterGuid = this.GetObjectListFilterGuidFromCurrentAutosotRule();
      if (!selectedFilterGuid.HasValue)
        selectedFilterGuid = new Guid?(ObjectListFilter.DefaultFilter.Guid);
      this.SelectFilter(this.ChildrenView.FiltersComboBoxItem.ComboBox.Items.Cast<ObjectListFilter>().FirstOrDefault<ObjectListFilter>((Func<ObjectListFilter, bool>) (o =>
      {
        Guid guid = o.Guid;
        Guid? nullable = selectedFilterGuid;
        return nullable.HasValue && guid == nullable.GetValueOrDefault();
      })));
    }
    else
      this.UpdateControls();
  }

  private bool IsReady()
  {
    return this.CategoryTypeIconService != null && this.CurrentUserAndRole != null && this.ObjectListFiltersClientService != null && this.NamedImageList != null && this.NavGraphicsCache != null && this.ChildrenView != null;
  }

  private void FillComboBox()
  {
    this.ChildrenView.FiltersComboBoxItem.ComboBox.SelectedIndexChanged -= new EventHandler(this.FiltersComboBox_SelectedIndexChanged);
    this.ChildrenView.FiltersComboBoxItem.ComboBox.BeginUpdate();
    try
    {
      ObjectListFilter selectedFilter = this.SelectedFilter;
      this.ChildrenView.FiltersComboBoxItem.ComboBox.Items.Clear();
      this.ChildrenView.FiltersComboBoxItem.ComboBox.DisplayMember = "Name";
      this.ChildrenView.FiltersComboBoxItem.ComboBox.ValueMember = "ID";
      this.ChildrenView.FiltersComboBoxItem.ComboBox.Items.AddRange(ObjectTypeHelper.IsUnknownObjectTypeID(this.ParentObjectTypeID) ? (object[]) this.ObjectListFiltersClientService.GetAllFilters() : (object[]) this.ObjectListFiltersClientService.GetFiltersForObjectType(this.ParentObjectTypeID));
      this.SelectFilter(selectedFilter);
    }
    finally
    {
      this.ChildrenView.FiltersComboBoxItem.ComboBox.EndUpdate();
      this.ChildrenView.FiltersComboBoxItem.ComboBox.SelectedIndexChanged += new EventHandler(this.FiltersComboBox_SelectedIndexChanged);
    }
    this.UpdateControls();
  }

  private void SelectFilter(ObjectListFilter objectListFilter)
  {
    this.ChildrenView.FiltersComboBoxItem.ComboBox.SelectedItem = (object) (objectListFilter ?? ObjectListFilter.DefaultFilter);
  }

  private Guid? GetObjectListFilterGuidFromCurrentAutosotRule()
  {
    if (!ObjectTypeHelper.IsUnknownObjectTypeID(this.ParentObjectTypeID) && this.CurrentUserAndRole != null && this.CurrentUserAndRole.Rule != null)
    {
      ParentObjectType parentObjectType = this.CurrentUserAndRole.Rule.ParentObjectTypes.FirstOrDefault<ParentObjectType>((Func<ParentObjectType, bool>) (o => o.ObjectTypeID == this.ParentObjectTypeID));
      if (parentObjectType != null)
        return parentObjectType.DefaultObjectListFilter;
    }
    return new Guid?();
  }

  private void UpdateControls()
  {
    if (this.ChildrenView == null)
      return;
    this.ChildrenView.FiltersComboBoxItem.Enabled = this.IsEnabled;
    this.ChildrenView.FiltersComboBoxItem.Visible = this.IsAttached;
    this.ChildrenView.RefreshFiltersDropDownMenuItem.Enabled = this.IsEnabled;
    this.ChildrenView.RefreshFiltersDropDownMenuItem.Visible = this.IsAttached;
    this.ChildrenView.FilterCardMenuButtonItem.Enabled = this.ChildrenView.RemoveFilterMenuButtonItem.Enabled = !this.SelectedFilter.IsSystem;
  }

  private void CheckReady()
  {
    if (!this.IsReady())
      throw new Exception("Component not initialized.");
  }

  private void CreateFilter(ObjectListFilterType type)
  {
    ObjectListFilter newFilter = this.ObjectListFiltersClientService.CreateNewFilter(type);
    if (newFilter == null)
      return;
    int num = (int) this.ShowFilterCard(newFilter.ID);
    this.FillComboBox();
    this.SelectFilter(newFilter);
  }

  private DialogResult ShowFilterCard(long objectVersionID)
  {
    return PropertiesWindow.Execute(string.Empty, string.Empty, objectVersionID, false, "SelectionViewObject");
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
  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();
}
