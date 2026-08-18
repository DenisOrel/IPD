// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Selection.ImbaseSelector
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Imbase.API;
using Intermech.Imbase.AttributesDescribers;
using Intermech.Imbase.Editors;
using Intermech.Imbase.Views;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Client.Imbase;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Params;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.PropertyEditors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Selection;

public class ImbaseSelector : IImbaseSelector
{
  private long _contextObjectId;
  private long _recordId;
  private long _linkID = -1;
  private long _recID = -1;
  private Lazy<TableRecordRefFlagConverter> _tableRecordRefFlagConverter = new Lazy<TableRecordRefFlagConverter>();
  private Lazy<TableRecordRefFlagEditor> _tableRecordRefFlagEditor = new Lazy<TableRecordRefFlagEditor>();
  private Lazy<TableRecordRefAttributesDescriber> _tableRecordRefAttributesDescriber = new Lazy<TableRecordRefAttributesDescriber>();

  public ImbaseSelector() => this._contextObjectId = -1L;

  public TypeConverter GetConverterForTableRecordRefFlag()
  {
    return (TypeConverter) this._tableRecordRefFlagConverter.Value;
  }

  public UITypeEditor GetEditorForTableRecordRefFlag()
  {
    return (UITypeEditor) this._tableRecordRefFlagEditor.Value;
  }

  public IAttributePropertyDescriber GetDescriberForTableRecordRefFlag()
  {
    return (IAttributePropertyDescriber) this._tableRecordRefAttributesDescriber.Value;
  }

