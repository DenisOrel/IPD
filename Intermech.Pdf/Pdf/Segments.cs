// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Segments
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class Segments
    {
      private ushort endCode;
      private short idDelta;
      private ushort[] map;
      private ushort startCode;

      internal Segments()
      {
      }

      public Segments(ushort startCode, ushort endCode, short idDelta)
      {
        this.startCode = startCode;
        this.endCode = endCode;
        this.idDelta = idDelta;
      }

      public Segments(ushort startCode, ushort endCode, short idDelta, ushort[] mapval)
      {
        this.startCode = startCode;
        this.endCode = endCode;
        this.idDelta = idDelta;
        this.map = mapval;
      }

      public ushort GetGlyphId(ushort charCode)
      {
        if ((int) charCode >= (int) this.startCode && (int) charCode <= (int) this.endCode)
        {
          if (this.map == null)
            return (ushort) ((uint) charCode + (uint) (ushort) this.idDelta);
          int index = (int) charCode - (int) this.startCode;
          if (index <= this.map.Length && this.map[index] != (ushort) 0)
            return (ushort) ((uint) this.map[index] + (uint) (ushort) this.idDelta);
        }
        return 0;
      }

      public bool IsContain(ushort charCode) => (int) this.endCode >= (int) charCode;
    }
}
