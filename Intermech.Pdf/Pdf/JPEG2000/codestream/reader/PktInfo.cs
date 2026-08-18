// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.codestream.reader.PktInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.codestream.reader
{
    internal class PktInfo
    {
      public int cbLength;
      public int cbOff;
      public int layerIdx;
      public int numTruncPnts;
      public int packetIdx;
      public int[] segLengths;

      public PktInfo(int lyIdx, int pckIdx)
      {
        this.layerIdx = lyIdx;
        this.packetIdx = pckIdx;
      }

      public override string ToString()
      {
        return $"packet {(object) this.packetIdx} (lay:{(object) this.layerIdx}, off:{(object) this.cbOff}, len:{(object) this.cbLength}, numTruncPnts:{(object) this.numTruncPnts})\n";
      }
    }
}
