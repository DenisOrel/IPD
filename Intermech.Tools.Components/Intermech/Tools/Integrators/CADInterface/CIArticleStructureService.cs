// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIArticleStructureService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Collections;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Files;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.IO;
using Intermech.Localization;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>Реализует сервис для работы с составом изделия.</summary>
public class CIArticleStructureService : ArticleStructureService
{
  private ICADInterfaceService cadInterfaceService;
  private IFileVault fileVault;
  private CICommonApiOperations commonOperations;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Драйвер захвата изменений</param>
  /// <param name="driverContext">Контекст выполняемой операции</param>
  /// <param name="cadInterfaceService">Сервис доступа к API CAD-интерфейса</param>
  /// <exception cref="T:ArgumentNullException">driver or driverContext or cadInterfaceService</exception>
  public CIArticleStructureService(
    CICaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext,
    ICADInterfaceService cadInterfaceService)
    : base((MechanicalDriver) driver, driverContext)
  {
    this.cadInterfaceService = cadInterfaceService != null ? cadInterfaceService : throw new ArgumentNullException(nameof (cadInterfaceService));
    this.fileVault = ClientContext.FileVault;
    this.commonOperations = new CICommonApiOperations(driver, this.fileVault);
  }

  /// <summary>Возвращает драйвер захвата изменений.</summary>
  private CICaptureChangesDriver CIDriver
  {
    [DebuggerStepThrough] get => (CICaptureChangesDriver) this.Driver;
  }

  /// <summary>
  /// Возвращает объект, реализующий типовые операции с API CAD-системы.
  /// </summary>
  public CICommonApiOperations CommonOperations
  {
    [DebuggerStepThrough] get => this.commonOperations;
  }

  /// <summary>
  /// Проверяет, является ли указанное изделие сборочной единицей, т.е. изделием с конструкторским составом.
  /// Это метод используется для определения изделий, для которых требуется выполнить синхронизацию состава.
  /// </summary>
  /// <param name="articleItem">Сущность изделия</param>
  /// <param name="documentItem">Сущность конструкторского документа, по которому выпускается изделие. Значение параметра может быть null, если источником изделия является не документ, а что-то другое</param>
  /// <returns>true - указанное изделие является сборочной единицей и требует синхронизации состава, false - изделие не требует синхронизации состава</returns>
  protected override bool IsProjectArticle(SectionEntity articleItem, SectionEntity documentItem)
  {
    if (documentItem != null)
    {
      MechanicalDocumentKind? mechanicalDocumentKind = this.Driver.TryGetMechanicalDocumentKind(documentItem);
      if (mechanicalDocumentKind.HasValue && mechanicalDocumentKind.Value == MechanicalDocumentKind.AssemblyModel)
        return true;
    }
    return base.IsProjectArticle(articleItem, documentItem);
  }

  /// <summary>
  /// Возвращает состав указанной сборочной единицы в виде коллекции вхождений изделий-компонентов.
  /// Каждое вхождение компонента соответствует одной проектной связи с компонентом в базе IPS.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <returns>Коллекция вхождений изделий-компонентов</returns>
  protected override List<ArticleStructureOccurence> DoReadArticleStructure(
    SectionEntity projectArticleItem)
  {
    List<ArticleStructureOccurence> list = base.DoReadArticleStructure(projectArticleItem);
    SectionEntity articleMainDocument = this.CIDriver.MechanicalOperations.Articles.GetArticleMainDocument(projectArticleItem);
    List<AssemblyStructureRecord> articleStructureCached = this.commonOperations.GetArticleStructureCached(articleMainDocument);
    ArticleSection projectArticle = projectArticleItem.Sections.Get<ArticleSection>();
    string projModelPath = FilesSection.GetMasterFile(articleMainDocument);
    Predicate<AssemblyStructureRecord> match = (Predicate<AssemblyStructureRecord>) (structureRecord => structureRecord.ProjectConfiguration == null || PathUtils.IsSamePath(this.commonOperations.MakeArticleKey(structureRecord.ProjectConfiguration, projModelPath), projectArticle.ArticleKey));
    List<AssemblyStructureRecord> all = articleStructureCached.FindAll(match);
    CollectionUtils.EnsureNewItemsCapacity<ArticleStructureOccurence>(list, all.Count);
    foreach (AssemblyStructureRecord sectionObject in all)
    {
      ArticleStructureOccurence structureOccurence = new ArticleStructureOccurence(sectionObject.OccurenceGuid, this.commonOperations.MakeArticleKey((string) sectionObject.ComponentConfiguration.Name, sectionObject.ComponentMasterFile));
      structureOccurence.Sections.Set((object) sectionObject);
      structureOccurence.Attributes.ImportRange((IEnumerable<ValueRecord>) sectionObject.Attributes);
      structureOccurence.Attributes.SetFlagForAll(NamedFlags.ReadOnly);
      list.Add(structureOccurence);
    }
    return list;
  }

