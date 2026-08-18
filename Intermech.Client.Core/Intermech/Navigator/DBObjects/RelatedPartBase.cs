
// Type: Intermech.Navigator.DBObjects.RelatedPartBase
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using Intermech.Navigator.Selections.Implementation;
using System;
using System.Collections.Generic;
using System.Drawing;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Реализует часть элемента навигации, работающую со списком связей
/// </summary>
public class RelatedPartBase : ObjectsPartBase
{
  /// <summary>Название набора колонок - "Атрибуты связей"</summary>
  internal static string columnsSetNameRel = LocalizationHolder.rm.GetString("Client.Core_317");
  /// <summary>
  /// Составное значение: атрибут F_PRJLINK_ID : источник - связь
  /// </summary>
  public static NodeColumnID ncF_PRJLINK_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_PRJLINK_ID, AttributeSourceTypes.Relation);
  /// <summary>
  /// Составное значение: атрибут F_RELATION_TYPE : источник - связь
  /// </summary>
  public static NodeColumnID ncF_RELATION_TYPE = new NodeColumnID((object) ObligatoryObjectAttributes.F_RELATION_TYPE, AttributeSourceTypes.Relation);
  /// <summary>
  /// Составное значение: атрибут F_ELEMENT_STATUSES : источник - связь
  /// </summary>
  public new static NodeColumnID ncF_ELEMENT_STATUSES = new NodeColumnID((object) -77, AttributeSourceTypes.Relation);
  /// <summary>
  /// Составное значение: атрибут F_PROJ_ID : источник - связь
  /// </summary>
  public static NodeColumnID ncF_PROJ_ID = new NodeColumnID((object) ObligatoryObjectAttributes.F_PROJ_ID, AttributeSourceTypes.Relation);
  /// <summary>
  /// Составное значение: атрибут F_PRJ_GUID : источник - связь
  /// </summary>
  public static NodeColumnID ncF_PRJ_GUID = new NodeColumnID((object) ObligatoryObjectAttributes.F_PRJ_GUID, AttributeSourceTypes.Relation);
  /// <summary>
  /// Идентификатор версии объекта, состав/применяемость которого будет читать эта часть
  /// </summary>
  protected long _objID;
  /// <summary>
  /// Идентификатор типа объекта, состав/применяемость которого будет читать эта часть
  /// </summary>
  protected int _objTypeID;

  public long ProjectVersionID => this._objID;

  /// <summary>Конструктор</summary>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedPartBase(IServiceProvider services)
    : base(services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает одно
  /// условие на объекты, с которыми она работает.
  /// </summary>
  /// <param name="condition">Условие, которому должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedPartBase(ConditionStructure condition, IServiceProvider services)
    : base(condition, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает условия на объекты, с которыми она работает.
  /// </summary>
  /// <param name="conditions">Условия, которым должны удовлетворять объекты.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedPartBase(ConditionStructure[] conditions, IServiceProvider services)
    : base(conditions, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает динамически
  /// изменяющийся набор условий на объекты, с которыми она работает.
  /// </summary>
  /// <param name="conditionsProvider">Провайдер условий.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedPartBase(IConditionsProvider conditionsProvider, IServiceProvider services)
    : base((ConditionStructure[]) null, conditionsProvider, services)
  {
  }

  /// <summary>
  /// Конструктор, позволяющий создать часть, которая накладывает динамически
  /// изменяющийся набор условий на объекты, с которыми она работает.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <param name="conditionsProvider">Провайдер условий.</param>
  /// <param name="services">Контейнер сервисов</param>
  public RelatedPartBase(
    ConditionStructure[] conditions,
    IConditionsProvider conditionsProvider,
    IServiceProvider services)
    : base(conditions, conditionsProvider, services)
  {
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
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columns, true, false);
    Helper.AddObligatoryColumnsRelation(columns);
    return columns;
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
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    NodeColumnCollection columns = new NodeColumnCollection();
    Helper.AddObligatoryColumns(columns, true, true);
    Helper.AddObligatoryColumnsAdv(columns);
    Helper.AddObligatoryColumnsRelation(columns);
    Helper.AddObligatoryColumnsRelationAdv(columns);
    if (ColumnSetName == Intermech.Navigator.Consts.ColumnSetNameAllAttrs || ColumnSetName == string.Empty)
    {
      Helper.AddAllColumns(columns);
      Helper.AddAllColumnsRelation(columns);
    }
    return columns;
  }

  /// <summary>
  /// Возвращает коллекцию всех поддерживаемых данным элементом
  /// виртуальных колонок навигатора. Этот метод используется диалогом
  /// настройки отображения грида.
  /// </summary>
  /// <param name="ColumnSetName">Название набора колонок.
  /// Intermech.Navigator.Consts.NavigatorDefaultColumnSetName - набор колонок по умолчанию</param>
  /// <param name="columns">Коллекция колонок</param>
  /// <returns>Коллекция виртуальных колонок навигатора</returns>
  public override void GetSupportedColumns(string ColumnSetName, NodeColumnCollection columns)
  {
    if (columns == null)
      return;
    Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
    IColumnSchemes service = (IColumnSchemes) ServicesManager.GetService(typeof (IColumnSchemes));
    Helper.AddObligatoryColumns(columns, true, true);
    Helper.AddObligatoryColumnsAdv(columns);
    Helper.AddObligatoryColumnsRelation(columns);
    Helper.AddObligatoryColumnsRelationAdv(columns);
    if (!(ColumnSetName == Intermech.Navigator.Consts.ColumnSetNameAllAttrs) && !(ColumnSetName == string.Empty))
      return;
    Helper.AddAllColumns(columns);
    Helper.AddAllColumnsRelation(columns);
  }

  /// <summary>
  /// Вернуть список поддерживаемых названий наборов колонок.
  /// Если null - есть только название по умолчанию (Intermech.Navigator.Consts.NavigatorDefaultColumnSetName)
  /// </summary>
  /// <returns></returns>
  public override List<string> GetSupportedColumnSetNames()
  {
    List<string> supportedColumnSetNames = base.GetSupportedColumnSetNames() ?? new List<string>();
    if (!supportedColumnSetNames.Contains(RelatedPartBase.columnsSetNameRel))
      supportedColumnSetNames.Add(RelatedPartBase.columnsSetNameRel);
    return supportedColumnSetNames;
  }

  /// <summary>
  /// Возвращает интерфейс объекта-запроса, с помощью которого эта часть
  /// читает список обрабатываемых ею объектов.
  /// </summary>
  /// <param name="conditions">Массив условий, которым должны удовлетворять объекты.</param>
  /// <returns>Ссылка на интерфейс объекта-запроса.</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions) => (INodeQuery) null;

  public override object GetService(Type service)
  {
    return service == typeof (INodeStatusesInfo) ? (object) ObjectsPartBase.StatusesInfoService : (object) null;
  }

  public override object MapColumnToField(NodeColumn column)
  {
    if (column.SchemeGuid == Intermech.Navigator.Consts.NavigatorColumnSchemeGuid && column.ID.Equals((object) "F_STATUSES"))
      return (object) new NodeColumnID((object) -77, AttributeSourceTypes.Relation);
    return column.SchemeGuid == Intermech.Navigator.Consts.RelationColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.CurrentRelationColumnSchemeGuid || column.SchemeGuid == Intermech.Navigator.Consts.RelationObligatoryColumnSchemeGuid ? (object) new NodeColumnID(column.ID, AttributeSourceTypes.Relation) : base.MapColumnToField(column);
  }

  /// <summary>
  /// Получить список служебных полей (которые загружаются в узел независимо от настройки вида)
  /// </summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields();
    if (!specialFields.Contains((object) RelatedPartBase.ncF_PRJLINK_ID))
      specialFields.Add((object) RelatedPartBase.ncF_PRJLINK_ID);
    if (!specialFields.Contains((object) ObjectsPartBase.ncF_LC_STEP))
      specialFields.Add((object) ObjectsPartBase.ncF_LC_STEP);
    if (!specialFields.Contains((object) ObjectsPartBase.ncCAPTION))
      specialFields.Add((object) ObjectsPartBase.ncCAPTION);
    if (!specialFields.Contains((object) RelatedPartBase.ncF_RELATION_TYPE))
      specialFields.Add((object) RelatedPartBase.ncF_RELATION_TYPE);
    if (!specialFields.Contains((object) ObjectsPartBase.ncOWNER))
      specialFields.Add((object) ObjectsPartBase.ncOWNER);
    if (!specialFields.Contains((object) ObjectsPartBase.ncVERSION))
      specialFields.Add((object) ObjectsPartBase.ncVERSION);
    if (!specialFields.Contains((object) ObjectsPartBase.ncMODIFICATION_ID))
      specialFields.Add((object) ObjectsPartBase.ncMODIFICATION_ID);
    if (!specialFields.Contains((object) RelatedPartBase.ncF_PROJ_ID))
      specialFields.Add((object) RelatedPartBase.ncF_PROJ_ID);
    if (!specialFields.Contains((object) RelatedPartBase.ncF_PRJ_GUID))
      specialFields.Add((object) RelatedPartBase.ncF_PRJ_GUID);
    if (!specialFields.Contains((object) RelatedPartBase.ncF_ELEMENT_STATUSES))
      specialFields.Add((object) RelatedPartBase.ncF_ELEMENT_STATUSES);
    return specialFields;
  }

  public override object CreateRecordId(INodeID nodeId)
  {
    return ((NodeID) nodeId).RelGuid == Guid.Empty ? (object) ((NodeID) nodeId).PrjLinkID : (object) ((NodeID) nodeId).RelGuid;
  }

  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJLINK_ID)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string caption = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    int int32_3 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_RELATION_TYPE)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]);
    long int64_7 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)]);
    string siteID = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    long int64_8 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PROJ_ID)]);
    Guid guidValue = DataSetProcessor.GetGuidValue(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJ_GUID)], Guid.Empty);
    long int64_9 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    long int64_10 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)] == DBNull.Value ? 0L : Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)]);
    byte[] fieldValue = adapter.GetFieldIndex((object) RelatedPartBase.ncF_ELEMENT_STATUSES) < 0 || fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_ELEMENT_STATUSES)] == DBNull.Value ? (byte[]) null : fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_ELEMENT_STATUSES)] as byte[];
    ObjectFiltrationState state = ObjectFiltrationState.fsNotRequired;
    if (fieldValue != null)
      state = (ObjectFiltrationState) (ServicesManager.GetService(typeof (IElementStatusesClientService)) as IElementStatusesClientService).GetElementStatuses32("cad005f2-306c-11d8-b4e9-00304f19f545", fieldValue);
    FontStyle fontStyle = FontStyle.Regular;
    IFontStyledNode service = this.Services.GetService<IFontStyledNode>(false);
    if (service != null)
      fontStyle = service.ComputeFontStyleStatus(fieldValues, adapter, fieldValue);
    NodeID nodeId;
    if (this.Owner is DesktopObjectNode && MetaDataHelper.GetObjectTypeChildrenID(new Guid("cad00156-306c-11d8-b4e9-00304f19f545")).Contains(int32_1))
    {
      bool handSelection = adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)] != DBNull.Value && Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)]) == 1L;
      SelectionType selectionType = adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)] == DBNull.Value ? SelectionType.None : (SelectionType) Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)]);
      int bindedObjectTypeID = -1;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        ConditionStructure[] conditionStructures = ((ISelectionsService) ServicesManager.GetService(typeof (ISelectionsService))).GetConditionStructures((object) sessionKeeper.Session, int64_1);
        if (conditionStructures != null)
        {
          if (conditionStructures.Length != 0)
          {
            foreach (ConditionStructure conditionStructure in conditionStructures)
            {
              if (conditionStructure.RelationalOperator == RelationalOperators.ObjectTypeFilter)
              {
                bindedObjectTypeID = Convert.ToInt32(conditionStructure.Value);
                break;
              }
            }
          }
        }
      }
      int int32_4 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)] == DBNull.Value ? 0 : Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)]);
      bool searchInLocalTypes = adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)] != DBNull.Value && Convert.ToBoolean(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)]);
      nodeId = (NodeID) new SelectionNodeID((CreateObjectNodeParams) new CreateSelectionNodeParams(int32_1, int64_1, int64_2, int64_3, int64_4, int32_2, caption, int32_3, int64_5, int64_10, state, int64_6, int64_7, handSelection, selectionType, siteID, int64_8, guidValue, int64_9, bindedObjectTypeID, int32_4, searchInLocalTypes));
    }
    else
      nodeId = (NodeID) this.CreateObjectNodeIdFromParams(fieldValues, adapter, new CreateObjectNodeParams(int32_1, int64_1, int64_2, int64_3, int64_4, int32_2, caption, int32_3, int64_5, int64_10, state, int64_6, int64_7, siteID, int64_8, guidValue, int64_9, fontStyle));
    return (INodeID) nodeId;
  }
}
