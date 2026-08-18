
// Type: Intermech.Search.Diff.ObjectDiffControl
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Search.Data.Repositories;
using Intermech.Search.UI.PropertyGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace Intermech.Search.Diff;

public class ObjectDiffControl : UserControl
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;
  private TableLayoutPanel tableLayoutPanel1;
  private SimplePropertyGrid _leftPropertyGrid;
  private SimplePropertyGrid _rightPropertyGrid;
  private Panel panel1;
  private Label label3;
  private Panel panel4;
  private Label label2;
  private Panel panel3;
  private Label label1;
  private Panel panel2;

  public ObjectDiffControl()
  {
    this.InitializeComponent();
    this._leftPropertyGrid.PropertyTabs.AddTabType(typeof (ObjectDiffControl.AllAttributesPropertyTab));
    this._leftPropertyGrid.PropertyTabs.AddTabType(typeof (ObjectDiffControl.AllNotSystemAttributesPropertyTab));
    this._leftPropertyGrid.SelectedPropertyTabType = typeof (ObjectDiffControl.AllAttributesPropertyTab);
    this._rightPropertyGrid.PropertyTabs.AddTabType(typeof (ObjectDiffControl.AllAttributesPropertyTab));
    this._rightPropertyGrid.PropertyTabs.AddTabType(typeof (ObjectDiffControl.AllNotSystemAttributesPropertyTab));
    this._rightPropertyGrid.SelectedPropertyTabType = typeof (ObjectDiffControl.AllAttributesPropertyTab);
  }

  public void SetObjectVersionIds(long leftObjectVersionID, long rightObjectVersionID)
  {
    IObjectRepository objectRepository = ServiceLocator.Get<IObjectRepository>();
    _Object otherAttributeHolder1 = objectRepository.Find(leftObjectVersionID);
    _Object otherAttributeHolder2 = objectRepository.Find(rightObjectVersionID);
    AttributeDiffCollection attributeDiffCollection1 = new AttributeDiffCollection((IAttributeHolder) otherAttributeHolder1, (IAttributeHolder) otherAttributeHolder2);
    AttributeDiffCollection attributeDiffCollection2 = new AttributeDiffCollection((IAttributeHolder) otherAttributeHolder2, (IAttributeHolder) otherAttributeHolder1);
    this._leftPropertyGrid.SelectedObject = (object) attributeDiffCollection1;
    this._rightPropertyGrid.SelectedObject = (object) attributeDiffCollection2;
  }

  private void LeftPropertyGrid_SelectedGridItemChanged(object sender, EventArgs e)
  {
    this._leftPropertyGrid.SelectedGridItemChanged -= new EventHandler(this.LeftPropertyGrid_SelectedGridItemChanged);
    try
    {
      int selectedGridItemIndex = this._leftPropertyGrid.SelectedGridItemIndex;
      if (selectedGridItemIndex < 0)
        return;
      this._rightPropertyGrid.SelectedGridItemIndex = selectedGridItemIndex;
    }
    finally
    {
      this._leftPropertyGrid.SelectedGridItemChanged += new EventHandler(this.LeftPropertyGrid_SelectedGridItemChanged);
    }
  }

  private void RightPropertyGrid_SelectedGridItemChanged(object sender, EventArgs e)
  {
    this._rightPropertyGrid.SelectedGridItemChanged -= new EventHandler(this.RightPropertyGrid_SelectedGridItemChanged);
    try
    {
      int selectedGridItemIndex = this._rightPropertyGrid.SelectedGridItemIndex;
      if (selectedGridItemIndex < 0)
        return;
      this._leftPropertyGrid.SelectedGridItemIndex = selectedGridItemIndex;
    }
    finally
    {
      this._rightPropertyGrid.SelectedGridItemChanged += new EventHandler(this.RightPropertyGrid_SelectedGridItemChanged);
    }
  }

  private void LeftPropertyGrid_PropertySortChanged(object sender, EventArgs e)
  {
    this._leftPropertyGrid.PropertySortChanged -= new EventHandler(this.LeftPropertyGrid_PropertySortChanged);
    try
    {
      this._rightPropertyGrid.PropertySort = this._leftPropertyGrid.PropertySort;
    }
    finally
    {
      this._leftPropertyGrid.PropertySortChanged += new EventHandler(this.LeftPropertyGrid_PropertySortChanged);
    }
  }

  private void RightPropertyGrid_PropertySortChanged(object sender, EventArgs e)
  {
    this._rightPropertyGrid.PropertySortChanged -= new EventHandler(this.RightPropertyGrid_PropertySortChanged);
    try
    {
      this._leftPropertyGrid.PropertySort = this._rightPropertyGrid.PropertySort;
    }
    finally
    {
      this._rightPropertyGrid.PropertySortChanged += new EventHandler(this.RightPropertyGrid_PropertySortChanged);
    }
  }

  private void LeftPropertyGrid_PropertyTabChanged(object sender, EventArgs e)
  {
    this._leftPropertyGrid.PropertyTabChanged -= new EventHandler(this.LeftPropertyGrid_PropertyTabChanged);
    try
    {
      this._rightPropertyGrid.SelectedPropertyTabType = this._leftPropertyGrid.SelectedPropertyTabType;
    }
    finally
    {
      this._leftPropertyGrid.PropertyTabChanged += new EventHandler(this.LeftPropertyGrid_PropertyTabChanged);
    }
  }

  private void RightPropertyGrid_PropertyTabChanged(object sender, EventArgs e)
  {
    this._rightPropertyGrid.PropertyTabChanged -= new EventHandler(this.RightPropertyGrid_PropertyTabChanged);
    try
    {
      this._leftPropertyGrid.SelectedPropertyTabType = this._rightPropertyGrid.SelectedPropertyTabType;
    }
    finally
    {
      this._rightPropertyGrid.PropertyTabChanged += new EventHandler(this.RightPropertyGrid_PropertyTabChanged);
    }
  }

  private void LeftPropertyGrid_GridItemCollapse(object sender, GridItemEventArgs e)
  {
    this._leftPropertyGrid.GridItemCollapse -= new EventHandler<GridItemEventArgs>(this.LeftPropertyGrid_GridItemCollapse);
    try
    {
      this._rightPropertyGrid.CollapseGridItem(e.GridItemIndex);
    }
    finally
    {
      this._leftPropertyGrid.GridItemCollapse += new EventHandler<GridItemEventArgs>(this.LeftPropertyGrid_GridItemCollapse);
    }
  }

  private void LeftPropertyGrid_GridItemExpand(object sender, GridItemEventArgs e)
  {
    this._leftPropertyGrid.GridItemExpand -= new EventHandler<GridItemEventArgs>(this.LeftPropertyGrid_GridItemExpand);
    try
    {
      this._rightPropertyGrid.ExpandGridItem(e.GridItemIndex);
    }
    finally
    {
      this._leftPropertyGrid.GridItemExpand += new EventHandler<GridItemEventArgs>(this.LeftPropertyGrid_GridItemExpand);
    }
  }

  private void RightPropertyGrid_GridItemCollapse(object sender, GridItemEventArgs e)
  {
    this._rightPropertyGrid.GridItemCollapse -= new EventHandler<GridItemEventArgs>(this.RightPropertyGrid_GridItemCollapse);
    try
    {
      this._leftPropertyGrid.CollapseGridItem(e.GridItemIndex);
    }
    finally
    {
      this._rightPropertyGrid.GridItemCollapse += new EventHandler<GridItemEventArgs>(this.RightPropertyGrid_GridItemCollapse);
    }
  }

  private void RightPropertyGrid_GridItemExpand(object sender, GridItemEventArgs e)
  {
    this._rightPropertyGrid.GridItemExpand -= new EventHandler<GridItemEventArgs>(this.RightPropertyGrid_GridItemExpand);
    try
    {
      this._leftPropertyGrid.ExpandGridItem(e.GridItemIndex);
    }
    finally
    {
      this._rightPropertyGrid.GridItemExpand += new EventHandler<GridItemEventArgs>(this.RightPropertyGrid_GridItemExpand);
    }
  }

  private void LeftPropertyGrid_TopGridItemChanged(object sender, EventArgs e)
  {
    this._leftPropertyGrid.TopGridItemChanged -= new EventHandler(this.LeftPropertyGrid_TopGridItemChanged);
    try
    {
      this._rightPropertyGrid.TopGridItemIndex = this._leftPropertyGrid.TopGridItemIndex;
    }
    finally
    {
      this._leftPropertyGrid.TopGridItemChanged += new EventHandler(this.LeftPropertyGrid_TopGridItemChanged);
    }
  }

  private void RightPropertyGrid_TopGridItemChanged(object sender, EventArgs e)
  {
    this._rightPropertyGrid.TopGridItemChanged -= new EventHandler(this.RightPropertyGrid_TopGridItemChanged);
    try
    {
      this._leftPropertyGrid.TopGridItemIndex = this._rightPropertyGrid.TopGridItemIndex;
    }
    finally
    {
      this._rightPropertyGrid.TopGridItemChanged += new EventHandler(this.RightPropertyGrid_TopGridItemChanged);
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
    this.tableLayoutPanel1 = new TableLayoutPanel();
    this._leftPropertyGrid = new SimplePropertyGrid();
    this._rightPropertyGrid = new SimplePropertyGrid();
    this.panel1 = new Panel();
    this.label3 = new Label();
    this.panel4 = new Panel();
    this.label2 = new Label();
    this.panel3 = new Panel();
    this.label1 = new Label();
    this.panel2 = new Panel();
    this.tableLayoutPanel1.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.tableLayoutPanel1.ColumnCount = 2;
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
    this.tableLayoutPanel1.Controls.Add((Control) this._leftPropertyGrid, 0, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this._rightPropertyGrid, 1, 0);
    this.tableLayoutPanel1.Controls.Add((Control) this.panel1, 0, 1);
    this.tableLayoutPanel1.Dock = DockStyle.Fill;
    this.tableLayoutPanel1.Location = new Point(0, 0);
    this.tableLayoutPanel1.Name = "tableLayoutPanel1";
    this.tableLayoutPanel1.RowCount = 2;
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
    this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
    this.tableLayoutPanel1.Size = new Size(779, 375);
    this.tableLayoutPanel1.TabIndex = 0;
    this._leftPropertyGrid.Dock = DockStyle.Fill;
    this._leftPropertyGrid.Location = new Point(3, 3);
    this._leftPropertyGrid.Name = "_leftPropertyGrid";
    this._leftPropertyGrid.PropertySort = PropertySort.CategorizedAlphabetical;
    this._leftPropertyGrid.SelectedObject = (object) null;
    this._leftPropertyGrid.Size = new Size(383, 319);
    this._leftPropertyGrid.TabIndex = 0;
    this._leftPropertyGrid.PropertyTabChanged += new EventHandler(this.LeftPropertyGrid_PropertyTabChanged);
    this._leftPropertyGrid.PropertySortChanged += new EventHandler(this.LeftPropertyGrid_PropertySortChanged);
    this._leftPropertyGrid.SelectedGridItemChanged += new EventHandler(this.LeftPropertyGrid_SelectedGridItemChanged);
    this._leftPropertyGrid.GridItemExpand += new EventHandler<GridItemEventArgs>(this.LeftPropertyGrid_GridItemExpand);
    this._leftPropertyGrid.GridItemCollapse += new EventHandler<GridItemEventArgs>(this.LeftPropertyGrid_GridItemCollapse);
    this._leftPropertyGrid.TopGridItemChanged += new EventHandler(this.LeftPropertyGrid_TopGridItemChanged);
    this._rightPropertyGrid.Dock = DockStyle.Fill;
    this._rightPropertyGrid.Location = new Point(392, 3);
    this._rightPropertyGrid.Name = "_rightPropertyGrid";
    this._rightPropertyGrid.PropertySort = PropertySort.CategorizedAlphabetical;
    this._rightPropertyGrid.SelectedObject = (object) null;
    this._rightPropertyGrid.Size = new Size(384, 319);
    this._rightPropertyGrid.TabIndex = 1;
    this._rightPropertyGrid.PropertyTabChanged += new EventHandler(this.RightPropertyGrid_PropertyTabChanged);
    this._rightPropertyGrid.PropertySortChanged += new EventHandler(this.RightPropertyGrid_PropertySortChanged);
    this._rightPropertyGrid.SelectedGridItemChanged += new EventHandler(this.RightPropertyGrid_SelectedGridItemChanged);
    this._rightPropertyGrid.GridItemExpand += new EventHandler<GridItemEventArgs>(this.RightPropertyGrid_GridItemExpand);
    this._rightPropertyGrid.GridItemCollapse += new EventHandler<GridItemEventArgs>(this.RightPropertyGrid_GridItemCollapse);
    this._rightPropertyGrid.TopGridItemChanged += new EventHandler(this.RightPropertyGrid_TopGridItemChanged);
    this.panel1.BackColor = Color.White;
    this.panel1.BorderStyle = BorderStyle.FixedSingle;
    this.tableLayoutPanel1.SetColumnSpan((Control) this.panel1, 2);
    this.panel1.Controls.Add((Control) this.label3);
    this.panel1.Controls.Add((Control) this.panel4);
    this.panel1.Controls.Add((Control) this.label2);
    this.panel1.Controls.Add((Control) this.panel3);
    this.panel1.Controls.Add((Control) this.label1);
    this.panel1.Controls.Add((Control) this.panel2);
    this.panel1.Dock = DockStyle.Fill;
    this.panel1.Location = new Point(3, 328);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(773, 44);
    this.panel1.TabIndex = 2;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(471, 14);
    this.label3.Name = "label3";
    this.label3.Size = new Size(205, 13);
    this.label3.TabIndex = 1;
    this.label3.Text = "Атрибут отсутствует у другого объекта";
    this.panel4.BackColor = Color.LightSteelBlue;
    this.panel4.Location = new Point(440, 7);
    this.panel4.Name = "panel4";
    this.panel4.Size = new Size(25, 20);
    this.panel4.TabIndex = 0;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(268, 14);
    this.label2.Name = "label2";
    this.label2.Size = new Size(166, 13);
    this.label2.TabIndex = 1;
    this.label2.Text = "Атрибут отсутствует у  объекта";
    this.panel3.BackColor = Color.Red;
    this.panel3.Location = new Point(237, 7);
    this.panel3.Name = "panel3";
    this.panel3.Size = new Size(25, 20);
    this.panel3.TabIndex = 0;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(35, 14);
    this.label1.Name = "label1";
    this.label1.Size = new Size(194, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Атрибуты с различными значениями";
    this.panel2.BackColor = Color.Yellow;
    this.panel2.Location = new Point(4, 7);
    this.panel2.Name = "panel2";
    this.panel2.Size = new Size(25, 20);
    this.panel2.TabIndex = 0;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Controls.Add((Control) this.tableLayoutPanel1);
    this.Name = nameof (ObjectDiffControl);
    this.Size = new Size(779, 375);
    this.tableLayoutPanel1.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }

  private class AllAttributesPropertyTab : PropertyTab
  {
    private Bitmap _bitmap;

    public AllAttributesPropertyTab() => this.CreateBitmap();

    public virtual string BitmapName => "imgPrintPreview";

    public override Bitmap Bitmap => this._bitmap;

    public override PropertyDescriptorCollection GetProperties(
      object component,
      Attribute[] attributes)
    {
      return component != null && Attribute.GetCustomAttribute((MemberInfo) component.GetType(), typeof (TypeConverterAttribute)) is TypeConverterAttribute customAttribute ? (Activator.CreateInstance(System.Type.GetType(customAttribute.ConverterTypeName)) as TypeConverter).GetProperties(component) : new PropertyDescriptorCollection(new System.ComponentModel.PropertyDescriptor[0]);
    }

    public override string TabName => "Все атрибуты";

    private void CreateBitmap()
    {
      if (!ServiceLocator.IsRegistered<INamedImageList>())
        return;
      INamedImageList namedImageList = ServiceLocator.Get<INamedImageList>();
      this._bitmap = new Bitmap(namedImageList.ImageList.Images[namedImageList.ImageIndex(this.BitmapName)]);
    }
  }

  private sealed class AllNotSystemAttributesPropertyTab : ObjectDiffControl.AllAttributesPropertyTab
  {
    public override string BitmapName => "imgProp";

    public override PropertyDescriptorCollection GetProperties(
      object component,
      Attribute[] attributes)
    {
      return new PropertyDescriptorCollection((System.ComponentModel.PropertyDescriptor[]) base.GetProperties(component, attributes).Cast<AttributeDiffPropertyDescriptor>().Where<AttributeDiffPropertyDescriptor>((Func<AttributeDiffPropertyDescriptor, bool>) (o => !this.IsSystemNotCaptionAttribute(o.AttributeType))).ToArray<AttributeDiffPropertyDescriptor>());
    }

    public override string TabName => "Все несистемные атрибуты";

    private bool IsSystemNotCaptionAttribute(IMSAttributeType attributeType)
    {
      return attributeType.AttributeID < 0 && attributeType.AttributeID != -50 || attributeType.Options.HasFlag((Enum) AttributeOptions.Internal);
    }
  }
}
