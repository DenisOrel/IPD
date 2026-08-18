// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TechCardClientConst
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Expert;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Kernel.Search;
using Intermech.Navigator;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.TechCard.Client.Navigator.Descriptors;
using Intermech.TechCard.Client.Services.ClassifyObject;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.TechCard.Client;

/// <summary>Summary description for TechCardClientConst.</summary>
public static class TechCardClientConst
{
  /// <summary>Диалог выбора объекта</summary>
  /// <param name="objectTypeGuid"></param>
  /// <param name="dialogCaption"></param>
  /// <param name="extraDescriptors">Дополнительный дескриптор</param>
  /// <param name="descriptorCaption">Наименование корневого узла (дескриптора)</param>
  /// <returns>идентификатор выбранного типа объекта</returns>
  public static long SelectObjectDlg(
    Guid objectTypeGuid,
    string dialogCaption,
    IDescriptor[] extraDescriptors = null,
    string descriptorCaption = null,
    IServiceProvider serviceProvider = null)
  {
    List<long> longList = TechCardClientConst.SelectObjectsDlg(objectTypeGuid, dialogCaption, extraDescriptors, descriptorCaption, serviceProvider);
    return longList == null || longList.Count <= 0 ? 0L : longList[0];
  }

  /// <summary>Objects selection dialog</summary>
  /// <param name="objectTypeGuid">Object's type</param>
  /// <param name="dialogCaption">Caption</param>
  /// <param name="extraDescriptors"></param>
  /// <param name="descriptorCaption"></param>
  /// <returns></returns>
  public static List<long> SelectObjectsDlg(
    Guid objectTypeGuid,
    string dialogCaption,
    IDescriptor[] extraDescriptors = null,
    string descriptorCaption = null,
    IServiceProvider serviceProvider = null)
  {
    return TechCardClientConst.SelectObjectsDlg((IEnumerable<Guid>) new Guid[1]
    {
      objectTypeGuid
    }, dialogCaption, extraDescriptors, descriptorCaption, serviceProvider);
  }

  /// <summary>Objects selection dialog</summary>
  /// <param name="objectTypeGuids">Object's types</param>
  /// <param name="dialogCaption">Caption</param>
  /// <param name="extraDescriptors"></param>
  /// <param name="descriptorCaption"></param>
  /// <returns></returns>
  public static List<long> SelectObjectsDlg(
    IEnumerable<Guid> objectTypeGuids,
    string dialogCaption,
    IDescriptor[] extraDescriptors = null,
    string descriptorCaption = null,
    IServiceProvider serviceProvider = null)
  {
    return TechCardClientConst.SelectObjectsDlg((IEnumerable<int>) objectTypeGuids.Select<Guid, int>(new System.Func<Guid, int>(MetaDataHelper.GetObjectTypeID)).Where<int>((System.Func<int, bool>) (typeId => typeId != -1)).ToArray<int>(), dialogCaption, extraDescriptors, descriptorCaption, serviceProvider);
  }

  /// <summary>Objects selection dialog</summary>
  /// <param name="objectTypeGuids">Object's types</param>
  /// <param name="dialogCaption">Caption</param>
  /// <param name="extraDescriptors"></param>
  /// <param name="descriptorCaption"></param>
  /// <returns></returns>
  public static List<long> SelectObjectsDlg(
    IEnumerable<int> objectTypeIds,
    string dialogCaption,
    IDescriptor[] extraDescriptors = null,
    string descriptorCaption = null,
    IServiceProvider serviceProvider = null)
  {
    List<long> longList = new List<long>();
    if (!objectTypeIds.Any<int>())
      return longList;
    IDescriptor composition = Intermech.Navigator.DBObjectTypes.Descriptor.CreateComposition(objectTypeIds);
    IDescriptor rootDescriptor;
    if (extraDescriptors == null)
    {
      rootDescriptor = composition;
    }
    else
    {
      DescriptorCollection descriptors = new DescriptorCollection()
      {
        composition
      };
      descriptors.AddRange((IEnumerable<IDescriptor>) extraDescriptors);
      rootDescriptor = (IDescriptor) new TechDescriptor(Intermech.Navigator.Consts.CategorySelectObjectListsNode, ((System.Func<IEnumerable<int>, int>) (childObjectTypes =>
      {
        int parentObjectTypeId = MetaDataHelper.GetCommonParentObjectTypeID(childObjectTypes);
        return parentObjectTypeId == -1 ? 0 : parentObjectTypeId;
      }))(objectTypeIds), descriptorCaption, descriptors);
    }
    if (SelectionWindow.Select(dialogCaption, rootDescriptor, typeof (IDBObjectID), serviceProvider, SelectionOptions.Default | SelectionOptions.ForceRebuildNavTree) is IDBObjectID[] source)
    {
      longList.Capacity = source.Length;
      longList.AddRange(((IEnumerable<IDBObjectID>) source).Select<IDBObjectID, long>((System.Func<IDBObjectID, long>) (dbObjId => dbObjId.Value)));
    }
    return longList;
  }

