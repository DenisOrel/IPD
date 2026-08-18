
// Type: Intermech.Interfaces.Sets.IRangeFactory`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Sets
{
    /// <summary>
    /// Интерфейс фабрики, позволяющей создавать экземпляры классов IRange[T]
    /// </summary>
    /// <typeparam name="T">Общий тип</typeparam>
    public interface IRangeFactory<T>
    {
      /// <summary>Создать пустой экземпляр класса IRange[T]</summary>
      /// <returns>Пустой экземпляр класса IRange[T]</returns>
      IRange<T> Create();

      /// <summary>Создать и заполнить экземпляр класса IRange[T]</summary>
      /// <param name="source">Объект-источник</param>
      /// <returns>Экземпляр класса IRange[T]</returns>
      IRange<T> Create(object source);
    }
}
