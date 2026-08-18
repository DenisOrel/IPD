// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportScript
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportScript : XmlExchangeImportItem
{
  public XmlExchangeImportScript()
  {
  }

  public XmlExchangeImportScript(XmlImportBase owner)
    : base("script", owner)
  {
    this.ScriptName = "New Import Script";
    this.ScriptCode = XmlConfigEmptyScript.xmlImportEmptyScript;
  }

  public string ScriptName { get; set; }

  public string ScriptCode { get; set; }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    this.ScriptCode = this.XmlImportItemSetting.Value;
    this.ScriptName = this.XmlImportItemSetting.GetAsString("name", string.Empty);
    return string.Equals(this.ItemName, "script", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    if (string.IsNullOrEmpty(this.ScriptCode))
      return;
    this.XmlImportItemSetting.SetAsString("name", this.ScriptName);
    this.XmlImportItemSetting.Value = this.ScriptCode;
  }
}
