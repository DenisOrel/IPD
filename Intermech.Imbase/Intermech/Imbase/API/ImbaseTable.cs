// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImbaseTable
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.Imbase.API;

internal class ImbaseTable : SingleThreadedObject, IIPSImbaseTable
{
  private long _linkId;
  private long _tableId;
  private int _recordNo;
  private DataTable _recordsTable;
  private IIPSImbaseFolder _folder;
  private ImbaseKeyInfo _keyInfo;
  private AttributeTypeProperties[] _atts;
  private string _tableName;
  private string _name;
  private bool _eof;

  public ImbaseTable(IUserSession session, long linkId, IIPSImbaseFolder folder)
  {
    Logger.Log("Table.Ctor");
    this._linkId = linkId;
    this._folder = folder;
    this._recordNo = 0;
    this._recordsTable = (DataTable) null;
    this._eof = false;
    IDBObject dbObject = session.GetObject(linkId);
    this._name = dbObject.Caption;
    IDBAttribute attributeById1 = dbObject.GetAttributeByID(Intermech.Imbase.Consts.ImbaseTableRefAttID);
    if (attributeById1 == null)
      return;
    this._tableId = attributeById1.AsInteger;
    IDBAttribute attributeById2 = session.GetObject(this._tableId).GetAttributeByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
    if (attributeById2 == null)
      return;
    this._tableName = attributeById2.AsString;
  }

  public int Count => this._recordsTable == null ? 0 : this._recordsTable.Rows.Count;

  public int Eof
  {
    get
    {
      return this._recordsTable == null || this._recordsTable.Rows.Count == 0 ? 1 : Convert.ToInt32(this._eof);
    }
  }

  public void First()
  {
    if (this._recordsTable == null)
      return;
    this._recordNo = 1;
    this._eof = false;
  }

  public void Last()
  {
    if (this._recordsTable == null)
      return;
    this._recordNo = this.Count;
    this._eof = false;
  }

  public void Next()
  {
    if (this._recordsTable == null)
      return;
    if (this._recordNo < this.Count)
      ++this._recordNo;
    else
      this._eof = true;
  }

  public void Prev()
  {
    if (this._recordsTable == null)
      return;
    if (this._recordNo > 1)
      --this._recordNo;
    this._eof = false;
  }

  public object GetValue(object index)
  {
    string columnName = this.GetColumnName(index, out AttributeTypeProperties _);
    return string.IsNullOrEmpty(columnName) ? (object) null : this._recordsTable.Rows[this._recordNo - 1][columnName];
  }

  public void Close()
  {
    this._recordsTable = (DataTable) null;
    this._recordNo = 0;
    this._keyInfo = new ImbaseKeyInfo(-1L);
    this._atts = (AttributeTypeProperties[]) null;
  }

  public void Open()
  {
    Logger.Log("Table.Open");
    this._recordNo = 1;
    this._eof = false;
    if (this._recordsTable != null)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      IImbaseServer customService = session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer;
      this._keyInfo = new ImbaseKeyInfo(-1L);
      Guid sessionGuid = session.SessionGUID;
      long linkId = this._linkId;
      string decimalSeparator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
      ref DataTable local1 = ref this._recordsTable;
      ref AttributeTypeProperties[] local2 = ref this._atts;
      ref ImbaseKeyInfo local3 = ref this._keyInfo;
      customService.LoadRecords(sessionGuid, linkId, "", decimalSeparator, out local1, out local2, out local3);
    }
  }

  public string Name
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        QuickObjectInfo objectInfo = sessionKeeper.Session.GetObjectInfo(this._linkId);
        return !objectInfo.Empty ? objectInfo.Caption : string.Empty;
      }
    }
  }

  public string TableName => this._tableName;

  public void GetProperties(out string[] names, out object[] values)
  {
    ImbaseFolder.GetObjectProperties(this._linkId, out names, out values);
  }

  public object GetProperty(object index) => ImbaseFolder.GetObjectProperty(this._linkId, index);

  public void SetProperty(object index, object value)
  {
    ImbaseFolder.SetObjectProperty(this._linkId, index, value);
    this._recordsTable = (DataTable) null;
    this.Open();
  }

  public IIPSImbaseFolder Folder => this._folder;

  public IIPSImbaseRawTable RawTable => (IIPSImbaseRawTable) new ImbaseRawTable(this._tableId);

  private string GetColumnName(object index, out AttributeTypeProperties atp)
  {
    return ImbaseRawTable.GetColumnName(index, this._atts, out atp);
  }
}
