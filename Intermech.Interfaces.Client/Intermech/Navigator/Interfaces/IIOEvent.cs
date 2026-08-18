// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IIOEvent
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс события</summary>
public interface IIOEvent : IIOSourceInfo
{
  /// <summary>Флажки сообщения</summary>
  IOEventFlags EventFlags { get; set; }

  /// <summary>Событие</summary>
  IOEventType EventType { get; }

  /// <summary>
  /// Данные события. Трактуются в зависимости от значения Event:
  /// evKeyDown, evKeyUp: в поле хранится объект типа KeyEventArgs,
  /// evMouseClick, evMouseDoubleClick: в поле хранится объект типа MouseEventArgs или EventArgs,
  /// evDragDrop*: в поле хранится объект типа DragEventArgs
  /// </summary>
  object EventData { get; }

  /// <summary>Пользовательские данные</summary>
  object Tag { get; set; }
}
