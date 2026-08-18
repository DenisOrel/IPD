
// Type: Intermech.Client.Core.IODispatcher
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System.Collections.Generic;


namespace Intermech.Client.Core;

/// <summary>Класс диспетчера событий</summary>
public class IODispatcher : IIODispatcher
{
  /// <summary>Список обработчиков событий</summary>
  private List<IIODestination> FDestinations = new List<IIODestination>(0);

  /// <summary>Зарегистрировать обработчик событий</summary>
  /// <param name="Destination">Добавляемый обработчик событий</param>
  public void RegisterDestination(IIODestination Destination)
  {
    if (this.FDestinations.IndexOf(Destination) >= 0)
      return;
    this.FDestinations.Add(Destination);
  }

  /// <summary>Удалить из внутренних списков обработчик событий</summary>
  /// <param name="Destination">Удаляемый обработчик событий</param>
  public void UnregisterDestination(IIODestination Destination)
  {
    this.FDestinations.Remove(Destination);
  }

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если хотя бы один из зарегистрированных обработчиков обработал сообщение</returns>
  public void ProcessEvent(IIOEvent Event)
  {
    if (this.FDestinations.Count <= 0 || Event == null)
      return;
    bool flag = (Event.EventFlags & IOEventFlags.efBroadcast) == IOEventFlags.efBroadcast;
    for (int index = 0; index < this.FDestinations.Count; ++index)
    {
      IIODestination fdestination = this.FDestinations[index];
      if ((fdestination.SupportedEvents & (IOEventTypes) Event.EventType) == (IOEventTypes) Event.EventType && ((Event.EventFlags & IOEventFlags.efProcessed) == IOEventFlags.efProcessed ? 1 : (fdestination.ProcessEvent(Event) ? 1 : 0)) != 0 && !flag)
        break;
    }
  }
}
