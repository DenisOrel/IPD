// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.DeletionStatus
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;

#nullable disable
namespace Intermech.Workflow;

public enum DeletionStatus
{
  [CustomDescription("")] Normal,
  [CustomDescription("DeletionStatus.Deleted")] Deleted,
  [CustomDescription("DeletionStatus.Deleted")] CompletelyDeleted,
}
