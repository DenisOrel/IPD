// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfUriAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfUriAnnotation : PdfActionLinkAnnotation
{
  private PdfUriAction m_uriAction;

  public PdfUriAnnotation(RectangleF rectangle)
    : base(rectangle)
  {
    this.m_uriAction = new PdfUriAction();
  }

  public PdfUriAnnotation(RectangleF rectangle, string uri)
    : base(rectangle)
  {
    this.m_uriAction = new PdfUriAction();
    this.Uri = uri != null ? uri : throw new ArgumentNullException(nameof (uri));
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("Link"));
    this.Dictionary.SetProperty("A", (IPdfWrapper) this.m_uriAction);
  }

  public override PdfAction Action
  {
    get => base.Action;
    set
    {
      base.Action = value;
      this.m_uriAction.Next = value;
    }
  }

  public string Uri
  {
    get => this.m_uriAction.Uri;
    set
    {
      if (value == null)
        throw new ArgumentNullException("uri");
      if (!(this.m_uriAction.Uri != value))
        return;
      this.m_uriAction.Uri = value;
    }
  }
}
