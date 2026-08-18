// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfUriAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfUriAction : PdfAction
{
  private string m_uri;

  public PdfUriAction() => this.m_uri = string.Empty;

  public PdfUriAction(string uri)
  {
    this.m_uri = string.Empty;
    this.Uri = uri;
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("URI"));
  }

  public string Uri
  {
    get => this.m_uri;
    set
    {
      if (value == null)
        throw new ArgumentNullException("uri");
      if (!(this.m_uri != value))
        return;
      this.m_uri = value;
      this.Dictionary.SetString("URI", this.m_uri);
    }
  }
}
