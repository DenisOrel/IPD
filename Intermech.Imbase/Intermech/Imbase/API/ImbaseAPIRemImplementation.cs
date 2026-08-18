// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.API.ImbaseAPIRemImplementation
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using Intermech.Navigator.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.API;

internal class ImbaseAPIRemImplementation : IImbaseAPIRem
{
  private readonly int _apiVersion = 16389;
  internal static ImbaseAPIRemImplementation _APIImplementation;

  internal ImbaseAPIRemImplementation() => ImbaseAPIRemImplementation._APIImplementation = this;

  public int Version => this._apiVersion;

  public int SelectFromTable(
    string catalogDef,
    string objectDef,
    string filter,
    string showFields,
    string sortOrder,
    int recordsCount,
    string comment,
    ref DataTable records,
    ref FieldInfo[] fields,
    ref ContextInfo context)
  {
    long recordKey = -1;
    using (TableViewForm tableViewForm = new TableViewForm())
    {
      tableViewForm.InitializeView(objectDef, catalogDef, filter, showFields, sortOrder, comment);
      int data;
      if (recordsCount == 1)
      {
        if (tableViewForm.ShowDialog() != DialogResult.OK)
          return 0;
        data = tableViewForm.GetData(ref records, ref fields, ref context, recordsCount, ref recordKey);
      }
      else
      {
        if (tableViewForm.HasTree && tableViewForm.ShowOnlyTree() != DialogResult.OK)
          return 0;
        data = tableViewForm.GetData(ref records, ref fields, ref context, recordsCount, ref recordKey);
      }
      return data;
    }
  }

  public int CreateObject(long recordId, long linkId, ref string objectGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      long objectID = (session.GetCustomService(typeof (IImbaseServer)) as IImbaseServer).CreateObject(session.SessionGUID, -1L, linkId, recordId, true, -1);
      IDBObject dbObject = session.GetObject(objectID);
      objectGuid = dbObject.GUID.ToString();
      return 1;
    }
  }

  public int CreateObjectFromTempKey(string tempKey, ref string objectGuid)
  {
    return CadmechHelper.CreateObjectFromTempKey(tempKey, ref objectGuid);
  }

  public int ShowPropertyWindow(string guids)
  {
    if (guids == null || guids.Length == 0)
      return 0;
    string[] strArray = guids.Split(',');
    int length = strArray.Length;
    IInvokeService service = ServicesManager.GetService(typeof (IInvokeService)) as IInvokeService;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      for (int index = 0; index < length; ++index)
      {
        if (strArray[index].Length > 0)
        {
          string str = strArray[index];
          if (str.Length == 38 && (str.StartsWith("IG") || str.StartsWith("IV")))
          {
            Guid guid = new Guid(str.Substring(2));
            IDBObject obj = (IDBObject) null;
            obj = !str.StartsWith("IV") ? sessionKeeper.Session.GetObjectByID(guid, true) : sessionKeeper.Session.GetObject(guid);
            service.InvokeAction(-1, (Action) (() => this.ShowPropertyWindowFunc(obj.ObjectID)));
          }
        }
      }
    }
    return 1;
  }

  public int MaterialEntry(string command, ref string fileData)
  {
    string[] fileData1 = fileData.Split('\n');
    int num;
    try
    {
      num = CadmechHelper.Execute(command, fileData1);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message);
    }
    StringBuilder stringBuilder = new StringBuilder();
    List<string> resultList = CadmechHelper.ResultList;
    int count = resultList.Count;
    for (int index = 0; index < count; ++index)
    {
      if (index > 0)
        stringBuilder.Append('\n');
      stringBuilder.Append(resultList[index]);
    }
    fileData = stringBuilder.ToString();
    return num;
  }

  internal void ShowPropertyWindowFunc(long objectId)
  {
    int num = (int) PropertiesWindow.Execute(string.Empty, string.Empty, objectId);
  }

  public int GetKeyInfo(
    string key,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList)
  {
    return CadmechHelper.GetKeyInfo(key, ref tableRecord, ref catalogRecord, ref keysList);
  }

  public int ShowTables(
    int showFlags,
    string fieldNames,
    ref string tableRecord,
    ref string catalogRecord,
    ref string keysList)
  {
    return CadmechHelper.ShowTables(showFlags, fieldNames, ref tableRecord, ref catalogRecord, ref keysList);
  }

  public int SelectTable(
    long catalogId,
    string prompt,
    ref long tableId,
    ref string fullList,
    ref long recordKey)
  {
    long num = Intermech.Imbase.API.SelectTable.Select(catalogId, prompt, ref tableId, ref fullList);
    if (num == -1L)
      return 1;
    recordKey = num;
    return 0;
  }

  public int SelectFolder(long catalogId, string prompt, ref long folderId, ref string fullList)
  {
    long num = Intermech.Imbase.API.SelectFolder.Select(catalogId, prompt, ref fullList);
    if (num == -1L)
      return 1;
    folderId = num;
    return 0;
  }
}
