// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal abstract class XmlExchangeImportItem
{
  private protected XmlImportBase XmlImportItemSetting;
  private protected string ItemName = string.Empty;

  protected internal XmlExchangeImportItem()
  {
  }

  public XmlImportBase ImportItemSetting => this.XmlImportItemSetting;

  protected internal XmlExchangeImportItem(string itemName, XmlImportBase owner)
  {
    this.XmlImportItemSetting = new XmlImportBase(owner);
    this.ItemName = itemName;
  }

  public string Comments { get; set; }

  public virtual bool LoadData() => this.LoadData(this.XmlImportItemSetting);

  public virtual bool LoadData(XmlImportBase xmlImportBase)
  {
    this.XmlImportItemSetting = xmlImportBase;
    this.ItemName = this.XmlImportItemSetting.Name;
    this.Comments = this.XmlImportItemSetting.GetAsString("comment", string.Empty);
    return !string.IsNullOrEmpty(this.ItemName);
  }

  public virtual void SaveData()
  {
    if (this.XmlImportItemSetting == null)
      return;
    this.XmlImportItemSetting.attributes.Clear();
    this.XmlImportItemSetting.Name = this.ItemName;
    if (string.IsNullOrEmpty(this.Comments))
      return;
    this.XmlImportItemSetting.SetAsString("comment", this.Comments);
  }

  public virtual bool RemoveItemSetting()
  {
    XmlImportBase owner = this.XmlImportItemSetting.Owner;
    if (owner == null)
      return false;
    owner.Remove(this.XmlImportItemSetting);
    return !owner.Items.Contains(this.XmlImportItemSetting);
  }
}
