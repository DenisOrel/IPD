// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.DBObjectAttributeCollection
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using System;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class DBObjectAttributeCollection : ObservableCollection<DBObjectAttributeEntry>
{
  public event EventHandler CollectionItemChanged;

  protected override void ClearItems()
  {
    for (int index = 0; index < this.Items.Count; ++index)
      this.DetachItem(this.Items[index]);
    base.ClearItems();
  }

  protected override void InsertItem(int index, DBObjectAttributeEntry item)
  {
    base.InsertItem(index, item);
    this.AttachItem(item);
  }

  protected override void SetItem(int index, DBObjectAttributeEntry item)
  {
    if (index < 0 || index >= this.Items.Count)
      throw new ArgumentOutOfRangeException(nameof (index));
    this.DetachItem(this.Items[index]);
    base.SetItem(index, item);
    this.AttachItem(item);
  }

  protected override void RemoveItem(int index)
  {
    if (index < 0 || index >= this.Items.Count)
      throw new ArgumentOutOfRangeException(nameof (index));
    this.DetachItem(this.Items[index]);
    base.RemoveItem(index);
  }

  private void AttachItem(DBObjectAttributeEntry item)
  {
    item.NewValuesChanged += new EventHandler(this.OnItemNewValuesChanged);
  }

  private void DetachItem(DBObjectAttributeEntry item)
  {
    item.NewValuesChanged -= new EventHandler(this.OnItemNewValuesChanged);
  }

  private void OnItemNewValuesChanged(object sender, EventArgs e)
  {
    EventHandler collectionItemChanged = this.CollectionItemChanged;
    if (collectionItemChanged == null)
      return;
    collectionItemChanged((object) this, EventArgs.Empty);
  }
}
