
// Type: Intermech.Interfaces.Show.IBlockTable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы с массивом блоков</summary>
    public interface IBlockTable : IEnumerable
    {
      /// <summary>длинна списка блоков</summary>
      int Length { get; }

      /// <summary>массив блоков</summary>
      IBlock[] Array { get; }

      /// <summary>получить по индексу в массиве сам блок</summary>
      /// <param name="index">индекс в массиве</param>
      /// <returns>блок</returns>
      IBlock this[int index] { get; }
    }
}
