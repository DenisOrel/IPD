// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityStatus
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Interfaces.Workflow;
using System.ComponentModel;

#nullable disable
namespace Intermech.Workflow;

public enum ActivityStatus
{
  [CustomDescription("Attribute.Workflow.Design_40")] OnApproach,
  [CustomDescription("Attribute.Workflow.Design_41")] CollectorWaiting,
  [CustomDescription("Attribute.Workflow.Design_42")] DefineWaiting,
  [CustomDescription("Attribute.Workflow.Design_43")] ParticipantWaiting,
  [CustomDescription("Attribute.Workflow.Design_44")] Executed,
  [CustomDescription("Attribute.Workflow.Design_45")] Terminated,
  [CustomDescription("Attribute.Workflow.Design_46")] Completed,
  [CustomDescription("Attribute.Workflow.Design_47")] AutoCompleted,
  [CustomDescription("ActStatusRecalled")] Recalled,
  [Description("Выполняется серверный сценарий")] ScriptExecuted,
  [Description("Выполняется смена шагов ЖЦ")] LCStepExecuted,
}
