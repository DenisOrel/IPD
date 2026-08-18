// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Table.eTableCollection
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Interfaces;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Expert.Table;

/// <summary>Класс-описатель коллекции таблиц</summary>
[Serializable]
public class eTableCollection : ISerializable
{
  private eTable[] _tables;

  /// <summary>Конструктор</summary>
  /// <param name="tables">Список таблицы</param>
  public eTableCollection(eTable[] tables) => this._tables = tables;

  /// <summary>Список таблиц</summary>
  public eTable[] Tables
  {
    get => this._tables;
    set => this._tables = value;
  }

  /// <summary>десериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected eTableCollection(SerializationInfo info, StreamingContext context)
  {
    int int32 = info.GetInt32("Count");
    this._tables = new eTable[int32];
    for (int index = 0; index < int32; ++index)
      this._tables[index] = info.GetValue(index.ToString(), typeof (eTable)) as eTable;
  }

  /// <summary>сериализация</summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    info.AddValue("Count", this._tables.Length);
    for (int index = 0; index < this._tables.Length; ++index)
      info.AddValue(index.ToString(), (object) this._tables[index]);
  }

  public bool PerformAttrCombine(
    IDBAttributeType fromAttribute,
    IDBAttributeType toAttribute,
    IUserSession session)
  {
    bool flag = false;
    if (this._tables != null)
    {
      foreach (eTable table in this._tables)
        flag = table.PerformAttrCombine(fromAttribute, toAttribute, session) | flag;
    }
    return flag;
  }
}
