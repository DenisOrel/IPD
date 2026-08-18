// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectProperties
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class ProjectProperties
{
  [CanBeNull]
  public readonly Intermech.Project.Project Project;
  private bool _requireTaskVerification = true;
  private bool _enableMailNotifications = true;
  private TaskStartingMode _taskStartingMode;
  private bool _completeTasksOnProcess;
  internal readonly bool AllowStartTasksWithNoResources = true;
  private const string DsSection = "Props";

  public ProjectProperties([CanBeNull] Intermech.Project.Project project) => this.Project = project;

  private void SetModified(bool value)
  {
    this.Modified = value;
    if (!value || this.Project == null)
      return;
    this.Project.Modified = true;
  }

  public bool RequireTaskVerification
  {
    get => this._requireTaskVerification;
    set
    {
      if (this._requireTaskVerification == value)
        return;
      this._requireTaskVerification = value;
      this.SetModified(true);
    }
  }

  public bool EnableMailNotifications
  {
    get => this._enableMailNotifications;
    set
    {
      if (this._enableMailNotifications == value)
        return;
      this._enableMailNotifications = value;
      this.SetModified(true);
    }
  }

  public bool Modified { get; private set; }

  public TaskStartingMode TaskStartingMode
  {
    get => this._taskStartingMode;
    set
    {
      if (this._taskStartingMode == value)
        return;
      this._taskStartingMode = value;
      this.SetModified(true);
    }
  }

  /// <summary>
  /// Настройка "Автоматически завершать задачи при завершении процессов согласования результатов"
  /// </summary>
  public bool CompleteTasksOnProcess
  {
    get => this._completeTasksOnProcess;
    set
    {
      if (this._completeTasksOnProcess == value)
        return;
      this._completeTasksOnProcess = value;
      this.SetModified(true);
    }
  }

  public void Save([NotNull] XmlIni ini)
  {
    ini.WriteBoolean("Props", "EnableMailNotifications", this.EnableMailNotifications);
    ini.WriteBoolean("Props", "RequireTaskVerification", this.RequireTaskVerification);
    ini.WriteBoolean("Props", "CompleteTasksOnProcess", this.CompleteTasksOnProcess);
    this.Modified = false;
  }

  public void Load([NotNull] XmlIni ini)
  {
    this.EnableMailNotifications = ini.ReadBoolean("Props", "EnableMailNotifications", this.RequireTaskVerification);
    this.RequireTaskVerification = ini.ReadBoolean("Props", "RequireTaskVerification", this.RequireTaskVerification);
    this.CompleteTasksOnProcess = ini.ReadBoolean("Props", "CompleteTasksOnProcess", this.CompleteTasksOnProcess);
    this.Modified = false;
  }
}
