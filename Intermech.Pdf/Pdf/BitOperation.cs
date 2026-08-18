// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.BitOperation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class BitOperation
    {
      public const int LEFT_SHIFT = 0;
      public const int RIGHT_SHIFT = 1;

      public long Bit32Shift(long number, int shift, int direction)
      {
        if (direction == 0)
          number <<= shift;
        else
          number >>= shift;
        long maxValue = (long) uint.MaxValue;
        return number & maxValue;
      }

      public int Bit8Shift(int number, int shift, int direction)
      {
        if (direction == 0)
          number <<= shift;
        else
          number >>= shift;
        int maxValue = (int) byte.MaxValue;
        return number & maxValue;
      }

      public int GetInt16(short[] number) => (int) number[0] << 8 | (int) number[1];

      public int GetInt32(short[] number)
      {
        return (int) number[0] << 24 | (int) number[1] << 16 /*0x10*/ | (int) number[2] << 8 | (int) number[3];
      }
    }
}
