// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Graphics.PdfMargins
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;

#nullable disable
namespace Syncfusion.Pdf.Graphics;

public class PdfMargins : ICloneable
{
  private float m_bottom;
  private float m_left;
  private float m_right;
  private float m_top;
  private const float PageMargin = 0.0f;

  public PdfMargins() => this.SetMargins(0.0f);

  public object Clone() => (object) (PdfMargins) this.MemberwiseClone();

  internal void SetMargins(float margin)
  {
    this.m_left = this.m_top = this.m_right = this.m_bottom = margin;
  }

  internal void SetMargins(float leftRight, float topBottom)
  {
    this.m_left = this.m_right = leftRight;
    this.m_top = this.m_bottom = topBottom;
  }

  internal void SetMargins(float left, float top, float right, float bottom)
  {
    this.m_left = left;
    this.m_top = top;
    this.m_right = right;
    this.m_bottom = bottom;
  }

  public float All
  {
    set => this.SetMargins(value);
  }

  public float Bottom
  {
    get => this.m_bottom;
    set => this.m_bottom = value;
  }

  public float Left
  {
    get => this.m_left;
    set => this.m_left = value;
  }

  public float Right
  {
    get => this.m_right;
    set => this.m_right = value;
  }

  public float Top
  {
    get => this.m_top;
    set => this.m_top = value;
  }
}
