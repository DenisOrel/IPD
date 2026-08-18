// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Controls.ItemData
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;

#nullable disable
namespace Intermech.Imbase.Controls;

internal class ItemData : IComparable, IEquatable<ItemData>
{
  private string _guid;
  private string _caption;

  public ItemData(string caption, string guid)
  {
    this._caption = caption;
    this._guid = guid;
  }

  public override string ToString() => this._caption;

  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  public override int GetHashCode() => this.Guid.GetHashCode();

  public int CompareTo(object obj)
  {
    string empty = string.Empty;
    string strB;
    switch (obj)
    {
      case ItemData _:
        strB = ((ItemData) obj).Guid;
        break;
      case System.Guid guid:
        strB = guid.ToString();
        break;
      case string _:
        strB = (string) obj;
        break;
      default:
        return -1;
    }
    return this.Guid.CompareTo(strB);
  }

  public bool Equals(ItemData other) => this.Equals((object) other);

  public string Guid => this._guid;
}
