// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.MRPAddToContextAction
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

using Intermech.Interfaces.Contexts;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// Действие, позволяющее добавить одну или несколько версий объектов
/// в контекст редактирования
/// </summary>
public class MRPAddToContextAction : MRPBaseAction
{
  /// <summary>Идентификатор версии контекста редектирования</summary>
  private long contextID;
  /// <summary>
  /// Список идентификаторов версий объектов, которые требуется добавить в контекст редактирования
  /// </summary>
  private IList<long> objectIDs;
  /// <summary>Колонки для работы с контекстом редактирования</summary>
  private static readonly object[] objectColumns = new object[3]
  {
    (object) ObligatoryObjectAttributes.F_OBJECT_ID,
    (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
    (object) ObligatoryObjectAttributes.F_ID
  };

  /// <summary>
  /// Создать действие, позволяющее добавить одну или несколько версий объектов
  /// в контекст редактирования
  /// </summary>
  /// <param name="services">Контейнер сервисов (MRP)</param>
  /// <param name="contextID">Идентификатор версии контекста редектирования</param>
  /// <param name="objectIDs">Список идентификаторов версий объектов, которые требуется добавить в контекст редактирования</param>
  public MRPAddToContextAction(IServiceProvider services, long contextID, IList<long> objectIDs)
    : base(services)
  {
    if (contextID == 0L)
      throw new ArgumentException();
    if (objectIDs == null)
      throw new ArgumentNullException(nameof (objectIDs));
    this.contextID = contextID;
    this.objectIDs = objectIDs;
  }

  /// <summary>
  /// Создать экземпляр класса, заполнить его информацией из указанного объекта-источника
  /// </summary>
  /// <param name="source">Объект-источник</param>
  public MRPAddToContextAction(object source)
    : base((IServiceProvider) null)
  {
    this.Assign(source);
  }

  /// <summary>Очистить поля класса</summary>
  public override void Clear()
  {
    base.Clear();
    this.contextID = 0L;
    this.objectIDs = (IList<long>) null;
  }

  /// <summary>Скопировать в текущий объект поля из другого объекта.</summary>
  /// <param name="source">Объект-источник</param>
  public override void Assign(object source)
  {
    if (this == source)
      return;
    base.Assign(source);
    if (!(source is MRPAddToContextAction addToContextAction))
      return;
    this.contextID = addToContextAction.contextID;
    this.objectIDs = (IList<long>) new List<long>((IEnumerable<long>) addToContextAction.objectIDs);
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
      if (!(contextSession.GetCustomService(typeof (IDBEditingContextsService)) is IDBEditingContextsService customService))
        throw new ArgumentNullException("IDBEditingContextsService");
      EditingContextsObjectContainer editingContextsObject = customService.GetEditingContextsObject((object) contextSession.SessionGUID, this.contextID, false, false);
      IDBObjectCollection objectCollection = contextSession.GetObjectCollection(-1);
      objectCollection.LocalTypesMode = true;
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) new List<long>((IEnumerable<long>) this.objectIDs).ToArray(), LogicalOperators.NONE, 0, true)
      }, MRPAddToContextAction.objectColumns);
      DataTable dataTable = objectCollection.Select(paramSet);
      List<long> versionIDs = new List<long>(dataTable.Rows.Count);
      List<long> fIDs = new List<long>(dataTable.Rows.Count);
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 0, 0L);
        int int32Value = DataSetProcessor.GetInt32Value(dataTable.Rows[index], 1, -1);
        long int64Value2 = DataSetProcessor.GetInt64Value(dataTable.Rows[index], 2, 0L);
        IMSObjectType objectType = MetaDataHelper.GetObjectType(int32Value);
        bool flag = objectType != null && MetaDataHelper.IsObjectTypeChildOf(int32Value, MetaDataHelper.GetObjectTypeID("cad00348-306c-11d8-b4e9-00304f19f545"));
        int num = editingContextsObject.SimpleContext ? 1 : 0;
        if ((editingContextsObject.ExistsObject(int64Value2) && !editingContextsObject.ExistsLinkedVersion(int64Value1) || editingContextsObject.ExistsVersion(int64Value1, false) || MetaDataHelper.IsObjectTypeEditingContext(int32Value) || !flag && (objectType == null || objectType.VersionsMode != ObjectVersionModes.MultiVersion)) && versionIDs.IndexOf(int64Value1) < 0)
        {
          versionIDs.Add(int64Value1);
          fIDs.Add(int64Value2);
        }
      }
      if (versionIDs.Count <= 0)
        return;
      customService.AddToContext((object) contextSession.SessionGUID, editingContextsObject.ContextID, editingContextsObject.ModificationID, (IList<long>) fIDs, (IList<long>) versionIDs, !editingContextsObject.SimpleContext, true);
    }
  }
}
