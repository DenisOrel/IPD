// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.FileDependencyProcessingParameters
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Контейнер для параметров обработки ссылочной зависимости.
/// </summary>
public sealed class FileDependencyProcessingParameters : FreezableObject
{
  private static readonly FileDependencyProcessingParameters analyseInstance = FileDependencyProcessingParameters.CreateFrozen(FileDependencyProcessingMode.Analyze);
  private static readonly FileDependencyProcessingParameters ignoreInstance = FileDependencyProcessingParameters.CreateFrozen(FileDependencyProcessingMode.Ignore);
  private static readonly FileDependencyProcessingParameters deferImportInstance = FileDependencyProcessingParameters.CreateFrozen(FileDependencyProcessingMode.DeferImport);
  private FileDependencyProcessingMode mode;
  private object documentAnalysisOptions;

  /// <summary>Создает объект.</summary>
  /// <param name="mode">Режим обработки</param>
  public FileDependencyProcessingParameters(FileDependencyProcessingMode mode) => this.Mode = mode;

  private static FileDependencyProcessingParameters CreateFrozen(FileDependencyProcessingMode mode)
  {
    FileDependencyProcessingParameters frozen = new FileDependencyProcessingParameters(mode);
    frozen.Freeze();
    return frozen;
  }

  /// <summary>
  /// Возвращает или задает режим обработки ссылочной зависимости.
  /// </summary>
  public FileDependencyProcessingMode Mode
  {
    [DebuggerStepThrough] get => this.mode;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (Mode));
      this.mode = value;
    }
  }

  /// <summary>
  /// Возвращает или задает опции анализа для документа, который будет создан по ссылочной зависимости.
  /// </summary>
  public object DocumentAnalysisOptions
  {
    [DebuggerStepThrough] get => this.documentAnalysisOptions;
    [DebuggerStepThrough] set
    {
      this.RequireNotFrozenBeforePropertyChange(nameof (DocumentAnalysisOptions));
      this.documentAnalysisOptions = value;
    }
  }

  /// <summary>
  /// Возвращает константный объект, который используется при необходимости полного анализа ссылочной зависимости.
  /// </summary>
  public static FileDependencyProcessingParameters Analyse
  {
    get => FileDependencyProcessingParameters.analyseInstance;
  }

  /// <summary>
  /// Возвращает константный объект, который используется при необходимости игнорирования ссылочной зависимости.
  /// </summary>
  public static FileDependencyProcessingParameters Ignore
  {
    get => FileDependencyProcessingParameters.ignoreInstance;
  }

  /// <summary>
  /// Возвращает константный объект, который используется при необходимости отложенного импорта ссылочной зависимости.
  /// </summary>
  public static FileDependencyProcessingParameters DeferImport
  {
    get => FileDependencyProcessingParameters.deferImportInstance;
  }
}
