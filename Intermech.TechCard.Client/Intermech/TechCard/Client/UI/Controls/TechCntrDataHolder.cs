// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechCntrDataHolder
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>
/// 
/// </summary>
public class TechCntrDataHolder
{
  private object _data;
  private readonly TechCntrRecBase _recInfo;

  /// <summary>Constructor</summary>
  /// <param name="dbAttributable"></param>
  /// <param name="data"></param>
  public TechCntrDataHolder(IDBAttributable dbAttributable, object data)
  {
    switch (dbAttributable)
    {
      case IDBObject _:
        this._recInfo = (TechCntrRecBase) new TechCntrRecObj(dbAttributable);
        break;
      case IDBRelation _:
        this._recInfo = (TechCntrRecBase) new TechCntrRecRel(dbAttributable);
        break;
    }
    this._data = data;
  }

  /// <summary>Record info</summary>
  public TechCntrRecBase RecInfo => this._recInfo;

  /// <summary>Get data</summary>
  public object Data
  {
    get => this._data;
    set => this._data = value;
  }
}
