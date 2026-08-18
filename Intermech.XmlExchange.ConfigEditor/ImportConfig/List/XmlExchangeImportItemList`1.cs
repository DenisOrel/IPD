// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportItemList`1
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal abstract class XmlExchangeImportItemList<T> : System.Collections.Generic.List<T> where T : XmlExchangeImportItem, new()
{
  private protected XmlImportBase XmlImportItemSetting;
  private protected string ItemName = string.Empty;

  protected XmlExchangeImportItemList()
  {
  }

  protected XmlExchangeImportItemList(string itemName, XmlImportBase owner)
  {
    this.ItemName = itemName;
    this.XmlImportItemSetting = new XmlImportBase(owner);
  }

  public virtual bool LoadData(XmlImportBase xmlImportBase)
  {
    this.XmlImportItemSetting = xmlImportBase;
    this.ItemName = this.XmlImportItemSetting.Name;
    return !string.IsNullOrEmpty(this.ItemName);
  }

  public virtual void SaveData()
  {
    this.XmlImportItemSetting.Name = this.ItemName;
    if (this.XmlImportItemSetting.Items == null)
      this.XmlImportItemSetting.Items = new System.Collections.Generic.List<XmlImportBase>();
    else if (this.Count > 0)
      this.XmlImportItemSetting.Items.Clear();
    foreach (T obj in (System.Collections.Generic.List<T>) this)
    {
      obj.SaveData();
      this.XmlImportItemSetting.Items.Add(obj.ImportItemSetting);
    }
    if (this.XmlImportItemSetting.Items != null && this.XmlImportItemSetting.Items.Count != 0 || this.XmlImportItemSetting.Owner == null)
      return;
    this.XmlImportItemSetting.Owner?.Items.Remove(this.XmlImportItemSetting);
  }
}
