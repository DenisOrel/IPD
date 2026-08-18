// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPAttachTechRouteAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Kernel.Search;
using System;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее назначить маршрут обработки указанному объекту.
/// Из состава объекта будут исключены все маршруты обработки, за исключением указанного,
/// если оно существует, либо будет создана новая связь
/// </summary>
internal sealed class MRPAttachTechRouteAction : 
  MRPBaseAction,
  IMRPAction,
  IMRPContext,
  IMRPTypedObjectRef,
  IMRPObjectRef,
  IMRPGuidItem,
  IMRPUpdateableItemRef,
  IMRPTypedItem,
  IMRPRelationRef
{
  /// <summary>Родительский тип объекта</summary>
  private IMRPTypedObjectRef projObjRef;
  /// <summary>Описание требуемого маршрута обработки</summary>
  private IMRPTypedObjectRef techObjRef;
  /// <summary>
  /// Описание созданной (найденной) связи маршрута обработки
  /// </summary>
  private IMRPRelationRef techRelRef;
  /// <summary>Коллекция колонок</summary>
  private static ColumnDescriptor[] columns;

  /// <summary>
  /// Создать действие, позволяющее назначить маршрут обработки указанному объекту.
  /// Из состава объекта будут исключены все маршруты обработки, за исключением указанного,
  /// если оно существует, либо будет создана новая связь
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="projObjRef">Родительский тип объекта</param>
  /// <param name="techObjRef">Описание требуемого маршрута обработки</param>
  public MRPAttachTechRouteAction(
    IServiceProvider services,
    IMRPTypedObjectRef projObjRef,
    IMRPTypedObjectRef techObjRef)
    : base(services)
  {
    if (projObjRef == null)
      throw new ArgumentNullException(nameof (projObjRef));
    if (techObjRef == null)
      throw new ArgumentNullException(nameof (projObjRef));
    this.projObjRef = projObjRef;
    this.techObjRef = techObjRef;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPAttachTechRouteAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.projObjRef = (IMRPTypedObjectRef) null;
    this.techObjRef = (IMRPTypedObjectRef) null;
    this.techRelRef = (IMRPRelationRef) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPAttachTechRouteAction attachTechRouteAction))
      return;
    this.projObjRef = attachTechRouteAction.projObjRef;
    this.techObjRef = attachTechRouteAction.techObjRef;
    this.techRelRef = attachTechRouteAction.techRelRef;
  }

  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectID
  {
    [DebuggerStepThrough] get => this.techObjRef == null ? 0L : this.techObjRef.ObjectID;
  }

  /// <summary>Уникальный глобальный идентификатор связи</summary>
  public Guid Guid
  {
    [DebuggerStepThrough] get => this.techRelRef == null ? Guid.Empty : this.techRelRef.Guid;
  }

  /// <summary>
  /// Обновить целочисленный идентификатор объекта на указанное значение
  /// </summary>
  /// <param name="newItemID">Новый целочисленный идентификатор объекта</param>
  public void UpdateItemID(long newItemID)
  {
    if (this.techObjRef == null)
      return;
    this.techObjRef.UpdateItemID(newItemID);
  }

  /// <summary>
  /// Является ли связь созданной (новой), либо она существующая (значение по умолчанию)
  /// </summary>
  public bool IsNewRelation
  {
    [DebuggerStepThrough] get => this.techRelRef != null && this.techRelRef.IsNewRelation;
  }

  /// <summary>Идентификатор версии родительского объекта</summary>
  public long ProjectID
  {
    [DebuggerStepThrough] get => this.techRelRef == null ? 0L : this.techRelRef.ProjectID;
  }

  /// <summary>Идентификатор связи</summary>
  public long PrjLinkID
  {
    [DebuggerStepThrough] get => this.techRelRef == null ? 0L : this.techRelRef.PrjLinkID;
  }

  /// <summary>32-битный идентификатор типа связи</summary>
  public int TypeID
  {
    [DebuggerStepThrough] get => this.techRelRef == null ? -1 : this.techRelRef.TypeID;
  }

  /// <summary>Выполнить действие</summary>
  public override void Execute() => this.Execute((IServiceProvider) null);

  /// <summary>Выполнить действие в рамках указанного контекста</summary>
  /// <param name="context">Контекст, в рамках которого выполняется действие</param>
  public override void Execute(IServiceProvider context)
  {
    if (this.projObjRef == null || this.projObjRef.ObjectID == 0L || this.techObjRef == null || this.techObjRef.ObjectID == 0L)
      return;
    using (new MRPContextFix((IMRPContext) this, context ?? this.services.AdvancedProvider))
    {
      IUserSession contextSession = MRPContextHelper.GetContextSession((IMRPContext) this);
      if (contextSession == null)
        throw new ArgumentNullException("session");
      MRPNavigatorEventsRef service = this.Services.GetService(typeof (MRPNavigatorEventsRef)) as MRPNavigatorEventsRef;
      IDBRelationCollection relationCollection = contextSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"));
      relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545");
      ConditionStructure conditionStructure = new ConditionStructure(-21, RelationalOperators.Equal, (object) this.projObjRef.ObjectID, LogicalOperators.NONE, 0, true);
      if (MRPAttachTechRouteAction.columns == null)
        MRPAttachTechRouteAction.columns = new ColumnDescriptor[3]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0),
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_PRJ_GUID, AttributeSourceTypes.Relation, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        };
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        conditionStructure
      }, MRPAttachTechRouteAction.columns);
      DataTable dataTable;
      try
      {
        dataTable = relationCollection.Select(paramSet);
      }
      catch
      {
        dataTable = (DataTable) null;
      }
      if (dataTable != null && dataTable.Rows.Count > 0)
      {
        for (int index = 0; index < dataTable.Rows.Count; ++index)
        {
          long int64Value1 = DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L);
          long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index][1], 0L);
          Guid guidValue = DataSetProcessor.GetGuidValue(dataTable.Rows[index][2], Guid.Empty);
          if (this.techRelRef == null && Math.Abs(int64Value2) == Math.Abs(this.techObjRef.ObjectID))
            this.techRelRef = (IMRPRelationRef) new MRPRelationRef(this.Services, this.projObjRef.ObjectID, int64Value1, guidValue, relationCollection.RelationTypeID, false);
          else if (this.techRelRef == null || this.techRelRef.PrjLinkID != int64Value1)
          {
            IDBRelation relation = contextSession.GetRelation(int64Value1, false);
            if (relation != null)
            {
              relation.Delete(0L);
              service?.AddDeletedRelation(int64Value1, relationCollection.RelationTypeID);
            }
          }
        }
      }
      if (this.techRelRef != null)
        return;
      bool parIsCheckedOut = false;
      this.techRelRef = MRPCreateRelationAction.CreateRelation(this.Services, contextSession, this.projObjRef, this.techObjRef, (IMRPRelationRef) null, false, MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"), out parIsCheckedOut);
      MRPFixRelationPartAction relationPartAction = new MRPFixRelationPartAction(this.Services, this.techRelRef, (IMRPObjectRef) this.techObjRef);
      if (this.techRelRef == null || this.techRelRef.PrjLinkID == 0L || service == null)
        return;
      service.AddCreatedRelation(this.techRelRef.PrjLinkID, this.techRelRef.TypeID, this.techRelRef.ProjectID, this.projObjRef.TypeID);
    }
  }
}
