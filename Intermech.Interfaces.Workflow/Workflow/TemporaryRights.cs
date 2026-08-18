// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.TemporaryRights
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[Flags]
public enum TemporaryRights
{
  None = 0,
  View = 1,
  Edit = 2,
  Admin = 4,
  HandleGrouped = 128, // 0x00000080
}
