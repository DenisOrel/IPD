// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.TemporaryAccessService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Server.Security;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services;

public class TemporaryAccessService : LongLifeObject, ITemporaryAccessService, IInternalUserSessions
{
  public void ClearAccess(IUserSession session, long toUserID, long toObjectID)
  {
    IDbManager dataManager = (session as UserSession).DataManager;
    dataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_USER_ID = :usrID AND F_CATEGORY_TYPE = :catType AND F_CATEGORY_ID = :catID AND F_RIGHT_TYPE = :rtType", dataManager.Parameter("usrID", (object) toUserID), dataManager.Parameter("catType", (object) 1), dataManager.Parameter("catID", (object) toObjectID), dataManager.Parameter("rtType", (object) Convert.ToInt32((object) AccessType.GrantAlways)));
    if (session.UserID == toUserID)
      (session as UserSession).DBSecurity.SetClearCacheFlag();
    else
      (UserSession.Sessions as UserSessionCollection).SetDBSecurityClearCacheFlag(toUserID);
  }

  public void GrantAccess(
    IUserSession session,
    long fromUserID,
    long toUserID,
    long toObjectID,
    bool acRead,
    bool acWrite,
    bool acAdmin)
  {
    UserSession userSession;
    bool flag;
    if (session.UserID == fromUserID)
    {
      userSession = session as UserSession;
      flag = false;
    }
    else
    {
      userSession = new UserSession();
      userSession.SetLoginCapabilities(allowLoginWithoutPassword: true);
      userSession.InternalLogin(session as UserSession, -1L, fromUserID, nameof (GrantAccess));
      flag = true;
    }
    try
    {
      IDBObject dbObject = userSession.GetObject(toObjectID, true);
      (dbObject as IDBSecurity).CheckAccess(ActionType.SetAccess);
      IDbManager dataManager = (session as UserSession).DataManager;
      Dictionary<ActionType, ActionCategory> accessTypesCategory = (dbObject as DBSessionable).GetAccessTypesCategory();
      long num = 0;
      foreach (KeyValuePair<ActionType, ActionCategory> keyValuePair in accessTypesCategory)
      {
        if (acRead && keyValuePair.Value == ActionCategory.Read || acWrite && keyValuePair.Value == ActionCategory.Write || acAdmin && keyValuePair.Value == ActionCategory.Admin)
          dataManager.ExecuteSpNonQuery("IMS_ADD_CATEGORY_ACCESS", dataManager.Parameter("inCATEGORY_TYPE", (object) 1), dataManager.Parameter("inCATEGORY_ID", (object) toObjectID), dataManager.Parameter("inRIGHT_ID", (object) Convert.ToInt32((object) keyValuePair.Key)), dataManager.Parameter("inUSER_ID", (object) toUserID), dataManager.Parameter("inRIGHT_TYPE", (object) Convert.ToInt32((object) AccessType.GrantAlways)), dataManager.Parameter("inOWNER_ID", (object) fromUserID), dataManager.Parameter("inPARENT_KEY", (object) 0L), dataManager.OutputParameter("outKEY", (object) num));
      }
    }
    finally
    {
      if (flag)
        userSession.Logout(nameof (GrantAccess));
    }
  }

