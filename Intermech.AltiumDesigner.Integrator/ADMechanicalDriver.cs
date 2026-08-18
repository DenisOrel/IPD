// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADMechanicalDriver
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Components.Integrators.Electrical;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADMechanicalDriver(IIntegrator integrator) : ECADMechanicalDriver(integrator)
{
  private FileTypeService fileTypeSvc;
  private SchDocumentApi documentApi;
  private ProjectDocumentApi projectApi;
  private PCBDocumentApi pcbDocumentApi;
  private SettingsService _settingsSvc;

  internal ADIntegratorSettings IntegratorSettings { get; private set; }

  protected override void InitializeDriver()
  {
    base.InitializeDriver();
    this.fileTypeSvc = ServiceUtils.GetService<FileTypeService>((object) this.Integrator, true);
    this._settingsSvc = ServiceUtils.GetService<SettingsService>((object) this.Integrator, true);
    this.IntegratorSettings = this._settingsSvc.GetSettings();
    this.Proxy = ServiceUtils.GetService<ADInterfaceService>((object) this.Integrator, true).GetApplicationObject();
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.fileTypeSvc = (FileTypeService) null;
    this.Proxy = (AddInProxy) null;
    this.documentApi = (SchDocumentApi) null;
    this.projectApi = (ProjectDocumentApi) null;
    this.pcbDocumentApi = (PCBDocumentApi) null;
    this.IntegratorSettings = (ADIntegratorSettings) null;
  }

  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.documentApi = new SchDocumentApi(this, this.DriverContext);
    this.pcbDocumentApi = new PCBDocumentApi(this, this.DriverContext);
    this.projectApi = new ProjectDocumentApi(this, this.DriverContext, ServiceUtils.GetService<IIntegratorOutput>((object) this.Integrator, true));
  }

  protected override IArticleExternalKeysService CreateDefaultArticleExternalKeysService()
  {
    return (IArticleExternalKeysService) new ProjectExternalKeysService((MechanicalDriver) this, this.DriverContext);
  }

  protected override IArticleAttributesProcessingService CreateDefaultArticleAttributesProcessingService()
  {
    return (IArticleAttributesProcessingService) new ADArticleAttributesProcessingService((MechanicalDriver) this, this.DriverContext);
  }

  protected override IArticleTypesService CreateDefaultArticleTypesService()
  {
    return (IArticleTypesService) new ADArticleTypesService((MechanicalDriver) this, this.DriverContext, this._settingsSvc);
  }

  protected override IArticleStructureService CreateDefaultArticleStructureService()
  {
    return (IArticleStructureService) new ElectricalArticleStructureService((AppMechanicalDriver) this, this.DriverContext);
  }

  protected override IArticleDocumentationService CreateDefaultArticleDocumentationService()
  {
    return (IArticleDocumentationService) new ADArticleDocumentationService((MechanicalDriver) this, this.DriverContext, ClientContext.FileVault, this.fileTypeSvc, this.IntegratorSettings);
  }

  protected override void ValidateRootFile(string rootFilePath, long rootObjectId)
  {
    base.ValidateRootFile(rootFilePath, rootObjectId);
    this.CheckIfDocument(rootFilePath);
  }

  protected override ImbaseObjectArticleHandler CreateImbaseObjectArticleHandler(
    SectionEntity articleEntity)
  {
    return this.IntegratorSettings.ImbaseSync ? (ImbaseObjectArticleHandler) new ECADImbaseObjectArticleHandler((MechanicalDriver) this, this.DriverContext, articleEntity, (ECADIntegratorSettings) this.IntegratorSettings) : base.CreateImbaseObjectArticleHandler(articleEntity);
  }

  private void CheckIfDocument(string rootFilePath)
  {
    if (!this.fileTypeSvc.IsApplicationFile(rootFilePath))
      throw new FaultException($"Файл '{rootFilePath}' не является документом приложения");
  }

  protected override ICollection<Type> GetRemovableSectionTypes()
  {
    return base.GetRemovableSectionTypes();
  }

  protected override IDocumentCADApiService DoTryGetDocumentApiService(SectionEntity documentItem)
  {
    FilesSection filesSection = documentItem.Sections.Get<FilesSection>();
    if (filesSection != null)
    {
      string firstPath = Path.GetExtension(filesSection.MasterFile);
      if (PathUtils.IsSamePath(firstPath, ".PrjPcb"))
        return (IDocumentCADApiService) this.projectApi;
      if (PathUtils.IsSamePath(firstPath, ".SchDoc"))
        return (IDocumentCADApiService) this.documentApi;
      if (PathUtils.IsSamePath(firstPath, ".PcbDoc"))
        return (IDocumentCADApiService) this.pcbDocumentApi;
    }
    return base.DoTryGetDocumentApiService(documentItem);
  }

  protected override IArticleCADApiService DoTryGetArticleApiService(SectionEntity articleItem)
  {
    return articleItem.Sections.Contains<ElectricalArticleCache>() ? (IArticleCADApiService) this.projectApi : base.DoTryGetArticleApiService(articleItem);
  }

  public override DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    if (fullPath == null)
      throw new ArgumentNullException(nameof (fullPath));
    this.Proxy.OpenObject(fullPath);
    return DocumentHelper.ReadDocumentData(fullPath, this.Proxy);
  }

  public override bool IsDocumentTypeSupported(int documentType)
  {
    return this.IntegratorSettings.ProjectType != null && this.IntegratorSettings.ProjectType.Id == documentType || this.IntegratorSettings.PCBDocumentTypes != null && this.IntegratorSettings.PCBDocumentTypes.Exists((Predicate<GlobalId<int>>) (x => x.Id == documentType)) || this.IntegratorSettings.SchemaDocumentTypes != null && this.IntegratorSettings.SchemaDocumentTypes.Exists((Predicate<GlobalId<int>>) (x => x.Id == documentType));
  }

  public override MechanicalDocumentKind GetMechanicalDocumentKindByType(int documentType)
  {
    if (this.IntegratorSettings.SchemaDocumentTypes != null && this.IntegratorSettings.SchemaDocumentTypes.Exists((Predicate<GlobalId<int>>) (x => x.Id == documentType)) || this.IntegratorSettings.PCBDocumentTypes != null && this.IntegratorSettings.PCBDocumentTypes.Exists((Predicate<GlobalId<int>>) (x => x.Id == documentType)))
      return MechanicalDocumentKind.GenericDocument;
    if (this.IntegratorSettings.ProjectType != null && this.IntegratorSettings.ProjectType.Id == documentType)
      return MechanicalDocumentKind.AssemblyModel;
    throw new NotSupportedException();
  }

  public override List<LocalId<int>> GetTypesByMechanicalDocumentKind(
    MechanicalDocumentKind documentKind)
  {
    List<LocalId<int>> mechanicalDocumentKind = new List<LocalId<int>>();
    if (documentKind != MechanicalDocumentKind.AssemblyModel)
    {
      if (documentKind != MechanicalDocumentKind.GenericDocument)
        throw new NotSupportedException();
      mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.SchemaDocumentTypes);
      mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.PCBDocumentTypes);
    }
    else
      mechanicalDocumentKind.Add((LocalId<int>) this.IntegratorSettings.ProjectType);
    return mechanicalDocumentKind;
  }

  public List<LocalId<int>> GetDocumentTypesByExt(string ext)
  {
    List<LocalId<int>> documentTypesByExt = new List<LocalId<int>>(8);
    switch (ext)
    {
      case ".PrjPcb":
        if (this.IntegratorSettings.ProjectType != null)
        {
          documentTypesByExt.Add((LocalId<int>) this.IntegratorSettings.ProjectType);
          break;
        }
        break;
      case ".PcbDoc":
        if (this.IntegratorSettings.PCBDocumentTypes != null)
        {
          documentTypesByExt.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.PCBDocumentTypes);
          break;
        }
        break;
      case ".SchDoc":
        if (this.IntegratorSettings.SchemaDocumentTypes != null && this.IntegratorSettings.SchemaDocumentTypes.Count > 0)
        {
          documentTypesByExt.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.SchemaDocumentTypes);
          break;
        }
        break;
    }
    if (documentTypesByExt.Count == 0)
      documentTypesByExt.AddRange((IEnumerable<LocalId<int>>) this.IntegratorSettings.SchemaDocumentTypes);
    return documentTypesByExt;
  }

  public AddInProxy Proxy { get; private set; }
}
