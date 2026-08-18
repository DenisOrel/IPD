// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.InvalidAttributes
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class InvalidAttributes : CustomBackgroundTask
{
  private List<InvalidAttributesClass> listOfInvalidAttributes;

  public InvalidAttributes()
  {
    this._name = LocalizationHolder.rm.GetString("DatabaseConfigurator_217");
    this._canStop = false;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
  }

  public void FindInvaildAttributes()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    try
    {
      this.listOfInvalidAttributes = new List<InvalidAttributesClass>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
        try
        {
          Guid sessionGuid = sessionKeeper.Session.SessionGUID;
          MetaDataHelper.SyncAttrTypesMetadata((sessionKeeper.Session as IUserSessionCacheDataSet).CacheDataSet);
          List<IMSObjectType> objectTypesList = MetaDataHelper.GetObjectTypesList();
          this._maxValue = objectTypesList.Count;
          foreach (IMSObjectType imsObjectType in objectTypesList)
          {
            if (imsObjectType.VersionsMode != ObjectVersionModes.Abstract && !imsObjectType.AnyAttributes)
            {
              DataTable tbl;
              int objectAttributes = customService.FindInvalidObjectAttributes(imsObjectType.ObjectTypeID, sessionGuid, out tbl);
              this.listOfInvalidAttributes.Add(new InvalidAttributesClass(imsObjectType.ObjectTypeID, objectAttributes, tbl));
            }
            ++this._value;
            this.OnChanged(BackgroundTaskChangedType.Value);
          }
          this.State = BackgroundTaskState.Terminated;
          Thread.Sleep(500);
        }
        finally
        {
          this.OnChanged(BackgroundTaskChangedType.Dispose);
        }
        using (InvalidAttributesForm invalidAttributesForm = new InvalidAttributesForm())
        {
          invalidAttributesForm.ListOfInvalidAttributes = this.listOfInvalidAttributes;
          int num = (int) invalidAttributesForm.ShowDialog();
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }
}
