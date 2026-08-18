// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ChangesDriver
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using System;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class ChangesDriver : CompositeChangesDriver
{
  private readonly IServiceProvider integrator;
  private readonly MechanicalDwgDriver mechDriver;
  private readonly ConstructionalExtension constrDriver;
  private IApplicationFileTypes fileTypeSvc;
  private DwgDriverProcessingSchemas processingSchemas;

  public ChangesDriver(IIntegrator integrator)
  {
    this.integrator = integrator != null ? (IServiceProvider) integrator : throw new ArgumentNullException();
    this.mechDriver = new MechanicalDwgDriver(integrator);
    this.constrDriver = new ConstructionalExtension(integrator);
  }

  public DwgDriverProcessingSchemas ProcessingSchemas
  {
    get => this.processingSchemas;
    set => this.processingSchemas = value;
  }

  public bool MechanicalDocumentsEnabled
  {
    get => (this.ProcessingSchemas & DwgDriverProcessingSchemas.MechanicalDocuments) != 0;
  }

  public bool ConstructionalDocumentsEnabled
  {
    get => (this.ProcessingSchemas & DwgDriverProcessingSchemas.ConstructionalDocuments) != 0;
  }

  public void ApplyTypicalSettings()
  {
    AcadIntegratorSettings settings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.integrator, true).GetSettings();
    if (this.ProcessingSchemas == (DwgDriverProcessingSchemas) 0)
    {
      if (settings.MechanicalSettings.IsEnabled)
        this.ProcessingSchemas |= DwgDriverProcessingSchemas.MechanicalDocuments;
      if (settings.ConstructionalSettings.IsEnabled)
        this.ProcessingSchemas |= DwgDriverProcessingSchemas.ConstructionalDocuments;
      if (this.ProcessingSchemas == (DwgDriverProcessingSchemas) 0)
        throw this.NoAnyProcessingSchemas();
    }
    if (!this.MechanicalDocumentsEnabled)
      return;
    if (RuntimeOptions.DisableExtendedSave.Value)
    {
      this.MechanicalDocuments.UpdateArticles = false;
      this.MechanicalDocuments.ArticleEmitter = (IDwgArticleEmitter) new EmptyArticleEmitter();
    }
    else
    {
      this.MechanicalDocuments.UpdateArticles = true;
      this.MechanicalDocuments.ArticleEmitter = (IDwgArticleEmitter) new NormalDwgArticleEmitter((MechanicalDriver) this.MechanicalDocuments, (IDrawingTypesInfo) settings.MechanicalSettings);
    }
  }

  private FaultException NoAnyProcessingSchemas()
  {
    return new FaultException("Обработка всех видов чертежей dwg отключена в настройках интегратора.");
  }

  public MechanicalDwgDriver MechanicalDocuments => this.mechDriver;

  public ConstructionalExtension ConstructionalDocuments => this.constrDriver;

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.fileTypeSvc = ServiceUtils.GetService<IApplicationFileTypes>((object) this.integrator, true);
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.fileTypeSvc = (IApplicationFileTypes) null;
    this.mechDriver.ArticleEmitter = (IDwgArticleEmitter) null;
    this.mechDriver.RootDocumentGroup = Guid.Empty;
  }

  protected override void ValidateRootFile(string rootFilePath, long rootObjectId)
  {
    this.ValidateDocumentFile(rootFilePath);
  }

  protected override string ValidateRootFile(string rootFilePath)
  {
    this.ValidateDocumentFile(rootFilePath);
    return rootFilePath;
  }

  private void ValidateDocumentFile(string rootFilePath)
  {
    if (!this.fileTypeSvc.IsApplicationFile(rootFilePath))
      throw new FaultException($"Файл '{rootFilePath}' не является чертежем dwg.");
  }

  protected override ICaptureChangesDriver SelectDriver(string fullPath)
  {
    if (this.ProcessingSchemas == DwgDriverProcessingSchemas.MechanicalDocuments)
      return (ICaptureChangesDriver) this.mechDriver;
    if (this.ProcessingSchemas == DwgDriverProcessingSchemas.ConstructionalDocuments)
      return (ICaptureChangesDriver) this.constrDriver;
    if (this.ProcessingSchemas != (DwgDriverProcessingSchemas) 0)
    {
      AcadSetupSettings appSetupSettings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.integrator, true).GetAppSetupSettings();
      if (appSetupSettings.UseSpecificProfile)
      {
        string upper = appSetupSettings.ProfileName.ToUpper();
        return !upper.Contains("SPDS") && !upper.Contains("PGS") ? (ICaptureChangesDriver) this.mechDriver : (ICaptureChangesDriver) this.constrDriver;
      }
    }
    return (ICaptureChangesDriver) this.mechDriver;
  }

  protected override ICaptureChangesDriver SelectDriver(long documentId, LocalId<int> documentType)
  {
    AcadIntegratorSettings settings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.integrator, true).GetSettings();
    if (this.MechanicalDocumentsEnabled && settings.MechanicalSettings.GetGroupTypeByDrawingType(documentType.Id, false) != Guid.Empty)
      return (ICaptureChangesDriver) this.mechDriver;
    if (this.ConstructionalDocumentsEnabled && settings.ConstructionalSettings.GetGroupTypeByDrawingType(documentType.Id, false) != Guid.Empty)
      return (ICaptureChangesDriver) this.constrDriver;
    throw this.NoAnyProcessingSchemas();
  }
}
