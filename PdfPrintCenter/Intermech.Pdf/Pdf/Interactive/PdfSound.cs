// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfSound
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfSound : IPdfWrapper
{
  private int m_bits;
  private PdfSoundChannels m_channels;
  private PdfSoundEncoding m_encoding;
  private string m_fileName;
  private int m_rate;
  private PdfStream m_stream;

  internal PdfSound()
  {
    this.m_rate = 22050;
    this.m_channels = PdfSoundChannels.Mono;
    this.m_bits = 8;
    this.m_fileName = string.Empty;
    this.m_stream = new PdfStream();
    this.m_stream.SetProperty("R", (IPdfPrimitive) new PdfNumber(this.m_rate));
    this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
  }

  public PdfSound(string fileName)
  {
    this.m_rate = 22050;
    this.m_channels = PdfSoundChannels.Mono;
    this.m_bits = 8;
    this.m_fileName = string.Empty;
    this.m_stream = new PdfStream();
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    Utils.CheckFilePath(fileName);
    this.FileName = fileName;
    this.m_stream.SetString("T", fileName);
    this.m_stream.SetProperty("R", (IPdfPrimitive) new PdfNumber(this.m_rate));
    this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
  }

  internal PdfSound(string fileName, bool test)
  {
    this.m_rate = 22050;
    this.m_channels = PdfSoundChannels.Mono;
    this.m_bits = 8;
    this.m_fileName = string.Empty;
    this.m_stream = new PdfStream();
    this.FileName = fileName != null ? fileName : throw new ArgumentNullException(nameof (fileName));
    this.m_stream.SetString("T", fileName);
    this.m_stream.SetProperty("R", (IPdfPrimitive) new PdfNumber(this.m_rate));
    this.m_stream.BeginSave += new SavePdfPrimitiveEventHandler(this.Stream_BeginSave);
  }

  protected void Save()
  {
    using (FileStream fileStream = new FileStream(this.FileName, FileMode.Open, FileAccess.Read))
    {
      byte[] bigEndian = PdfStream.StreamToBigEndian((Stream) fileStream);
      this.m_stream.Clear();
      this.m_stream.InternalStream.Write(bigEndian, 0, bigEndian.Length);
    }
  }

  private void Stream_BeginSave(object sender, SavePdfPrimitiveEventArgs ars) => this.Save();

  public int Bits
  {
    get => this.m_bits;
    set
    {
      if (this.m_bits == value)
        return;
      this.m_bits = value;
      this.m_stream.SetNumber("B", this.m_bits);
    }
  }

  public PdfSoundChannels Channels
  {
    get => this.m_channels;
    set
    {
      if (this.m_channels == value)
        return;
      this.m_channels = value;
      this.m_stream.SetNumber("C", (int) this.m_channels);
    }
  }

  public PdfSoundEncoding Encoding
  {
    get => this.m_encoding;
    set
    {
      if (this.m_encoding == value)
        return;
      this.m_encoding = value;
      this.m_stream.SetName("E", this.m_encoding.ToString());
    }
  }

  public string FileName
  {
    get => this.m_fileName;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(nameof (FileName));
        case "":
          throw new ArithmeticException("FileName can't be empty string.");
        default:
          this.m_fileName = Path.GetFullPath(value);
          break;
      }
    }
  }

  public int Rate
  {
    get => this.m_rate;
    set
    {
      if (this.m_rate == value)
        return;
      this.m_rate = value;
      this.m_stream.SetNumber("R", this.m_rate);
    }
  }

  IPdfPrimitive IPdfWrapper.Element => (IPdfPrimitive) this.m_stream;
}
