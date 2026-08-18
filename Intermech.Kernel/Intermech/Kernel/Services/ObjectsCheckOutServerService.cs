// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.ObjectsCheckOutServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel.Services;

public class ObjectsCheckOutServerService : LongLifeObject, IObjectsCheckOutServerService
{
  private IUserSession GetUserSession(object usrSession)
  {
    switch (usrSession)
    {
      case IUserSession _:
        return usrSession as IUserSession;
      case Guid sessionGUID:
        return UserSession.GetSessionByID(sessionGUID);
      case string _:
        return UserSession.GetSessionByID(new Guid((string) usrSession));
      default:
        return (IUserSession) null;
    }
  }

  private ObjectCheckOutVersionDescription IDBObject2ObjectCheckOutVersionDescription(
    IDBObject obj,
    ObjectCheckedOutVersionMode mode)
  {
    if (obj == null)
      return (ObjectCheckOutVersionDescription) null;
    return new ObjectCheckOutVersionDescription(obj)
    {
      Mode = mode
    };
  }

  private ObjectCheckOutVersionDescription CheckOutVersion(
    UserSession session,
    ObjectCheckOutVersionDescription version,
    ObjectCheckedOutVersionsHolder holder)
  {
    if (session == null || version == null || version.F_OBJECT_ID == 0L || holder == null)
      return (ObjectCheckOutVersionDescription) null;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(version.F_OBJECT_TYPE);
    if (objectType == null || objectType.VersionsMode == ObjectVersionModes.Abstract)
      throw new KernelException(LocalizationHolder.rm.GetString("Kernel_1027"));
    IDBObject objectActualCopy = session.GetObjectActualCopy(version.F_OBJECT_ID, true);
    if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.InBase)
      return this.IDBObject2ObjectCheckOutVersionDescription(objectActualCopy, ObjectCheckedOutVersionMode.InBase);
    if (objectActualCopy.CheckoutBy == session.UserID)
      return this.IDBObject2ObjectCheckOutVersionDescription(objectActualCopy, ObjectCheckedOutVersionMode.ActualCopy);
    if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.CantModify)
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1028"), (object) objectActualCopy.ObjectID, (object) objectActualCopy.Caption)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectActualCopy.ObjectID));
    if (objectActualCopy.CheckoutBy != 0L)
    {
      QuickObjectInfo objectInfo = session.GetObjectInfo(objectActualCopy.CheckoutBy);
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_1029"), (object) objectActualCopy.ObjectID, (object) objectActualCopy.Caption, (object) objectInfo.Caption)).WithRecoveryActions((ErrorRecoveryAction) new OpenIPSObjectRecoveryAction(objectActualCopy.ObjectID));
    }
    if (objectActualCopy.ObjectModifyMode == ObjectModifyModes.Checkout)
      return this.IDBObject2ObjectCheckOutVersionDescription(objectActualCopy.CheckOut(), ObjectCheckedOutVersionMode.CheckOut);
    if (objectActualCopy.ObjectModifyMode != ObjectModifyModes.CreateVersion)
      return (ObjectCheckOutVersionDescription) null;
    bool startedLogHistory = session.IsStartedLogHistory;
    try
    {
      long num = Math.Abs(objectActualCopy.ObjectID);
      for (int index = 0; index < holder.PairVersionSources.Count; ++index)
      {
        if (num == Math.Abs(holder.PairVersionSources[index].F_OBJECT_ID))
        {
          IDBObject dbObject = session.GetObject(holder.PairVersionTargets[index].F_OBJECT_ID);
          holder.PairVersionSources.RemoveAt(index);
          holder.PairVersionTargets.RemoveAt(index);
          if (dbObject.IsCreationMode)
            dbObject.CommitCreation(true, true);
          if (dbObject.ObjectModifyMode == ObjectModifyModes.Checkout && dbObject.ObjectID > 0L)
            dbObject = dbObject.CheckOut();
          return this.IDBObject2ObjectCheckOutVersionDescription(dbObject, ObjectCheckedOutVersionMode.NewVersion);
        }
      }
      IDBObjectCollection objectCollection = session.GetObjectCollection(objectActualCopy.ObjectType);
      IDBObject dbObject1 = (IDBObject) null;
      try
      {
        if (!startedLogHistory)
          session.StartLogHistory();
        dbObject1 = objectCollection.CreateVersion(objectActualCopy.ObjectID);
        dbObject1.CommitCreation(true, true);
      }
      finally
      {
        if (!startedLogHistory && session.IsStartedLogHistory)
          session.StopLogHistory();
      }
      List<CategoryValue> modificationsHistoryList = session.GetModificationsHistoryList();
      if (modificationsHistoryList != null && modificationsHistoryList.Count > 0)
      {
        for (int index = 0; index < modificationsHistoryList.Count; ++index)
        {
          CategoryValue categoryValue = modificationsHistoryList[index];
          if (categoryValue.CategoryType == 1 && categoryValue.ActionID == ActionType.Create && categoryValue.CategoryID != dbObject1.ObjectID)
          {
            IDBObject source = session.GetObject(categoryValue.CategoryID, false);
            if (source != null)
            {
              if (source.IsCreationMode)
                source.CommitCreation(true, true);
              long objectID = source.ParentVersionID;
              if (objectID > 0L && !session.GetObjectInfo(-objectID).Empty)
                objectID = -objectID;
              ObjectCheckOutVersionDescription versionDescription = new ObjectCheckOutVersionDescription(session.GetObject(objectID));
              holder.PairVersionSources.Add(versionDescription);
              holder.PairVersionTargets.Add(new ObjectCheckOutVersionDescription(source)
              {
                Mode = ObjectCheckedOutVersionMode.NewVersion
              });
            }
          }
        }
      }
      return this.IDBObject2ObjectCheckOutVersionDescription(dbObject1, ObjectCheckedOutVersionMode.NewVersion);
    }
    catch
    {
      if (!startedLogHistory && session.IsStartedLogHistory)
        session.StopLogHistory();
      throw;
    }
  }

  public ObjectCheckedOutVersionsHolder CheckOut(
    object usrSession,
    IList<long> versions,
    bool throwException)
  {
    IDBTransactions dbTransactions = this.GetUserSession(usrSession) is UserSession userSession ? userSession.GetCustomService(typeof (IDBTransactions)) as IDBTransactions : throw new KernelExceptionID(sc_14026.ssp_appserver_14027(422240048), (object) "ObjectsCheckOutServerService.CheckOut");
    List<long> longList = new List<long>((IEnumerable<long>) versions);
    ObjectCheckedOutVersionsHolder holder = new ObjectCheckedOutVersionsHolder();
    try
    {
      List<ObjectCheckOutVersionDescription> versionDescriptionList = this.LoadDescriptions((object) userSession, versions, false);
      if (versionDescriptionList == null || versionDescriptionList.Count != versions.Count)
      {
        if (throwException)
          throw new Exception(LocalizationHolder.rm.GetString("Kernel_1030"));
        return (ObjectCheckedOutVersionsHolder) null;
      }
      dbTransactions.StartTransaction();
      dbTransactions.StartCreationLog();
      for (int index = 0; index < versionDescriptionList.Count; ++index)
      {
        ObjectCheckOutVersionDescription version = versionDescriptionList[index];
        ObjectCheckOutVersionDescription versionDescription = this.CheckOutVersion(userSession, version, holder);
        if (versionDescription != null)
        {
          holder.Objects.Add(versionDescription);
        }
        else
        {
          if (throwException)
            throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1031"), (object) version.F_OBJECT_ID, (object) version.CAPTION));
          if (dbTransactions.InCreationLogMode)
            dbTransactions.CommitCreationLog();
          dbTransactions.Rollback();
          dbTransactions = (IDBTransactions) null;
          return (ObjectCheckedOutVersionsHolder) null;
        }
      }
      if (dbTransactions.InTransaction)
      {
        if (dbTransactions.InCreationLogMode)
          dbTransactions.CommitCreationLog();
        dbTransactions.Commit();
        dbTransactions = (IDBTransactions) null;
      }
      return holder;
    }
    catch
    {
      if (dbTransactions.InCreationLogMode)
        dbTransactions.CommitCreationLog();
      dbTransactions.Rollback();
      dbTransactions = (IDBTransactions) null;
      if (throwException)
        throw;
    }
    finally
    {
      if (dbTransactions != null && dbTransactions.InTransaction)
      {
        if (dbTransactions.InCreationLogMode)
          dbTransactions.CommitCreationLog();
        dbTransactions.Commit();
      }
    }
    return (ObjectCheckedOutVersionsHolder) null;
  }

  public List<ObjectCheckOutVersionDescription> LoadDescriptions(
    object usrSession,
    IList<long> versions,
    bool throwException)
  {
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_14026.ssp_appserver_14028(1131299092), (object) "ObjectsCheckOutServerService.LoadDescriptions");
    try
    {
      List<object> objectList = ObjectVersionDescriptionsHelper.LoadUnsortedDescriptions((IUserSession) userSession, typeof (ObjectCheckOutVersionDescription), versions, -1);
      List<ObjectCheckOutVersionDescription> versionDescriptionList = new List<ObjectCheckOutVersionDescription>(objectList.Count);
      for (int index = 0; index < objectList.Count; ++index)
      {
        if (objectList[index] is ObjectCheckOutVersionDescription versionDescription)
          versionDescriptionList.Add(versionDescription);
      }
      return versionDescriptionList;
    }
    catch
    {
      if (throwException)
        throw;
    }
    return (List<ObjectCheckOutVersionDescription>) null;
  }

  public void Rollback(
    object usrSession,
    ObjectCheckedOutVersionsHolder rollback,
    bool throwException)
  {
    if (rollback == null)
      return;
    if (!(this.GetUserSession(usrSession) is UserSession userSession))
      throw new KernelExceptionID(sc_14026.ssp_appserver_14029(1634411107), (object) "ObjectsCheckOutServerService.Rollback");
    try
    {
      for (int index = 0; index < rollback.Objects.Count; ++index)
      {
        ObjectCheckOutVersionDescription versionDescription = rollback.Objects[index];
        if (versionDescription.Mode == ObjectCheckedOutVersionMode.CheckOut)
          userSession.GetObject(versionDescription.F_OBJECT_ID, false)?.CancelChanges();
      }
      if (!(userSession.GetCustomService(typeof (IDBTransactions)) is IDBTransactions customService) || !customService.InCreationLogMode)
        return;
      customService.RollBackCreationLog();
    }
    catch
    {
      if (!throwException)
        return;
      throw;
    }
  }
}
