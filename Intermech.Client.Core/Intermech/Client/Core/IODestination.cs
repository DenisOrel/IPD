
// Type: Intermech.Client.Core.IODestination
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core;

/// <summary>Класс обработчика событий</summary>
public class IODestination : IIODestination
{
  /// <summary>
  /// Список поддерживаемых обработчиком событий. По умолчанию - никакие события не нужны.
  /// </summary>
  private IOEventTypes FSupportedEvents;

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="ASupportedEvents">Список поддерживаемых обработчиком событий</param>
  public IODestination(IOEventTypes ASupportedEvents) => this.FSupportedEvents = ASupportedEvents;

  /// <summary>Список поддерживаемых обработчиком событий</summary>
  public IOEventTypes SupportedEvents
  {
    get => this.FSupportedEvents;
    set => this.FSupportedEvents = value;
  }

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если обработка выполнена успешно, false, если событие не обработано</returns>
  public virtual bool ProcessEvent(IIOEvent Event) => false;
}
