
// Type: Intermech.Search.UI.PropertyGrid.SimplePropertyGrid
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls;
using Infralution.Controls.VirtualTree;
using Intermech.Bars;
using Intermech.Client.Core.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Search.UI.PropertyGrid;

public class SimplePropertyGrid : UserControl
{
  private object _selectedObject;
  private PropertySort _propertySort = PropertySort.CategorizedAlphabetical;
  private List<ButtonItem> _propertyTabsButtons = new List<ButtonItem>();
  private System.Type _selectedPropertyTabType;
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private PropertyGridTreeView _tree;
  private Column _nameColumn;
  private Column _valueColumn;
  private Intermech.Bars.ToolBar _toolBar;
  private ButtonItem _setCategorizedButtonItem;
  private ButtonItem _setAlphabeticalButtonItem;
  private FlowLayoutPanel flowLayoutPanel1;
  private Label _gridItemCaptionLabel;
  private Label _gridItemDescriptionLabel;
  private SplitContainer splitContainer1;

  public SimplePropertyGrid()
  {
    this.InitializeComponent();
    this.PropertyTabs = new PropertyTabCollection(this);
    this.PropertyTabs.TabTypeAdded += new EventHandler(this.PropertyTabs_TabTypeAdded);
    this.SetTreeColumnsSize();
    this._tree.TopRowChanged += new EventHandler(this.Tree_TopRowChanged);
    ObjectRowBinding objectRowBinding = new ObjectRowBinding(typeof (CategoryGridItem));
    objectRowBinding.ChildPolicy = RowChildPolicy.AutoExpand;
    objectRowBinding.ChildProperty = "Children";
    ObjectCellBinding objectCellBinding1 = new ObjectCellBinding(this._nameColumn, "Label");
    objectCellBinding1.Style.BackColor = Color.LightGray;
    objectCellBinding1.Style.Font = new Font(this._tree.Font, FontStyle.Bold);
    objectRowBinding.CellBindings.Add((CellBinding) objectCellBinding1);
    ObjectCellBinding objectCellBinding2 = new ObjectCellBinding(this._valueColumn, "Value");
    objectCellBinding2.Style.BackColor = Color.LightGray;
    objectCellBinding2.Style.Font = new Font(this._tree.Font, FontStyle.Bold);
    objectRowBinding.CellBindings.Add((CellBinding) objectCellBinding2);
    this._tree.RowBindings.Add((RowBinding) objectRowBinding);
    SimplePropertyGrid.PropertyDescriptorGridItemRowBinding gridItemRowBinding = new SimplePropertyGrid.PropertyDescriptorGridItemRowBinding();
    gridItemRowBinding.ChildPolicy = RowChildPolicy.Normal;
    gridItemRowBinding.ChildProperty = "Children";
    ObjectCellBinding objectCellBinding3 = new ObjectCellBinding(this._nameColumn, "Label");
    gridItemRowBinding.CellBindings.Add((CellBinding) objectCellBinding3);
    ObjectCellBinding objectCellBinding4 = new ObjectCellBinding(this._valueColumn, "Value");
    gridItemRowBinding.CellBindings.Add((CellBinding) objectCellBinding4);
    this._tree.RowBindings.Add((RowBinding) gridItemRowBinding);
  }

  public event EventHandler PropertyTabChanged;

  public event EventHandler PropertySortChanged;

  public event EventHandler SelectedGridItemChanged;

  public event EventHandler<GridItemEventArgs> GridItemExpand;

  public event EventHandler<GridItemEventArgs> GridItemCollapse;

  public event EventHandler TopGridItemChanged;

  public PropertySort PropertySort
  {
    get => this._propertySort;
    set
    {
      if (value == this._propertySort)
        return;
      this._propertySort = value;
      this.RebuildView();
      this.OnPropertySortChanged();
    }
  }

  public object SelectedObject
  {
    get => this._selectedObject;
    set
    {
      if (object.Equals(this._selectedObject, value) || value == null)
        return;
      this._selectedObject = value;
      this.RebuildView();
    }
  }

