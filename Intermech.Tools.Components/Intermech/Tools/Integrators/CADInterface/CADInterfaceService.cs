// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADInterfaceService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Interfaces.Data;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop;
using Intermech.Tools.Components.Properties;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public class CADInterfaceService : 
  CADInterfaceServiceBase,
  ICADInterfaceService,
  IApplicationApiService,
  IExternalApiService,
  IIntegratorService,
  IDocumentApiService
{
  private ICADSettingsService settingsService;
  private IAttributeCodec genDocCodec;
  private IAttributeCodec modelDocumentCodec;
  private IAttributeCodec modelArticleCodec;
  private ArticleLocatorBuilder articleLocatorBuilder;

  /// <summary>Создает сервис.</summary>
  /// <param name="owner">Владелец сервиса</param>
  /// <param name="applicationName">Название приложения</param>
  /// <param name="cadInterfaceProvider">Провайдер типа COM-объекта CAD-интерфейса</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект провайдера типа COM-объекта не может быть null</exception>
  public CADInterfaceService(
    IIntegrator owner,
    string applicationName,
    ComObjectProvider cadInterfaceProvider)
    : base(owner, applicationName, cadInterfaceProvider)
  {
    this.articleLocatorBuilder = new ArticleLocatorBuilder();
  }

  /// <summary>
  /// Возвращает или задает ссылку на сервис настроек интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public ICADSettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsService;
    }
    set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsService = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
    this.genDocCodec = (IAttributeCodec) new DocumentAttributesCodec((IValueBagFormatter) new CADInterfaceFormatter());
    this.modelDocumentCodec = (IAttributeCodec) new ModelDocumentCodec();
    this.modelArticleCodec = (IAttributeCodec) new ModelArticleCodec((IServiceProvider) this.Integrator);
  }

  /// <summary>
  /// Возвращает кодек атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов документа</returns>
  protected override IAttributeCodec DoGetDocumentCodec(CADDocumentProxy document)
  {
    return !document.HasConfigurations ? this.genDocCodec : this.modelDocumentCodec;
  }

  /// <summary>
  /// Возвращает кодек атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов документа</returns>
  public IAttributeCodec GetDocumentCodec(CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.RequireReadyState();
    this.CheckApiSessionOpen();
    return this.DoGetDocumentCodec(document);
  }

  /// <summary>
  /// Возвращает контейнер атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Контейнер с атрибутами документа</returns>
  public IValueBagContainer GetDocumentAttributeContainer(CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.RequireReadyState();
    this.CheckApiSessionOpen();
    return (IValueBagContainer) CADInterfaceAdapters.AsValueBagContainer(document);
  }

  /// <summary>
  /// Возвращает кодек атрибутов изделия для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов изделия</returns>
  public IAttributeCodec GetArticleCodec(CADDocumentProxy document)
  {
    if (document == null)
      throw new ArgumentNullException(nameof (document));
    this.RequireReadyState();
    this.CheckApiSessionOpen();
    return this.DoGetArticleCodec(document);
  }

  /// <summary>
  /// Возвращает кодек атрибутов изделия для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов изделия</returns>
  protected virtual IAttributeCodec DoGetArticleCodec(CADDocumentProxy document)
  {
    if (document.HasConfigurations)
      return this.modelArticleCodec;
    throw new NotSupportedException("A cad document must have configurations.");
  }

  /// <summary>
  /// Возвращает контейнер атрибутов изделия для указанной конфигурации документа CAD-системы.
  /// </summary>
  /// <param name="configuration">Открытая конфигурация документа CAD-системы</param>
  /// <returns>Контейнер с атрибутами изделия</returns>
  public IValueBagContainer GetArticleAttributeContainer(ModelConfigurationProxy configuration)
  {
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    this.RequireReadyState();
    return CADInterfaceAdapters.AsValueBagContainer(configuration);
  }

  /// <summary>
  /// Определяет способ обработки изделия, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="articleInfo">Сведения о конфигурации документа</param>
  /// <returns>Разновидность изделия и способ его обработки</returns>
  public ArticleProcessingMethod GetArticleProcessingMethod(ArticleProcessingParams articleInfo)
  {
    if (articleInfo == null)
      throw new ArgumentNullException(nameof (articleInfo));
    this.RequireReadyState();
    return this.DoGetArticleProcessingMethod(articleInfo);
  }

  /// <summary>
  /// Возвращает имя конфигурации в 3D-модели, соответствующей изделию.
  /// </summary>
  /// <param name="documentFile">Имя файла 3D-модели</param>
  /// <param name="savedConfigurationName">Имя конфигурации, сохраненное в базе данных</param>
  /// <returns>Имя конфигурации в 3D-модели</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentFile" /> содержит null; параметр <paramref name="savedConfigurationName" /> содержит null</exception>
  public string GetArticleRawConfigurationName(string documentFile, string savedConfigurationName)
  {
    if (savedConfigurationName == null)
      throw new ArgumentNullException(nameof (savedConfigurationName));
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
      return this.GetOrCreateProxyBuilder().ConfigurationNameMangler.ToRawName(documentFile, savedConfigurationName);
  }

  /// <summary>
  /// Определяет способ обработки изделия, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="articleInfo">Сведения о конфигурации документа</param>
  /// <returns>Разновидность изделия и способ его обработки</returns>
  protected virtual ArticleProcessingMethod DoGetArticleProcessingMethod(
    ArticleProcessingParams articleInfo)
  {
    if (!string.IsNullOrEmpty(articleInfo.ConfigurationAttributes.Read<string>((StringKey) IDCache.Default.ImbaseKey.Text, (string) null)))
      return ArticleProcessingMethod.ImbaseObject;
    int objectType = CADInterfaceService.TryDetectArticleType(articleInfo);
    return objectType != -1 && PDMHelper.IsMaterial(objectType) ? ArticleProcessingMethod.MinorMaterial : ArticleProcessingMethod.NormalObject;
  }

  private static int TryDetectArticleType(ArticleProcessingParams articleInfo)
  {
    ValueRecord valueRecord = articleInfo.ConfigurationAttributes.Find((StringKey) CADDocumentResources.EMB_ArticleTypeAttribute);
    if (valueRecord != null && valueRecord.DataType == typeof (string))
    {
      string anObjectTypeName = valueRecord.Read<string>(string.Empty);
      if (!string.IsNullOrEmpty(anObjectTypeName))
      {
        using (SessionKeeper sessionKeeper = new SessionKeeper())
        {
          IDBObjectType objectType = sessionKeeper.Session.GetObjectType(anObjectTypeName, false);
          if (objectType != null)
            return objectType.ObjectType;
        }
      }
    }
    return -1;
  }

  /// <summary>
  /// Создает алгоритм для поиска изделия в базе IPS, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="processingMethod">Разновидность изделия и способ его обработки</param>
  /// <param name="dataProvider">Провайдер данных об изделии, которые доступны для поиска изделия</param>
  /// <returns>Алгоритм поиска изделия в базе IPS</returns>
  public IObjectLocator CreateArticleLocator(
    ArticleProcessingMethod processingMethod,
    ArticleLocatorDataProvider dataProvider)
  {
    if (dataProvider == null)
      throw new ArgumentNullException(nameof (dataProvider));
    this.RequireReadyState();
    return this.DoCreateArticleLocator(processingMethod, dataProvider);
  }

  /// <summary>
  /// Создает алгоритм для поиска изделия в базе IPS, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="processingMethod">Разновидность изделия и способ его обработки</param>
  /// <param name="dataProvider">Провайдер данных об изделии, которые доступны для поиска изделия</param>
  /// <returns>Алгоритм поиска изделия в базе IPS</returns>
  protected virtual IObjectLocator DoCreateArticleLocator(
    ArticleProcessingMethod processingMethod,
    ArticleLocatorDataProvider dataProvider)
  {
    lock (this.articleLocatorBuilder)
    {
      this.articleLocatorBuilder.DataProvider = dataProvider;
      return this.articleLocatorBuilder.CreateLocator(processingMethod);
    }
  }

  /// <summary>
  /// Находит изделие в базе IPS, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="configuration">Конфигурация документа</param>
  /// <param name="documentIdOrNull">Идентификатор версии документа, которому принадлежим конфигурация. Может быть не задан (например, документ может быть виртуальным, либо конфигурация описывает материал).</param>
  /// <returns>Сведения о найденном изделии или null</returns>
  /// <exception cref="T:ArgumentNullException">configuration</exception>
  public ObjectLocatorResult FindArticle(
    ModelConfigurationProxy configuration,
    long documentIdOrNull)
  {
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    this.RequireReadyState();
    int documentType = documentIdOrNull != 0L ? DBHelper.GetObjectType(documentIdOrNull) : -1;
    IAttributeCodec articleCodec = this.GetArticleCodec(configuration.Document);
    ICollection<StringKey> attributes = this.settingsService.SynchronizedArticleAttributes.GetAttributes();
    DecodeAttributesOptions decodeOptions = DocumentAttributesOptions.GetDecodeOptions(documentType);
    IValueBagContainer attributeContainer = this.GetArticleAttributeContainer(configuration);
    ICollection<StringKey> attributeKeys = attributes;
    DecodeAttributesOptions options = decodeOptions;
    ContainerValues containerValues = articleCodec.ReadAttributes(attributeContainer, attributeKeys, options);
    ArticleProcessingParams articleInfo = new ArticleProcessingParams((string) configuration.Name, containerValues.Bag);
    if (documentType != -1)
      articleInfo.SetDocumentInfo(documentType);
    return this.CreateArticleLocator(this.GetArticleProcessingMethod(articleInfo), (ArticleLocatorDataProvider) new CADArticleLocatorDataProvider(containerValues.Bag, (string) configuration.Name, documentIdOrNull)).LocateObject();
  }
}
