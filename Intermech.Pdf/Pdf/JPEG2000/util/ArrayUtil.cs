// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.util.ArrayUtil
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.JPEG2000.util
{
    internal class ArrayUtil
    {
      public const int INIT_EL_COPYING = 4;
      public const int MAX_EL_COPYING = 8;

      public static void byteArraySet(byte[] arr, byte val)
      {
        int length = arr.Length;
        if (length < 8)
        {
          for (int index = length - 1; index >= 0; --index)
            arr[index] = val;
        }
        else
        {
          int num = length >> 1;
          int index;
          for (index = 0; index < 4; ++index)
            arr[index] = val;
          for (; index <= num; index <<= 1)
            Array.Copy((Array) arr, 0, (Array) arr, index, index);
          if (index >= length)
            return;
          Array.Copy((Array) arr, 0, (Array) arr, index, length - index);
        }
      }

      public static void intArraySet(int[] arr, int val)
      {
        int length = arr.Length;
        if (length < 8)
        {
          for (int index = length - 1; index >= 0; --index)
            arr[index] = val;
        }
        else
        {
          int num = length >> 1;
          int index;
          for (index = 0; index < 4; ++index)
            arr[index] = val;
          for (; index <= num; index <<= 1)
            Array.Copy((Array) arr, 0, (Array) arr, index, index);
          if (index >= length)
            return;
          Array.Copy((Array) arr, 0, (Array) arr, index, length - index);
        }
      }
    }
}
