
// Type: Intermech.Security.SecurityProcs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Collections.Specialized;
using System.Data;


namespace Intermech.Security;

internal class SecurityProcs
{
  internal static readonly string F_UID = "UID";
  internal static long rightUIDGenerator = 0;

  /// <summary>
  /// Вычисляет хэш записи таблицы прав
  /// участвует F_USER_ID + F_BEGIN_DATE + F_END_DATE + F_CONDITION
  /// </summary>
  /// <param name="dr"></param>
  /// <returns></returns>
  public static int GetRightsRowHashCode(DataRow dr)
  {
    return SecurityProcs.GetRightsRowHashCode(Convert.ToInt64(dr["F_USER_ID"]), dr["F_BEGIN_DATE"], dr["F_END_DATE"], dr["F_CONDITION_ID"]);
  }

  /// <summary>Вычисляет хэш</summary>
  /// <param name="userId"></param>
  /// <param name="startDate"></param>
  /// <param name="endDate"></param>
  /// <param name="condition"></param>
  /// <returns></returns>
  public static int GetRightsRowHashCode(
    long userId,
    object startDate,
    object endDate,
    object condition)
  {
    startDate = startDate == null || startDate == DBNull.Value ? (object) DBNull.Value : (object) Convert.ToDateTime(startDate);
    endDate = endDate == null || endDate == DBNull.Value ? (object) DBNull.Value : (object) Convert.ToDateTime(endDate);
    condition = condition == null || condition == DBNull.Value ? (object) Convert.ToInt64(0) : (object) Convert.ToInt64(condition);
    return userId.GetHashCode() ^ startDate.GetHashCode() << 4 ^ endDate.GetHashCode() << 6 ^ condition.ToString().GetHashCode() << 12;
  }

  /// <summary>
  /// функция получения нового уникального идентификатора для группировки прав в таблицах получаемых от сервера прав
  /// </summary>
  internal static long GetNewRightUID
  {
    get
    {
      ++SecurityProcs.rightUIDGenerator;
      return SecurityProcs.rightUIDGenerator;
    }
  }

  /// <summary>
  /// группирует таблицу прав добавлением колонки с уникальными идентификаторами
  /// 
  /// требуется после зачитки прав с сервера для последующего редактировани
  /// </summary>
  /// <param name="accessDataTable"></param>
  internal static DataTable GroupRightsByUID(DataTable accessDataTable)
  {
    if (accessDataTable != null && accessDataTable.Columns.IndexOf(SecurityProcs.F_UID) == -1)
    {
      accessDataTable.Columns.Add(SecurityProcs.F_UID, typeof (long));
      HybridDictionary hybridDictionary = new HybridDictionary();
      foreach (DataRow row in (InternalDataCollectionBase) accessDataTable.Rows)
      {
        int rightsRowHashCode = SecurityProcs.GetRightsRowHashCode(row);
        long getNewRightUid;
        if (hybridDictionary.Contains((object) rightsRowHashCode))
        {
          getNewRightUid = (long) hybridDictionary[(object) rightsRowHashCode];
        }
        else
        {
          getNewRightUid = SecurityProcs.GetNewRightUID;
          hybridDictionary[(object) rightsRowHashCode] = (object) getNewRightUid;
        }
        row[SecurityProcs.F_UID] = (object) getNewRightUid;
      }
      accessDataTable.AcceptChanges();
    }
    return accessDataTable;
  }

  /// <summary>
  /// дегруппирует таблицу прав удаленим колонки с уникальными идентификаторами
  /// 
  /// требуется после редактировани таблицы прав перед передачей их на сервер для сохранения
  /// </summary>
  /// <param name="accessDataTable"></param>
  internal static DataTable DegroupRightsByUID(DataTable accessDataTable)
  {
    if (accessDataTable != null)
    {
      int index = accessDataTable.Columns.IndexOf(SecurityProcs.F_UID);
      if (index != -1)
      {
        accessDataTable.Columns.RemoveAt(index);
        accessDataTable.AcceptChanges();
      }
    }
    return accessDataTable;
  }

  /// <summary>Собираем словарь "хэш записи"-"UID"</summary>
  /// <param name="accessDataTable"></param>
  /// <returns></returns>
  internal static HybridDictionary SaveUIDByHash(DataTable accessDataTable)
  {
    HybridDictionary hybridDictionary = new HybridDictionary();
    foreach (DataRow row in (InternalDataCollectionBase) accessDataTable.Rows)
      hybridDictionary[(object) SecurityProcs.GetRightsRowHashCode(row)] = row[SecurityProcs.F_UID];
    return hybridDictionary;
  }

  /// <summary>
  /// Для записей с совпадающим хэшем возвращаем сохраненные UID
  /// </summary>
  /// <param name="accessDataTable"></param>
  /// <returns></returns>
  internal static DataTable RestoreUIDByHash(DataTable dataTable, HybridDictionary uidHash)
  {
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      int rightsRowHashCode = SecurityProcs.GetRightsRowHashCode(row);
      if (uidHash.Contains((object) rightsRowHashCode))
        row[SecurityProcs.F_UID] = uidHash[(object) rightsRowHashCode];
    }
    dataTable.AcceptChanges();
    return dataTable;
  }

  /// <summary>
  /// Убираем права по умолчанию - для уменьшения трафика перед сохранением прав, т.к. ядру по барабану на права по умолчанию
  /// </summary>
  /// <param name="dataTable"></param>
  /// <returns></returns>
  internal static DataTable ExcludeDefaultRights(DataTable dataTable)
  {
    for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
    {
      DataRow row = dataTable.Rows[index];
      if (Convert.ToInt64(row["F_PARENT_KEY"]) == -1L)
        row.Delete();
    }
    dataTable.AcceptChanges();
    return dataTable;
  }
}
