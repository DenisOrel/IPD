// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImbaseFolders
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Imbase.API;

internal class ImbaseFolders : SingleThreadedObject, IIPSImbaseFolders
{
  private long _parentId;
  private bool _isRoot;
  private DataTable _folders;
  private Dictionary<object, ImbaseFolder> _items = new Dictionary<object, ImbaseFolder>();

  public ImbaseFolders(long parentId, bool isRoot)
  {
    Logger.Log("Folders.Ctor");
    this._parentId = parentId;
    this._isRoot = isRoot;
  }

  private DataTable GetFolders()
  {
    Logger.Log("Folders.GetFolders");
    if (this._folders == null)
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        this._folders = ImbaseCatalog.GetSubfolders(sessionKeeper.Session, this._parentId);
    }
    return this._folders;
  }

  public int Count
  {
    get
    {
      Logger.Log("Folders.Count");
      DataTable folders = this.GetFolders();
      return folders == null ? 0 : folders.Rows.Count;
    }
  }

  public IIPSImbaseFolder Item(object index)
  {
    Logger.Log("Folders.Item " + index.ToString());
    if (this._items.ContainsKey(index))
      return (IIPSImbaseFolder) this._items[index];
    DataTable folders = this.GetFolders();
    if (folders == null)
      return (IIPSImbaseFolder) null;
    DataRow dataRow;
    switch (index)
    {
      case int index1:
        dataRow = folders.Rows[index1];
        break;
      case string _:
        dataRow = (DataRow) null;
        string str = Convert.ToString(index);
        IEnumerator enumerator = folders.Rows.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            DataRow current = (DataRow) enumerator.Current;
            if (str.Equals(Convert.ToString(current[1]), StringComparison.InvariantCultureIgnoreCase))
            {
              dataRow = current;
              break;
            }
          }
          break;
        }
        finally
        {
          if (enumerator is IDisposable disposable)
            disposable.Dispose();
        }
      default:
        return (IIPSImbaseFolder) null;
    }
    if (dataRow == null)
      return (IIPSImbaseFolder) null;
    ImbaseFolder imbaseFolder = new ImbaseFolder(this._parentId, Convert.ToInt64(dataRow[0]));
    this._items[index] = imbaseFolder;
    return (IIPSImbaseFolder) imbaseFolder;
  }

  public IIPSImbaseFolder Add(string newName)
  {
    if (this._items.ContainsKey((object) newName))
      return (IIPSImbaseFolder) this._items[(object) newName];
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      DataTable folders = this.GetFolders();
      if (folders != null)
      {
        foreach (DataRow row in (InternalDataCollectionBase) folders.Rows)
        {
          if (newName.Equals(Convert.ToString(row[1]), StringComparison.InvariantCultureIgnoreCase))
            return (IIPSImbaseFolder) new ImbaseFolder(this._parentId, Convert.ToInt64(row[0]));
        }
      }
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID).Create();
      dbObject.Caption = newName;
      int relationType = MetaDataHelper.GetDefaultRelationTypeID(Intermech.Imbase.Consts.ImbaseFolderTypeID);
      if (relationType == -1)
        relationType = MetaDataHelper.GetRelationTypeID(new Guid("cad00151-306c-11d8-b4e9-00304f19f545"));
      sessionKeeper.Session.GetRelationCollection(relationType).Create(this._parentId, dbObject.ObjectID);
      dbObject.CommitCreation(true);
      this._folders = (DataTable) null;
      ImbaseFolder imbaseFolder = new ImbaseFolder(this._parentId, dbObject.ObjectID);
      this._items[(object) newName] = imbaseFolder;
      return (IIPSImbaseFolder) imbaseFolder;
    }
  }

  public IIPSImbaseFolder FindFolder(object value, IpsFindObject findBy)
  {
    if (value == null)
      return (IIPSImbaseFolder) null;
    Logger.Log("Folders.FindFolder " + value.ToString());
    if (this._items.ContainsKey(value))
      return (IIPSImbaseFolder) this._items[value];
    string key = (string) null;
    int columnIndex;
    switch (findBy)
    {
      case IpsFindObject.IFO_KEY:
        columnIndex = 0;
        break;
      case IpsFindObject.IFO_NAME:
        key = Convert.ToString(value);
        columnIndex = 1;
        break;
      default:
        return (IIPSImbaseFolder) null;
    }
    DataTable folders = this.GetFolders();
    if (folders == null)
      return (IIPSImbaseFolder) null;
    foreach (DataRow row in (InternalDataCollectionBase) folders.Rows)
    {
      if (columnIndex != 1)
      {
        if (row[columnIndex].Equals(value))
        {
          ImbaseFolder folder = new ImbaseFolder(this._parentId, Convert.ToInt64(row[0]));
          this._items[value] = folder;
          return (IIPSImbaseFolder) folder;
        }
      }
      else if (key.Equals(Convert.ToString(row[columnIndex]), StringComparison.InvariantCultureIgnoreCase))
      {
        ImbaseFolder folder = new ImbaseFolder(this._parentId, Convert.ToInt64(row[0]));
        this._items[(object) key] = folder;
        return (IIPSImbaseFolder) folder;
      }
    }
    return (IIPSImbaseFolder) null;
  }
}
