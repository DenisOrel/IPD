// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportExtensions
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal class XmlExchangeImportExtensions : XmlExchangeImportItemList<XmlExchangeImportExtension>
{
  public XmlExchangeImportExtensions()
  {
  }

  public XmlExchangeImportExtensions(XmlImportBase owner)
    : base("Extentions", owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    if (!string.Equals(this.ItemName, "Extentions", StringComparison.CurrentCultureIgnoreCase))
      return false;
    if (xmlImportBase.Items != null)
    {
      foreach (XmlImportBase xmlImportBase1 in xmlImportBase.Items)
      {
        XmlExchangeImportExtension exchangeImportExtension = new XmlExchangeImportExtension();
        if (exchangeImportExtension.LoadData(xmlImportBase1))
          this.Add(exchangeImportExtension);
      }
    }
    return true;
  }

  public XmlExchangeImportExtension CreateExtension()
  {
    XmlExchangeImportExtension exchangeImportExtension = new XmlExchangeImportExtension(this.XmlImportItemSetting);
    this.Add(exchangeImportExtension);
    return exchangeImportExtension;
  }
}
