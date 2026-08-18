// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseObjectInfoItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Compositions;
using System;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseObjectInfoItem : 
  IComparable<ImbaseObjectInfoItem>,
  IEquatable<ImbaseObjectInfoItem>
{
  public ITypedInfoItem ObjectInfo { get; private set; }

  public long RecordId { get; private set; }

  public ImbaseObjectInfoItem(ITypedInfoItem objInfoItem, long recordId = -1)
  {
    this.ObjectInfo = objInfoItem;
    this.RecordId = recordId;
  }

  public int CompareTo(ImbaseObjectInfoItem other)
  {
    if (other == null)
      return -1;
    int num = this.ObjectInfo.ItemID.CompareTo(other.ObjectInfo.ItemID);
    return num != 0 ? num : this.RecordId.CompareTo(other.RecordId);
  }

  public bool Equals(ImbaseObjectInfoItem other) => this.CompareTo(other) == 0;
}
