// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.ViewsBuilder
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using ImSSP;
using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class ViewsBuilder : CustomBackgroundTask
{
  private DataTable typesTable;
  private ArrayList _Log = new ArrayList();

  public ViewsBuilder()
  {
    this._name = LocalizationHolder.rm.GetString("DatabaseConfigurator_139");
    this._canStop = true;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.typesTable = sessionKeeper.Session.GetObjectTypeCollection(-2).Select("");
    this._maxValue = this.typesTable.Rows.Count + 1;
  }

  public void Rebuild()
  {
    try
    {
      this._state = BackgroundTaskState.Running;
      this.OnChanged(BackgroundTaskChangedType.State);
      try
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IUserSession session = sessionKeeper.Session;
          if (!(session.GetCustomService(typeof (IAdminUtilsService)) is IAdminUtilsService customService1))
            throw new Exception(LocalizationHolder.rm.GetString(sc_5896.ssp_imclient_5897()));
          customService1.RebuildObjectsView(session.SessionGUID);
          IDatabaseLocker customService2 = session.GetCustomService(typeof (IDatabaseLocker)) as IDatabaseLocker;
          DatabaseLockInfo databaseLockInfo = customService2.Lock(session, "RebuildViews", TimeSpan.FromDays(2.0));
          if (databaseLockInfo.Success)
          {
            try
            {
              ++this._value;
              this.OnChanged(BackgroundTaskChangedType.Value);
              foreach (DataRow row in (InternalDataCollectionBase) this.typesTable.Rows)
              {
                if (this._state != BackgroundTaskState.Stopped)
                {
                  IDBObjectType objectType = session.GetObjectType(Convert.ToInt32(row["F_OBJECT_TYPE"]));
                  this._name = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_141"), (object) objectType.ObjectTypeName);
                  this.OnChanged(BackgroundTaskChangedType.Text);
                  try
                  {
                    objectType.RebuildView();
                  }
                  catch (Exception ex)
                  {
                    this._Log.Add((object) string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_142"), (object) objectType.ObjectTypeName, (object) ex.Message));
                  }
                  ++this._value;
                  this.OnChanged(BackgroundTaskChangedType.Value);
                }
                else
                  break;
              }
              this.typesTable = session.GetRelationTypeCollection().Select("");
              this._maxValue = this.typesTable.Rows.Count;
              this._value = 0;
              this.OnChanged(BackgroundTaskChangedType.All);
              foreach (DataRow row in (InternalDataCollectionBase) this.typesTable.Rows)
              {
                if (this._state != BackgroundTaskState.Stopped)
                {
                  IDBRelationType relationType = session.GetRelationType(Convert.ToInt32(row["F_RELATION_TYPE"]));
                  this._name = string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_204"), (object) relationType.Description);
                  this.OnChanged(BackgroundTaskChangedType.Text);
                  try
                  {
                    relationType.RebuildView();
                  }
                  catch (Exception ex)
                  {
                    this._Log.Add((object) string.Format(LocalizationHolder.rm.GetString("DatabaseConfigurator_143"), (object) relationType.Description, (object) ex.Message));
                  }
                  ++this._value;
                  this.OnChanged(BackgroundTaskChangedType.Value);
                }
                else
                  break;
              }
            }
            finally
            {
              customService2.UnLock(session, "RebuildViews");
            }
          }
          else
            this._Log.Add((object) databaseLockInfo.GetErrorMessage("Перегенерация представлений данных"));
          if (this._Log.Count <= 0)
            return;
          int num = (int) MessageBox.Show(LocalizationHolder.rm.GetString("DatabaseConfigurator_144"), LocalizationHolder.rm.GetString("DatabaseConfigurator_145"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          IOutputView service = ServicesManager.GetService(typeof (IOutputView)) as IOutputView;
          string category = LocalizationHolder.rm.GetString("DatabaseConfigurator_146");
          foreach (string text in this._Log)
            service.WriteString(category, text);
          service.Activate(category);
          service.ShowView();
        }
      }
      finally
      {
        this.OnChanged(BackgroundTaskChangedType.Dispose);
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }
}
