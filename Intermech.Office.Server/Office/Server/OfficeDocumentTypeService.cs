// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.OfficeDocumentTypeService
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel;
using Intermech.Office.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

#nullable disable
namespace Intermech.Office.Server;

internal class OfficeDocumentTypeService : LongLifeObject, IOfficeDocumentTypeService
{
  private const string CfgEnableTypes = "EnableTypes";
  private const string CfgEmptyRegNumber = "EmptyRegNumber";
  private const string CfgEnableEmptyRegNumbers = "EnableEmptyRegNumbers";
  private const string CfgAutoGenerateRegNumber = "AutoGenerateRegNumber";
  private const string CfgTemplateString = "TemplateString";
  private const string CfgCountResetType = "CountResetType";
  private const string CfgCountWithinType = "CountWithinType";
  private const string CfgCountWithinUnit = "CountWithinUnit";
  private const string CfgDesignationEqualRegNumber = "DesignationEqualRegNumber";
  private const string CfgProcessTemplateControl = "ProcessTemplateControl";
  private const string CfgProcessTemplateNoControl = "ProcessTemplateNoControl";
  private const string CfgSuccessiveProcessTemplateControl = "SuccProcessTemplateControl";
  private const string CfgSuccessiveProcessTemplateNoControl = "SuccProcessTemplateNoControl";
  [NotNull]
  private readonly TypeSettingsForUnitsCache _unitSettingsCache;

  public OfficeDocumentTypeService()
  {
    this._unitSettingsCache = new TypeSettingsForUnitsCache();
    this._unitSettingsCache.Reload();
  }

  [NotNull]
  private string GetPropertyName(OfficeDocumentTypes type, [NotNull] string property)
  {
    return $"{property}_{(int) type}";
  }

