// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.TtfMetrics
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Native;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal struct TtfMetrics
{
  public int LineGap;
  public bool ContainsCFF;
  public bool IsSymbol;
  public RECT FontBox;
  public bool IsFixedPitch;
  public float ItalicAngle;
  public string PostScriptName;
  public string FontFamily;
  public float CapHeight;
  public float Leading;
  public float MacAscent;
  public float MacDescent;
  public float WinDescent;
  public float WinAscent;
  public float StemV;
  public int[] WidthTable;
  public int MacStyle;
  public float SubScriptSizeFactor;
  public float SuperscriptSizeFactor;

  public bool IsItalic => (this.MacStyle & 2) != 0;

  public bool IsBold => (this.MacStyle & 1) != 0;
}
