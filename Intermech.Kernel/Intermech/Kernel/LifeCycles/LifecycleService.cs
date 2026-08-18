// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.LifeCycles.LifecycleService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.LifeCycles;
using Intermech.Interfaces.Server;
using System;
using System.Data;
using System.Text;


namespace Intermech.Kernel.LifeCycles;

public class LifecycleService : LongLifeObject, ILifecycleService
{
  public string ValidateChangeLCStep(long[] objectIDs, NewLCStepInfo[] stepInfos)
  {
    StringBuilder stringBuilder = new StringBuilder();
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone("lcService.ValidateChangeLCStep");
    try
    {
      foreach (int objectId in objectIDs)
      {
        IDBObject dbObject = sessionTemporaryClone.GetObject((long) objectId, false);
        if (dbObject != null)
        {
          int objectType = dbObject.ObjectType;
          NewLCStepInfo newLcStepInfo = (NewLCStepInfo) null;
          int num1 = 1000;
          foreach (NewLCStepInfo stepInfo in stepInfos)
          {
            if (stepInfo.ObjectTypeID == objectType)
            {
              newLcStepInfo = stepInfo;
              break;
            }
            int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectType);
            int num2 = 1;
            for (; stepInfo.ObjectTypeID != objectTypeParentId && objectTypeParentId != -1; objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(objectTypeParentId))
              ++num2;
            if (objectTypeParentId != -1 && num2 < num1)
            {
              newLcStepInfo = stepInfo;
              num1 = num2;
            }
          }
          if (newLcStepInfo != null)
          {
            if (newLcStepInfo.LCStepID > 0)
            {
              if (dbObject.CheckoutBy != 0L && newLcStepInfo.LCStepID != dbObject.LCStep)
              {
                stringBuilder.AppendFormat("Объект '{0}' взят на изменение пользователем {1}, поэтому он не может быть перемещен на другой шаг ЖЦ.", (object) dbObject.NameInMessages, (object) sessionTemporaryClone.GetObjectInfo(dbObject.CheckoutBy).Caption);
                stringBuilder.AppendLine();
              }
              else
              {
                string errorMessage;
                if (!dbObject.CanSetNextLCStep(newLcStepInfo.LCStepID, out errorMessage))
                {
                  stringBuilder.Append(errorMessage);
                  stringBuilder.AppendLine();
                }
              }
            }
            else
            {
              IDBLifecycleStep lcStepObject = (dbObject as DBObject).LCStepObject;
              if (lcStepObject.LevelID != newLcStepInfo.LevelID)
              {
                int nextStep = lcStepObject.GetNextStep(newLcStepInfo.LevelID);
                if (nextStep != -1)
                {
                  if (dbObject.CheckoutBy != 0L && nextStep != dbObject.LCStep)
                  {
                    stringBuilder.AppendFormat("Объект '{0}' взят на изменение пользователем {1}, поэтому он не может быть перемещен на другой шаг ЖЦ.", (object) dbObject.NameInMessages, (object) sessionTemporaryClone.GetObjectInfo(dbObject.CheckoutBy).Caption);
                    stringBuilder.AppendLine();
                  }
                  else
                  {
                    string errorMessage;
                    if (!dbObject.CanSetNextLCStep(nextStep, out errorMessage))
                    {
                      stringBuilder.Append(errorMessage);
                      stringBuilder.AppendLine();
                    }
                  }
                }
                else
                {
                  stringBuilder.AppendFormat("Ошибка перевода объекта '{0}' на шаг ЖЦ с уровнем продвижения '{1}': схема жизненного цикла не допускает такой перевод с шага '{2}'", (object) dbObject.NameInMessages, (object) MetaDataHelper.GetLCLevelName(newLcStepInfo.LevelID), (object) lcStepObject.LCName);
                  stringBuilder.AppendLine();
                }
              }
            }
          }
        }
        else
          stringBuilder.AppendFormat("Объект N{0} не найден.", (object) objectId);
      }
    }
    finally
    {
      sessionTemporaryClone.Logout("lcService.ValidateChangeLCStep");
    }
    return stringBuilder.ToString();
  }

  public bool CanCreateObjectVersion(
    IUserSession session,
    long id,
    long modificationID,
    int stepID,
    out string errorMsg)
  {
    errorMsg = string.Empty;
    bool objectVersion = true;
    IDbManager dataManager = (session as UserSession).DataManager;
    if (!dataManager.InTransaction)
      throw new KernelException("Функция CanCreateObjectVersion должна вызываться только в транзакции!");
    IDBLifecycleStep lifecycleStep1 = session.GetLifecycleStep(stepID);
    bool flag1 = (lifecycleStep1.Options & LCStepOptions.DisableParallelVersions) == LCStepOptions.DisableParallelVersions;
    bool flag2 = (lifecycleStep1.Options & LCStepOptions.DisableContextParallelVersions) == LCStepOptions.DisableContextParallelVersions;
    int autoTransferStepId = lifecycleStep1.AutoTransferStepID;
    if (autoTransferStepId == 0)
    {
      if (modificationID == 0L && flag1)
      {
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id217 AND F_LC_STEP = :lcStep AND F_LEVEL_ID <> :delLevel AND F_OBJECT_VER_TYPE <> :blankID AND F_MODIFICATION_ID = 0", dataManager.Parameter("id217", (object) id), dataManager.Parameter("lcStep", (object) stepID), dataManager.Parameter("delLevel", (object) session.IdentHelper.DeletedID), dataManager.Parameter("blankID", (object) -1));
        if (obj != null && obj != DBNull.Value)
        {
          IDBObject dbObject = session.GetObject(Convert.ToInt64(obj), false);
          string str = dbObject == null ? dbObject.ToString() : dbObject.NameInMessages;
          errorMsg = $"На шаге '{lifecycleStep1.LCName}' нельзя создавать новую версию объекта '{str}', т.к. схема '{session.GetLCSchema(lifecycleStep1.SchemaID).Name}' не допускает наличия более 1 версии объекта на данном шаге.";
          objectVersion = false;
        }
      }
      if (flag2)
      {
        object obj = dataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id317 AND F_LC_STEP = :lcStep AND F_LEVEL_ID <> :delLevel AND F_OBJECT_VER_TYPE <> :blankID AND F_MODIFICATION_ID <> 0", dataManager.Parameter("id317", (object) id), dataManager.Parameter("lcStep", (object) stepID), dataManager.Parameter("delLevel", (object) session.IdentHelper.DeletedID), dataManager.Parameter("blankID", (object) -1));
        if (obj != null && obj != DBNull.Value)
        {
          IDBObject dbObject = session.GetObject(Convert.ToInt64(obj), false);
          string str = dbObject == null ? dbObject.ToString() : dbObject.NameInMessages;
          errorMsg = $"На шаге '{lifecycleStep1.LCName}' нельзя создавать новую версию объекта '{str}', т.к. схема '{session.GetLCSchema(lifecycleStep1.SchemaID).Name}' не допускает наличия более 1 версии объекта на данном шаге.";
          objectVersion = false;
        }
      }
    }
    else
    {
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT F_OBJECT_ID FROM IMS_OBJECTS WHERE F_ID = :id417 AND F_LC_STEP = :lcStep", dataManager.Parameter("id417", (object) id), dataManager.Parameter("lcStep", (object) stepID));
      IDBLifecycleStep lifecycleStep2 = session.GetLifecycleStep(autoTransferStepId);
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        IDBObject dbObject = session.GetObject(Convert.ToInt64(row[0]), false);
        if (dbObject != null)
          (dbObject as DBObject).SetLCStep(lifecycleStep2);
      }
    }
    return objectVersion;
  }
}
