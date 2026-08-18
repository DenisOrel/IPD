// Decompiled with JetBrains decompiler
// Type: Intermech.Files.BatchFileImportOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.UI;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Files;

/// <summary>Опции группового импорта файлов в базу данных IPS.</summary>
[Serializable]
public sealed class BatchFileImportOptions : ICloneable
{
  private bool notifyOnMasterFileErrors;
  private bool notifyOnDeferredFilesErrors;
  [NonSerialized]
  private Action<FileImportResult.Success> afterImportAction;
  private IMasterSlaveProgressSink customProgressSink;

  /// <summary>Создает объект</summary>
  public BatchFileImportOptions()
  {
    this.notifyOnMasterFileErrors = false;
    this.notifyOnDeferredFilesErrors = false;
  }

  /// <summary>
  /// Возвращает или задает обработчик, который будет применен к каждому успешно импортированному файлу.
  /// Значение свойства может быть не задано.
  /// </summary>
  public Action<FileImportResult.Success> AfterImportAction
  {
    [DebuggerStepThrough] get => this.afterImportAction;
    [DebuggerStepThrough] set => this.afterImportAction = value;
  }

  /// <summary>
  /// Возвращает или задает индикатор хода выполнения операции.
  /// Значение свойства может быть не задано.
  /// </summary>
  public IMasterSlaveProgressSink CustomProgressSink
  {
    [DebuggerStepThrough] get => this.customProgressSink;
    [DebuggerStepThrough] set => this.customProgressSink = value;
  }

  /// <summary>
  /// Включает или выключает режим отображения уведомлений об ошибках импорта головных файлов.
  /// По умолчанию значение свойства установлено в false.
  /// </summary>
  public bool NotifyOnMasterFileErrors
  {
    [DebuggerStepThrough] get => this.notifyOnMasterFileErrors;
    [DebuggerStepThrough] set => this.notifyOnMasterFileErrors = value;
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

  /// <summary>Клонирует текщий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  public BatchFileImportOptions Clone()
  {
    return new BatchFileImportOptions()
    {
      NotifyOnMasterFileErrors = this.NotifyOnMasterFileErrors,
      NotifyOnDeferredFilesErrors = this.NotifyOnDeferredFilesErrors,
      AfterImportAction = this.AfterImportAction,
      CustomProgressSink = this.CustomProgressSink
    };
  }

  /// <summary>Клонирует текщий объект.</summary>
  /// <returns>Клон текущего объекта</returns>
  object ICloneable.Clone() => (object) this.Clone();
}
