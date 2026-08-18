// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Cadmech.StandardPartImporter
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Files;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Data;
using Intermech.IO;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Tools.StandardParts.Cadmech;

internal sealed class StandardPartImporter : ImportContextTask
{
  private IFileVault fileVault;
  private ArticleLocatorBuilder builder;
  private IStandardPartLibraryService libraryService;
  private StandardLibraryMode libraryMode;
  private string libraryFolderName;
  private string libraryPath;
  private ICADSettingsService integratorSettingsService;
  private ICollection<StringKey> allArticleAttributes;
  private DecodeAttributesOptions articleAttributesDecodeOptions;
  private EncodeAttributesOptions articleAttributesEncodeOptions;
  private ICADInterfaceService applicationApiService;

  public StandardPartImporter(IFileVault fileVault)
  {
    this.fileVault = fileVault != null ? fileVault : throw new ArgumentNullException(nameof (fileVault));
    this.builder = new ArticleLocatorBuilder();
  }

  protected override void DoInitializeContextData()
  {
    base.DoInitializeContextData();
    this.libraryService = ServiceUtils.GetService<IStandardPartLibraryService>((object) this.ImportContext.Integrator, true);
    this.libraryMode = StandardLibraryServices.GetMode((IServiceProvider) this.ImportContext.Integrator);
    this.libraryFolderName = StandardLibraryServices.GetModelFolderName((IServiceProvider) this.ImportContext.Integrator);
    this.libraryPath = StandardLibraryServices.GetModelFolderPath((IServiceProvider) this.ImportContext.Integrator);
    this.integratorSettingsService = ServiceUtils.GetService<ICADSettingsService>((object) this.ImportContext.Integrator, true);
    this.allArticleAttributes = this.integratorSettingsService.SynchronizedArticleAttributes.GetAttributes();
    this.articleAttributesDecodeOptions = DocumentAttributesOptions.GetDecodeOptions(this.ImportContext.StandardModelType.Id);
    this.articleAttributesEncodeOptions = DocumentAttributesOptions.GetEncodeOptions(this.ImportContext.StandardModelType.Id);
    this.articleAttributesEncodeOptions.ReportErrorsOnly = true;
    this.applicationApiService = ServiceUtils.GetService<ICADInterfaceService>((object) this.ImportContext.Integrator, true);
  }

  protected override void DoCleanupContextData()
  {
    base.DoCleanupContextData();
    this.libraryService = (IStandardPartLibraryService) null;
    this.libraryFolderName = (string) null;
    this.libraryPath = (string) null;
    this.integratorSettingsService = (ICADSettingsService) null;
    this.allArticleAttributes = (ICollection<StringKey>) null;
    this.articleAttributesDecodeOptions = (DecodeAttributesOptions) null;
    this.articleAttributesEncodeOptions = (EncodeAttributesOptions) null;
    this.applicationApiService = (ICADInterfaceService) null;
  }

  public bool CanOpenModel(string modelFullPath)
  {
    if (modelFullPath == null)
      throw new ArgumentNullException(nameof (modelFullPath));
    this.RequireImportContext();
    return Array.Exists<string>(this.ImportContext.PartModelExtensions, (Predicate<string>) (ext => PathUtils.IsSamePath(ext, Path.GetExtension(modelFullPath))));
  }

