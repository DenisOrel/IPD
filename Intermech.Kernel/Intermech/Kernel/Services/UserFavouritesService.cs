// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.UserFavouritesService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Intermech.Kernel.Services;

public sealed class UserFavouritesService : LongLifeObject, IUserFavouritesService
{
  public const string SectionFavourites = "FAVOURITES";
  public const string ParameterObjectTypes = "OTYPES";

  private UserSession GetSession(Guid sessionGUID)
  {
    if (!(UserSession.GetSessionByID(sessionGUID) is UserSession sessionById))
      throw new KernelException($"Сессия с гуидом {sessionGUID} не найдена.");
    return !sessionById.IsSystemSession ? sessionById : throw new KernelException(sc_13831.ssp_appserver_13832());
  }

  public void IncludeObjects(Guid sessionGUID, long[] objectIDs)
  {
    UserSession session = this.GetSession(sessionGUID);
    ClassifierProcessor.DoAdd(session, session.UserID, objectIDs, string.Empty);
  }

  public void ExcludeObjects(Guid sessionGUID, long[] objectIDs)
  {
    UserSession session = this.GetSession(sessionGUID);
    ClassifierProcessor.DoDelete(session, session.UserID, objectIDs);
  }

  public void ClearFavourites(Guid sessionGUID)
  {
    UserSession session = this.GetSession(sessionGUID);
    session.DataManager.ExecuteNonQuery("DELETE FROM IMS_SELECTIONS WHERE F_FOLDER_ID = :folderID", session.DataManager.Parameter("folderID", (object) session.UserID));
  }

  public int[] GetObjectTypes(Guid sessionGUID)
  {
    UserSession session = this.GetSession(sessionGUID);
    try
    {
      string str = session.Configurations.ReadString("KERNEL", "FAVOURITES", "OTYPES", string.Empty, DBConfigMode.UserOnly);
      if (str == string.Empty)
        return new int[0];
      return ((IEnumerable<string>) str.Split(',')).Select<string, int>(new Func<string, int>(int.Parse)).ToArray<int>();
    }
    catch (Exception ex)
    {
      session.Configurations.WriteString("KERNEL", "FAVOURITES", "OTYPES", string.Empty, session.UserID);
      return new int[0];
    }
  }

  public void AddObjectType(Guid sessionGUID, int objectTypeID)
  {
    UserSession session = this.GetSession(sessionGUID);
    session.GetObjectType(objectTypeID, true);
    string str1 = session.Configurations.ReadString("KERNEL", "FAVOURITES", "OTYPES", string.Empty, DBConfigMode.UserOnly);
    if (str1 == string.Empty)
    {
      session.Configurations.WriteString("KERNEL", "FAVOURITES", "OTYPES", objectTypeID.ToString(), session.UserID);
    }
    else
    {
      if (Array.IndexOf<int>(((IEnumerable<string>) str1.Split(',')).Select<string, int>(new Func<string, int>(int.Parse)).ToArray<int>(), objectTypeID) >= 0)
        return;
      string str2 = $"{str1},{objectTypeID.ToString()}";
      session.Configurations.WriteString("KERNEL", "FAVOURITES", "OTYPES", str2, session.UserID);
    }
  }

  public void DeleteObjectType(Guid sessionGUID, int objectTypeID)
  {
    UserSession session = this.GetSession(sessionGUID);
    List<int> list = ((IEnumerable<string>) session.Configurations.ReadString("KERNEL", "FAVOURITES", "OTYPES", string.Empty, DBConfigMode.UserOnly).Split(',')).Select<string, int>(new Func<string, int>(int.Parse)).ToList<int>();
    if (!list.Remove(objectTypeID))
      return;
    this.ReplaceObjectTypes(session, list);
  }

  private void ReplaceObjectTypes(UserSession session, List<int> typeIDs)
  {
    if (typeIDs.Count == 0)
    {
      session.Configurations.WriteString("KERNEL", "FAVOURITES", "OTYPES", string.Empty, session.UserID);
    }
    else
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (int typeId in typeIDs)
        stringBuilder.Append(typeId.ToString() + ",");
      --stringBuilder.Length;
      session.Configurations.WriteString("KERNEL", "FAVOURITES", "OTYPES", stringBuilder.ToString(), session.UserID);
    }
  }
}
