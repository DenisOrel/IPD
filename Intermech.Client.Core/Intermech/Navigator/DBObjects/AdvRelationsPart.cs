
// Type: Intermech.Navigator.DBObjects.AdvRelationsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком объектов,
/// входящих в состав указанного объекта. Для чтения объектов используется
/// коллекция связей объектов, что позволяет получать значения как атрибутов
/// объектов, так и атрибутов связей.
/// </summary>
public class AdvRelationsPart : RelatedObjectsPart
{
  /// <summary>Составные описания дополнительных атрибутов</summary>
  protected List<NodeColumnID> ncAdvAttributes;
  /// <summary>
  /// Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно
  /// указать константу Intermech.SystemGUIDs.filtrationAllVersions.
  /// </summary>
  protected string _filtrationOwnerID;
  /// <summary>Контексты, в рамках которых будет получен состав</summary>
  protected List<long> _contexts;
  /// <summary>
  /// Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок
  /// </summary>
  private List<int> _attributes = new List<int>();

  /// <summary>
  /// Конструктор части, позволяющий указать обрабатываемый объект и роль связанных
  /// с ним объектов. Созданная часть будет возвращать все объекты из
  /// состава/применяемости обрабатываемого объекта, связанные с ним указанным типом связи.
  /// </summary>
  /// <param name="projObjTypeID">Идентификатор типа родительского объекта.</param>
  /// <param name="projID">Идентификатор версии родительского объекта.</param>
  /// <param name="relationTypeID">Тип связи, по которому надо получить состав</param>
  /// <param name="filtrationOwnerID">Уникальный ключ настроек фильтрации состава.
  /// Если фильтрация состава не требуется, можно указать константу Intermech.SystemGUIDs.filtrationAllVersions.</param>
  /// <param name="contexts">Список контекстов, в рамках которых будет считываться состав</param>
  /// <param name="attributes">Список дополнительных идентификаторов атрибутов, которые будут загружаться в узел независимо от видимых колонок</param>
  /// <param name="services">Контейнер сервисов</param>
  public AdvRelationsPart(
    int projObjTypeID,
    long projID,
    int relationTypeID,
    string filtrationOwnerID,
    List<long> contexts,
    List<int> attributes,
    IServiceProvider services)
    : base(projObjTypeID, projID, RelatedObjectsRole.Composition, relationTypeID, services)
  {
    this._filtrationOwnerID = filtrationOwnerID;
    this._contexts = contexts;
    this.ncAdvAttributes = attributes == null || attributes.Count <= 0 ? (List<NodeColumnID>) null : new List<NodeColumnID>(attributes.Count);
    this._attributes = attributes;
    if (attributes == null)
      return;
    for (int index = 0; index < attributes.Count; ++index)
      this.ncAdvAttributes.Add(new NodeColumnID((object) attributes[index], AttributeSourceTypes.Relation));
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
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJLINK_ID)]);
    string caption = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]);
    long int64_7 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)]);
    string siteID = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    Guid guidValue = DataSetProcessor.GetGuidValue(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJ_GUID)], Guid.Empty);
    long int64_8 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    long int64_9 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)] == DBNull.Value ? 0L : Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)]);
    object[] values = this.ncAdvAttributes != null ? new object[this.ncAdvAttributes.Count] : (object[]) null;
    if (this.ncAdvAttributes != null)
    {
      for (int index = 0; index < this.ncAdvAttributes.Count; ++index)
      {
        int fieldIndex = adapter.GetFieldIndex((object) this.ncAdvAttributes[index]);
        values[index] = fieldIndex >= 0 ? fieldValues[fieldIndex] : (object) null;
        values[index] = values[index] != DBNull.Value ? values[index] : (object) null;
      }
    }
    return (INodeID) new AdvRelationsNodeID((CreateObjectNodeParams) new AdvCreateObjectNodeParams(int32_1, int64_1, int64_2, int64_4, int64_3, int32_2, caption, this._relTypeID, int64_5, int64_9, ObjectFiltrationState.fsNotRequired, int64_6, int64_7, siteID, this._filtrationOwnerID, this._contexts, this._objTypeID, this._objID, guidValue, int64_8, this._attributes, values));
  }

  /// <summary>
  /// Возвращает коллекцию колонок, которые должны отображаться в гриде
  /// для данного элемента. Используется только в том случае, если для
  /// данного элемента нет сохраненных в конфиграции пользователя
  /// настроек отображения грида.
  /// </summary>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetDefaultColumns()
  {
    NodeColumnCollection defaultColumns = new NodeColumnCollection();
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_TYPE, NodeColumnSortOrder.None, -1), 90);
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.F_OBJECT_ID), 65);
    defaultColumns.Add(service.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION), 250);
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.RelationColumnSchemeGuid, (object) MetaDataHelper.GetAttributeTypeID("cad00202-306c-11d8-b4e9-00304f19f545")), 75);
    defaultColumns.Add(service.CreateColumn(Intermech.Navigator.Consts.NavigatorColumnSchemeGuid, (object) "F_STATUSES"), 75);
    return defaultColumns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override NodeColumnCollection GetSupportedColumns(string ColumnSetName)
  {
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObjectTypeColumns(columns, this._objTypeID);
    Helper.AddRelationTypeColumns(columns, this._relTypeID);
    Helper.AddObligatoryColumns(columns, true, true);
    Helper.AddObligatoryColumnsAdv(columns);
    Helper.AddObligatoryColumnsRelation(columns);
    Helper.AddObligatoryColumnsRelationAdv(columns);
    Helper.AddAllColumns(columns);
    Helper.AddAllColumnsRelation(columns);
    return columns;
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    if (this._relTypeID != -1)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBRelationsApplicabilityCollection applicabilityCollection = sessionKeeper.Session.GetRelationsApplicabilityCollection();
        if ((this._role == RelatedObjectsRole.Composition ? applicabilityCollection.GetApplicabilitiesList(this._relTypeID, -1, this._objTypeID) : applicabilityCollection.GetApplicabilitiesList(this._relTypeID, this._objTypeID, -1)).Rows.Count == 0)
          return (INodeQuery) null;
      }
    }
    AdvRelationsQuery query = new AdvRelationsQuery((INodeQuerySupport) this, this._objID, this._objTypeID, this._role, this._relTypeID, conditions, this._filtrationOwnerID, this._contexts);
    query.Services = this.Services;
    return (INodeQuery) query;
  }

  /// <summary>
  /// Вернуть список служебных полей, которые всегда загружаются вместе с составом
  /// </summary>
  /// <returns>Список служебных полей, которые всегда загружаются вместе с составом</returns>
  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    if (!specialFields.Contains((object) ObjectsPartBase.ncF_ID))
      specialFields.Add((object) ObjectsPartBase.ncF_ID);
    if (!specialFields.Contains((object) ObjectsPartBase.ncCAPTION))
      specialFields.Add((object) ObjectsPartBase.ncCAPTION);
    if (!specialFields.Contains((object) ObjectsPartBase.ncOWNER))
      specialFields.Add((object) ObjectsPartBase.ncOWNER);
    MetaDataHelper.GetAttribute4RelationType(this._relTypeID, Convert.ToInt32(ObjectsPartBase.ncSORTING.ID));
    if (!specialFields.Contains((object) ObjectsPartBase.ncSORTING))
      specialFields.Add((object) ObjectsPartBase.ncSORTING);
    if (!specialFields.Contains((object) ObjectsPartBase.ncF_LC_STEP))
      specialFields.Add((object) ObjectsPartBase.ncF_LC_STEP);
    if (!specialFields.Contains((object) ObjectsPartBase.ncVERSION))
      specialFields.Add((object) ObjectsPartBase.ncVERSION);
    if (!specialFields.Contains((object) ObjectsPartBase.ncBASE_VERSION))
      specialFields.Add((object) ObjectsPartBase.ncBASE_VERSION);
    if (!specialFields.Contains((object) ObjectsPartBase.ncSITE_ID))
      specialFields.Add((object) ObjectsPartBase.ncSITE_ID);
    if (this.ncAdvAttributes != null)
    {
      for (int index = 0; index < this.ncAdvAttributes.Count; ++index)
        specialFields.Add((object) this.ncAdvAttributes[index]);
    }
    return specialFields;
  }

  /// <summary>Вернуть дочерний узел на основании его описания</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел на основании его описания или null</returns>
  public override INode GetChild(INodeID nodeID)
  {
    return nodeID is AdvRelationsNodeID advRelationsNodeId ? (INode) new AdvRelationsNode((CreateObjectNodeParams) new AdvCreateObjectNodeParams(advRelationsNodeId.ObjectTypeID, advRelationsNodeId.ObjectID, advRelationsNodeId.ID, advRelationsNodeId.CheckedOutBy, advRelationsNodeId.PrjLinkID, advRelationsNodeId.LCStepID, advRelationsNodeId.Caption, advRelationsNodeId.RelationTypeID, advRelationsNodeId.Owner, advRelationsNodeId.Sorting, advRelationsNodeId.State, advRelationsNodeId.Version, advRelationsNodeId.BaseVersion, advRelationsNodeId.SiteID, advRelationsNodeId.FiltrationOwnerID, advRelationsNodeId.Contexts, advRelationsNodeId.ProjObjType, advRelationsNodeId.ProjID, advRelationsNodeId.RelGuid, advRelationsNodeId.ModificationID, advRelationsNodeId.Attributes, advRelationsNodeId.Values)) : (INode) null;
  }
}
