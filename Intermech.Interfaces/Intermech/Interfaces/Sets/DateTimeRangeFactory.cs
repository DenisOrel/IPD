
// Type: Intermech.Interfaces.Sets.DateTimeRangeFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Interfaces.Sets
{
    public sealed class DateTimeRangeFactory : IRangeFactory<DateTime>
    {
      /// <summary>Экземпляр класса</summary>
      public static DateTimeRangeFactory Factory = new DateTimeRangeFactory();

      /// <summary>Создать пустой экземпляр класса</summary>
      /// <returns>Пустой экземпляр класса</returns>
      public IRange<DateTime> Create() => (IRange<DateTime>) new DateTimeRange();

      /// <summary>Создать и заполнить экземпляр класса</summary>
      /// <param name="source">Объект-источник</param>
      /// <returns>Заполненный экземпляр класса</returns>
      public IRange<DateTime> Create(object source) => (IRange<DateTime>) new DateTimeRange(source);
    }
}
