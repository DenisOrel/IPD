// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.PDFSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Xmp
{
    public class PDFSchema : XmpSchema
    {
      private const string c_Keywords = "Keywords";
      private const string c_name = "http://ns.adobe.com/pdf/1.3/";
      private const string c_PDFVersion = "PDFVersion";
      private const string c_prefix = "pdf";
      private const string c_Producer = "Producer";

      protected internal PDFSchema(XmpMetadata xmp)
        : base(xmp)
      {
      }

      public string Keywords
      {
        get => this.GetSimpleProperty(nameof (Keywords)).Value;
        set
        {
          this.GetSimpleProperty(nameof (Keywords)).Value = value != null ? value : throw new ArgumentNullException(nameof (Keywords));
        }
      }

      protected override string Name => "http://ns.adobe.com/pdf/1.3/";

      public string PDFVersion
      {
        get => this.GetSimpleProperty(nameof (PDFVersion)).Value;
        set
        {
          this.GetSimpleProperty(nameof (PDFVersion)).Value = value != null ? value : throw new ArgumentNullException(nameof (PDFVersion));
        }
      }

      protected override string Prefix => "pdf";

      public string Producer
      {
        get => this.GetSimpleProperty(nameof (Producer)).Value;
        set
        {
          this.GetSimpleProperty(nameof (Producer)).Value = value != null ? value : throw new ArgumentNullException(nameof (Producer));
        }
      }

      public override XmpSchemaType SchemaType => XmpSchemaType.PDFSchema;
    }
}
