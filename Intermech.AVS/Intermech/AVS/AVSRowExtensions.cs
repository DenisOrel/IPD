// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AVSRowExtensions
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces;
using Intermech.Interfaces.AVS;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AVS;

public static class AVSRowExtensions
{
  /// <summary>
  /// Определяет, содержит ли изделие в текущей строке другое изделие в качестве материала
  /// </summary>
  public static bool HasPartAsMaterial(this AVSRow row, out long partMaterialObjectID)
  {
    partMaterialObjectID = -1L;
    if (row.ObjectId.IsUndefinedId())
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBAttribute attributeById = sessionKeeper.Session.GetObjectActual(row.ObjectId, false)?.GetAttributeByID(AvsIDCache.Attr_Material);
      if (attributeById == null || attributeById.Value == null || attributeById.Value == DBNull.Value)
        return false;
      partMaterialObjectID = Convert.ToInt64(attributeById.Value);
      return AVSDocument.IsParentObjectType(AvsIDCache.ObjType_Product, sessionKeeper.Session.GetObjectInfo(partMaterialObjectID).ObjectTypeID);
    }
  }

  /// <summary>Определяет, является ли строка записью о заготовке</summary>
  public static bool IsZagotovka(this AVSRow row)
  {
    return row.HasRelation && row.RelType == AvsIDCache.Relation_Zagotovka;
  }

  /// <summary>Удалить запись о заготовке из документа</summary>
  public static List<KeyValuePair<long, RelInfo>> RemoveZagotovka(this AVSRow row)
  {
    List<KeyValuePair<long, RelInfo>> keyValuePairList = new List<KeyValuePair<long, RelInfo>>();
    AVSDocument avsDocument = row.avsDocument;
    if (avsDocument.ReadOnly)
      return keyValuePairList;
    foreach (AVSRow row1 in avsDocument.GetAllRows(true, false).Where<AVSRow>((Func<AVSRow, bool>) (r => r.IsZagotovka())).ToList<AVSRow>())
    {
      long fieldInt64Value = row1.GetFieldInt64Value(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), 0, (List<RelationAttributeValuesCache>) null, false);
      if (fieldInt64Value.IsDefinedId() && fieldInt64Value == Math.Abs(row.ObjectId) && row1.Section != null && row.Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.projInfo.Guid)).Distinct<Guid>().OrderBy<Guid, Guid>((Func<Guid, Guid>) (i => i)).SequenceEqual<Guid>((IEnumerable<Guid>) row1.Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.projInfo.Guid)).Distinct<Guid>().OrderBy<Guid, Guid>((Func<Guid, Guid>) (i => i))))
      {
        keyValuePairList = row1.Section.RemoveRow(row1, true, true, true, true, false);
        break;
      }
    }
    return keyValuePairList;
  }

  /// <summary>
  /// Найти строку дорабатываемой детали по строке заготовки в документе
  /// </summary>
  public static AVSRow GetPartForDraft(this AVSRow draftRow)
  {
    AVSDocument avsDocument = draftRow.avsDocument;
    long num = draftRow.GetFieldInt64Value(new AvsRowAttributeInfo(true, AvsIDCache.Attr_ArticleID), 0, (List<RelationAttributeValuesCache>) null, false);
    if (num.IsUndefinedId())
    {
      AVSRow avsRow = (AVSRow) null;
      if (draftRow.IsZagotovka())
      {
        AvsRowAttributeInfo attr_Material = new AvsRowAttributeInfo(false, AvsIDCache.Attr_Material);
        avsRow = draftRow.avsDocument.GetRows(true, true).FirstOrDefault<AVSRow>((Func<AVSRow, bool>) (r => Math.Abs(r.GetFieldInt64Value(attr_Material, -1, (List<RelationAttributeValuesCache>) null, false)) == Math.Abs(draftRow.ObjectId)));
      }
      if (avsRow == null)
        return (AVSRow) null;
      num = avsRow.ObjectId;
    }
    List<AVSRow> avsRowsByObjectId = avsDocument.GetAvsRowsByObjectId(num);
    IOrderedEnumerable<Guid> second = draftRow.Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.projInfo.Guid)).Distinct<Guid>().OrderBy<Guid, Guid>((Func<Guid, Guid>) (i => i));
    foreach (AVSRow partForDraft in avsRowsByObjectId)
    {
      if (partForDraft.HasRelation)
      {
        IOrderedEnumerable<Guid> first = partForDraft.Relations.Select<RelationAttributeValuesCache, Guid>((Func<RelationAttributeValuesCache, Guid>) (r => r.projInfo.Guid)).Distinct<Guid>().OrderBy<Guid, Guid>((Func<Guid, Guid>) (i => i));
        if (first.SequenceEqual<Guid>((IEnumerable<Guid>) second) || avsDocument.IsFormB && !first.Except<Guid>((IEnumerable<Guid>) second).Any<Guid>())
          return partForDraft;
      }
    }
    return (AVSRow) null;
  }
}
