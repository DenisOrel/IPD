
// Type: Intermech.Navigator.Selections.Implementation.SelectionsPart
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Queries;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Selections.Implementation;

/// <summary>Работает со списком выборок и классификаторов</summary>
internal class SelectionsPart : RelatedObjectsPart
{
  /// <summary>Привязки</summary>
  private IBinding _binding;
  /// <summary>Внешние условия</summary>
  private IConditionsProvider _externalConditions;
  private int _sampleFunction;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="selTypeID">Тип объекта</param>
  /// <param name="selID">Идентификатор версии объекта</param>
  /// <param name="relTypeID">Тип связи</param>
  /// <param name="binding">Привязки</param>
  /// <param name="externalConditions">Внешние условия</param>
  /// <param name="propagateConditions">Наследовать условия</param>
  /// <param name="services">Контейнер сервисов</param>
  public SelectionsPart(
    int selTypeID,
    long selID,
    int relTypeID,
    IBinding binding,
    IConditionsProvider externalConditions,
    int sampleFunction,
    IServiceProvider services)
    : base(selTypeID, selID, RelatedObjectsRole.Composition, relTypeID, services)
  {
    this._binding = binding;
    this._externalConditions = externalConditions;
    this._sampleFunction = sampleFunction;
  }

  /// <summary>Создать дочерний узел по его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID)
  {
    IDBTypedObjectID data = this.GetData(nodeID, typeof (IDBTypedObjectID)) as IDBTypedObjectID;
    SelectionNodeID selectionNodeId = nodeID as SelectionNodeID;
    IFactory service = (IFactory) ServicesManager.GetService(typeof (IFactory));
    if (selectionNodeId == null)
      return base.GetChild(nodeID);
    return service.GetNode(nodeID, (object) data.ObjectType, (object) data.ObjectID, (object) this._binding, (object) this._externalConditions, (object) selectionNodeId.HandSelection, (object) selectionNodeId.BindedObjectTypeID, (object) selectionNodeId.SampleFunction, (object) selectionNodeId.SearchInLocalTypes);
  }

  /// <summary>Вернуть данные указанного формата</summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Тип запрашиваемых данных</param>
  /// <returns>Данные указанного формата или null</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IBinding))
      return (object) this._binding;
    if (nodeID is SelectionNodeID selectionNodeId)
    {
      if (dataFormat == typeof (INavigatorIconInformation))
        return (object) new NavigatorIconInformation((object) new DBSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType));
      if (dataFormat == typeof (IDBSelectionID) || dataFormat == typeof (IDBObjectTypeSelectionID))
        return (object) new DBObjectTypeSelectionID(selectionNodeId.ObjectID, selectionNodeId.ID, selectionNodeId.HandSelection, selectionNodeId.SelectionType, selectionNodeId.BindedObjectTypeID);
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
    if (!specialFields.Contains((object) ObjectsPartBase.ncOWNER))
      specialFields.Add((object) ObjectsPartBase.ncOWNER);
    if (MetaDataHelper.GetAttribute4RelationType(this._relTypeID, Convert.ToInt32(ObjectsPartBase.ncSORTING.ID)) != null && !specialFields.Contains((object) ObjectsPartBase.ncSORTING))
      specialFields.Add((object) ObjectsPartBase.ncSORTING);
    if (MetaDataHelper.IsObjectTypeChildOf(this._objTypeID, MetaDataHelper.GetObjectTypeID("cad00156-306c-11d8-b4e9-00304f19f545")))
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
    long int64_4 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJLINK_ID)]);
    int int32_2 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_RELATION_TYPE)]);
    int int32_3 = Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncF_LC_STEP)]);
    string str1 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncCAPTION)]);
    long int64_5 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncOWNER)]);
    long int64_6 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncVERSION)]);
    long int64_7 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncBASE_VERSION)]);
    long int64_8 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PROJ_ID)]);
    Guid guidValue = DataSetProcessor.GetGuidValue(fieldValues[adapter.GetFieldIndex((object) RelatedPartBase.ncF_PRJ_GUID)], Guid.Empty);
    string str2 = Convert.ToString(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSITE_ID)]);
    long int64_9 = Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncMODIFICATION_ID)]);
    long int64_10 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)] == DBNull.Value ? 0L : Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSORTING)]);
    bool flag1 = adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)] != DBNull.Value && Convert.ToInt64(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncHANDS_SELECTION)]) == 1L;
    SelectionType selectionType = adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)] == DBNull.Value ? SelectionType.None : (SelectionType) Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSELECTION_TYPE)]);
    int int32_4 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION) < 0 || fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)] == DBNull.Value ? 0 : Convert.ToInt32(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSAMPLE_FUNCTION)]);
    bool flag2 = adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES) >= 0 && fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)] != DBNull.Value && Convert.ToBoolean(fieldValues[adapter.GetFieldIndex((object) ObjectsPartBase.ncSEARCH_LOCALTYPES)]);
    IDBTypedObjectID data = this._binding is ITopBinding binding ? binding.GetData(typeof (IDBTypedObjectID)) as IDBTypedObjectID : (IDBTypedObjectID) null;
    long objId = int64_1;
    long id = int64_2;
    long checkedOutBy = int64_3;
    long prjLinkId = int64_4;
    int lcStepID = int32_3;
    string caption = str1;
    int relTypeID = int32_2;
    long owner = int64_5;
    long sorting = int64_10;
    long version = int64_6;
    long baseVersion = int64_7;
    int num1 = flag1 ? 1 : 0;
    int num2 = (int) selectionType;
    string siteID = str2;
    long projID = int64_8;
    Guid relGuid = guidValue;
    long modificationID = int64_9;
    int bindedObjectTypeID = data != null ? data.ObjectType : -1;
    int sampleFunction = int32_4;
    int num3 = flag2 ? 1 : 0;
    return (INodeID) new SelectionNodeID((CreateObjectNodeParams) new CreateSelectionNodeParams(int32_1, objId, id, checkedOutBy, prjLinkId, lcStepID, caption, relTypeID, owner, sorting, ObjectFiltrationState.fsNotRequired, version, baseVersion, num1 != 0, (SelectionType) num2, siteID, projID, relGuid, modificationID, bindedObjectTypeID, sampleFunction, num3 != 0));
  }
}
