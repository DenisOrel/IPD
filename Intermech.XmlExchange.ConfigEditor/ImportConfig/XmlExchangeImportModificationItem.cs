// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportModificationItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal abstract class XmlExchangeImportModificationItem : XmlExchangeImportItem
{
  protected XmlExchangeImportModificationItem()
  {
  }

  protected XmlExchangeImportModificationItem(string itemName, XmlImportBase owner)
    : base(itemName, owner)
  {
  }

  public string Description { get; set; }

  public int Order { get; set; }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    this.Description = this.XmlImportItemSetting.GetAsString("description", string.Empty);
    this.Order = this.XmlImportItemSetting.GetAsInt32("order", -1);
    return !string.IsNullOrEmpty(this.ItemName);
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsString("description", this.Description);
    this.XmlImportItemSetting.SetAsString("order", this.Order.ToString());
  }
}
