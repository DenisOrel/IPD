
// Type: Intermech.Client.Core.IOEvent
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Client.Core;

/// <summary>Класс события</summary>
public class IOEvent : IIOEvent, IIOSourceInfo
{
  /// <summary>Источник события</summary>
  private IIOSource FSource;
  /// <summary>Флажки сообщения</summary>
  private IOEventFlags FEventFlags;
  /// <summary>Событие</summary>
  private IOEventType FEvent;
  /// <summary>
  /// Данные события. Трактуются в зависимости от значения Event:
  /// evKeyDown, evKeyUp: в поле хранится объект типа KeyEventArgs,
  /// evMouseClick, evMouseDoubleClick: в поле хранится объект типа MouseEventArgs или EventArgs
  /// </summary>
  private object FEventData;
  /// <summary>Пользовательские данные</summary>
  private object FTag;

  /// <summary>Создать заполненный экземпляр класса IOMessage</summary>
  /// <param name="ASource">Источник события</param>
  /// <param name="AEventFlags">Флажки сообщения</param>
  /// <param name="AEvent">Событие</param>
  /// <param name="AEventData">Данные события. Трактуются в зависимости от значения Event:
  /// evKeyDown, evKeyUp: в поле хранится объект типа KeyEventArgs,
  /// evMouseClick, evMouseDoubleClick: в поле хранится объект типа MouseEventArgs</param>
  /// <param name="ATag">Пользовательские данные</param>
  public IOEvent(
    IIOSource ASource,
    IOEventFlags AEventFlags,
    IOEventType AEvent,
    object AEventData,
    object ATag)
  {
    this.FSource = ASource;
    this.FEventFlags = AEventFlags;
    this.FEvent = AEvent;
    this.FEventData = AEventData;
    this.FTag = ATag;
  }

  /// <summary>Источник события</summary>
  public IIOSource Source => this.FSource;

  /// <summary>Флажки сообщения</summary>
  public IOEventFlags EventFlags
  {
    get => this.FEventFlags;
    set => this.FEventFlags = value;
  }

  /// <summary>Событие</summary>
  public IOEventType EventType => this.FEvent;

  /// <summary>
  /// Данные события. Трактуются в зависимости от значения Event:
  /// evKeyDown, evKeyUp: в поле хранится объект типа KeyEventArgs,
  /// evMouseClick, evMouseDoubleClick: в поле хранится объект типа MouseEventArgs
  /// </summary>
  public object EventData => this.FEventData;

  /// <summary>Пользовательские данные</summary>
  public object Tag
  {
    get => this.FTag;
    set => this.FTag = value;
  }
}
