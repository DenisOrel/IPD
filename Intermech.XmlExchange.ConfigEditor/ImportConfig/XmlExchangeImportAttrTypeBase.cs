// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportAttrTypeBase
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportAttrTypeBase : XmlExchangeImportTypeItem
{
  public XmlExchangeImportAttrTypeBase()
  {
  }

  public XmlExchangeImportAttrTypeBase(Guid attrGuid, string attrName, XmlImportBase owner)
    : base(attrGuid, attrName, "attribute", owner)
  {
  }

  public string UserId { get; set; }

  public override bool LoadData(XmlImportBase xmlImportSetting)
  {
    base.LoadData(xmlImportSetting);
    this.UserId = this.XmlImportItemSetting.GetAsString("user_id", (string) null);
    return string.Equals(this.ItemName, "attribute", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    if (string.IsNullOrEmpty(this.UserId))
      return;
    this.XmlImportItemSetting.SetAsString("user_id", this.UserId);
  }
}
