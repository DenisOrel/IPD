// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ConstructionalDependenciesBuilder
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class ConstructionalDependenciesBuilder : FileDependenciesHandler
{
  private readonly ConstructionalExtension driver;
  private readonly DwgDependenciesPeek<ConstructionalExtension> impl;

  public ConstructionalDependenciesBuilder(
    ConstructionalExtension driver,
    CaptureChangesDriverContext driverContext)
    : base(driverContext, (IDocumentBuilder) driver, ClientContext.FileVault, driver.Operations.DraftDocuments)
  {
    this.driver = driver != null ? driver : throw new ArgumentNullException(nameof (driver));
    this.impl = new DwgDependenciesPeek<ConstructionalExtension>(driver);
  }

  protected override void CollectDependencies()
  {
    base.CollectDependencies();
    this.DocumentDependencies.AddRange((IEnumerable<DocumentFileData>) this.impl.GetDependencies(this.DocumentFile));
  }
}
