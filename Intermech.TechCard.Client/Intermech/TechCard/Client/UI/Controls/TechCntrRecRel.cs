// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechCntrRecRel
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>Class for storing relation info</summary>
public class TechCntrRecRel : TechCntrRecBase
{
  private long _partId;
  private long _projId;

  /// <summary>Constructor</summary>
  /// <param name="dbAttributable"></param>
  public TechCntrRecRel(IDBAttributable dbAttributable)
    : base(dbAttributable)
  {
    if (!(dbAttributable is IDBRelation dbRelation))
      return;
    this._partId = dbRelation.PartID;
    this._projId = dbRelation.ProjID;
  }

  /// <summary>Part id</summary>
  public long PartID
  {
    get => this._partId;
    set => this._partId = value;
  }

  /// <summary>Project id</summary>
  public long ProjID
  {
    get => this._projId;
    set => this._projId = value;
  }
}
