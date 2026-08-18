// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.IIntegratorLicense
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Tools.Integrators;

/// <summary>
/// Сервис, занимающийся выделением лицензии для интегратора с приложением.
/// </summary>
public interface IIntegratorLicense : IIntegratorService
{
  /// <summary>
  /// Проверяет ключ защиты и выполняет отъем лицензии, если это еще не было сделано.
  /// </summary>
  void Check();
}
