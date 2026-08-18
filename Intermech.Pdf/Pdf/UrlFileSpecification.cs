// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.UrlFileSpecification
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Primitives;
using System;


namespace Syncfusion.Pdf
{
    internal class UrlFileSpecification : PdfFileSpecificationBase
    {
      private string m_fileName;

      public UrlFileSpecification(string fileName)
        : base(fileName)
      {
        this.m_fileName = string.Empty;
      }

      protected override void Initialize()
      {
        base.Initialize();
        this.Dictionary.SetProperty("FS", (IPdfPrimitive) new PdfName("URL"));
      }

      protected override void Save()
      {
        this.Dictionary.SetProperty("F", (IPdfPrimitive) new PdfString(this.FileName));
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
              if (!(this.m_fileName != value))
                break;
              this.m_fileName = value;
              break;
          }
        }
      }
    }
}