  /// <summary>
  /// Диалог выбора объекта (только дерево и без раскрытия состава)
  /// </summary>
  /// <param name="objectTypeId">Ид. типов объектов</param>
  /// <param name="objIdList">Ид-ры версий объектов</param>
  /// <param name="rootCaption"></param>
  /// <param name="dialogCaption"></param>
  /// <returns>Идентификаторы выбранных объектов</returns>
  public static List<long> SelectObjectOnlyDlg(
    int objectTypeId,
    IDictionary<long, int> objIdList,
    string rootCaption,
    string dialogCaption)
  {
    return TechCardClientConst.SelectObjectOnlyDlg(objectTypeId, (IList<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList(objIdList), rootCaption, dialogCaption);
  }

  /// <summary>
  /// Диалог выбора объекта (только дерево и без раскрытия состава)
  /// </summary>
  /// <param name="objectTypeId">Ид. типов объектов</param>
  /// <param name="objInfoList">Ид-ры версий объектов</param>
  /// <param name="rootCaption"></param>
  /// <param name="dialogCaption"></param>
  /// <returns>Идентификаторы выбранных объектов</returns>
  public static List<long> SelectObjectOnlyDlg(
    int objectTypeId,
    IList<ObjInfoItem> objInfoList,
    string rootCaption,
    string dialogCaption)
  {
    if (objInfoList == null || objInfoList.Count == 0)
      return (List<long>) null;
    Guid guid = new Guid("B3C37988-8FCF-47D3-B31E-AE4BA0C299D7");
    IGuidMapper service = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service == null)
      return (List<long>) null;
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoList);
    int num = service.Register(guid);
    try
    {
      ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false).AddNodeType(num, typeof (ObjectsListNode));
      DictDescriptor rootDescriptor = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, objectTypeId, rootCaption, objectTypeCache)
      {
        ExpandNodes = false
      };
      long[] collection = SelectionWindow.SelectObjects(dialogCaption, "", (IDescriptor) rootDescriptor, SelectionOptions.HideViews | SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromViews | SelectionOptions.DisableObjectListFilter | SelectionOptions.ForceRebuildNavTree);
      return collection != null ? new List<long>((IEnumerable<long>) collection) : new List<long>();
    }
    finally
    {
      service.Unregister(num);
    }
  }

  /// <summary>Диалог выбора локальных объектов</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="objIdList"></param>
  /// <param name="rootCaption"></param>
  /// <param name="dialogCaption"></param>
  /// <returns>Идентификаторы выбранных объектов</returns>
  public static List<long> SelectObjectDlg(
    int objectTypeId,
    IDictionary<long, int> objIdList,
    string rootCaption,
    string dialogCaption)
  {
    return TechCardClientConst.SelectObjectDlg(objectTypeId, (IList<ObjInfoItem>) ObjInfoHelper.GetObjectInfoList(objIdList), rootCaption, dialogCaption);
  }

  /// <summary>Диалог выбора локальных объектов</summary>
  /// <param name="objTypeId"></param>
  /// <param name="objInfoList"></param>
  /// <param name="rootCaption"></param>
  /// <param name="dialogCaption"></param>
  /// <returns>Идентификаторы выбранных объектов</returns>
  public static List<long> SelectObjectDlg(
    int objectTypeId,
    IList<ObjInfoItem> objInfoList,
    string rootCaption,
    string dialogCaption)
  {
    List<long> longList = new List<long>();
    if (objInfoList == null || objInfoList.Count == 0)
      return longList;
    Guid guid = new Guid("B3C37988-8FCF-47D3-B31E-AE4BA0C299D7");
    IGuidMapper service1 = ServiceUtils.GetService<IGuidMapper>((object) ApplicationServices.Container, false);
    if (service1 == null)
      return longList;
    Dictionary<int, List<long>> objectTypeCache = ObjInfoHelper.GetObjectTypeCache((IEnumerable<ObjInfoItem>) objInfoList);
    int num = service1.Register(guid);
    try
    {
      IFactory service2 = ServiceUtils.GetService<IFactory>((object) ApplicationServices.Container, false);
      service2.AddNodeType(num, typeof (ObjectsListNode));
      service2.AddViewsProvider(num, (IViewsProvider) new AdvObjectsPropertiesProvider());
      DictDescriptor rootDescriptor = new DictDescriptor(Intermech.Navigator.Consts.CategorySelectObjectsNode, objectTypeId, rootCaption, objectTypeCache)
      {
        ExpandNodes = false
      };
      if (SelectionWindow.Select(dialogCaption, (IDescriptor) rootDescriptor, typeof (IDBObjectID), SelectionOptions.Default | SelectionOptions.ForceRebuildNavTree) is IDBObjectID[] source)
        longList.AddRange(((IEnumerable<IDBObjectID>) source).Select<IDBObjectID, long>((System.Func<IDBObjectID, long>) (dbObjId => dbObjId.Value)));
    }
    finally
    {
      service1.Unregister(num);
    }
    return longList;
  }

  /// <summary>Классификация документа по изделию</summary>
  /// <param name="artObjId"></param>
  /// <param name="tpTypeId"></param>
  /// <param name="prodObjId"></param>
  /// <param name="tpName"></param>
  /// <param name="tpDesign"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  [Obsolete("Use ITechCardClassifyObjectService service instead. Will be removed in IPS 8", false)]
  public static bool ClassifyTpDoc(
    long artObjId,
    long prodObjId,
    int tpTypeId,
    ref string tpName,
    ref string tpDesign,
    IUserSession session)
  {
    if (tpName == null)
      throw new ArgumentNullException(nameof (tpName));
    if (tpDesign == null)
      throw new ArgumentNullException(nameof (tpDesign));
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(0L, tpTypeId);
    ObjInfoItem contextObjectItem = new ObjInfoItem(artObjId);
    ITechCardClassifyObjectService classifyObjectService = service;
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams objectAttributeParams = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
    objectAttributeParams.AttributeValues = (IEnumerable<AttributeValues>) new AttributeValues[1]
    {
      new AttributeValues(TechCardConsts.AttributeTypes.ProductionAttrID, (object) prodObjId)
    };
    TechCardClassifyObjectAttributeParams classifyParams = objectAttributeParams;
    TechCardClassifyTechProcessDesignationStrategy classifyStrategy = new TechCardClassifyTechProcessDesignationStrategy();
    ref string local = ref tpDesign;
    return classifyObjectService.ClassifyObjectAttribute(session1, classifyParams, (ITechCardClassifyObjectStrategy) classifyStrategy, out local) | service.ClassifyObjectAttribute(session, new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem), (ITechCardClassifyObjectStrategy) new TechCardClassifyObjectNameStrategy(), out tpName);
  }

  /// <summary>Классификация документа по обозначению</summary>
  /// <param name="text"></param>
  /// <param name="prodObjId"></param>
  /// <param name="tpTypeId"></param>
  /// <param name="tpDesign"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  [Obsolete("Use ITechCardClassifyObjectService service instead. Will be removed in IPS 8", true)]
  public static bool ClassifyTpDocDes(
    string text,
    long prodObjId,
    int tpTypeId,
    ref string tpDesign,
    IUserSession session)
  {
    if (tpDesign == null)
      throw new ArgumentNullException(nameof (tpDesign));
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(0L, tpTypeId);
    ObjInfoItem contextObjectItem = new ObjInfoItem(0L);
    IUserSession session1 = session;
    TechCardClassifyObjectAttributeParams classifyParams = new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem);
    TechCardClassifyTechProcessDesignationStrategy classifyStrategy = new TechCardClassifyTechProcessDesignationStrategy();
    ref string local = ref tpDesign;
    return service.ClassifyObjectAttribute(session1, classifyParams, (ITechCardClassifyObjectStrategy) classifyStrategy, out local);
  }

  /// <summary>
  /// Классификация объекта по изделию (в частности любому другому объекту)
  /// </summary>
  /// <param name="sourceObjId">Ид. версии объекта на основе которого будет проводиться классификация</param>
  /// <param name="destObjTypeId">Ид. типа классифицируемого объекта</param>
  /// <param name="objName">Наименование</param>
  /// <param name="objDesign">Обозначение</param>
  /// <param name="session"></param>
  /// <returns></returns>
  [Obsolete("Use ITechCardClassifyObjectService service instead. Will be removed in IPS 8", false)]
  public static bool ClassifyObj(
    long sourceObjId,
    int destObjTypeId,
    ref string objName,
    ref string objDesign,
    IUserSession session)
  {
    if (objName == null)
      throw new ArgumentNullException(nameof (objName));
    if (objDesign == null)
      throw new ArgumentNullException(nameof (objDesign));
    ITechCardClassifyObjectService service = ServiceUtils.GetService<ITechCardClassifyObjectService>((object) ApplicationServices.Container, true);
    ObjInfoItem classifyObjectItem = new ObjInfoItem(0L, destObjTypeId);
    ObjInfoItem contextObjectItem = new ObjInfoItem(sourceObjId);
    return service.ClassifyObjectAttribute(session, new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.DesignationAttrTypeID, classifyObjectItem, contextObjectItem), (ITechCardClassifyObjectStrategy) new TechCardClassifyObjectDesignationStrategy(), out objDesign) | service.ClassifyObjectAttribute(session, new TechCardClassifyObjectAttributeParams(TechCardConsts.AttributeTypes.NameAttrTypeID, classifyObjectItem, contextObjectItem), (ITechCardClassifyObjectStrategy) new TechCardClassifyObjectNameStrategy(), out objName);
  }

  /// <summary>
  /// Получения префикса типа документа по его ID типа объекта
  /// </summary>
  /// <param name="docTypeId"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  [Obsolete("Use ITechCardClassifyObjectService service instead. Will be removed in IPS 8", true)]
  public static string GetDocPrefix(int docTypeId, IUserSession session)
  {
    Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(docTypeId);
    if (objectTypeGuid.Equals(TechCardConsts.ObjectTypes.TechProcEdinGUID))
      return EnumTypeHelper.GetCaption((Enum) TpTypePrefixEnum.TechProcEdin);
    if (objectTypeGuid.Equals(TechCardConsts.ObjectTypes.TechProcGroupGUID))
      return EnumTypeHelper.GetCaption((Enum) TpTypePrefixEnum.TechProcGroup);
    if (objectTypeGuid.Equals(TechCardConsts.ObjectTypes.TechProcTipovGUID))
      return EnumTypeHelper.GetCaption((Enum) TpTypePrefixEnum.TechProcTipov);
    return objectTypeGuid.Equals(TechCardConsts.ObjectTypes.TechProcElemBaseGUID) ? EnumTypeHelper.GetCaption((Enum) TpTypePrefixEnum.TechProcElemBase) : "";
  }

  /// <summary>Получения префикса типа объекта по его ID</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  [Obsolete("Use ITechCardClassifyObjectService service instead. Will be removed in IPS 8", true)]
  public static string GetObjPrefix(int objectTypeId, IUserSession session)
  {
    return TechCardClassifyObjectService.GetObjectTypePostfix(objectTypeId);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objType"></param>
  /// <returns></returns>
  [Obsolete("Use MetaDataHelper instead. Will be removed in IPS 8", true)]
  public static List<int> GetObjectTypeHierchy(int objType)
  {
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objType);
    if (!objectTypeChildrenId.Contains(objType))
      objectTypeChildrenId.Add(objType);
    return objectTypeChildrenId;
  }

  /// <summary>Открыть объект в новом окне</summary>
  /// <param name="objectId"></param>
  public static void OpenObjectInNewWindow(long objectId)
  {
    if (objectId == 0L)
      return;
    Intermech.Navigator.Utils.OpenNewWindow((IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(objectId), (IServiceProvider) ApplicationServices.Container);
  }

  /// <summary>Получение списка "Видов производств"</summary>
  /// <returns>ArrayList of TechProduction</returns>
  /// <param name="session">Пользов. сессия</param>
  /// <param name="forUserOnly">Признак получения списка только для тек. пользователя (позже допишу)</param>
  public static List<TechProduction> GetTechProductions(IUserSession session, bool forUserOnly)
  {
    List<TechProduction> techProductions = new List<TechProduction>();
    IMSObjectType objectType = MetaDataHelper.GetObjectType(TechCardConsts.ObjectTypes.ProductTypeObjectGUID);
    if (objectType == null)
      return techProductions;
    DBRecordSetParams dbRsp = new DBRecordSetParams(new ConditionStructure[0], new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) -2, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0),
      new ColumnDescriptor((object) -50, AttributeSourceTypes.Auto, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.NONE, 0)
    });
    if (forUserOnly)
    {
      dbRsp.Tags = new HybridDictionary();
      dbRsp.Tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
    }
    DataTable objectData = DataHelper.GetObjectData(objectType.ObjectTypeID, session, dbRsp, (IEnumerable<long>) null);
    if (objectData != null && objectData.Rows.Count != 0)
    {
      foreach (DataRow row in (InternalDataCollectionBase) objectData.Rows)
        techProductions.Add(new TechProduction(Convert.ToInt64(row["F_OBJECT_ID"]), Convert.ToString(row["CAPTION"])));
    }
    return techProductions;
  }

  /// <summary>Помечаем объект как измененный</summary>
  /// <remarks>Для аннулирования подписей only</remarks>
  /// <param name="objectId"></param>
  /// <param name="session"></param>
  /// <returns></returns>
  public static bool MarkObjectAsModified(long objectId, IUserSession session)
  {
    if (objectId == 0L)
      return false;
    return session != null ? TechCardClientConst.MarkObjectAsModified(session.GetObject(objectId, false)) : throw new ArgumentNullException(nameof (session));
  }

  /// <summary>Помечаем объект как измененный</summary>
  /// <remarks>Для аннулирования подписей only</remarks>
  /// <param name="dbObject"></param>
  /// <returns></returns>
  public static bool MarkObjectAsModified(IDBObject dbObject)
  {
    if (dbObject == null || dbObject.ReadOnly)
      return false;
    Guid attributeGuid = new Guid("cad0013a-306c-11d8-b4e9-00304f19f545");
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(attributeGuid, false);
    if (attributeByGuid == null)
      return false;
    attributeByGuid.AsDateTime = DateTime.Now;
    return true;
  }

  /// <summary>
  /// Фильтрация / получения списка форм для создания объекта
  /// </summary>
  /// <param name="formList">Исходный список форм</param>
  /// <param name="session">Пользовательская сессия</param>
  /// <returns>"Отфильтрованный" список форм</returns>
  public static ICollection<FormInformation> GetFormsForObjectCreate(
    ICollection<FormInformation> formList,
    IUserSession session)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    if (formList == null || formList.Count == 0)
      return formList;
    List<FormInformation> formsForObjectCreate = new List<FormInformation>((IEnumerable<FormInformation>) formList);
    List<ObjInfoItem> objInfoItemList = new List<ObjInfoItem>(formList.Count);
    foreach (FormInformation form in (IEnumerable<FormInformation>) formList)
    {
      if (form.CheckOutBy == session.UserID && form.ID > 0L)
        objInfoItemList.Add(new ObjInfoItem(-form.ID, form.TypeID));
      else
        objInfoItemList.Add(new ObjInfoItem(form.ID, form.TypeID));
    }
    GenericListHelper.MakeUnique<ObjInfoItem>(objInfoItemList);
    DBRecordSetParams dbRsp = new DBRecordSetParams(new List<ConditionStructure>(1)
    {
      new ConditionStructure(new Guid("cadd9212-306c-11d8-b4e9-00304f19f545"), RelationalOperators.Equal, (object) true, LogicalOperators.NONE, 0)
    }.ToArray(), new List<ColumnDescriptor>(1)
    {
      new ColumnDescriptor((object) -2, ColumnContents.Text, ColumnNameMapping.FieldName, SortOrders.ASC, 0)
    }.ToArray());
    DataTable objectDataEx = DataHelper.GetObjectDataEx(MetaDataHelper.GetObjectTypeID("cad0011b-306c-11d8-b4e9-00304f19f545"), session, dbRsp, (IEnumerable<ObjInfoItem>) objInfoItemList);
    if (objectDataEx == null || objectDataEx.Rows.Count == 0)
      return (ICollection<FormInformation>) formsForObjectCreate;
    foreach (DataRow row in (InternalDataCollectionBase) objectDataEx.Rows)
    {
      long formId = Convert.ToInt64(row[0]);
      int index = formsForObjectCreate.FindIndex((Predicate<FormInformation>) (obj => Math.Abs(obj.ID) == Math.Abs(formId)));
      if (index != -1)
        formsForObjectCreate.RemoveAt(index);
    }
    return (ICollection<FormInformation>) formsForObjectCreate;
  }
}
