// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CIModelDrawingsImportService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.IO;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис драйвера захвата изменений, обслуживающих задачи импорта чертежей моделей.
/// </summary>
internal sealed class CIModelDrawingsImportService : 
  MechanicalDriverService,
  IModelDrawingsImportService
{
  private IModelDrawingsService modelDrawingsIntegratorService;

  /// <summary>Создает объект.</summary>
  /// <param name="driver">Драйвер захвата изменений</param>
  /// <param name="driverContext">Контекст выполняемой операции захвата изменений</param>
  /// <param name="modelDrawingsIntegratorService">Сервис интегратора, обслуживающий чертежи моделей</param>
  /// <exception cref="T:ArgumentNullException">driver or driverContext</exception>
  public CIModelDrawingsImportService(
    CICaptureChangesDriver driver,
    CaptureChangesDriverContext driverContext,
    IModelDrawingsService modelDrawingsIntegratorService)
    : base((MechanicalDriver) driver, driverContext)
  {
    this.modelDrawingsIntegratorService = modelDrawingsIntegratorService != null ? modelDrawingsIntegratorService : throw new ArgumentNullException(nameof (modelDrawingsIntegratorService));
  }

  private CICaptureChangesDriver CIDriver
  {
    [DebuggerStepThrough] get => (CICaptureChangesDriver) this.Driver;
  }

  /// <summary>
  /// Возвращает режим импорта новых файлов чертежей в базу данных.
  /// </summary>
  /// <returns>Режим импорта новых файлов чертежей</returns>
  public NewDrawingMode GetNewDrawingMode() => this.CIDriver.IntegratorSettings.NewDrawingMode;

  /// <summary>
  /// Позволяет найти все файлы чертежей, связанные с указанным документом 3D-модели.
  /// </summary>
  /// <param name="modelDocumentFiles">Список файлов документа 3D-модели</param>
  /// <returns>Коллекция найденных файлов чертежей</returns>
  /// <exception cref="T:System.ArgumentNullException">Ни один из аргументов метода не может быть null</exception>
  public PathCollection FindAllDrawingFiles(IEnumerable<string> modelDocumentFiles)
  {
    return this.modelDrawingsIntegratorService.FindAllDrawingFiles(modelDocumentFiles, new Func<string, bool>(File.Exists));
  }
}
