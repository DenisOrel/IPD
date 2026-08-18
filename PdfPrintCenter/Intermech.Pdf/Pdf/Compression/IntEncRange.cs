// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Compression.IntEncRange
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Compression;

internal class IntEncRange
{
  private int m_bits;
  private int m_bot;
  private int m_data;
  private short m_delta;
  private int m_intBits;
  private int m_top;

  internal IntEncRange(int bot, int top, int data, int bits, short delta, int intbits)
  {
    this.Bot = bot;
    this.Top = top;
    this.Data = data;
    this.Bits = bits;
    this.Delta = delta;
    this.IntBits = intbits;
  }

  internal int Bits
  {
    get => this.m_bits;
    set => this.m_bits = value;
  }

  internal int Bot
  {
    get => this.m_bot;
    set => this.m_bot = value;
  }

  internal int Data
  {
    get => this.m_data;
    set => this.m_data = value;
  }

  internal short Delta
  {
    get => this.m_delta;
    set => this.m_delta = value;
  }

  internal int IntBits
  {
    get => this.m_intBits;
    set => this.m_intBits = value;
  }

  internal int Top
  {
    get => this.m_top;
    set => this.m_top = value;
  }
}
