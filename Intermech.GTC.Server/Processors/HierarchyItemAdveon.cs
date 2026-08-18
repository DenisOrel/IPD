// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.HierarchyItemAdveon
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class HierarchyItemAdveon
{
  private readonly List<LanguageTranslation> _prefferedName = new List<LanguageTranslation>();

  public HierarchyItemAdveon(XElement node)
  {
    this.Id = !node.Descendants((XName) "NodeID").Any<XElement>() || node.Descendants((XName) "NodeID").First<XElement>() == null ? string.Empty : node.Descendants((XName) "NodeID").First<XElement>().Value;
    this.SortOrder = !node.Descendants((XName) nameof (SortOrder)).Any<XElement>() || node.Descendants((XName) nameof (SortOrder)).First<XElement>() == null ? 0 : Convert.ToInt32(node.Descendants((XName) nameof (SortOrder)).First<XElement>().Value);
    this.ParentId = string.Empty;
    XElement xelement1 = node.Descendants((XName) "IconURI").FirstOrDefault<XElement>();
    if (xelement1 != null)
    {
      XElement xelement2 = xelement1.Descendants((XName) "DocumentId").FirstOrDefault<XElement>();
      XElement xelement3 = xelement1.Descendants((XName) "Uri").FirstOrDefault<XElement>();
      this.IconUri = new DocumentAdveon(xelement2 != null ? xelement2.Value : string.Empty, xelement3 != null ? xelement3.Value : string.Empty);
    }
    XElement xelement4 = node.Descendants((XName) "PrefferedName").FirstOrDefault<XElement>();
    if (xelement4 == null)
      return;
    XElement xelement5 = xelement4.Descendants((XName) "Translations").FirstOrDefault<XElement>();
    if (xelement5 == null)
      return;
    foreach (XElement descendant in xelement5.Descendants((XName) "LanguageTranslation"))
    {
      XElement xelement6 = descendant.Descendants((XName) "Language").FirstOrDefault<XElement>();
      XElement xelement7 = descendant.Descendants((XName) "Value").FirstOrDefault<XElement>();
      if (xelement6 != null && xelement7 != null)
        this._prefferedName.Add(new LanguageTranslation(xelement6.Value, xelement7.Value));
    }
  }

  public string Id { get; private set; }

  public string ParentId { get; set; }

  public int SortOrder { get; private set; }

  public DocumentAdveon IconUri { get; private set; }

  public string Name
  {
    get
    {
      if (this._prefferedName.Count <= 0)
        return string.Empty;
      if (this._prefferedName.Count == 1)
        return this._prefferedName.First<LanguageTranslation>().Value;
      if (!this._prefferedName.Any<LanguageTranslation>((Func<LanguageTranslation, bool>) (x => x.Language == "ru-RU")))
        return this._prefferedName.First<LanguageTranslation>().Value;
      LanguageTranslation languageTranslation = this._prefferedName.FirstOrDefault<LanguageTranslation>((Func<LanguageTranslation, bool>) (x => x.Language == "ru-RU"));
      return languageTranslation == null ? string.Empty : languageTranslation.Value;
    }
  }
}
