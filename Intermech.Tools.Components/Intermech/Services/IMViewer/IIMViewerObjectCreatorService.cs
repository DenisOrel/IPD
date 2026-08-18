// Decompiled with JetBrains decompiler
// Type: Intermech.Services.IMViewer.IIMViewerObjectCreatorService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Services.IMViewer;

/// <summary>
/// Интерфейс внутреннего сервиса для создания/обновления объектов IMViewer,
/// используя API CAD-интерфейса и CADMECH.
/// </summary>
public interface IIMViewerObjectCreatorService : IIMViewerClientService
{
  /// <summary>
  /// Возвращает путь к выделенной папке для генерации IMV-файлов.
  /// </summary>
  string ConverterBaseDirectory { get; }

  /// <summary>
  /// Создает или обновляет объект IMViewer, непосредственно связанный с указанным документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <param name="preOpenDocumentsMode">Режим предварительного открытия документа в CAD-системе</param>
  /// <returns>Список ошибок обновления объектов IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null; параметр <paramref name="versionsRule" /> содержит null</exception>
  IList<ErrorInfo> CreateOrUpdateViewerObject(
    long documentId,
    int documentTypeId,
    VersionsRulePackage versionsRule,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode);

  /// <summary>
  /// Создает или рекурсивно обновляет все объекты IMViewer, связанные с указанным документом.
  /// Состав документа раскручивается по связям типа "Состав документации".
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <param name="preOpenDocumentsMode">Режим предварительного открытия документа в CAD-системе</param>
  /// <returns>Список ошибок обновления объектов IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null; параметр <paramref name="versionsRule" /> содержит null</exception>
  IList<ErrorInfo> CreateOrUpdateViewerObjectsRecursive(
    long documentId,
    int documentTypeId,
    VersionsRulePackage versionsRule,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode);

  /// <summary>
  /// Создает пустой объект IMViewer, непосредственно связанный с указанным документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="createBlankObject">Признак создания заготовки объекта</param>
  /// <returns>Идентификатор созданного объекта IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.Exception">При создании объекта IMViewer произошла ошибка</exception>
  long CreateEmptyViewerObject(long documentId, int documentTypeId, bool createBlankObject);

  /// <summary>
  /// Изменяет у объекта IMViewer статус с "актуальный" на "устаревший".
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  void MakeViewerObjectOutdated(long documentId, int documentTypeId);

  /// <summary>
  /// Вычисляет значение заголовка для объекта IMViewer, используя значения атрибутов исходного документа.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="documentAttributeBag">Контейнер атрибутов исходного документа</param>
  /// <param name="identityAttributeNames">Коллекция имен идентифицирующих атрибутов документа</param>
  /// <returns>Заголовок для объекта IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="documentAttributeBag" /> содержит null; параметр <paramref name="identityAttributeNames" /> содержит null</exception>
  string CreateViewerObjectCaption(
    long documentId,
    int documentTypeId,
    ValueBag documentAttributeBag,
    IEnumerable<StringKey> identityAttributeNames);

  /// <summary>
  /// Создает или обновляет файл объекта IMViewer, используя путь к файлу исходного документа.
  /// </summary>
  /// <param name="documentPath">Путь к файлу исходного документа</param>
  /// <param name="documentBaseDirectory">Путь к базовому каталогу для вычисления относительного пути к файлу документа</param>
  /// <param name="cadSystem">Объект CAD-системы</param>
  /// <param name="preOpenDocumentsMode">Режим предварительного открытия документа в CAD-системе</param>
  /// <returns>Абсолютный путь к файлу IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentPath" /> содержит некорректное значение; параметр <paramref name="documentBaseDirectory" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="cadSystem" /> содержит null; параметр <paramref name="documentPath" /> содержит null; параметр <paramref name="documentBaseDirectory" /> содержит null</exception>
  string CreateViewerFile(
    string documentPath,
    string documentBaseDirectory,
    CADSystemProxy cadSystem,
    bool preOpenDocumentsMode);
}
