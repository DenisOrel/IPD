// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ActivityKind
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Workflow;

public enum ActivityKind
{
  Start,
  [Order(1)] Task,
  [Order(2)] Approve,
  [Order(3)] Automated,
  [Order(7)] SubProcess,
  [Order(4)] Condition,
  [Order(5)] Case,
  [Order(99)] Stop,
  [Order(98)] Abort,
  [Order(11)] Timer,
  [Order(10)] Register,
  [Order(9)] Script,
  [Order(8)] Result,
  [Order(14)] RemoteSubProcess,
  Process,
  [Order(13)] LCStep,
  None,
}
