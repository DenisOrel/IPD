// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationServiceStates
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Флажки, по которым обработчики событий от INotificationService могут
/// выполнять какие-то дополнительные проверки перед обработкой событий
/// </summary>
[Flags]
public enum NotificationServiceStates
{
  /// <summary>Никаких требований к обработчикам нет</summary>
  Default = 0,
  /// <summary>
  /// Обработчики находятся в неактивной форме (например, в неактивной или скрытой закладке "Навигатора")
  /// </summary>
  InactiveForm = 1,
  /// <summary>
  /// Обработчики находятся в неактивной модальной форме (например, закэшированном диалоговом окне)
  /// </summary>
  InactiveDialog = 2,
}
