// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCreateRelationAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее создавать связь</summary>
/// <summary>Создать действие, позволяющее создавать связь</summary>
/// <param name="services">Контейнер сервисов (контест MRP)</param>
/// <param name="projID">Описание родительского объекта</param>
/// <param name="partID">Описание дочернего объекта</param>
/// <param name="relTypeID">Тип создаваемой связи</param>
public class MRPCreateRelationAction(
  IServiceProvider services,
  IMRPTypedObjectRef projID,
  IMRPTypedObjectRef partID,
  int relTypeID) : MRPCreateRelationActionBase(services, projID, partID, relTypeID)
{
  /// <summary>
  /// Создать связь между указанной версией родительского объекта и указанной версией дочернего объекта
  /// </summary>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="partID">Идентификатор версии дочернего объекта</param>
  /// <param name="collection">Коллекция связей</param>
  /// <returns>Описание созданной связи</returns>
  protected override IDBRelation CreateRelation(
    long projID,
    long partID,
    IDBRelationCollection collection)
  {
    if (collection == null)
      throw new ArgumentNullException(nameof (collection));
    this.isNewRelation = true;
    return collection.Create(projID, partID);
  }

  /// <summary>Создать связь между указанными объектами</summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="session">Сессия</param>
  /// <param name="projObj">Родительский объект</param>
  /// <param name="partObj">Дочерний объект</param>
  /// <param name="sourceRel">Исходная связь</param>
  /// <param name="parIsCheckedOut">Был ли взят на изменение родительский объект</param>
  /// <param name="relTypeID">Тип создаваемой связи</param>
  /// <param name="ifNeedOnly">Создавать связь только если она не существует, иначе возвращать существующую</param>
  /// <returns>Описание созданной связи</returns>
  public static IMRPRelationRef CreateRelation(
    IServiceProvider services,
    IUserSession session,
    IMRPTypedObjectRef projObj,
    IMRPTypedObjectRef partObj,
    IMRPRelationRef sourceRel,
    bool ifNeedOnly,
    int relTypeID,
    out bool parIsCheckedOut)
  {
    if (services == null)
      throw new ArgumentNullException(nameof (services));
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (projObj == null)
      throw new ArgumentNullException(nameof (projObj));
    if (partObj == null)
      throw new ArgumentNullException(nameof (partObj));
    parIsCheckedOut = false;
    if (projObj.ObjectID >= 0L)
    {
      new MRPCheckoutIfNeedRelAction(services, projObj, partObj, relTypeID, true).Execute();
      parIsCheckedOut = projObj.ObjectID < 0L;
    }
    IMRPRelationRef destRelRef = ifNeedOnly ? (IMRPRelationRef) new MRPCreateRelationIfNeedAction(services, (IMRPTypedObjectRef) new MRPTypedObjectRef(services, projObj.ObjectID, projObj.Guid, projObj.TypeID), partObj, relTypeID, sourceRel.Guid) : (IMRPRelationRef) new MRPCreateRelationAction(services, (IMRPTypedObjectRef) new MRPTypedObjectRef(services, projObj.ObjectID, projObj.Guid, projObj.TypeID), partObj, relTypeID);
    (destRelRef as IMRPAction).Execute();
    if (sourceRel != null && destRelRef.IsNewRelation)
      new MRPSyncRelationsAttrsAction(services, sourceRel, destRelRef).Execute();
    return destRelRef;
  }
}
