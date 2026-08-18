// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportRuleCreate
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using System;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportRuleCreate : XmlExchangeImportObjectType
{
  private CreationRuleMode _rule;
  private VersionOwnerMode _versionOwner;
  private VersionNoMode _versionNo;

  public XmlExchangeImportRuleCreate()
  {
  }

  public XmlExchangeImportRuleCreate(IMSObjectType objType, XmlImportBase owner)
    : base(objType, owner)
  {
    Enum.TryParse<CreationRuleMode>(EnumToXmlValueConverter.GetEnumValue(typeof (CreationRuleMode), "refreshBase"), out this._rule);
  }

  public CreationRuleMode Rule
  {
    get => this._rule;
    set => this._rule = value;
  }

  public VersionOwnerMode VersionOwner
  {
    get => this._versionOwner;
    set => this._versionOwner = value;
  }

  public VersionNoMode VersionNo
  {
    get => this._versionNo;
    set => this._versionNo = value;
  }

  public Guid LcStep { get; set; } = Guid.Empty;

  public object VersionNoAttrId { get; set; }

  public override bool LoadData(XmlImportBase xmlImportNode)
  {
    base.LoadData(xmlImportNode);
    Enum.TryParse<VersionOwnerMode>(EnumToXmlValueConverter.GetEnumValue(typeof (VersionOwnerMode), this.XmlImportItemSetting.GetAsString("version_owner", string.Empty)), out this._versionOwner);
    Enum.TryParse<VersionNoMode>(EnumToXmlValueConverter.GetEnumValue(typeof (VersionNoMode), this.XmlImportItemSetting.GetAsString("version_no", string.Empty)), out this._versionNo);
    this.LcStep = this.XmlImportItemSetting.GetAsGuid("lcStep", Guid.Empty);
    this.VersionNoAttrId = this.XmlImportItemSetting.GetAsObject("version_no_attr_id", (object) null);
    return Enum.TryParse<CreationRuleMode>(EnumToXmlValueConverter.GetEnumValue(typeof (CreationRuleMode), this.XmlImportItemSetting.GetAsString("rule", string.Empty)), out this._rule);
  }

  public override void SaveData()
  {
    base.SaveData();
    this.XmlImportItemSetting.SetAsString("version_owner", this.VersionOwner.GetEnumXmlValue());
    this.XmlImportItemSetting.SetAsString("version_no", this.VersionNo.GetEnumXmlValue());
    if (this.LcStep != Guid.Empty)
      this.XmlImportItemSetting.SetAsGuid("lcStep", this.LcStep);
    if (this.VersionNoAttrId != null && this.VersionNoAttrId.ToString() != "0")
      this.XmlImportItemSetting.SetAsObject("version_no_attr_id", this.VersionNoAttrId);
    this.XmlImportItemSetting.SetAsString("rule", this.Rule.GetEnumXmlValue());
  }
}
