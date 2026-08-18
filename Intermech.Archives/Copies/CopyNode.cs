// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.CopyNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Archives.Common;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Часть элемента из пространства навигации, отвечающая за копии,
/// созданные для выбранного документа
/// </summary>
public class CopyNode : ObjectsPart
{
  /// <summary>
  /// Конструктор, позволяющий указать, для какого документа ищем копии
  /// </summary>
  /// <param name="docObjectID">Идентификатор версии документа</param>
  /// <param name="services">Контейнер сервисов</param>
  public CopyNode(long docObjectID, IServiceProvider services)
    : base(ConstsHolder.CopyOfDocumentID, CopyNode.GetConditions(docObjectID), services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий указать, для какого документа ищем копии
  /// </summary>
  /// <param name="docObjectID">Идентификатор версии документа</param>
  /// <param name="conditionsProvider">Провайдер условий выбора копий документов</param>
  /// <param name="services">Контейнер сервисов</param>
  public CopyNode(
    long docObjectID,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(ConstsHolder.CopyOfDocumentID, new ConditionStructure[1]
    {
      CopyNode.GetConditions(docObjectID)
    }, conditionsProvider, services)
  {
  }

  /// <summary>
  /// Создать описание корневого узла на основании данных, полученных из запроса
  /// </summary>
  /// <param name="fieldValues">Значения атрибутов</param>
  /// <param name="adapter">Преобразователь</param>
  /// <returns>Описание корневого узла</returns>
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string str1 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    long int64_6 = fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) ConstsHolder.OriginalObjectVersionID, AttributeSourceTypes.Object))] != DBNull.Value ? Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) ConstsHolder.OriginalObjectVersionID, AttributeSourceTypes.Object))]) : 0L;
    long int64_7 = fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) ConstsHolder.OriginalObjectID, AttributeSourceTypes.Object))] != DBNull.Value ? Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) ConstsHolder.OriginalObjectID, AttributeSourceTypes.Object))]) : 0L;
    string str2 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)] == DBNull.Value ? string.Empty : Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    long int64_8 = fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) ConstsHolder.AlbumSubscriberID, AttributeSourceTypes.Object))] != DBNull.Value ? Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) new NodeColumnID((object) ConstsHolder.AlbumSubscriberID, AttributeSourceTypes.Object))]) : 0L;
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    int lcStepID = int32_2;
    string caption = str1;
    long owner = int64_4;
    string siteID = str2;
    Guid empty = Guid.Empty;
    long modificationID = int64_5;
    return (INodeID) new CopyNodeID(new CreateObjectNodeParams(int32_1, objId, id, checkedOutBy, -1L, lcStepID, caption, -1, owner, 0L, ObjectFiltrationState.fsNonVersionable, 0L, 0L, siteID, -1L, empty, modificationID), int64_7, int64_6, int64_8);
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде.
  /// Используется только в том случае, если
  /// для данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetDefaultColumns()
  {
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    return new NodeColumnCollection()
    {
      service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID),
      service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION),
      service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.IndexOfCopyID),
      service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.OriginalObjectVersionID),
      service.CreateColumn(ConstsHolder.CopySchemeName, (object) ConstsHolder.AlbumSubscriberID),
      service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.RecipientID),
      service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.ReceiptDateID),
      service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.WhoReturnID),
      service.CreateColumn(Intermech.Navigator.Consts.CurrentObjectColumnSchemeGuid, (object) ConstsHolder.ReturnDateID),
      service.CreateColumn(Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid, (object) ObligatoryObjectAttributes.F_LC_STEP)
    };
  }

  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection supportedColumns = new NodeColumnCollection();
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    supportedColumns.AddRange((IEnumerable<NodeColumn>) base.GetSupportedColumns(ColumnSetName));
    return supportedColumns;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="column"></param>
  /// <returns></returns>
  public override object MapColumnToField(NodeColumn column)
  {
    return column.SchemeGuid == ConstsHolder.CopySchemeName ? (object) new NodeColumnID(column.ID, AttributeSourceTypes.Object) : base.MapColumnToField(column);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="conditions"></param>
  /// <returns></returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    IServiceProvider services = this.Owner is IContextAware owner ? owner.Services : (IServiceProvider) null;
    IObjectTypeNodeOptionsHolder service = services != null ? services.GetService(typeof (IObjectTypeNodeOptionsHolder)) as IObjectTypeNodeOptionsHolder : (IObjectTypeNodeOptionsHolder) null;
    ObjectTypeNodeOptions objectTypeNodeOptions = ObjectTypeNodeOptions.None;
    if (service != null)
      objectTypeNodeOptions = service.Options;
    return (objectTypeNodeOptions & ObjectTypeNodeOptions.EmptyQuery) == ObjectTypeNodeOptions.EmptyQuery ? (INodeQuery) null : (INodeQuery) new CopyObjectsQuery((INodeQuerySupport) this, this.objTypeID, conditions, services);
  }

  /// <summary>
  /// Получить список служебных полей (которые загружаются в узел независимо от настройки вида)
  /// </summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    specialFields.Add((object) new NodeColumnID((object) ConstsHolder.AlbumSubscriberID, AttributeSourceTypes.Object));
    specialFields.Add((object) new NodeColumnID((object) ConstsHolder.OriginalObjectVersionID, AttributeSourceTypes.Object));
    specialFields.Add((object) new NodeColumnID((object) ConstsHolder.OriginalObjectID, AttributeSourceTypes.Object));
    return specialFields;
  }

  /// <summary>
  /// Возвращает данные указанного формата для объекта базы данных с указанным
  /// идентификатором.
  /// </summary>
  /// <param name="nodeID">Унифицированный идентификатор объекта базы данных</param>
  /// <param name="dataFormat">Формат данных</param>
  /// <returns>Объект, представляющий данные указанного формата</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    CopyNodeID copyNodeId = nodeID as CopyNodeID;
    return dataFormat == typeof (ICopyNodeID) ? (object) copyNodeId : base.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Формирует и возвращает условия запроса, позволяющее получить копии для указанного документа
  /// </summary>
  /// <param name="docObjectID">версия документа, для которого ищем копии</param>
  /// <returns>Массив условий запроса к базе данных</returns>
  private static ConditionStructure GetConditions(long docObjectID)
  {
    long id = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(docObjectID).ID;
    return new ConditionStructure(ConstsHolder.OriginalObjectID, RelationalOperators.Equal, (object) id, LogicalOperators.NONE, 0, false);
  }
}
