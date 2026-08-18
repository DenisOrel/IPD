// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Services.SelectCompositionThread
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.Pdm;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server.Services;

internal sealed class SelectCompositionThread : CustomSelectThread<DataTable>
{
  private readonly long _objectID;
  private readonly long _schemeID;
  private RuntimeSearchScheme _scheme;
  private readonly List<ColumnDescriptor> _columns;
  private string _filtrationOwnerID = string.Empty;
  private readonly List<ConditionStructure> _filterConditions;
  private readonly HybridDictionary _tags;

  public SelectCompositionThread(
    Guid id,
    Guid userSessionGuid,
    long objectID,
    object scheme,
    List<ConditionStructure> filterConditions,
    List<ColumnDescriptor> columns,
    string filtrationOwnerID,
    HybridDictionary tags)
    : base(id, userSessionGuid)
  {
    if (scheme == null)
      throw new ArgumentNullException(nameof (scheme));
    this._objectID = objectID != 0L ? objectID : throw new ArgumentNullException(nameof (objectID));
    this._schemeID = 0L;
    this._scheme = (RuntimeSearchScheme) null;
    if (scheme.GetType() == typeof (long))
      this._schemeID = (long) scheme;
    else if (scheme.GetType() == typeof (RuntimeSearchScheme))
      this._scheme = (RuntimeSearchScheme) scheme;
    this._filterConditions = filterConditions;
    this._columns = columns;
    this._filtrationOwnerID = filtrationOwnerID;
    this._tags = tags;
  }

