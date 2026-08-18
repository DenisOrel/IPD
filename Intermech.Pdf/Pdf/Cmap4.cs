// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Cmap4
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;


namespace Syncfusion.Pdf
{
    internal class Cmap4 : CmapTables
    {
      private ushort m_firstCode;
      private Segments[] segments;

      public override ushort GetGlyphId(ushort charCode)
      {
        foreach (Segments segment in this.segments)
        {
          if (segment.IsContain(charCode))
            return segment.GetGlyphId(charCode);
        }
        return 0;
      }

      public override void Read(ReadFontArray reader)
      {
        int num1 = (int) reader.getnextUshort();
        int num2 = (int) reader.getnextUshort();
        ushort length = (ushort) ((uint) reader.getnextUshort() / 2U);
        int num3 = (int) reader.getnextUshort();
        int num4 = (int) reader.getnextUshort();
        int num5 = (int) reader.getnextUshort();
        ushort[] numArray1 = new ushort[(int) length];
        ushort[] numArray2 = new ushort[(int) length];
        short[] numArray3 = new short[(int) length];
        ushort[] numArray4 = new ushort[(int) length];
        this.segments = new Segments[(int) length];
        this.m_firstCode = ushort.MaxValue;
        for (int index = 0; index < (int) length; ++index)
          numArray1[index] = reader.getnextUshort();
        int num6 = (int) reader.getnextUshort();
        for (int index = 0; index < (int) length; ++index)
        {
          numArray2[index] = reader.getnextUshort();
          if ((int) this.m_firstCode > (int) numArray2[index])
            this.m_firstCode = numArray2[index];
        }
        for (int index = 0; index < (int) length; ++index)
          numArray3[index] = reader.getnextshort();
        for (int index1 = 0; index1 < (int) length; ++index1)
        {
          long pointer1 = (long) reader.Pointer;
          numArray4[index1] = reader.getnextUshort();
          if (numArray4[index1] <= (ushort) 0)
          {
            this.segments[index1] = new Segments(numArray2[index1], numArray1[index1], numArray3[index1]);
          }
          else
          {
            int pointer2 = reader.Pointer;
            long num7 = pointer1 + (long) numArray4[index1];
            ushort[] mapval = new ushort[(int) numArray1[index1] - (int) numArray2[index1] + 1];
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            reader.Pointer = (int) num7;
            for (int index2 = 0; index2 < mapval.Length; ++index2)
              mapval[index2] = reader.getnextUshort();
            this.segments[index1] = new Segments(numArray2[index1], numArray1[index1], numArray3[index1], mapval);
            reader.Pointer = pointer2;
          }
        }
      }

      public override ushort FirstCode => this.m_firstCode;
    }
}