  public long GetObjectIdByOldImbaseKey(string oldImbaseKey, int objectType, bool createIfNotFound)
  {
    if (oldImbaseKey == null || oldImbaseKey.Length <= 0)
      return -1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      return (session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).GetObjectIdByOldImbaseKey(session.SessionGUID, oldImbaseKey, objectType, createIfNotFound, out ScanOldKeyStatus _);
    }
  }

  public long GetObjectIdByImbaseKey(string imbaseKey, bool createIfNotFound)
  {
    if (imbaseKey == null)
      throw new ArgumentException(LocalizationHolder.rm.GetString(sc_7911.ssp_imbase_7912()));
    if (char.ToUpper(imbaseKey[0]) == 'I' && char.ToUpper(imbaseKey[1]) == 'K' && imbaseKey.IndexOf('.') != -1)
    {
      string empty = string.Empty;
      CadmechHelper.CreateObjectFromTempKey(imbaseKey, ref empty);
      imbaseKey = empty;
    }
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (char.ToUpper(imbaseKey[0]) == 'I')
      {
        if (char.ToUpper(imbaseKey[1]) == 'G')
        {
          Guid guid = new Guid(imbaseKey.Substring(2));
          IDBObject objectById = session.GetObjectByID(guid, true);
          return this.SelectByDefaultRule(session, objectById.ID);
        }
        if (char.ToUpper(imbaseKey[1]) == 'V')
        {
          Guid objectGUID = new Guid(imbaseKey.Substring(2));
          return session.GetObject(objectGUID, true).ObjectID;
        }
        if (char.ToUpper(imbaseKey[1]) == '6')
        {
          if (imbaseKey.Length == 20)
          {
            long idByOldImbaseKey = this.GetObjectIdByOldImbaseKey(imbaseKey, -1, createIfNotFound);
            if (idByOldImbaseKey == -1L)
              return idByOldImbaseKey;
            IDBObject dbObject = session.GetObject(idByOldImbaseKey, true);
            return this.SelectByDefaultRule(session, dbObject.ID);
          }
        }
      }
    }
    throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_92"));
  }

  private long SelectByDefaultRule(IUserSession session, long Id)
  {
    if (Id == -1L)
      return Id;
    IDBObject objectByVersionsRule = session.GetObjectByVersionsRule(Id, "cad005aa-306c-11d8-b4e9-00304f19f545", false);
    return objectByVersionsRule == null ? -1L : objectByVersionsRule.ObjectID;
  }

  public long SelectFromCatalog(
    string caption,
    string description,
    object catalogObject,
    bool rawObject,
    bool commitCreation,
    int[] allowedTypes,
    int needType,
    long contextObjectId,
    object selectedItemsAnalyzer = null)
  {
    return this.SelectFromCatalog(new ImbaseSelectorParams(caption, description, catalogObject, rawObject, commitCreation, allowedTypes, needType)
    {
      ContextObjectId = contextObjectId,
      SelectedItemsAnalyzer = selectedItemsAnalyzer
    });
  }

  public long SelectFromCatalog(ImbaseSelectorParams selectorParams)
  {
    if (selectorParams == null)
      throw new ArgumentNullException(nameof (selectorParams));
    if (selectorParams.CatalogObject == null)
      throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_93"), "CatalogObject");
    long num = -1;
    this._contextObjectId = -1L;
    long[] source = new long[1]
    {
      (long) -sc_7911.ssp_imbase_7913(1535889654)
    };
    List<int> rootObjTypeIDs = (List<int>) null;
    IDescriptor rootDescriptor;
    if (selectorParams.CatalogObject is DescriptorCollection catalogObject)
    {
      if (ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service)
        catalogObject.Add(service.GetMaterialsHandbookDescriptor());
      rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Каталоги и объекты", catalogObject);
      if (selectorParams.AllowedTypes != null)
        rootObjTypeIDs = new List<int>((IEnumerable<int>) selectorParams.AllowedTypes);
    }
    else
    {
      bool flag = false;
      if (!(selectorParams.CatalogObject is IDescriptor descriptor))
      {
        source = this.GetCatalogIds(selectorParams.CatalogObject);
        if (source.Length == 1 && source[0] == -1L)
          return -1;
        if (source.Length > 1)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            QuickObjectInfo objectInfo1 = sessionKeeper.Session.GetObjectInfo(new Guid("cad008db-306c-11d8-b4e9-00304f19f545"));
            QuickObjectInfo objectInfo2 = sessionKeeper.Session.GetObjectInfo(new Guid("cad008da-306c-11d8-b4e9-00304f19f545"));
            flag = ((IEnumerable<long>) source).Contains<long>(objectInfo1.ObjectID) && ((IEnumerable<long>) source).Contains<long>(objectInfo2.ObjectID);
          }
        }
        descriptor = this.GetRootDescriptor(((IEnumerable<long>) source).ToList<long>());
      }
      IIMHSelector service = ServicesManager.GetService(typeof (IIMHSelector)) as IIMHSelector;
      if (flag && service != null)
        rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor("Выбор материала", new DescriptorCollection()
        {
          service.GetMaterialsHandbookDescriptor(),
          descriptor
        });
      else
        rootDescriptor = descriptor;
      if (descriptor is ForCADMechDescriptor cadMechDescriptor)
        rootObjTypeIDs = cadMechDescriptor.TypeIDs;
    }
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (ImbaseHelper.GetImbaseDataFromObject(sessionKeeper.Session, selectorParams.ContextObjectId, ref this._linkID, ref this._recID))
          Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
      }
      if (!(selectorParams.SelectedItemsAnalyzer is ISelectedItemsAnalyzer analyzer))
        analyzer = (ISelectedItemsAnalyzer) new SelectedImbaseMaterialAnalizer(rootObjTypeIDs);
      Intermech.Navigator.SelectionWindow.RegisterAnalyze(analyzer, true);
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(selectorParams.Caption, selectorParams.Description, rootDescriptor, selectorParams.SelectionOptions);
      if (numArray != null)
      {
        if (numArray.Length != 0)
        {
          num = numArray[0];
          if (!selectorParams.RawObject)
          {
            using (SessionKeeper sessionKeeper = new SessionKeeper())
            {
              IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, true);
              if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
              {
                ImbaseObjectCaptionItem imbaseObject;
                if (this._contextObjectId == -1L)
                {
                  QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num);
                  imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.Caption), -1L);
                }
                else
                {
                  QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._contextObjectId);
                  imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.Caption), num);
                }
                if (!ImbaseUsageHelper.CanUseImbaseObject(imbaseObject))
                  return -1;
              }
              long catalogId = source.Length != 0 ? source[0] : -1L;
              if (this._contextObjectId == -1L)
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(num);
                if (objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseFolderTypeID)
                {
                  if (objectInfo.ObjectTypeID != Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID)
                    goto label_52;
                }
                num = service.CreateObject(sessionKeeper.Session.SessionGUID, catalogId, num, this._contextObjectId, selectorParams.CommitCreation, selectorParams.NeedType);
              }
              else
                num = service.CreateObject(sessionKeeper.Session.SessionGUID, catalogId, this._contextObjectId, num, selectorParams.CommitCreation, selectorParams.NeedType);
            }
          }
        }
      }
    }
    finally
    {
      Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
    }
