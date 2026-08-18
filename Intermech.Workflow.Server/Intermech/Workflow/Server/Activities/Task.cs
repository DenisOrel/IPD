// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.Activities.Task
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server.Activities;

public class Task(UserSession uSession, DataTable objectsTable) : UserActivity(uSession, objectsTable)
{
  public override ActivityKind Kind => ActivityKind.Task;
}
