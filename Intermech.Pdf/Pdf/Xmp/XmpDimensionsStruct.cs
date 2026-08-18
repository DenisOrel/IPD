// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpDimensionsStruct
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public class XmpDimensionsStruct : XmpStructure
    {
      private const string c_height = "h";
      private const string c_name = "http://ns.adobe.com/xap/1.0/sType/Dimensions#";
      private const string c_prefix = "stDim";
      private const string c_unit = "unit";
      private const string c_width = "w";

      internal XmpDimensionsStruct(
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

      public float Height
      {
        get => this.GetSimpleProperty("h").GetReal();
        set => this.GetSimpleProperty("h").SetReal(value);
      }

      protected override string StructurePrefix => "stDim";

      protected override string StructureURI => "http://ns.adobe.com/xap/1.0/sType/Dimensions#";

      public string Unit
      {
        get => this.GetSimpleProperty("unit").Value;
        set
        {
          this.GetSimpleProperty("unit").Value = value != null ? value : throw new ArgumentNullException(nameof (Unit));
        }
      }

      public float Width
      {
        get => this.GetSimpleProperty("w").GetReal();
        set => this.GetSimpleProperty("w").SetReal(value);
      }
    }
}
