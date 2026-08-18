// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportAction
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportAction : XmlExchangeImportItem
{
  public XmlExchangeImportAction()
  {
  }

  public XmlExchangeImportAction(XmlImportBase owner)
    : base("Action", owner)
  {
  }

  public int Mode { get; set; }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    this.Mode = this.XmlImportItemSetting.GetAsInt32("mode", 0);
    return string.Equals(this.ItemName, "Action", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsInt32("mode", this.Mode);
  }
}
