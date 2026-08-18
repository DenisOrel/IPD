// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADCaptureChangesFactory
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Data.Metadata;
using Intermech.Interfaces.Data.SidecarObjects;
using Intermech.Services.IMViewer;
using Intermech.Tools.Integrators.Mechanical;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует фабрику объектов, общих для команд импорта файлов, сохранения изменений, расширенного сохранения.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
public class CADCaptureChangesFactory(IIntegrator owner) : IntegratorService(owner), IIntegratorService
{
  private bool enableIMViewerExtension;

  /// <summary>
  /// Включает или выключает расширение для интеграции с IMViewer.
  /// Свойство должно быть заполнено до начала использования текущего сервиса.
  /// По умолчанию значение свойства равно false.
  /// </summary>
  public bool EnableIMViewerExtension
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.enableIMViewerExtension;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.enableIMViewerExtension = value;
      }
    }
  }

  /// <summary>
  /// Создает объект драйвера для захвата изменений в документах интегрируемого приложения.
  /// </summary>
  /// <returns>Объект драйвера для захвата изменений</returns>
  public CICaptureChangesDriver CreateDriver()
  {
    this.RequireReadyState();
    CICaptureChangesDriver driver = this.DoCreateDriver();
    if (this.EnableIMViewerExtension)
      this.AddIMViewerExtension(driver);
    return driver;
  }

  private void AddIMViewerExtension(CICaptureChangesDriver driver)
  {
    IMViewerObjectsCaptureChangesExtension changesExtension = new IMViewerObjectsCaptureChangesExtension((MechanicalDriver) driver, new IMViewerObjectsIDCache(MetadataResolvers.Factory), ServiceUtils.GetService<ICADSettingsService>((object) this.Integrator, true), ServiceUtils.GetService<IIMViewerObjectCreatorService>((object) ApplicationServices.Container, true));
    changesExtension.EnableSanityChecks = false;
    driver.SidecarObjectsExtensions.Add((ISidecarObjectsCaptureChangesExtension) changesExtension);
  }

  /// <summary>
  /// Создает объект драйвера для захвата изменений в документах интегрируемого приложения.
  /// </summary>
  /// <returns>Объект драйвера для захвата изменений</returns>
  protected virtual CICaptureChangesDriver DoCreateDriver()
  {
    return new CICaptureChangesDriver(this.Integrator);
  }
}
