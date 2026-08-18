// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.Synchronization.CheckBindWithImbaseState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.UpdaterStates;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Localization;
using System;

#nullable disable
namespace Intermech.Imbase.Server.Synchronization;

internal class CheckBindWithImbaseState : IObjUpdaterState
{
  public SynchObjectsStatus Handle(SynchronizationAttributesUpdater context)
  {
    IDBObject dbObject = context.Session.GetObject(context.ImbaseObjId, false);
    if (dbObject == null)
    {
      context.Log.AddMessage(MessageType.Normal, "Не удалось получить объект, с которым связан синхронизируемый объект");
      return SynchObjectsStatus.DontLinkedWithIMBASE;
    }
    if ((dbObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableTypeID || dbObject.ObjectType == Intermech.Imbase.Consts.ImbaseTableRefTypeID) && ImbaseServer.GetRecordRow(context.Session, context.ImbaseObjId, context.ImbaseRecId, false) == null)
    {
      context.Log.AddMessage(MessageType.Normal, "Запись, с которой был связан объект, удалена из таблицы");
      return SynchObjectsStatus.DontLinkedWithIMBASE;
    }
    if (context.NewAttributeValues.Count == 0)
    {
      context.Log.AddMessage(MessageType.Extended, "Не нашлось изменившихся атрибутов.");
      return SynchObjectsStatus.NotNeedToModified;
    }
    context.Log.AddMessage(MessageType.Extended, $"{Environment.NewLine}Найдено {context.NewAttributeValues.Count} изменившихся атрибутов.");
    context.State = this.GetState(context);
    return context.Update();
  }

  private IObjUpdaterState GetState(SynchronizationAttributesUpdater context)
  {
    switch (context.Obj.ObjectModifyMode)
    {
      case ObjectModifyModes.InBase:
        context.Log.AddMessage(MessageType.Normal, "Объект можно модифицировать без взятия на редактирование, либо взят на редактирование текущим пользователем.");
        return (IObjUpdaterState) new InBaseObjUpdaterState();
      case ObjectModifyModes.Checkout:
        if (context.Obj.CheckoutBy != context.Session.UserID)
        {
          context.Log.AddMessage(MessageType.Normal, "Объект можно модифицировать через рабочую копию.");
          return (IObjUpdaterState) new CheckoutObjUpdaterState();
        }
        goto case ObjectModifyModes.InBase;
      case ObjectModifyModes.CreateVersion:
        if (context.CreateVersion)
        {
          context.Log.AddMessage(MessageType.Normal, "Объект можно модифицировать через выпуск новой версии объекта.");
          return (IObjUpdaterState) new CreateVersionObjUpdaterState();
        }
        goto case ObjectModifyModes.CantModify;
      case ObjectModifyModes.CantModify:
        context.Log.AddMessage(MessageType.Normal, "Объект на шаге ЖЦ, который запрещает модифицирование объекта.");
        return (IObjUpdaterState) new CantModifyObjUpdaterState();
      default:
        throw new ApplicationException(LocalizationHolder.rm.GetString("Imbase_ObjectModifyModes_Unknown"));
    }
  }
}
