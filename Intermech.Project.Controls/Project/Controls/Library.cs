// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.Library
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Plugins;
using Intermech.Metadata;
using Intermech.Navigator.Views;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Project.Controls;

/// <summary>Статический класс-хелпер для инициализации сервисов, кэшей и т.п. библиотеки Intermech.Project.Controls</summary>
public static class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  [NotNull]
  internal static Assembly Assembly => typeof (Library).Assembly;

  /// <summary>Инициализация сервисов, кэшей и т.п. библиотеки Intermech.Project.Controls</summary>
  public static void Init([NotNull] IPackage package, [NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke(ref session, (Action) (() =>
    {
      Intermech.Project.Library.Init(serviceProvider);
      Intermech.Extensions.Client.Library.Init(serviceProvider, session);
      Intermech.Workflow.Design.Holder.Init(package, serviceProvider);
      TaskFilters.Init(session);
      OrganizerPlugin.Init();
      TaskPropProvider provider = new TaskPropProvider();
      Intermech.Client.Services.Factory.AddViewsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task, (IViewsProvider) provider);
      Intermech.Client.Services.Factory.AddViewsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.ProjectMessage, (IViewsProvider) provider);
      Intermech.Client.Services.Factory.AddViewsProvider(1, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, (IViewsProvider) new ProjectPropProvider());
      Task._GlobalNotifier = (INotifier) new Notifier();
    }));
  }
}
