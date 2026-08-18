// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportTypeItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal abstract class XmlExchangeImportTypeItem : XmlExchangeImportItem
{
  protected XmlExchangeImportTypeItem()
  {
  }

  protected XmlExchangeImportTypeItem(
    Guid guid,
    string name,
    string itemName,
    XmlImportBase owner)
    : base(itemName, owner)
  {
    this.Guid = guid;
    this.Name = name;
  }

  public Guid Guid { get; set; }

  public string Name { get; set; }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    this.Guid = this.XmlImportItemSetting.GetAsGuid("guid", Guid.Empty);
    this.Name = this.XmlImportItemSetting.GetAsString("name", string.Empty);
    return this.Guid != Guid.Empty && this.Name != string.Empty;
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsGuid("guid", this.Guid);
    this.XmlImportItemSetting.SetAsString("name", this.Name);
  }
}
