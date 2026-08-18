
// Type: Intermech.Controls.Grid.ListSubItemCollection
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System.Collections;
using System.ComponentModel;


namespace Intermech.Controls.Grid;

/// <summary>Sub Item collection</summary>
public class ListSubItemCollection : CollectionBase
{
  private ListGrid _parent;

  public ListSubItemCollection()
  {
  }

  public ListSubItemCollection(ListGrid parent) => this._parent = parent;

  public event ChangedEventHandler Changed;

  public void SubItem_Changed(object source, ChangedEventArgs e)
  {
    if (this.Changed == null)
      return;
    this.Changed(source, e);
  }

  protected override void OnClear()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.ItemCollectionChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
  }

  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ListGrid Parent
  {
    get => this._parent;
    set
    {
      this._parent = value;
      foreach (ListSubItem listSubItem in (IEnumerable) this.List)
        listSubItem.Parent = value;
    }
  }

  public ListSubItem this[int nItemIndex]
  {
    get
    {
      int num = 0;
      while (this.List.Count <= nItemIndex)
      {
        ListSubItem listSubItem = new ListSubItem();
        listSubItem.Changed += new ChangedEventHandler(this.SubItem_Changed);
        listSubItem.Parent = this._parent;
        this.List.Add((object) listSubItem);
        if (num++ > 25)
          break;
      }
      return (ListSubItem) this.List[nItemIndex];
    }
  }

  public void AddRange(ListSubItem[] subItems)
  {
    lock (this.List.SyncRoot)
    {
      for (int index = 0; index < subItems.Length; ++index)
        this.Add(subItems[index]);
    }
  }

  public ListSubItem Add(string strItemText) => this.Insert(-1, strItemText);

  public int Add(ListSubItem subItem) => this.Insert(-1, subItem);

  public ListSubItem Insert(int nIndex, string strItemText)
  {
    ListSubItem subItem = new ListSubItem();
    subItem.Text = strItemText;
    nIndex = this.Insert(nIndex, subItem);
    return subItem;
  }

  public int Insert(int nIndex, ListSubItem subItem)
  {
    subItem.Parent = this._parent;
    subItem.Changed += new ChangedEventHandler(this.SubItem_Changed);
    if (nIndex < 0)
      nIndex = this.List.Add((object) subItem);
    else
      this.List.Insert(nIndex, (object) subItem);
    if (this.Changed != null)
      this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemCollectionChanged, (ListColumn) null, (ListItem) null, subItem));
    return nIndex;
  }

  public void Remove(int nSubItemIndex)
  {
    if (nSubItemIndex >= this.Count || nSubItemIndex < 0)
      return;
    this.List.RemoveAt(nSubItemIndex);
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemCollectionChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
  }

  public void Remove(ListSubItem subItem)
  {
    this.List.Remove((object) subItem);
    if (this.Changed == null)
      return;
    this.Changed((object) this, new ChangedEventArgs(ChangedType.SubItemCollectionChanged, (ListColumn) null, (ListItem) null, (ListSubItem) null));
  }

  public void ClearSelection()
  {
    for (int nItemIndex = 0; nItemIndex < this.List.Count; ++nItemIndex)
      this[nItemIndex].Selected = false;
  }
}