  protected override void ThreadMethod()
  {
    IServerSession session = (UserSession.GetSessionByID(this.userSessionGuid) as IServerSession).Clone(true, "CompoositionService") as IServerSession;
    try
    {
      this.SetPercent(0);
      IDBObject scheme = (IDBObject) null;
      if (this._scheme == null && this._schemeID != 0L)
        scheme = session.GetObject(this._schemeID, false);
      if (this._scheme == null && scheme == null)
      {
        this.SetPercent(sc_17058.ssp_pdm_server_17059(989858588));
      }
      else
      {
        IDBObject dbObject1 = session.GetObject(this._objectID, false);
        if (dbObject1 == null)
        {
          this.SetPercent(sc_17058.ssp_pdm_server_17060(2082480847));
        }
        else
        {
          SearchOptions searchOptions = SearchOptions.None;
          VersionsRule versionsRule = (VersionsRule) null;
          int[] numArray = (int[]) null;
          List<int> intList1;
          List<int> intList2;
          SearchDirection searchDirection;
          long selectionID;
          if (scheme != null)
          {
            IDBAttribute attributeByGuid1 = scheme.GetAttributeByGuid(new Guid("cad0014a-306c-11d8-b4e9-00304f19f545"), true);
            IDBAttribute attributeByGuid2 = scheme.GetAttributeByGuid(new Guid("cad00149-306c-11d8-b4e9-00304f19f545"), false);
            intList1 = this.GetObjectTypesInComposition((IUserSession) session, attributeByGuid2);
            intList2 = new List<int>();
            foreach (object obj in attributeByGuid1.Values)
            {
              if (obj != null && obj != DBNull.Value && GuidHelper.IsGuid(Convert.ToString(obj)))
              {
                IDBRelationType relationType = session.GetRelationType(new Guid(obj.ToString()), false);
                if (relationType != null)
                  intList2.Add(relationType.RelationType);
              }
            }
            searchDirection = (SearchDirection) scheme.GetAttributeByGuid(new Guid("cad00131-306c-11d8-b4e9-00304f19f545"), true).AsInteger;
            selectionID = scheme.GetAttributeByGuid(new Guid("cad00621-306c-11d8-b4e9-00304f19f545"), true).AsInteger;
            IDBAttribute attributeByGuid3 = scheme.GetAttributeByGuid(new Guid(SearchConsts.attributeVersionRule), false);
            if (attributeByGuid3 != null && GuidHelper.IsGuid(attributeByGuid3.AsString))
            {
              versionsRule = new VersionsRule();
              versionsRule.LoadFromObject((IUserSession) session, session.GetObject(new Guid(attributeByGuid3.AsString)));
            }
            IDBAttribute attributeByGuid4 = scheme.GetAttributeByGuid(new Guid(SearchConsts.attributeSearchOptions), false);
            if (attributeByGuid4 != null && attributeByGuid4.Value != null && attributeByGuid4.Value != DBNull.Value)
              searchOptions = (SearchOptions) attributeByGuid4.AsInteger;
            numArray = this.GetTypesToExpand((IUserSession) session, scheme);
          }
          else
          {
            intList1 = this._scheme.ObjectTypes;
            intList2 = this._scheme.RelationTypes;
            searchDirection = this._scheme.Direction;
            selectionID = this._scheme.Selection;
            searchOptions = this._scheme.Options;
            if (intList1.Count > 0)
              intList1 = MetaDataHelper.GetLocalObjectTypeChildrenIDRecursive((IEnumerable<int>) intList1);
          }
          if (intList1.Count == 0)
            intList1 = MetaDataHelper.GetObjectTypesList().ConvertAll<int>((Converter<IMSObjectType, int>) (type => type.ObjectTypeID));
          if (intList2 == null || intList2.Count == 0)
          {
            List<IMSRelationType> relationTypesList = MetaDataHelper.GetRelationTypesList();
            if (intList2 == null)
              intList2 = new List<int>();
            for (int index = 0; index < relationTypesList.Count; ++index)
              intList2.Add(relationTypesList[index].RelationTypeID);
          }
          List<int> intList3 = new List<int>();
          if ((searchOptions & SearchOptions.InSelectionProd) == SearchOptions.InSelectionProd && this._columns.FindIndex((Predicate<ColumnDescriptor>) (x => x.AttributeID.Equals((object) -3))) < 0)
          {
            this._columns.Add(new ColumnDescriptor((object) -3, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.Index, SortOrders.NONE, 0));
            intList3.Add(this._columns.Count - 1);
          }
          if ((searchOptions & SearchOptions.ActualSubstitutesOnly) == SearchOptions.ActualSubstitutesOnly)
            this._tags[(object) "{82E381A1-8952-416A-B303-F81BA2945F8F}"] = (object) true;
          if (session.GetCustomService(typeof (ISearchSchemeSettingsService)) is ISearchSchemeSettingsService customService && customService.VisibilityFilter)
            this._tags[(object) "{7FB30639-2F65-4407-B78E-523547B1B133}"] = (object) true;
          ICompositionLoadService service = ServerServices.ServiceContainer.GetService<ICompositionLoadService>();
          SelectionTableFilter selectionTableFilter = new SelectionTableFilter();
          selectionTableFilter.BeforeSelectComposition((IUserSession) session, this._columns, selectionID, this._filterConditions);
          IServerSession usrSession = session;
          long objectId = dbObject1.ObjectID;
          int objectType = dbObject1.ObjectType;
          List<int> searchRelationTypes = intList2;
          List<int> searchObjectTypes = intList1;
          List<ColumnDescriptor> columns = this._columns;
          int num1 = searchDirection == SearchDirection.RecursiveContains ? 1 : (searchDirection == SearchDirection.Contains ? 1 : 0);
          int num2 = (searchOptions & SearchOptions.ObjectGrouping) == SearchOptions.ObjectGrouping ? 1 : 0;
          VersionsRule rule = versionsRule;
          string filtrationOwnerId = this._filtrationOwnerID;
          HybridDictionary tags = this._tags;
          int loadLevels = searchDirection == SearchDirection.RecursiveContains || searchDirection == SearchDirection.RecursiveEntersTo ? -1 : 1;
          int[] expandObjectTypes = numArray;
          DataTable resultTable = service.LoadComposition((object) usrSession, objectId, objectType, (IEnumerable<int>) searchRelationTypes, (IEnumerable<int>) searchObjectTypes, (IEnumerable<ColumnDescriptor>) columns, num1 != 0, num2 != 0, rule, (IEnumerable<ConditionStructure>) null, filtrationOwnerId, tags, loadLevels, (IEnumerable<int>) expandObjectTypes);
          this.SetPercent(98);
          if (resultTable != null && resultTable.Rows.Count > 0)
          {
            resultTable = selectionTableFilter.FilterTable((IUserSession) session, resultTable, this._columns, intList1);
            if ((searchOptions & SearchOptions.InSelectionProd) == SearchOptions.InSelectionProd)
            {
              IDBObject dbObject2 = session.GetObject(new Guid("cad00796-306c-11d8-b4e9-00304f19f545"));
              int index = this._columns.FindIndex((Predicate<ColumnDescriptor>) (x => x.AttributeID.Equals((object) -3)));
              IDbManager dataManager = (session as UserSession).DataManager;
              DataTable dataTable = dataManager.ExecuteDataTable(string.Format(sc_17058.ssp_pdm_server_17061(), (object) "F_ID", (object) "IMS_SELECTIONS", (object) "F_FOLDER_ID"), dataManager.Parameter("selectionID", (object) dbObject2.ObjectID));
              List<long> longList = new List<long>();
              foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
                longList.Add(Convert.ToInt64(row[0]));
              DataTable toTable = resultTable.Clone();
              foreach (DataRow row in (InternalDataCollectionBase) resultTable.Rows)
              {
                if (longList.Contains(Convert.ToInt64(row[index])))
                  DataSetProcessor.AddRow(toTable, row, false);
              }
              resultTable = toTable;
            }
          }
          if (resultTable != null)
          {
            for (int index = 0; index < intList3.Count; ++index)
              resultTable.Columns.RemoveAt(intList3[index] - index);
          }
          this.result = resultTable;
          this.SetPercent(sc_17058.ssp_pdm_server_17062(2041348697));
        }
      }
    }
    catch (Exception ex)
    {
      this.IsError = true;
      this.ErrorException = ex;
      this.SetPercent(sc_17058.ssp_pdm_server_17063(709054124));
    }
    finally
    {
      session.Logout("CompoositionService");
    }
  }

