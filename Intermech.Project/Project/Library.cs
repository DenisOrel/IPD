// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Library
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using Intermech.Workflow;
using System;
using System.Reflection;

#nullable disable
namespace Intermech.Project;

/// <summary>Статический класс-хелпер для инициализации сервисов, кэшей и т.п. библиотеки Intermech.Project</summary>
public static class Library
{
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  [NotNull]
  internal static Assembly Assembly => typeof (Library).Assembly;

  /// <summary>Инициализация сервисов, кэшей и т.п. библиотеки Intermech.Project</summary>
  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library._initOnce.Invoke(ref session, (Action) (() =>
    {
      Intermech.Extensions.Interfaces.Library.Init(serviceProvider);
      Intermech.Workflow.Library.Init(serviceProvider);
      MetadataLoader.Init(session);
      wfConsts.Init(session);
      wfConsts.MailObjectTypes.Add((int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectMessage);
      Schedule._Standard = ScheduleList.GetSchedule(Intermech.Metadata.SystemObject.StandardCalendar.ID, session);
      WorkTimeUnits.Init(session);
      if (!Portal.Enabled)
        return;
      SiteID.Init(Portal.SitesCacheService);
    }));
  }
}
