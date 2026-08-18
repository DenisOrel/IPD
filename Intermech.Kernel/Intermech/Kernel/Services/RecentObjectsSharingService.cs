// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.RecentObjectsSharingService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.Text;


namespace Intermech.Kernel.Services;

public class RecentObjectsSharingService : LongLifeObject, IRecentObjectsSharingService
{
  private const string ParamName = "RecentObjSharing";

  private object GetModeFromBase(IDbManager db, long userID)
  {
    return db.ExecuteScalar("SELECT F_VALUE FROM IMS_CONFIGS WHERE F_USER_ID = :usrID AND F_MODULE_NAME = :modulName AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", db.Parameter("usrID", (object) userID), db.Parameter("modulName", (object) "KERNEL"), db.Parameter("sectID", (object) "COMMON"), db.Parameter("parName", (object) "RecentObjSharing"));
  }

  public long[] GetAccessObjectIDs(Guid sessionGuid)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    object modeFromBase = this.GetModeFromBase(sessionById.DataManager, sessionById.UserID);
    if (modeFromBase == null || modeFromBase == DBNull.Value || !(modeFromBase.ToString() != string.Empty))
      return new long[0];
    string[] strArray = modeFromBase.ToString().Split(',');
    List<long> longList = new List<long>(strArray.Length);
    for (int index = 0; index < strArray.Length; ++index)
      longList.Add(Convert.ToInt64(strArray[index]));
    return longList.ToArray();
  }

  public void SetAccessObjectIDs(Guid sessionGuid, long[] userGrpIDs)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    IDbManager dataManager = sessionById.DataManager;
    object modeFromBase = this.GetModeFromBase(dataManager, sessionById.UserID);
    StringBuilder stringBuilder = new StringBuilder();
    if (userGrpIDs.Length != 0)
    {
      for (int index = 0; index < userGrpIDs.Length; ++index)
        stringBuilder.Append(userGrpIDs[index].ToString() + ",");
      --stringBuilder.Length;
    }
    if (modeFromBase == null || modeFromBase == DBNull.Value)
      dataManager.ExecuteNonQuery("INSERT INTO IMS_CONFIGS (F_USER_ID, F_MODULE_NAME, F_SECTION_ID, F_PARAM_NAME, F_VALUE) VALUES (:usrID, :modulName, :sectID, :parName, :val1)", dataManager.Parameter("usrID", (object) sessionById.UserID), dataManager.Parameter("modulName", (object) "KERNEL"), dataManager.Parameter("sectID", (object) "COMMON"), dataManager.Parameter("parName", (object) "RecentObjSharing"), dataManager.Parameter("val1", (object) stringBuilder.ToString()));
    else
      dataManager.ExecuteScalar("UPDATE IMS_CONFIGS SET F_VALUE = :val1 WHERE F_USER_ID = :usrID AND F_MODULE_NAME = :modulName AND F_SECTION_ID = :sectID AND F_PARAM_NAME = :parName", dataManager.Parameter("usrID", (object) sessionById.UserID), dataManager.Parameter("modulName", (object) "KERNEL"), dataManager.Parameter("sectID", (object) "COMMON"), dataManager.Parameter("parName", (object) "RecentObjSharing"), dataManager.Parameter("val1", (object) stringBuilder.ToString()));
  }

  public void ValidateAccessMode(Guid sessionGuid, long userID)
  {
    UserSession sessionById = UserSession.GetSessionByID(sessionGuid) as UserSession;
    object obj = this.GetModeFromBase(sessionById.DataManager, userID);
    if (obj == null || obj == DBNull.Value)
      obj = (object) string.Empty;
    this.ValidateAccessMode(sessionById, userID, obj.ToString());
  }

  private void ValidateAccessMode(UserSession user_session, long userID, string grantStr)
  {
    bool flag = user_session.IsAdmin;
    if (!flag && !string.IsNullOrEmpty(grantStr))
    {
      string[] strArray = grantStr.Split(',');
      if (strArray.Length != 0)
      {
        long[] objectIDs = new long[strArray.Length];
        for (int index = 0; index < strArray.Length; ++index)
          objectIDs[index] = Convert.ToInt64(strArray[index]);
        List<Tuple<long, int>> objectTypes = SqlHelper.GetObjectTypes((ICollection<long>) objectIDs, user_session.DataManager);
        List<long> groupsArrayList = user_session.DBSecurity.GetGroupsArrayList();
        foreach (Tuple<long, int> tuple in objectTypes)
        {
          if (tuple.Item1 == user_session.UserID)
          {
            flag = true;
            break;
          }
          if (tuple.Item2 == user_session.IdentHelper.GroupsTypeID)
          {
            for (int index = 0; index < groupsArrayList.Count; ++index)
            {
              if (tuple.Item1 == groupsArrayList[index])
              {
                flag = true;
                break;
              }
            }
          }
          else if (tuple.Item2 == user_session.IdentHelper.RolesTypeID && user_session.RoleID == tuple.Item1)
            flag = true;
          if (flag)
            break;
        }
      }
    }
    if (!flag)
      throw new KernelException(string.Format(sc_13818.ssp_appserver_13819(), (object) user_session.UserName, (object) user_session.GetObjectInfo(userID).Caption));
  }
}
