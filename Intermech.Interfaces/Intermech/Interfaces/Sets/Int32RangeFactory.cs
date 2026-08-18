
// Type: Intermech.Interfaces.Sets.Int32RangeFactory
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml


namespace Intermech.Interfaces.Sets
{
    /// <summary>
    /// Класс, позволяющий создавать диапазоны множеств чисел IRange
    /// </summary>
    public sealed class Int32RangeFactory : IRangeFactory<int>
    {
      /// <summary>Экземпляр класса</summary>
      public static Int32RangeFactory Factory = new Int32RangeFactory();

      /// <summary>Создать пустой экземпляр класса</summary>
      /// <returns>Пустой экземпляр класса</returns>
      public IRange<int> Create() => (IRange<int>) new Int32Range();

      /// <summary>Создать и заполнить экземпляр класса</summary>
      /// <param name="source">Объект-источник</param>
      /// <returns>Заполненный экземпляр класса</returns>
      public IRange<int> Create(object source) => (IRange<int>) new Int32Range(source);
    }
}
