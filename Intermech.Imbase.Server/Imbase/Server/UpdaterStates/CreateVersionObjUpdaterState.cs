// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.UpdaterStates.CreateVersionObjUpdaterState
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Imbase.Server.Synchronization;
using Intermech.Interfaces.Imbase;
using System;

#nullable disable
namespace Intermech.Imbase.Server.UpdaterStates;

internal class CreateVersionObjUpdaterState : IObjUpdaterState
{
  public SynchObjectsStatus Handle(SynchronizationAttributesUpdater context)
  {
    context.Log.AddMessage(MessageType.Normal, "Попытка обновить атрибуты объекта через выпуск версии.");
    SynchObjectsStatus synchObjectsStatus;
    try
    {
      context.State = (IObjUpdaterState) new CantModifyObjUpdaterState();
      synchObjectsStatus = context.Update();
      if (synchObjectsStatus == SynchObjectsStatus.NotSynchronized)
      {
        context.Log.AddMessage(MessageType.Normal, $"Выпуск новой версии объекта {context.Obj.NameInMessages} [{context.Obj.ObjectID}].");
        int objectType = context.Obj.ObjectType;
        long[] versionEx = context.Session.GetObjectCollection(objectType).CreateVersionEx(context.Obj.ObjectID);
        context.Obj = context.Session.GetObject(versionEx[0]);
        context.State = (IObjUpdaterState) new InBaseObjUpdaterState();
        synchObjectsStatus = context.Update();
        context.Log.AddMessage(MessageType.Normal, "Завершение создания версии объекта.");
        context.Obj.CommitCreation(true);
        for (int index = 1; index < versionEx.Length; ++index)
          context.Session.GetObject(versionEx[index], false)?.CommitCreation(true);
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
