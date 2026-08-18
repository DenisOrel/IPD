// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileDependencyProcessingData
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using System;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Контейнер сведений об обрабатываемой анализатором ссылочной зависимости.
/// </summary>
public sealed class FileDependencyProcessingData
{
  private FileOrigin fileOrigin;
  private long? objectIdCache;
  private bool? isNewFileCache;

  internal FileDependencyProcessingData(DocumentFileData dependencyFile)
  {
    this.File = dependencyFile != null ? dependencyFile : throw new ArgumentNullException(nameof (dependencyFile));
  }

  /// <summary>Возвращает контейнер с открытым файлом зависимости.</summary>
  public DocumentFileData File { get; private set; }

  /// <summary>Возвращает сведения о происхождении файла.</summary>
  public FileOrigin FileOrigin
  {
    get => this.fileOrigin;
    internal set
    {
      if (this.fileOrigin == value)
        return;
      this.fileOrigin = value;
      this.ResetFileOriginCache();
    }
  }

  private void ResetFileOriginCache()
  {
    this.objectIdCache = new long?();
    this.isNewFileCache = new bool?();
  }

  /// <summary>
  /// Возвращает идентификатор версии объекта IPS, которому принадлежит файл.
  /// Значение свойства может быть равно Intermech.Consts.UnknownObjectId, если файл не принадлежит объекту IPS.
  /// </summary>
  public long ObjectId
  {
    get
    {
      if (this.FileOrigin == null)
        return 0;
      if (!this.objectIdCache.HasValue)
        this.objectIdCache = new long?(this.FileOrigin.WorkObject != null ? this.FileOrigin.WorkObject.ObjectId : 0L);
      return this.objectIdCache.Value;
    }
  }

  /// <summary>
  /// Возвращает признак, что это новый файл, не принадлежащий какому-либо объекту IPS.
  /// </summary>
  public bool IsNewFile
  {
    get
    {
      if (this.FileOrigin == null)
        return false;
      if (!this.isNewFileCache.HasValue)
        this.isNewFileCache = new bool?(this.ObjectId == 0L);
      return this.isNewFileCache.Value;
    }
  }

  /// <summary>
  /// Возвращает или задает идентификатор существующего черновика, связанного с ссылочной зависимостью.
  /// Значение свойства может быть заполнено только для импортируемых ссылочных зависимостей.
  /// </summary>
  internal long? DraftDocumentId { get; set; }
}
