// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.DocumentFileData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using System;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует контейнер для хранения данных, прочитанных из файла документа.
/// </summary>
public sealed class DocumentFileData
{
  private readonly string documentFilePath;
  private readonly bool foreignFile;
  private readonly SectionCollection customSections;

  /// <summary>Создает объект.</summary>
  /// <param name="documentFilePath">Полный путь к файлу документа</param>
  /// <exception cref="T:System.ArgumentException">Ошибка в аргументах конструктора</exception>
  public DocumentFileData(string documentFilePath)
    : this(documentFilePath, false)
  {
  }

  /// <summary>Создает объект.</summary>
  /// <param name="documentFilePath">Полный путь к файлу документа</param>
  /// <param name="foreignFile">Признак, что файл создан в другом приложении</param>
  /// <exception cref="T:System.ArgumentException">Ошибка в аргументах конструктора</exception>
  public DocumentFileData(string documentFilePath, bool foreignFile)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentException();
    this.documentFilePath = Path.IsPathRooted(documentFilePath) ? documentFilePath : throw new ArgumentException();
    this.foreignFile = foreignFile;
    this.customSections = new SectionCollection();
  }

  /// <summary>Создает объект.</summary>
  /// <param name="documentFilePath">Полный путь к файлу документа</param>
  /// <param name="foreignFile">Признак, что файл создан в другом приложении</param>
  /// <param name="customSections">Произвольные данные, относящиеся к файлу</param>
  /// <exception cref="T:System.ArgumentException">Ошибка в аргументах конструктора</exception>
  public DocumentFileData(
    string documentFilePath,
    bool foreignFile,
    SectionCollection customSections)
  {
    if (string.IsNullOrEmpty(documentFilePath))
      throw new ArgumentException();
    if (!Path.IsPathRooted(documentFilePath))
      throw new ArgumentException();
    if (customSections == null)
      throw new ArgumentNullException(nameof (customSections));
    this.documentFilePath = documentFilePath;
    this.foreignFile = foreignFile;
    this.customSections = customSections;
  }

  /// <summary>Создает объект.</summary>
  /// <param name="documentItem">Элемент рабочего контекста, представляющий документ</param>
  public DocumentFileData(SectionEntity documentItem)
    : this(FilesSection.GetMasterFile(documentItem), false, documentItem.Sections)
  {
  }

  /// <summary>Возвращает полный путь к файлу документа.</summary>
  public string DocumentFilePath => this.documentFilePath;

  /// <summary>
  /// Возвращает признак, что файл создан в другом приложении.
  /// </summary>
  public bool ForeignFile => this.foreignFile;

  /// <summary>
  /// Возвращает контейнер с произвольными данными, относящимися к файлу.
  /// </summary>
  public SectionCollection CustomSections => this.customSections;
}