  [NotNull]
  private OfficeDocumentTypeSettings GetOfficeTypeSettings([NotNull] IUserSession session, int documentType)
  {
    OfficeDocumentTypeSettings officeTypeSettings = OfficeDocumentTypeSettings.CreateDefault();
    DBMetadataExtensions metadataExtensions = session.GetObjectType(documentType, true).As<DBMetadataExtensions>();
    int[] mdValuesInt = metadataExtensions.GetMDValuesInt("EnableTypes");
    if (mdValuesInt != null && mdValuesInt.Length != 0)
    {
      List<OfficeDocumentTypes> officeDocumentTypesList = new List<OfficeDocumentTypes>(mdValuesInt.Length);
      officeDocumentTypesList.AddRange((IEnumerable<OfficeDocumentTypes>) Array.ConvertAll<int, OfficeDocumentTypes>(mdValuesInt, (Converter<int, OfficeDocumentTypes>) (item => (OfficeDocumentTypes) item)));
      officeTypeSettings.EnableTypes = officeDocumentTypesList.ToArray();
    }
    string mdValue1 = metadataExtensions.GetMDValue("ProcessTemplateControl");
    if (GuidHelper.IsGuid(mdValue1))
      officeTypeSettings.ProcessTemplates.Control = new Guid(mdValue1);
    string mdValue2 = metadataExtensions.GetMDValue("ProcessTemplateNoControl");
    if (GuidHelper.IsGuid(mdValue2))
      officeTypeSettings.ProcessTemplates.NoControl = new Guid(mdValue2);
    string mdValue3 = metadataExtensions.GetMDValue("SuccProcessTemplateControl");
    if (GuidHelper.IsGuid(mdValue3))
      officeTypeSettings.ProcessTemplates.SuccessiveControl = new Guid(mdValue3);
    string mdValue4 = metadataExtensions.GetMDValue("SuccProcessTemplateNoControl");
    if (GuidHelper.IsGuid(mdValue4))
      officeTypeSettings.ProcessTemplates.SuccessiveNoControl = new Guid(mdValue4);
    int capacity = Enum.GetValues(typeof (OfficeDocumentTypes)).Length - 1;
    officeTypeSettings.Templates = new Dictionary<OfficeDocumentTypes, RegNumberSettings>(capacity);
    officeTypeSettings.EnableEmptyRegNumbers = new Dictionary<OfficeDocumentTypes, bool>(capacity);
    foreach (OfficeDocumentTypes officeDocumentTypes in Enum.GetValues(typeof (OfficeDocumentTypes)))
    {
      if (officeDocumentTypes != OfficeDocumentTypes.Unknown)
      {
        RegNumberSettings regNumberSettings = new RegNumberSettings();
        regNumberSettings.Template = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "TemplateString"));
        string mdValue5 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "EnableEmptyRegNumbers"));
        if (mdValue5 != string.Empty)
          regNumberSettings.EnableEmptyRegNumbers = Convert.ToBoolean(mdValue5, (IFormatProvider) CultureInfo.InvariantCulture);
        string mdValue6 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "AutoGenerateRegNumber"));
        if (mdValue6 != string.Empty)
          regNumberSettings.AutoGenerateRegNumber = Convert.ToBoolean(mdValue6, (IFormatProvider) CultureInfo.InvariantCulture);
        string mdValue7 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "CountResetType"));
        if (mdValue7 != string.Empty)
          regNumberSettings.CountResetType = (CountResetTypes) Convert.ToInt32(mdValue7);
        string mdValue8 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "CountWithinType"));
        if (mdValue8 != string.Empty)
          regNumberSettings.CountWithinType = Convert.ToBoolean(mdValue8, (IFormatProvider) CultureInfo.InvariantCulture);
        string mdValue9 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "CountWithinUnit"));
        if (mdValue9 != string.Empty)
          regNumberSettings.CountWithinUnit = Convert.ToBoolean(mdValue9, (IFormatProvider) CultureInfo.InvariantCulture);
        string mdValue10 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "DesignationEqualRegNumber"));
        if (mdValue10 != string.Empty)
          regNumberSettings.DesignationEqualRegNumber = Convert.ToBoolean(mdValue10, (IFormatProvider) CultureInfo.InvariantCulture);
        officeTypeSettings.Templates.Add(officeDocumentTypes, regNumberSettings);
        string mdValue11 = metadataExtensions.GetMDValue(this.GetPropertyName(officeDocumentTypes, "EmptyRegNumber"));
        officeTypeSettings.EnableEmptyRegNumbers.Add(officeDocumentTypes, !string.IsNullOrEmpty(mdValue11) && Convert.ToBoolean(mdValue11));
      }
    }
    return officeTypeSettings;
  }

  [NotNull]
  public OfficeDocumentTypeSettings GetSettings(Guid sessionGuid, int documentType)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    OfficeDocumentTypeSettings officeTypeSettings = this.GetOfficeTypeSettings(sessionById, documentType);
    if (officeTypeSettings.ProcessTemplates.Empty)
    {
      int objectTypeParentId = MetaDataHelper.GetObjectTypeParentID(documentType);
      if (objectTypeParentId != -1)
      {
        OrderProcessTemplates processTemplates = OfficeDocumentTypeService.GetOrderProcessTemplates(sessionById, objectTypeParentId);
        if (!processTemplates.Empty)
        {
          processTemplates.FromParent = true;
          officeTypeSettings.ProcessTemplates = processTemplates;
        }
      }
    }
    return officeTypeSettings;
  }

  [NotNull]
  private static OrderProcessTemplates GetOrderProcessTemplates(
    [NotNull] IUserSession session,
    int documentType)
  {
    OrderProcessTemplates processTemplates = new OrderProcessTemplates();
    DBMetadataExtensions metadataExtensions = session.GetObjectType(documentType, true).As<DBMetadataExtensions>();
    string mdValue1 = metadataExtensions.GetMDValue("ProcessTemplateControl");
    if (GuidHelper.IsGuid(mdValue1))
      processTemplates.Control = new Guid(mdValue1);
    string mdValue2 = metadataExtensions.GetMDValue("ProcessTemplateNoControl");
    if (GuidHelper.IsGuid(mdValue2))
      processTemplates.NoControl = new Guid(mdValue2);
    string mdValue3 = metadataExtensions.GetMDValue("SuccProcessTemplateControl");
    if (GuidHelper.IsGuid(mdValue3))
      processTemplates.SuccessiveControl = new Guid(mdValue3);
    string mdValue4 = metadataExtensions.GetMDValue("SuccProcessTemplateNoControl");
    if (GuidHelper.IsGuid(mdValue4))
      processTemplates.SuccessiveNoControl = new Guid(mdValue4);
    return processTemplates;
  }

  public void SetSettings(Guid sessionGuid, int documentType, OfficeDocumentTypeSettings settings)
  {
    IUserSession sessionById = UserSession.GetSessionByID(sessionGuid);
    OfficeDocumentTypeSettings settings1 = this.GetSettings(sessionGuid, documentType);
    int anObjectType = documentType;
    DBMetadataExtensions metadataExtensions = sessionById.GetObjectType(anObjectType, true).As<DBMetadataExtensions>();
    bool flag = !settings.ProcessTemplates.Empty && (!settings.ProcessTemplates.FromParent || settings.ProcessTemplates.Changed);
    if (settings1.Equals((object) settings))
      return;
    int[] valuesList = Array.Empty<int>();
    if (settings.EnableTypes != null && settings.EnableTypes.Length != 0)
    {
      valuesList = new int[settings.EnableTypes.Length];
      for (int index = 0; index < settings.EnableTypes.Length; ++index)
        valuesList[index] = (int) settings.EnableTypes[index];
    }
    metadataExtensions.SetMDValues("EnableTypes", 0, valuesList);
    metadataExtensions.SetMDValue("ProcessTemplateControl", flag ? Convert.ToString((object) settings.ProcessTemplates.Control) : string.Empty);
    metadataExtensions.SetMDValue("ProcessTemplateNoControl", flag ? Convert.ToString((object) settings.ProcessTemplates.NoControl) : string.Empty);
    metadataExtensions.SetMDValue("SuccProcessTemplateControl", flag ? Convert.ToString((object) settings.ProcessTemplates.SuccessiveControl) : string.Empty);
    metadataExtensions.SetMDValue("SuccProcessTemplateNoControl", flag ? Convert.ToString((object) settings.ProcessTemplates.SuccessiveNoControl) : string.Empty);
    Intermech.Diagnostics.Check.NotNull<Dictionary<OfficeDocumentTypes, RegNumberSettings>>(settings.Templates, "settings.Templates");
    Intermech.Diagnostics.Check.NotNull<Dictionary<OfficeDocumentTypes, bool>>(settings.EnableEmptyRegNumbers, "settings.EnableEmptyRegNumbers");
    foreach (OfficeDocumentTypes officeDocumentTypes in Enum.GetValues(typeof (OfficeDocumentTypes)))
    {
      if (officeDocumentTypes != OfficeDocumentTypes.Unknown)
      {
        RegNumberSettings template = settings.Templates[officeDocumentTypes];
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "TemplateString"), template.Template);
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "EnableEmptyRegNumbers"), Convert.ToString(template.EnableEmptyRegNumbers, (IFormatProvider) CultureInfo.InvariantCulture));
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "AutoGenerateRegNumber"), Convert.ToString(template.AutoGenerateRegNumber, (IFormatProvider) CultureInfo.InvariantCulture));
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "CountResetType"), Convert.ToString((int) template.CountResetType));
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "CountWithinType"), Convert.ToString(template.CountWithinType, (IFormatProvider) CultureInfo.InvariantCulture));
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "CountWithinUnit"), Convert.ToString(template.CountWithinUnit, (IFormatProvider) CultureInfo.InvariantCulture));
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "DesignationEqualRegNumber"), Convert.ToString(template.DesignationEqualRegNumber, (IFormatProvider) CultureInfo.InvariantCulture));
        metadataExtensions.SetMDValue(this.GetPropertyName(officeDocumentTypes, "EmptyRegNumber"), Convert.ToString(settings.EnableEmptyRegNumbers[officeDocumentTypes]));
      }
    }
  }

  public Dictionary<OfficeDocumentTypes, CountResetTypes> GetOwnResetModes(Guid sessionGuid)
  {
    IDBAttribute attributeById = UserSession.GetSessionByID(sessionGuid).GetObject(OfficeConsts.ObjectCounterID).GetAttributeByID(OfficeConsts.AttrCounterResetModesID);
    Dictionary<OfficeDocumentTypes, CountResetTypes> ownResetModes = new Dictionary<OfficeDocumentTypes, CountResetTypes>(3);
    if (attributeById == null || attributeById.AsString.Length != 3)
    {
      ownResetModes.Add(OfficeDocumentTypes.Incoming, CountResetTypes.None);
      ownResetModes.Add(OfficeDocumentTypes.Outgoing, CountResetTypes.None);
      ownResetModes.Add(OfficeDocumentTypes.Internal, CountResetTypes.None);
    }
    else
    {
      ownResetModes.Add(OfficeDocumentTypes.Incoming, (CountResetTypes) Convert.ToInt32(attributeById.AsString[0].ToString()));
      ownResetModes.Add(OfficeDocumentTypes.Outgoing, (CountResetTypes) Convert.ToInt32(attributeById.AsString[1].ToString()));
      ownResetModes.Add(OfficeDocumentTypes.Internal, (CountResetTypes) Convert.ToInt32(attributeById.AsString[2].ToString()));
    }
    return ownResetModes;
  }

  public void SetOwnResetModes(
    Guid sessionGuid,
    Dictionary<OfficeDocumentTypes, CountResetTypes> resetTypes)
  {
    IDBObject dbObject = UserSession.GetSessionByID(sessionGuid).GetObject(OfficeConsts.ObjectCounterID);
    (dbObject.GetAttributeByID(OfficeConsts.AttrCounterResetModesID) ?? dbObject.Attributes.AddAttribute(OfficeConsts.AttrCounterResetModesID, false)).AsString = Convert.ToString((int) resetTypes[OfficeDocumentTypes.Incoming]) + Convert.ToString((int) resetTypes[OfficeDocumentTypes.Outgoing]) + Convert.ToString((int) resetTypes[OfficeDocumentTypes.Internal]);
  }

  public Dictionary<int, OfficeDocumentTypeSettingsForUnit> GetTypeSettingsForUnit(long unitID)
  {
    return this._unitSettingsCache.GetSettingsForUnit(unitID);
  }

  public void SetTypeSettingsForUnit(
    long unitID,
    Dictionary<int, OfficeDocumentTypeSettingsForUnit> settings)
  {
    this._unitSettingsCache.SetSettingsForUnit(unitID, settings);
  }

  public OfficeDocumentTypeSettingsForUnit GetSettings(long unitID, int documentType)
  {
    Dictionary<int, OfficeDocumentTypeSettingsForUnit> settingsForUnit = this._unitSettingsCache.GetSettingsForUnit(unitID);
    if (settingsForUnit == null)
      return (OfficeDocumentTypeSettingsForUnit) null;
    OfficeDocumentTypeSettingsForUnit typeSettingsForUnit;
    return !settingsForUnit.TryGetValue(documentType, out typeSettingsForUnit) ? (OfficeDocumentTypeSettingsForUnit) null : typeSettingsForUnit;
  }
}
