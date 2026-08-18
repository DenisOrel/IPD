// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DCaptureChangesDriver
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Collections;
using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DCaptureChangesDriver : CICaptureChangesDriver
{
  private Drawing2DDetectorService drawing2DDetectorService;
  private K3DCADInterfaceService cadInterfaceService;
  private Lazy<Drawing2DOperations> drawing2DOperations;
  private Drawing2DExternalKeysService drawing2DExternalKeysService;
  private Drawing2DArticleAttributesProcessingService drawing2DArticleAttributesService;
  private Drawing2DComponentArticleLocatorService drawing2DcomponentArticleLocatorService;
  private Drawing2DGenericDocumentApiService drawing2DGenericDocumentApiService;
  private Drawing2DAssemblyDocumentApiService drawing2DAssemblyDocumentApiService;
  private Drawing2DHeadArticleApiService drawing2DHeadArticleApiService;
  private Drawing2DComponentArticleApiService drawing2DComponentArticleApiService;
  private Lazy<IArticleStructureService> headArticleStructureService;

  public K3DCaptureChangesDriver(
    IIntegrator integrator,
    K3DCADInterfaceService cadInterfaceService,
    Drawing2DDetectorService drawing2DDetectorService)
    : base(integrator)
  {
    if (cadInterfaceService == null)
      throw new ArgumentNullException(nameof (cadInterfaceService));
    if (drawing2DDetectorService == null)
      throw new ArgumentNullException(nameof (drawing2DDetectorService));
    this.cadInterfaceService = cadInterfaceService;
    this.drawing2DDetectorService = drawing2DDetectorService;
  }

  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.drawing2DOperations = new Lazy<Drawing2DOperations>((Func<Drawing2DOperations>) (() => new Drawing2DOperations(this.K3DSettings)));
    this.drawing2DGenericDocumentApiService = new Drawing2DGenericDocumentApiService(this, this.DriverContext, this.cadInterfaceService);
    this.drawing2DAssemblyDocumentApiService = new Drawing2DAssemblyDocumentApiService(this, this.DriverContext, this.cadInterfaceService);
    this.drawing2DHeadArticleApiService = new Drawing2DHeadArticleApiService(this, this.DriverContext);
    this.drawing2DComponentArticleApiService = new Drawing2DComponentArticleApiService(this, this.DriverContext);
    this.headArticleStructureService = new Lazy<IArticleStructureService>((Func<IArticleStructureService>) (() => (IArticleStructureService) new Drawing2DHeadArticleStructureService(this, this.DriverContext, (ICADInterfaceService) this.cadInterfaceService)));
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.drawing2DOperations = (Lazy<Drawing2DOperations>) null;
    this.drawing2DExternalKeysService = (Drawing2DExternalKeysService) null;
    this.drawing2DArticleAttributesService = (Drawing2DArticleAttributesProcessingService) null;
    this.drawing2DcomponentArticleLocatorService = (Drawing2DComponentArticleLocatorService) null;
    this.drawing2DGenericDocumentApiService = (Drawing2DGenericDocumentApiService) null;
    this.drawing2DAssemblyDocumentApiService = (Drawing2DAssemblyDocumentApiService) null;
    this.drawing2DHeadArticleApiService = (Drawing2DHeadArticleApiService) null;
    this.drawing2DComponentArticleApiService = (Drawing2DComponentArticleApiService) null;
    this.headArticleStructureService = (Lazy<IArticleStructureService>) null;
  }

  protected override IArticleExternalKeysService DoTryGetArticleExternalKeysService(
    SectionEntity documentItem)
  {
    if (!this.Drawing2DOperations.IsDrawing2D(documentItem))
      return base.DoTryGetArticleExternalKeysService(documentItem);
    if (this.drawing2DExternalKeysService == null)
      this.drawing2DExternalKeysService = new Drawing2DExternalKeysService(this, this.DriverContext);
    return (IArticleExternalKeysService) this.drawing2DExternalKeysService;
  }

  public override DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath)
  {
    DocumentFileData documentFileData = base.OpenDocumentFile(documentItem, fullPath);
    if (this.DriverContext.Database.IsEntryPointDocument(documentItem) && this.IsDrawing2DFile(documentItem, documentFileData.CustomSections))
      this.Drawing2DOperations.AddCustomDocumentData(documentFileData.CustomSections);
    return documentFileData;
  }

  private bool IsDrawing2DFile(SectionEntity documentItem, SectionCollection openFileData)
  {
    ObjectSection objectSection = documentItem.Sections.Get<ObjectSection>((ObjectSection) null);
    return objectSection != null && objectSection.ObjectType != -1 ? this.drawing2DDetectorService.IsDrawing2D(objectSection.ObjectType) : this.drawing2DDetectorService.IsDrawing2D(openFileData.Get<CIDocumentData>().Document);
  }

  protected override void ValidateAllowImportAsDocument(DocumentFileData openRootFile)
  {
    if (ServiceUtils.GetService<IModelDrawingsService>((object) this.Integrator, true).IsDrawingFileName(openRootFile.DocumentFilePath) && this.Drawing2DOperations.IsDrawing2D(openRootFile.CustomSections))
      return;
    base.ValidateAllowImportAsDocument(openRootFile);
  }

  protected override void ValidateAllowImportByPDMFlag(DocumentFileData openRootFile)
  {
    if (this.Drawing2DOperations.IsDrawing2D(openRootFile.CustomSections))
      return;
    base.ValidateAllowImportByPDMFlag(openRootFile);
  }

  public override bool IsDocumentTypeSupported(int documentType)
  {
    return this.drawing2DDetectorService.IsDrawing2D(documentType) || base.IsDocumentTypeSupported(documentType);
  }

  protected override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    return this.Drawing2DOperations.IsDrawing2D(docItem) ? this.DetectNewDrawing2DType(docItem, this.drawing2DGenericDocumentApiService) : base.DetectNewDocumentType(docItem);
  }

  private List<LocalId<int>> DetectNewDrawing2DType(
    SectionEntity docItem,
    Drawing2DGenericDocumentApiService docApi)
  {
    List<LocalId<int>> possibleTypes1 = new List<LocalId<int>>(32 /*0x20*/);
    possibleTypes1.AddRange((IEnumerable<LocalId<int>>) this.K3DSettings.PartDrawings2D.DocumentTypes);
    possibleTypes1.AddRange((IEnumerable<LocalId<int>>) this.K3DSettings.AssemblyDrawings2D.DocumentTypes);
    LocalId<int> localId = docApi.TryReadDocumentType(docItem, (ICollection<LocalId<int>>) possibleTypes1);
    if (localId != null)
      return CollectionUtils.CreateList<LocalId<int>>(localId);
    List<LocalId<int>> possibleTypes2 = new List<LocalId<int>>(32 /*0x20*/);
    possibleTypes2.AddRange((IEnumerable<LocalId<int>>) this.K3DSettings.AssemblyDrawings2D.DocumentTypes);
    return docApi.TryReadDesignDocumentType(docItem, (ICollection<LocalId<int>>) possibleTypes2) ?? possibleTypes1;
  }

  protected override object DoMapDocumentTypeToKind(int documentType)
  {
    if (this.drawing2DDetectorService.IsDrawing2D(documentType))
    {
      if (this.K3DSettings.PartDrawings2D.ContainsType(documentType))
        return (object) Drawing2DDocumentKind.PartDrawing;
      if (this.K3DSettings.AssemblyDrawings2D.ContainsType(documentType))
        return (object) Drawing2DDocumentKind.AssemblyDrawing;
    }
    return base.DoMapDocumentTypeToKind(documentType);
  }

  public override List<LocalId<int>> GetTypesByMechanicalDocumentKind(
    MechanicalDocumentKind documentKind)
  {
    List<LocalId<int>> mechanicalDocumentKind = base.GetTypesByMechanicalDocumentKind(documentKind);
    if (documentKind == MechanicalDocumentKind.GenericDocument && this.K3DSettings.EnableDrawings2DSupport)
    {
      mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.K3DSettings.PartDrawings2D.DocumentTypes);
      mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.K3DSettings.AssemblyDrawings2D.DocumentTypes);
    }
    return mechanicalDocumentKind;
  }

  public override MechanicalDocumentKind GetMechanicalDocumentKindByType(int documentType)
  {
    return this.drawing2DDetectorService.IsDrawing2D(documentType) ? MechanicalDocumentKind.GenericDocument : base.GetMechanicalDocumentKindByType(documentType);
  }

  protected override IAction CreateTypedDocumentHandler(
    SectionEntity docItem,
    object documentKind,
    int documentType)
  {
    return documentKind is Drawing2DDocumentKind ? this.CreateDrawing2DHandler(docItem) : base.CreateTypedDocumentHandler(docItem, documentKind, documentType);
  }

  private IAction CreateDrawing2DHandler(SectionEntity docItem)
  {
    return docItem != null ? (IAction) new Drawing2DDocumentHandler((MechanicalDriver) this, this.DriverContext, docItem) : throw new ArgumentNullException(nameof (docItem));
  }

  protected override IDocumentCADApiService DoTryGetDocumentApiService(SectionEntity documentItem)
  {
    if (!this.Drawing2DOperations.IsDrawing2D(documentItem))
      return base.DoTryGetDocumentApiService(documentItem);
    object documentKind;
    return this.TryGetDocumentKind(documentItem, out documentKind) && documentKind.Equals((object) Drawing2DDocumentKind.AssemblyDrawing) ? (IDocumentCADApiService) this.drawing2DAssemblyDocumentApiService : (IDocumentCADApiService) this.drawing2DGenericDocumentApiService;
  }

  protected override IArticleCADApiService DoTryGetArticleApiService(SectionEntity articleItem)
  {
    if (!this.Drawing2DOperations.IsDrawing2DArticle(articleItem))
      return base.DoTryGetArticleApiService(articleItem);
    Drawing2DArticleKind articleKind = this.Drawing2DOperations.GetArticleKind(articleItem);
    switch (articleKind)
    {
      case Drawing2DArticleKind.HeadArticle:
        return (IArticleCADApiService) this.drawing2DHeadArticleApiService;
      case Drawing2DArticleKind.ComponentArticle:
        return (IArticleCADApiService) this.drawing2DComponentArticleApiService;
      default:
        throw new NotSupportedEnumException((Enum) articleKind);
    }
  }

  protected override IArticleStructureService DoTryGetArticleStructureService(
    SectionEntity articleItem)
  {
    if (!this.Drawing2DOperations.IsDrawing2DArticle(articleItem))
      return base.DoTryGetArticleStructureService(articleItem);
    Drawing2DArticleKind articleKind = this.Drawing2DOperations.GetArticleKind(articleItem);
    switch (articleKind)
    {
      case Drawing2DArticleKind.HeadArticle:
        return this.headArticleStructureService.Value;
      case Drawing2DArticleKind.ComponentArticle:
        return (IArticleStructureService) null;
      default:
        throw new NotSupportedEnumException((Enum) articleKind);
    }
  }

  protected override IArticlePhysicalPropertiesService DoTryGetArticlePhysicalPropertiesService(
    SectionEntity articleItem)
  {
    return this.Drawing2DOperations.IsDrawing2DArticle(articleItem) ? (IArticlePhysicalPropertiesService) null : base.DoTryGetArticlePhysicalPropertiesService(articleItem);
  }

  protected override IArticleAttributesProcessingService DoTryGetArticleAttributesProcessingService(
    SectionEntity articleItem)
  {
    if (!this.Drawing2DOperations.IsDrawing2DArticle(articleItem))
      return base.DoTryGetArticleAttributesProcessingService(articleItem);
    if (this.drawing2DArticleAttributesService == null)
      this.drawing2DArticleAttributesService = new Drawing2DArticleAttributesProcessingService(this, this.DriverContext);
    return (IArticleAttributesProcessingService) this.drawing2DArticleAttributesService;
  }

  protected override IArticleLocatorService DoTryGetArticleLocatorService(SectionEntity articleItem)
  {
    if (!this.Drawing2DOperations.IsComponentArticle(articleItem))
      return base.DoTryGetArticleLocatorService(articleItem);
    if (this.drawing2DcomponentArticleLocatorService == null)
      this.drawing2DcomponentArticleLocatorService = new Drawing2DComponentArticleLocatorService(this, this.DriverContext);
    return (IArticleLocatorService) this.drawing2DcomponentArticleLocatorService;
  }

  protected override void DoDetachItem(SectionEntity dbItem)
  {
    base.DoDetachItem(dbItem);
    if (this.Drawing2DOperations.IsDrawing2D(dbItem))
      this.Drawing2DOperations.RemoveCustomDocumentData(dbItem);
    if (!this.Drawing2DOperations.IsDrawing2DArticle(dbItem))
      return;
    this.Drawing2DOperations.RemoveCustomArticleData(dbItem);
  }

  protected override bool CanSynchronizeSubstitutions(SectionEntity documentItem)
  {
    return !this.Drawing2DOperations.IsDrawing2D(documentItem) && base.CanSynchronizeSubstitutions(documentItem);
  }

  protected override NormalArticleHandler CreateNormalArticleHandler(SectionEntity articleEntity)
  {
    return (NormalArticleHandler) new K3DNormalArticleHandler(this, this.DriverContext, articleEntity)
    {
      EnableUpdatingArticleMaterial = this.CanUpdateArticleMaterial(articleEntity)
    };
  }

  private bool CanUpdateArticleMaterial(SectionEntity articleEntity)
  {
    SectionEntity articleInitialDocument = this.MechanicalOperations.Articles.TryGetArticleInitialDocument(articleEntity);
    if (articleInitialDocument != null)
    {
      MechanicalDocumentKind? mechanicalDocumentKind = this.TryGetMechanicalDocumentKind(articleInitialDocument);
      if (mechanicalDocumentKind.HasValue && mechanicalDocumentKind.Value == MechanicalDocumentKind.PartModel)
        return true;
    }
    return false;
  }

  protected override ModelHandler CreateModelDocumentHandler(SectionEntity docItem)
  {
    return (ModelHandler) new K3DModelDocumentHandler(this, this.DriverContext, docItem);
  }

  internal K3DIntegratorSettings K3DSettings
  {
    [DebuggerStepThrough] get => (K3DIntegratorSettings) this.IntegratorSettings;
  }

  internal Drawing2DOperations Drawing2DOperations
  {
    [DebuggerStepThrough] get => this.drawing2DOperations.Value;
  }
}
