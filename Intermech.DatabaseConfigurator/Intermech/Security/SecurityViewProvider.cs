// Decompiled with JetBrains decompiler
// Type: Intermech.Security.SecurityViewProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces.Client;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Security;

public class SecurityViewProvider : IViewsProvider
{
  private int userTypeId = -1;

  public SecurityViewProvider()
  {
    this.userTypeId = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).UsersTypeID;
  }

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    if (items.GetItemID(0).TypeID == this.userTypeId)
      views.Add("UserEvents", new ViewInfo(2, 1611, typeof (UserEventsView)));
    return views;
  }
}
