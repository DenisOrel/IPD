
// Type: Intermech.Controls.Grid.ListItemCollection
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.ComponentModel;


namespace Intermech.Controls.Grid;

/// <summary>Collection of GLItems</summary>
public class ListItemCollection : CollectionBase
{
  private ListGrid _parent;
  private bool _updating;

  /// <summary>Fires when a change occurs to the data</summary>
  public event ChangedEventHandler Changed;

  /// <summary>item has changed</summary>
  /// <param name="source"></param>
  /// <param name="e"></param>
  public void Item_Changed(object source, ChangedEventArgs e)
  {
    if (this.Changed == null || this.Updating)
      return;
    this.Changed(source, e);
  }

  /// <summary>Items have been cleared event</summary>
  protected override void OnClear()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ItemCollectionChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
  }

  /// <summary>Constructor</summary>
  /// <param name="newParent"></param>
  public ListItemCollection(ListGrid newParent) => this.Parent = newParent;

  /// <summary>
  /// this is used for operations where you are changing multiple items consecutively and don't want to send
  /// a larger number of change events than necessary.
  /// </summary>
  [Description("Extra user information.")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public bool Updating
  {
    set => this._updating = value;
    get => this._updating;
  }

  /// <summary>
  /// Sets the parent variable so we know what to refresh when there is a change
  /// </summary>
  [Description("Parent")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ListGrid Parent
  {
    get => this._parent;
    set
    {
      this._parent = value;
      foreach (ListItem listItem in (IEnumerable) this.List)
        listItem.Parent = this._parent;
    }
  }

  /// <summary>Indexer that allows the use of Items by []</summary>
  [Description("Item Collection")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
  [Browsable(true)]
  public ListItem this[int nItemIndex]
  {
    get => (ListItem) this.List[nItemIndex];
    set => this.List[nItemIndex] = (object) value;
  }

  /// <summary>returns a list of only the selected items</summary>
  [Description("Selected Items Array")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ArrayList SelectedItems
  {
    get
    {
      ArrayList selectedItems = new ArrayList();
      for (int nItemIndex = 0; nItemIndex < this.Count; ++nItemIndex)
      {
        if (this[nItemIndex].Selected)
          selectedItems.Add((object) this[nItemIndex]);
      }
      return selectedItems;
    }
  }

  /// <summary>returns a list of only the selected items indexes</summary>
  [Description("Selected Items Array Of Indicies")]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  [Browsable(false)]
  public ArrayList SelectedIndicies
  {
    get
    {
      ArrayList selectedIndicies = new ArrayList();
      for (int nItemIndex = 0; nItemIndex < this.Count; ++nItemIndex)
      {
        if (this[nItemIndex].Selected)
          selectedIndicies.Add((object) nItemIndex);
      }
      return selectedIndicies;
    }
  }

  /// <summary>Compatability with collection editor</summary>
  /// <param name="items"></param>
  public void AddRange(ListItem[] items)
  {
    lock (this.List.SyncRoot)
    {
      for (int index = 0; index < items.Length; ++index)
        this.Add(items[index]);
    }
  }

  /// <summary>add an item to the end of the list</summary>
  /// <param name="strItemText"></param>
  /// <returns></returns>
  public ListItem Add(string strItemText) => this.Insert(-1, strItemText);

  /// <summary>add an itemto the items collection</summary>
  /// <param name="item"></param>
  /// <returns></returns>
  public int Add(ListItem item) => this.Insert(-1, item);

  /// <summary>insert an item into the list at specified index</summary>
  /// <param name="nIndex"></param>
  /// <param name="strItemText"></param>
  /// <returns></returns>
  public ListItem Insert(int nIndex, string strItemText)
  {
    ListItem listItem = new ListItem(this.Parent);
    listItem.SubItems[0].Text = strItemText;
    nIndex = this.Insert(nIndex, listItem);
    return listItem;
  }

  /// <summary>
  /// lowest level of add/insert.  All add and insert routines eventually call this
  /// 
  /// in the future always have routines call this one as well to keep one point of entry
  /// </summary>
  /// <param name="nIndex"></param>
  /// <param name="item"></param>
  /// <returns></returns>
  public int Insert(int nIndex, ListItem item)
  {
    item.Parent = this.Parent;
    item.Changed += new ChangedEventHandler(this.Item_Changed);
    if (nIndex < 0)
      nIndex = this.List.Add((object) item);
    else
      this.List.Insert(nIndex, (object) item);
    if (this.Changed != null)
      this.Changed((object) this, new ChangedEventArgs(ChangedType.ItemCollectionAdded, (ListColumn) null, item, (ListSubItem) null));
    return nIndex;
  }

  /// <summary>remove an item from the list</summary>
  /// <param name="nItemIndex"></param>
  public void Remove(int itemIndex)
  {
    if (itemIndex >= this.Count || itemIndex < 0)
      return;
    ListItem listItem = (ListItem) this.List[itemIndex];
    if (listItem.Selected)
      listItem.Selected = false;
    this.List.RemoveAt(itemIndex);
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ItemCollectionRemoved, (ListColumn) null, listItem, (ListSubItem) null));
  }

  /// <summary>remove an item from the list</summary>
  /// <param name="item"></param>
  public void Remove(ListItem item)
  {
    this.List.Remove((object) item);
    if (item.Selected)
      item.Selected = false;
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ItemCollectionRemoved, (ListColumn) null, item, (ListSubItem) null));
  }

  /// <summary>Clears all selection bits in the item structure</summary>
  public void ClearSelection()
  {
    for (int nItemIndex = 0; nItemIndex < this.List.Count; ++nItemIndex)
      this[nItemIndex].Selected = false;
  }

  /// <summary>
  /// Clears all selection bits in the item structure
  /// 
  /// this overload is an optimization to stop a redraw on a re-selection
  /// </summary>
  public void ClearSelection(ListItem itemIgnore)
  {
    for (int nItemIndex = 0; nItemIndex < this.List.Count; ++nItemIndex)
    {
      ListItem listItem = this[nItemIndex];
      if (listItem != itemIgnore)
        listItem.Selected = false;
    }
  }

  public int FindNextItemIndex(int nStartIndex, int nColumn, string strItemText)
  {
    if (nStartIndex < 0 || nStartIndex > this.Count)
      nStartIndex = 0;
    for (int nItemIndex = nStartIndex; nItemIndex < this.Count; ++nItemIndex)
    {
      if (strItemText == this[nItemIndex].SubItems[nColumn].Text)
        return nItemIndex;
    }
    return -1;
  }

  public int GetNextSelectedItemIndex(int nStartIndex)
  {
    if (nStartIndex < 0 || nStartIndex > this.Count)
      nStartIndex = -1;
    for (int nItemIndex = nStartIndex + 1; nItemIndex < this.Count; ++nItemIndex)
    {
      if (this[nItemIndex].Selected)
        return nItemIndex;
    }
    return -1;
  }

  public int FindItemIndex(ListItem item)
  {
    for (int nItemIndex = 0; nItemIndex < this.Count; ++nItemIndex)
    {
      if (item == this[nItemIndex])
        return nItemIndex;
    }
    return -1;
  }
}
