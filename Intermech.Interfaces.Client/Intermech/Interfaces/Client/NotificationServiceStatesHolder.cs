// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationServiceStatesHolder
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Класс, предоставляющий дополнительную информацию обработчикам событий от INotificationService.
/// </summary>
public class NotificationServiceStatesHolder : INotificationServiceStatesHolder
{
  /// <summary>
  /// Флажки, по которым обработчики событий от INotificationService могут
  /// выполнять какие-то дополнительные проверки перед обработкой событий
  /// </summary>
  private NotificationServiceStates _states;

  /// <summary>Создать экземпляр класса, заполнить его значением</summary>
  /// <param name="states">Флажки, по которым обработчики событий от INotificationService могут
  /// выполнять какие-то дополнительные проверки перед обработкой событий</param>
  public NotificationServiceStatesHolder(NotificationServiceStates states) => this._states = states;

  /// <summary>
  /// Флажки, по которым обработчики событий от INotificationService могут
  /// выполнять какие-то дополнительные проверки перед обработкой событий
  /// </summary>
  public NotificationServiceStates States
  {
    [DebuggerStepThrough] get => this._states;
    set => this._states = value;
  }
}
