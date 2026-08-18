// Decompiled with JetBrains decompiler
// Type: Intermech.VirtualTreeView.VirtualTreeView
// Assembly: Intermech.VirtualTreeView, Version=4.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: CFAE8D69-6554-4155-8AB7-42592C2FC48A
// Assembly location: D:\IPS\Client\Intermech.VirtualTreeView.dll

using Infralution.Controls.VirtualTree;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.VirtualTreeView;

public class VirtualTreeView : Infralution.Controls.VirtualTree.VirtualTree
{
  private IContainer components;
  protected bool _disableHeaderContextMenu;
  internal Intermech.VirtualTreeView.VirtualTreeView.VisibleNodesComparer _nodesComparer = new Intermech.VirtualTreeView.VirtualTreeView.VisibleNodesComparer();

  public VirtualTreeView()
  {
    this.InitializeComponent();
    this.InitTreeResources();
    this.InitTreeServices();
    this.InitTreeStyles();
    this.InitEventHandlers();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent() => this.components = (IContainer) new System.ComponentModel.Container();

  [Description("Событие вызывается для вызова пользовательского контекстного меню")]
  [Browsable(true)]
  public event MouseEventHandler ShowContextMenu;

  protected virtual void InitTreeResources()
  {
  }

  protected virtual void InitTreeServices()
  {
  }

  protected virtual void InitTreeStyles()
  {
  }

  protected virtual void InitEventHandlers()
  {
    this.MouseUp += new MouseEventHandler(this._MouseUp);
  }

  protected virtual void DeactivateTreeServices()
  {
  }

  protected virtual void DeactivateTreeResources()
  {
  }

  [Category("Appearance")]
  [Description("Свойство позволяет скрыть контекстное меню на заголовке дерева")]
  [Browsable(true)]
  public virtual bool DisableHeaderContextMenu
  {
    get => this._disableHeaderContextMenu;
    set => this._disableHeaderContextMenu = value;
  }

  [Description("Fired before the tree shows editor in selected cell")]
  [Category("Data")]
  [Browsable(true)]
  public event BeforeShowCellEditHandler BeforeShowCellEdit;

  private void _MouseUp(object sender, MouseEventArgs e) => this.TreeMouseUp(sender, e);

  protected override bool ProcessSpaceCmdKey(Keys modifiers) => false;

  internal BeforeShowCellEditEventArgs FireBeforeShowCellEdit(AdvCellWidget cellWidget)
  {
    if (cellWidget == null || this.BeforeShowCellEdit == null)
      return new BeforeShowCellEditEventArgs((Row) null, (Column) null);
    BeforeShowCellEditEventArgs e = new BeforeShowCellEditEventArgs(cellWidget.Row, cellWidget.Column);
    this.BeforeShowCellEdit((object) this, e);
    return e;
  }

  protected virtual void TreeMouseUp(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Right || this.ShowContextMenu == null || e.Y <= this.HeaderHeight)
      return;
    this.ShowContextMenu(sender, e);
  }

