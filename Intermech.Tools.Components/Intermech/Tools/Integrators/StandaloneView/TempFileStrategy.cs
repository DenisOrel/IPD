// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.StandaloneView.TempFileStrategy
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Interfaces.Client;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.StandaloneView;

/// <summary>
/// Базовый класс для стратегии создания временного файла, используемого для внедрения в файла документа сведений, необходимых для режима автономного просмотра.
/// </summary>
public abstract class TempFileStrategy
{
  private IFileVault fileVaultService;
  private bool isInitialized;
  private string directoryPath;
  private string filePath;

  /// <summary>Создает объект.</summary>
  protected TempFileStrategy() => this.fileVaultService = ClientContext.FileVault;

  /// <summary>Возвращает сервис файлового хранилища.</summary>
  protected IFileVault FileVault
  {
    [DebuggerStepThrough] get => this.fileVaultService;
  }

  /// <summary>Инициализирует стратегию.</summary>
  /// <param name="operation">Контейнер данных для операции</param>
  /// <exception cref="T:ArgumentNullException">operation</exception>
  public void Initialize(StandaloneViewDataInjectionOperation operation)
  {
    if (operation == null)
      throw new ArgumentNullException(nameof (operation));
    this.Cleanup();
    try
    {
      Tuple<string, string> tuple = this.DoInitialize(operation);
      this.directoryPath = tuple.Item1;
      this.filePath = tuple.Item2;
      this.isInitialized = true;
    }
    catch
    {
      this.DoCleanup();
      throw;
    }
  }

  /// <summary>Очищает стратегию.</summary>
  public void Cleanup()
  {
    if (!this.isInitialized)
      return;
    this.DoCleanup();
    this.isInitialized = false;
  }

  /// <summary>Инициализирует стратегию.</summary>
  /// <param name="operation">Контейнер данных для операции</param>
  /// <returns>Кортеж из абсолютного пути к каталогу временного файла и абсолютного пути к самому временному файлу документа</returns>
  protected abstract Tuple<string, string> DoInitialize(
    StandaloneViewDataInjectionOperation operation);

  /// <summary>Очищает стратегию.</summary>
  protected virtual void DoCleanup()
  {
    this.directoryPath = (string) null;
    this.filePath = (string) null;
  }

  /// <summary>Проверяет, была ли стратегия инициализирована.</summary>
  /// <exception cref="T:InvalidOperationException">Стратегия не была инициализирована</exception>
  protected void RequireInitialized()
  {
    if (!this.isInitialized)
      throw new InvalidOperationException("Object must be initialized first.");
  }

  /// <summary>Возвращает признак инициализации стратегии.</summary>
  public bool IsInitialized
  {
    [DebuggerStepThrough] get => this.isInitialized;
  }

  /// <summary>
  /// Возвращает абсолютный путь к каталогу временного файла.
  /// </summary>
  public string DirectoryPath
  {
    [DebuggerStepThrough] get
    {
      this.RequireInitialized();
      return this.directoryPath;
    }
  }

  /// <summary>Возвращает абсолютный путь к временному файлу.</summary>
  public string FilePath
  {
    [DebuggerStepThrough] get
    {
      this.RequireInitialized();
      return this.filePath;
    }
  }

  /// <summary>Удаляет все временные файлы.</summary>
  public void RemoveFiles()
  {
    this.RequireInitialized();
    this.DoRemoveFiles();
  }

  /// <summary>Удаляет все временные файлы.</summary>
  protected virtual void DoRemoveFiles()
  {
  }

  /// <summary>Возвращает случайное имя файла без пути и расширения.</summary>
  /// <returns>Случайное имя файла</returns>
  protected string MakeRandomFileName()
  {
    return this.FileVault.TempArea.GetRandomFileName().Replace('.', '_');
  }
}
