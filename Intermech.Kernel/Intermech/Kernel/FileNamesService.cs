// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.FileNamesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Pools;
using Intermech.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;


namespace Intermech.Kernel;

public class FileNamesService : LongLifeObject, IFileNamesService
{
  public string GetUniqueFileName(string fileName, long id, Guid sessionGuid)
  {
    if (string.IsNullOrEmpty(fileName))
      return fileName;
    IDbManager dataManager = (UserSession.GetSessionByID(sessionGuid) as UserSession).DataManager;
    object obj = dataManager.ExecuteScalar("SELECT F_KEY FROM IMS_FILENAMES WHERE F_FILENAME = :fname AND F_ID <> :id1", dataManager.Parameter("fname", (object) fileName.ToUpperInvariant()), dataManager.Parameter("id1", (object) id));
    if (obj != null && obj != DBNull.Value)
    {
      long num = dataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", dataManager);
      string directoryName = Path.GetDirectoryName(fileName);
      fileName = $"{directoryName}{(directoryName.Length > 0 ? (object) Path.DirectorySeparatorChar.ToString() : (object) string.Empty)}{Path.GetFileNameWithoutExtension(fileName)}_{num}{Path.GetExtension(fileName)}";
    }
    return fileName;
  }

  public long GetIDByFileName(string fileName, Guid sessionGuid)
  {
    IDbManager dataManager = (UserSession.GetSessionByID(sessionGuid) as UserSession).DataManager;
    object obj = dataManager.ExecuteScalar("SELECT F_ID FROM IMS_FILENAMES WHERE F_FILENAME = :fname", dataManager.Parameter("fname", (object) fileName.ToUpperInvariant()));
    return obj != null && obj != DBNull.Value ? Convert.ToInt64(obj) : -1L;
  }

  private DataTable GetFileNameTable(List<string> fileName, UserSession session)
  {
    IDbManager dataManager = session.DataManager;
    List<IDbDataParameter> dbDataParameterList = new List<IDbDataParameter>(fileName.Count);
    using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
    {
      StringBuilder stringBuilder = objectPoolScope.Object;
      for (int index = 0; index < fileName.Count; ++index)
      {
        stringBuilder.AppendFormat($":f{index.ToString()},");
        dbDataParameterList.Add(dataManager.Parameter("f" + index.ToString(), (object) fileName[index].ToUpperInvariant()));
      }
      --stringBuilder.Length;
      return dataManager.ExecuteDataTable($"SELECT F.F_KEY F_OBJECT_ID, F.F_ID, F.F_FILENAME, O.F_OBJECT_TYPE, (SELECT F_MODIFY_MODE FROM IMS_LC_STEPS WHERE IMS_LC_STEPS.F_LC_STEP = O.F_LC_STEP) F_MODIFY_MODE FROM IMS_FILENAMES F, IMS_OBJECTS O WHERE F_FILENAME IN ({stringBuilder.ToString()}) AND O.F_OBJECT_VER_TYPE <> -1 AND O.F_OBJECT_ID = F.F_KEY ORDER BY F.F_FILENAME", dbDataParameterList.ToArray());
    }
  }

