// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPSyncRelationsAttrsAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее синхронизировать атрибуты двух связей
/// </summary>
public class MRPSyncRelationsAttrsAction : MRPBaseAction
{
  /// <summary>Описание исходной связи</summary>
  private IMRPRelationRef sourceRelRef;
  /// <summary>Описание связи-назначения</summary>
  private IMRPRelationRef destRelRef;
  /// <summary>
  /// Словарь-кэш списков типов атрибутов для синхронизации пар типов связей [Исходный тип связи] - [Тип связи-назначения]
  /// Ключ - пара идентификаторов типов связей [Исходная связь] - [Связь-назначение], значение - список идентификаторов типов атрибутов
  /// </summary>
  private static Dictionary<Tuple<int, int>, List<int>> toSyncAttrs = new Dictionary<Tuple<int, int>, List<int>>();

  /// <summary>
  /// Создать действие, позволяющее синхронизировать атрибуты двух связей
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="sourceRelRef">Описание исходной связи</param>
  /// <param name="destRelRef">Описание связи-назначения</param>
  public MRPSyncRelationsAttrsAction(
    IServiceProvider services,
    IMRPRelationRef sourceRelRef,
    IMRPRelationRef destRelRef)
    : base(services)
  {
    if (sourceRelRef == null)
      throw new ArgumentNullException(nameof (sourceRelRef));
    if (destRelRef == null)
      throw new ArgumentNullException(nameof (destRelRef));
    this.sourceRelRef = sourceRelRef;
    this.destRelRef = destRelRef;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPSyncRelationsAttrsAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.sourceRelRef = (IMRPRelationRef) null;
    this.destRelRef = (IMRPRelationRef) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPSyncRelationsAttrsAction relationsAttrsAction))
      return;
    this.sourceRelRef = relationsAttrsAction.sourceRelRef;
    this.destRelRef = relationsAttrsAction.destRelRef;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.sourceRelRef == null || this.sourceRelRef.PrjLinkID == 0L || this.destRelRef == null || this.destRelRef.PrjLinkID == 0L || Math.Abs(this.sourceRelRef.PrjLinkID) == Math.Abs(this.destRelRef.PrjLinkID))
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      int sourceRelType = this.sourceRelRef.TypeID;
      IDBRelation relation = contextSession.GetRelation(this.sourceRelRef.PrjLinkID, false);
      if (relation == null)
        return;
      if (sourceRelType == -1)
        sourceRelType = relation.RelationType;
      int destRelType = this.destRelRef.TypeID;
      if (destRelType == -1)
        destRelType = contextSession.GetRelation(this.destRelRef.PrjLinkID).RelationType;
      List<int> attrsToSync = this.GetAttrsToSync(contextSession, sourceRelType, destRelType);
      if (attrsToSync.Count == 0)
        return;
      List<AttributeValues> attributeValuesList = new List<AttributeValues>(attrsToSync.Count);
      for (int index = 0; index < attrsToSync.Count; ++index)
      {
        IDBAttribute attributeById = relation.GetAttributeByID(attrsToSync[index]);
        if (attributeById != null)
        {
          IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(attrsToSync[index]);
          AttributeValues attributeValues = new AttributeValues(attributeById.AttributeID, attributeById.DataType, attributeType.MultiValueMode, attributeById.Values);
          attributeValuesList.Add(attributeValues);
        }
      }
      if (attributeValuesList.Count <= 0)
        return;
      new MRPWriteRelationAttributesAction(this.Services, this.destRelRef, attributeValuesList.ToArray()).Execute();
    }
  }

  /// <summary>
  /// Метод позволяет получить список типов атрибутов для синхронизации между двумя типами связей
  /// </summary>
  /// <param name="session">Сессия</param>
  /// <param name="sourceRelType">Идентификатор типа исходной связи</param>
  /// <param name="destRelType">Идентификатор типа связи-назначения</param>
  /// <returns>Список типов атрибутов для синхронизации</returns>
  private List<int> GetAttrsToSync(IUserSession session, int sourceRelType, int destRelType)
  {
    if (session == null || sourceRelType == -1 || destRelType == -1)
      return new List<int>();
    Tuple<int, int> key = new Tuple<int, int>(sourceRelType, destRelType);
    lock (MRPSyncRelationsAttrsAction.toSyncAttrs)
    {
      if (MRPSyncRelationsAttrsAction.toSyncAttrs.ContainsKey(key))
        return MRPSyncRelationsAttrsAction.toSyncAttrs[key];
    }
    List<IMSAttribute4RelationType> relationTypeList1 = MetaDataHelper.GetAttribute4RelationTypeList(sourceRelType);
    List<IMSAttribute4RelationType> relationTypeList2 = MetaDataHelper.GetAttribute4RelationTypeList(destRelType);
    relationTypeList1.RemoveAll((Predicate<IMSAttribute4RelationType>) (attrType => attrType.AttributeID < 0));
    relationTypeList2.RemoveAll((Predicate<IMSAttribute4RelationType>) (attrType => attrType.AttributeID < 0));
    Dictionary<int, IMSAttribute4RelationType> sourceAttrsDict = new Dictionary<int, IMSAttribute4RelationType>(relationTypeList1.Count);
    relationTypeList1.ForEach((Action<IMSAttribute4RelationType>) (attrType => sourceAttrsDict[attrType.AttributeID] = attrType));
    relationTypeList2.RemoveAll((Predicate<IMSAttribute4RelationType>) (attrType => !sourceAttrsDict.ContainsKey(attrType.AttributeID) || attrType.Computed != 0));
    List<int> attrsToSync = relationTypeList2.ConvertAll<int>((Converter<IMSAttribute4RelationType, int>) (attrType => attrType.AttributeID));
    lock (MRPSyncRelationsAttrsAction.toSyncAttrs)
      MRPSyncRelationsAttrsAction.toSyncAttrs[key] = attrsToSync;
    return attrsToSync;
  }
}
