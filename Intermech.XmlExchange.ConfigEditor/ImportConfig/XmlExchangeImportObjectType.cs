// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportObjectType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal abstract class XmlExchangeImportObjectType : XmlExchangeImportTypeItem
{
  protected XmlExchangeImportObjectType()
  {
  }

  protected XmlExchangeImportObjectType(IMSObjectType objType, XmlImportBase owner)
    : base(objType.Guid, objType.ObjectTypeName, "object_type", owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    return "object_type" == this.ItemName;
  }
}
