// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.CompositionView.CVTechCutButton
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Client.Core.CompositionView;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client.CompositionView;

/// <summary>
/// Перекроем стандартный класс для вставки вырезанных объектов
/// </summary>
[Serializable]
public class CVTechCutButton : CVButtonBase
{
  /// <summary>Информация о объектах и связях</summary>
  private readonly List<RelObjInfoItem> _relObjInfoItems;

  /// <summary>Constructor</summary>
  /// <param name="relObjInfoItems"></param>
  public CVTechCutButton(List<RelObjInfoItem> relObjInfoItems)
  {
    this._relObjInfoItems = relObjInfoItems;
  }

  /// <summary>Создание нового объекта по прототипу</summary>
  /// <param name="ownerObjId"></param>
  /// <param name="objectId"></param>
  /// <param name="relationHash"></param>
  /// <param name="session"></param>
  /// <param name="throwException"></param>
  /// <param name="errorString"></param>
  /// <returns></returns>
  public override IDBObject DoCreateObject(
    IDBTypedObjectID ownerObjId,
    IDBTypedObjectID objectId,
    Dictionary<int, List<cvRelationInfo>> relationHash,
    IUserSession session,
    bool throwException,
    out string errorString)
  {
    errorString = "";
    return session.GetObject(objectId.ObjectID, false);
  }

  /// <summary>Создание связи согласно параметрам</summary>
  /// <param name="relTypeId">Тип создаваемой связи</param>
  /// <param name="newRelPros">Параметры для создания связи</param>
  /// <param name="projTypedObjId">Родительский объект</param>
  /// <param name="partTypedObjId">Дочерний объект</param>
  /// <param name="session">Сессия</param>
  /// <returns></returns>
  public override IDBRelationID DoCreateRelation(
    int relTypeId,
    NewRelationProperties newRelPros,
    IDBTypedObjectID projTypedObjId,
    IDBTypedObjectID partTypedObjId,
    IUserSession session)
  {
    if (partTypedObjId == null || projTypedObjId == null || session == null)
      return (IDBRelationID) null;
    RelObjInfoItem relObjInfoItem = this._relObjInfoItems.FirstOrDefault<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => item.PartInfo.ObjectID == partTypedObjId.ObjectID));
    if (!((TypedInfoItem) relObjInfoItem != (TypedInfoItem) null))
      return (IDBRelationID) new DBRelationID(0L, 0L, -1, 0L, Guid.Empty, 0L);
    long relationId = relObjInfoItem.RelationID;
    this._relObjInfoItems.Remove(relObjInfoItem);
    IDBRelation relation = session.GetRelation(relationId, false);
    if (relation == null)
      return (IDBRelationID) new DBRelationID(0L, 0L, -1, 0L, Guid.Empty, 0L);
    if (relation.ProjID != projTypedObjId.ObjectID)
      relation.ProjID = projTypedObjId.ObjectID;
    if (newRelPros.ValuesList != null)
      relation.SetAttributesValues(newRelPros.ValuesList);
    return (IDBRelationID) new DBRelationID(relation.RelationID, relation.PartID, relation.RelationType, 0L, relation.GUID, relation.ProjID);
  }
}
