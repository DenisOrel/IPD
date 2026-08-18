// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ProjectDocumentApi
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ProjectDocumentApi : ADDocumentApi, IArticleCADApiService
{
  private IIntegratorOutput _outputSvc;

  public ProjectDocumentApi(
    ADMechanicalDriver driver,
    CaptureChangesDriverContext driverContext,
    IIntegratorOutput outputSvc)
    : base(driver, driverContext)
  {
    this._outputSvc = outputSvc;
  }

  public override List<LocalId<int>> DetectNewDocumentType(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    return new List<LocalId<int>>(1)
    {
      (LocalId<int>) this.driver.IntegratorSettings.ProjectType
    };
  }

  public override ICollection<InitialArticleData> ReadArticles(SectionEntity documentItem)
  {
    if (documentItem == null)
      throw new ArgumentNullException(nameof (documentItem));
    LinkedList<InitialArticleData> articles = new LinkedList<InitialArticleData>();
    FileTypeService service = ServiceUtils.GetService<FileTypeService>((object) this.driver.Integrator, true);
    AddInProxy proxy = documentItem.Sections.Get<AddInProxy>();
    ADIntegratorSettings settings = this.settingsSvc.GetSettings();
    ADProject project = this.GetProject(documentItem);
    List<BoardData<ADDocument>> projectBoards = DocumentHelper.GetProjectBoards(project.GetDocuments(true), service, proxy, settings);
    if (projectBoards != null)
    {
      ElectricalSchemeDescriptors bomAssemblies;
      articles = ADCompositionReader.ReadArticles(projectBoards, settings, this._outputSvc, out bomAssemblies, (CaptureChangesDatabase) documentItem.Database, proxy, (IADProject) project);
      documentItem.Sections.Set((object) bomAssemblies);
    }
    return ImbaseSynchronizationHepler.Synchronize((ICollection<InitialArticleData>) articles, (ECADIntegratorSettings) this.settingsSvc.GetSettings(), this.GetArticleAttributeCodec(ArticleTypes.Component)) ? (ICollection<InitialArticleData>) articles : throw new AbortException("Не удалось синхронизировать компоненты с Imbase");
  }

  public ContainerValues ReadArticleProperties(SectionEntity articleItem)
  {
    ElectricalArticleCache electricalArticleCache = articleItem != null ? articleItem.Sections.Get<ElectricalArticleCache>() : throw new ArgumentNullException(nameof (articleItem));
    ContainerValues containerValues = this.GetArticleAttributeCodec(electricalArticleCache.ArticleType).ReadFileProperties(electricalArticleCache.Article, this.GetArticleFileAttributes(articleItem));
    if (articleItem.Sections.Contains<ImbaseSyncInfo>() && !containerValues.Bag.Exists((StringKey) IDCache.Default.ImbaseKey.Text))
    {
      ImbaseSyncInfo imbaseSyncInfo = articleItem.Sections.Get<ImbaseSyncInfo>();
      containerValues.Bag.Add((StringKey) IDCache.Default.ImbaseKey.Text, (object) imbaseSyncInfo.ImbaseKey);
    }
    return containerValues;
  }

  public bool WriteArticleProperties(SectionEntity articleItem, ContainerValues fileProperties)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    return this.GetArticleAttributeCodec(electricalArticleCache.ArticleType).Formatter.Write(electricalArticleCache.Article, fileProperties);
  }

  public ValueBag DecodeArticleAttributes(SectionEntity articleItem, ContainerValues fileProperties)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    DecodeAttributesOptions decodeOptions = this.driver.MechanicalOperations.Articles.GetDecodeOptions(articleItem);
    DecodeAttributesParams decodeParams = new DecodeAttributesParams(electricalArticleCache.Article, this.GetArticleFileAttributes(articleItem), fileProperties, decodeOptions);
    return this.GetArticleAttributeCodec(electricalArticleCache.ArticleType).Decode(decodeParams);
  }

  public void EncodeArticleAttributes(
    SectionEntity articleItem,
    ICollection<StringKey> attributeKeys,
    ValueBag attributes,
    ContainerValues fileProperties)
  {
    if (articleItem == null)
      throw new ArgumentNullException(nameof (articleItem));
    if (fileProperties == null)
      throw new ArgumentNullException(nameof (fileProperties));
    if (attributes == null)
      throw new ArgumentNullException(nameof (attributes));
    if (attributeKeys == null)
      throw new ArgumentNullException(nameof (attributeKeys));
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    EncodeAttributesOptions encodeOptions = this.driver.MechanicalOperations.Articles.GetEncodeOptions(articleItem);
    EncodeAttributesParams encodeParams = new EncodeAttributesParams(electricalArticleCache.Article, attributeKeys, attributes, fileProperties, encodeOptions)
    {
      ContainerDisplayName = DisplaySection.GetQualifiedName(articleItem)
    };
    this.GetArticleAttributeCodec(electricalArticleCache.ArticleType).Encode(encodeParams);
  }

  public ICollection<StringKey> GetArticleSyncAttributes(SectionEntity articleItem)
  {
    return articleItem != null ? this.GetArticleFileAttributes(articleItem) : throw new ArgumentNullException(nameof (articleItem));
  }

  protected override List<string> OnGetSatelliteFiles(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file)
  {
    return AdditionalFiles.GetProjectAdditionalFiles(ServiceUtils.GetService<FileTypeService>((object) this.driver.Integrator, true), (IADProject) this.GetProject(docItem), file.MasterFile, this.settingsSvc.GetSettings());
  }

  protected override IFileDependenciesHandler OnTryGetFileDependenciesHandler(
    SectionEntity docItem,
    AddInProxy proxy,
    FilesSection file)
  {
    FileTypeService service = ServiceUtils.GetService<FileTypeService>((object) this.driver.Integrator, true);
    return (IFileDependenciesHandler) new ProjectDependenciesBuilder(this.driver, this.driverContext, DocumentHelper.GetProjectDocuments(this.GetProject(docItem).GetDocuments(false), service, proxy), proxy);
  }

  protected override IAttributeCodec documentAttributeCodec => this.apiSvc.ProjectCodec;

  protected override ISynchronizedObjectAttributes documentAttributes
  {
    get => this.settingsSvc.ProjectAttributes;
  }

  protected override IValueBagContainer GetBagContainer(SectionEntity docItem)
  {
    if (docItem == null)
      throw new ArgumentNullException(nameof (docItem));
    return (IValueBagContainer) new ParametersContainer((IParametrable) ApiHelper.GetProject(docItem.Sections.Get<AddInProxy>().AddIn, docItem.Sections.Get<FilesSection>().MasterFile));
  }

  private ADProject GetProject(SectionEntity docItem)
  {
    return new ADProject(ApiHelper.GetProject(docItem.Sections.Get<AddInProxy>().AddIn, docItem.Sections.Get<FilesSection>().MasterFile));
  }

  private ICollection<StringKey> GetAssemblyFileAttributes(SectionEntity articleItem)
  {
    return this.settingsSvc.AssemblyAttributes.GetAttributes(ObjectSection.TryGetObjectType(articleItem), false);
  }

  private ICollection<StringKey> GetPartFileAttributes(SectionEntity articleItem)
  {
    return this.settingsSvc.ComponentAttributes.GetAttributes(ObjectSection.TryGetObjectType(articleItem), false);
  }

  private ICollection<StringKey> GetArticleFileAttributes(SectionEntity articleItem)
  {
    ElectricalArticleCache electricalArticleCache = articleItem.Sections.Get<ElectricalArticleCache>();
    switch (electricalArticleCache.ArticleType)
    {
      case ArticleTypes.Component:
        return this.GetPartFileAttributes(articleItem);
      case ArticleTypes.Assembly:
      case ArticleTypes.VirtualAssembly:
        return this.GetAssemblyFileAttributes(articleItem);
      default:
        throw new Exception($"{electricalArticleCache.ArticleType} not support");
    }
  }

  private IAttributeCodec GetArticleAttributeCodec(ArticleTypes articleType)
  {
    switch (articleType)
    {
      case ArticleTypes.Component:
        return this.apiSvc.ComponentCodec;
      case ArticleTypes.Assembly:
      case ArticleTypes.VirtualAssembly:
        return this.apiSvc.AssemblyCodec;
      default:
        throw new Exception($"{articleType} not support");
    }
  }
}
