// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.QuickSearch.ImbaseQuickSearchHelper
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.Imbase.Filters;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.QuickSearch;

public class ImbaseQuickSearchHelper
{
  private readonly string _colTypeId = Convert.ToString(-7);
  private readonly string _colId = Convert.ToString(-2);
  private readonly string _colCaption = Convert.ToString(-50);
  private readonly string _colClassifKey = Convert.ToString(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
  private IList<long> _catalogIDs;
  private EnumerableRowCollection<DataRow> _sourceRowsForSearch;

  public IList<long> CatalogIDs
  {
    get => this._catalogIDs;
    set
    {
      this._catalogIDs = value;
      if (value == null || value.Count == 0)
        this._sourceRowsForSearch = (EnumerableRowCollection<DataRow>) null;
      else
        this._sourceRowsForSearch = this.GetSourceForSearch(value);
    }
  }

  public DataTable Filter { get; set; }

  public bool NeedTimerForServerRequest => this.IIS != null;

  internal IImbaseIndexingService IIS
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetCustomService(typeof (IImbaseIndexingService)) as IImbaseIndexingService;
    }
  }

  public List<ImbaseQuickSearchItem> SearchFolders(string text, int count = -1)
  {
    List<ImbaseQuickSearchItem> imbaseQuickSearchItemList = new List<ImbaseQuickSearchItem>();
    if (this._sourceRowsForSearch != null)
    {
      string empty = string.Empty;
      foreach (DataRow row in this._sourceRowsForSearch)
      {
        if (Convert.ToString(row[this._colCaption]).IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0 && this.CheckFilter(row))
        {
          imbaseQuickSearchItemList.Add(new ImbaseQuickSearchItem(Convert.ToInt32(row[this._colTypeId]), Convert.ToInt64(row[this._colId]), Convert.ToString(row[this._colCaption])));
          if (imbaseQuickSearchItemList.Count == count)
            break;
        }
      }
    }
    return imbaseQuickSearchItemList.Count <= 0 ? (List<ImbaseQuickSearchItem>) null : imbaseQuickSearchItemList;
  }

  public List<ImbaseQuickSearchItem> SearchRecords(string text, int count = 0)
  {
    List<ImbaseQuickSearchItem> imbaseQuickSearchItemList = new List<ImbaseQuickSearchItem>();
    DataTable dataTable1 = (DataTable) null;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        count = count > -1 ? count : 0;
        IImbaseIndexingService iis = this.IIS;
        DataTable dataTable2;
        if (iis == null)
        {
          dataTable2 = (DataTable) null;
        }
        else
        {
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          IList<long> catalogIds = this._catalogIDs;
          List<long> list = catalogIds != null ? catalogIds.ToList<long>() : (List<long>) null;
          string request = text;
          DataTable filter = this.Filter;
          int recordCount = count;
          dataTable2 = iis.QuickSearch(sessionGuid, list, request, filter, recordCount);
        }
        dataTable1 = dataTable2;
      }
    }
    catch
    {
    }
    if (dataTable1 != null)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataTable1.Rows)
        imbaseQuickSearchItemList.Add(new ImbaseQuickSearchItem(Intermech.Imbase.Consts.ImbaseTableRefTypeID, Convert.ToInt64(row[IndexesField.F_LINK_ID]), Convert.ToString(row[IndexesField.F_TEXT]), Convert.ToInt64(row[IndexesField.F_TABKEY])));
    }
    return imbaseQuickSearchItemList.Count <= 0 ? (List<ImbaseQuickSearchItem>) null : imbaseQuickSearchItemList;
  }

  private EnumerableRowCollection<DataRow> GetSourceForSearch(IList<long> catalogIDs)
  {
    DataTable dataTable = (DataTable) null;
    if (catalogIDs.Count > 0)
    {
      ColumnDescriptor[] columns = new ColumnDescriptor[4]
      {
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.F_OBJECT_TYPE, AttributeSourceTypes.Object, ColumnContents.ID, ColumnNameMapping.ID, SortOrders.NONE, 0),
        new ColumnDescriptor((object) ObligatoryObjectAttributes.CAPTION, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 0),
        new ColumnDescriptor((object) Intermech.Imbase.Consts.ClassifFolderKeyAttId, AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0)
      };
      string empty = string.Empty;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DBRecordSetParams rParams;
        if (catalogIDs.Count == 1)
        {
          long catalogId = catalogIDs[0];
          string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(sessionKeeper.Session, catalogId);
          if (string.IsNullOrEmpty(classifKeyByObjId))
            throw new ApplicationException(string.Format(LocalizationHolder.rm.GetString("Imbase_CatalogClassifKey_Null"), (object) sessionKeeper.Session.GetObjectInfo(catalogId).Caption, (object) catalogId));
          rParams = new DBRecordSetParams(new ConditionStructure[1]
          {
            new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKeyByObjId, LogicalOperators.NONE, 0, true)
          }, columns);
        }
        else
        {
          List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(catalogIDs.Count);
          foreach (long catalogId in (IEnumerable<long>) catalogIDs)
          {
            string classifKeyByObjId = ImbaseHelper.GetClassifKeyByObjID(sessionKeeper.Session, catalogId);
            if (!string.IsNullOrEmpty(classifKeyByObjId))
              conditionStructureList.Add(new ConditionStructure(Intermech.Imbase.Consts.ClassifFolderKeyAttId, RelationalOperators.StartString, (object) classifKeyByObjId, LogicalOperators.OR, 0, true));
          }
          rParams = new DBRecordSetParams(conditionStructureList.ToArray(), columns);
        }
        rParams.Tags = new HybridDictionary()
        {
          {
            (object) "{7FB30639-2F65-4407-B78E-523547B1B133}",
            (object) true
          }
        };
        dataTable = ImbaseHelper.SelectObjects(sessionKeeper.Session, rParams, new int[2]
        {
          Intermech.Imbase.Consts.ImbaseFolderTypeID,
          Intermech.Imbase.Consts.ImbaseCatalogRecordTypeID
        });
        if (dataTable != null)
        {
          if (dataTable.Rows.Count > 0)
          {
            if (sessionKeeper.Session.GetCustomService(typeof (IObjectFilterService)) is IObjectFilterService customService)
              dataTable = customService.RemoveWithMissingParents(dataTable, Intermech.Imbase.Consts.ImbaseFolderTypeID, 3);
          }
        }
      }
    }
    return dataTable == null ? (EnumerableRowCollection<DataRow>) null : dataTable.AsEnumerable();
  }

  private bool CheckFilter(DataRow row)
  {
    bool flag = true;
    if (this.Filter != null)
    {
      string classifKey = Convert.ToString(row[this._colClassifKey]);
      EnumerableRowCollection<DataRow> source = this.Filter.AsEnumerable();
      if (source.FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => Convert.ToString(x["F_PATH"]) == classifKey)) == null)
        flag = source.Where<DataRow>((System.Func<DataRow, bool>) (x => x["#FLT"] != DBNull.Value && Convert.ToBoolean(x["#FLT"]))).ToList<DataRow>().FirstOrDefault<DataRow>((System.Func<DataRow, bool>) (x => classifKey.StartsWith(Convert.ToString(x["F_PATH"])))) != null;
    }
    return flag;
  }
}
