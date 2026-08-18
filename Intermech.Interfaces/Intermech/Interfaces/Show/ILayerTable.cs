
// Type: Intermech.Interfaces.Show.ILayerTable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы с массивом слоёв</summary>
    public interface ILayerTable : IEnumerable
    {
      /// <summary>длинна массива слоёв</summary>
      int Length { get; }

      ILayer[] Array { get; }

      /// <summary>получить по индексу сам слой</summary>
      /// <param name="index">индекс в массиве</param>
      /// <returns>слой</returns>
      ILayer this[int index] { get; }

      /// <summary>пересчитать границы включённых слоёв</summary>
      RectangleD Bounds { get; }

      /// <summary>габариты при всех слоях</summary>
      RectangleD BoundsAll { get; }
    }
}
