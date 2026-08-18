// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImbaseCatalog
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Kernel.Search;
using Intermech.Runtime.ComInterop.LocalServer;
using System;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Intermech.Imbase.API;

internal class ImbaseCatalog : SingleThreadedObject, IIPSImbaseCatalog
{
  private long _catalogId;
  private ImbaseFolders _folders;

  internal static DataTable GetSubfolders(IUserSession session, long parentId)
  {
    Logger.Log("Catalog.GetSubfolders");
    return session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseFolderTypeID).Select(ImbaseCatalog.CreateParamsSet(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) parentId, LogicalOperators.AND, 0, false)
    }));
  }

  internal static DataTable GetFolderTables(IUserSession session, long parentId)
  {
    Logger.Log("Catalog.GetSubfolders");
    int tickCount = Environment.TickCount;
    DataTable folderTables = session.GetObjectCollection(Intermech.Imbase.Consts.ImbaseTableRefTypeID).Select(ImbaseCatalog.CreateParamsSet(new ConditionStructure[1]
    {
      new ConditionStructure((string) null, RelationalOperators.EntersIn, (object) parentId, LogicalOperators.AND, 0, false)
    }));
    int num = Environment.TickCount - tickCount;
    return folderTables;
  }

  internal static void DeleteObject(long objectId)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObjectActualCopy(objectId, false)?.Delete(0L);
  }

  internal static DBRecordSetParams CreateParamsSet(ConditionStructure[] conds)
  {
    ColumnDescriptor[] columns = new ColumnDescriptor[2]
    {
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.F_OBJECT_ID), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.NONE, 0),
      new ColumnDescriptor((object) Convert.ToInt32((object) ObligatoryObjectAttributes.CAPTION), AttributeSourceTypes.Object, ColumnContents.Text, ColumnNameMapping.ID, SortOrders.ASC, 2)
    };
    DBRecordSetParams paramsSet = new DBRecordSetParams(conds, columns);
    paramsSet.TableName = "f";
    paramsSet.FailIfNotFound = false;
    if (paramsSet.Tags == null)
      paramsSet.Tags = new HybridDictionary();
    return paramsSet;
  }

  public ImbaseCatalog(long catalogId) => this._catalogId = catalogId;

  public string Name
  {
    get
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        return sessionKeeper.Session.GetObjectInfo(this._catalogId).Caption;
    }
    set
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.GetObject(this._catalogId).Caption = value;
    }
  }

  public IIPSImbaseFolders Folders
  {
    get
    {
      Logger.Log("Catalog->Folders");
      if (this._folders == null)
        this._folders = new ImbaseFolders(this._catalogId, true);
      return (IIPSImbaseFolders) this._folders;
    }
  }

  public IIPSImbaseFolder FindFolder(object value, IpsFindObject findBy)
  {
    Logger.Log($"Catalog->FindFolder '{value.ToString()}'");
    DataTable dataTable = (DataTable) null;
    switch (findBy)
    {
      case IpsFindObject.IFO_KEY:
        int columnIndex = 0;
        using (SessionKeeper sessionKeeper = new SessionKeeper())
          dataTable = ImbaseCatalog.GetSubfolders(sessionKeeper.Session, this._catalogId);
        if (dataTable == null)
          return (IIPSImbaseFolder) null;
        foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        {
          if (row[columnIndex].Equals(value))
            return (IIPSImbaseFolder) new ImbaseFolder(this._catalogId, Convert.ToInt64(row[0]));
        }
        return (IIPSImbaseFolder) null;
      case IpsFindObject.IFO_NAME:
      case IpsFindObject.IFO_PATH:
        string str = value as string;
        if (string.IsNullOrEmpty(str))
          return (IIPSImbaseFolder) null;
        string[] strArray = str.Split(new char[1]{ '\\' }, StringSplitOptions.RemoveEmptyEntries);
        IIPSImbaseFolders folders = this.Folders;
        IIPSImbaseFolder folder = (IIPSImbaseFolder) null;
        foreach (string index in strArray)
        {
          folder = folders.Item((object) index);
          if (folder == null)
            return (IIPSImbaseFolder) null;
          folders = folder.Folders;
        }
        return folder;
      default:
        return (IIPSImbaseFolder) null;
    }
  }

  public int GetTableId() => (int) this._catalogId;
}
