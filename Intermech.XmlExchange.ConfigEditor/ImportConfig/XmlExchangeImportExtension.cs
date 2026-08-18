// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportExtension
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportExtension : XmlExchangeImportItem
{
  public XmlExchangeImportExtension()
  {
  }

  public XmlExchangeImportExtension(XmlImportBase owner)
    : base("extention", owner)
  {
    this.Guid = Guid.Empty;
    this.Enabled = true;
    this.Name = "New Import Extension";
  }

  public XmlExchangeImportExtension(XmlImportBase owner, Guid guid, bool enabled = true)
    : base("extention", owner)
  {
    this.Guid = guid;
    this.Enabled = enabled;
  }

  public Guid Guid { get; set; }

  public bool Enabled { get; set; }

  public string Name { get; set; }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    this.Guid = this.XmlImportItemSetting.GetAsGuid("guid", Guid.Empty);
    this.Enabled = this.XmlImportItemSetting.GetAsBoolean("enabled", false);
    this.Name = this.XmlImportItemSetting.GetAsString("name", string.Empty);
    return string.Equals(this.ItemName, "extention", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsGuid("guid", this.Guid);
    this.XmlImportItemSetting.SetAsBoolean("enabled", this.Enabled);
    this.XmlImportItemSetting.SetAsString("name", this.Name);
  }
}
