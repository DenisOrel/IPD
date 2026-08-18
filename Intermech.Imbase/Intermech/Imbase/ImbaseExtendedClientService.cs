// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ImbaseExtendedClientService
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Imbase;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Imbase;

internal sealed class ImbaseExtendedClientService : IImbaseExtendedService
{
  private ImbaseExtendedData _cacheData = new ImbaseExtendedData();

  public ImbaseExtendedClientService()
  {
    this.LoadConfigData(Guid.Empty);
    this.SubscribeEvents();
  }

  public Dictionary<int, ImbaseExtendedItem> GetValues(int objTypeID)
  {
    ImbaseExtendedObjectTypeInfo extendedObjectTypeInfo;
    this._cacheData.ObjectTypeData.TryGetValue(objTypeID, out extendedObjectTypeInfo);
    return extendedObjectTypeInfo?.AttributeData as Dictionary<int, ImbaseExtendedItem>;
  }

  public bool LoadConfigData(Guid sessionGuid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IImbaseExtendedService service = ServiceUtils.GetService<IImbaseExtendedService>((object) sessionKeeper.Session, false);
      if (service == null)
        return false;
      this._cacheData = service.GetAllValues() ?? new ImbaseExtendedData();
    }
    return true;
  }

  public bool SaveConfigData(Guid sessionGuid)
  {
    throw new NotImplementedException("Must use server service!");
  }

  public void SetValues(Guid sessionGuid, int objTypeID, IDictionary<int, ImbaseExtendedItem> dict)
  {
    throw new NotImplementedException("Must use server service!");
  }

  public ImbaseExtendedData GetAllValues()
  {
    throw new NotImplementedException("Must use server service!");
  }

  private void SubscribeEvents()
  {
    if (!(ServicesManager.GetService(typeof (INotificationService)) is INotificationService service))
      return;
    service.Subscribe("MetadataCacheReloaded", new NotificationEventHandler(this.OnMetadataChanged));
  }

  private void OnMetadataChanged(object sender, NotificationEventArgs e)
  {
    this.LoadConfigData(Guid.Empty);
  }
}
