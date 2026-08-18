// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.AutoNotification.AutoNotificationViewProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Interfaces.Workflow;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Workflow.Client.AutoNotification;

internal class AutoNotificationViewProvider : IViewsProvider
{
  private static bool _registeredView;

  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!AutoNotificationViewProvider._registeredView)
    {
      AdjustableViewsHelper.RegisterView("AutoNotificationSettingsView", LocalizationHolder.rm.GetString("Workflow.Client_103"), "", "", "", true, 0);
      AutoNotificationViewProvider._registeredView = true;
    }
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("AutoNotificationSettingsView", new ViewInfo(0, typeof (AutoNotificationSettingsView)));
    return views;
  }
}
