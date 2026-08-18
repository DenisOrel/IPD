
// Type: Intermech.Protection.BinHexEncoder
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using System;


namespace Intermech.Protection
{
    internal class BinHexEncoder
    {
      private const string _hexDigits = "0123456789ABCDEF";

      internal static string EncodeToBinHex(byte[] inArray)
      {
        return BinHexEncoder.EncodeToBinHex(inArray, 0, inArray.Length);
      }

      internal static string EncodeToBinHex(byte[] inArray, int offsetIn, int count)
      {
        if (inArray == null)
          throw new ArgumentNullException(nameof (inArray));
        if (0 > offsetIn)
          throw new ArgumentOutOfRangeException(nameof (offsetIn));
        if (0 > count)
          throw new ArgumentOutOfRangeException(nameof (count));
        if (count > inArray.Length - offsetIn)
          throw new ArgumentException("count > inArray.Length - offsetIn");
        char[] outArray = new char[2 * count];
        int binHex = BinHexEncoder.EncodeToBinHex(inArray, offsetIn, count, outArray);
        return new string(outArray, 0, binHex);
      }

      private static int EncodeToBinHex(byte[] inArray, int offsetIn, int count, char[] outArray)
      {
        int num1 = 0;
        int num2 = 0;
        int length = outArray.Length;
        for (int index = 0; index < count; ++index)
        {
          byte num3 = inArray[offsetIn++];
          outArray[num1++] = "0123456789ABCDEF"[(int) num3 >> 4];
          if (num1 != length)
          {
            outArray[num1++] = "0123456789ABCDEF"[(int) num3 & 15];
            if (num1 == length)
              break;
          }
          else
            break;
        }
        return num1 - num2;
      }
    }
}
