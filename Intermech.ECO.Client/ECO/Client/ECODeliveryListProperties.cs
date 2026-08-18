// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Client.ECODeliveryListProperties
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.ECO;
using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.ECO.Client;

internal class ECODeliveryListProperties
{
  private bool _CanCopyDeliveryList;
  internal bool _inited;

  private void CheckInited()
  {
    if (this._inited)
      return;
    this.LoadCurrentValues();
    this._inited = true;
  }

  private void LoadCurrentValues()
  {
    this._CanCopyDeliveryList = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadBool("ECO", "DELIVERYLIST", "COPY_DELIVERY_LIST_TO_DOC", false, DBConfigMode.GlobalOnly);
  }

  public void ApplyUpdates()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      sessionKeeper.Session.Configurations.WriteBool("ECO", "DELIVERYLIST", "COPY_DELIVERY_LIST_TO_DOC", this._CanCopyDeliveryList, 0L);
      ECOHolder.DeliveryListParametersInit(sessionKeeper.Session);
      if (!(sessionKeeper.Session.GetCustomService(typeof (IECOServer)) is IECOServer customService))
        return;
      customService.SaveDeliveryListParams(sessionKeeper.Session.SessionGUID);
    }
  }

  [TypeConverter(typeof (YesNoBooleanConverter))]
  [CustomDescription("Attribute.ECO.Client_17")]
  [CustomDisplayName("Attribute.ECO.Client_16")]
  public bool CanCopyDeliveryList
  {
    get
    {
      this.CheckInited();
      return this._CanCopyDeliveryList;
    }
    set => this._CanCopyDeliveryList = value;
  }
}
