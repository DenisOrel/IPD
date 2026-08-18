// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.TaskStatusChangedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public class TaskStatusChangedEventArgs : TaskEventArgs
{
  public TaskStatus NewStatus;
  public double Percent;
  public bool PercentChanged;

  public TaskStatusChangedEventArgs(IUserSession session, TaskStatus newStatus)
    : this(session, newStatus, 0.0, false)
  {
    this.NewStatus = newStatus;
  }

  public TaskStatusChangedEventArgs(
    IUserSession session,
    TaskStatus newStatus,
    double percent,
    bool percentChanged = true)
    : base(session)
  {
    this.NewStatus = newStatus;
    this.Percent = percent;
    this.PercentChanged = percentChanged;
  }
}