  public string[] GetAccessReport(Guid userSession, long[] usersID, long[] objectsID)
  {
    UserSession sessionById = UserSession.GetSessionByID(userSession) as UserSession;
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_13827.ssp_appserver_13828(1457988019));
    List<string> stringList = new List<string>();
    for (int index1 = 0; index1 < usersID.Length; ++index1)
    {
      RoleProperties[] rolesList = sessionById.GetRolesList(usersID[index1]);
      for (int index2 = 0; index2 < rolesList.Length; ++index2)
      {
        UserSession userSession1 = new UserSession();
        userSession1.SetLoginCapabilities(allowLoginWithoutPassword: true);
        try
        {
          userSession1.InternalLogin(sessionById, rolesList[index2].RoleID, usersID[index1], nameof (GetAccessReport));
        }
        catch (AccessDeniedException ex)
        {
          stringList.Add(string.Empty);
          stringList.Add($"Пользователю {sessionById.GetObjectInfo(usersID[index1]).Caption} назначена роль {sessionById.GetObjectInfo(rolesList[index2].RoleID).Caption}, под которой невозможно зайти в систему.");
          continue;
        }
        try
        {
          stringList.Add(string.Empty);
          stringList.Add($"========= Проверка пользователя {userSession1.UserName} в роли {rolesList[index2].RoleName} (уровень доступа {userSession1.DBCache.GetAccessCaption(userSession1.SecurityLevel)}) =========");
          for (int index3 = 0; index3 < objectsID.Length; ++index3)
          {
            DBObject dbObject = userSession1.GetObject(objectsID[index3]) as DBObject;
            stringList.Add(string.Empty);
            stringList.Add($"Объект {dbObject.NameInMessages} [версия {dbObject.VersionID}]:");
            stringList.Add(string.Empty);
            string str1 = dbObject.AccessLevel <= userSession1.SecurityLevel ? (!dbObject.ReadOnly ? "Объект доступен для изменения." : (dbObject.CheckoutBy == 0L || dbObject.CheckoutBy == userSession1.UserID ? (dbObject.ObjectModifyMode == ObjectModifyModes.CantModify || dbObject.ObjectModifyMode == ObjectModifyModes.CreateVersion ? $"Объект нельзя менять, т.к. шаг ЖЦ '{dbObject.LCStepObject.LCName}' не допускает его изменения." : (dbObject.SiteID.Length <= 0 || !dbObject.ReadonlyPublishedObject(false) ? "Объект не доступен для изменения." : "Объект запрещено изменять, т.к. он принадлежит другому узлу информационной системы.")) : "Объект нельзя менять, т.к. он взят на изменение пользователем " + sessionById.GetObjectInfo(dbObject.CheckoutBy).Caption)) : "Объект не виден пользователю, т.к. имеет уровень доступа " + userSession1.DBCache.GetAccessCaption(dbObject.AccessLevel);
            stringList.Add(str1);
            stringList.Add("Права доступа к объекту:");
            List<ActionType> possibleActions1 = dbObject.GetPossibleActions();
            for (int index4 = 0; index4 < possibleActions1.Count; ++index4)
            {
              if (possibleActions1[index4] != ActionType.NextLCStep)
              {
                string actionName = sessionById.EventLogHelper.GetActionName(dbObject.CategoryType, dbObject.GetCategoryID4ActionName(dbObject.ObjectID), possibleActions1[index4]);
                string str2 = !dbObject.CheckAccess(possibleActions1[index4], dbObject.GetDefaultAccess(possibleActions1[index4]), false) ? "Запрещено" : "Разрешено";
                stringList.Add($"{actionName} : {str2}");
              }
            }
            foreach (IDBSecurity dbSecurity in dbObject.GetRelatedSecurity())
            {
              if (dbSecurity is DBSessionable dbSessionable)
              {
                List<ActionType> possibleActions2 = dbSessionable.GetPossibleActions();
                for (int index5 = 0; index5 < possibleActions2.Count; ++index5)
                {
                  if (!possibleActions1.Contains(possibleActions2[index5]))
                  {
                    string actionName = sessionById.EventLogHelper.GetActionName(dbSessionable.CategoryType, dbSessionable.GetCategoryID4ActionName(dbSessionable.CategoryID), possibleActions2[index5]);
                    string str3 = !dbSessionable.CheckAccess(possibleActions2[index5], dbSessionable.GetDefaultAccess(possibleActions2[index5]), false) ? "Запрещено" : "Разрешено";
                    stringList.Add($"{actionName} : {str3}");
                  }
                }
              }
            }
          }
        }
        finally
        {
          userSession1.Logout(nameof (GetAccessReport));
        }
      }
    }
    return stringList.ToArray();
  }
}
