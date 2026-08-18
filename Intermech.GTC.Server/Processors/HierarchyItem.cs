// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.HierarchyItem
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Linq;
using System.Web;
using System.Xml.Linq;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class HierarchyItem
{
  private readonly XNamespace _ns = (XNamespace) "http://www.plmxml.org/Schemas/PLMXMLClassificationSchema";

  public string Name { get; private set; }

  public string ClassId { get; private set; }

  public DateTime CreationDate { get; private set; }

  public DateTime ModifiedDate { get; private set; }

  public Tuple<string, string>[] Files { get; private set; }

  public string Parent { get; private set; }

  public HierarchyItem(XElement node)
  {
    XElement xelement1 = node.Descendants().FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Name == this._ns + nameof (Name)));
    if (xelement1 != null)
      this.Name = xelement1.Value;
    XElement xelement2 = node.Descendants().FirstOrDefault<XElement>((Func<XElement, bool>) (x => x.Name == this._ns + nameof (Parent)));
    if (xelement2 != null)
      this.Parent = xelement2.Value.Replace("SV#", string.Empty);
    XAttribute xattribute1 = node.Attribute((XName) "classId");
    if (xattribute1 != null && xattribute1.Value != string.Empty)
      this.ClassId = xattribute1.Value.Replace("SV#", string.Empty);
    XAttribute xattribute2 = node.Attribute((XName) "creationDate");
    if (xattribute2 != null && xattribute2.Value != string.Empty)
      this.CreationDate = DateTime.Parse(xattribute2.Value);
    XAttribute xattribute3 = node.Attribute((XName) "modifiedDate");
    if (xattribute3 != null && xattribute3.Value != string.Empty)
      this.ModifiedDate = DateTime.Parse(xattribute3.Value);
    this.Files = node.Descendants().Where<XElement>((Func<XElement, bool>) (x => x.Name == this._ns + "File")).Where<XElement>((Func<XElement, bool>) (x => x.Attribute((XName) "locationRef") != null)).Select<XElement, Tuple<string, string>>((Func<XElement, Tuple<string, string>>) (x =>
    {
      XAttribute xattribute4 = x.Attribute((XName) "locationRef");
      XAttribute xattribute5 = x.Attribute((XName) "usage");
      string str1 = xattribute4 != null ? xattribute4.Value : string.Empty;
      string str2 = xattribute5 != null ? xattribute5.Value : string.Empty;
      return new Tuple<string, string>(HttpUtility.UrlDecode(str1), str2);
    })).ToArray<Tuple<string, string>>();
  }
}