  protected virtual ToolStripMenuItem AddMenuItem(
    ContextMenuStrip menu,
    string name,
    string text,
    string iconName,
    bool addToContainer)
  {
    Image image = (Image) null;
    if (iconName != null)
      image = (Image) new Icon(typeof (Infralution.Controls.VirtualTree.VirtualTree), iconName).ToBitmap();
    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text, image);
    toolStripMenuItem.Tag = (object) name;
    menu.Items.Add((ToolStripItem) toolStripMenuItem);
    if (addToContainer)
      this.AddToContainer((Component) toolStripMenuItem, name);
    return toolStripMenuItem;
  }

  private void AddSeparatorMenuItem(ContextMenuStrip menu, string name, bool addToContainer)
  {
    ToolStripItem toolStripItem = (ToolStripItem) new ToolStripSeparator();
    menu.Items.Add(toolStripItem);
    if (!addToContainer)
      return;
    this.AddToContainer((Component) toolStripItem, name);
  }

  protected new void AddToContainer(Component component, string name)
  {
    string name1 = name;
    int num = 1;
    while (this.Container.Components[name1] != null)
      name1 = name + (object) num++;
    this.Container.Add((IComponent) component, name1);
  }

  public override ContextMenuStrip CreateHeaderContextMenu(bool addToContainer)
  {
    if (this.DisableHeaderContextMenu)
      return new ContextMenuStrip();
    ContextMenuStrip menu = new ContextMenuStrip();
    if (addToContainer)
      this.AddToContainer((Component) menu, "headerContextMenu");
    this.AddMenuItem(menu, "sortAscendingMenuItem", "Сортировать по возрастанию", "Icons.SortAscendingMenu.ico", addToContainer);
    this.AddMenuItem(menu, "sortDescendingMenuItem", "Сортировать по убыванию", "Icons.SortDescendingMenu.ico", addToContainer);
    this.AddSeparatorMenuItem(menu, "separator1MenuItem", addToContainer);
    this.AddMenuItem(menu, "bestFitMenuItem", "Рассчитать ширину колонки", "Icons.BestFit.ico", addToContainer);
    this.AddMenuItem(menu, "bestFitAllMenuItem", "Рассчитать ширину всех колонок", "Icons.BestFitAll.ico", addToContainer);
    this.AddMenuItem(menu, "autoFitMenuItem", "Вписать все колонки", "Icons.AutoFit.ico", addToContainer);
    return menu;
  }

  protected override CellWidget CreateCellWidget(RowWidget rowWidget, Column column)
  {
    return (CellWidget) new AdvCellWidget(rowWidget, column);
  }

  public Column GetColumnAt(int x, int y)
  {
    int num = 0;
    for (int index = 0; index < this.Columns.Count; ++index)
    {
      Column column = this.Columns[index];
      if (x >= num && x <= column.Width + num)
        return column;
      num += column.Width;
    }
    return (Column) null;
  }

  public Row GetRowAt(int x, int y)
  {
    Hashtable rows = new Hashtable();
    this.GetRows(this.TopRowIndex, this.BottomRowIndex + 1, rows);
    if (rows.Count > 0)
    {
      foreach (DictionaryEntry dictionaryEntry in rows)
      {
        RowWidget rowWidget = this.PinnedPanel.GetRowWidget(dictionaryEntry.Value as Row);
        if (rowWidget != null && rowWidget.Bounds.Top <= y && rowWidget.Bounds.Bottom >= y)
          return dictionaryEntry.Value as Row;
      }
    }
    return (Row) null;
  }

  public Row GetNodeAt(int x, int y)
  {
    Hashtable rows = new Hashtable();
    this.GetRows(this.TopRowIndex, this.BottomRowIndex + 1, rows);
    if (rows.Count > 0)
    {
      foreach (DictionaryEntry dictionaryEntry in rows)
      {
        RowWidget rowWidget = this.PinnedPanel.GetRowWidget(dictionaryEntry.Value as Row);
        if (rowWidget != null && rowWidget.Bounds.Top <= y && rowWidget.Bounds.Bottom >= y)
          return dictionaryEntry.Value as Row;
      }
    }
    return (Row) null;
  }

  public Row[] GetVisibleNodes()
  {
    Hashtable rows = new Hashtable();
    List<Row> rowList = new List<Row>();
    this.GetRows(this.TopRowIndex, this.BottomRowIndex, rows);
    if (rows.Count > 0)
    {
      foreach (DictionaryEntry dictionaryEntry in rows)
      {
        if (dictionaryEntry.Value is Row row)
          rowList.Add(row);
      }
    }
    rowList.Sort((IComparer<Row>) this._nodesComparer);
    return rowList.ToArray();
  }

  internal class VisibleNodesComparer : IComparer<Row>
  {
    public int Compare(Row x, Row y)
    {
      return x == null || y == null ? 0 : x.ChildIndex.CompareTo(y.ChildIndex);
    }
  }
}
