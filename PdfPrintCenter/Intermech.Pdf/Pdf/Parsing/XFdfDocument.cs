// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Parsing.XFdfDocument
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

#nullable disable
namespace Syncfusion.Pdf.Parsing;

public class XFdfDocument
{
  private string PdfFilePath = "";
  private Dictionary<object, object> table = new Dictionary<object, object>();

  public XFdfDocument(string filename) => this.PdfFilePath = filename;

  internal void Save(Stream stream)
  {
    XmlTextWriter xmlTextWriter = new XmlTextWriter(stream, (Encoding) new UTF8Encoding());
    xmlTextWriter.Formatting = Formatting.Indented;
    xmlTextWriter.WriteStartDocument();
    xmlTextWriter.WriteStartElement("xfdf".ToLower());
    xmlTextWriter.WriteAttributeString("xmlns", (string) null, (string) null, "http://ns.adobe.com/xfdf/");
    xmlTextWriter.WriteAttributeString("xml", "space", (string) null, "preserve");
    xmlTextWriter.WriteStartElement("Fields".ToLower());
    foreach (KeyValuePair<object, object> keyValuePair in this.table)
    {
      xmlTextWriter.WriteStartElement("field");
      xmlTextWriter.WriteAttributeString("Name".ToLower(), keyValuePair.Key.ToString());
      if (keyValuePair.Value.GetType().Name == "PdfArray")
      {
        foreach (PdfString pdfString in keyValuePair.Value as PdfArray)
        {
          xmlTextWriter.WriteStartElement("value");
          xmlTextWriter.WriteString(pdfString.Value.ToString());
          xmlTextWriter.WriteEndElement();
        }
      }
      else
      {
        xmlTextWriter.WriteStartElement("value");
        xmlTextWriter.WriteString(keyValuePair.Value.ToString());
        xmlTextWriter.WriteEndElement();
      }
      xmlTextWriter.WriteEndElement();
    }
    xmlTextWriter.WriteEndElement();
    xmlTextWriter.WriteStartElement("f");
    xmlTextWriter.WriteAttributeString("href", this.PdfFilePath);
    xmlTextWriter.WriteEndElement();
    xmlTextWriter.WriteEndElement();
    xmlTextWriter.WriteEndDocument();
    xmlTextWriter.Flush();
  }

  internal void SetFields(object fieldName, object Fieldvalue)
  {
    this.table.Add(fieldName, Fieldvalue);
  }
}
