// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.BasicJobTicketSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml


namespace Syncfusion.Pdf.Xmp
{
    public class BasicJobTicketSchema : XmpSchema
    {
      private const string c_name = "http://ns.adobe.com/xap/1.0/bj/";
      private const string c_prefix = "xmpBJ";
      private const string c_propJobRef = "JobRef";

      protected internal BasicJobTicketSchema(XmpMetadata xmp)
        : base(xmp)
      {
      }

      public XmpArray JobRef => this.GetArray(nameof (JobRef), XmpArrayType.Bag);

      protected override string Name => "http://ns.adobe.com/xap/1.0/bj/";

      protected override string Prefix => "xmpBJ";

      public override XmpSchemaType SchemaType => XmpSchemaType.BasicJobTicketSchema;
    }
}
