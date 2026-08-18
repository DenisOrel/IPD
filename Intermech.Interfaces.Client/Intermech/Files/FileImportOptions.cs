// Decompiled with JetBrains decompiler
// Type: Intermech.Files.FileImportOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.UI;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>Опции импорта файла в базу данных IPS.</summary>
[Serializable]
public sealed class FileImportOptions : ICloneable
{
  private bool importDeferredFiles;
  private bool notifyOnDeferredFilesErrors;
  private IPercentageProgressSink progressSink;

  /// <summary>Создает объект</summary>
  public FileImportOptions()
  {
    this.importDeferredFiles = true;
    this.notifyOnDeferredFilesErrors = false;
  }

  /// <summary>
  /// Включает или выключает режим автоматического импорта отложенных ссылочных зависимостей,
  /// представленных в базе данных с помощью черновиков документов IPS.
  /// По умолчанию значение свойства установлено в true.
  /// </summary>
  public bool ImportDeferredFiles
  {
    [DebuggerStepThrough] get => this.importDeferredFiles;
    [DebuggerStepThrough] set => this.importDeferredFiles = value;
  }

  /// <summary>
  /// Включает или выключает режим отображения уведомлений об ошибках импорта отложенных ссылочных зависимостей.
  /// По умолчанию значение свойства установлено в false.
  /// </summary>
  public bool NotifyOnDeferredFilesErrors
  {
    [DebuggerStepThrough] get => this.notifyOnDeferredFilesErrors;
    [DebuggerStepThrough] set => this.notifyOnDeferredFilesErrors = value;
  }

  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IPercentageProgressSink ProgressSink
  {
    [DebuggerStepThrough] get => this.progressSink;
    [DebuggerStepThrough] set => this.progressSink = value;
  }

  /// <summary>Клонирует текщий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  public FileImportOptions Clone()
  {
    return new FileImportOptions()
    {
      ImportDeferredFiles = this.ImportDeferredFiles,
      NotifyOnDeferredFilesErrors = this.NotifyOnDeferredFilesErrors,
      ProgressSink = this.ProgressSink
    };
  }

  /// <summary>Клонирует текщий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();
}