  public ImportedStandardPart ImportModel(string modelFullPath)
  {
    if (modelFullPath == null)
      throw new ArgumentNullException(nameof (modelFullPath));
    this.RequireImportContext();
    string str = Path.Combine(this.libraryFolderName, Path.GetFileName(modelFullPath));
    if (StandardLibraryServices.FindModel((IServiceProvider) this.ImportContext.Integrator, str, this.ImportContext.VersionsRule.OwnerId) != 0L)
      return (ImportedStandardPart) null;
    StandardPartImporter.StandardPartArticlesInfo articlesInfo = this.AnalizeModel(modelFullPath);
    if (articlesInfo.Articles.Count <= 0 && this.libraryMode != StandardLibraryMode.EmbeddedStandardSizes)
      return (ImportedStandardPart) null;
    long model1 = StandardLibraryServices.CreateModel((IServiceProvider) this.ImportContext.Integrator, this.ImportContext.StandardModelType.Id, this.GetModelDesignation(articlesInfo), this.GetModelName(str, articlesInfo), str, modelFullPath);
    this.ImportContext.NotifyQueue.QueueEvent((NotificationEventArgs) new CreatedExternallyEventArgs("ObjectsCreated", model1));
    using (new SessionKeeper())
    {
      foreach (StandardPartImporter.StandardPartArticleInfo article in articlesInfo.Articles)
      {
        Tuple<long, bool> model2 = StandardLibraryServices.LinkPartToModel(article.ObjectId, model1);
        if (model2.Item2)
          this.ImportContext.NotifyQueue.QueueEvent((NotificationEventArgs) new DBRelationsEventArgs("RelationsCreated", model2.Item1));
      }
    }
    if (PathUtils.IsPlacedIn(Path.GetDirectoryName(modelFullPath), this.libraryPath))
      this.fileVault.WorkArea.Attach(model1);
    return new ImportedStandardPart(model1, (IList<long>) articlesInfo.Articles.ConvertAll<long>((Converter<StandardPartImporter.StandardPartArticleInfo, long>) (item => item.ObjectId)));
  }

  private string GetModelDesignation(
    StandardPartImporter.StandardPartArticlesInfo articlesInfo)
  {
    string modelDesignation = string.Empty;
    if (articlesInfo.Articles.Count == 1 && this.libraryMode == StandardLibraryMode.SeparateStandardSizes)
      modelDesignation = articlesInfo.Articles[0].FileAttributes.Read<string>((StringKey) IDCache.Default.Designation.Text, string.Empty);
    return modelDesignation;
  }

  private string GetModelName(
    string modelRelativePath,
    StandardPartImporter.StandardPartArticlesInfo articlesInfo)
  {
    string modelName = modelRelativePath;
    if (articlesInfo.Articles.Count == 1 && this.libraryMode == StandardLibraryMode.SeparateStandardSizes)
    {
      string str = articlesInfo.Articles[0].FileAttributes.Read<string>((StringKey) IDCache.Default.Name.Text, (string) null);
      if (!string.IsNullOrEmpty(str))
        modelName = $"{str} ({modelRelativePath})";
    }
    return modelName;
  }

  private StandardPartImporter.StandardPartArticlesInfo AnalizeModel(string modelFullPath)
  {
    using (CADApiSession cadApiSession = new CADApiSession((IApplicationApiService) this.applicationApiService))
    {
      bool readOnlyAttribute = FileUtils.GetReadOnlyAttribute(modelFullPath);
      if (readOnlyAttribute)
        FileUtils.SetReadOnlyAttribute(modelFullPath, false);
      CADDocumentProxy document = cadApiSession.Application.OpenDocument(modelFullPath, false);
      try
      {
        IAttributeCodec articleCodec = this.applicationApiService.GetArticleCodec(document);
        ICollection<ModelConfigurationProxy> allConfigurations = document.GetAllConfigurations();
        StandardPartImporter.StandardPartArticlesInfo partArticlesInfo = new StandardPartImporter.StandardPartArticlesInfo(allConfigurations.Count);
        foreach (ModelConfigurationProxy configuration in (IEnumerable<ModelConfigurationProxy>) allConfigurations)
        {
          StandardPartImporter.ModelConfigurationData modelConfigurationData = new StandardPartImporter.ModelConfigurationData(this.applicationApiService, configuration);
          modelConfigurationData.AttachAttributeCodec(articleCodec, this.articleAttributesDecodeOptions, this.articleAttributesEncodeOptions);
          modelConfigurationData.ReadParametersAndAttributes(this.allArticleAttributes);
          StandardPartImporter.StandardPartArticleInfo standardPartArticleInfo = (StandardPartImporter.StandardPartArticleInfo) null;
          if (this.IsCadmechStandardPart(modelConfigurationData.ArticleAttributes))
            standardPartArticleInfo = this.TryFindOrCreateCadmechStandardPart(modelConfigurationData);
          else if (this.IsCustomStandardPart(modelConfigurationData.ArticleAttributes))
            standardPartArticleInfo = this.TryFindOrCreateCustomStandardPart(modelConfigurationData);
          if (standardPartArticleInfo != null)
            partArticlesInfo.Articles.Add(standardPartArticleInfo);
        }
        return partArticlesInfo;
      }
      finally
      {
        if (document.Modified)
          document.Save();
        document.Close();
        if (readOnlyAttribute)
          FileUtils.SetReadOnlyAttribute(modelFullPath, true);
      }
    }
  }

