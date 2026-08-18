// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.UserSessionSharedData
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Projects;
using System.Diagnostics;
using System.Threading;


namespace Intermech.Kernel;

internal sealed class UserSessionSharedData
{
  private long _currentProjectID;
  private int _projectFiltrationMode;

  public UserSessionSharedData()
  {
    this._currentProjectID = 0L;
    this._projectFiltrationMode = 0;
  }

  public long CurrentProjectID
  {
    [DebuggerStepThrough] get => Interlocked.Read(ref this._currentProjectID);
    set => Interlocked.Exchange(ref this._currentProjectID, value);
  }

  public ProjectFiltrationModes ProjectFiltrationMode
  {
    [DebuggerStepThrough] get => (ProjectFiltrationModes) this._projectFiltrationMode;
    set => Interlocked.Exchange(ref this._projectFiltrationMode, (int) value);
  }
}
