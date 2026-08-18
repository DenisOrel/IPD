// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.EmailViewProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using Intermech.Workflow.Client.Email;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

internal class EmailViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("Workflow.EmailInboxView", new ViewInfo(0, -1, typeof (EmailInboxView)));
    return views;
  }
}
