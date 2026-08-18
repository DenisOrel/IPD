
// Type: Intermech.Controls.Grid.ColumnCollection
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Controls.Grid;

public class ColumnCollection : CollectionBase
{
  private ListGrid _parent;

  public ColumnCollection(ListGrid parent) => this.Parent = parent;

  public event ChangedEventHandler Changed;

  public void GLColumn_Changed(object source, ChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(source, e);
  }

  [Description("Parent")]
  [Browsable(false)]
  public ListGrid Parent
  {
    get => this._parent;
    set => this._parent = value;
  }

  public ListColumn this[int nColumnIndex] => this.List[nColumnIndex] as ListColumn;

  public ListColumn this[string strColumnName]
  {
    get => (ListColumn) this.List[this.GetColumnIndex(strColumnName)];
  }

  /// <summary>
  /// Get the column index that corresponds to the column name
  /// </summary>
  /// <param name="strColumnName"></param>
  /// <returns></returns>
  public int GetColumnIndex(string strColumnName)
  {
    for (int index = 0; index < this.List.Count; ++index)
    {
      if (((ListColumn) this.List[index]).Name == strColumnName)
        return index;
    }
    return -1;
  }

  /// <summary>the combined width of all of the columns</summary>
  public int Width
  {
    get
    {
      int width = 0;
      for (int index = 0; index < this.List.Count; ++index)
      {
        ListColumn listColumn = (ListColumn) this.List[index];
        width += listColumn.Width;
      }
      return width;
    }
  }

  /// <summary>Get Span Size for column spanning</summary>
  /// <param name="strStartColumnName"></param>
  /// <param name="nColumnsSpanned"></param>
  /// <returns></returns>
  public int GetSpanSize(string strStartColumnName, int nColumnsSpanned)
  {
    int columnIndex = this.GetColumnIndex(strStartColumnName);
    int spanSize = 0;
    if (nColumnsSpanned + columnIndex > this.Count)
      nColumnsSpanned = this.Count - columnIndex;
    for (int nColumnIndex = columnIndex; nColumnIndex < columnIndex + nColumnsSpanned; ++nColumnIndex)
      spanSize += this[nColumnIndex].Width;
    return spanSize;
  }

  /// <summary>Add a column to collection</summary>
  /// <param name="newColumn"></param>
  public void Add(ListColumn newColumn)
  {
    newColumn.Parent = this.Parent;
    newColumn.Changed += new ChangedEventHandler(this.GLColumn_Changed);
    ListColumn listColumn;
    for (; this.GetColumnIndex(newColumn.Name) != -1; listColumn.Name += "x")
      listColumn = newColumn;
    this.List.Add((object) newColumn);
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnCollectionChanged, newColumn, (ListItem) null, (ListSubItem) null));
  }

  /// <summary>Add Column to collection</summary>
  /// <param name="strColumnName"></param>
  /// <param name="nColumnWidth"></param>
  public ListColumn Add(string strColumnName, int nColumnWidth)
  {
    ListColumn newColumn = new ListColumn();
    newColumn.Text = strColumnName;
    newColumn.Name = strColumnName;
    newColumn.Width = nColumnWidth;
    newColumn.State = ColumnState.Normal;
    newColumn.TextAlignment = ContentAlignment.MiddleLeft;
    newColumn.Parent = this.Parent;
    this.Add(newColumn);
    return newColumn;
  }

  /// <summary>Add Column to collection</summary>
  /// <param name="strColumnName"></param>
  /// <param name="nColumnWidth"></param>
  /// <param name="align"></param>
  public ListColumn Add(string strColumnName, int nColumnWidth, HorizontalAlignment align)
  {
    ListColumn newColumn = new ListColumn();
    newColumn.Text = strColumnName;
    newColumn.Name = strColumnName;
    newColumn.Width = nColumnWidth;
    newColumn.State = ColumnState.Normal;
    newColumn.TextAlignment = ContentAlignment.MiddleLeft;
    this.Add(newColumn);
    return newColumn;
  }

  /// <summary>Add Range of columns to collection</summary>
  /// <param name="columns"></param>
  public void AddRange(ListColumn[] columns)
  {
    lock (this.List.SyncRoot)
    {
      for (int index = 0; index < columns.Length; ++index)
        this.Add(columns[index]);
    }
  }

  /// <summary>Remove Column from collection</summary>
  /// <param name="nColumnIndex"></param>
  public void Remove(int nColumnIndex)
  {
    if (nColumnIndex >= this.Count || nColumnIndex < 0)
      return;
    this.List.RemoveAt(nColumnIndex);
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnCollectionChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
  }

  /// <summary>Remove all columns from collection</summary>
  public new void Clear()
  {
    this.List.Clear();
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ColumnCollectionChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
  }

  /// <summary>Return index of column in collection</summary>
  /// <param name="column"></param>
  /// <returns></returns>
  public int IndexOf(ListColumn column) => this.List.IndexOf((object) column);

  /// <summary>Clear column states</summary>
  /// <remarks>Primarily used to clear pressed / hot states</remarks>
  public void ClearStates()
  {
    foreach (ListColumn listColumn in (IEnumerable) this.List)
      listColumn.State = ColumnState.Normal;
  }

  /// <summary>Clear only hot states from column collection</summary>
  public void ClearHotStates()
  {
    foreach (ListColumn listColumn in (IEnumerable) this.List)
    {
      if (listColumn.State == ColumnState.Hot)
        listColumn.State = ColumnState.Normal;
    }
  }

  /// <summary>
  /// if any of the columns are in a pressed state then disable all hotting
  /// </summary>
  /// <returns></returns>
  public bool AnyPressed()
  {
    foreach (ListColumn listColumn in (IEnumerable) this.List)
    {
      if (listColumn.State == ColumnState.Pressed)
        return true;
    }
    return false;
  }
}
