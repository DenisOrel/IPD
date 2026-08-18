// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPCreateRelationActionBase
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Базовый класс действия, позволяющего создавать связь</summary>
public abstract class MRPCreateRelationActionBase : 
  MRPBaseAction,
  IMRPRelationRef,
  IMRPGuidItem,
  IMRPTypedItem,
  IMRPUpdateableItemRef,
  IMRPContext
{
  /// <summary>Описание родительского объекта</summary>
  protected IMRPTypedObjectRef projID;
  /// <summary>Описание дочернего объекта</summary>
  protected IMRPTypedObjectRef partID;
  /// <summary>Тип создаваемой связи</summary>
  protected int relTypeID;
  /// <summary>Идентификатор связи</summary>
  protected long prjLinkID;
  /// <summary>Глобальный уникальный идентификатор связи</summary>
  protected Guid guid;
  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  protected bool isNewRelation;

  /// <summary>Создать действие, позволяющее создавать связь</summary>
  /// <param name="services">Контейнер сервисов (контест MRP)</param>
  /// <param name="projID">Описание родительского объекта</param>
  /// <param name="partID">Описание дочернего объекта</param>
  /// <param name="relTypeID">Тип создаваемой связи</param>
  public MRPCreateRelationActionBase(
    IServiceProvider services,
    IMRPTypedObjectRef projID,
    IMRPTypedObjectRef partID,
    int relTypeID)
    : base(services)
  {
    if (projID == null)
      throw new ArgumentNullException(nameof (projID));
    if (partID == null)
      throw new ArgumentNullException(nameof (partID));
    if (relTypeID == -1)
      throw new ArgumentException();
    this.projID = projID;
    this.partID = partID;
    this.relTypeID = relTypeID;
  }

  /// <summary>
  /// Создать связь между указанной версией родительского объекта и указанной версией дочернего объекта
  /// </summary>
  /// <param name="projID">Идентификатор версии родительского объекта</param>
  /// <param name="partID">Идентификатор версии дочернего объекта</param>
  /// <param name="collection">Коллекция связей</param>
  /// <returns>Описание созданной связи</returns>
  protected abstract IDBRelation CreateRelation(
    long projID,
    long partID,
    IDBRelationCollection collection);

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      long projectId = this.ProjectID;
      long objectId = this.partID.ObjectID;
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      MRPNavigatorEventsRef service1 = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
      MRPCheckInObjectsRef service2 = this.Services.GetService(typeof (MRPCheckInObjectsRef)) as MRPCheckInObjectsRef;
      IDBRelationCollection relationCollection = contextSession.GetRelationCollection(this.TypeID, MRPContextHelper.GetContextFiltration((IMRPContext) this));
      if (this.projID.ObjectID >= 0L)
      {
        new MRPCheckoutIfNeedRelAction((IServiceProvider) this.services, this.projID, this.partID, this.TypeID, true).Execute();
        if (this.projID.ObjectID < 0L && service2 != null)
          service2.Add((IMRPObjectRef) this.projID);
        projectId = this.ProjectID;
      }
      IDBRelation relation = this.CreateRelation(projectId, objectId, relationCollection);
      if (relation == null)
        return;
      this.prjLinkID = relation.RelationID;
      this.guid = relation.GUID;
      service1?.AddCreatedRelation(relation.RelationID, relation.RelationType, relation.ProjID, -1);
    }
  }

  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  public virtual bool IsNewRelation
  {
    [DebuggerStepThrough] get => this.isNewRelation;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public virtual long ProjectID
  {
    [DebuggerStepThrough] get => this.projID.ObjectID;
  }

  /// <summary>Идентификатор связи</summary>
  public virtual long PrjLinkID
  {
    [DebuggerStepThrough] get => this.prjLinkID;
  }

  /// <summary>Уникальный глобальный идентификатор связи</summary>
  public virtual Guid Guid
  {
    [DebuggerStepThrough] get => this.guid;
  }

  /// <summary>Тип созданной связи</summary>
  public virtual int TypeID
  {
    [DebuggerStepThrough] get => this.relTypeID;
  }

  /// <summary>Заменить идентификатор связи на новое значение</summary>
  /// <param name="newItemID">Новый идентификатор связи</param>
  public virtual void UpdateItemID(long newItemID) => this.prjLinkID = newItemID;
}
