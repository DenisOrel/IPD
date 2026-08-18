// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechCntrRecObj
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>Class for storing object info</summary>
public class TechCntrRecObj : TechCntrRecBase
{
  private long _checkOutBy;

  /// <summary>Constructor</summary>
  /// <param name="dbAttributable"></param>
  public TechCntrRecObj(IDBAttributable dbAttributable)
    : base(dbAttributable)
  {
    if (!(dbAttributable is IDBObject dbObject))
      return;
    this._checkOutBy = dbObject.CheckoutBy;
  }

  /// <summary>
  /// 
  /// </summary>
  public long CheckOutBy
  {
    get => this._checkOutBy;
    set => this._checkOutBy = value;
  }
}
