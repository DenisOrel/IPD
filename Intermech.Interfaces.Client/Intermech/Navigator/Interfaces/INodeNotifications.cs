// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeNotifications
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Позволяет элементу навигации реагировать на события обновления.
/// </summary>
public interface INodeNotifications
{
  /// <summary>Вернуть код реагирования на событие обновления</summary>
  /// <param name="e">Аргументы возникшего события</param>
  /// <param name="AdditionalInfo">Дополнительная информация</param>
  /// <returns>Код реагирования на событие</returns>
  ProcessResult Process(NotificationEventArgs e, object AdditionalInfo);
}
