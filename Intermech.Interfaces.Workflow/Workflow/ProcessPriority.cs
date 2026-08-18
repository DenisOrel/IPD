// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ProcessPriority
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow;

public enum ProcessPriority
{
  [CustomDescription("Attribute.Workflow.Design_50")] Low = -1, // 0xFFFFFFFF
  [CustomDescription("Attribute.Workflow.Design_51")] Normal = 0,
  [CustomDescription("Attribute.Workflow.Design_52")] High = 1,
  Unreal = 2,
}
