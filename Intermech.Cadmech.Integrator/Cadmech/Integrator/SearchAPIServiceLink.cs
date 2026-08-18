// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.SearchAPIServiceLink
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Pdm;
using Intermech.Tools.Integrators;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class SearchAPIServiceLink
{
  public SearchAPIServiceLink(
    IOutputView outputView,
    INotificationService notificationService,
    IIntegratorRegistry integratorRegistry,
    IFileVault fileVaultService,
    Lazy<IArticleService> pdmArticleService,
    Lazy<IImportStructureFromCadService> avsImportService,
    ActiveCADSystemService activeCADSystemService)
  {
    if (outputView == null)
      throw new ArgumentNullException(nameof (outputView));
    if (notificationService == null)
      throw new ArgumentNullException(nameof (notificationService));
    if (integratorRegistry == null)
      throw new ArgumentNullException(nameof (integratorRegistry));
    if (fileVaultService == null)
      throw new ArgumentNullException(nameof (fileVaultService));
    if (pdmArticleService == null)
      throw new ArgumentNullException(nameof (pdmArticleService));
    if (avsImportService == null)
      throw new ArgumentNullException(nameof (avsImportService));
    if (activeCADSystemService == null)
      throw new ArgumentNullException(nameof (activeCADSystemService));
    this.OutputView = outputView;
    this.NotificationService = notificationService;
    this.IntegratorRegistry = integratorRegistry;
    this.FileVaultService = fileVaultService;
    this.PdmArticleService = pdmArticleService;
    this.AvsImportService = avsImportService;
    this.ActiveCADSystemService = activeCADSystemService;
  }

  public IOutputView OutputView { get; private set; }

  public INotificationService NotificationService { get; private set; }

  public IIntegratorRegistry IntegratorRegistry { get; private set; }

  public IFileVault FileVaultService { get; private set; }

  public Lazy<IArticleService> PdmArticleService { get; private set; }

  public Lazy<IImportStructureFromCadService> AvsImportService { get; private set; }

  public ActiveCADSystemService ActiveCADSystemService { get; private set; }
}