  private List<int> GetObjectTypesInComposition(IUserSession session, IDBAttribute objTypes)
  {
    List<int> typesInComposition = new List<int>();
    if (objTypes != null && objTypes.Values.Length != 0)
    {
      foreach (object obj in objTypes.Values)
      {
        if (obj != null && obj.ToString().Length > 0)
        {
          IDBObjectType objectType = session.GetObjectType(new Guid(obj.ToString()), false);
          if (objectType != null)
          {
            typesInComposition.Add(objectType.ObjectType);
            List<int> childTypes = ObjectTypesCacheHelper.GetChildTypes(session, objectType.ObjectType);
            if (childTypes.Count > 0)
              typesInComposition.AddRange((IEnumerable<int>) childTypes);
          }
        }
      }
    }
    return typesInComposition;
  }

  private List<int> ReadTypesFromAttribute(IDBObject scheme, Guid attributeGuid)
  {
    List<int> intList = new List<int>();
    IDBAttribute attributeByGuid = scheme.GetAttributeByGuid(attributeGuid, false);
    if (attributeByGuid != null && attributeByGuid.ValuesCount > 0)
    {
      foreach (object obj in attributeByGuid.Values)
      {
        string str = Convert.ToString(obj);
        if (GuidHelper.IsGuid(str))
        {
          List<int> childrenIdRecursive = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(str));
          if (childrenIdRecursive != null && childrenIdRecursive.Count > 0)
          {
            foreach (int num in childrenIdRecursive)
            {
              if (!intList.Contains(num))
                intList.Add(num);
            }
          }
        }
      }
    }
    return intList;
  }

  private int[] GetTypesToExpand(IUserSession session, IDBObject scheme)
  {
    List<int> intList1 = this.ReadTypesFromAttribute(scheme, PDMHelper.attributeTypesToExpand);
    List<int> intList2 = this.ReadTypesFromAttribute(scheme, PDMHelper.attributeTypesToDisableExpand);
    if (intList1.Count > 0)
      return intList1.ToArray();
    if (intList2.Count <= 0)
      return (int[]) null;
    foreach (DataRow row in (InternalDataCollectionBase) session.GetObjectTypeCollection(-2, true).Select(string.Empty).Rows)
    {
      int int32 = Convert.ToInt32(row["F_OBJECT_TYPE"]);
      if (!intList2.Contains(int32))
        intList1.Add(int32);
    }
    return intList1.ToArray();
  }
}