  private bool IsCadmechStandardPart(ValueBag articleAttributes)
  {
    string str1 = articleAttributes.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, (string) null);
    string str2 = articleAttributes.Read<string>((StringKey) IDCache.Default.Name.Text, (string) null);
    return !string.IsNullOrEmpty(str1) && !string.IsNullOrEmpty(str2);
  }

  private StandardPartImporter.StandardPartArticleInfo TryFindOrCreateCadmechStandardPart(
    StandardPartImporter.ModelConfigurationData modelConfigurationData)
  {
    this.builder.DataProvider = (ArticleLocatorDataProvider) new CADArticleLocatorDataProvider(modelConfigurationData.ArticleAttributes);
    ObjectLocatorResult objectLocatorResult = this.builder.CreateLocator(ArticleProcessingMethod.ImbaseObject).LocateObject();
    if (objectLocatorResult == null)
    {
      Tuple<long, int, string> createImbaseObject = ImbaseHelper.FindOrCreateImbaseObject(modelConfigurationData.ArticleAttributes);
      if (createImbaseObject != null)
      {
        objectLocatorResult = new ObjectLocatorResult(createImbaseObject.Item1, createImbaseObject.Item2);
        modelConfigurationData.ArticleAttributes.Update((StringKey) IDCache.Default.ImbaseKey.Text, (object) createImbaseObject.Item3);
        modelConfigurationData.ArticleAttributes.SetFlag((StringKey) IDCache.Default.ImbaseKey.Text, NamedFlags.ThrowSetException);
        modelConfigurationData.WriteChangesToModelConfiguration();
        this.ImportContext.NotifyQueue.QueueEvent((NotificationEventArgs) new CreatedExternallyEventArgs("ObjectsCreated", objectLocatorResult.ObjectId));
      }
    }
    return objectLocatorResult != null ? new StandardPartImporter.StandardPartArticleInfo(objectLocatorResult.ObjectId, modelConfigurationData.ArticleAttributes) : (StandardPartImporter.StandardPartArticleInfo) null;
  }

  private bool IsCustomStandardPart(ValueBag articleAttributes)
  {
    return this.libraryService.CanImportCustomParts && this.libraryService.IsCustomPartArticle(articleAttributes);
  }

  private StandardPartImporter.StandardPartArticleInfo TryFindOrCreateCustomStandardPart(
    StandardPartImporter.ModelConfigurationData modelConfigurationData)
  {
    this.libraryService.PrepareCustomPartArticleToImport(modelConfigurationData.ArticleAttributes);
    modelConfigurationData.WriteChangesToModelConfiguration();
    this.builder.DataProvider = (ArticleLocatorDataProvider) new CADArticleLocatorDataProvider(modelConfigurationData.ArticleAttributes);
    ObjectLocatorResult objectLocatorResult = this.builder.CreateLocator(ArticleProcessingMethod.NormalObject).LocateObject();
    if (objectLocatorResult == null)
    {
      string str1 = modelConfigurationData.ArticleAttributes.Read<string>((StringKey) IDCache.Default.Designation.Text, (string) null);
      string str2 = modelConfigurationData.ArticleAttributes.Read<string>((StringKey) IDCache.Default.Name.Text, (string) null);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(IDCache.Default.StandardArticles.Id).Create();
        if (!string.IsNullOrEmpty(str1))
          dbObject.Attributes.AddAttribute(IDCache.Default.Designation.Id, false, new object[1]
          {
            (object) str1
          });
        if (!string.IsNullOrEmpty(str2))
          dbObject.Attributes.AddAttribute(IDCache.Default.Name.Id, false, new object[1]
          {
            (object) str2
          });
        dbObject.CommitCreation(true, true);
        objectLocatorResult = new ObjectLocatorResult(dbObject.ObjectID, IDCache.Default.StandardArticles.Id);
      }
    }
    if (objectLocatorResult == null)
      return (StandardPartImporter.StandardPartArticleInfo) null;
    return new StandardPartImporter.StandardPartArticleInfo(objectLocatorResult.ObjectId, modelConfigurationData.ArticleAttributes)
    {
      IsCustomPart = true
    };
  }

  private sealed class ModelConfigurationData
  {
    public ModelConfigurationData(
      ICADInterfaceService cadService,
      ModelConfigurationProxy configuration)
    {
      this.CADService = cadService;
      this.Configuration = configuration;
    }

    public void AttachAttributeCodec(
      IAttributeCodec codec,
      DecodeAttributesOptions decodeOptions,
      EncodeAttributesOptions encodeOptions)
    {
      this.AttributeCodec = codec;
      this.DecodeOptions = decodeOptions;
      this.EncodeOptions = encodeOptions;
    }

    public void ReadParametersAndAttributes(ICollection<StringKey> attributeKeys)
    {
      this.CheckAttributeCodecIsAttached();
      this.ParametersContainer = this.CADService.GetArticleAttributeContainer(this.Configuration);
      this.Parameters = this.AttributeCodec.ReadFileProperties(this.ParametersContainer, attributeKeys);
      this.ArticleAttributes = this.AttributeCodec.Decode(new DecodeAttributesParams(this.ParametersContainer, attributeKeys, this.Parameters, this.DecodeOptions));
    }

    public void WriteChangesToModelConfiguration()
    {
      this.CheckAttributeCodecIsAttached();
      this.CheckAttributesIsRead();
      if (!this.ArticleAttributes.HasChanges)
        return;
      this.AttributeCodec.Encode(new EncodeAttributesParams(this.ParametersContainer, (ICollection<StringKey>) this.ArticleAttributes.GetChangedItemsKeys(), this.ArticleAttributes, this.Parameters, this.EncodeOptions)
      {
        ContainerDisplayName = Path.GetFileName(this.Configuration.Document.FullName)
      });
      this.AttributeCodec.Formatter.Write(this.ParametersContainer, this.Parameters);
    }

    private void CheckAttributeCodecIsAttached()
    {
      if (this.AttributeCodec == null || this.DecodeOptions == null || this.EncodeOptions == null)
        throw new InvalidOperationException("AttributeCodec object is not attached to a model configuration.");
    }

    private void CheckAttributesIsRead()
    {
      if (this.ParametersContainer == null || this.Parameters == null || this.ArticleAttributes == null)
        throw new InvalidOperationException("Model configuration parameters is not read from a model.");
    }

    private ICADInterfaceService CADService { get; set; }

    public ModelConfigurationProxy Configuration { get; private set; }

    private IAttributeCodec AttributeCodec { get; set; }

    private DecodeAttributesOptions DecodeOptions { get; set; }

    private EncodeAttributesOptions EncodeOptions { get; set; }

    private IValueBagContainer ParametersContainer { get; set; }

    public ContainerValues Parameters { get; private set; }

    public ValueBag ArticleAttributes { get; private set; }
  }

  private sealed class StandardPartArticleInfo
  {
    public StandardPartArticleInfo(long objectId, ValueBag fileAttributes)
    {
      this.ObjectId = objectId;
      this.FileAttributes = fileAttributes;
    }

    public long ObjectId { get; private set; }

    public ValueBag FileAttributes { get; private set; }

    public bool IsCustomPart { get; set; }
  }

  private sealed class StandardPartArticlesInfo
  {
    public StandardPartArticlesInfo(int capacity)
    {
      this.Articles = capacity >= 0 ? new List<StandardPartImporter.StandardPartArticleInfo>(capacity) : throw new ArgumentOutOfRangeException(nameof (capacity));
    }

    public List<StandardPartImporter.StandardPartArticleInfo> Articles { get; private set; }
  }
}
