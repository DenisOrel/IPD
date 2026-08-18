// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.UI.Controls.TechCntrRecBase
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;

#nullable disable
namespace Intermech.TechCard.Client.UI.Controls;

/// <summary>Base class for storing info about rec</summary>
public class TechCntrRecBase
{
  private long _recId;
  private long _recType;

  /// <summary>Constructor</summary>
  /// <param name="dbAttributable"></param>
  public TechCntrRecBase(IDBAttributable dbAttributable)
  {
    switch (dbAttributable)
    {
      case IDBObject dbObject:
        this._recId = dbObject.ObjectID;
        this._recType = (long) dbObject.ObjectType;
        break;
      case IDBRelation _:
        IDBRelation dbRelation = (IDBRelation) dbAttributable;
        this._recId = dbRelation.RelationID;
        this._recType = (long) dbRelation.RelationType;
        break;
      default:
        throw new Exception("Invalid input parameter type");
    }
  }

  /// <summary>Object version/Relation ID</summary>
  public long RecID
  {
    get => this._recId;
    set => this._recId = value;
  }

  /// <summary>Object /Relation type ID</summary>
  public long RecType
  {
    get => this._recType;
    set => this._recType = value;
  }
}
