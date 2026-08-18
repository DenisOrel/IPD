
// Type: Intermech.IO.PathDictionary`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.Collections.Generic;


namespace Intermech.IO
{
    /// <summary>Реализует словарь, где ключем выступает имя файла.</summary>
    /// <typeparam name="TValue">Тип значений в словаре</typeparam>
    public class PathDictionary<TValue> : Dictionary<string, TValue>
    {
      /// <summary>Создает объект.</summary>
      public PathDictionary()
        : base((IEqualityComparer<string>) new PathComparer())
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="capacity">Начальная емкость словаря</param>
      public PathDictionary(int capacity)
        : base(capacity, (IEqualityComparer<string>) new PathComparer())
      {
      }
    }
}
