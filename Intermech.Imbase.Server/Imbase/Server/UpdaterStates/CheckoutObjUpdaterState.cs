// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.UpdaterStates.CheckoutObjUpdaterState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Synchronization;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Imbase.Server.UpdaterStates;

internal class CheckoutObjUpdaterState : IObjUpdaterState
{
  public SynchObjectsStatus Handle(SynchronizationAttributesUpdater context)
  {
    if (context.Obj.CheckoutBy != 0L && context.Obj.CheckoutBy != context.Session.UserID)
      throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectCheckOutAnotherUser_Error"));
    context.Log.AddMessage(MessageType.Normal, "Попытка обновить атрибуты объекта через рабочую копию объекта.");
    SynchObjectsStatus synchObjectsStatus;
    try
    {
      context.State = (IObjUpdaterState) new CantModifyObjUpdaterState();
      synchObjectsStatus = context.Update();
      if (synchObjectsStatus == SynchObjectsStatus.NotSynchronized)
      {
        context.Log.AddMessage(MessageType.Normal, "Взятие объекта на редактирование.");
        context.Obj = context.Obj.CheckOut(false);
        context.State = (IObjUpdaterState) new InBaseObjUpdaterState();
        synchObjectsStatus = context.Update();
        context.Log.AddMessage(MessageType.Normal, "Сохранение изменений и возврат в базу.");
        context.Obj.CheckIn();
      }
    }
    catch (Exception ex)
    {
      context.Log.AddMessage(MessageType.Normal, ex.Message);
      synchObjectsStatus = SynchObjectsStatus.NotSynchronized;
    }
    return synchObjectsStatus;
  }
}
