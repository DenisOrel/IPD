// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.PCBDocumentDependenciesBuilder
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class PCBDocumentDependenciesBuilder : MechanicalFileDependenciesHandler
{
  private ADMechanicalDriver _driver;
  private IPCBDocument _document;
  private AddInProxy _proxy;

  public PCBDocumentDependenciesBuilder(
    ADMechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    IPCBDocument pcbDocument,
    AddInProxy proxy)
    : base((MechanicalDriver) driver, driverContext, ClientContext.FileVault)
  {
    this._driver = driver ?? throw new ArgumentNullException(nameof (driver));
    this._document = pcbDocument ?? throw new ArgumentNullException(nameof (pcbDocument));
    this._proxy = proxy;
  }

  protected override void CollectDependencies() => base.CollectDependencies();
}
