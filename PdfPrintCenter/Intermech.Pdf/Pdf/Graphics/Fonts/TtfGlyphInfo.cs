// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.Fonts.TtfGlyphInfo
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics.Fonts;

internal struct TtfGlyphInfo : IComparable
{
  public int Index;
  public int Width;
  public int CharCode;

  public bool Empty
  {
    get => this.Index == this.Width && this.Width == this.CharCode && this.CharCode == 0;
  }

  public int CompareTo(object obj) => this.Index - ((TtfGlyphInfo) obj).Index;
}