  public PropertyTabCollection PropertyTabs { get; private set; }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public System.Type SelectedPropertyTabType
  {
    get => this._selectedPropertyTabType;
    set
    {
      if (!(this._selectedPropertyTabType != value))
        return;
      this._selectedPropertyTabType = value;
      this.RebuildView();
      this.OnPropertyTabChanged();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public GridItem SelectedGridItem => this._tree.SelectedItem as GridItem;

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int SelectedGridItemIndex
  {
    get
    {
      Row selectedRow = this._tree.SelectedRow;
      return selectedRow == null ? -1 : selectedRow.RowIndex;
    }
    set
    {
      this._tree.SelectedRow = value >= 0 ? this._tree.GetRow(value) : throw new ArgumentException();
    }
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public int TopGridItemIndex
  {
    get => this._tree.TopRowIndex;
    set => this._tree.TopRowIndex = value;
  }

  public void ExpandGridItem(int gridItemIndex)
  {
    Row row = gridItemIndex >= 0 ? this._tree.GetRow(gridItemIndex) : throw new ArgumentException();
    if (row == null)
      return;
    row.Expanded = true;
  }

  public void CollapseGridItem(int gridItemIndex)
  {
    Row row = gridItemIndex >= 0 ? this._tree.GetRow(gridItemIndex) : throw new ArgumentException();
    if (row == null || row.ChildItems.Count <= 0)
      return;
    row.Expanded = false;
  }

  private void Tree_TopRowChanged(object sender, EventArgs e)
  {
    EventHandler topGridItemChanged = this.TopGridItemChanged;
    if (topGridItemChanged == null)
      return;
    topGridItemChanged((object) this, new EventArgs());
  }

  private void Tree_RowExpand(object sender, RowEventArgs e)
  {
    EventHandler<GridItemEventArgs> gridItemExpand = this.GridItemExpand;
    if (gridItemExpand == null || !(e.Row.Item is GridItem))
      return;
    gridItemExpand((object) this, new GridItemEventArgs(e.Row.Item as GridItem, e.Row.RowIndex));
  }

  private void Tree_RowCollapse(object sender, RowEventArgs e)
  {
    EventHandler<GridItemEventArgs> gridItemCollapse = this.GridItemCollapse;
    if (gridItemCollapse == null || !(e.Row.Item is GridItem))
      return;
    gridItemCollapse((object) this, new GridItemEventArgs(e.Row.Item as GridItem, e.Row.RowIndex));
  }

  private void PropertyTabs_TabTypeAdded(object sender, EventArgs e) => this.RebuildTabsButtons();

  private void Tree_SelectionChanged(object sender, EventArgs e)
  {
    GridItem selectedGridItem = this.SelectedGridItem;
    if (selectedGridItem != null)
    {
      this._gridItemCaptionLabel.Text = selectedGridItem.Label;
      this._gridItemDescriptionLabel.Text = selectedGridItem is PropertyDescriptorGridItem ? ((PropertyDescriptorGridItem) selectedGridItem).PropertyDescriptor.Description : (string) null;
    }
    else
    {
      this._gridItemCaptionLabel.Text = (string) null;
      this._gridItemDescriptionLabel.Text = (string) null;
    }
    this.OnSelectedGridItemChanged();
  }

  private void Tree_Resize(object sender, EventArgs e) => this.SetTreeColumnsSize();

  private void SetCategorizedButtonItem_Click(object sender, EventArgs e)
  {
    this.PropertySort = PropertySort.CategorizedAlphabetical;
  }

  private void SetAlphabeticalButtonItem_Click(object sender, EventArgs e)
  {
    this.PropertySort = PropertySort.Alphabetical;
  }

  private void ChangePropertyTabButtonItem_Click(object sender, EventArgs e)
  {
    this.SelectedPropertyTabType = ((ToolbarItemBase) sender).Tag.GetType();
  }

  private void OnPropertyTabChanged()
  {
    EventHandler propertyTabChanged = this.PropertyTabChanged;
    if (propertyTabChanged == null)
      return;
    propertyTabChanged((object) this, new EventArgs());
  }

  private void RebuildTabsButtons()
  {
    foreach (ButtonItem propertyTabsButton in this._propertyTabsButtons)
    {
      propertyTabsButton.Click -= new EventHandler(this.ChangePropertyTabButtonItem_Click);
      this._toolBar.Items.Remove((ToolbarItemBase) propertyTabsButton);
    }
    this._propertyTabsButtons.Clear();
    foreach (PropertyTab propertyTab in this.PropertyTabs)
    {
      ButtonItem buttonItem1 = new ButtonItem();
      buttonItem1.Image = (Image) propertyTab.Bitmap;
      buttonItem1.Text = propertyTab.TabName;
      buttonItem1.ToolTipText = propertyTab.TabName;
      buttonItem1.Tag = (object) propertyTab;
      ButtonItem buttonItem2 = buttonItem1;
      buttonItem2.Click += new EventHandler(this.ChangePropertyTabButtonItem_Click);
      this._propertyTabsButtons.Add(buttonItem2);
    }
    if (this._propertyTabsButtons.Count > 0)
      this._propertyTabsButtons[0].BeginGroup = true;
    this._toolBar.Items.AddRange((ToolbarItemBase[]) this._propertyTabsButtons.ToArray());
  }

  private void OnSelectedGridItemChanged()
  {
    EventHandler selectedGridItemChanged = this.SelectedGridItemChanged;
    if (selectedGridItemChanged == null)
      return;
    selectedGridItemChanged((object) this, new EventArgs());
  }

  private void OnPropertySortChanged()
  {
    EventHandler propertySortChanged = this.PropertySortChanged;
    if (propertySortChanged == null)
      return;
    propertySortChanged((object) this, new EventArgs());
  }

  private void SetTreeColumnsSize()
  {
    this._nameColumn.Width = this._tree.ClientRectangle.Width / 2;
    this._valueColumn.Width = this._tree.ClientRectangle.Width / 2;
  }

  private List<PropertyDescriptorGridItem> CreateGridItems(
    PropertyDescriptorCollection propertyDescriptors,
    object component)
  {
    return propertyDescriptors.Cast<System.ComponentModel.PropertyDescriptor>().Select<System.ComponentModel.PropertyDescriptor, PropertyDescriptorGridItem>((Func<System.ComponentModel.PropertyDescriptor, PropertyDescriptorGridItem>) (o => new PropertyDescriptorGridItem(o, component))).ToList<PropertyDescriptorGridItem>();
  }

  private PropertyDescriptorCollection GetPropertyDescriptorsForSelectedObject()
  {
    if (this._selectedObject == null)
      return new PropertyDescriptorCollection(new System.ComponentModel.PropertyDescriptor[0]);
    if (this.SelectedPropertyTabType != (System.Type) null)
      return this.PropertyTabs[this.SelectedPropertyTabType].GetProperties(this._selectedObject);
    if (!(Attribute.GetCustomAttribute((MemberInfo) this._selectedObject.GetType(), typeof (TypeConverterAttribute)) is TypeConverterAttribute customAttribute))
      return new PropertyDescriptorCollection(new System.ComponentModel.PropertyDescriptor[0]);
    TypeConverter instance = Activator.CreateInstance(System.Type.GetType(customAttribute.ConverterTypeName)) as TypeConverter;
    return instance.GetPropertiesSupported() ? instance.GetProperties(this._selectedObject) : new PropertyDescriptorCollection(new System.ComponentModel.PropertyDescriptor[0]);
  }

  private void RebuildView()
  {
    List<PropertyDescriptorGridItem> propertyDescriptorGridItems = this.CreateGridItems(this.GetPropertyDescriptorsForSelectedObject(), this._selectedObject);
    if (this._propertySort == PropertySort.Alphabetical)
    {
      this._tree.DataSource = (object) propertyDescriptorGridItems.OrderBy<PropertyDescriptorGridItem, string>((Func<PropertyDescriptorGridItem, string>) (o => o.Label)).ToList<PropertyDescriptorGridItem>();
      this._setAlphabeticalButtonItem.Checked = true;
      this._setCategorizedButtonItem.Checked = false;
    }
    else if (this._propertySort == PropertySort.Categorized)
    {
      this._tree.DataSource = (object) propertyDescriptorGridItems.Select<PropertyDescriptorGridItem, string>((Func<PropertyDescriptorGridItem, string>) (o => o.PropertyDescriptor.Category ?? "Прочие")).Distinct<string>().Select<string, CategoryGridItem>((Func<string, CategoryGridItem>) (o => new CategoryGridItem(o, propertyDescriptorGridItems.Where<PropertyDescriptorGridItem>((Func<PropertyDescriptorGridItem, bool>) (pd =>
      {
        if (pd.PropertyDescriptor.Category == o)
          return true;
        return pd.PropertyDescriptor.Category == null && o == "Прочие";
      })).ToArray<PropertyDescriptorGridItem>()))).ToList<CategoryGridItem>();
      this._setAlphabeticalButtonItem.Checked = false;
      this._setCategorizedButtonItem.Checked = false;
    }
    else if (this._propertySort == PropertySort.CategorizedAlphabetical)
    {
      propertyDescriptorGridItems = propertyDescriptorGridItems.OrderBy<PropertyDescriptorGridItem, string>((Func<PropertyDescriptorGridItem, string>) (o => o.Label)).ToList<PropertyDescriptorGridItem>();
      this._tree.DataSource = (object) propertyDescriptorGridItems.Select<PropertyDescriptorGridItem, string>((Func<PropertyDescriptorGridItem, string>) (o => o.PropertyDescriptor.Category ?? "Прочие")).Distinct<string>().OrderBy<string, string>((Func<string, string>) (o => o)).Select<string, CategoryGridItem>((Func<string, CategoryGridItem>) (o => new CategoryGridItem(o, propertyDescriptorGridItems.Where<PropertyDescriptorGridItem>((Func<PropertyDescriptorGridItem, bool>) (pd =>
      {
        if (pd.PropertyDescriptor.Category == o)
          return true;
        return pd.PropertyDescriptor.Category == null && o == "Прочие";
      })).ToArray<PropertyDescriptorGridItem>()))).ToList<CategoryGridItem>();
      this._setAlphabeticalButtonItem.Checked = false;
      this._setCategorizedButtonItem.Checked = true;
    }
    else
    {
      this._tree.DataSource = (object) propertyDescriptorGridItems;
      this._setAlphabeticalButtonItem.Checked = false;
      this._setCategorizedButtonItem.Checked = false;
    }
    if (!(this.SelectedPropertyTabType != (System.Type) null))
      return;
    foreach (ButtonItem propertyTabsButton in this._propertyTabsButtons)
      propertyTabsButton.Checked = propertyTabsButton.Tag.GetType() == this.SelectedPropertyTabType;
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
    this._nameColumn = new Column();
    this._valueColumn = new Column();
    this._toolBar = new Intermech.Bars.ToolBar();
    this.flowLayoutPanel1 = new FlowLayoutPanel();
    this._gridItemCaptionLabel = new Label();
    this._gridItemDescriptionLabel = new Label();
    this.splitContainer1 = new SplitContainer();
    this._tree = new PropertyGridTreeView();
    this._setCategorizedButtonItem = new ButtonItem();
    this._setAlphabeticalButtonItem = new ButtonItem();
    this.flowLayoutPanel1.SuspendLayout();
    this.splitContainer1.BeginInit();
    this.splitContainer1.Panel1.SuspendLayout();
    this.splitContainer1.Panel2.SuspendLayout();
    this.splitContainer1.SuspendLayout();
    this._tree.BeginInit();
    this.SuspendLayout();
    this._nameColumn.Caption = (string) null;
    this._nameColumn.Name = "_nameColumn";
    this._valueColumn.Caption = (string) null;
    this._valueColumn.Name = "_valueColumn";
    this._toolBar.FullMenus = true;
    this._toolBar.Guid = new Guid("0df1a6d6-2c22-4c34-baa7-2673a8599966");
    this._toolBar.Hidden = false;
    this._toolBar.Items.AddRange(new ToolbarItemBase[2]
    {
      (ToolbarItemBase) this._setCategorizedButtonItem,
      (ToolbarItemBase) this._setAlphabeticalButtonItem
    });
    this._toolBar.Location = new Point(0, 0);
    this._toolBar.Name = "_toolBar";
    this._toolBar.Size = new Size(434, 24);
    this._toolBar.TabIndex = 1;
    this._toolBar.Text = "";
    this.flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
    this.flowLayoutPanel1.Controls.Add((Control) this._gridItemCaptionLabel);
    this.flowLayoutPanel1.Controls.Add((Control) this._gridItemDescriptionLabel);
    this.flowLayoutPanel1.Dock = DockStyle.Fill;
    this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
    this.flowLayoutPanel1.Location = new Point(0, 0);
    this.flowLayoutPanel1.Name = "flowLayoutPanel1";
    this.flowLayoutPanel1.Size = new Size(422, 96 /*0x60*/);
    this.flowLayoutPanel1.TabIndex = 3;
    this._gridItemCaptionLabel.AutoSize = true;
    this._gridItemCaptionLabel.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 204);
    this._gridItemCaptionLabel.Location = new Point(3, 0);
    this._gridItemCaptionLabel.Name = "_gridItemCaptionLabel";
    this._gridItemCaptionLabel.Padding = new Padding(5);
    this._gridItemCaptionLabel.Size = new Size(10, 23);
    this._gridItemCaptionLabel.TabIndex = 0;
    this._gridItemDescriptionLabel.AutoSize = true;
    this._gridItemDescriptionLabel.Location = new Point(3, 23);
    this._gridItemDescriptionLabel.Name = "_gridItemDescriptionLabel";
    this._gridItemDescriptionLabel.Padding = new Padding(5);
    this._gridItemDescriptionLabel.Size = new Size(10, 23);
    this._gridItemDescriptionLabel.TabIndex = 1;
    this.splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.splitContainer1.Location = new Point(9, 30);
    this.splitContainer1.Name = "splitContainer1";
    this.splitContainer1.Orientation = Orientation.Horizontal;
    this.splitContainer1.Panel1.Controls.Add((Control) this._tree);
    this.splitContainer1.Panel2.Controls.Add((Control) this.flowLayoutPanel1);
    this.splitContainer1.Size = new Size(422, 424);
    this.splitContainer1.SplitterDistance = 324;
    this.splitContainer1.TabIndex = 6;
    this._tree.AllowDrop = true;
    this._tree.AllowMultiSelect = false;
    this._tree.Columns.Add(this._nameColumn);
    this._tree.Columns.Add(this._valueColumn);
    this._tree.Dock = DockStyle.Fill;
    this._tree.EnableRowCaching = false;
    this._tree.ImageList = (ImageList) null;
    this._tree.LineStyle = LineStyle.None;
    this._tree.Location = new Point(0, 0);
    this._tree.MainColumn = this._nameColumn;
    this._tree.Name = "_tree";
    this._tree.ShowColumnHeaders = false;
    this._tree.ShowRootRow = false;
    this._tree.Size = new Size(422, 324);
    this._tree.TabIndex = 0;
    this._tree.RowCollapse += new RowEventHandler(this.Tree_RowCollapse);
    this._tree.RowExpand += new RowEventHandler(this.Tree_RowExpand);
    this._tree.SelectionChanged += new EventHandler(this.Tree_SelectionChanged);
    this._tree.Resize += new EventHandler(this.Tree_Resize);
    this._setCategorizedButtonItem.CommandName = "_setCategorizedButtonItem";
    this._setCategorizedButtonItem.Image = (Image) Resources.data_sort_icon1;
    this._setCategorizedButtonItem.Text = "Группировать по категориям";
    this._setCategorizedButtonItem.ToolTipText = "Группировать по категориям";
    this._setCategorizedButtonItem.Click += new EventHandler(this.SetCategorizedButtonItem_Click);
    this._setAlphabeticalButtonItem.CommandName = "_setAlphabeticalButtonItem";
    this._setAlphabeticalButtonItem.Image = (Image) Resources.sort_columns_icon1;
    this._setAlphabeticalButtonItem.Text = "Сортировать";
    this._setAlphabeticalButtonItem.ToolTipText = "Сортировать";
    this._setAlphabeticalButtonItem.Click += new EventHandler(this.SetAlphabeticalButtonItem_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.splitContainer1);
    this.Controls.Add((Control) this._toolBar);
    this.Name = nameof (SimplePropertyGrid);
    this.Size = new Size(434, 457);
    this.flowLayoutPanel1.ResumeLayout(false);
    this.flowLayoutPanel1.PerformLayout();
    this.splitContainer1.Panel1.ResumeLayout(false);
    this.splitContainer1.Panel2.ResumeLayout(false);
    this.splitContainer1.EndInit();
    this.splitContainer1.ResumeLayout(false);
    this._tree.EndInit();
    this.ResumeLayout(false);
  }

  public sealed class PropertyDescriptorGridItemRowBinding : ObjectRowBinding
  {
    public PropertyDescriptorGridItemRowBinding()
      : base(typeof (PropertyDescriptorGridItem))
    {
    }

    public override void GetCellData(Row row, Column column, CellData cellData)
    {
      base.GetCellData(row, column, cellData);
      if (!(row.Item is GridItem))
        return;
      Color backColor = ((GridItem) row.Item).BackColor;
      if (!(backColor != Color.Empty))
        return;
      cellData.EvenStyle = new Style(cellData.EvenStyle, new StyleDelta()
      {
        BackColor = backColor
      });
      cellData.OddStyle = new Style(cellData.OddStyle, new StyleDelta()
      {
        BackColor = backColor
      });
    }
  }
}
