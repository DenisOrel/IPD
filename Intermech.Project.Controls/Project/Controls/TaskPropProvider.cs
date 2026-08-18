// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.TaskPropProvider
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Project.Controls;

public class TaskPropProvider : IViewsProvider
{
  [NotNull]
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("UserTaskView", new ViewInfo(0, 0, typeof (UserTaskView)));
    views.Add("MailAttachments", new ViewInfo(0));
    views.Add("TaskResultsView", new ViewInfo(100, 0, typeof (TaskResultsView)));
    views.Add("TaskDataView", new ViewInfo(100, 0, typeof (TaskDataView)));
    return views;
  }
}
