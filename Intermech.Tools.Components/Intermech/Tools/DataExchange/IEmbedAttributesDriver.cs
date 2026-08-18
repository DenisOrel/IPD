// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.IEmbedAttributesDriver
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>Драйвер для работы с атрибутами CAD-элемента</summary>
public interface IEmbedAttributesDriver
{
  /// <summary>
  /// Подготавливает драйвер к обработке нового объекта. Этот метод следует использовать для контроля установки свойств объекта, а также
  /// создания вспомогательных объектов и сервисов.
  /// </summary>
  /// <param name="documentId">Идентификатор версии документа</param>
  /// <param name="documentTypeId">Идентификатор типа документа</param>
  /// <exception cref="T:Intermech.Tools.Integrators.DataExchange.DataExchangeConfigurationException">Одно из свойств объекта не инициализировано должным образом</exception>
  void BeginAction(long documentId, int documentTypeId);

  /// <summary>
  /// Очищает драйвер в конце обработки объекта. Этот метод следует использовать для освобождения ссылок на вспомогательные объекты и сервисы.
  /// Метод не должен сбрасывать исключений, так как он может являться частью обработчика уже возникшего исключения.
  /// </summary>
  void EndAction();

  /// <summary>
  /// Возвращает признак, что метод BeginAction был выполнен без ошибок.
  /// </summary>
  bool Active { get; }

  ICollection<StringKey> GetEmbeddableAttributes(long documentId, int documentType);

  string FindMasterFile(long documentId);

  bool HasAncillaryDocumentFiles(long documentId);

  ICollection<string> GetAncillaryDocumentFiles(long documentId);

  bool EmbedAttributes(
    long documentId,
    int documentType,
    string documentFilePath,
    ValueBag attributes);

  void FlushChanges();
}
