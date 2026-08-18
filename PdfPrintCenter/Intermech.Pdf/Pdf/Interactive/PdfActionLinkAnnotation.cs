// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfActionLinkAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public abstract class PdfActionLinkAnnotation : PdfLinkAnnotation
{
  private PdfAction m_action;

  public PdfActionLinkAnnotation(RectangleF rectangle)
    : base(rectangle)
  {
  }

  public PdfActionLinkAnnotation(RectangleF rectangle, PdfAction action)
    : base(rectangle)
  {
    this.m_action = action != null ? action : throw new ArgumentNullException(nameof (action));
  }

  public virtual PdfAction Action
  {
    get => this.m_action;
    set => this.m_action = value != null ? value : throw new ArgumentNullException(nameof (Action));
  }
}
