// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Maxp
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf;

internal class Maxp : TableBase
{
  private int m_id;
  private ushort m_numGlyphs;
  private float m_version;

  public Maxp(FontFile2 fontsource)
    : base(fontsource)
  {
    this.m_id = 1;
  }

  public override void Read(ReadFontArray reader)
  {
    this.m_version = (float) reader.getnextshort();
    this.m_version = this.Version + (float) ((int) reader.getnextUshort() / 65536 /*0x010000*/);
    this.m_numGlyphs = reader.getnextUshort();
  }

  internal override int Id => this.m_id;

  public ushort NumGlyphs
  {
    get => this.m_numGlyphs;
    private set => this.m_numGlyphs = value;
  }

  public float Version
  {
    get => this.m_version;
    private set => this.m_version = value;
  }
}
