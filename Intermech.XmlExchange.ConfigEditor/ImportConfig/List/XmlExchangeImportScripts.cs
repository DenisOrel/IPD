// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportScripts
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal class XmlExchangeImportScripts : XmlExchangeImportScriptsBase
{
  public XmlExchangeImportScripts()
  {
  }

  public XmlExchangeImportScripts(XmlImportBase owner)
    : base("SCRIPTS", owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    return string.Equals(this.ItemName, "SCRIPTS", StringComparison.CurrentCultureIgnoreCase);
  }
}
