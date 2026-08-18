// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Strings
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Project.Properties;

#nullable disable
namespace Intermech.Project;

/// <summary>Строки в ресурсах проекта Intermech.Project</summary>
public static class Strings
{
  /// <summary>Наименование фильтра для фильтрации всех задач ("Все задачи")</summary>
  [NotNull]
  public static readonly string AllTasksFilterName = Intermech.Diagnostics.Check.NotNullNotEmpty<string>(Resources.FilterAllTasks);
}
