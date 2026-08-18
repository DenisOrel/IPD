// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.ThumbnailsDictionary
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase.Views;

internal sealed class ThumbnailsDictionary : Dictionary<int, ThumbnailItem>
{
  private List<long> _imgIDCollection = new List<long>();

  public void Add(long imgID, int typeID)
  {
    if (this._imgIDCollection.Contains(imgID))
      return;
    this.Add(this.Count, new ThumbnailItem(imgID, typeID));
  }

  public void Add(ThumbnailsDictionary dict)
  {
    for (int key = 0; key < dict.Count; ++key)
      this.Add(this.Count, dict[key]);
  }

  public new void Add(int key, ThumbnailItem value)
  {
    if (this.ContainsKey(key) || this._imgIDCollection.Contains(value.ImageID))
      return;
    base.Add(this.Count, value);
    this._imgIDCollection.Add(value.ImageID);
  }
}
