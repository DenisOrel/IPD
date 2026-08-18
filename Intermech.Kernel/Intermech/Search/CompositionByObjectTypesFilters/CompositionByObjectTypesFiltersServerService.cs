// Decompiled with JetBrains decompiler
// Type: Intermech.Search.CompositionByObjectTypesFilters.CompositionByObjectTypesFiltersServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Projects;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using Intermech.Search.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


namespace Intermech.Search.CompositionByObjectTypesFilters;

public sealed class CompositionByObjectTypesFiltersServerService : 
  LongLifeObject,
  ICompositionByObjectTypesFiltersServerService
{
  private ICompositionByObjectTypesFilterXmlConverter _xmlConverter;

  public CompositionByObjectTypesFiltersServerService(
    ICompositionByObjectTypesFilterXmlConverter xmlConverter)
  {
    this._xmlConverter = xmlConverter != null ? xmlConverter : throw new ArgumentNullException(nameof (xmlConverter));
  }

  public CompositionByObjectTypesFilter FindFilterByVersionID(
    Guid userSessionGuid,
    long filterVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(filterVersionID) ? this.FindFilterByVersionID(filterVersionID) : throw new ArgumentException();
  }

  public void SaveFilter(
    Guid userSessionGuid,
    long filterVersionID,
    CompositionByObjectTypesFilter filter)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(filterVersionID))
        throw new ArgumentException();
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      this.SaveFilter(filterVersionID, filter);
    }
  }

  public void AddFiltersToObjectComposition(
    Guid userSessionGuid,
    long[] filterVersionIds,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (filterVersionIds == null)
        throw new ArgumentNullException(nameof (filterVersionIds));
      if (ObjectHelper.IsAnyUnknownObjectVersionID((IEnumerable<long>) filterVersionIds))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.AddFiltersToObjectComposition(filterVersionIds, objectVersionID);
    }
  }

  public void RemoveFilterFromObjectComposition(
    Guid userSessionGuid,
    long filterVersionID,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (ObjectHelper.IsUnknownObjectVersionID(filterVersionID))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.RemoveFilterFromObjectComposition(filterVersionID, objectVersionID);
    }
  }

  public string CreateTextFromFiltersInObjectComposition(Guid userSessionGuid, long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? this.CreateTextFromFiltersInObjectComposition(objectVersionID) : throw new ArgumentException();
  }

  public long GetCurrentUserConfigurationVersionID(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetCurrentUserConfigurationVersionID();
  }

  public void CreateFiltersAndAddToCurrentUserConfigurationComposition(
    Guid userSessionGuid,
    CompositionByObjectTypesFilter[] filters)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (filters == null || filters.Length == 0)
        throw new ArgumentException();
      this.CreateFiltersAndAddToCurrentUserConfigurationComposition(filters);
    }
  }

  public CompositionByObjectTypesFilter[] GetFiltersForCurrentUser(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetFiltersForCurrentUser();
  }

  public CompositionByObjectTypesFilter[] GetFiltersForCurrentRole(Guid userSessionGuid)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return this.GetFiltersForCurrentRole();
  }

  public bool IsFilterWithNameExistsInObjectComposition(
    Guid userSessionGuid,
    string filterName,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
      return !ObjectHelper.IsUnknownObjectVersionID(objectVersionID) ? this.IsFilterWithNameExistsInObjectComposition(filterName, objectVersionID) : throw new ArgumentException();
  }

  public void CreateFiltersAndAddToObjectComposition(
    Guid userSessionGuid,
    CompositionByObjectTypesFilter[] filters,
    long objectVersionID)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    using (UserSessionContext.CaptureSession(userSessionGuid))
    {
      if (filters == null || filters.Length == 0 || ((IEnumerable<CompositionByObjectTypesFilter>) filters).Any<CompositionByObjectTypesFilter>((System.Func<CompositionByObjectTypesFilter, bool>) (o => o == null)))
        throw new ArgumentException();
      if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
        throw new ArgumentException();
      this.CreateFiltersAndAddToObjectComposition(filters, objectVersionID);
    }
  }

  private CompositionByObjectTypesFilter FindFilterByVersionID(long filterVersionID)
  {
    CompositionByObjectTypesFilter filter = new CompositionByObjectTypesFilter(filterVersionID);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(filterVersionID);
      filter.Name = dbObject.Caption;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BlobProcReader(dbObject.GetAttributeByID(Constants.FileAttributeTypeID), 0, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).ReadData();
        if (memoryStream.Length > 0L)
        {
          memoryStream.Seek(0L, SeekOrigin.Begin);
          if (new BinaryFormatter().Deserialize((Stream) memoryStream) is CompositionByObjectTypesFiltersServerService.CompositionByObjectTypesFilterStorageState storageState)
            this.CompleteFilterFromStorageState(filter, storageState);
        }
      }
    }
    return filter;
  }

  private void CompleteFilterFromStorageState(
    CompositionByObjectTypesFilter filter,
    CompositionByObjectTypesFiltersServerService.CompositionByObjectTypesFilterStorageState storageState)
  {
    if (storageState.CheckedPartTypeGuidDictionaryByProjectTypeGuid == null)
      return;
    foreach (KeyValuePair<Guid, Guid[]> keyValuePair in storageState.CheckedPartTypeGuidDictionaryByProjectTypeGuid)
    {
      int objectTypeId = MetaDataHelper.GetObjectTypeID(keyValuePair.Key);
      if (!ObjectTypeHelper.IsUnknownObjectTypeID(objectTypeId))
      {
        CompositionByObjectTypesFilterProjectType projectType = CompositionByObjectTypesFiltersHelper.CreateProjectType(objectTypeId);
        int[] array = ((IEnumerable<Guid>) keyValuePair.Value).Select<Guid, int>((System.Func<Guid, int>) (o => MetaDataHelper.GetObjectTypeID(o))).ToArray<int>();
        projectType.CheckPartTypesAndDescendants(array);
        filter.ProjectTypes.Add(projectType);
      }
    }
  }

  private void SaveFilter(long filterVersionID, CompositionByObjectTypesFilter filter)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(filterVersionID);
      dbObject.Caption = filter.Name;
      IDBAttribute attributeById = dbObject.GetAttributeByID(Constants.FileAttributeTypeID);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new BinaryFormatter().Serialize((Stream) memoryStream, (object) this.CreateStorageStateForFilter(filter));
        memoryStream.Seek(0L, SeekOrigin.Begin);
        BlobInformation aBlobInformation = new BlobInformation(memoryStream.Length, memoryStream.Length, DateTime.Now, $"Файл фильтра состава по типам объектов #{dbObject.ObjectGUID}", ArcMethods.ZLibPacked, (string) null);
        new BlobProcWriter(attributeById, 0, aBlobInformation, (Stream) memoryStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
      }
    }
  }

  private CompositionByObjectTypesFiltersServerService.CompositionByObjectTypesFilterStorageState CreateStorageStateForFilter(
    CompositionByObjectTypesFilter filter)
  {
    return new CompositionByObjectTypesFiltersServerService.CompositionByObjectTypesFilterStorageState()
    {
      CheckedPartTypeGuidDictionaryByProjectTypeGuid = this.CreateCheckedPartTypeGuidDictionaryByProjectTypeGuidForFilter(filter)
    };
  }

  private Dictionary<Guid, Guid[]> CreateCheckedPartTypeGuidDictionaryByProjectTypeGuidForFilter(
    CompositionByObjectTypesFilter filter)
  {
    Dictionary<Guid, Guid[]> typeGuidForFilter = new Dictionary<Guid, Guid[]>();
    foreach (CompositionByObjectTypesFilterProjectType projectType in (Collection<CompositionByObjectTypesFilterProjectType>) filter.ProjectTypes)
    {
      Guid objectTypeGuid = MetaDataHelper.GetObjectTypeGuid(projectType.ProjectTypeID);
      Guid[] array = ((IEnumerable<CompositionByObjectTypesFilterPartType>) projectType.GetCheckedGetPartTypesAndDescendants()).Select<CompositionByObjectTypesFilterPartType, Guid>((System.Func<CompositionByObjectTypesFilterPartType, Guid>) (o => MetaDataHelper.GetObjectTypeGuid(o.PartTypeID))).ToArray<Guid>();
      typeGuidForFilter[objectTypeGuid] = array;
    }
    return typeGuidForFilter;
  }

  private void AddFiltersToObjectComposition(long[] filterVersionIds, long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFiltersRelationTypeID);
      foreach (long filterVersionId in filterVersionIds)
        relationCollection.Create(objectVersionID, filterVersionId);
    }
  }

  private void RemoveFilterFromObjectComposition(long filterVersionID, long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(filterVersionID);
      sessionKeeper.Session.GetRelation(objectVersionID, dbObject.ID).Delete((long) Consts.PurgeMode);
    }
  }

  private Tuple<long, long, long, string>[] FindFiltersForObject(long objectVersionID)
  {
    if (ObjectHelper.IsUnknownObjectVersionID(objectVersionID))
      return new Tuple<long, long, long, string>[0];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFiltersRelationTypeID);
      relationCollection.ChildObjectTypes = (IList<int>) new List<int>()
      {
        CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFilterObjectTypeID
      };
      DBRecordSetParams paramSet = new DBRecordSetParams()
      {
        Columns = new object[4]
        {
          (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
          (object) ObligatoryObjectAttributes.F_OBJECT_ID,
          (object) Constants.SortingAttributeTypeID,
          (object) ObligatoryObjectAttributes.CAPTION
        }
      };
      DataTable dataTable = relationCollection.ConsistFrom(paramSet, objectVersionID);
      List<Tuple<long, long, long, string>> tupleList = new List<Tuple<long, long, long, string>>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long int64Value1 = DataSetProcessor.GetInt64Value(row, 0, 0L);
        long int64Value2 = DataSetProcessor.GetInt64Value(row, 1, 0L);
        long int64Value3 = DataSetProcessor.GetInt64Value(row, 2, 0L);
        string stringValue = DataSetProcessor.GetStringValue(row, 3, (string) null);
        tupleList.Add(new Tuple<long, long, long, string>(int64Value1, int64Value2, int64Value3, stringValue));
      }
      return tupleList.ToArray();
    }
  }

  private void CreateFiltersAndAddToObjectComposition(
    CompositionByObjectTypesFilter[] filters,
    long objectVersionID)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBTransactions customService = (IDBTransactions) sessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFilterObjectTypeID);
        IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(CompositionByObjectTypesFiltersConstants.CompositionByObjectTypesFiltersRelationTypeID);
        foreach (CompositionByObjectTypesFilter filter in filters)
        {
          this.RemoveFilterWithNameFromObjectComposition(filter.Name, objectVersionID);
          IDBObject dbObject = objectCollection.Create();
          dbObject.Caption = filter.Name;
          dbObject.CommitCreation(true);
          relationCollection.Create(objectVersionID, dbObject.ObjectID);
          this.SaveFilter(dbObject.ObjectID, filter);
        }
        customService.Commit();
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private void RemoveFilterWithNameFromObjectComposition(string filterName, long objectVersionID)
  {
    this.GetRelationForFilterWithNameInComposition(filterName, objectVersionID)?.Delete((long) Consts.PurgeMode);
  }

  private string CreateTextFromFiltersInObjectComposition(long objectVersionID)
  {
    return this._xmlConverter.ConvertToXml(((IEnumerable<Tuple<long, long, long, string>>) this.FindFiltersForObject(objectVersionID)).Select<Tuple<long, long, long, string>, CompositionByObjectTypesFilter>((System.Func<Tuple<long, long, long, string>, CompositionByObjectTypesFilter>) (o => this.FindFilterByVersionID(o.Item2))).ToArray<CompositionByObjectTypesFilter>());
  }

  private long GetCurrentUserConfigurationVersionID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      ProjectFiltrationModes projectFiltrationMode = sessionKeeper.Session.ProjectFiltrationMode;
      sessionKeeper.Session.ProjectFiltrationMode = ProjectFiltrationModes.None;
      try
      {
        return DataSetProcessor.GetInt64Value(sessionKeeper.Session.GetObjectCollection(Constants.UserConfigurationObjectTypeID).Select(new DBRecordSetParams()
        {
          Columns = new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          },
          RecordCount = -1
        }).Rows[0], 0, 0L);
      }
      finally
      {
        sessionKeeper.Session.ProjectFiltrationMode = projectFiltrationMode;
      }
    }
  }

  private void CreateFiltersAndAddToCurrentUserConfigurationComposition(
    CompositionByObjectTypesFilter[] filters)
  {
    this.CreateFiltersAndAddToObjectComposition(filters, this.GetCurrentUserConfigurationVersionID());
  }

  private CompositionByObjectTypesFilter[] GetFiltersForCurrentUser()
  {
    return ((IEnumerable<Tuple<long, long, long, string>>) this.FindFiltersForObject(this.GetCurrentUserConfigurationVersionID())).Select<Tuple<long, long, long, string>, CompositionByObjectTypesFilter>((System.Func<Tuple<long, long, long, string>, CompositionByObjectTypesFilter>) (o => this.FindFilterByVersionID(o.Item2))).ToArray<CompositionByObjectTypesFilter>();
  }

  private CompositionByObjectTypesFilter[] GetFiltersForCurrentRole()
  {
    return ((IEnumerable<Tuple<long, long, long, string>>) this.FindFiltersForObject(this.GetCurrentRoleConfigurationVersionID())).Select<Tuple<long, long, long, string>, CompositionByObjectTypesFilter>((System.Func<Tuple<long, long, long, string>, CompositionByObjectTypesFilter>) (o => this.FindFilterByVersionID(o.Item2))).ToArray<CompositionByObjectTypesFilter>();
  }

  private long GetCurrentRoleConfigurationVersionID()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(sessionKeeper.Session.RoleID).GetAttributeByID(Constants.RoleConfigurationAttributeTypeID).AsInteger;
  }

  private bool IsFilterWithNameExistsInObjectComposition(string filterName, long objectVersionID)
  {
    return this.GetRelationForFilterWithNameInComposition(filterName, objectVersionID) != null;
  }

  private IDBRelation GetRelationForFilterWithNameInComposition(
    string filterName,
    long objectVersionID)
  {
    Tuple<long, long, long, string> tuple = ((IEnumerable<Tuple<long, long, long, string>>) this.FindFiltersForObject(objectVersionID)).FirstOrDefault<Tuple<long, long, long, string>>((System.Func<Tuple<long, long, long, string>, bool>) (o => o.Item4 == filterName));
    if (RelationHelper.IsUnknownRelationID(tuple.Item1))
      return (IDBRelation) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetRelation(tuple.Item1, false);
  }

  [Serializable]
  private sealed class CompositionByObjectTypesFilterStorageState
  {
    private Dictionary<Guid, Guid[]> _checkedPartTypeGuidDictionaryByProjectTypeGuid;

    public Dictionary<Guid, Guid[]> CheckedPartTypeGuidDictionaryByProjectTypeGuid
    {
      get => this._checkedPartTypeGuidDictionaryByProjectTypeGuid;
      set => this._checkedPartTypeGuidDictionaryByProjectTypeGuid = value;
    }
  }
}
