// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportRuleImport
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportRuleImport : XmlExchangeImportAttributes
{
  private ImportRuleMode _rule;
  private Guid _dictionary = Guid.Empty;

  public XmlExchangeImportRuleImport()
  {
  }

  public XmlExchangeImportRuleImport(IMSObjectType objType, XmlImportBase owner)
    : base(objType, owner)
  {
    Enum.TryParse<ImportRuleMode>(EnumToXmlValueConverter.GetEnumValue(typeof (ImportRuleMode), "refresh"), out this._rule);
  }

  public ImportRuleMode Rule
  {
    get => this._rule;
    set => this._rule = value;
  }

  public SkipExistsMode SkipExists { get; set; }

  public Guid Dictionary
  {
    get => this._dictionary;
    set => this._dictionary = value;
  }

  public override XmlExchangeImportAttrTypeBase CreateAttrType(Guid attrGuid, string attrName)
  {
    return new XmlExchangeImportAttrTypeBase(attrGuid, attrName, this.XmlImportItemSetting);
  }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    Guid.TryParse(this.XmlImportItemSetting.GetAsString("dictionary", string.Empty), out this._dictionary);
    this.SkipExists = (SkipExistsMode) this.XmlImportItemSetting.GetAsInt32("skipExists", 0);
    if (!Enum.TryParse<ImportRuleMode>(EnumToXmlValueConverter.GetEnumValue(typeof (ImportRuleMode), this.XmlImportItemSetting.GetAsString("rule", string.Empty)), out this._rule))
      return false;
    if (xmlImportNode.Items != null)
    {
      this.Attributes.Clear();
      foreach (XmlImportBase xmlImportBase in xmlImportNode.Items)
      {
        if (xmlImportBase.Name == "attribute")
        {
          XmlExchangeImportAttrTypeBase importAttrTypeBase = new XmlExchangeImportAttrTypeBase();
          if (importAttrTypeBase.LoadData(xmlImportBase))
            this.Attributes.Add(importAttrTypeBase);
        }
      }
    }
    return true;
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsString("rule", this.Rule.GetEnumXmlValue());
    if (this.Dictionary != Guid.Empty)
      this.XmlImportItemSetting.SetAsGuid("dictionary", this.Dictionary);
    if (this.SkipExists == SkipExistsMode.None)
      return;
    this.XmlImportItemSetting.SetAsInt32("skipExists", (int) this.SkipExists);
  }
}
