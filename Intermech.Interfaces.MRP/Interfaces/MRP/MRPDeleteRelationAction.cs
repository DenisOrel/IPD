// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPDeleteRelationAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Действие, позволяющее удалить связь</summary>
public class MRPDeleteRelationAction : 
  MRPBaseAction,
  IMRPRelationRef,
  IMRPGuidItem,
  IMRPTypedItem,
  IMRPUpdateableItemRef,
  IMRPContext
{
  /// <summary>Описание родительского объекта</summary>
  protected IMRPObjectRef projID;
  /// <summary>Тип удаляемой связи</summary>
  protected int relTypeID;
  /// <summary>Идентификатор удаляемой связи</summary>
  protected long prjLinkID;
  /// <summary>Глобальный уникальный идентификатор связи</summary>
  protected Guid guid;

  /// <summary>Создать действие, позволяющее удалить связь</summary>
  /// <param name="services">Контейнер сервисов (контест MRP)</param>
  /// <param name="projID">Описание родительского объекта</param>
  /// <param name="guid">Глобальный уникальный идентификатор связи</param>
  /// <param name="relTypeID">Тип удаляемой связи</param>
  public MRPDeleteRelationAction(
    IServiceProvider services,
    IMRPObjectRef projID,
    Guid guid,
    int relTypeID)
    : base(services)
  {
    if (projID == null)
      throw new ArgumentNullException(nameof (projID));
    if (guid.Equals(Guid.Empty))
      throw new ArgumentNullException(nameof (guid));
    if (relTypeID == -1)
      throw new ArgumentException();
    this.projID = projID;
    this.guid = guid;
    this.relTypeID = relTypeID;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      MRPNavigatorEventsRef service1 = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
      IMRPCheckInObjectsRef service2 = this.Services.GetService(typeof (IMRPCheckInObjectsRef)) as IMRPCheckInObjectsRef;
      bool flag = false;
      if (this.projID.ObjectID >= 0L)
      {
        if (contextSession.GetObject(this.projID.ObjectID).ObjectModifyMode == ObjectModifyModes.Checkout)
          new MRPCheckoutAction((IServiceProvider) this.services, this.projID, true).Execute();
        flag = this.projID.ObjectID < 0L;
      }
      IDBRelation relation = contextSession.GetRelation(this.Guid, this.ProjectID, true);
      this.prjLinkID = relation.RelationID;
      relation.Delete(512L /*0x0200*/);
      service1?.AddDeletedRelation(relation.RelationID, relation.RelationType);
      if (!flag)
        return;
      if (service2 != null)
        service2.Add(this.projID);
      else
        new MRPCheckInAction(this.Services, this.projID, true).Execute();
    }
  }

  /// <summary>Удаляемая связь является существующей</summary>
  public virtual bool IsNewRelation
  {
    [DebuggerStepThrough] get => false;
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

  /// <summary>Тип удаляемой связи</summary>
  public virtual int TypeID
  {
    [DebuggerStepThrough] get => this.relTypeID;
  }

  /// <summary>Заменить идентификатор связи на новое значение</summary>
  /// <param name="newItemID">Новый идентификатор связи</param>
  public virtual void UpdateItemID(long newItemID) => this.prjLinkID = newItemID;
}