label_52:
    return num;
  }

  public long SelectFromCatalog(
    string caption,
    string description,
    object catalogObject,
    bool rawObject,
    bool commitCreation,
    int[] allowedTypes,
    int needType)
  {
    return this.SelectFromCatalog(new ImbaseSelectorParams(caption, description, catalogObject, rawObject, commitCreation, allowedTypes, needType));
  }

  public long SelectFromCatalog(
    string caption,
    string description,
    object catalogObject,
    int needType,
    long contextObjsID)
  {
    long num = -1;
    this._contextObjectId = -1L;
    long[] numArray1 = catalogObject != null ? this.GetCatalogIds(catalogObject) : throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_93"), "catalogDef");
    IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.CustomNode.Descriptor(LocalizationHolder.rm.GetString("Imbase.Client_1148"), catalogObject as DescriptorCollection);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (ImbaseHelper.GetImbaseDataFromObject(sessionKeeper.Session, contextObjsID, ref this._linkID, ref this._recID))
          Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
      }
      SelectionOptions options = SelectionOptions.SelectObjects | SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect;
      long[] numArray2 = Intermech.Navigator.SelectionWindow.SelectObjects(caption, description, rootDescriptor, options);
      if (numArray2 != null)
      {
        if (numArray2.Length != 0)
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            if (ServiceUtils.GetService<IImbaseParamsService>((object) sessionKeeper.Session, true).CommonParams.CheckApplicabilityBeforeCreateComposition)
            {
              ImbaseObjectCaptionItem imbaseObject;
              if (this._contextObjectId == -1L)
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(numArray2[0]);
                imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.Caption), -1L);
              }
              else
              {
                QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._contextObjectId);
                imbaseObject = new ImbaseObjectCaptionItem((IObjInfoCaption) new ObjInfoCaptionItem(objectInfo.ObjectID, objectInfo.ObjectTypeID, objectInfo.Caption), numArray2[0]);
              }
              if (!ImbaseUsageHelper.CanUseImbaseObject(imbaseObject))
                return 0;
            }
            num = this._contextObjectId != -1L ? (sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).CreateObject(sessionKeeper.Session.SessionGUID, numArray1[0], this._contextObjectId, numArray2[0], true, needType) : numArray2[0];
          }
        }
      }
    }
    finally
    {
      Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
    }
    return num;
  }

  public long SelectFromCatalog(object catalogObject, long selectedID)
  {
    return this.SelectFromCatalog("", "", catalogObject, -1, 0L);
  }

  public Tuple<long, long> SelectRecord(string caption, string description, long contextObjsID)
  {
    Tuple<long, long> tuple = (Tuple<long, long>) null;
    long[] collection = (long[]) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        throw new Exception(LocalizationHolder.rm.GetString("Imbase_NullImbaseServer"));
      collection = customService.GetCatalogsList(sessionKeeper.Session.SessionGUID);
    }
    IDescriptor descriptor = (IDescriptor) new ImbaseRootNodeDescriptor(collection == null || collection.Length == 0 ? (List<long>) null : new List<long>((IEnumerable<long>) collection));
    if (ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service)
    {
      string caption1 = caption;
      string description1 = description;
      DescriptorCollection descriptorCollection = new DescriptorCollection();
      descriptorCollection.Add(descriptor);
      long contextObjsID1 = contextObjsID;
      tuple = service.SelectMaterial(caption1, description1, (object) descriptorCollection, contextObjsID1);
    }
    else
    {
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          if (ImbaseHelper.GetImbaseDataFromObject(sessionKeeper.Session, contextObjsID, ref this._linkID, ref this._recID))
            Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
        }
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedImbaseMaterialAnalizer(), true);
        SelectionOptions options = SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableSelectAbstractTypes | SelectionOptions.DisableMultiselect;
        long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(caption, description, descriptor, options);
        if (numArray != null)
        {
          if (numArray.Length != 0)
            tuple = this._contextObjectId != 0L ? new Tuple<long, long>(this._contextObjectId, numArray[0]) : new Tuple<long, long>(numArray[0], -1L);
        }
      }
      finally
      {
        Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
      }
    }
    return tuple;
  }

  public Tuple<long, long> SelectRecord(long imbaseObjID, string caption, string description)
  {
    Tuple<long, long> tuple = (Tuple<long, long>) null;
    IDescriptor rootDescriptor = (IDescriptor) new Intermech.Navigator.DBObjects.Descriptor(imbaseObjID);
    Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedImbaseMaterialAnalizer(), true);
    Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
    try
    {
      long[] numArray = Intermech.Navigator.SelectionWindow.SelectObjects(caption, description, rootDescriptor, SelectionOptions.DisableSelectFromTree | SelectionOptions.DisableMultiselect);
      if (numArray != null)
      {
        if (numArray.Length != 0)
          tuple = this._contextObjectId != 0L ? new Tuple<long, long>(this._contextObjectId, numArray[0]) : new Tuple<long, long>(numArray[0], -1L);
      }
    }
    finally
    {
      Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
    }
    return tuple;
  }

  public long CreateFromCatalog(object catalogObject, long selectedID)
  {
    long[] numArray = catalogObject != null ? this.GetCatalogIds(catalogObject) : throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_93"), "catalogDef");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IImbaseServer customService = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
      return this._contextObjectId == -1L ? selectedID : customService.CreateObject(session.SessionGUID, numArray[0], this._contextObjectId, selectedID, true, -1);
    }
  }

  public long[] DynamicSelection(
    string caption,
    string description,
    object catalogObject,
    bool rawObject,
    bool commitCreation,
    int needType,
    DynamicSelectionEventHandler dynamicSelection,
    long contextObjsID)
  {
    ArrayList arrayList = new ArrayList();
    long[] source = catalogObject != null ? this.GetCatalogIds(catalogObject) : throw new ArgumentException(LocalizationHolder.rm.GetString("Imbase.Client_94"), "catalogDef");
    IDescriptor rootDescriptor = this.GetRootDescriptor(((IEnumerable<long>) source).ToList<long>());
    DynamicSelectionHelper dynamicSelectionHelper = new DynamicSelectionHelper((IImbaseSelector) this, arrayList, dynamicSelection, source[0], needType, rawObject, commitCreation);
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        if (ImbaseHelper.GetImbaseDataFromObject(sessionKeeper.Session, contextObjsID, ref this._linkID, ref this._recID))
          Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
      }
      if (needType > -1)
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new TypedObjectsSelectedItemsAnalyzer(needType, true), true);
      else
        Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedRecordAnalizer((List<int>) null), true);
      Intermech.Navigator.SelectionWindow.DynamicSelectObjects(caption, description, rootDescriptor, new DynamicSelectionEventHandler(dynamicSelectionHelper.Handler), SelectionOptions.Default);
    }
    finally
    {
      Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
    }
    DynamicSelectionHelper.Clear();
    return arrayList.Cast<long>().ToArray<long>();
  }

  public long[] DynamicSelection(
    string caption,
    string description,
    object catalogObject,
    bool rawObject,
    bool commitCreation,
    int needType,
    DynamicSelectionEventHandler dynamicSelection)
  {
    return this.DynamicSelection(caption, description, catalogObject, rawObject, commitCreation, needType, dynamicSelection, -1L);
  }

  public long ContextObjectId
  {
    get => this._contextObjectId;
    set => this._contextObjectId = value;
  }

  public long RecordId
  {
    get => this._recordId;
    set => this._recordId = value;
  }

  public long[] CatalogsForObjectAtt(int objTypeId, int attTypeId)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IDBAttributeType attributeType = session.GetAttributeType(attTypeId);
      DBRecordSetParams paramSet = new DBRecordSetParams();
      paramSet.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      paramSet.ColumnNames = new ColumnNameMapping[1]
      {
        ColumnNameMapping.ID
      };
      ConditionStructure conditionStructure = new ConditionStructure(Intermech.Imbase.Consts.ObjectTypeAndAttCatalogLink, RelationalOperators.Equal, (object) string.Empty, LogicalOperators.NONE, 0);
      paramSet.Conditions = new ConditionStructure[1]
      {
        conditionStructure
      };
      paramSet.TableName = "tbl";
      paramSet.RecordCount = -1;
      IDBObjectCollection objectCollection = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeGUID);
      DataTable dataTable;
      do
      {
        IDBObjectType objectType = session.GetObjectType(objTypeId);
        string str = ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue.FormatGuids(objectType.PropertiesStructure.ObjectTypeGuid, attributeType.PropertiesStructure.AttributeGuid);
        paramSet.Conditions[0].Value = (object) str;
        dataTable = objectCollection.Select(paramSet);
        if (dataTable.Rows.Count == 0)
          objTypeId = objectType.ParentTypeID;
        else
          goto label_4;
      }
      while (objTypeId != -1);
      goto label_15;
