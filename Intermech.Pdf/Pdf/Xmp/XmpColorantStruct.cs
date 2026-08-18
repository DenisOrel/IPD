// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Xmp.XmpColorantStruct
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Xml;


namespace Syncfusion.Pdf.Xmp
{
    public class XmpColorantStruct : XmpStructure
    {
      private const string c_A = "A";
      private const string c_B = "B";
      private const string c_black = "black";
      private const string c_blue = "blue";
      private const string c_cyan = "cyan";
      private const string c_green = "green";
      private const string c_L = "L";
      private const string c_magenta = "magenta";
      private const string c_mode = "mode";
      private const string c_name = "http://ns.adobe.com/xap/1.0/g/";
      private const string c_prefix = "xapG";
      private const string c_red = "red";
      private const string c_swatchName = "swatchName";
      private const string c_type = "type";
      private const string c_yellow = "yellow";

      internal XmpColorantStruct(
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

      public float A
      {
        get => this.GetSimpleProperty(nameof (A)).GetReal();
        set => this.GetSimpleProperty(nameof (A)).SetReal(value);
      }

      public float B
      {
        get => this.GetSimpleProperty(nameof (B)).GetReal();
        set => this.GetSimpleProperty(nameof (B)).SetReal(value);
      }

      public float Black
      {
        get => this.GetSimpleProperty("black").GetReal();
        set => this.GetSimpleProperty("black").SetReal(value);
      }

      public float Blue
      {
        get => this.GetSimpleProperty("blue").GetReal();
        set => this.GetSimpleProperty("blue").SetReal(value);
      }

      public float Cyan
      {
        get => this.GetSimpleProperty("cyan").GetReal();
        set => this.GetSimpleProperty("cyan").SetReal(value);
      }

      public float Green
      {
        get => this.GetSimpleProperty("green").GetReal();
        set => this.GetSimpleProperty("green").SetReal(value);
      }

      public float L
      {
        get => this.GetSimpleProperty(nameof (L)).GetReal();
        set => this.GetSimpleProperty(nameof (L)).SetReal(value);
      }

      public float Magenta
      {
        get => this.GetSimpleProperty("magenta").GetReal();
        set => this.GetSimpleProperty("magenta").SetReal(value);
      }

      public string Mode
      {
        get => this.GetSimpleProperty("mode").Value;
        set
        {
          this.GetSimpleProperty("mode").Value = value != null ? value : throw new ArgumentNullException("mode");
        }
      }

      public float Red
      {
        get => this.GetSimpleProperty("red").GetReal();
        set => this.GetSimpleProperty("red").SetReal(value);
      }

      protected override string StructurePrefix => "xapG";

      protected override string StructureURI => "http://ns.adobe.com/xap/1.0/g/";

      public string SwatchName
      {
        get => this.GetSimpleProperty("swatchName").Value;
        set
        {
          this.GetSimpleProperty("swatchName").Value = value != null ? value : throw new ArgumentNullException("swatchName");
        }
      }

      public string Type
      {
        get => this.GetSimpleProperty("type").Value;
        set
        {
          this.GetSimpleProperty("type").Value = value != null ? value : throw new ArgumentNullException("type");
        }
      }

      public float Yellow
      {
        get => this.GetSimpleProperty("yellow").GetReal();
        set => this.GetSimpleProperty("yellow").SetReal(value);
      }
    }
}
