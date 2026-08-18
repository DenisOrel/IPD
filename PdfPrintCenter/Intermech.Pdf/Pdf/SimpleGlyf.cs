// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.SimpleGlyf
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

internal class SimpleGlyf(FontFile2 fontFile, ushort glyphIndex) : TrueTypeGlyphs(fontFile, glyphIndex)
{
  private List<OutlinePoint[]> contours;

  internal static bool GetBit(int n, byte bit) => (n & 1 << (int) bit) != 0;

  public override void Read(ReadFontArray reader)
  {
    int num1 = (int) reader.getnextshort();
    int num2 = (int) reader.getnextshort();
    int num3 = (int) reader.getnextshort();
    int num4 = (int) reader.getnextshort();
    ushort[] numArray1 = new ushort[(int) this.NumberOfContours];
    for (int index = 0; index < (int) this.NumberOfContours; ++index)
      numArray1[index] = reader.getnextUshort();
    int length1 = (int) numArray1[(int) this.NumberOfContours - 1] + 1;
    ushort length2 = reader.getnextUshort();
    byte[] numArray2 = new byte[(int) length2];
    for (int index = 0; index < (int) length2; ++index)
      numArray2[index] = reader.getnextbyte();
    byte[] flags = new byte[length1];
    for (int index1 = 0; index1 < length1; ++index1)
    {
      flags[index1] = reader.getnextbyte();
      if (SimpleGlyf.Repeat(flags, index1))
      {
        byte num5 = flags[index1];
        byte num6 = reader.getnextbyte();
        for (int index2 = 0; index2 < (int) num6; ++index2)
          flags[++index1] = num5;
      }
    }
    int[] numArray3 = new int[length1];
    for (int index = 0; index < length1; ++index)
    {
      if (index > 0)
        numArray3[index] = numArray3[index - 1];
      if (SimpleGlyf.XIsByte(flags, index))
      {
        int num7 = (int) reader.getnextbyte();
        if (!SimpleGlyf.XIsSame(flags, index))
          num7 = -num7;
        numArray3[index] += num7;
      }
      else if (!SimpleGlyf.XIsSame(flags, index))
        numArray3[index] += (int) reader.getnextshort();
    }
    int[] numArray4 = new int[length1];
    for (int index = 0; index < length1; ++index)
    {
      if (index > 0)
        numArray4[index] = numArray4[index - 1];
      if (SimpleGlyf.YIsByte(flags, index))
      {
        int num8 = (int) reader.getnextbyte();
        if (!SimpleGlyf.YIsSame(flags, index))
          num8 = -num8;
        numArray4[index] += num8;
      }
      else if (!SimpleGlyf.YIsSame(flags, index))
        numArray4[index] += (int) reader.getnextshort();
    }
    this.contours = new List<OutlinePoint[]>();
    int index3 = 0;
    int num9 = 0;
    OutlinePoint[] outlinePointArray = new OutlinePoint[(int) numArray1[0] + 1];
    this.contours.Add(outlinePointArray);
    for (int index4 = 0; index4 < length1; ++index4)
    {
      outlinePointArray[num9++] = new OutlinePoint((double) numArray3[index4], (double) numArray4[index4], flags[index4]);
      if (index4 == (int) numArray1[index3])
      {
        if (index3 == numArray1.Length - 1)
          break;
        ++index3;
        outlinePointArray = new OutlinePoint[(int) numArray1[index3] - (int) numArray1[index3 - 1]];
        this.contours.Add(outlinePointArray);
        num9 = 0;
      }
    }
  }

  private static bool Repeat(byte[] flags, int index)
  {
    return SimpleGlyf.GetBit((int) flags[index], (byte) 3);
  }

  private static bool XIsByte(byte[] flags, int index)
  {
    return SimpleGlyf.GetBit((int) flags[index], (byte) 1);
  }

  private static bool XIsSame(byte[] flags, int index)
  {
    return SimpleGlyf.GetBit((int) flags[index], (byte) 4);
  }

  private static bool YIsByte(byte[] flags, int index)
  {
    return SimpleGlyf.GetBit((int) flags[index], (byte) 2);
  }

  private static bool YIsSame(byte[] flags, int index)
  {
    return SimpleGlyf.GetBit((int) flags[index], (byte) 5);
  }

  internal new List<OutlinePoint[]> Contours
  {
    get
    {
      if (this.contours == null)
        this.contours = new List<OutlinePoint[]>();
      return this.contours;
    }
  }
}
