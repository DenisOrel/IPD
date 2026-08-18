// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.ReferenceFileSpecification
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Primitives;
using System;
using System.IO;

#nullable disable
namespace Syncfusion.Pdf;

internal class ReferenceFileSpecification : PdfFileSpecificationBase
{
  private string m_fileName;
  private PdfFilePathType m_path;

  internal ReferenceFileSpecification(string fileName)
    : base(fileName)
  {
    this.m_fileName = string.Empty;
    this.m_fileName = fileName;
  }

  public ReferenceFileSpecification(string fileName, PdfFilePathType path)
    : base(fileName)
  {
    this.m_fileName = string.Empty;
    this.m_path = path;
    this.FileName = fileName;
  }

  protected override void Save()
  {
    this.Dictionary.SetProperty("UF", (IPdfPrimitive) new PdfString(this.FormatFileName(this.FileName, this.m_path == PdfFilePathType.Relative)));
  }

  public override string FileName
  {
    get => this.m_fileName;
    set
    {
      switch (value)
      {
        case null:
          throw new ArgumentNullException(nameof (FileName));
        case "":
          throw new ArgumentException("FileName can't be empty");
        default:
          if (this.m_path == PdfFilePathType.Absolute)
          {
            this.m_fileName = Path.GetFullPath(value);
            break;
          }
          if (this.m_path != PdfFilePathType.Relative)
            break;
          this.m_fileName = value;
          break;
      }
    }
  }
}
