
// Type: Intermech.Navigator.Selections.Implementation.TopSelectionsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>Работаем с верхним списком выборок и классификаторов</summary>
internal class TopSelectionsPart : ObjectsPart
{
  /// <summary>Привязки</summary>
  private ITopBinding _binding;
  /// <summary>Внешние условия</summary>
  private IConditionsProvider _externalConditions;
  /// <summary>Идентификатор типа объекта "Выборки"</summary>
  private static int _selectionTypeID = -1;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="binding">Привязки</param>
  /// <param name="externalConditions">Внешние условия</param>
  /// <param name="propagateConditions">Наследовать условия</param>
  /// <param name="services">Контейнер сервисов</param>
  public TopSelectionsPart(
    int selTypeID,
    ITopBinding binding,
    IConditionsProvider externalConditions,
    IServiceProvider services)
    : base(selTypeID, binding.TopConditions, services)
  {
    this._binding = binding;
    this._externalConditions = externalConditions;
  }

  /// <summary>Создать дочерний узел по его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    IDBTypedObjectID data1 = this.GetData(nodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    IDBTypedObjectID data2 = this._binding != null ? this._binding.GetData(typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    if (!(nodeID is SelectionNodeID selectionNodeId))
      return base.GetChild(nodeID);
    return service.GetNode(nodeID, (object) data1.ObjectType, (object) data1.ObjectID, (object) this._binding, (object) this._externalConditions, (object) selectionNodeId.HandSelection, (object) (data2 != null ? data2.ObjectType : -1), (object) selectionNodeId.SampleFunction, (object) selectionNodeId.SearchInLocalTypes);
  }

  /// <summary>Вернуть данные указанного формата</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Тип запрашиваемых данных</param>
  /// <returns>Данные указанного формата или null</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IBinding))
      return (object) this._binding;
    IDBTypedObjectID data = this._binding != null ? this._binding.GetData(typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    if (nodeID is SelectionNodeID selectionNodeId)
    {
      if (dataFormat == typeof (INavigatorIconInformation))
        return (object) new NavigatorIconInformation((object) new DBSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType));
      if (dataFormat == typeof (IDBSelectionID) || dataFormat == typeof (IDBObjectTypeSelectionID))
      {
        int bindedObjectTypeID = -1;
        if (data == null)
        {
          INode child = this.GetChild(nodeID);
          if (child is SelectionNode)
            bindedObjectTypeID = ((SelectionNode) child).FilterObjectType;
        }
        else
          bindedObjectTypeID = data.ObjectType;
        return (object) new DBObjectTypeSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType, bindedObjectTypeID);
      }
    }
    return base.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Получить список служебных полей (которые загружаются в узел независимо от настройки вида)
  /// </summary>
  /// <returns>Список служебных полей (которые загружаются в узел независимо от настройки вида)</returns>
  public override List<object> GetSpecialFields()
  {
    List<object> specialFields = base.GetSpecialFields() ?? new List<object>();
    if (TopSelectionsPart._selectionTypeID == -1)
      TopSelectionsPart._selectionTypeID = MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545");
    if (MetaDataHelper.IsObjectTypeChildOf(this.objTypeID, TopSelectionsPart._selectionTypeID))
    {
      if (!specialFields.Contains((object) ObjectsPartBase.ncHANDS_SELECTION))
        specialFields.Add((object) ObjectsPartBase.ncHANDS_SELECTION);
      if (!specialFields.Contains((object) ObjectsPartBase.ncSELECTION_TYPE))
        specialFields.Add((object) ObjectsPartBase.ncSELECTION_TYPE);
      if (!specialFields.Contains((object) ObjectsPartBase.ncSAMPLE_FUNCTION))
        specialFields.Add((object) ObjectsPartBase.ncSAMPLE_FUNCTION);
      if (!specialFields.Contains((object) ObjectsPartBase.ncSEARCH_LOCALTYPES))
        specialFields.Add((object) ObjectsPartBase.ncSEARCH_LOCALTYPES);
    }
    return specialFields;
  }

  /// <summary>Создать описание корневого узла</summary>
  /// <param name="fieldValues">Значения полей</param>
  /// <param name="adapter">Адаптер</param>
  /// <returns>Описание корневого узла</returns>
  public override INodeID CreateNodeId(object[] fieldValues, RecordAdapter adapter)
  {
    int int32_1 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_TYPE)]);
    long int64_1 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_OBJECT_ID)]);
    long int64_2 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_ID)]);
    long int64_3 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_CHKOUT_BY)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string str1 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)]);
    string str2 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    long int64_7 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    SelectionType selectionType = adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)] == DBNull.Value ? SelectionType.None : (SelectionType) Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)]);
    long int64_8 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)] == DBNull.Value ? 0L : Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)]);
    int int32_3 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)] == DBNull.Value ? 0 : Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)]);
    bool flag1 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)] != DBNull.Value && Convert.ToBoolean(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)]);
    IDBTypedObjectID data = this._binding != null ? this._binding.GetData(typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    bool flag2 = adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)] != DBNull.Value && Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)]) == 1L;
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    int lcStepID = int32_2;
    string caption = str1;
    long owner = int64_4;
    long sorting = int64_8;
    long version = int64_5;
    long baseVersion = int64_6;
    int num1 = flag2 ? 1 : 0;
    int num2 = (int) selectionType;
    string siteID = str2;
    Guid empty = Guid.Empty;
    long modificationID = int64_7;
    int bindedObjectTypeID = data != null ? data.ObjectType : -1;
    int sampleFunction = int32_3;
    int num3 = flag1 ? 1 : 0;
    return (INodeID) new SelectionNodeID((CreateObjectNodeParams) new CreateSelectionNodeParams(int32_1, objId, id, checkedOutBy, -1L, lcStepID, caption, -1, owner, sorting, ObjectFiltrationState.fsNotRequired, version, baseVersion, num1 != 0, (SelectionType) num2, siteID, 0L, empty, modificationID, bindedObjectTypeID, sampleFunction, num3 != 0));
  }

  /// <summary>Получить объект для запросов к источнику данных</summary>
  /// <param name="conditions">Список условий</param>
  /// <returns>Объект для запросов к источнику данных</returns>
  protected override INodeQuery GetQuery(ConditionStructure[] conditions)
  {
    INodeQuery query = base.GetQuery(conditions);
    if (!(query is IObjectCollectionFilters collectionFilters))
      return query;
    collectionFilters.PluginsData[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    return query;
  }
}
