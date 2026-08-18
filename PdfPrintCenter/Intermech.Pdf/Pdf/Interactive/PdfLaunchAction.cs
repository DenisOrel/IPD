// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfLaunchAction
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfLaunchAction : PdfAction
{
  private ReferenceFileSpecification m_fileSpecification;
  private PdfFilePathType m_pathType;

  public PdfLaunchAction(string fileName)
  {
    this.m_pathType = PdfFilePathType.Absolute;
    this.m_fileSpecification = fileName != null ? new ReferenceFileSpecification(fileName, this.m_pathType) : throw new ArgumentNullException(nameof (fileName));
  }

  public PdfLaunchAction(string fileName, PdfFilePathType path)
  {
    this.m_pathType = PdfFilePathType.Absolute;
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.m_pathType = path;
    this.m_fileSpecification = new ReferenceFileSpecification(fileName, this.m_pathType);
  }

  internal PdfLaunchAction(string fileName, bool loaded)
  {
    this.m_pathType = PdfFilePathType.Absolute;
    if (!loaded)
      return;
    this.m_fileSpecification = fileName != null ? new ReferenceFileSpecification(fileName) : throw new ArgumentNullException(nameof (fileName));
  }

  private void Dictionary_BeginSave(object sender, SavePdfPrimitiveEventArgs ars)
  {
    this.Dictionary.SetProperty("F", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_fileSpecification));
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.BeginSave += new SavePdfPrimitiveEventHandler(this.Dictionary_BeginSave);
    this.Dictionary.SetProperty("S", (IPdfPrimitive) new PdfName("Launch"));
  }

  public string FileName
  {
    get => this.m_fileSpecification.FileName;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(nameof (FileName));
        case "":
          throw new ArgumentException("File name can not be empty");
        default:
          if (!(this.m_fileSpecification.FileName != value))
            break;
          this.m_fileSpecification.FileName = value;
          break;
      }
    }
  }
}
