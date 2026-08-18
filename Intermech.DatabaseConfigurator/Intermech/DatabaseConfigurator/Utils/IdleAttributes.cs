// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.Utils.IdleAttributes
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.DatabaseConfigurator.Utils;

public class IdleAttributes : CustomBackgroundTask
{
  private List<InvalidAttributesClass> listOfIdleAttributes;

  public IdleAttributes()
  {
    this._name = "Поиск неиспользуемых атрибутов";
    this._canStop = false;
    this._canResume = false;
    this._canPause = false;
    this._minValue = 0;
    this._value = 0;
  }

  public void FindIdleAttributes()
  {
    this._state = BackgroundTaskState.Running;
    this.OnChanged(BackgroundTaskChangedType.State);
    this._minValue = 0;
    this._maxValue = 100;
    this._value = 0;
    try
    {
      this.listOfIdleAttributes = new List<InvalidAttributesClass>();
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        DataTable dataTable = (DataTable) null;
        IAdminUtilsService customService = sessionKeeper.Session.GetCustomService(typeof (IAdminUtilsService)) as IAdminUtilsService;
        try
        {
          this._value = 50;
          this.OnChanged(BackgroundTaskChangedType.Value);
          dataTable = customService.GetIdleAttributes(sessionKeeper.Session.SessionGUID);
          this._value = 100;
          this.OnChanged(BackgroundTaskChangedType.Value);
          this.State = BackgroundTaskState.Terminated;
          Thread.Sleep(500);
        }
        finally
        {
          this.OnChanged(BackgroundTaskChangedType.Dispose);
        }
      }
    }
    catch (Exception ex)
    {
      ExceptionHelper.ExceptionService.ShowException(ex);
    }
  }
}
