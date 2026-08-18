// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.NotificationServiceMode
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Localization;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Режим обработки сообщений службы уведомлений</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Serializable]
public enum NotificationServiceMode
{
  /// <summary>Обрабатывать все сообщения службы уведомлений</summary>
  [CustomDescription("NotificationServiceMode_Default")] Default,
  /// <summary>Обновлять все окна Навигатора, если много сообщений</summary>
  [CustomDescription("NotificationServiceMode_Refresh")] RefreshWindows,
  /// <summary>Не обновлять ничего, только уведомлять пользователя</summary>
  [CustomDescription("NotificationServiceMode_NotifyUser")] NotifyUser,
}
