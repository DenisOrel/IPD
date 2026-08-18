
// Type: Intermech.Navigator.Controls.NavigatorTreeColumn
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;


namespace Intermech.Navigator.Controls;

/// <summary>Колонка дерева "Навигатора"</summary>
public class NavigatorTreeColumn : Column
{
  /// <summary>
  /// Колонка "Навигатора", на основании которой построена данная колонка
  /// </summary>
  private NodeColumn _navigatorColumn;
  /// <summary>
  /// Абсолютный индекс колонки в коллекции колонок "Навигатора"
  /// </summary>
  private int _absoluteIndex = -1;

  /// <summary>Конструктор</summary>
  public NavigatorTreeColumn()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="tree">Дерево</param>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  public NavigatorTreeColumn(
    NavigatorTreeView tree,
    NodeColumn column,
    NodeColumnCollection columns)
  {
    this.SetNavigatorColumn(tree, column, columns);
  }

  /// <summary>
  /// Колонка "Навигатора", на основании которой построена данная колонка
  /// </summary>
  public virtual NodeColumn NavigatorColumn
  {
    [DebuggerStepThrough] get => this._navigatorColumn;
  }

  /// <summary>
  /// Установить свойства колонки на основании данных, полученных у колонки "Навигатора"
  /// </summary>
  /// <param name="tree">Дерево</param>
  /// <param name="column">Колонка "Навигатора"</param>
  /// <param name="columns">Коллекция колонок "Навигатора"</param>
  public virtual void SetNavigatorColumn(
    NavigatorTreeView tree,
    NodeColumn column,
    NodeColumnCollection columns)
  {
    if (column == null || columns == null)
      return;
    this._navigatorColumn = column;
    this._absoluteIndex = columns.IndexOf(column);
    this.Name = column.Key;
    this.Caption = !UISettings.ShowShortAttributeNames ? column.Caption : column.ShortCaption;
    this.Width = column.Width;
    this.AutoSizePolicy = columns.Count > 1 ? ColumnAutoSizePolicy.Manual : ColumnAutoSizePolicy.AutoSize;
    this.Resizable = !tree.DisableColumnsSizing;
    this.Movable = !tree.DisableColumnsMoving;
    this.HeaderStyle.HorzAlignment = StringAlignment.Near;
    INodeColumnTransform defaultTransform = ((IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes))).GetDefaultTransform(column.SchemeGuid, column.ID);
    Type type = defaultTransform != null ? defaultTransform.DataType : column.DataType;
    if (type == typeof (int) || type == typeof (long) || type == typeof (double) || type == typeof (DateTime))
      this.HeaderStyle.HorzAlignment = StringAlignment.Far;
    this.SortDirection = column.SortOrder == NodeColumnSortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
    this.Sortable = !tree.DisableColumnsSorting && !column.DisableSorting;
    if (tree.DisableColumnsSorting || column.DataType == typeof (byte[]))
    {
      column.SortOrder = NodeColumnSortOrder.None;
      column.SortIndex = -1;
      this.Sortable = false;
    }
    this.HeaderStyle.WordWrap = false;
    this.ToolTip = column.Hint;
    if (columns.Count == 1)
      tree.RowStyle.BorderWidth = 0;
    else
      tree.RowStyle.BorderWidth = 1;
    tree.Columns.Add((Column) this);
    if (tree.Columns.IndexOf((Column) this) == 0)
      this.Tree.MainColumn = (Column) this;
    this.SortIndex = column.SortIndex;
  }

  /// <summary>
  /// Индекс колонки в порядке её создания/добавления в дерево
  /// </summary>
  public virtual int AbsoluteIndex
  {
    [DebuggerStepThrough] get => this._absoluteIndex;
    set => this._absoluteIndex = value;
  }

  /// <summary>
  /// Номер колонки в списке сортируемых колонок.
  /// Внимание! Дерево может сортировать только по одной колонке!
  /// </summary>
  public virtual int SortIndex
  {
    get => this.Tree.SortColumn == this ? 0 : -1;
    set
    {
      if (value == -1 && this.Tree.SortColumn == this)
        this.Tree.SortColumn = (Column) null;
      if (value < 0 || this.Tree.SortColumn == this)
        return;
      this.Tree.SortColumn = (Column) this;
    }
  }

  /// <summary>Автоматически рассчитать ширину колонки</summary>
  public virtual void BestFit() => this.Tree.SetBestFitWidth((Column) this);
}
