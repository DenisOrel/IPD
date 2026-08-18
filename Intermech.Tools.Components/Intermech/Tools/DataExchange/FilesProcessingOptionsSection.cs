// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.DataExchange.FilesProcessingOptionsSection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.DataExchange;

/// <summary>
/// Секция для хранения параметров обработки файлов объекта IPS.
/// </summary>
/// <remarks>
/// Секция не является обязательной и может отсутствовать. В этом случае считается,
/// что значения ее свойств равны соответствующим значениям по умолчанию.
/// </remarks>
public sealed class FilesProcessingOptionsSection
{
  /// <summary>Создает объект</summary>
  public FilesProcessingOptionsSection()
    : this(true, true)
  {
  }

  /// <summary>Создает объект</summary>
  /// <param name="enableFilesProcessing">Управляет анализом собственных файлов объекта</param>
  /// <param name="enableDependenciesProcessing">Управляет анализом файловых зависимостей объекта</param>
  public FilesProcessingOptionsSection(
    bool enableFilesProcessing,
    bool enableDependenciesProcessing)
  {
    this.EnableFilesProcessing = enableFilesProcessing;
    this.EnableDependenciesProcessing = enableDependenciesProcessing;
  }

  /// <summary>
  /// Возвращает или задает признак, что собственные файлы объекта должны быть проанализированы на наличие изменений.
  /// По умолчанию значение свойства равно true.
  /// Если значение свойства равно false, то обработка собственных файлов объекта должна быть отключена,
  /// а в базу данных IPS не следует вносить какие-либо изменения.
  /// </summary>
  public bool EnableFilesProcessing { get; set; }

  /// <summary>
  /// Возвращает или задает признак, что файловые зависимости объекта должны быть проанализированы на наличие изменений.
  /// По умолчанию значение свойства равно true.
  /// Если значение свойства равно false, то обработка файловых зависимостей объекта должна быть отключена,
  /// а в базу данных IPS не следует вносить какие-либо изменения.
  /// </summary>
  public bool EnableDependenciesProcessing { get; set; }
}
