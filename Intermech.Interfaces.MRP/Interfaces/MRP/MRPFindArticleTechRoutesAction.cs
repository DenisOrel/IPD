// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPFindArticleTechRoutesAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее отыскать маршруты обработки для изделия, связанного с указанным экземпляром/партией
/// </summary>
internal sealed class MRPFindArticleTechRoutesAction : MRPBaseAction, IMRPAction, IMRPContext
{
  /// <summary>
  /// Описание экземпляра/партии, для которого требуется отыскать маршруты обработки
  /// </summary>
  private IMRPObjectRef instanceObjRef;
  /// <summary>Описание найденного изделия</summary>
  public IMRPTypedObjectRef ArticleObjRef;
  /// <summary>
  /// Список маршрутов обработки (идентификаторы версий объектов)
  /// </summary>
  public List<long> TechRoutes = new List<long>();
  /// <summary>Коллекция колонок</summary>
  private static ColumnDescriptor[] columns;

  /// <summary>
  /// Создать действие, отыскать маршруты обработки для изделия, связанного с указанным экземпляром/партией
  /// </summary>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="instanceObjRef">Описание экземпляра/партии, для которого требуется отыскать маршруты обработки</param>
  public MRPFindArticleTechRoutesAction(IServiceProvider services, IMRPObjectRef instanceObjRef)
    : base(services)
  {
    this.instanceObjRef = instanceObjRef != null ? instanceObjRef : throw new ArgumentNullException(nameof (instanceObjRef));
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPFindArticleTechRoutesAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.instanceObjRef = (IMRPObjectRef) null;
    this.ArticleObjRef = (IMRPTypedObjectRef) null;
    this.TechRoutes = new List<long>();
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPFindArticleTechRoutesAction techRoutesAction))
      return;
    this.instanceObjRef = techRoutesAction.instanceObjRef;
    this.ArticleObjRef = techRoutesAction.ArticleObjRef;
    this.TechRoutes = techRoutesAction.TechRoutes != null ? new List<long>((IEnumerable<long>) techRoutesAction.TechRoutes) : new List<long>();
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
      this.ArticleObjRef = (IMRPTypedObjectRef) new MRPFindArticle4InstanceAction(this.Services, this.instanceObjRef);
      (this.ArticleObjRef as MRPFindArticle4InstanceAction).Execute();
      if (this.ArticleObjRef.ObjectID == 0L)
        return;
      IDBRelationCollection relationCollection = contextSession.GetRelationCollection(MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"));
      relationCollection.ObjectTypeID = MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545");
      ConditionStructure conditionStructure = new ConditionStructure(-21, RelationalOperators.Equal, (object) this.ArticleObjRef.ObjectID, LogicalOperators.NONE, 0, true);
      if (MRPFindArticleTechRoutesAction.columns == null)
        MRPFindArticleTechRoutesAction.columns = new ColumnDescriptor[1]
        {
          new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0)
        };
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        conditionStructure
      }, MRPFindArticleTechRoutesAction.columns);
      DataTable dataTable;
      try
      {
        dataTable = relationCollection.Select(paramSet);
      }
      catch
      {
        dataTable = (DataTable) null;
      }
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long int64Value = DataSetProcessor.GetInt64Value(dataTable.Rows[index][0], 0L);
        if (this.TechRoutes.IndexOf(int64Value) < 0)
          this.TechRoutes.Add(int64Value);
      }
    }
  }
}
