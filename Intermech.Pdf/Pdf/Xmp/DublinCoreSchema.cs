// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.DublinCoreSchema
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;


namespace Syncfusion.Pdf.Xmp
{
    public class DublinCoreSchema : XmpSchema
    {
      private const string c_contributor = "contributor";
      private const string c_coverage = "coverage";
      private const string c_creator = "creator";
      private const string c_date = "date";
      private const string c_description = "description";
      private const string c_format = "format";
      private const string c_identifier = "identifier";
      private const string c_mimeType = "application/pdf";
      private const string c_name = "http://purl.org/dc/elements/1.1/";
      private const string c_prefix = "dc";
      private const string c_publisher = "publisher";
      private const string c_relation = "relation";
      private const string c_rights = "rights";
      private const string c_source = "source";
      private const string c_subject = "subject";
      private const string c_title = "title";
      private const string c_type = "type";

      protected internal DublinCoreSchema(XmpMetadata xmp)
        : base(xmp)
      {
      }

      protected override void CreateEntity()
      {
        base.CreateEntity();
        this.GetSimpleProperty("format").Value = "application/pdf";
      }

      public XmpArray Contributor => this.GetArray("contributor", XmpArrayType.Bag);

      public string Coverage
      {
        get => this.GetSimpleProperty("coverage").Value;
        set
        {
          this.GetSimpleProperty("coverage").Value = value != null ? value : throw new ArgumentNullException(nameof (Coverage));
        }
      }

      public XmpArray Creator
      {
        get
        {
          return this.XmlData.InnerXml.Contains("rdf:Bag") ? this.GetArray("creator", XmpArrayType.Bag) : this.GetArray("creator", XmpArrayType.Seq);
        }
      }

      public XmpArray Date => this.GetArray("date", XmpArrayType.Seq);

      public XmpLangArray Description => this.GetLangArray("description");

      public string Identifier
      {
        get => this.GetSimpleProperty("identifier").Value;
        set
        {
          this.GetSimpleProperty("identifier").Value = value != null ? value : throw new ArgumentNullException(nameof (Identifier));
        }
      }

      protected override string Name => "http://purl.org/dc/elements/1.1/";

      protected override string Prefix => "dc";

      public XmpArray Publisher => this.GetArray("publisher", XmpArrayType.Bag);

      public XmpArray Relation => this.GetArray("relation", XmpArrayType.Bag);

      public XmpLangArray Rights => this.GetLangArray("rights");

      public override XmpSchemaType SchemaType => XmpSchemaType.DublinCoreSchema;

      public string Source
      {
        get => this.GetSimpleProperty("source").Value;
        set
        {
          this.GetSimpleProperty("source").Value = value != null ? value : throw new ArgumentNullException(nameof (Source));
        }
      }

      public XmpArray Sublect => this.GetArray("subject", XmpArrayType.Bag);

      public XmpLangArray Title => this.GetLangArray("title");

      public XmpArray Type => this.GetArray("type", XmpArrayType.Bag);
    }
}
