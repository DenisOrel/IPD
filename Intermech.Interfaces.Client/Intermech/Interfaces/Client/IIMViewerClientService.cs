// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IIMViewerClientService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс клиентского сервиса интеграции с IMViewer.</summary>
public interface IIMViewerClientService
{
  /// <summary>
  /// Возвращает глобальные настройки интеграции с IMViewer.
  /// Настройки зачитываются при старте приложения и в дальнейшем не изменяются.
  /// </summary>
  IMViewerSystemSettings Settings { get; }

  /// <summary>
  /// Проверяет, может ли у документа указанного типа быть связанный с ним объект IMViewer.
  /// </summary>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <returns>Результат проверки</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  bool CanHaveViewerObject(int documentTypeId);

  /// <summary>
  /// Проверяет, имеется ли у указанного документа связанный с ним объект IMViewer.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <returns>Результат проверки</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  bool HasViewerObject(long documentId, int documentTypeId);

  /// <summary>
  /// Находит для указанного документа связанный с ним объект IMViewer.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <returns>Идентификатор версии объекта IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  long FindViewerObjectId(long documentId, int documentTypeId);

  /// <summary>
  /// Возвращает данные, необходимые для извлечения на локальный диск файлов IMViewer,
  /// связанных с указанным документом.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="versionsRule">Правило подбора версий</param>
  /// <returns>Данные для извлечения на локальный диск файлов IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="versionsRule" /> содержит null</exception>
  List<IMViewerPublishItem> GetViewerDataForOpenFiles(
    long documentId,
    int documentTypeId,
    VersionsRulePackage versionsRule);

  /// <summary>Возвращает имя конфигурации 3D-модели для IMViewer.</summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <param name="savedConfigurationName">Имя конфигурации, сохраненное в базе данных</param>
  /// <returns>Имя конфигурации 3D-модели для IMViewer</returns>
  /// <exception cref="T:System.ArgumentException">параметр <paramref name="documentId" /> содержит некорректное значение; параметр <paramref name="documentTypeId" /> содержит некорректное значение</exception>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="savedConfigurationName" /> содержит null</exception>
  string GetViewerModelConfigurationName(
    long documentId,
    int documentTypeId,
    string savedConfigurationName);
}
