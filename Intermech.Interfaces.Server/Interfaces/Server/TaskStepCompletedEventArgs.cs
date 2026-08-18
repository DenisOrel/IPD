// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.TaskStepCompletedEventArgs
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using Intermech.Interfaces.WebPortal;

#nullable disable
namespace Intermech.Interfaces.Server;

public class TaskStepCompletedEventArgs : TaskEventArgs
{
  public double Percent;
  public bool PercentChanged;
  public ITransferedObject Unit;
  public int UnitIndex;

  public TaskStepCompletedEventArgs(
    IUserSession session,
    double percent,
    bool percentChanged,
    ITransferedObject unit,
    int unitIndex)
    : base(session)
  {
    this.PercentChanged = percentChanged;
    this.Percent = percent;
    this.Unit = unit;
    this.UnitIndex = unitIndex;
  }
}
