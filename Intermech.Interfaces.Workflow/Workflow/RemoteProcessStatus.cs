// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.RemoteProcessStatus
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Workflow;

[DBEnum("cadd94c6-306c-11d8-b4e9-00304f19f545")]
public enum RemoteProcessStatus
{
  None,
  WaitingForPublish,
  PublishError,
  Published,
  RemoteExecError,
  InProgress,
  ImportError,
  Completed,
}
