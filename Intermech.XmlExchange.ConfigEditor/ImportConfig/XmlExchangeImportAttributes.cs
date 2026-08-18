// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportAttributes
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal abstract class XmlExchangeImportAttributes : XmlExchangeImportObjectType
{
  public List<XmlExchangeImportAttrTypeBase> Attributes = new List<XmlExchangeImportAttrTypeBase>();

  protected XmlExchangeImportAttributes()
  {
  }

  protected XmlExchangeImportAttributes(IMSObjectType objType, XmlImportBase owner)
    : base(objType, owner)
  {
  }

  public abstract XmlExchangeImportAttrTypeBase CreateAttrType(Guid attrGuid, string attrName);

  public override void SaveData()
  {
    base.SaveData();
    if (this.Attributes.Count > 0 && this.XmlImportItemSetting.Items != null)
      this.XmlImportItemSetting.Items.Clear();
    else
      this.XmlImportItemSetting.Items = new List<XmlImportBase>();
    foreach (XmlExchangeImportAttrTypeBase attribute in this.Attributes)
    {
      attribute.SaveData();
      this.XmlImportItemSetting.Items.Add(attribute.ImportItemSetting);
    }
  }
}
