// Decompiled with JetBrains decompiler
// Type: Intermech.Search.EditingContexts.EditingContextServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Data.Repositories;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Search.EditingContexts;

public sealed class EditingContextServerService : LongLifeObject, IEditingContextServerService
{
  private LazyService<IObjectRepository> _objectRepository = new LazyService<IObjectRepository>();

  public EditingContext FindEditingContext(
    Guid userSessionGuid,
    FindEditingContextParams findEditingContextParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (findEditingContextParams == null)
        throw new ArgumentNullException(nameof (findEditingContextParams));
      return FindEditingContextParams.Check(findEditingContextParams) ? this.FindEditingContextInternal(findEditingContextParams) : throw new ArgumentException();
    }
  }

  public EditingContext[] FindLinkedEdintingContexts(
    Guid userSessionGuid,
    FindEditingContextParams findEditingContextParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (findEditingContextParams == null)
        throw new ArgumentNullException(nameof (findEditingContextParams));
      return FindEditingContextParams.Check(findEditingContextParams) ? this.FindLinkedEditingContextsInternal(findEditingContextParams) : throw new ArgumentException();
    }
  }

  public void SaveEditingContext(
    Guid userSessionGuid,
    SaveEditingContextParams saveEditingContextParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (saveEditingContextParams == null)
        throw new ArgumentNullException(nameof (saveEditingContextParams));
      if (!SaveEditingContextParams.Check(saveEditingContextParams))
        throw new ArgumentException();
      this.SaveEditingContextInternal(saveEditingContextParams);
    }
  }

  public AddObjectsToEditingContextResult AddObjectsToEditingContext(
    Guid userSessionGuid,
    AddObjectsToEditingContextParams addObjectsToEditingContextParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (addObjectsToEditingContextParams == null)
        throw new ArgumentNullException(nameof (addObjectsToEditingContextParams));
      return AddObjectsToEditingContextParams.Check(addObjectsToEditingContextParams) ? this.AddObjectsToEditingContextInternal(addObjectsToEditingContextParams) : throw new ArgumentException();
    }
  }

  public void ReplaceVersionInEditingContext(
    Guid userSessionGuid,
    long objectVersionID,
    long replacementVersionID,
    long editingContextVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(replacementVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(editingContextVersionID))
        throw new ArgumentException();
      this.ReplaceVersionInEditingContextInternal(objectVersionID, replacementVersionID, editingContextVersionID);
    }
  }

  public _Object[] FindObjectsForAddToEditingContext(
    Guid userSessionGuid,
    AddObjectsToEditingContextParams addObjectsToEditingContextParams)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (addObjectsToEditingContextParams == null)
        throw new ArgumentException();
      return AddObjectsToEditingContextParams.Check(addObjectsToEditingContextParams) ? this.FindObjectsForAddToEditingContextInternal(addObjectsToEditingContextParams) : throw new ArgumentException();
    }
  }

  public bool CheckEditingContextEditRights(Guid userSessionGuid, long editingContextVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(editingContextVersionID) ? this.CheckEditingContextEditRightsInternal(editingContextVersionID) : throw new ArgumentException();
  }

  public void RemoveNotVersionedObjectsFromAllEditingContexts(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      this.RemoveNotVersionedObjectsFromAllEditingContextsInternal();
  }

  public long[] FindProductsForDocuments(Guid userSessionGuid, long[] documentVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.FindProductsForDocumentsInternal(documentVersionIds);
  }

  private EditingContext FindEditingContextInternal(
    FindEditingContextParams findEditingContextParams)
  {
    return this.FindEditingContextInternal(findEditingContextParams, findEditingContextParams.EditingContextVersionID);
  }

  private EditingContext FindEditingContextInternal(
    FindEditingContextParams findEditingContextParams,
    long editingContextVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBEditingContextsServerService service = ServerServices.GetService(typeof (IDBEditingContextsServerService)) as IDBEditingContextsServerService;
      if (!(sessionKeeper.Session.GetObject(editingContextVersionID, false) is IDBEditingContextsObject))
      {
        editingContextVersionID = -editingContextVersionID;
        sessionKeeper.Session.GetObject(editingContextVersionID);
      }
      IUserSession session = sessionKeeper.Session;
      long ContextID = editingContextVersionID;
      EditingContextsObjectContainer editingContextsObject = service.GetEditingContextsObject((object) session, ContextID, true, false);
      List<ObjectVersionDescription> descriptions = editingContextsObject.Descriptions;
      Dictionary<int, List<long>> dictionary = new Dictionary<int, List<long>>();
      List<long> longList1 = new List<long>();
      foreach (ObjectVersionDescription versionDescription in descriptions)
      {
        ObjectVersionDescription objectVersionDescription = versionDescription;
        EditingContextsObjectVersion contextsObjectVersion = editingContextsObject.Objects.FirstOrDefault<EditingContextsObjectVersion>((System.Func<EditingContextsObjectVersion, bool>) (o => Math.Abs(o.F_OBJECT_ID) == Math.Abs(objectVersionDescription.F_OBJECT_ID)));
        if (contextsObjectVersion != null && Math.Abs(contextsObjectVersion.F_CONTEXT_ID) == Math.Abs(editingContextVersionID))
        {
          List<long> longList2 = (List<long>) null;
          if (!dictionary.TryGetValue(objectVersionDescription.F_OBJECT_TYPE, out longList2))
          {
            longList2 = new List<long>();
            dictionary.Add(objectVersionDescription.F_OBJECT_TYPE, longList2);
          }
          if (!longList2.Contains(objectVersionDescription.F_OBJECT_ID))
            longList2.Add(objectVersionDescription.F_OBJECT_ID);
          if (objectVersionDescription.Options.HasFlag((Enum) ObjectVersionDescriptionOptions.FromECOComposition))
            longList1.Add(objectVersionDescription.F_OBJECT_ID);
        }
      }
      List<int> intList = new List<int>()
      {
        -2,
        -3,
        -7,
        -5,
        -6,
        -15
      };
      if (findEditingContextParams.AttributeTypeIds != null)
      {
        foreach (int attributeTypeId in findEditingContextParams.AttributeTypeIds)
        {
          if (!intList.Contains(attributeTypeId) && attributeTypeId != -77)
            intList.Add(attributeTypeId);
        }
      }
      List<_Object> objectList = this._objectRepository.Value.Find(new FindObjectCollectionOptions()
      {
        AttributeTypeIds = intList.ToArray(),
        DisableEditingContextFiltration = true,
        ObjectVersionIdsByObjectTypeIDDictionary = dictionary
      });
      EditingContext editingContextInternal = new EditingContext(this._objectRepository.Value.Find(editingContextVersionID, false));
      foreach (_Object @object in objectList)
      {
        if (@object.VersionID != editingContextInternal.Object.VersionID)
          editingContextInternal.Items.Add(new EditingContextItem(@object)
          {
            ReadOnly = longList1.Contains(@object.VersionID)
          });
      }
      return editingContextInternal;
    }
  }

  private EditingContext[] FindLinkedEditingContextsInternal(
    FindEditingContextParams findEditingContextParams)
  {
    List<EditingContext> editingContextList = new List<EditingContext>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBEditingContextsService customService = sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService;
      IDBEditingContextsObject editingContextsObject = sessionKeeper.Session.GetObject(findEditingContextParams.EditingContextVersionID) as IDBEditingContextsObject;
      // ISSUE: variable of a boxed type
      __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
      long linkedContextNumber = editingContextsObject.LinkedContextNumber;
      foreach (long editingContextVersionID in customService.GetLinkedContexts((object) sessionGuid, linkedContextNumber) ?? new List<long>(0))
      {
        if (editingContextVersionID != findEditingContextParams.EditingContextVersionID)
        {
          EditingContext editingContextInternal = this.FindEditingContextInternal(findEditingContextParams, editingContextVersionID);
          editingContextList.Add(editingContextInternal);
        }
      }
    }
    return editingContextList.ToArray();
  }

  private void SaveEditingContextInternal(SaveEditingContextParams saveEditingContextParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBEditingContextsService customService = sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService;
      IDBEditingContextsObject editingContextsObject = sessionKeeper.Session.GetObject(saveEditingContextParams.EditingContextVersionID) as IDBEditingContextsObject;
      List<EditingContextsObjectVersion> objects = (List<EditingContextsObjectVersion>) null;
      if (saveEditingContextParams.ObjectVersionIds != null && saveEditingContextParams.ObjectVersionIds.Length != 0)
        objects = this.CreateEditingContextsObjectVersions(saveEditingContextParams.ObjectVersionIds, saveEditingContextParams.EditingContextVersionID, editingContextsObject.LinkedContextNumber);
      EditingContextsObjectContainer contextsObjectContainer = new EditingContextsObjectContainer(saveEditingContextParams.EditingContextVersionID, editingContextsObject.LinkedContextNumber, editingContextsObject.TypeID, objects, (List<ObjectVersionDescription>) null);
      // ISSUE: variable of a boxed type
      __Boxed<Guid> sessionGuid = (System.ValueType) sessionKeeper.Session.SessionGUID;
      EditingContextsObjectContainer context = contextsObjectContainer;
      customService.SetEditingContextsObject((object) sessionGuid, context, true);
    }
  }

  private _Object[] FindObjectsForAddToEditingContextInternal(
    AddObjectsToEditingContextParams addObjectsToEditingContextParams)
  {
    List<int> intList = new List<int>()
    {
      -2,
      -3,
      -7,
      -6,
      -5,
      -15
    };
    if (addObjectsToEditingContextParams.AttributeTypeIds != null)
    {
      foreach (int attributeTypeId in addObjectsToEditingContextParams.AttributeTypeIds)
      {
        if (!intList.Contains(attributeTypeId) && attributeTypeId != -77)
          intList.Add(attributeTypeId);
      }
    }
    if (addObjectsToEditingContextParams.Type == AddObjectsToEditingContextType.Objects)
      return this._objectRepository.Value.Find(new FindObjectCollectionOptions()
      {
        ObjectVersionIds = addObjectsToEditingContextParams.ObjectVersionIds,
        AttributeTypeIds = intList.ToArray(),
        DisableEditingContextFiltration = true
      }).ToArray();
    List<_Object> objectList = new List<_Object>();
    FindObjectCollectionOptions options = new FindObjectCollectionOptions()
    {
      ObjectVersionIds = addObjectsToEditingContextParams.ObjectVersionIds,
      AttributeTypeIds = intList.ToArray(),
      DisableEditingContextFiltration = true
    };
    objectList.AddRange((IEnumerable<_Object>) this._objectRepository.Value.Find(options));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ICompositionRepositoryServerService customService = sessionKeeper.Session.GetCustomService(typeof (ICompositionRepositoryServerService)) as ICompositionRepositoryServerService;
      FindCompositionParams findCompositionParams = new FindCompositionParams()
      {
        AllRelations = true,
        ObjectAttributeTypeIds = intList.ToArray(),
        ProjectVersionIds = addObjectsToEditingContextParams.ObjectVersionIds
      };
      CompositionPart[] source = addObjectsToEditingContextParams.Type != AddObjectsToEditingContextType.ObjectsWithComposition ? customService.FindRecursiveComposition(sessionKeeper.Session.SessionGUID, findCompositionParams) : customService.FindComposition(sessionKeeper.Session.SessionGUID, findCompositionParams);
      objectList.AddRange(((IEnumerable<CompositionPart>) source).Select<CompositionPart, _Object>((System.Func<CompositionPart, _Object>) (o => o.Object)));
    }
    return objectList.ToArray();
  }

  private AddObjectsToEditingContextResult AddObjectsToEditingContextInternal(
    AddObjectsToEditingContextParams addObjectsToEditingContextParams)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!this.CheckEditingContextEditRights(sessionKeeper.Session.SessionGUID, addObjectsToEditingContextParams.EditingContextVersionID))
        throw new Exception("Редактирование контекста редактирования запрещено. Убедитесь, что у вас  есть права на изменение контекста редактирования и он не взят на изменение другим пользователем.");
    }
    FindEditingContextParams findEditingContextParams = new FindEditingContextParams(addObjectsToEditingContextParams.EditingContextVersionID);
    EditingContext editingContextInternal = this.FindEditingContextInternal(findEditingContextParams);
    EditingContext[] contextsInternal = this.FindLinkedEditingContextsInternal(findEditingContextParams);
    AddObjectsToEditingContextResult editingContext = EditingContextsHelper.AddObjectsToEditingContext(this.FindObjectsForAddToEditingContextInternal(addObjectsToEditingContextParams), editingContextInternal, contextsInternal);
    if (editingContext.AddedObjectsCount <= 0)
      return editingContext;
    this.SaveEditingContextInternal(new SaveEditingContextParams(addObjectsToEditingContextParams.EditingContextVersionID)
    {
      ObjectVersionIds = editingContextInternal.Items.Select<EditingContextItem, long>((System.Func<EditingContextItem, long>) (o => o.Object.VersionID)).ToArray<long>()
    });
    return editingContext;
  }

  private void ReplaceVersionInEditingContextInternal(
    long objectVersionID,
    long replacementVersionID,
    long editingContextVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBEditingContextsService customService = sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService;
      EditingContextsObjectContainer editingContextsObject = customService.GetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, editingContextVersionID, true, true);
      IDBObject dbObject = sessionKeeper.Session.GetObject(replacementVersionID);
      if (dbObject.ModificationID == editingContextsObject.ModificationID)
        throw new Exception("Ошибка замены версии в контектсте. Версия уже присутствует в контексте или в связанном контексте");
      if (!ObjectHelper.IsUnknownObjectModificationID(dbObject.ModificationID) && dbObject.ModificationID != editingContextsObject.ModificationID)
        throw new Exception("Ошибка замены версии в контектсте. Версия уже присутствует в другом контексте");
      if (!(ObjectVersionDescriptionsHelper.LoadDescription(sessionKeeper.Session, typeof (ObjectVersionDescription), replacementVersionID) is ObjectVersionDescription newVerDesc))
        return;
      EditingContextsObjectVersion newVersion = new EditingContextsObjectVersion(editingContextsObject.ContextID, newVerDesc.F_ID, Math.Abs(newVerDesc.F_OBJECT_ID), editingContextsObject.ModificationID);
      editingContextsObject.ReplaceVersion(objectVersionID, newVersion, newVerDesc);
      customService.SetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, editingContextsObject, true);
    }
  }

  private List<EditingContextsObjectVersion> CreateEditingContextsObjectVersions(
    long[] objectVersionIds,
    long editingContextVersionID,
    long modificationID)
  {
    List<EditingContextsObjectVersion> contextsObjectVersions = new List<EditingContextsObjectVersion>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      objectCollection.LocalTypesMode = true;
      objectCollection.ShowAllModifications = true;
      DBRecordSetParams paramSet;
      // ISSUE: explicit reference operation
      ^ref paramSet = new DBRecordSetParams(new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          RelationalOperator = RelationalOperators.In,
          Value = (object) ((IEnumerable<long>) objectVersionIds).Distinct<long>().ToArray<long>(),
          SQL = ""
        }
      }, new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_ID
      }, 0L, (object) null, -1);
      foreach (DataRow row in (InternalDataCollectionBase) objectCollection.Select(paramSet).Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        EditingContextsObjectVersion contextsObjectVersion = new EditingContextsObjectVersion(editingContextVersionID, int64Value2, int64Value1, modificationID);
        contextsObjectVersions.Add(contextsObjectVersion);
      }
    }
    return contextsObjectVersions;
  }

  private bool CheckEditingContextEditRightsInternal(long editingContextVersionID)
  {
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObject(editingContextVersionID);
        bool flag = false;
        if (dbObject is IDBSecurity dbSecurity && dbSecurity.CheckAccess(ActionType.Edit, true, false))
          flag = true;
        if (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify)
          flag = false;
        if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.CheckoutBy != sessionKeeper.Session.UserID && dbObject.CheckoutBy != 0L)
          flag = false;
        if (dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion && dbObject.CheckoutBy != sessionKeeper.Session.UserID && dbObject.CheckoutBy != 0L)
          flag = false;
        return flag;
      }
    }
    catch
    {
      return false;
    }
  }

  private void RemoveNotVersionedObjectsFromAllEditingContextsInternal()
  {
    int[] array1 = MetaDataHelper.GetEditingContextObjectsIDs().ToArray();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
      objectCollection.LocalTypesMode = true;
      DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
      dbRecordSetParams.Columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
      };
      // ISSUE: explicit reference operation
      (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
      {
        new ConditionStructure()
        {
          Attribute = (object) ObligatoryObjectAttributes.F_OBJECT_TYPE,
          RelationalOperator = RelationalOperators.In,
          Value = (object) array1,
          SQL = string.Empty
        }
      };
      dbRecordSetParams.RecordCount = -1;
      DBRecordSetParams paramSet = dbRecordSetParams;
      DataTable dataTable = objectCollection.Select(paramSet);
      IDBEditingContextsService customService = sessionKeeper.Session.GetCustomService(typeof (IDBEditingContextsService)) as IDBEditingContextsService;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
        DataSetProcessor.GetInt32Value(row, 1, -1);
        EditingContextsObjectContainer editingContextsObjectContainer = customService.GetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, int64Value, true, true);
        if (editingContextsObjectContainer != null && editingContextsObjectContainer.Descriptions != null)
        {
          ObjectVersionDescription[] array2 = ((IEnumerable<ObjectVersionDescription>) editingContextsObjectContainer.Descriptions.ToArray()).Where<ObjectVersionDescription>((System.Func<ObjectVersionDescription, bool>) (o => !ObjectTypeHelper.IsVersionedObjectTypeID(o.F_OBJECT_TYPE) && editingContextsObjectContainer.ExistsVersion(o.F_OBJECT_ID, false))).ToArray<ObjectVersionDescription>();
          foreach (ObjectVersionDescription versionDescription in array2)
            editingContextsObjectContainer.DeleteVersion(versionDescription.F_OBJECT_ID);
          if (array2.Length != 0)
            customService.SetEditingContextsObject((object) sessionKeeper.Session.SessionGUID, editingContextsObjectContainer, true, true, true);
        }
      }
    }
  }

  private long[] FindProductsForDocumentsInternal(long[] documentVersionIds)
  {
    List<long> longList = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(EditingContextConstants.DocumentationRelationTypeID);
      relationCollection.ChildObjectTypes = (IList<int>) EditingContextConstants.ProductObjectTypesIds;
      foreach (long documentVersionId in documentVersionIds)
      {
        DBRecordSetParams dbRecordSetParams = new DBRecordSetParams();
        dbRecordSetParams.Columns = new object[1]
        {
          (object) ObligatoryObjectAttributes.F_PROJ_ID
        };
        // ISSUE: explicit reference operation
        (^ref dbRecordSetParams).Conditions = new ConditionStructure[1]
        {
          new ConditionStructure()
          {
            Attribute = (object) Constants.ExplicitPartVersionIDAttributeTypeID,
            RelationalOperator = RelationalOperators.In,
            Value = (object) new long[2]
            {
              documentVersionId,
              -documentVersionId
            },
            SQL = string.Empty
          }
        };
        DBRecordSetParams paramSet = dbRecordSetParams;
        foreach (DataRow row in (InternalDataCollectionBase) relationCollection.EntersInVersion(paramSet, documentVersionId).Rows)
        {
          long int64Value = DataSetProcessor.GetInt64Value(row, 0, 0L);
          if (!longList.Contains(int64Value) && !ObjectHelper.IsUnknownObjectVersionID(int64Value))
            longList.Add(int64Value);
        }
      }
    }
    return longList.ToArray();
  }
}
