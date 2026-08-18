// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSRelationAttributeValueBatch
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AVS;

internal class AVSRelationAttributeValueBatch
{
  private readonly Dictionary<long, List<RelationAttributeValues>> _data = new Dictionary<long, List<RelationAttributeValues>>();

  internal bool IsEmpty => this._data.Count == 0;

  /// <summary>
  /// Добавить отложенные изменения значений атрибутов связей для изделия
  /// </summary>
  /// <param name="projObjId">идентификатор изделия</param>
  /// <param name="rav">пакет изменений атрибутов связей</param>
  internal void Add(long projObjId, RelationAttributeValues rav)
  {
    if (this._data.ContainsKey(projObjId))
      this.Merge(projObjId, rav);
    else
      this._data[projObjId] = new List<RelationAttributeValues>()
      {
        rav
      };
  }

  /// <summary>
  /// Объединить пакет значений атрибутов связей с внутренним словарем данными
  /// </summary>
  /// <param name="projObjId">идентификатор изделия</param>
  /// <param name="rav">пакет значений атрибутов</param>
  private void Merge(long projObjId, RelationAttributeValues rav)
  {
    List<RelationAttributeValues> source = this._data[projObjId] ?? new List<RelationAttributeValues>();
    RelationAttributeValues relationAttributeValues = source.FirstOrDefault<RelationAttributeValues>((Func<RelationAttributeValues, bool>) (i => i.PartObjectID == rav.PartObjectID && i.RelationID == rav.RelationID));
    if (relationAttributeValues != null)
    {
      foreach (AttributeValues attributeValues in rav.Values)
      {
        AttributeValues v = attributeValues;
        if (((IEnumerable<AttributeValues>) relationAttributeValues.Values).All<AttributeValues>((Func<AttributeValues, bool>) (e => e.AttributeID != v.AttributeID)))
          relationAttributeValues.Values = ((IEnumerable<AttributeValues>) relationAttributeValues.Values).Append<AttributeValues>(v).ToArray<AttributeValues>();
        else
          ((IEnumerable<AttributeValues>) relationAttributeValues.Values).First<AttributeValues>((Func<AttributeValues, bool>) (e => e.AttributeID == v.AttributeID)).Values = v.Values;
      }
    }
    else
      source.Add(rav);
    this._data[projObjId] = source;
  }

  /// <summary>Отправить отложенные данные значений атрибутов в базу</summary>
  internal void CommitAll()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (long num in this._data.Keys.ToList<long>())
      {
        try
        {
          sessionKeeper.Session.GetObject(num)?.SetRelationsAttributes(this._data[num].ToArray());
          this._data.Remove(num);
        }
        catch (Exception ex)
        {
          this._data.Clear();
          throw;
        }
      }
    }
  }
}
