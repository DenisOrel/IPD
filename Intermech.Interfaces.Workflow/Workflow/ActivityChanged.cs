// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityChanged
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;

#nullable disable
namespace Intermech.Workflow;

[Flags]
public enum ActivityChanged
{
  UnreadStatus = 1,
  Variables = 2,
  Attachments = 4,
  ExtProps = 8,
  SaveVariables = 16, // 0x00000010
  SaveGlobalVariables = 32, // 0x00000020
}
