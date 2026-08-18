// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.TtfHeadTable
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal struct TtfHeadTable
{
  public long Modified;
  public long Created;
  public uint MagicNumber;
  public uint CheckSumAdjustment;
  public float FontRevision;
  public float Version;
  public short XMin;
  public short YMin;
  public ushort UnitsPerEm;
  public short YMax;
  public short XMax;
  public ushort MacStyle;
  public ushort Flags;
  public ushort LowestRecPPEM;
  public short FontDirectionHint;
  public short IndexToLocFormat;
  public short GlyphDataFormat;
}
