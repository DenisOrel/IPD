// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.FilesSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Collections;
using Intermech.Data.EntityDb;
using Intermech.Data.SectionEntities;
using Intermech.IO;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>Секция для работы с файлами</summary>
[DebuggerDisplay("FilesSection: {MasterFile}")]
public sealed class FilesSection
{
  /// <summary>Полный путь к мастер-файлу</summary>
  private string masterFile;
  /// <summary>Список дополнительных файлов</summary>
  private ObservableList<string> satellites;
  /// <summary>
  /// Список файлов, от которых непосредственно зависит мастер-файл
  /// </summary>
  private PathCollection dependencies;
  public static readonly SectionPropertyReference MasterFileRef = new SectionPropertyReference(typeof (FilesSection), nameof (MasterFile));
  public static readonly SectionPropertyReference SatellitesRef = new SectionPropertyReference(typeof (FilesSection), nameof (Satellites));

  /// <summary>
  /// Полный путь к мастер-файлу
  /// Значение свойства может быть не задано, если обработка собственных файлов объекта была выключена
  /// с помощью <see cref="T:Intermech.Tools.DataExchange.FilesProcessingOptionsSection" />.
  /// </summary>
  [Indexable(IndexType.Auto, false)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (PathComparer)})]
  public string MasterFile
  {
    [DebuggerStepThrough] get => this.masterFile;
    [DebuggerStepThrough] set
    {
      if (!string.IsNullOrEmpty(value) && !Path.IsPathRooted(value))
        throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_443"));
      if (PathUtils.IsSamePath(this.masterFile, value))
        return;
      this.masterFile = value;
      if (this.MasterFileChanged == null)
        return;
      this.MasterFileChanged((object) this, EventArgs.Empty);
    }
  }

  /// <summary>Список дополнительных файлов</summary>
  [Indexable(IndexType.Auto, false)]
  [Comparer(typeof (ServiceObjectAttribute.NewObject), new object[] {typeof (PathComparer)})]
  public ObservableList<string> Satellites
  {
    [DebuggerStepThrough] get => this.satellites;
  }

  /// <summary>
  /// Возвращает список мастер-файлов документов, от которых зависит данный документ. Для каждого документа-зависимости в рабочем контексте содержится элемент.
  /// </summary>
  public PathCollection Dependencies
  {
    [DebuggerStepThrough] get => this.dependencies;
  }

  public event EventHandler MasterFileChanged;

  /// <summary>Создать файловую секцию</summary>
  public FilesSection()
  {
    this.satellites = new ObservableList<string>((IList<string>) new PathCollection());
    this.dependencies = new PathCollection();
  }

  public static SectionEntity FindByMasterFile(CaptureChangesDatabase db, string masterFile)
  {
    if (db == null)
      throw new ArgumentNullException(nameof (db));
    if (string.IsNullOrEmpty(masterFile))
      throw new ArgumentException();
    return db.QueryFirst((IQueryCondition) new BinaryCondition((object) FilesSection.MasterFileRef, BinaryOperator.Equal, (object) masterFile));
  }

  public static SectionEntity FindByMasterOrSatelliteFile(
    CaptureChangesDatabase db,
    string filePath)
  {
    if (db == null)
      throw new ArgumentNullException(nameof (db));
    if (string.IsNullOrEmpty(filePath))
      throw new ArgumentException();
    return db.QueryFirst((IQueryCondition) new CompoundSetCondition(CompoundSetOperator.Union, new IQueryCondition[2]
    {
      (IQueryCondition) new BinaryCondition((object) FilesSection.MasterFileRef, BinaryOperator.Equal, (object) filePath),
      (IQueryCondition) new BinaryCondition((object) FilesSection.SatellitesRef, BinaryOperator.Equal, (object) filePath)
    }));
  }

  public static string GetMasterFile(SectionEntity documentItem)
  {
    FilesSection filesSection = documentItem != null ? documentItem.Sections.Get<FilesSection>() : throw new ArgumentNullException(nameof (documentItem));
    return !string.IsNullOrEmpty(filesSection.MasterFile) ? filesSection.MasterFile : throw new InvalidOperationException(LocalizationHolder.rm.GetString("Tools.Components_442"));
  }

  public static PathCollection CopyAllFiles(SectionEntity documentItem)
  {
    return documentItem != null ? FilesSection.CopyAllFiles(documentItem.Sections.Get<FilesSection>()) : throw new ArgumentNullException(nameof (documentItem));
  }

  public static PathCollection CopyAllFiles(FilesSection filesSection)
  {
    if (filesSection == null)
      throw new ArgumentNullException(nameof (filesSection));
    PathCollection collection = new PathCollection(1 + filesSection.Satellites.Count);
    collection.Add(filesSection.MasterFile);
    collection.AddRange<string>((IEnumerable<string>) filesSection.Satellites);
    return collection;
  }
}
