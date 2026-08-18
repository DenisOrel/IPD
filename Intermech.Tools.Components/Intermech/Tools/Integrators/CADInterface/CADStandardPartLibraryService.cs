// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADStandardPartLibraryService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Localization;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует базовые возможности сервиса поддержки библиотеки стандартных.
/// </summary>
public class CADStandardPartLibraryService : 
  IntegratorService,
  IStandardPartLibraryService,
  IIntegratorService
{
  private readonly StandardLibraryMode mode;
  private readonly string folderName;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <param name="mode">Режим взаимодействия с CADMECH</param>
  /// <param name="folderName">Имя папки в рабочей области для хранения моделей стандартных</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
  public CADStandardPartLibraryService(
    IIntegrator owner,
    StandardLibraryMode mode,
    string folderName)
    : base(owner)
  {
    if (string.IsNullOrEmpty(folderName))
      throw new ArgumentException(LocalizationHolder.rm.GetString("Tools.Components_364"));
    this.mode = mode;
    this.folderName = folderName;
  }

  /// <summary>Возвращает режим взаимодействия с CADMECH</summary>
  public StandardLibraryMode Mode
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this.mode;
    }
  }

  /// <summary>
  /// Возвращает имя папки в рабочей области пользователя, где располагаются модели библиотеки.
  /// </summary>
  public string FolderName
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return this.folderName;
    }
  }

  /// <summary>
  /// Выполняет подготовку библиотеки стандартных CADMECH к импорту в базу IPS.
  /// </summary>
  /// <param name="directoryPath">Путь к папке, где расположена библиотека стандартных CADMECH</param>
  /// <exception cref="T:ArgumentNullException">directoryPath</exception>
  public void PrepareToImportCadmechLibrary(string directoryPath)
  {
    if (directoryPath == null)
      throw new ArgumentNullException(nameof (directoryPath));
    this.RequireReadyState();
    this.DoPrepareToImportCadmechLibrary(directoryPath);
  }

  /// <summary>
  /// Выполняет подготовку библиотеки стандартных CADMECH к импорту в базу IPS.
  /// </summary>
  /// <param name="directoryPath">Путь к папке, где расположена библиотека стандартных CADMECH</param>
  protected virtual void DoPrepareToImportCadmechLibrary(string directoryPath)
  {
  }

  /// <summary>
  /// Возвращает true, если поддерживается импорт моделей стандартных, созданных пользователями вручную без CADMECH.
  /// </summary>
  public bool CanImportCustomParts
  {
    get
    {
      this.RequireReadyState();
      return this.OnCanImportCustomParts();
    }
  }

  /// <summary>
  /// Возвращает true, если поддерживается импорт моделей стандартных, созданных пользователями вручную без CADMECH.
  /// </summary>
  protected virtual bool OnCanImportCustomParts() => false;

  /// <summary>
  /// Реализует эвристическое определение стандартного изделия, выпускаемого по модели, созданной пользователями вручную без CADMECH.
  /// Метод вызывается в процессе импорта библиотеки стандартных CADMECH.
  /// </summary>
  /// <param name="articleAttributes">Атрибуты стандартного изделия, прочитанные из конфигурации модели</param>
  /// <returns>true - если это стандартное изделие, подлежащее импорту</returns>
  /// <exception cref="T:ArgumentNullException">articleAttributes</exception>
  public bool IsCustomPartArticle(ValueBag articleAttributes)
  {
    if (articleAttributes == null)
      throw new ArgumentNullException(nameof (articleAttributes));
    this.RequireReadyState();
    return this.OnIsCustomPartArticle(articleAttributes);
  }

  /// <summary>
  /// Реализует эвристическое определение стандартного изделия, выпускаемого по модели, созданной пользователями вручную без CADMECH.
  /// Метод вызывается в процессе импорта библиотеки стандартных CADMECH.
  /// </summary>
  /// <param name="articleAttributes">Атрибуты стандартного изделия, прочитанные из конфигурации модели</param>
  /// <returns>true - если это стандартное изделие, подлежащее импорту</returns>
  /// <exception cref="T:ArgumentNullException">articleAttributes</exception>
  protected virtual bool OnIsCustomPartArticle(ValueBag articleAttributes) => false;

  /// <summary>
  /// Подготавливает к импорту в IPS стандартное изделие, выпускаемое по модели, созданной пользователями вручную без CADMECH.
  /// Метод вызывается в процессе импорта библиотеки стандартных CADMECH.
  /// </summary>
  /// <param name="articleAttributes">Атрибуты стандартного изделия, прочитанные из конфигурации модели</param>
  /// <exception cref="T:ArgumentNullException">articleAttributes</exception>
  public void PrepareCustomPartArticleToImport(ValueBag articleAttributes)
  {
    if (articleAttributes == null)
      throw new ArgumentNullException(nameof (articleAttributes));
    this.RequireReadyState();
    this.DoPrepareCustomPartArticleToImport(articleAttributes);
  }

  /// <summary>
  /// Подготавливает к импорту в IPS стандартное изделие, выпускаемое по модели, созданной пользователями вручную без CADMECH.
  /// Метод вызывается в процессе импорта библиотеки стандартных CADMECH.
  /// </summary>
  /// <param name="articleAttributes">Атрибуты стандартного изделия, прочитанные из конфигурации модели</param>
  protected virtual void DoPrepareCustomPartArticleToImport(ValueBag articleAttributes)
  {
  }
}
