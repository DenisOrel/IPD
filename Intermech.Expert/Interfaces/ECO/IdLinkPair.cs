// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.ECO.IdLinkPair
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;

#nullable disable
namespace Intermech.Interfaces.ECO;

[Serializable]
public class IdLinkPair
{
  private long _objID;
  private long _relID;

  /// <summary>ИД объекта, включенного в извещение</summary>
  public long ObjID
  {
    get => this._objID;
    set => this._objID = value;
  }

  /// <summary>ИД связи от извещения к объекту</summary>
  public long RelID
  {
    get => this._relID;
    set => this._relID = value;
  }

  /// <summary>Цель включения</summary>
  public ECOGoal Goal { get; set; }

  public IdLinkPair(long oid, long rId)
  {
    this._objID = oid;
    this._relID = rId;
  }

  public IdLinkPair(long oid, long rId, int goal)
  {
    this._objID = oid;
    this._relID = rId;
    this.Goal = (ECOGoal) goal;
  }
}
