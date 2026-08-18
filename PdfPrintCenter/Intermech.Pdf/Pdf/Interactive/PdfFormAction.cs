// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfFormAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfFormAction : PdfAction
{
  private PdfFieldCollection m_fields;
  private bool m_include;

  public PdfFieldCollection Fields
  {
    get
    {
      if (this.m_fields == null)
      {
        this.m_fields = new PdfFieldCollection();
        this.Dictionary.SetProperty(nameof (Fields), (IPdfWrapper) this.m_fields);
      }
      return this.m_fields;
    }
  }

  public virtual bool Include
  {
    get => this.m_include;
    set => this.m_include = value;
  }
}
