// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JPEG2000.image.output.ImgWriterPGX
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.IO;

#nullable disable
namespace Syncfusion.Pdf.JPEG2000.image.output;

internal class ImgWriterPGX
{
  private int bitDepth;
  private byte[] buf;
  private int c;
  private DataBlockInt db = new DataBlockInt();
  private int fb;
  internal bool isSigned;
  internal int levShift;
  internal int maxVal;
  internal int minVal;
  private int offset;
  private FileStream out_Renamed;
  private int packBytes;
}
