// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INotificationServiceStatesHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, предоставляющий дополнительную информацию обработчикам событий от INotificationService
/// </summary>
public interface INotificationServiceStatesHolder
{
  /// <summary>
  /// Флажки, по которым обработчики событий от INotificationService могут
  /// выполнять какие-то дополнительные проверки перед обработкой событий
  /// </summary>
  NotificationServiceStates States { get; set; }
}
