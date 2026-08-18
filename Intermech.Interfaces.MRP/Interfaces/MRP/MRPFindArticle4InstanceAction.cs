// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPFindArticle4InstanceAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее отыскать исходное изделие для указанного экземпляра/партии
/// </summary>
internal sealed class MRPFindArticle4InstanceAction : 
  MRPBaseAction,
  IMRPAction,
  IMRPContext,
  IMRPTypedObjectRef,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPTypedItem
{
  /// <summary>
  /// Описание экземпляра/партии, для которого требуется отыскать исходное изделие
  /// </summary>
  private IMRPObjectRef instanceObjRef;
  /// <summary>Описание найденного изделия</summary>
  private IMRPTypedObjectRef articleObjRef;

  /// <summary>
  /// Создать действие, позволяющее отыскать исходное изделие для указанного экземпляра/партии
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="instanceObjRef">Действие, позволяющее отыскать исходное изделие для указанного экземпляра/партии</param>
  public MRPFindArticle4InstanceAction(IServiceProvider services, IMRPObjectRef instanceObjRef)
    : base(services)
  {
    this.instanceObjRef = instanceObjRef != null ? instanceObjRef : throw new ArgumentNullException(nameof (instanceObjRef));
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPFindArticle4InstanceAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.instanceObjRef = (IMRPObjectRef) null;
    this.articleObjRef = (IMRPTypedObjectRef) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPFindArticle4InstanceAction article4InstanceAction))
      return;
    this.instanceObjRef = article4InstanceAction.instanceObjRef;
    this.articleObjRef = article4InstanceAction.articleObjRef;
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this.articleObjRef == null ? 0L : this.articleObjRef.ObjectID;
  }

  /// <summary>Уникальный глобальный идентификатор версии объекта</summary>
  public Guid Guid
  {
    [DebuggerStepThrough] get => this.articleObjRef == null ? Guid.Empty : this.articleObjRef.Guid;
  }

  /// <summary>
  /// Обновить целочисленный идентификатор объекта на указанное значение
  /// </summary>
  /// <param name="newItemID">Новый целочисленный идентификатор объекта</param>
  public void UpdateItemID(long newItemID)
  {
    if (this.articleObjRef == null)
      return;
    this.articleObjRef.UpdateItemID(newItemID);
  }

  /// <summary>32-битный идентификатор типа найденного объекта</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this.articleObjRef == null ? -1 : this.articleObjRef.TypeID;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.instanceObjRef == null || this.instanceObjRef.ObjectID == 0L)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      IDBAttribute attributeById = contextSession.GetObject(this.instanceObjRef.ObjectID, false)?.GetAttributeByID(MetaDataHelper.GetAttributeTypeID("cad00622-306c-11d8-b4e9-00304f19f545"));
      IDBObject objectActualCopy = contextSession.GetObjectActualCopy(Math.Abs(attributeById != null ? DataSetProcessor.GetInt64Value(attributeById.Value, 0L) : 0L), false);
      if (objectActualCopy == null)
        return;
      this.articleObjRef = (IMRPTypedObjectRef) new MRPTypedObjectRef((IServiceProvider) this.services, objectActualCopy.ObjectID, objectActualCopy.ObjectGUID, objectActualCopy.ObjectType);
    }
  }
}
