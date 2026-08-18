// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.PropertyGridForm
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using Intermech.Bars;
using Intermech.Docking;
using Intermech.Interfaces.Document;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Панель свойств выбранного элемента документа</summary>
public class PropertyGridForm : DockControl, ISkipTargetActivate
{
  public static Guid DockGuid = new Guid("{1E0E4342-2C0C-429F-90CE-4FFD35A1F2CF}");
  private PropertyGrid propertyGrid;
  /// <summary>Required designer variable.</summary>
  private System.ComponentModel.Container components;

  /// <summary>Конструктор</summary>
  public PropertyGridForm()
  {
    this.InitializeComponent();
    this.HideOnClose = true;
    this.Guid = PropertyGridForm.DockGuid;
  }

  protected override void OnSizeChanged(EventArgs e) => base.OnSizeChanged(e);

  /// <summary>PropertyGrid</summary>
  private PropertyGrid PropertyGrid
  {
    [DebuggerStepThrough] get => this.propertyGrid;
  }

  /// <summary>Получение всех GridItem ProtertyGrid</summary>
  /// <param name="grid"></param>
  /// <returns></returns>
  public List<GridItem> GetPropertyGridItems(PropertyGrid grid)
  {
    List<GridItem> propertyGridItems = new List<GridItem>();
    if (grid.SelectedGridItem != null)
    {
      GridItem gridItem = grid.SelectedGridItem.Parent ?? grid.SelectedGridItem;
      while (gridItem.Parent != null)
        gridItem = gridItem.Parent;
      propertyGridItems = this.GetGridItems(gridItem);
    }
    return propertyGridItems;
  }

  /// <summary>Получить все дочерние GridItems текущего GridItem</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  public List<GridItem> GetGridItems(GridItem item)
  {
    List<GridItem> gridItems1 = new List<GridItem>();
    if ((!item.Expandable || item.Expandable && item.Expanded || item.GridItemType != GridItemType.Property) && item.GridItems != null && item.GridItems.Count != 0)
    {
      for (int index = 0; index < item.GridItems.Count; ++index)
      {
        List<GridItem> gridItems2 = this.GetGridItems(item.GridItems[index]);
        gridItems1.AddRange((IEnumerable<GridItem>) gridItems2);
      }
    }
    gridItems1.Add(item);
    return gridItems1;
  }

  /// <summary>Установить свойства в раскрытое состояние</summary>
  /// <param name="expandedProperties">имена свойств</param>
  /// <param name="grid">PropertyGrid со свойствами</param>
  public void SetExpandedPropertiesNames(List<string> expandedProperties, PropertyGrid grid)
  {
    List<GridItem> propertyGridItems = this.GetPropertyGridItems(grid);
    for (int index = 0; index < propertyGridItems.Count; ++index)
    {
      if (propertyGridItems[index].GridItemType == GridItemType.Property && expandedProperties.Contains(propertyGridItems[index].PropertyDescriptor.Name))
        propertyGridItems[index].Expanded = true;
    }
  }

