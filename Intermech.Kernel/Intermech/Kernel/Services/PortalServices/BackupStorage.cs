// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.BackupStorage
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Localization;
using System;


namespace Intermech.Kernel.Services.PortalServices;

public class BackupStorage
{
  private readonly object _syncRoot = new object();

  public static void CreateTask(IUserSession session, ITask newTask, out IDBObject dbTask)
  {
    try
    {
      IDBObjectCollection objectCollection = session.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545"));
      dbTask = objectCollection.Create();
      (dbTask as DBTask).SaveTaskToBase(newTask, Helper.CalculateTaskNo(objectCollection, newTask));
      dbTask.CommitCreation(true);
      newTask.TaskID = dbTask.ObjectID;
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1048"), (object) newTask.TaskID, (object) Helper.FormingLogError(ex)));
    }
  }

  public void UpdateTask(IUserSession session, IDBObject dbTask, ITask task)
  {
    try
    {
      lock (this._syncRoot)
        (dbTask as DBTask).SaveTaskToBase(task, Helper.CalculateTaskNo(session.GetObjectCollection(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545")), task));
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1049"), (object) task.TaskID, (object) Helper.FormingLogError(ex)));
    }
  }

  public void RemoveTask(IUserSession session, long taskID)
  {
    try
    {
      lock (this._syncRoot)
        session.GetObject(taskID).Delete((long) PortalConsts.DeleteWithoutFiles);
    }
    catch (Exception ex)
    {
      throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1050"), (object) taskID, (object) Helper.FormingLogError(ex)));
    }
  }
}
