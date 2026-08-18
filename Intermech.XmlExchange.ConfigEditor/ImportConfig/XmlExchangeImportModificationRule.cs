// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportModificationRule
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportModificationRule : XmlExchangeImportModificationItem
{
  public XmlExchangeImportModificationRule()
  {
  }

  public XmlExchangeImportModificationRule(XmlImportBase owner)
    : base("ModificationRule", owner)
  {
  }

  public int RelType { get; set; }

  public int ProjType { get; set; }

  public int PartTypе { get; set; }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    this.RelType = this.XmlImportItemSetting.GetAsInt32("reltype", 0);
    this.ProjType = this.XmlImportItemSetting.GetAsInt32("projtype", 0);
    this.PartTypе = this.XmlImportItemSetting.GetAsInt32("parttypе", 0);
    return string.Equals(this.ItemName, "ModificationRule", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsInt32("reltype", this.RelType);
    this.XmlImportItemSetting.SetAsInt32("projtype", this.RelType);
    this.XmlImportItemSetting.SetAsInt32("parttypе", this.PartTypе);
  }
}
