// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ExportApplSetting.ExportApplRelationType
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ExportApplSetting;

internal class ExportApplRelationType : IExportApplType
{
  private int _oldTypeId;
  private Guid _guidType;
  private Guid _oldGuidType;
  private string _nameType;

  public ExportApplRelationType(
    List<XmlExchangeExportAppl> applSettings,
    ExportApplObjectType projType,
    IMSRelationType relationType)
  {
    this.GetApplSettings = applSettings;
    this.GetProjType = projType;
    this._oldTypeId = this.TypeId = relationType.RelationTypeID;
    this._oldGuidType = this._guidType = relationType.Guid;
    this._nameType = relationType.Description;
    this.ExistInBase = true;
  }

  public ExportApplRelationType(
    List<XmlExchangeExportAppl> applSettings,
    ExportApplObjectType projType,
    int typeId,
    Guid guidType,
    string nameType)
  {
    this.GetApplSettings = applSettings;
    this.GetProjType = projType;
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

  public string ApplType { get; } = "Тип связи";

  public bool ExistInBase { get; private set; }

  public void UpdateExportAppl()
  {
    if (this.GetApplSettings == null)
      return;
    foreach (XmlExchangeExportAppl getApplSetting in this.GetApplSettings)
    {
      if (getApplSetting.RelTypeGuid == this._oldGuidType && getApplSetting.RelTypeID == this._oldTypeId && getApplSetting.ProjTypeID == this.GetProjType.TypeId && getApplSetting.ProjTypeGuid == this.GetProjType.TypeGuid)
      {
        getApplSetting.RelTypeGuid = this.TypeGuid;
        getApplSetting.RelTypeID = this.TypeId;
      }
    }
    this._oldTypeId = this.TypeId;
    this._oldGuidType = this.TypeGuid;
    IMSRelationType relationType = MetaDataHelper.GetRelationType(this.TypeGuid);
    if (relationType == null || relationType.RelationTypeID != this.TypeId)
      return;
    this.ExistInBase = true;
    this._nameType = relationType.Description;
  }

  public void UpdateExportAppl(IMSRelationType newRelationType)
  {
    this.TypeId = newRelationType.RelationTypeID;
    this.TypeGuid = newRelationType.Guid;
    this.TypeName = newRelationType.Description;
    this.UpdateExportAppl();
  }

  public void ResetValue()
  {
    this.TypeId = this._oldTypeId;
    this._guidType = this._oldGuidType;
  }

  public List<XmlExchangeExportAppl> GetCurrentApplList()
  {
    List<XmlExchangeExportAppl> currentApplList = new List<XmlExchangeExportAppl>();
    foreach (XmlExchangeExportAppl getApplSetting in this.GetApplSettings)
    {
      if (getApplSetting.RelTypeGuid == this.TypeGuid && getApplSetting.RelTypeID == this.TypeId && getApplSetting.ProjTypeGuid == this.GetProjType.TypeGuid && getApplSetting.ProjTypeID == this.GetProjType.TypeId)
        currentApplList.Add(getApplSetting);
    }
    return currentApplList;
  }

  public List<XmlExchangeExportAppl> GetApplSettings { get; }

  public ExportApplObjectType GetProjType { get; }
}
