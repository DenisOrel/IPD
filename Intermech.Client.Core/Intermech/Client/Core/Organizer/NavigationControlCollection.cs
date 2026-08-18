
// Type: Intermech.Client.Core.Organizer.NavigationControlCollection
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;


namespace Intermech.Client.Core.Organizer;

/// <summary>
/// 
/// </summary>
public class NavigationControlCollection : CollectionBase
{
  private CollectionEventHandler _itemAdded;
  private CollectionEventHandler _itemRemoved;
  protected bool _notify = true;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="comparer"></param>
  public virtual void Sort(IComparer comparer) => this.InnerList.Sort(comparer);

  /// <summary>
  /// 
  /// </summary>
  public event CollectionEventHandler ItemAdded
  {
    add => this._itemAdded += value;
    remove => this._itemAdded -= value;
  }

  /// <summary>
  /// 
  /// </summary>
  public event CollectionEventHandler ItemRemoved
  {
    add => this._itemRemoved += value;
    remove => this._itemRemoved -= value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <param name="value"></param>
  protected override void OnInsertComplete(int index, object value)
  {
    base.OnInsertComplete(index, value);
    if (this._itemAdded == null || !this._notify)
      return;
    this._itemAdded((object) this, new ChildCollectionEventArgs(value));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="index"></param>
  /// <param name="value"></param>
  protected override void OnRemoveComplete(int index, object value)
  {
    base.OnRemoveComplete(index, value);
    if (this._itemRemoved == null || !this._notify)
      return;
    this._itemRemoved((object) this, new ChildCollectionEventArgs(value));
  }
}
