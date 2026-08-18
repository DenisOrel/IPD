// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.GetTaskByTypeEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public class GetTaskByTypeEventArgs
{
  public TaskType Type;
  public ITask Task;
  public bool Handled;
  public IDBObject TaskObject;

  public GetTaskByTypeEventArgs(TaskType type, IDBObject taskObject)
  {
    this.Type = type;
    this.TaskObject = taskObject;
  }
}
