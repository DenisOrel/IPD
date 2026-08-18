// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportAttrType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportAttrType : XmlExchangeImportAttrTypeBase
{
  private SearchRuleOperation _operation;

  public XmlExchangeImportAttrType()
  {
  }

  public XmlExchangeImportAttrType(IMSAttributeType attrType, XmlImportBase owner)
    : base(attrType.AttributeGuid, attrType.Name, owner)
  {
  }

  public XmlExchangeImportAttrType(Guid attrGuid, string attrName, XmlImportBase owner)
    : base(attrGuid, attrName, owner)
  {
  }

  public string Value { get; set; }

  public CaseSensitiveMode CaseSensitive { get; set; }

  public SearchRuleOperation Operation
  {
    get => this._operation;
    set => this._operation = value;
  }

  public int Order { get; set; }

  public override bool LoadData(XmlImportBase xmlImportSetting)
  {
    base.LoadData(xmlImportSetting);
    this.Value = this.XmlImportItemSetting.GetAsString("value", (string) null);
    this.CaseSensitive = (CaseSensitiveMode) this.XmlImportItemSetting.GetAsInt32("casesensitive", 0);
    Enum.TryParse<SearchRuleOperation>(EnumToXmlValueConverter.GetEnumValue(typeof (SearchRuleOperation), this.XmlImportItemSetting.GetAsString("operation", SearchRuleOperation.None.GetEnumXmlValue())), out this._operation);
    this.Order = this.XmlImportItemSetting.GetAsInt32("order", 0);
    return string.Equals(this.ItemName, "attribute", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    if (!string.IsNullOrEmpty(this.Value))
      this.XmlImportItemSetting.SetAsString("value", this.Value);
    this.XmlImportItemSetting.SetAsInt32("casesensitive", (int) this.CaseSensitive);
    if (this.Operation != SearchRuleOperation.None)
      this.XmlImportItemSetting.SetAsString("operation", this.Operation.GetEnumXmlValue());
    if (this.Order == 0)
      return;
    this.XmlImportItemSetting.SetAsInt32("order", this.Order);
  }
}
