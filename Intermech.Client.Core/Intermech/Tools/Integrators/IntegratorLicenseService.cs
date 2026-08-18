
// Type: Intermech.Tools.Integrators.IntegratorLicenseService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Protection;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис, занимающийся выделением лицензии для интегратора с приложением. Класс является thread-safe.
/// </summary>
public abstract class IntegratorLicenseService : 
  IntegratorService,
  IIntegratorLicense,
  IIntegratorService
{
  public IntegratorLicenseService(IIntegrator owner)
    : base(owner)
  {
  }

  /// <summary>
  /// Проверяет ключ и выполняет отъем лицензии, если это еще не было сделано.
  /// </summary>
  public void Check()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      if (!this.DoWork())
        throw new ProtectionException(LocalizationHolder.rm.GetString("SR_1618"));
    }
  }

  /// <summary>Выполняет полезную работу.</summary>
  /// <returns>Результат выполнения</returns>
  protected abstract bool DoWork();
}
