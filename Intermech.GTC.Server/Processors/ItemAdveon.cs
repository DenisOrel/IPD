// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.ItemAdveon
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class ItemAdveon
{
  private readonly List<DocumentAdveon> _documents = new List<DocumentAdveon>();

  public ItemAdveon(XElement node)
  {
    XElement xelement1 = node.Element((XName) "ToolID");
    this.ToolId = xelement1 != null ? xelement1.Value : string.Empty;
    XElement xelement2 = node.Element((XName) "GTCGeneric");
    this.GtcGeneric = xelement2 != null ? xelement2.Value : string.Empty;
    XElement xelement3 = node.Element((XName) "GTCVendorSpecific");
    this.GtcVendorSpecific = xelement3 != null ? xelement3.Value : string.Empty;
    XElement xelement4 = node.Element((XName) nameof (TimeStamp));
    this.TimeStamp = xelement4 != null ? Convert.ToDateTime(xelement4.Value) : DateTime.MinValue;
    XElement xelement5 = node.Element((XName) nameof (DocumentTimeStamp));
    this.DocumentTimeStamp = xelement5 != null ? Convert.ToDateTime(xelement5.Value) : DateTime.MinValue;
    foreach (XElement descendant in node.Descendants((XName) "CatalogDocument"))
    {
      XElement xelement6 = descendant.Element((XName) "DocumentId");
      string id = xelement6 != null ? xelement6.Value : string.Empty;
      XElement xelement7 = descendant.Element((XName) "Uri");
      string uri = xelement7 != null ? xelement7.Value : string.Empty;
      this._documents.Add(new DocumentAdveon(id, uri));
    }
  }

  public string ToolId { get; private set; }

  public string GtcGeneric { get; private set; }

  public string GtcVendorSpecific { get; private set; }

  public DateTime TimeStamp { get; private set; }

  public DateTime DocumentTimeStamp { get; private set; }

  public DocumentAdveon[] Documents => this._documents.ToArray();

  public string P21Path
  {
    get
    {
      DocumentAdveon documentAdveon = this._documents.FirstOrDefault<DocumentAdveon>((Func<DocumentAdveon, bool>) (x => x.Id == "P21"));
      return documentAdveon == null ? string.Empty : documentAdveon.Uri;
    }
  }

  public Tuple<string, string>[] Files
  {
    get
    {
      return this._documents.Where<DocumentAdveon>((Func<DocumentAdveon, bool>) (x => x.Id != "P21")).Select<DocumentAdveon, Tuple<string, string>>((Func<DocumentAdveon, Tuple<string, string>>) (x => new Tuple<string, string>(x.Uri, x.Id))).ToArray<Tuple<string, string>>();
    }
  }
}
