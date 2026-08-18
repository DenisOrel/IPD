
// Type: Intermech.Interfaces.Show.ILayoutTable
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections;


namespace Intermech.Interfaces.Show
{
    /// <summary>интерфейс работы с массивом компоновок</summary>
    public interface ILayoutTable : IEnumerable
    {
      /// <summary>длинна массива компоновок</summary>
      int Length { get; }

      /// <summary>компоновка	с которой сохранён чертёж</summary>
      ILayout InFile { get; }

      /// <summary>массив компоновок</summary>
      ILayout[] Array { get; }

      /// <summary>получить по индексу в массиве саму компоновку</summary>
      /// <param name="index">индекс в таблице</param>
      /// <returns>компоновка</returns>
      ILayout this[int index] { get; }

      /// <summary>получить по компоновке индекс в массиве</summary>
      /// <param name="vitem">компоновка</param>
      /// <returns>индекс в массиве</returns>
      int this[ILayout vitem] { get; }
    }
}
