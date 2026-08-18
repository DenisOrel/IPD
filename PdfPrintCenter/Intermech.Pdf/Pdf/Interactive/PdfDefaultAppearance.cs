// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfDefaultAppearance
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using System.Text;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

internal class PdfDefaultAppearance
{
  private string m_fontName = string.Empty;
  private float m_fontSize;
  private PdfColor m_foreColor = new PdfColor((byte) 0, (byte) 0, (byte) 0);

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("/");
    stringBuilder.Append(this.FontName);
    stringBuilder.Append(" ");
    stringBuilder.Append(this.m_fontSize.ToString());
    stringBuilder.Append(" ");
    stringBuilder.Append("Tf");
    stringBuilder.Append(" ");
    stringBuilder.Append(this.m_foreColor.ToString(PdfColorSpace.RGB, false));
    return stringBuilder.ToString();
  }

  public string FontName
  {
    get => this.m_fontName;
    set
    {
      if (!(this.m_fontName != value))
        return;
      this.m_fontName = value;
    }
  }

  public float FontSize
  {
    get => this.m_fontSize;
    set
    {
      if ((double) this.m_fontSize == (double) value)
        return;
      this.m_fontSize = value;
    }
  }

  public PdfColor ForeColor
  {
    get => this.m_foreColor;
    set
    {
      if (!(this.m_foreColor != value))
        return;
      this.m_foreColor = value;
    }
  }
}
