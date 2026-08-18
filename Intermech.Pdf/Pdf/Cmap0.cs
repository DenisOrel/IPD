// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Cmap0
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf
{
    internal class Cmap0 : CmapTables
    {
      public byte[] glyphIdArray;
      private ushort m_firstcode;

      public override ushort GetGlyphId(ushort charCode)
      {
        return charCode >= (ushort) 0 && (int) charCode < this.glyphIdArray.Length ? (ushort) this.glyphIdArray[(int) charCode] : (ushort) 0;
      }

      public override void Read(ReadFontArray reader)
      {
        int num1 = (int) reader.getnextUshort();
        int num2 = (int) reader.getnextUshort();
        this.glyphIdArray = new byte[256 /*0x0100*/];
        for (int index = 0; index < 256 /*0x0100*/; ++index)
          this.glyphIdArray[index] = reader.getnextbyte();
      }

      public override ushort FirstCode => this.m_firstcode;
    }
}
