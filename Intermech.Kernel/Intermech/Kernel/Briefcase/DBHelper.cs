// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.DBHelper
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal static class DBHelper
{
  private static string InsertHintAppend(IDbManager db, string source)
  {
    if (db.DataProvider.Name == "Oracle")
    {
      string str = "insert";
      if (source.StartsWith(str, StringComparison.CurrentCultureIgnoreCase))
        return source.Insert(str.Length, " /*+ append */");
    }
    return source;
  }

  public static void ExecuteNonQuery(
    IUserSession session,
    bool insertHintAppend,
    string commandText,
    params IDbDataParameter[] commandParameters)
  {
    IDbManager dataManager = (session as UserSession).DataManager;
    if (insertHintAppend)
      dataManager.ExecuteNonQuery(DBHelper.InsertHintAppend(dataManager, commandText), commandParameters);
    else
      dataManager.ExecuteNonQuery(commandText, commandParameters);
  }

  public static void AddBatchSQL(
    IUserSession session,
    bool insertHintAppend,
    string commandText,
    DbCommandParam[] cmdParams)
  {
    IDbManager dataManager = (session as UserSession).DataManager;
    if (insertHintAppend)
      dataManager.AddBatchSQL(DBHelper.InsertHintAppend(dataManager, commandText), cmdParams);
    else
      dataManager.AddBatchSQL(commandText, cmdParams);
  }

  public static string GetSiteID(UserSession session, long objectID)
  {
    return Convert.ToString(session.DataManager.ExecuteScalar("SELECT F_SITE_ID FROM IMS_OBJECTS WHERE F_OBJECT_ID = :v_objid", session.DataManager.Parameter("v_objid", (object) objectID)));
  }

  public static int GetObjectTypeID(UserSession session, long objectID)
  {
    return Convert.ToInt32(session.DataManager.ExecuteScalar("SELECT F_OBJECT_TYPE FROM IMS_OBJECTS WHERE F_OBJECT_ID = :v_objid", session.DataManager.Parameter("v_objid", (object) objectID)));
  }
}