  /// <summary>Реализует поиск сущности для изделия-компонента.</summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Найденная сущность для изделия компонента или null</returns>
  protected override SectionEntity DoFindArticleComponent(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence)
  {
    SectionEntity articleComponent = base.DoFindArticleComponent(projectArticleItem, componentOccurence);
    if (articleComponent != null)
      return articleComponent;
    AssemblyStructureRecord customOccurence = componentOccurence.Sections.Get<AssemblyStructureRecord>();
    IWorkArea workArea = this.fileVault.WorkArea;
    if (!PathUtils.IsPlacedIn(customOccurence.ComponentMasterFile, workArea.AreaPath))
      return (SectionEntity) null;
    FileOrigin fileOrigin = workArea.GetFileOrigin(customOccurence.ComponentMasterFile, false);
    if (fileOrigin.OriginType == FileOriginType.DetachedFile)
      return (SectionEntity) null;
    switch (fileOrigin.OriginType)
    {
      case FileOriginType.NewFile:
        articleComponent = this.TryFindNewJTComponentEntity(projectArticleItem, componentOccurence, customOccurence, fileOrigin);
        break;
      case FileOriginType.WorkFile:
        articleComponent = this.TryFindExistingJTComponentEntity(projectArticleItem, componentOccurence, customOccurence, fileOrigin) ?? this.TryFindExistingArticleComponentEntity(projectArticleItem, componentOccurence, customOccurence, fileOrigin);
        break;
    }
    return articleComponent;
  }

  private SectionEntity TryFindNewJTComponentEntity(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence,
    AssemblyStructureRecord customOccurence,
    FileOrigin componentDocument)
  {
    if (!this.CIDriver.IntegratorSettings.JTDerivativesEnabled)
      return (SectionEntity) null;
    SectionEntity byMasterFile = FilesSection.FindByMasterFile(this.DriverContext.Database, customOccurence.ComponentMasterFile);
    if (byMasterFile == null)
      return (SectionEntity) null;
    JTDerivedFileInfo jtDerivedFileInfo = byMasterFile.Sections.Get<JTDerivedFileInfo>((JTDerivedFileInfo) null);
    if (jtDerivedFileInfo == null || jtDerivedFileInfo.JTDocumentId == 0L)
      return (SectionEntity) null;
    return this.FindOrCreateComponentEntity(componentOccurence, JTLinkManager.ArticleFromJTDocument(jtDerivedFileInfo.JTDocumentId).LocateObject() ?? throw this.CantFindJTComponent(projectArticleItem, componentOccurence, componentDocument.FileName, jtDerivedFileInfo.JTDocumentId), (SectionEntity) null);
  }

  private SectionEntity TryFindExistingJTComponentEntity(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence,
    AssemblyStructureRecord customOccurence,
    FileOrigin componentDocument)
  {
    if (!this.CIDriver.IntegratorSettings.JTDerivativesEnabled)
      return (SectionEntity) null;
    if (DBHelper.GetObjectType(componentDocument.WorkObject.ObjectId) != this.CIDriver.IntegratorSettings.JTDerivedDocumentType.Id)
      return (SectionEntity) null;
    ObjectLocatorResult objectLocatorResult = JTLinkManager.JTDocumentFromDerviedDocument(componentDocument.WorkObject.ObjectId).LocateObject();
    if (objectLocatorResult == null)
      return (SectionEntity) null;
    return this.FindOrCreateComponentEntity(componentOccurence, JTLinkManager.ArticleFromJTDocument(objectLocatorResult.ObjectId).LocateObject() ?? throw this.CantFindJTComponent(projectArticleItem, componentOccurence, componentDocument.FileName, objectLocatorResult.ObjectId), (SectionEntity) null);
  }

  private SectionEntity FindOrCreateComponentEntity(
    ArticleStructureOccurence componentOccurence,
    ObjectLocatorResult componentArticle,
    SectionEntity componentModel)
  {
    SectionEntity createComponentEntity = ObjectSection.FindByObjectId(this.DriverContext.Database, componentArticle.ObjectId, false);
    if (createComponentEntity == null)
    {
      createComponentEntity = this.DriverContext.Database.AddReferencedDBObject(componentArticle.ObjectId, componentArticle.ObjectType);
      ArticleSection sectionObject = new ArticleSection();
      sectionObject.ArticleKey = componentOccurence.ComponentKey;
      if (componentModel != null)
        sectionObject.SetInitialDocument(ArticleInitialDocumentType.Normal, componentModel);
      createComponentEntity.Sections.Set((object) sectionObject);
    }
    return createComponentEntity;
  }

