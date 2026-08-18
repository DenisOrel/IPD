// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpJobStruct
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public class XmpJobStruct : XmpStructure
    {
      private const string c_id = "id";
      private const string c_name = "name";
      private const string c_prefix = "stJob";
      private const string c_structName = "http://ns.adobe.com/xap/1.0/sType/Job#";
      private const string c_url = "url";

      internal XmpJobStruct(
        XmpMetadata xmp,
        XmlNode parent,
        string prefix,
        string localName,
        string namespaceURI,
        bool insideArray)
        : base(xmp, parent, prefix, localName, namespaceURI, insideArray)
      {
      }

      protected override void InitializeEntities()
      {
      }

      public string ID
      {
        get => this.GetSimpleProperty("id").Value;
        set
        {
          this.GetSimpleProperty("id").Value = value != null ? value : throw new ArgumentNullException(nameof (ID));
        }
      }

      public string Name
      {
        get => this.GetSimpleProperty("name").Value;
        set
        {
          this.GetSimpleProperty("name").Value = value != null ? value : throw new ArgumentNullException(nameof (Name));
        }
      }

      protected override string StructurePrefix => "stJob";

      protected override string StructureURI => "http://ns.adobe.com/xap/1.0/sType/Job#";

      public Uri Url
      {
        get => this.GetSimpleProperty("url").GetUri();
        set
        {
          if (value == (Uri) null)
            throw new ArgumentNullException(nameof (Url));
          this.GetSimpleProperty("url").SetUri(value);
        }
      }
    }
}
