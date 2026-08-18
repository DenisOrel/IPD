// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADFileImportSupportService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Files;
using Intermech.Tools.DataExchange;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис импорта файлов для CAD-систем на основе CAD-интерфейса Интермех. Класс является
/// thread-safe.
/// </summary>
public class CADFileImportSupportService : Intermech.Tools.Integrators.FileImportService
{
  private readonly CADCaptureChangesFactory factory;
  private CICaptureChangesDriver driver;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец компонента</param>
  /// <param name="factory">Фабрика используемых объектов</param>
  public CADFileImportSupportService(IIntegrator owner, CADCaptureChangesFactory factory)
    : base(owner)
  {
    this.factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory));
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.driver = this.factory.CreateDriver();
  }

  /// <summary>Возвращает флаги особенностей импорта файла.</summary>
  /// <returns>Флаги особенностей импорта файла</returns>
  protected override ImportFileCapabilities DoGetImportFileCapabilities()
  {
    return base.DoGetImportFileCapabilities() | ImportFileCapabilities.DeferredImport;
  }

  /// <summary>
  /// Возвращает экземпляр драйвера для импорта файла интегрируемого приложения. Метод обязательно должен вернуть созданный объект.
  /// </summary>
  /// <returns>Объект драйвера</returns>
  protected override ICaptureChangesDriver GetCaptureChangesDriver()
  {
    return (ICaptureChangesDriver) this.driver;
  }

  /// <summary>
  /// Устанавливает свойства драйвера, управляющие его поведением.
  /// </summary>
  /// <param name="extendedImport">Признак расширенного импорта. Если содержит true, то при импорте должен быть создан не только документ, но и выпускаемые по нему объекты (изделия и др.)</param>
  protected override void SetCaptureChangesParameters(bool extendedImport)
  {
    base.SetCaptureChangesParameters(extendedImport);
    if (!extendedImport)
      return;
    this.driver.UpdateArticles = true;
    this.driver.RecalculateMass = true;
  }

  /// <summary>
  /// Очищает свойства драйвера, управляющие его поведением.
  /// </summary>
  protected override void ResetCaptureChangesParameters()
  {
    base.ResetCaptureChangesParameters();
    this.driver.UpdateArticles = false;
    this.driver.RecalculateMass = false;
  }
}
