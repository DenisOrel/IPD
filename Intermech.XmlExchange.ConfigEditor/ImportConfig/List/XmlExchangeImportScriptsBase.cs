// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportScriptsBase
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal abstract class XmlExchangeImportScriptsBase : 
  XmlExchangeImportItemList<XmlExchangeImportScript>
{
  protected XmlExchangeImportScriptsBase()
  {
  }

  protected XmlExchangeImportScriptsBase(string itemName, XmlImportBase owner)
    : base(itemName, owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    bool flag = base.LoadData(xmlImportBase);
    if (xmlImportBase.Items != null)
    {
      foreach (XmlImportBase xmlImportBase1 in xmlImportBase.Items)
      {
        XmlExchangeImportScript exchangeImportScript = new XmlExchangeImportScript();
        if (exchangeImportScript.LoadData(xmlImportBase1))
          this.Add(exchangeImportScript);
      }
    }
    return flag;
  }

  public XmlExchangeImportScript CreateScript()
  {
    XmlExchangeImportScript script = new XmlExchangeImportScript(this.XmlImportItemSetting);
    this.Add(script);
    return script;
  }
}
