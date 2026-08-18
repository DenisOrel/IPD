// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.CffGlyphs
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

internal class CffGlyphs
{
  private Dictionary<int, string> m_differenceEncoding = new Dictionary<int, string>();
  private double[] m_fontMatrix;
  private Dictionary<string, byte[]> m_glyphs = new Dictionary<string, byte[]>();
  private Dictionary<string, object> m_renderedPath = new Dictionary<string, object>();

  internal Dictionary<int, string> DifferenceEncoding
  {
    get => this.m_differenceEncoding;
    set => this.m_differenceEncoding = value;
  }

  internal double[] FontMatrix
  {
    get => this.m_fontMatrix;
    set => this.m_fontMatrix = value;
  }

  internal Dictionary<string, byte[]> Glyphs
  {
    get => this.m_glyphs;
    set => this.m_glyphs = value;
  }

  internal Dictionary<string, object> RenderedPath
  {
    get => this.m_renderedPath;
    set => this.m_renderedPath = value;
  }
}
