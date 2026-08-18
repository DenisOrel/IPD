// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.io.BinaryDataOutput
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.JPEG2000.io
{
    internal interface BinaryDataOutput
    {
      void flush();

      void writeByte(int v);

      void writeDouble(double v);

      void writeFloat(float v);

      void writeInt(int v);

      void writeLong(long v);

      void writeShort(int v);

      int ByteOrdering { get; }
    }
}
