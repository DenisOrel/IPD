// Decompiled with JetBrains decompiler
// Type: Intermech.GTC.Server.Processors.GtcClassHierarchy
// Assembly: Intermech.GTC.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 9C6A94ED-A48D-4719-B6F5-18FD5E10EDC9
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.GTC.Server.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Intermech.GTC.Server.Processors;

internal class GtcClassHierarchy
{
  private readonly List<StringWithLanguage> _nodeName = new List<StringWithLanguage>();
  private readonly List<StringWithLanguage> _preferredName = new List<StringWithLanguage>();

  public GtcClassHierarchy(XElement node)
  {
    this.Id = !node.Descendants((XName) "id").Any<XElement>() || node.Descendants((XName) "id").First<XElement>() == null ? string.Empty : node.Descendants((XName) "id").First<XElement>().Value;
    this.ParentId = !node.Descendants((XName) "parent_id").Any<XElement>() || node.Descendants((XName) "parent_id").First<XElement>() == null ? string.Empty : node.Descendants((XName) "parent_id").First<XElement>().Value;
    this._nodeName.AddRange((IEnumerable<StringWithLanguage>) node.Descendants((XName) "node_name").Descendants<XElement>((XName) "string_with_language").Where<XElement>((Func<XElement, bool>) (y => y.Descendants((XName) "language").Any<XElement>() && y.Descendants((XName) "string_value").Any<XElement>())).Select<XElement, StringWithLanguage>((Func<XElement, StringWithLanguage>) (x => new StringWithLanguage(x.Descendants((XName) "language").First<XElement>().Value, x.Descendants((XName) "string_value").First<XElement>().Value))).ToArray<StringWithLanguage>());
    this._preferredName.AddRange((IEnumerable<StringWithLanguage>) node.Descendants((XName) "preferred_name").Descendants<XElement>((XName) "string_with_language").Where<XElement>((Func<XElement, bool>) (y => y.Descendants((XName) "language").Any<XElement>() && y.Descendants((XName) "string_value").Any<XElement>())).Select<XElement, StringWithLanguage>((Func<XElement, StringWithLanguage>) (x => new StringWithLanguage(x.Descendants((XName) "language").First<XElement>().Value, x.Descendants((XName) "string_value").First<XElement>().Value))).ToArray<StringWithLanguage>());
    this.ModifiedDate = !node.Descendants((XName) "modified_date").Any<XElement>() || node.Descendants((XName) "modified_date").First<XElement>() == null ? DateTime.MinValue : Convert.ToDateTime(node.Descendants((XName) "modified_date").First<XElement>().Value);
    this.MappingRule = !node.Descendants((XName) "mapping_rule").Any<XElement>() || node.Descendants((XName) "mapping_rule").First<XElement>() == null ? string.Empty : node.Descendants((XName) "mapping_rule").First<XElement>().Value;
    this.SortLevel = !node.Descendants((XName) "sort_level").Any<XElement>() || node.Descendants((XName) "sort_level").First<XElement>() == null ? 0 : Convert.ToInt32(node.Descendants((XName) "sort_level").First<XElement>().Value);
    this.IsLeaf = node.Descendants((XName) "is_leaf").Any<XElement>() && node.Descendants((XName) "is_leaf").First<XElement>() != null && Convert.ToBoolean(node.Descendants((XName) "is_leaf").First<XElement>().Value);
  }

  public string Id { get; private set; }

  public string ParentId { get; private set; }

  public string NodeName
  {
    get
    {
      if (this._nodeName.Count <= 0)
        return string.Empty;
      if (this._nodeName.Count == 1)
        return this._nodeName.First<StringWithLanguage>().Value;
      if (this._nodeName.All<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "rus")))
        return this._nodeName.First<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "rus")).Value;
      return this._nodeName.All<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "eng")) ? this._nodeName.First<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "eng")).Value : this._nodeName.First<StringWithLanguage>().Value;
    }
  }

  public string PreferredName
  {
    get
    {
      if (this._preferredName.Count <= 0)
        return string.Empty;
      if (this._preferredName.Count == 1)
        return this._preferredName.First<StringWithLanguage>().Value;
      if (this._preferredName.All<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "rus")))
        return this._preferredName.First<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "rus")).Value;
      return this._preferredName.All<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "eng")) ? this._preferredName.First<StringWithLanguage>((Func<StringWithLanguage, bool>) (x => x.Language == "eng")).Value : this._preferredName.First<StringWithLanguage>().Value;
    }
  }

  public DateTime ModifiedDate { get; private set; }

  public string MappingRule { get; private set; }

  public int SortLevel { get; private set; }

  public bool IsLeaf { get; private set; }
}