label_4:
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64 = Convert.ToInt64(row[0]);
        if (!longList.Contains(int64))
          longList.Add(int64);
      }
    }
label_15:
    return longList.ToArray();
  }

  public List<ImbaseObjectAttrLink> GetImbaseObjectAttrLinks(int objectType)
  {
    List<ImbaseObjectAttrLink> imbaseObjectAttrLinks1 = new List<ImbaseObjectAttrLink>();
    if (objectType == 0)
      return imbaseObjectAttrLinks1;
    List<ImbaseObjectAttrLink> imbaseObjectAttrLinks2 = this.GetImbaseObjectAttrLinks();
    if (imbaseObjectAttrLinks2 == null || imbaseObjectAttrLinks2.Count == 0)
      return imbaseObjectAttrLinks1;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      List<int> intList = new List<int>();
      int anObjectType = objectType;
      while (anObjectType != -1)
      {
        IDBObjectType objectType1 = session.GetObjectType(anObjectType, false);
        if (objectType1 != null)
        {
          intList.Add(anObjectType);
          anObjectType = objectType1.ParentTypeID;
        }
        else
          anObjectType = -1;
      }
      foreach (ImbaseObjectAttrLink imbaseObjectAttrLink in imbaseObjectAttrLinks2)
      {
        if (intList.Contains(imbaseObjectAttrLink._objectTypeID))
          imbaseObjectAttrLinks1.Add(imbaseObjectAttrLink);
      }
    }
    return imbaseObjectAttrLinks1;
  }

  public List<ImbaseObjectAttrLink> GetImbaseObjectAttrLinks()
  {
    List<ImbaseObjectAttrLink> imbaseObjectAttrLinks = new List<ImbaseObjectAttrLink>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      DBRecordSetParams paramSet = new DBRecordSetParams();
      paramSet.Columns = new object[1]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID
      };
      paramSet.ColumnNames = new ColumnNameMapping[1]
      {
        ColumnNameMapping.ID
      };
      ConditionStructure conditionStructure = new ConditionStructure(Intermech.Imbase.Consts.ObjectTypeAndAttCatalogLink, RelationalOperators.NotEmpty, (object) null, LogicalOperators.NONE, 0);
      paramSet.Conditions = new ConditionStructure[1]
      {
        conditionStructure
      };
      paramSet.TableName = "tbl";
      paramSet.RecordCount = -1;
      DataTable dataTable = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseCatalogTypeGUID).Select(paramSet);
      if (dataTable != null)
      {
        if (dataTable.Rows.Count != 0)
        {
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            long int64 = Convert.ToInt64(row[0]);
            if (int64 == 0L)
            {
              IDBObject dbObject = session.GetObject(int64, false);
              if (dbObject != null)
              {
                IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(Intermech.Imbase.Consts.ObjectTypeAndAttCatalogLink, false);
                if (attributeByGuid != null && attributeByGuid.ValuesCount != 0)
                {
                  for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
                  {
                    ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue andAttribiteValue = new ObjectAndAttLinkAttDescriber.TypeAndAttribiteValue(attributeByGuid.Values[index]);
                    if (andAttribiteValue.ObjectTypeId != -1)
                      imbaseObjectAttrLinks.Add(new ImbaseObjectAttrLink(andAttribiteValue.ObjectTypeId, andAttribiteValue.AttTypeId, int64));
                  }
                }
              }
            }
          }
        }
      }
    }
    return imbaseObjectAttrLinks;
  }

  public IDescriptor GetRootDescriptor(List<long> catalogIDs)
  {
    return catalogIDs == null || catalogIDs.Count <= 0 ? (IDescriptor) new ImbaseRootNodeDescriptor() : (IDescriptor) new ImbaseRootNodeDescriptor(new List<long>((IEnumerable<long>) catalogIDs));
  }

  public List<long> SelectImbaseObjects(
    List<long> catalogIDs = null,
    List<int> typeIDs = null,
    IServiceProvider services = null)
  {
    List<long> longList = (List<long>) null;
    IDescriptor rootDescriptor = (IDescriptor) new ImbaseRootNodeDescriptor(catalogIDs);
    if (typeIDs != null && typeIDs.Count > 0)
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new ObjectTypesSelectedItemsAnalyzer(typeIDs, false), true);
    long[] source1 = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase_SelectionWindow_Description"), "", rootDescriptor, services, SelectionOptions.SelectObjects);
    if (source1 != null && source1.Length != 0)
    {
      IEnumerable<long> source2 = ((IEnumerable<long>) source1).Where<long>((System.Func<long, bool>) (x => x != 0L));
      longList = source2.Count<long>() > 0 ? source2.ToList<long>() : (List<long>) null;
    }
    return longList;
  }

  public IDescriptor GetImbaseDescriptor(int objectTypeId = -1, int attributeId = 0)
  {
    List<long> catalogIDs = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (attributeId != 0)
      {
        ImbaseExtendedItem imbaseExtendedItem1 = ExtendedServiceHelper.GetImbaseExtendedItem(sessionKeeper.Session, objectTypeId, attributeId);
        if (imbaseExtendedItem1 != null)
        {
          foreach (long catalogId in imbaseExtendedItem1.CatalogIDs)
          {
            if (catalogId != 0L)
              catalogIDs.Add(catalogId);
          }
        }
        if (catalogIDs.Count == 0 && objectTypeId != -1)
        {
          ImbaseExtendedItem imbaseExtendedItem2 = ExtendedServiceHelper.GetImbaseExtendedItem(sessionKeeper.Session, -1, attributeId);
          if (imbaseExtendedItem2 != null)
          {
            foreach (long catalogId in imbaseExtendedItem2.CatalogIDs)
            {
              if (catalogId != 0L)
                catalogIDs.Add(catalogId);
            }
          }
        }
      }
      if (catalogIDs.Count == 0)
      {
        IImbaseServer service = ServiceUtils.GetService<IImbaseServer>((object) sessionKeeper.Session, false);
        if (service != null)
        {
          long[] catalogsList = service.GetCatalogsList(sessionKeeper.Session.SessionGUID);
          if (catalogsList != null)
          {
            if (catalogsList.Length != 0)
              catalogIDs.AddRange((IEnumerable<long>) catalogsList);
          }
        }
      }
    }
    return this.GetRootDescriptor(catalogIDs);
  }

  private long GetCatalogId(object catalogObject)
  {
    long catalogId = -1;
    switch (catalogObject)
    {
      case long _:
        catalogId = Convert.ToInt64(catalogObject);
        break;
      case Guid _:
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          catalogId = sessionKeeper.Session.GetObject((Guid) catalogObject).ObjectID;
          break;
        }
      case string _:
        if (this.IsStringGuld((object) (string) catalogObject))
        {
          using (SessionKeeper sessionKeeper = new SessionKeeper())
          {
            catalogId = sessionKeeper.Session.GetObject(new Guid((string) catalogObject)).ObjectID;
            break;
          }
        }
        break;
    }
    return catalogId;
  }

  private long[] GetCatalogIds(object catalogObjectDef)
  {
    if (catalogObjectDef is IEnumerable enumerable)
    {
      List<long> longList = new List<long>();
      foreach (object catalogObject in enumerable)
        longList.Add(this.GetCatalogId(catalogObject));
      return longList.ToArray();
    }
    return new long[1]
    {
      this.GetCatalogId(catalogObjectDef)
    };
  }

  private bool IsStringGuld(object obj)
  {
    if (!(obj is string str1) || str1.IndexOf('-', 0) < 0)
      return false;
    int num = 0;
    string str2 = str1.Trim();
    if (str2[0] == '{')
    {
      if (str2.Length != 38 || str2[37] != '}')
        return false;
      num = 1;
    }
    else if (str2[0] == '(')
    {
      if (str2.Length != 38 || str2[37] != ')')
        return false;
      num = 1;
    }
    else if (str2.Length != 36)
      return false;
    return str2[8 + num] == '-' && str2[13 + num] == '-' && str2[18 + num] == '-' && str2[23 + num] == '-';
  }

  private void SelectionWindow_OnSelectionWindowBeforeShow(object sender, EventArgs e)
  {
    if (this._linkID <= -1L)
      return;
    NavigatorTreeView navTreeView = (sender as Intermech.Navigator.Controls.SelectionWindow).TreeViewsBridge.NavTreeView;
    if (navTreeView == null)
      return;
    NavigatorTreeNode node = (NavigatorTreeNode) null;
    if (!(ServicesManager.GetService(typeof (IIMHSelector)) is IIMHSelector service) || !service.SearchAndSelectMaterial((object) navTreeView.RootNode, this._linkID, this._recID))
      node = FindHelper.SearchNodeByNodeID(navTreeView.RootNode, this._linkID);
    if (node == null || !FindHelper.IsValidNode(node))
      return;
    ISelectedItemsHost itemsHost = sender is ICurrentSelectedItemsHost selectedItemsHost ? selectedItemsHost.ItemsHost : (ISelectedItemsHost) null;
    if (!node.HasFocus)
      node.Focus();
    if (itemsHost != null)
      selectedItemsHost.ItemsHost = itemsHost;
    if (this._recID <= -1L)
      return;
    SelectedRecords.Clear();
    SelectedRecords.Add(this._linkID, new long[1]
    {
      this._recID
    });
    NodeIDPath nodeIdPath = navTreeView.GetNodeIDPath(node);
    if (nodeIdPath == null)
      return;
    navTreeView.TryBrowse(nodeIdPath);
  }

  public string SelectRecord(string strImbaseKey, bool useGuid)
  {
    this._contextObjectId = -1L;
    long[] numArray1 = new long[0];
    IDescriptor rootDescriptor = (IDescriptor) null;
    bool flag = false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
        return string.Empty;
      long[] catalogsList = customService.GetCatalogsList(sessionKeeper.Session.SessionGUID);
      rootDescriptor = (IDescriptor) new ListDescriptor(Intermech.Imbase.Consts.CatalogsListCategoryId, Intermech.Imbase.Consts.ImbaseCatalogTypeID, LocalizationHolder.rm.GetString(sc_7911.ssp_imbase_7914()), (IList) catalogsList);
      flag = false;
      this._linkID = -1L;
      if (!string.IsNullOrEmpty(strImbaseKey) && strImbaseKey.StartsWith("IK", StringComparison.InvariantCultureIgnoreCase))
      {
        int num = strImbaseKey.IndexOf('.');
        if (num != -1)
        {
          string str = strImbaseKey.Substring(2, num - 2);
          string s = strImbaseKey.Substring(num + 1);
          if (!long.TryParse(str, out this._linkID))
          {
            try
            {
              Guid objectGUID = new Guid(str);
              this._linkID = sessionKeeper.Session.GetObjectInfo(objectGUID).ObjectID;
            }
            catch
            {
              this._linkID = -1L;
            }
          }
          long.TryParse(s, out this._recID);
          if (this._linkID != -1L)
          {
            Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
            flag = true;
          }
        }
      }
      Intermech.Navigator.SelectionWindow.RegisterAnalyze((ISelectedItemsAnalyzer) new SelectedRecordAnalizer((List<int>) null), true);
    }
    long[] numArray2 = Intermech.Navigator.SelectionWindow.SelectObjects(LocalizationHolder.rm.GetString("Imbase_SelectionWindow_Caption"), LocalizationHolder.rm.GetString("Imbase_SelectionWindow_Description"), rootDescriptor, SelectionOptions.Default);
    if (flag)
      Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(this.SelectionWindow_OnSelectionWindowBeforeShow);
    if (numArray2 == null || numArray2.Length == 0)
      return string.Empty;
    if (!useGuid)
      return ImbaseHelper.MakeInternalImbaseKey(this._contextObjectId, numArray2[0]);
    string empty = string.Empty;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      empty = sessionKeeper.Session.GetObjectInfo(this._contextObjectId).VersionGuid.ToString();
    return $"IK{empty}.{numArray2[0]}";
  }
}
