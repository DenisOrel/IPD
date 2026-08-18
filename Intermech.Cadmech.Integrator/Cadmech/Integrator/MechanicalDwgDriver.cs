// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.MechanicalDwgDriver
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Mechanical;
using Intermech.Tools.Integrators.Notifications;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class MechanicalDwgDriver : MechanicalDriver, IDwgDriver, ICaptureChangesDriver
{
  private readonly IIntegrator integrator;
  private IDwgArticleEmitter articleEmitter;
  private Guid rootDocumentGroup;
  private AcadIntegratorSettings integratorSettings;
  private MechanicalLayer apiLayer;

  public MechanicalDwgDriver(IIntegrator integrator)
  {
    this.integrator = integrator != null ? integrator : throw new ArgumentNullException(nameof (integrator));
  }

  public CaptureChangesDatabase DriverDatabase => this.DriverContext.Database;

  public IDwgArticleEmitter ArticleEmitter
  {
    get => this.articleEmitter;
    set => this.articleEmitter = value;
  }

  public Guid RootDocumentGroup
  {
    get => this.rootDocumentGroup;
    set => this.rootDocumentGroup = value;
  }

  public IIntegrator Integrator => this.integrator;

  public AcadIntegratorSettings IntegratorSettings => this.integratorSettings;

  public IDrawingTypesInfo DrawingTypes
  {
    get => (IDrawingTypesInfo) this.integratorSettings.MechanicalSettings;
  }

  protected override void ValidateDriverProperties()
  {
    base.ValidateDriverProperties();
    this.integratorSettings = ServiceUtils.GetService<AcadIntegratorSettingsService>((object) this.Integrator, true).GetSettings();
    this.integratorSettings.MechanicalSettings.CheckEnabled();
    if (this.articleEmitter == null)
      throw new DataExchangeConfigurationException("ArticleEmitter");
  }

  protected override void ClearDriver()
  {
    base.ClearDriver();
    this.integratorSettings = (AcadIntegratorSettings) null;
    this.apiLayer = (MechanicalLayer) null;
  }

  protected override void InitializeDriverContextServices()
  {
    base.InitializeDriverContextServices();
    this.apiLayer = new MechanicalLayer(this, this.DriverContext);
  }

  protected override IArticleTypesService CreateDefaultArticleTypesService()
  {
    return (IArticleTypesService) new MechanicalDwgArticleTypesService((MechanicalDriver) this, this.DriverContext);
  }

  protected override IArticleLocatorService CreateDefaultArticleLocatorService()
  {
    return (IArticleLocatorService) new MechanicalDwgArticleLocatorService((MechanicalDriver) this, this.DriverContext);
  }

  protected override IArticleStructureService CreateDefaultArticleStructureService()
  {
    return (IArticleStructureService) new MechanicalDwgStructureService(this, this.DriverContext);
  }

  protected override ICollection<Type> GetRemovableSectionTypes()
  {
    ICollection<Type> removableSectionTypes = base.GetRemovableSectionTypes();
    removableSectionTypes.Add(typeof (DwgArticleData));
    return removableSectionTypes;
  }

  protected override void DoPostprocess()
  {
    base.DoPostprocess();
    this.RaiseNotifications();
  }

  private void RaiseNotifications()
  {
    INotificationService service = ServiceUtils.GetService<INotificationService>((object) ServicesManager.ServiceContainer, true);
    if (!service.HasSubscribers(CaptureChangesEventArgs.CaptureChangesCompleted))
      return;
    this.RaiseCaptureChangesCompletedNotification(service);
  }

  private void RaiseCaptureChangesCompletedNotification(INotificationService notificationService)
  {
    EntitySet entitySet = this.DriverContext.Database.Query((IQueryCondition) new BinaryCondition(SectionVirtualProperties.SectionTypeRef, BinaryOperator.Equal, (object) typeof (FilesSection)));
    List<CaptureChangesDocumentInfo> documents = new List<CaptureChangesDocumentInfo>(entitySet.Count);
    foreach (SectionEntity sectionEntity in (HashSet<IEntity>) entitySet)
    {
      long objectId = ObjectSection.GetObjectId(sectionEntity);
      int objectType = ObjectSection.GetObjectType(sectionEntity);
      string masterFile = FilesSection.GetMasterFile(sectionEntity);
      bool isInitialDocument = this.DriverContext.Database.IsEntryPointDocument(sectionEntity);
      bool db = this.IsSavedToDB(sectionEntity);
      documents.Add(new CaptureChangesDocumentInfo(objectId, objectType, masterFile, isInitialDocument, db));
    }
    CaptureChangesEventArgs e = new CaptureChangesEventArgs(CaptureChangesEventArgs.CaptureChangesCompleted, SaveChangesMode.Default, true, this.Integrator, documents);
    notificationService.FireEvent((object) null, (NotificationEventArgs) e);
  }

  private bool IsSavedToDB(SectionEntity objectEntity)
  {
    ObjectActionsSection objectActionsSection = objectEntity.Sections.Get<ObjectActionsSection>((ObjectActionsSection) null);
    return objectActionsSection != null && (objectActionsSection.ObjectActions.ServerActions.Count != 0 || objectActionsSection.RelationActions.ServerActions.Count != 0);
  }

  protected override IDocumentCADApiService DoTryGetDocumentApiService(SectionEntity documentItem)
  {
    return (IDocumentCADApiService) this.apiLayer;
  }

  protected override IArticleCADApiService DoTryGetArticleApiService(SectionEntity articleItem)
  {
    return (IArticleCADApiService) this.apiLayer;
  }

  public override DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    return fullPath != null ? new DocumentFileData(fullPath) : throw new ArgumentNullException(nameof (fullPath));
  }

  public override bool IsDocumentTypeSupported(int documentType)
  {
    return this.DrawingTypes.FindSettings(documentType) != null;
  }

  public override MechanicalDocumentKind GetMechanicalDocumentKindByType(int documentType)
  {
    Guid typeByDrawingType = this.DrawingTypes.GetGroupTypeByDrawingType(documentType, true);
    if (typeByDrawingType == MechanicalSettings.AssemblyDrawingsGroup)
      return MechanicalDocumentKind.AssemblyModel;
    if (typeByDrawingType == MechanicalSettings.PartDrawingsGroup)
      return MechanicalDocumentKind.PartModel;
    throw new NotImplementedException($"Не реализована поддержка группы документов типа '{typeByDrawingType}'.");
  }

  public override List<LocalId<int>> GetTypesByMechanicalDocumentKind(
    MechanicalDocumentKind documentKind)
  {
    List<LocalId<int>> mechanicalDocumentKind = new List<LocalId<int>>(32 /*0x20*/);
    if (documentKind != MechanicalDocumentKind.AssemblyModel)
    {
      if (documentKind != MechanicalDocumentKind.PartModel)
        throw new NotImplementedException($"Не реализована поддержка документов вида '{documentKind}'.");
      mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.DrawingTypes.GetDrawingTypesByGroupType(MechanicalSettings.PartDrawingsGroup));
    }
    else
      mechanicalDocumentKind.AddRange((IEnumerable<LocalId<int>>) this.DrawingTypes.GetDrawingTypesByGroupType(MechanicalSettings.AssemblyDrawingsGroup));
    return mechanicalDocumentKind;
  }

  protected override ModelHandler CreateModelDocumentHandler(SectionEntity docItem)
  {
    ModelHandler modelDocumentHandler = base.CreateModelDocumentHandler(docItem);
    if (this.IsPartDrawing2D(docItem))
      modelDocumentHandler.EnableUnusedArticlesProcessing = false;
    return modelDocumentHandler;
  }

  protected override NormalArticleHandler CreateNormalArticleHandler(SectionEntity articleEntity)
  {
    NormalArticleHandler normalArticleHandler = base.CreateNormalArticleHandler(articleEntity);
    if (this.IsArticleFromPartDrawing2D(articleEntity))
      normalArticleHandler.EnableGroupIdProcessing = false;
    return normalArticleHandler;
  }

  private bool IsArticleFromPartDrawing2D(SectionEntity articleEntity)
  {
    SectionEntity articleMainDocument = this.MechanicalOperations.Articles.TryGetArticleMainDocument(articleEntity);
    return articleMainDocument != null && this.IsPartDrawing2D(articleMainDocument);
  }

  private bool IsPartDrawing2D(SectionEntity documentEntity)
  {
    object documentKind;
    return this.TryGetDocumentKind(documentEntity, out documentKind) && documentKind.Equals((object) MechanicalDocumentKind.PartModel);
  }
}
