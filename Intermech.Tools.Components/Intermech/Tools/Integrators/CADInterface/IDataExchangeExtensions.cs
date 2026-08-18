// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.IDataExchangeExtensions
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Необязательный сервис, которые используется для адаптации DataExchange-сервисов интегратора под нужды конкретной CAD-системы.
/// Реализация этого сервиса не должна быть thread-safe, так как все обращения к сервису выполняются уже из-под блокировки.
/// </summary>
public interface IDataExchangeExtensions
{
  /// <summary>
  /// Создает объект, позволяющий фильтровать доступные интегратору файловые зависимости.
  /// </summary>
  /// <param name="cadProxy">Прокси-объект для CAD-системы</param>
  /// <returns>Управляющий объект. Может быть null, если специальное поведение не требуется</returns>
  IDependencyFilterBehavior CreateDependencyFilterBehavior(CADSystemProxy cadProxy);
}
