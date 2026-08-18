// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfAttachmentAnnotation
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;
using System.Drawing;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfAttachmentAnnotation : PdfFileAnnotation
{
  private PdfAttachmentIcon m_attachmentIcon;
  private PdfEmbeddedFileSpecification m_fileSpecification;

  public PdfAttachmentAnnotation(RectangleF rectangle, string fileName)
    : base(rectangle)
  {
    this.m_fileSpecification = fileName != null ? new PdfEmbeddedFileSpecification(fileName) : throw new ArgumentNullException(nameof (fileName));
  }

  public PdfAttachmentAnnotation(RectangleF rectangle, string fileName, byte[] data)
    : base(rectangle)
  {
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.m_fileSpecification = data != null ? new PdfEmbeddedFileSpecification(fileName, data) : throw new ArgumentNullException(nameof (data));
  }

  public PdfAttachmentAnnotation(RectangleF rectangle, string fileName, Stream stream)
    : base(rectangle)
  {
    if (fileName == null)
      throw new ArgumentNullException(nameof (fileName));
    this.m_fileSpecification = stream != null ? new PdfEmbeddedFileSpecification(fileName, stream) : throw new ArgumentNullException(nameof (stream));
  }

  protected override void Initialize()
  {
    base.Initialize();
    this.Dictionary.SetProperty("Subtype", (IPdfPrimitive) new PdfName("FileAttachment"));
  }

  protected override void Save()
  {
    base.Save();
    this.Dictionary.SetProperty("FS", (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) this.m_fileSpecification));
  }

  public override string FileName
  {
    get => this.m_fileSpecification.FileName;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(nameof (FileName));
        case "":
          throw new ArgumentException("FileName can't be empty");
        default:
          if (!(this.m_fileSpecification.FileName != value))
            break;
          this.m_fileSpecification.FileName = value;
          break;
      }
    }
  }

  public PdfAttachmentIcon Icon
  {
    get => this.m_attachmentIcon;
    set
    {
      this.m_attachmentIcon = value;
      this.Dictionary.SetName("Name", this.m_attachmentIcon.ToString());
    }
  }
}
