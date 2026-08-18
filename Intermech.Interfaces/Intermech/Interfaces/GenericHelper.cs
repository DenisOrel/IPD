
// Type: Intermech.Interfaces.GenericHelper
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System.Collections.Generic;


namespace Intermech.Interfaces
{
    /// <summary>Helper для типизированных объектов</summary>
    public sealed class GenericHelper
    {
      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static bool ArrayEquals<T>(T[] a, T[] b)
      {
        if (a == null || b == null)
          return a == b;
        if (a.Length != b.Length)
          return false;
        EqualityComparer<T> equalityComparer = EqualityComparer<T>.Default;
        for (int index = 0; index < a.Length; ++index)
        {
          if (!equalityComparer.Equals(a[index], b[index]))
            return false;
        }
        return true;
      }
    }
}
