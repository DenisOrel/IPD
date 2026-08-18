// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IIODispatcher
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс диспетчера событий</summary>
public interface IIODispatcher
{
  /// <summary>Зарегистрировать обработчик событий</summary>
  /// <param name="Destination">Добавляемый обработчик событий</param>
  void RegisterDestination(IIODestination Destination);

  /// <summary>Удалить из внутренних списков обработчик событий</summary>
  /// <param name="Destination">Удаляемый обработчик событий</param>
  void UnregisterDestination(IIODestination Destination);

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если хотя бы один из зарегистрированных обработчиков обработал сообщение</returns>
  void ProcessEvent(IIOEvent Event);
}
