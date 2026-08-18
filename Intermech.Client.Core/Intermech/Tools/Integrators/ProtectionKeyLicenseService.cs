
// Type: Intermech.Tools.Integrators.ProtectionKeyLicenseService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Protection;
using System;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис, занимающийся выделением лицензии для интегратора с приложением из ключа защиты. Класс является thread-safe.
/// </summary>
public abstract class ProtectionKeyLicenseService : IntegratorLicenseService
{
  private readonly Random rnd;
  private readonly int appId;
  private readonly byte[][] protectionCodes;
  private bool allocated;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец сервиса</param>
  /// <param name="appId">Идентификатор приложения в ключе защиты</param>
  /// <param name="protectionCodes">Массив кодов защиты</param>
  public ProtectionKeyLicenseService(IIntegrator owner, int appId, byte[][] protectionCodes)
    : base(owner)
  {
    if (protectionCodes == null)
      throw new ArgumentNullException(nameof (protectionCodes));
    this.rnd = new Random(Environment.TickCount);
    this.appId = appId;
    this.protectionCodes = protectionCodes;
  }

  /// <summary>Выполняет взаимодействие с ключем.</summary>
  /// <returns>true - если лицензия имеется и ключ отвечает, false - либо нет лицензии, либо ключ не отвечает</returns>
  protected override bool DoWork()
  {
    if (!this.allocated)
    {
      ServiceUtils.GetService<ILicenser>((object) ServicesManager.ServiceContainer, true).AllocateLicense(this.appId);
      this.allocated = true;
    }
    if (this.rnd.Next(100) % 13 == 0)
    {
      IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ServicesManager.ServiceContainer, false);
      if (service == null)
        return false;
      long index1 = (Stopwatch.GetTimestamp() & 15L) * 2L;
      byte[] protectionCode1 = this.protectionCodes[index1];
      byte[] protectionCode2 = this.protectionCodes[index1 + 1L];
      byte[] response = new byte[protectionCode2.Length];
      service.Query(true, this.appId, protectionCode1, response);
      for (int index2 = 0; index2 < response.Length; ++index2)
      {
        if ((int) response[index2] != (int) protectionCode2[index2])
          return false;
      }
    }
    return true;
  }
}
