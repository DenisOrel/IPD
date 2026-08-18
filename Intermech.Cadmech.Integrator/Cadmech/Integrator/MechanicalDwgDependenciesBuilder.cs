// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalDwgDependenciesBuilder
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalDwgDependenciesBuilder : MechanicalFileDependenciesHandler
{
  private readonly MechanicalDwgDriver driver;
  private readonly DwgDependenciesPeek<MechanicalDwgDriver> impl;

  public MechanicalDwgDependenciesBuilder(
    MechanicalDwgDriver driver,
    CaptureChangesDriverContext driverContext)
    : base((MechanicalDriver) driver, driverContext, ClientContext.FileVault)
  {
    this.driver = driver != null ? driver : throw new ArgumentNullException(nameof (driver));
    this.impl = new DwgDependenciesPeek<MechanicalDwgDriver>(driver);
  }

  protected override void CollectDependencies()
  {
    base.CollectDependencies();
    this.DocumentDependencies.AddRange((IEnumerable<DocumentFileData>) this.impl.GetDependencies(this.DocumentFile));
  }
}
