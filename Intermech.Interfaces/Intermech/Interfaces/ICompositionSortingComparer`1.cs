
// Type: Intermech.Interfaces.ICompositionSortingComparer`1
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>
    /// Интерфейс для сортировки элементов согласно заданному правилу и направлению
    /// </summary>
    public interface ICompositionSortingComparer<in T> : IComparer<T>
    {
      /// <summary>Правило сортировки</summary>
      CompositionsAutosortRule SortingRule { get; }

      /// <summary>Направление сортировки</summary>
      CompositionSortingDirectionMode DirectionMode { get; }
    }
}
