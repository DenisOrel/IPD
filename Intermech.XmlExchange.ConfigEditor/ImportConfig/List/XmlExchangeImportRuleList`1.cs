// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.List.XmlExchangeImportRuleList`1
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

internal abstract class XmlExchangeImportRuleList<T> : XmlExchangeImportItemList<T> where T : XmlExchangeImportObjectType, new()
{
  protected XmlExchangeImportRuleList()
  {
  }

  protected XmlExchangeImportRuleList(string itemsName, XmlImportBase owner)
    : base(itemsName, owner)
  {
  }

  public abstract XmlExchangeImportObjectType CreateRule(IMSObjectType objType);
}
