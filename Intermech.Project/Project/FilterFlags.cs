// Decompiled with JetBrains decompiler
// Type: Intermech.Project.FilterFlags
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using System;

#nullable disable
namespace Intermech.Project;

[Flags]
public enum FilterFlags
{
  None = 0,
  ShowInMenu = 1,
  Global = 2,
  ShowSummaryTasks = 4,
}
