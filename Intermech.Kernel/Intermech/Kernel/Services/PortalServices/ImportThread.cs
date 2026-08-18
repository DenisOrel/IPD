// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.ImportThread
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.WebPortal;
using Intermech.Portal.Connector;
using System;
using System.Threading;


namespace Intermech.Kernel.Services.PortalServices;

internal abstract class ImportThread
{
  protected IPortalProxy proxy;

  public ImportThread(IPortalProxy proxy) => this.proxy = proxy;

  public void CreateTask(ImportThreadArgs ita)
  {
    ImportTask importTask = this.GetImportTask(ita);
    importTask.Priority = TaskPriority.Normal;
    importTask.Status = TaskStatus.Forming;
    IDBObject dbTask;
    BackupStorage.CreateTask(ita.Session, (ITask) importTask, out dbTask);
    ita.TaskID = importTask.TaskID;
    this.AfterCreateTask(dbTask, ita);
    new Thread(new ParameterizedThreadStart(this.ImportThreadMethod))
    {
      IsBackground = true,
      Name = $"Request_InTo_Portal_For_Import_{Guid.NewGuid()}"
    }.Start((object) ita);
  }

  protected virtual void AfterCreateTask(IDBObject dbTask, ImportThreadArgs ita)
  {
  }

  protected abstract ImportTask GetImportTask(ImportThreadArgs ita);

  protected void ImportThreadMethod(object pars)
  {
    ImportThreadArgs ita = (ImportThreadArgs) pars;
    Guid connectGuid = Guid.Empty;
    string sessionName = $"Portal.ImportThread_{Guid.NewGuid()}";
    IUserSession cloneSession = PortalServicesSessionHelper.GetCloneSession(ita.Session, sessionName, "ImportThread.ImportThreadMethod");
    DBTask dBTask = cloneSession.GetObject(ita.TaskID, true) as DBTask;
    IPortalConnector customService = (IPortalConnector) cloneSession.GetCustomService(typeof (IPortalConnector));
    try
    {
      connectGuid = customService.Login(cloneSession.SessionGUID);
      this.OnImport(connectGuid, dBTask, ita);
      if (ita.StartImmediately)
        ((IPortalTasksQueue) cloneSession.GetCustomService(typeof (IPortalTasksQueue))).StartUpdate(cloneSession.SessionGUID, ita.UpdateGuid.ToString(), (object) ita.TaskID);
      else
        (dBTask.GetAttributeByGuid(PortalConsts.attributeTaskStatus) ?? dBTask.Attributes.AddAttribute(cloneSession.GetAttributeType(PortalConsts.attributeFileError).AttributeID, false)).AsInteger = 4L;
    }
    catch (Exception ex)
    {
      try
      {
        dBTask.SetError(ex);
        (dBTask.GetAttributeByGuid(PortalConsts.attributeTaskStatus) ?? dBTask.Attributes.AddAttribute(cloneSession.GetAttributeType(PortalConsts.attributeFileError).AttributeID, false)).AsInteger = 2L;
      }
      catch
      {
      }
    }
    finally
    {
      if (connectGuid != Guid.Empty)
        customService.Logout(connectGuid);
      PortalServicesSessionHelper.LogoutSession(cloneSession, sessionName, "ImportThread.ImportThreadMethod");
    }
  }

  protected void SetError(long taskID, string message, string stack)
  {
  }

  protected abstract void OnImport(Guid connectGuid, DBTask dBTask, ImportThreadArgs ita);
}
