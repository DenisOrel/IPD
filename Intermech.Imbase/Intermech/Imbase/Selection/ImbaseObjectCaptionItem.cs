// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseObjectCaptionItem
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Compositions;
using System;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseObjectCaptionItem : 
  ImbaseObjectInfoItem,
  IComparable<ImbaseObjectCaptionItem>,
  IEquatable<ImbaseObjectCaptionItem>
{
  public ImbaseObjectCaptionItem(IObjInfoCaption objectInfo, long recordId)
    : base((ITypedInfoItem) objectInfo, recordId)
  {
    this.ObjectInfo = objectInfo;
  }

  public int CompareTo(ImbaseObjectCaptionItem other)
  {
    return this.CompareTo((ImbaseObjectInfoItem) other);
  }

  public bool Equals(ImbaseObjectCaptionItem other) => this.Equals((ImbaseObjectInfoItem) other);

  public IObjInfoCaption ObjectInfo { get; private set; }
}
