// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ICADInterfaceService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Interfaces.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис доступа к API CAD-системы, указанной в настройках интегратора.
/// </summary>
public interface ICADInterfaceService : 
  IApplicationApiService,
  IExternalApiService,
  IIntegratorService,
  IDocumentApiService
{
  /// <summary>
  /// Возвращает кодек атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов документа</returns>
  IAttributeCodec GetDocumentCodec(CADDocumentProxy document);

  /// <summary>
  /// Возвращает контейнер атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Контейнер с атрибутами документа</returns>
  IValueBagContainer GetDocumentAttributeContainer(CADDocumentProxy document);

  /// <summary>
  /// Возвращает кодек атрибутов изделия для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов изделия</returns>
  IAttributeCodec GetArticleCodec(CADDocumentProxy document);

  /// <summary>
  /// Возвращает контейнер атрибутов изделия для указанной конфигурации документа CAD-системы.
  /// </summary>
  /// <param name="configuration">Открытая конфигурация документа CAD-системы</param>
  /// <returns>Контейнер с атрибутами изделия</returns>
  IValueBagContainer GetArticleAttributeContainer(ModelConfigurationProxy configuration);

  /// <summary>
  /// Определяет способ обработки изделия, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="articleInfo">Сведения о конфигурации документа</param>
  /// <returns>Разновидность изделия и способ его обработки</returns>
  ArticleProcessingMethod GetArticleProcessingMethod(ArticleProcessingParams articleInfo);

  /// <summary>
  /// Возвращает имя конфигурации в 3D-модели, соответствующей изделию.
  /// </summary>
  /// <param name="documentFile">Имя файла 3D-модели</param>
  /// <param name="savedConfigurationName">Имя конфигурации, сохраненное в базе данных</param>
  /// <returns>Имя конфигурации в 3D-модели</returns>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentFile" /> содержит null; параметр <paramref name="savedConfigurationName" /> содержит null</exception>
  string GetArticleRawConfigurationName(string documentFile, string savedConfigurationName);

  /// <summary>
  /// Создает алгоритм для поиска изделия в базе IPS, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="processingMethod">Разновидность изделия и способ его обработки</param>
  /// <param name="dataProvider">Провайдер данных об изделии, которые доступны для поиска изделия</param>
  /// <returns>Алгоритм поиска изделия в базе IPS</returns>
  IObjectLocator CreateArticleLocator(
    ArticleProcessingMethod processingMethod,
    ArticleLocatorDataProvider dataProvider);

  /// <summary>
  /// Находит изделие в базе IPS, анализируя соответствующую этому изделию конфигурацию документа.
  /// </summary>
  /// <param name="configuration">Конфигурация документа</param>
  /// <param name="documentId">Идентификатор версии документа, которому принадлежим конфигурация; может быть не задан</param>
  /// <returns>Сведения о найденном изделии или null</returns>
  /// <exception cref="T:ArgumentNullException">configuration</exception>
  ObjectLocatorResult FindArticle(ModelConfigurationProxy configuration, long documentId);
}
