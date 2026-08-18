// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEventArgsOptimizationMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс аргументов службы уведомлений, позволяющий проверять корректность применяемой оптимизации
/// </summary>
public interface IEventArgsOptimizationMode
{
  /// <summary>
  /// Проверить, поддерживается ли указанный режим оптимизации аргументами события и,
  /// в случае необходимости, вернуть максимальный уровень поддерживаемой оптимизации
  /// </summary>
  /// <param name="mode">Запрашиваемый режим оптимизации</param>
  /// <returns>Допустимый режим оптимизации</returns>
  NotificationServiceMode GetSupportedOptimization(NotificationServiceMode mode);
}
