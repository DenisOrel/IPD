
// Type: Intermech.Interfaces.EventArgs`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces
{
    /// <summary>объет содержащий данные события</summary>
    /// <typeparam name="T">тип данных события</typeparam>
    public sealed class EventArgs<T> : EventArgs
    {
      /// <summary>конструктор</summary>
      /// <param name="value">данные события</param>
      public EventArgs(T value) => this.Value = value;

      /// <summary>данные события</summary>
      public T Value { get; private set; }
    }
}