  public List<string> GetExpandedPropertiesNames(List<GridItem> items)
  {
    List<string> expandedPropertiesNames = new List<string>();
    if (items != null)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index] != null && items[index].Expanded && items[index].PropertyDescriptor != null)
          expandedPropertiesNames.Add(items[index].PropertyDescriptor.Name);
      }
    }
    return expandedPropertiesNames;
  }

  private void PropertyGridForm_Closing(object sender, CancelEventArgs e)
  {
  }

  private void PropertyGridForm_Closed(object sender, EventArgs e)
  {
  }

  protected override bool ProcessDialogKey(Keys keyData)
  {
    if (keyData == (Keys.C | Keys.Control) && this.ActiveControl is PropertyGrid activeControl1 && activeControl1.ActiveControl is TextBoxBase)
    {
      (activeControl1.ActiveControl as TextBoxBase).Copy();
      return true;
    }
    if (keyData == (Keys.V | Keys.Control) && this.ActiveControl is PropertyGrid activeControl2 && activeControl2.ActiveControl is TextBoxBase)
    {
      (activeControl2.ActiveControl as TextBoxBase).Paste();
      return true;
    }
    if (keyData != (Keys.X | Keys.Control) || !(this.ActiveControl is PropertyGrid activeControl3) || !(activeControl3.ActiveControl is TextBoxBase))
      return base.ProcessDialogKey(keyData);
    (activeControl3.ActiveControl as TextBoxBase).Cut();
    return true;
  }

  /// <summary>Clean up any resources being used.</summary>
  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.propertyGrid != null)
        this.propertyGrid.SelectedObject = (object) null;
      if (this.components != null)
        this.components.Dispose();
    }
    base.Dispose(disposing);
  }

  /// <summary>Required method for Designer support - do not modify
  /// the contents of this method with the code editor.</summary>
  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PropertyGridForm));
    this.propertyGrid = new PropertyGrid();
    this.SuspendLayout();
    this.propertyGrid.AccessibleDescription = (string) null;
    this.propertyGrid.AccessibleName = (string) null;
    componentResourceManager.ApplyResources((object) this.propertyGrid, "propertyGrid");
    this.propertyGrid.BackgroundImage = (Image) null;
    this.propertyGrid.Font = (Font) null;
    this.propertyGrid.LineColor = SystemColors.ScrollBar;
    this.propertyGrid.Name = "propertyGrid";
    this.propertyGrid.SelectedObjectsChanged += new EventHandler(this.propertyGrid_SelectedObjectsChanged);
    this.AccessibleDescription = (string) null;
    componentResourceManager.ApplyResources((object) this, "$this");
    this.BackgroundImage = (Image) null;
    this.Controls.Add((Control) this.propertyGrid);
    this.Font = (Font) null;
    this.HideOnClose = true;
    this.Name = nameof (PropertyGridForm);
    this.Closed += new EventHandler(this.PropertyGridForm_Closed);
    this.Closing += new CancelEventHandler(this.PropertyGridForm_Closing);
    this.ResumeLayout(false);
  }

  /// <summary>Выделенный объект</summary>
  public object SelectedObject
  {
    get => this.PropertyGrid.SelectedObject;
    set
    {
      if (this.IsDisposed)
        return;
      List<string> expandedPropertiesNames = this.GetExpandedPropertiesNames(this.GetPropertyGridItems(this.PropertyGrid));
      this.PropertyGrid.SelectedObject = value;
      this.SetExpandedPropertiesNames(expandedPropertiesNames, this.PropertyGrid);
    }
  }

  /// <summary>Выделенные объекты</summary>
  public object[] SelectedObjects
  {
    get => this.PropertyGrid.SelectedObjects;
    set
    {
      if (this.IsDisposed)
        return;
      List<string> expandedPropertiesNames = this.GetExpandedPropertiesNames(this.GetPropertyGridItems(this.PropertyGrid));
      this.PropertyGrid.SelectedObjects = value;
      this.SetExpandedPropertiesNames(expandedPropertiesNames, this.PropertyGrid);
    }
  }

  private void propertyGrid_SelectedObjectsChanged(object sender, EventArgs e)
  {
    if (this.propertyGrid.SelectedObject is DocumentTreeNode selectedObject && (this.propertyGrid.SelectedObjects == null || this.propertyGrid.SelectedObjects.Length < 2))
    {
      if (selectedObject.Parent is TableData parent)
      {
        if (parent.IsColumn)
          this.Text = $"{LocalizationHolder.rm.GetString("Document.Model_84")}{selectedObject.GetDefautCaption()}\"";
        else
          this.Text = $"{LocalizationHolder.rm.GetString("Document.Model_85")}{selectedObject.GetDefautCaption()}\"";
      }
      else
        this.Text = $"{LocalizationHolder.rm.GetString("Document.Model_86")}{selectedObject.NodeTypeCaption} \"{selectedObject.GetDefautCaption()}\"";
    }
    else
      this.Text = LocalizationHolder.rm.GetString("Document.Model_87");
  }
}
