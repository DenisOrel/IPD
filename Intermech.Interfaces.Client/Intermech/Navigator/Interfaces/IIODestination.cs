// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IIODestination
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>Интерфейс обработчика событий</summary>
public interface IIODestination
{
  /// <summary>Список поддерживаемых обработчиком событий</summary>
  IOEventTypes SupportedEvents { get; set; }

  /// <summary>Выполнить обработку события</summary>
  /// <param name="Event">Событие</param>
  /// <returns>true, если обработка выполнена успешно, false, если событие не обработано</returns>
  bool ProcessEvent(IIOEvent Event);
}
