// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IDocumentBuilder
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ControlFlow;
using Intermech.Data.SectionEntities;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Описывает функционал драйвера захвата изменений, который позволяет открывать файлы документов, а также создавать для них обработчики.
/// </summary>
public interface IDocumentBuilder
{
  /// <summary>Позволяет открыть документ.</summary>
  /// <param name="documentItem">Элемент документа в базе данных контекста</param>
  /// <param name="fullPath">Абсолютный путь к файлу документа</param>
  /// <returns>Открытый документ</returns>
  /// <exception cref="T:ArgumentNullException">documentItem || fullPath</exception>
  DocumentFileData OpenDocumentFile(SectionEntity documentItem, string fullPath);

  /// <summary>Добавляет к документу сведения из открытого файла.</summary>
  /// <param name="docItem">Элемент документа в базе данных контекста</param>
  /// <param name="openFileData">Сведения из открытого файла документа</param>
  void AttachDocumentFile(SectionEntity docItem, DocumentFileData openFileData);

  /// <summary>Создает обработчик для документа.</summary>
  /// <param name="docItem">Элемент документа в базе данных контекста</param>
  /// <returns>Обработчик для документа</returns>
  IAction CreateDocumentHandler(SectionEntity docItem);

  /// <summary>
  /// Возвращает true, если документы указанного типа могут быть обработаны драйвером захвата изменений.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <returns>Признак возможности обработки</returns>
  bool IsDocumentTypeSupported(int documentType);

  /// <summary>
  /// Проверяет, могут ли документы указанного типа могут быть обработаны драйвером захвата изменений. Если нет, то метод сбрасывает исключение.
  /// </summary>
  /// <param name="documentType">Идентификатор типа документа</param>
  /// <exception cref="T:Intermech.FaultException">Обработка документов указанного типа не поддерживается</exception>
  void CheckDocumentTypeSupported(int documentType);
}
