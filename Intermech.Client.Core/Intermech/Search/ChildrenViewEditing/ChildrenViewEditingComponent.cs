
// Type: Intermech.Search.ChildrenViewEditing.ChildrenViewEditingComponent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using TenTec.Windows.iGridLib;


namespace Intermech.Search.ChildrenViewEditing;

public sealed class ChildrenViewEditingComponent : AttributeEditingComponent
{
  /// <summary>Required designer variable.</summary>
  private IContainer components;

  public ChildrenViewEditingComponent() => this.InitializeComponent();

  public ChildrenViewEditingComponent(IContainer container)
  {
    container.Add((IComponent) this);
    this.InitializeComponent();
  }

  protected override void DoAttach()
  {
    this.ChildrenView.Grid.CellClick += new iGCellClickEventHandler(this.ChildrenViewGrid_CellClick);
    this.ChildrenView.Grid.Resize += new EventHandler(this.ChildrenViewGrid_Resize);
  }

  protected override void DoDetach()
  {
    this.ChildrenView.Grid.CellClick -= new iGCellClickEventHandler(this.ChildrenViewGrid_CellClick);
    this.ChildrenView.Grid.Resize -= new EventHandler(this.ChildrenViewGrid_Resize);
  }

  public override int[] GetPresentAttributes()
  {
    return this.ChildrenView.GetNodeColumns().Select<NodeColumn, IMSAttributeType>((Func<NodeColumn, IMSAttributeType>) (o => o.Attribute)).Where<IMSAttributeType>((Func<IMSAttributeType, bool>) (o => o != null)).Select<IMSAttributeType, int>((Func<IMSAttributeType, int>) (o => o.AttributeID)).Distinct<int>().ToArray<int>();
  }

  private void ChildrenViewGrid_CellClick(object sender, iGCellClickEventArgs e)
  {
    ChildrenViewCellData cellData = this.ChildrenView.GetCellData(e.RowIndex, e.ColIndex);
    if (cellData != null)
    {
      this.NodeColumn = cellData.NodeColumn;
      this.NodeID = cellData.RowData.NodeID;
      iGCell cell = this.ChildrenView.Grid.Cells[e.RowIndex, e.ColIndex];
      int x = this.ChildrenView.Grid.Location.X + cell.TextBounds.X;
      int y = this.ChildrenView.Grid.Location.Y + cell.TextBounds.Y;
      Rectangle textBounds = cell.TextBounds;
      int width = textBounds.Width;
      textBounds = cell.TextBounds;
      int height = textBounds.Height;
      this.Bounds = new Rectangle(x, y, width, height);
      this.InitializeEditor();
      this.SetCellsReadOnly(cellData.RowData);
      if (!this.IsUndetermined)
        this.ShowEditor();
      else
        cellData.ReadOnly = new bool?(true);
    }
    else
      this.SetUndetermined();
  }

  private void ChildrenViewGrid_Resize(object sender, EventArgs e) => this.HideEditor();

  private ChildrenView ChildrenView => (ChildrenView) this.Control;

  private void SetCellsReadOnly(ChildrenViewRowData rowData)
  {
    if (this.AttributesValues == null || this.NodeColumn == null)
      return;
    foreach (AttributeValues attributesValue in this.AttributesValues)
    {
      ChildrenViewCellData cellData = this.GetCellData(rowData, attributesValue.AttributeID, this.NodeColumn.AttrSource);
      if (cellData != null)
        cellData.ReadOnly = new bool?(attributesValue.ReadOnly);
    }
  }

  private ChildrenViewCellData GetCellData(
    ChildrenViewRowData rowData,
    int attributeTypeID,
    AttributeSourceTypes attributeSourceType)
  {
    return rowData.CellDataDictionary.Values.FirstOrDefault<ChildrenViewCellData>((Func<ChildrenViewCellData, bool>) (o => o.NodeColumn != null && o.NodeColumn.Attribute != null && o.NodeColumn.Attribute.AttributeID == attributeTypeID && o.NodeColumn.AttrSource == attributeSourceType));
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
