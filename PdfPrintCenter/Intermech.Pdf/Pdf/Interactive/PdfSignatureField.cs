// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfSignatureField
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Primitives;
using Syncfusion.Pdf.Security;
using System.Drawing;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfSignatureField : PdfSignatureAppearanceField
{
  private PdfSignature m_signature;

  internal PdfSignatureField()
  {
  }

  public PdfSignatureField(PdfPageBase page, string name)
    : base(page, name)
  {
  }

  internal override void Draw()
  {
    base.Draw();
    if (this.Widget.GetAppearance() == null)
      return;
    this.Page.Graphics.DrawPdfTemplate(this.Appearance.Normal, this.Location);
  }

  protected override void DrawAppearance(PdfTemplate template)
  {
    base.DrawAppearance(template);
    if (this.m_signature == null || !this.m_signature.DrawFieldAppearance)
      return;
    PaintParams paintParams = new PaintParams(new RectangleF(PointF.Empty, this.Size), this.BackBrush, (PdfBrush) null, this.BorderPen, this.BorderStyle, this.BorderWidth, this.ShadowBrush, this.RotationAngle);
    FieldPainter.DrawSignature(template.Graphics, paintParams);
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("FT", (IPdfPrimitive) new PdfName("Sig"));
  }

  internal override void Save()
  {
    base.Save();
    if (this.m_signature == null)
      return;
    this.Dictionary["V"] = (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) new PdfSignatureDictionary((PdfDocumentBase) ((PdfPage) this.Page).Document, this.m_signature, this.m_signature.Certificate));
  }

  public new PdfAppearance Appearance => this.Widget.Appearance;

  public PdfSignature Signature
  {
    get => this.m_signature;
    set => this.m_signature = value;
  }
}
