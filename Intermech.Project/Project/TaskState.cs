// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskState
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using System;

#nullable disable
namespace Intermech.Project;

[Flags]
public enum TaskState
{
  Loading = 1,
  Saving = 2,
  SettingConstraint = 4,
  Starting = 8,
  MailRefreshNeeded = 16, // 0x00000010
  Copying = 32, // 0x00000020
  LoadingSubtasks = 64, // 0x00000040
  IndexChanging = 128, // 0x00000080
  SubtaskIndexChanging = 256, // 0x00000100
  GraphCalculating = 512, // 0x00000200
  SkippedByScript = 1024, // 0x00000400
  ChildrenModified = 2048, // 0x00000800
}