  public DataTable GetFileNameTable(string[] fileName, Guid sessionGuid)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    if (fileName.Length == 0)
      fileName = new string[1]{ "?" };
    DataTable toTable = (DataTable) null;
    int num = 0;
    List<string> fileName1 = new List<string>();
    for (int index = 0; index < fileName.Length; ++index)
    {
      fileName1.Add(fileName[index]);
      if (++num > 500)
      {
        DataTable fileNameTable = this.GetFileNameTable(fileName1, sessionById);
        if (toTable == null)
          toTable = fileNameTable;
        else
          SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) fileNameTable.Select());
        fileName1.Clear();
        num = 0;
      }
    }
    if (fileName1.Count > 0)
    {
      DataTable fileNameTable = this.GetFileNameTable(fileName1, sessionById);
      if (toTable == null)
        toTable = fileNameTable;
      else
        SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) fileNameTable.Select());
    }
    return toTable;
  }

  public DataTable GetFileNameTable(string fileName, Guid sessionGuid)
  {
    return this.GetFileNameTable(new string[1]{ fileName }, sessionGuid);
  }

  public long[] GetObjectIDByFileName(string fileName, Guid sessionGuid)
  {
    IDbManager dataManager = (UserSession.GetSessionByID(sessionGuid) as UserSession).DataManager;
    DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_KEY FROM IMS_FILENAMES WHERE F_FILENAME = :fname", dataManager.Parameter("fname", (object) fileName.ToUpperInvariant()));
    long[] objectIdByFileName = new long[dataTable.Rows.Count];
    for (int index = 0; index < dataTable.Rows.Count; ++index)
      objectIdByFileName[index] = Convert.ToInt64(dataTable.Rows[index][0]);
    return objectIdByFileName;
  }

  public DataTable GetFilesTable(long[] objectIDs, Guid sessionGuid)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    DataTable dataTable1 = sessionById.GetObjectCollection(sessionById.IdentHelper.StorageTypeID).Select(new DBRecordSetParams((ConditionStructure[]) null, new object[3]
    {
      (object) -50,
      (object) new Guid("cad00028-306c-11d8-b4e9-00304f19f545"),
      (object) -2
    }));
    DataTable toTable = (DataTable) null;
    BlobStoragesPool service = ServerServices.GetService(typeof (IBlobStoragesPool)) as BlobStoragesPool;
    for (int index1 = 0; index1 < dataTable1.Rows.Count; ++index1)
    {
      IBlobStorage storage = service.GetStorage(Convert.ToInt64(dataTable1.Rows[index1][2]), (IUserSession) sessionById);
      try
      {
        string str = storage.StorageName;
        if (str == string.Empty)
          str = "IMS_STORAGE";
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate())
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          int num = 0;
          for (int index2 = 0; index2 < objectIDs.Length; ++index2)
          {
            stringBuilder.Append(objectIDs[index2].ToString() + ",");
            if (++num > 500 || index2 == objectIDs.Length - 1)
            {
              --stringBuilder.Length;
              try
              {
                DataTable dataTable2 = storage.DataManager.ExecuteDataTable($"SELECT F_FILE_ID, F_FILENAME, F_FILESIZE, F_FILEDATE, F_ARC_METHOD, F_ZIPSIZE, F_OBJECTLINK_ID, F_LINKTYPE FROM {str} WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECTLINK_ID IN ({stringBuilder.ToString()})", storage.DataManager.Parameter("attrID", (object) sessionById.IdentHelper.FileAttributeID));
                if (toTable == null)
                  toTable = dataTable2;
                else if (dataTable2.Rows.Count > 0)
                {
                  bool flag = true;
                  long int64 = Convert.ToInt64(dataTable2.Rows[0]["F_FILE_ID"]);
                  for (int index3 = 0; index3 < toTable.Rows.Count; ++index3)
                  {
                    if (Convert.ToInt64(toTable.Rows[index3]["F_FILE_ID"]) == int64)
                    {
                      flag = false;
                      break;
                    }
                  }
                  if (flag)
                    SqlHelper.AssignRows(toTable, (IEnumerable<DataRow>) dataTable2.Select());
                }
                stringBuilder.Length = 0;
                num = 0;
              }
              catch (Exception ex)
              {
                stringBuilder.Length = 0;
                num = 0;
                sessionById.EventLogHelper.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_985"), (object) dataTable1.Rows[index1][0].ToString(), (object) ex.Message), Consts.traceAlways, string.Empty);
              }
            }
          }
        }
      }
      finally
      {
        service.ReleaseStorage(storage);
      }
    }
    if (toTable != null)
      toTable.Columns[3].DateTimeMode = DataSetDateTime.Unspecified;
    return toTable;
  }

  public long GetNextFileID(Guid sessionGuid)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    return sessionById.DataManager.DataProvider.NextGeneratorValue("IMS_FILE_ID_GEN", sessionById.DataManager);
  }
}
