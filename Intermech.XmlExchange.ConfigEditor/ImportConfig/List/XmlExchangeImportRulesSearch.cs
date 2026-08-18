// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportRulesSearch
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal class XmlExchangeImportRulesSearch : XmlExchangeImportRuleList<XmlExchangeImportRuleSearch>
{
  public XmlExchangeImportRulesSearch()
  {
  }

  public XmlExchangeImportRulesSearch(XmlImportBase owner)
    : base("SearchRules", owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    if (!string.Equals(this.ItemName, "SearchRules", StringComparison.CurrentCultureIgnoreCase))
      return false;
    if (xmlImportBase.Items != null)
    {
      foreach (XmlImportBase xmlImportBase1 in xmlImportBase.Items)
      {
        XmlExchangeImportRuleSearch importRuleSearch = new XmlExchangeImportRuleSearch();
        if (importRuleSearch.LoadData(xmlImportBase1))
          this.Add(importRuleSearch);
      }
    }
    return true;
  }

  public override XmlExchangeImportObjectType CreateRule(IMSObjectType objType)
  {
    if (objType == null)
      return (XmlExchangeImportObjectType) null;
    XmlExchangeImportRuleSearch rule = new XmlExchangeImportRuleSearch(objType, this.XmlImportItemSetting);
    this.Add(rule);
    return (XmlExchangeImportObjectType) rule;
  }
}
