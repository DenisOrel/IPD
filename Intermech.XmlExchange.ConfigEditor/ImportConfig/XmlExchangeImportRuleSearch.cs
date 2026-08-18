// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportRuleSearch
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportRuleSearch : XmlExchangeImportAttributes
{
  public XmlExchangeImportRuleSearch()
  {
  }

  public SearchRuleOperation Operation { get; set; }

  public Guid SearchType { get; set; } = Guid.Empty;

  public XmlExchangeImportRuleSearch(IMSObjectType objType, XmlImportBase owner)
    : base(objType, owner)
  {
    this.Operation = SearchRuleOperation.And;
  }

  public override XmlExchangeImportAttrTypeBase CreateAttrType(Guid attrGuid, string attrName)
  {
    return (XmlExchangeImportAttrTypeBase) new XmlExchangeImportAttrType(attrGuid, attrName, this.XmlImportItemSetting);
  }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    this.Operation = (SearchRuleOperation) this.XmlImportItemSetting.GetAsInt32("operation", 2);
    this.SearchType = this.XmlImportItemSetting.GetAsGuid("search_type", Guid.Empty);
    if (!base.LoadData(xmlImportNode))
      return false;
    if (xmlImportNode.Items != null)
    {
      this.Attributes.Clear();
      foreach (XmlImportBase xmlImportBase in xmlImportNode.Items)
      {
        if (xmlImportBase.Name == "attribute")
        {
          XmlExchangeImportAttrType exchangeImportAttrType = new XmlExchangeImportAttrType();
          if (exchangeImportAttrType.LoadData(xmlImportBase))
            this.Attributes.Add((XmlExchangeImportAttrTypeBase) exchangeImportAttrType);
        }
      }
    }
    return true;
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsString("operation", this.Operation.GetEnumXmlValue());
    if (!(this.SearchType != Guid.Empty))
      return;
    this.XmlImportItemSetting.SetAsGuid("search_type", this.SearchType);
  }
}
