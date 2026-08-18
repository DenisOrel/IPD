
// Type: Intermech.Protection.RandomCache
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;
using System.Collections;


namespace Intermech.Protection
{
    internal static class RandomCache
    {
      private static Hashtable _cache = new Hashtable((IEqualityComparer) new RandomCache.BytesEqualityComparer());
      private static Random _randomizer = new Random();

      internal static void Add(byte[] query, byte[] reply, int sz)
      {
        if (RandomCache._cache.Contains((object) query))
          return;
        byte[] numArray = new byte[sz];
        byte[] dst = new byte[sz];
        Buffer.BlockCopy((Array) query, 0, (Array) numArray, 0, sz);
        Buffer.BlockCopy((Array) reply, 0, (Array) dst, 0, sz);
        if (RandomCache._cache.Count > 128 /*0x80*/)
        {
          int num = RandomCache._randomizer.Next(0, RandomCache._cache.Count - 2);
          IEnumerator enumerator = RandomCache._cache.Keys.GetEnumerator();
          enumerator.MoveNext();
          while (num-- > 0)
            enumerator.MoveNext();
          RandomCache._cache.Remove(enumerator.Current);
        }
        RandomCache._cache.Add((object) numArray, (object) dst);
      }

      internal static byte[] Get(byte[] query) => (byte[]) RandomCache._cache[(object) query];

      internal static int Count => RandomCache._cache.Count;

      internal static byte[] Get(int index)
      {
        IEnumerator enumerator = RandomCache._cache.Keys.GetEnumerator();
        enumerator.MoveNext();
        while (index-- > 0)
          enumerator.MoveNext();
        return (byte[]) enumerator.Current;
      }

      internal class BytesEqualityComparer : IEqualityComparer
      {
        bool IEqualityComparer.Equals(object x, object y)
        {
          byte[] numArray1 = (byte[]) x;
          byte[] numArray2 = (byte[]) y;
          int length = numArray1.Length;
          if (numArray2.Length != length)
            return false;
          for (int index = 0; index < length; ++index)
          {
            if ((int) numArray1[index] != (int) numArray2[index])
              return false;
          }
          return true;
        }

        int IEqualityComparer.GetHashCode(object obj)
        {
          int num1 = 367867417;
          int num2 = ~num1;
          byte[] numArray = (byte[]) obj;
          for (int length = numArray.Length; length > 0; length -= 2)
          {
            num1 = (num1 << 5) + num1 + (num1 >> 27) ^ (int) numArray[length - 1];
            if (length >= 2)
              num2 = (num2 << 7) + num2 + (num2 >> 26) ^ (int) numArray[length - 2];
            else
              break;
          }
          return num1 + num2 * 1566083941;
        }
      }
    }
}
