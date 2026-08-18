// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImbaseRawTable
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.API;

internal class ImbaseRawTable : SingleThreadedObject, IIPSImbaseRawTable
{
  private long _tableId;
  private int _recordNo;
  private DataTable _attTable;
  private DataTable _recTable;
  private DataSet _dataSet;
  private AttributeTypeProperties[] _atts;
  private string _tableName;
  private string _name;

  internal static long GetTableIdByName(IUserSession session, string internalTableName, int type)
  {
    DataTable dataTable = session.GetObjectCollection(type).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID, RelationalOperators.Equal, (object) internalTableName, LogicalOperators.NONE, 0, false)
    }, new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    }));
    if (dataTable.Rows.Count > 0)
      return Convert.ToInt64(dataTable.Rows[0][0]);
    if (internalTableName.Equals("CTE_CATALOG"))
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(new Guid("cae0ad0b-ffcf-461b-a08b-6f288f1efd28"));
      if (!objectInfo.Empty)
        return objectInfo.ObjectID;
    }
    return 0;
  }

  internal static string GetColumnName(
    object index,
    AttributeTypeProperties[] atts,
    out AttributeTypeProperties atp)
  {
    atp = new AttributeTypeProperties();
    string empty = string.Empty;
    if (index is string)
    {
      int length = atts.Length;
      string str = index as string;
      for (int index1 = 0; index1 < length; ++index1)
      {
        if (atts[index1].Name.Equals(str, StringComparison.InvariantCultureIgnoreCase))
        {
          atp = atts[index1];
          empty = atp.AttributeGuid.ToString();
          break;
        }
      }
      if (string.IsNullOrEmpty(empty))
      {
        for (int index2 = 0; index2 < length; ++index2)
        {
          if (atts[index2].ShortName.Equals(str, StringComparison.InvariantCulture))
          {
            atp = atts[index2];
            empty = atp.AttributeGuid.ToString();
            break;
          }
        }
      }
    }
    else
    {
      int result;
      if (int.TryParse(index.ToString(), out result))
      {
        atp = atts[result];
        empty = atp.AttributeGuid.ToString();
      }
    }
    return empty;
  }

  private string GetColumnName(object index, out AttributeTypeProperties atp)
  {
    return ImbaseRawTable.GetColumnName(index, this._atts, out atp);
  }

  public ImbaseRawTable(long tableId)
  {
    this._tableId = tableId;
    this._recordNo = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(this._tableId);
      IDBAttribute attributeById = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
      if (attributeById != null)
        this._tableName = attributeById.AsString;
      this._name = dbObject.Caption;
    }
  }

  public int Count => this._recTable == null ? 0 : this._recTable.Rows.Count;

  public int Eof
  {
    get
    {
      return this._recTable == null || this._recTable.Rows.Count == 0 ? 1 : Convert.ToInt32(this._recordNo == this.Count);
    }
  }

  public void First() => this._recordNo = 1;

  public void Last() => this._recordNo = this.Count;

  public void Next()
  {
    if (this._recordNo >= this.Count)
      return;
    ++this._recordNo;
  }

  public void Prev()
  {
    if (this._recordNo <= 1)
      return;
    --this._recordNo;
  }

  public void Append()
  {
    DataRow row = this._recTable.NewRow();
    row["F_GUID"] = (object) Guid.NewGuid();
    this._recTable.Rows.Add(row);
    this._recordNo = this._recTable.Rows.Count;
  }

  public void Delete()
  {
    this._recTable.Rows.RemoveAt(this._recordNo - 1);
    if (this._recordNo <= 1)
      return;
    --this._recordNo;
  }

  public void SetValue(object index, object value)
  {
    string columnName = this.GetColumnName(index, out AttributeTypeProperties _);
    if (string.IsNullOrEmpty(columnName))
      return;
    this._recTable.Rows[this._recordNo - 1][columnName] = value;
  }

  public object GetValue(object index)
  {
    string columnName = this.GetColumnName(index, out AttributeTypeProperties _);
    return string.IsNullOrEmpty(columnName) ? (object) null : this._recTable.Rows[this._recordNo - 1][columnName];
  }

  public void Close() => this._recTable = (DataTable) null;

  public void Post()
  {
    if (!this._dataSet.HasChanges())
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      TableLoadHelper.StoreData(sessionKeeper.Session, this._tableId, this._dataSet, sessionKeeper.Session.GetCustomService(typeof (ITablesIndexer)) as ITablesIndexer);
  }

  public void Edit()
  {
  }

  public void Open()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      this._dataSet = TableLoadHelper.GetTables(sessionKeeper.Session, this._tableId, false);
      this._attTable = this._dataSet.Tables["IMS_ATTR_TYPES"];
      this._recTable = this._dataSet.Tables["IMS_DATA"];
      this._atts = new AttributeTypeProperties[this._recTable.Columns.Count - 2];
      AttributeTypeProperties[] attProperties = TableLoadHelper.GetAttProperties(sessionKeeper.Session, this._attTable);
      int num = 0;
      foreach (AttributeTypeProperties attributeTypeProperties in attProperties)
      {
        if (this._recTable.Columns.IndexOf(attributeTypeProperties.AttributeGuid.ToString()) != -1)
          this._atts[num++] = attributeTypeProperties;
      }
    }
  }

  public string Name => this._name;

  public string TableName => this._tableName;

  public void DeleteTable()
  {
    ImbaseCatalog.DeleteObject(this._tableId);
    this._tableId = 0L;
    this._recTable = (DataTable) null;
  }
}