  private Exception CantFindJTComponent(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence,
    string componentFilePath,
    long jtDocId)
  {
    ObjectLocatorResult objectLocatorResult = JTLinkManager.SourceDocumentFromJTDocument(jtDocId).LocateObject();
    if (objectLocatorResult == null)
      throw this.CantFindJTComponentSourceDocument(projectArticleItem, componentFilePath);
    string masterFileName = this.fileVault.DBFilesInfo.GetMasterFileName(objectLocatorResult.ObjectId, false);
    if (string.IsNullOrEmpty(masterFileName))
      throw this.CantFindJTComponentSourceDocument(projectArticleItem, componentFilePath);
    string componentFilePath1 = Path.Combine(this.fileVault.WorkArea.AreaPath, masterFileName);
    string objectCaption = DBHelper.GetObjectCaption(objectLocatorResult.ObjectId);
    throw this.CantFindArticleComponent(projectArticleItem, componentOccurence, componentFilePath1, objectLocatorResult.ObjectId, objectCaption);
  }

  private Exception CantFindJTComponentSourceDocument(
    SectionEntity projectArticleItem,
    string componentFilePath)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Tools.Components_486"), (object) DisplaySection.GetDisplayName(projectArticleItem));
    stringBuilder.Append(' ');
    stringBuilder.AppendFormat("В базе IPS не удалось найти исходный конструкторский документ, который является основой для JT-представления документа и для используемого в сборочной единице представления '{0}'.", (object) componentFilePath);
    stringBuilder.AppendLine();
    stringBuilder.AppendLine();
    stringBuilder.AppendFormat("Восстановите связи между представлением '{0}', JT-представлением документа и исходным документом, а затем повторите текущую операцию.", (object) componentFilePath);
    throw new FaultException(stringBuilder.ToString());
  }

  private SectionEntity TryFindExistingArticleComponentEntity(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence,
    AssemblyStructureRecord customOccurence,
    FileOrigin componentDocument)
  {
    ObjectLocatorResult article = this.cadInterfaceService.FindArticle(customOccurence.ComponentConfiguration, componentDocument.WorkObject.ObjectId);
    if (article == null)
      throw this.CantFindArticleComponent(projectArticleItem, componentOccurence, componentDocument.FileName, componentDocument.WorkObject.ObjectId, componentDocument.WorkObject.Caption);
    SectionEntity componentModel = ObjectSection.FindByObjectId(this.DriverContext.Database, componentDocument.WorkObject.ObjectId, true) ?? this.DriverContext.Database.AddReferencedDBObject(componentDocument.WorkObject.ObjectId);
    return this.FindOrCreateComponentEntity(componentOccurence, article, componentModel);
  }

  private Exception CantFindArticleComponent(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence,
    string componentFilePath,
    long componentObjectId,
    string componentCaption)
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Tools.Components_486"), (object) DisplaySection.GetDisplayName(projectArticleItem));
    stringBuilder.Append(' ');
    stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_554"), (object) componentOccurence.ComponentKey, (object) componentCaption);
    stringBuilder.Append(' ');
    stringBuilder.Append(LocalizationHolder.rm.GetString("SR_555"));
    stringBuilder.AppendLine();
    stringBuilder.AppendLine();
    stringBuilder.Append(LocalizationHolder.rm.GetString("SR_556"));
    stringBuilder.AppendLine();
    stringBuilder.AppendLine();
    stringBuilder.AppendLine(LocalizationHolder.rm.GetString("SR_557"));
    stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_558"), (object) 1, (object) componentObjectId);
    stringBuilder.AppendLine();
    stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_559"), (object) 2, (object) componentCaption);
    stringBuilder.AppendLine();
    stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("SR_560"), (object) 3, (object) componentFilePath);
    throw new FaultException(stringBuilder.ToString());
  }

  /// <summary>
  /// Возвращает путь к файлу документа, в котором описан компонент.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="componentOccurence">Вхождение изделия-компонента</param>
  /// <returns>Путь к файлу документа или null</returns>
  protected override string DoTryGetArticleComponentFile(
    SectionEntity projectArticleItem,
    ArticleStructureOccurence componentOccurence)
  {
    return componentOccurence.Sections.Get<AssemblyStructureRecord>().ComponentMasterFile;
  }

  /// <summary>
  /// Записывает в объект CAD-системы изменения, сделанные в процессе синхронизации состава сборочной единицы.
  /// </summary>
  /// <param name="projectArticleItem">Сущность сборочной единицы</param>
  /// <param name="stats">Статистика по изменениям в составе сборочной единицы</param>
  protected override void DoFlushArticleStructureChanges(
    SectionEntity projectArticleItem,
    ArticleStructureStats stats)
  {
    base.DoFlushArticleStructureChanges(projectArticleItem, stats);
    if (stats.CreatedRelations <= 0)
      return;
    SectionEntity articleMainDocument = this.CIDriver.MechanicalOperations.Articles.GetArticleMainDocument(projectArticleItem);
    if (!this.commonOperations.FlushArticleStructureChanges(articleMainDocument))
      return;
    AnalyzerChangesSection.Mark(articleMainDocument);
  }
}
