// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;


namespace Intermech.Kernel;

public class DBCollection : DBSessionable, IDBCollection
{
  protected string _DBTableName;
  protected string _DBKeyField = "";
  protected bool _AreaSupport;
  protected bool _LanguageSupport;
  protected bool _SelectFromCache = true;
  protected bool _Filtering;
  private object _ParentID;

  public DBCollection(UserSession uSession, bool filterRecs)
    : base(uSession)
  {
    this._Filtering = filterRecs;
    this._ParentID = (object) 0;
  }

  public int[] GetVisibleList()
  {
    if (!this._Filtering)
      throw new KernelExceptionID(sc_12736.ssp_appserver_12737(1359803719));
    DataTable dataTable = this.Select(string.Empty, Array.Empty<object>());
    List<int> intList = new List<int>(dataTable.Rows.Count);
    int columnIndex = dataTable.Columns.IndexOf(this._DBKeyField);
    for (int index = 0; index < dataTable.Rows.Count; ++index)
    {
      int int32 = Convert.ToInt32(dataTable.Rows[index][columnIndex]);
      intList.Add(int32);
    }
    return intList.ToArray();
  }

  public virtual object ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  protected virtual void FillCaptions(DataTable datatable)
  {
    ICaptionsHelper service = ServerServices.GetService(typeof (ICaptionsHelper)) as ICaptionsHelper;
    foreach (DataColumn column in (InternalDataCollectionBase) datatable.Columns)
      column.Caption = service.GetCaption(column.ColumnName);
  }

  public virtual string DBTableName => this._DBTableName;

  public virtual string DBKeyField => this._DBKeyField;

  public virtual bool AreaSupport => this._AreaSupport;

  public virtual bool LanguageSupport => this._LanguageSupport;

  public virtual long Create(params object[] properties) => 0;

  protected virtual void DeleteNotVisibleRows(DataTable table)
  {
    if (!this._Filtering)
      return;
    try
    {
      if (this.LoadCacheTable(ActionType.List) <= 0)
        return;
      CategoryValue categoryValue = new CategoryValue(this._CategoryType, 0L, ActionType.List);
      CategoryValue aCategory = new CategoryValue(this._CategoryType, 0L, ActionType.List);
      long num = -1;
      int columnIndex = this._AccessCacheTable.Columns.IndexOf("F_CATEGORY_ID");
      List<DataRow> dataRowList = new List<DataRow>();
      for (int index = 0; index < this._AccessCacheTable.Rows.Count; ++index)
      {
        categoryValue.CategoryID = Convert.ToInt64(this._AccessCacheTable.Rows[index][columnIndex]);
        if (categoryValue.CategoryID != num)
        {
          if (dataRowList.Count > 0)
          {
            aCategory.CategoryID = num;
            if (!this.DoCheckMetadataAccess(aCategory, dataRowList.ToArray(), ActionType.List))
            {
              DataRow row = table.Rows.Find((object) Convert.ToInt32(num));
              if (row != null)
                table.Rows.Remove(row);
            }
          }
          dataRowList.Clear();
          num = categoryValue.CategoryID;
        }
        dataRowList.Add(this._AccessCacheTable.Rows[index]);
      }
      if (dataRowList.Count > 0)
      {
        aCategory.CategoryID = num;
        if (!this.DoCheckMetadataAccess(aCategory, dataRowList.ToArray(), ActionType.List))
        {
          DataRow row = table.Rows.Find((object) Convert.ToInt32(num));
          if (row != null)
            table.Rows.Remove(row);
        }
      }
      table.AcceptChanges();
    }
    finally
    {
      this.ClearCacheTable();
    }
  }

  public List<int> GetDisabledAccess(ActionType at)
  {
    List<int> disabledAccess = new List<int>();
    try
    {
      if (this.LoadCacheTable(at) > 0)
      {
        CategoryValue categoryValue = new CategoryValue(this._CategoryType, 0L, at);
        CategoryValue aCategory = new CategoryValue(this._CategoryType, 0L, at);
        long num = -1;
        int columnIndex = this._AccessCacheTable.Columns.IndexOf("F_CATEGORY_ID");
        List<DataRow> dataRowList = new List<DataRow>();
        for (int index = 0; index < this._AccessCacheTable.Rows.Count; ++index)
        {
          categoryValue.CategoryID = Convert.ToInt64(this._AccessCacheTable.Rows[index][columnIndex]);
          if (categoryValue.CategoryID != num)
          {
            if (dataRowList.Count > 0)
            {
              aCategory.CategoryID = num;
              if (!this.DoCheckMetadataAccess(aCategory, dataRowList.ToArray(), at))
                disabledAccess.Add(Convert.ToInt32(num));
            }
            dataRowList.Clear();
            num = categoryValue.CategoryID;
          }
          dataRowList.Add(this._AccessCacheTable.Rows[index]);
        }
        if (dataRowList.Count > 0)
        {
          aCategory.CategoryID = num;
          if (!this.DoCheckMetadataAccess(aCategory, dataRowList.ToArray(), at))
            disabledAccess.Add(Convert.ToInt32(num));
        }
      }
    }
    finally
    {
      this.ClearCacheTable();
    }
    return disabledAccess;
  }

  protected virtual string GetParentSQL(object parentID) => "";

  protected virtual string GetWhereString(object parentID)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append(this.GetParentSQL(parentID));
    if (this.AreaSupport && this.UserSession.AreaSQL != "")
    {
      if (stringBuilder.Length != 0)
        stringBuilder.Append(" AND ");
      stringBuilder.Append(this.UserSession.AreaSQL);
    }
    if (this.LanguageSupport && this.UserSession.LanguageSQL != "")
    {
      if (stringBuilder.Length != 0)
        stringBuilder.Append(" AND ");
      stringBuilder.Append(this.UserSession.LanguageSQL);
    }
    if (stringBuilder.Length > 0 && !this._SelectFromCache)
      stringBuilder.Insert(0, "WHERE ");
    return stringBuilder.ToString();
  }

  private DataTable SelectIt(object parentID, string orderBy, params object[] addInfo)
  {
    DataTable table = this.UserSession.DBCache.GetTable(this.DBTableName);
    DataTable dataTable;
    if (this._SelectFromCache)
    {
      this.UserSession.DBCache.EnterReadLocker();
      try
      {
        dataTable = table.Clone();
        DataRow[] fromRows = table.Select(this.GetWhereString(parentID), orderBy);
        SqlHelper.AssignRows(dataTable, (IEnumerable<DataRow>) fromRows);
      }
      finally
      {
        this.UserSession.DBCache.ExitReadLocker();
      }
    }
    else
    {
      if (orderBy != "")
        orderBy = "ORDER BY " + orderBy;
      dataTable = this.UserSession.DataManager.ExecuteDataTable($"SELECT * FROM {this.DBTableName} {this.GetWhereString(parentID)} {orderBy}");
    }
    dataTable.TableName = this.DBTableName;
    this.DeleteNotVisibleRows(dataTable);
    this.FillCaptions(dataTable);
    return dataTable;
  }

  public virtual DataTable Select(string orderBy, params object[] addInfo)
  {
    return this.SelectIt(this.ParentID, orderBy, addInfo);
  }

  public virtual long Count => (long) this.Select("", Array.Empty<object>()).Rows.Count;
}
