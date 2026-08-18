// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportActions
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal class XmlExchangeImportActions : XmlExchangeImportItemList<XmlExchangeImportAction>
{
  public XmlExchangeImportActions()
  {
  }

  public XmlExchangeImportActions(XmlImportBase owner)
    : base("Actions", owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    foreach (XmlImportBase xmlImportBase1 in xmlImportBase.Items)
    {
      XmlExchangeImportAction exchangeImportAction = new XmlExchangeImportAction();
      if (exchangeImportAction.LoadData(xmlImportBase1))
        this.Add(exchangeImportAction);
    }
    return string.Equals(this.ItemName, "Actions", StringComparison.CurrentCultureIgnoreCase);
  }
}
