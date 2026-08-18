// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IStandardPartLibraryService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Сервис поддержки библиотеки стандартных CADMECH. Этот сервис используется PDM-деревом и командой импорта библиотеки стандартных.
/// </summary>
public interface IStandardPartLibraryService : IIntegratorService
{
  /// <summary>Возвращает режим взаимодействия с CADMECH</summary>
  StandardLibraryMode Mode { get; }

  /// <summary>
  /// Возвращает имя папки в рабочей области пользователя, где располагаются модели библиотеки.
  /// </summary>
  string FolderName { get; }

  /// <summary>
  /// Выполняет подготовку библиотеки стандартных CADMECH к импорту в базу IPS.
  /// </summary>
  /// <param name="directoryPath">Путь к папке, где расположена библиотека стандартных CADMECH</param>
  /// <exception cref="T:ArgumentNullException">directoryPath</exception>
  void PrepareToImportCadmechLibrary(string directoryPath);

  /// <summary>
  /// Возвращает true, если поддерживается импорт моделей стандартных, созданных пользователями вручную без CADMECH.
  /// </summary>
  bool CanImportCustomParts { get; }

  /// <summary>
  /// Реализует эвристическое определение стандартного изделия, выпускаемого по модели, созданной пользователями вручную без CADMECH.
  /// Метод вызывается в процессе импорта библиотеки стандартных CADMECH.
  /// </summary>
  /// <param name="articleAttributes">Атрибуты стандартного изделия, прочитанные из конфигурации модели</param>
  /// <returns>true - если это стандартное изделие, подлежащее импорту</returns>
  /// <exception cref="T:ArgumentNullException">articleAttributes</exception>
  bool IsCustomPartArticle(ValueBag articleAttributes);

  /// <summary>
  /// Подготавливает к импорту в IPS стандартное изделие, выпускаемое по модели, созданной пользователями вручную без CADMECH.
  /// Метод вызывается в процессе импорта библиотеки стандартных CADMECH.
  /// </summary>
  /// <param name="articleAttributes">Атрибуты стандартного изделия, прочитанные из конфигурации модели</param>
  /// <exception cref="T:ArgumentNullException">articleAttributes</exception>
  void PrepareCustomPartArticleToImport(ValueBag articleAttributes);
}
