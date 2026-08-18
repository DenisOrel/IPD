// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.MasterDocumentsMapping
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Реализует карту отображения между документами производных конфигураций и мастер-конфигурации. Позволяет для любого набора документов определить
/// соответствующий им набор мастер-документов и наоборот.
/// </summary>
public class MasterDocumentsMapping
{
  private readonly CADSystemProxy cadSystem;
  private PathDictionary<CADDocumentProxy> masterDocsMap;
  private PathDictionary<string> directMap;
  private PathDictionary<PathCollection> reverseMap;

  /// <summary>Создает карту.</summary>
  /// <param name="appProxy">Ссылка на объект CAD-системы</param>
  /// <param name="capacity">Начальная емкость карты</param>
  public MasterDocumentsMapping(CADSystemProxy appProxy, int capacity)
  {
    if (appProxy == null)
      throw new ArgumentNullException(nameof (appProxy));
    if (capacity < 0)
      throw new ArgumentOutOfRangeException(nameof (capacity));
    this.cadSystem = appProxy;
    this.masterDocsMap = new PathDictionary<CADDocumentProxy>(capacity);
    this.directMap = new PathDictionary<string>(capacity);
    this.reverseMap = new PathDictionary<PathCollection>(capacity);
  }

  /// <summary>Создает карту.</summary>
  /// <param name="appProxy">Ссылка на объект CAD-системы</param>
  public MasterDocumentsMapping(CADSystemProxy appProxy)
    : this(appProxy, 4)
  {
  }

  /// <summary>Очищает карту отображения.</summary>
  public void Clear()
  {
    this.directMap.Clear();
    this.masterDocsMap.Clear();
    this.reverseMap.Clear();
  }

  /// <summary>
  /// Добавляет в карту коллекцию путей к документами конфигураций.
  /// </summary>
  /// <param name="files">Коллекция абсолютных путей к документам конфигураций</param>
  public void AddSources(ICollection<string> files)
  {
    if (files == null)
      throw new ArgumentNullException(nameof (files));
    foreach (string file in (IEnumerable<string>) files)
      this.AddSource(file);
  }

  /// <summary>
  /// Добавляет в карту путь к документу конфигурации в карту.
  /// </summary>
  /// <param name="filePath">Абсолютный путь к файлу документа</param>
  public void AddSource(string filePath)
  {
    if (filePath == null)
      throw new ArgumentNullException(nameof (filePath));
    if (!Path.IsPathRooted(filePath))
      throw new ArgumentException("Требуется путь к файлу в абсолютной форме.", nameof (filePath));
    if (this.directMap.ContainsKey(filePath))
      return;
    CADDocumentProxy cadDocumentProxy1 = this.cadSystem.OpenDocument(filePath, false);
    CADDocumentProxy cadDocumentProxy2;
    if (!this.masterDocsMap.TryGetValue(cadDocumentProxy1.MasterFile, out cadDocumentProxy2))
    {
      cadDocumentProxy2 = cadDocumentProxy1.IsMasterDocument ? cadDocumentProxy1 : this.cadSystem.OpenDocument(cadDocumentProxy1.MasterFile, false);
      this.masterDocsMap.Add(cadDocumentProxy1.MasterFile, cadDocumentProxy2);
      this.reverseMap.Add(cadDocumentProxy1.MasterFile, new PathCollection());
    }
    this.directMap.Add(filePath, cadDocumentProxy2.FullName);
    this.reverseMap[cadDocumentProxy2.FullName].Add(filePath);
  }

  /// <summary>
  /// Возвращает коллекцию мастер-документов для документов конфигураций, добавленных в карту.
  /// </summary>
  /// <returns>Список мастер-документов</returns>
  public List<CADDocumentProxy> GetAllMasterDocuments()
  {
    return new List<CADDocumentProxy>((IEnumerable<CADDocumentProxy>) this.masterDocsMap.Values);
  }

  /// <summary>
  /// Возвращает коллекцию путей к документам конфигураций, которые были отображены в указанный мастер-документ.
  /// </summary>
  /// <param name="masterFilePath">Абсолютный путь к мастер-документу</param>
  /// <returns>Список добавленных в карту абсолютных путей, которые были отображены в указанный мастер-документ</returns>
  public PathCollection GetMasterDocumentSources(string masterFilePath)
  {
    if (masterFilePath == null)
      throw new ArgumentNullException(nameof (masterFilePath));
    PathCollection collection;
    return this.reverseMap.TryGetValue(masterFilePath, out collection) ? new PathCollection((IEnumerable<string>) collection) : new PathCollection();
  }

  /// <summary>
  /// Возвращает коллекцию мастер-документов для указанных документов конфигураций.
  /// </summary>
  /// <param name="appProxy">Ссылка на объект CAD-системы</param>
  /// <param name="sourceFiles">Коллекция абсолютных путей к документам конфигураций</param>
  /// <returns>Список мастер-документов</returns>
  public static List<CADDocumentProxy> OpenMasterDocuments(
    CADSystemProxy appProxy,
    ICollection<string> sourceFiles)
  {
    if (appProxy == null)
      throw new ArgumentNullException(nameof (appProxy));
    if (sourceFiles == null)
      throw new ArgumentNullException("files");
    MasterDocumentsMapping documentsMapping = new MasterDocumentsMapping(appProxy, sourceFiles.Count);
    documentsMapping.AddSources(sourceFiles);
    return documentsMapping.GetAllMasterDocuments();
  }
}
