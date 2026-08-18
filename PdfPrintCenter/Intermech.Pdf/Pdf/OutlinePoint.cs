// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.OutlinePoint
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf;

internal class OutlinePoint
{
  private byte m_flags;
  private PointF m_point;

  public OutlinePoint(byte flags) => this.m_flags = flags;

  public OutlinePoint(double x, double y, byte flags)
  {
    this.m_point = new PointF((float) x, (float) y);
    this.m_flags = flags;
  }

  public byte Flags
  {
    get => this.m_flags;
    set => this.m_flags = value;
  }

  public bool IsOnCurve => ((uint) this.Flags & 1U) > 0U;

  public PointF Point
  {
    get => this.m_point;
    set => this.m_point = value;
  }
}
