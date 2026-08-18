// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ExportApplSetting.ExportApplObjectType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ExportApplSetting;

internal class ExportApplObjectType : IExportApplType
{
  private int _oldTypeId;
  private Guid _guidType;
  private Guid _oldGuidType;
  private string _nameType;
  private readonly List<XmlExchangeExportAppl> _applSettings;

  public ExportApplObjectType(List<XmlExchangeExportAppl> applSettings, IMSObjectType objType)
  {
    this._applSettings = applSettings;
    this._oldTypeId = this.TypeId = objType.ObjectTypeID;
    this._oldGuidType = this._guidType = objType.Guid;
    this._nameType = objType.ObjectTypeName;
    this.ExistInBase = true;
  }

  public ExportApplObjectType(
    List<XmlExchangeExportAppl> applSettings,
    int typeId,
    Guid guidType,
    string nameType)
  {
    this._applSettings = applSettings;
    this._oldTypeId = this.TypeId = typeId;
    this._oldGuidType = this._guidType = guidType;
    this._nameType = nameType;
  }

  public string TypeName
  {
    get => this.ExistInBase ? this._nameType : this._guidType.ToString();
    set => this._nameType = value;
  }

  public Guid TypeGuid
  {
    get => this._guidType;
    set => this._guidType = value;
  }

  public int TypeId { get; set; }

  public bool ExistInBase { get; private set; }

  public string ApplType { get; } = "Родительский тип объекта";

  public List<XmlExchangeExportAppl> GetCurrentApplList()
  {
    List<XmlExchangeExportAppl> currentApplList = new List<XmlExchangeExportAppl>();
    foreach (XmlExchangeExportAppl applSetting in this._applSettings)
    {
      if (applSetting.ProjTypeGuid == this.TypeGuid && applSetting.ProjTypeID == this.TypeId)
        currentApplList.Add(applSetting);
    }
    return currentApplList;
  }

  public void UpdateExportAppl()
  {
    if (this._applSettings == null)
      return;
    foreach (XmlExchangeExportAppl applSetting in this._applSettings)
    {
      if (applSetting.ProjTypeGuid == this._oldGuidType && applSetting.ProjTypeID == this._oldTypeId)
      {
        applSetting.ProjTypeGuid = this.TypeGuid;
        applSetting.ProjTypeID = this.TypeId;
      }
    }
    this._oldTypeId = this.TypeId;
    this._oldGuidType = this.TypeGuid;
    IMSObjectType objectType = MetaDataHelper.GetObjectType(this.TypeGuid);
    if (objectType == null || objectType.ObjectTypeID != this.TypeId)
      return;
    this.ExistInBase = true;
    this._nameType = objectType.ObjectTypeName;
  }

  public void UpdateExportAppl(IMSObjectType newObjectType)
  {
    this.TypeId = newObjectType.ObjectTypeID;
    this.TypeGuid = newObjectType.Guid;
    this.UpdateExportAppl();
  }

  public void ResetValue()
  {
    this.TypeId = this._oldTypeId;
    this._guidType = this._oldGuidType;
  }
}
