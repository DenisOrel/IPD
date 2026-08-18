// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.ResolutionAccessService
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

internal sealed class ResolutionAccessService : LongLifeObject, IResolutionAccessService
{
  public void ReturnResolution(long resolutionID)
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.ReturnResolution"))
    {
      IDBObject dbObject = systemSessionKeeper.Session.GetObject(resolutionID);
      IDBObjectType objectType = systemSessionKeeper.Session.GetObjectType(dbObject.ObjectType, true);
      IDBLCSchema lcSchema = systemSessionKeeper.Session.GetLCSchema(objectType.SchemaID);
      dbObject.LCStep = lcSchema.GetStepsCollection().GetFirstStep();
    }
  }

  public bool SetAccess(long resolutionID)
  {
    using (SystemSessionKeeper systemSessionKeeper = new SystemSessionKeeper("OfficeServer.SetAccess"))
    {
      IDBTransactions customService = (IDBTransactions) systemSessionKeeper.Session.GetCustomService(typeof (IDBTransactions));
      customService.StartTransaction();
      try
      {
        DBResolution resolution = systemSessionKeeper.Session.GetResolution(resolutionID);
        DataTable accessList = resolution.GetAccessList(out ActionProperties[] _, out QuickObjectInfo[] _);
        DataTable dataTable = accessList.Clone();
        for (int index = 0; index < accessList.Rows.Count; ++index)
        {
          if (Convert.ToInt64(accessList.Rows[index]["F_PARENT_KEY"]) != -1L)
          {
            accessList.Rows[index]["F_RIGHT_TYPE"] = (object) Consts.DeleteRecord;
            DataSetProcessor.AddRow(dataTable, accessList.Rows[index], false);
          }
        }
        dataTable.AcceptChanges();
        ResolutionAccessService.AddAccessRow(resolutionID, dataTable, OfficeConsts.ObjectCreatorUserGroupID, systemSessionKeeper.Session.UserID, 2, new int[9]
        {
          8,
          2,
          4,
          25,
          26,
          18,
          19,
          31 /*0x1F*/,
          45
        });
        if (resolution.IsControlResolution)
        {
          long controllerId = resolution.ControllerID;
          if (controllerId != 0L)
          {
            ResolutionAccessService.AddAccessRow(resolutionID, dataTable, controllerId, systemSessionKeeper.Session.UserID, 2, new int[2]
            {
              2,
              8
            });
            ResolutionAccessService.AddAccessRow(resolutionID, dataTable, controllerId, systemSessionKeeper.Session.UserID, 1, new int[7]
            {
              4,
              25,
              26,
              18,
              19,
              31 /*0x1F*/,
              45
            });
          }
        }
        long authorId = resolution.AuthorID;
        if (authorId != 0L)
          ResolutionAccessService.AddAccessRow(resolutionID, dataTable, authorId, systemSessionKeeper.Session.UserID, 2, new int[9]
          {
            8,
            2,
            4,
            25,
            26,
            18,
            19,
            31 /*0x1F*/,
            45
          });
        foreach (long executorId in (IEnumerable<long>) resolution.ExecutorIDs)
        {
          ResolutionAccessService.AddAccessRow(resolutionID, dataTable, executorId, systemSessionKeeper.Session.UserID, 2, new int[2]
          {
            2,
            8
          });
          ResolutionAccessService.AddAccessRow(resolutionID, dataTable, executorId, systemSessionKeeper.Session.UserID, 1, new int[7]
          {
            4,
            25,
            26,
            18,
            19,
            31 /*0x1F*/,
            45
          });
        }
        ResolutionAccessService.SetAccess4AllUsers(systemSessionKeeper.Session, resolutionID, dataTable, systemSessionKeeper.Session.UserID, resolution.ObjectType == OfficeConsts.ObjtypeConfidentialResolutionsID || MetaDataHelper.IsObjectTypeChildOf(resolution.ObjectType, OfficeConsts.ObjtypeConfidentialResolutionsID));
        resolution.SetAccess(dataTable);
        customService.Commit();
        return true;
      }
      catch
      {
        customService.Rollback();
        throw;
      }
    }
  }

  private static void AddAccessRow(
    [NotEmpty] long resolutionID,
    [NotNull] DataTable accessTable,
    long userID,
    long ownerID,
    int rightType,
    [NotNull] int[] rightIDs)
  {
    foreach (int rightId in rightIDs)
    {
      DataRow row = accessTable.NewRow();
      row["F_CATEGORY_TYPE"] = (object) 1;
      row["F_CATEGORY_ID"] = (object) resolutionID;
      row["F_USER_ID"] = (object) userID;
      row["F_OWNER_ID"] = (object) ownerID;
      row["F_PARENT_KEY"] = (object) 0;
      row["F_KEY"] = (object) 0;
      row["F_RIGHT_TYPE"] = (object) rightType;
      row["F_RIGHT_ID"] = (object) rightId;
      accessTable.Rows.Add(row);
    }
  }

  private static void SetAccess4AllUsers(
    [NotNull] IUserSession session,
    [NotEmpty] long resolutionID,
    [NotNull] DataTable accessTable,
    long ownerID,
    bool confidential)
  {
    if (!confidential)
    {
      ResolutionAccessService.AddAccessRow(resolutionID, accessTable, OfficeConsts.ObjectAllUsersUserGroupID, ownerID, 2, new int[1]
      {
        8
      });
      ResolutionAccessService.AddAccessRow(resolutionID, accessTable, OfficeConsts.ObjectAllUsersUserGroupID, ownerID, 1, new int[8]
      {
        2,
        4,
        25,
        26,
        18,
        19,
        31 /*0x1F*/,
        45
      });
    }
    else
      ResolutionAccessService.AddAccessRow(resolutionID, accessTable, OfficeConsts.ObjectAllUsersUserGroupID, ownerID, 1, new int[10]
      {
        8,
        2,
        4,
        25,
        6,
        26,
        18,
        19,
        31 /*0x1F*/,
        45
      });
  }
}
