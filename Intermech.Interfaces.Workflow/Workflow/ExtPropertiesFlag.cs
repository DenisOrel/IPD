// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.ExtPropertiesFlag
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

#nullable disable
namespace Intermech.Workflow;

public enum ExtPropertiesFlag
{
  [MultiFlag] Approve = 65, // 0x00000041
  [MultiFlag] PreExecuted = 69, // 0x00000045
  ThreadID = 73, // 0x00000049
  Messages = 77, // 0x0000004D
  Portal = 80, // 0x00000050
  [MultiFlag] RemoteSubprocess = 82, // 0x00000052
  [MultiFlag] SubProcess = 83, // 0x00000053
  Timer = 84, // 0x00000054
}
