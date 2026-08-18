// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadIntegratorSettingsCodec
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadIntegratorSettingsCodec(string integratorName) : IntegratorSettingsCodec(integratorName)
{
  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new AcadIntegratorSettings();
  }

  protected override int GetEncoderFormatVersion() => 2;

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    this.EncodeV2((AcadIntegratorSettings) settingsObject, settingsBuilder);
  }

  private void EncodeV2(AcadIntegratorSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    settingsBuilder.AppendElement((XmlNode) this.EncodeStartupConfigurations(settings, settingsBuilder));
    settingsBuilder.AppendElement((XmlNode) this.EncodeMechanicalSettings(settings, settingsBuilder));
    settingsBuilder.AppendElement((XmlNode) this.EncodeConstructionalSettings(settings, settingsBuilder));
  }

  private XmlElement EncodeStartupConfigurations(
    AcadIntegratorSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element1 = settingsBuilder.CreateElement("StartupConfigurations");
    foreach (AcadStartupConfiguration startupConfiguration in settings.StartupConfigurations)
    {
      XmlElement element2 = settingsBuilder.CreateElement("Item");
      if (startupConfiguration.UserRole != null)
      {
        XmlElement element3 = settingsBuilder.CreateElement("UserRole");
        settingsBuilder.AppendAttribute((XmlNode) element3, "id", (object) startupConfiguration.UserRole.Id);
        settingsBuilder.AppendAttribute((XmlNode) element3, "name", (object) startupConfiguration.UserRole.Name);
        element2.AppendChild((XmlNode) element3);
      }
      XmlElement element4 = settingsBuilder.CreateElement("Profile");
      settingsBuilder.AppendAttribute((XmlNode) element4, "use", (object) startupConfiguration.UseSpecificProfile);
      settingsBuilder.AppendAttribute((XmlNode) element4, "name", (object) startupConfiguration.ProfileName);
      element2.AppendChild((XmlNode) element4);
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  private XmlElement EncodeMechanicalSettings(
    AcadIntegratorSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("MechanicalSettings");
    settingsBuilder.AppendAttribute((XmlNode) element, "EnableSupport", (object) settings.MechanicalSettings.IsEnabled);
    element.AppendChild((XmlNode) this.EncodeDrawingTypeList("AssemblyDrawings", settings.MechanicalSettings.AssemblyDrawings, settingsBuilder));
    element.AppendChild((XmlNode) this.EncodeDrawingTypeList("PartDrawings", settings.MechanicalSettings.PartDrawings, settingsBuilder));
    return element;
  }

  private XmlElement EncodeConstructionalSettings(
    AcadIntegratorSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("ConstructionalSettings");
    settingsBuilder.AppendAttribute((XmlNode) element, "EnableSupport", (object) settings.ConstructionalSettings.IsEnabled);
    element.AppendChild((XmlNode) this.EncodeDrawingTypeList("Drawings", settings.ConstructionalSettings.Drawings, settingsBuilder));
    return element;
  }

  private XmlElement EncodeDrawingTypeList(
    string elementName,
    List<DrawingTypeSettings> drawingTypes,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element1 = settingsBuilder.CreateElement(elementName);
    foreach (DrawingTypeSettings drawingType in drawingTypes)
    {
      XmlElement element2 = settingsBuilder.CreateElement("DocumentType");
      settingsBuilder.AppendAttribute((XmlNode) element2, "guid", (object) drawingType.DocumentType.Guid);
      XmlElement element3 = settingsBuilder.CreateElement("XRef");
      settingsBuilder.AppendAttribute((XmlNode) element3, "mode", (object) Enum.GetName(typeof (XRefMode), (object) drawingType.XRefMode));
      element2.AppendChild((XmlNode) element3);
      element2.AppendChild((XmlNode) settingsBuilder.EncodeText("StmName", drawingType.StmName));
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    IntegratorServerDataBuilder serverData)
  {
    base.EncodeServerData(settingsObject, serverData);
    AcadIntegratorSettings integratorSettings = (AcadIntegratorSettings) settingsObject;
    if (integratorSettings.MechanicalSettings.IsEnabled)
    {
      foreach (DrawingTypeSettings assemblyDrawing in integratorSettings.MechanicalSettings.AssemblyDrawings)
        serverData.AddObjectType(assemblyDrawing.DocumentType.Guid);
      foreach (DrawingTypeSettings partDrawing in integratorSettings.MechanicalSettings.PartDrawings)
        serverData.AddObjectType(partDrawing.DocumentType.Guid);
    }
    if (!integratorSettings.ConstructionalSettings.IsEnabled)
      return;
    foreach (DrawingTypeSettings drawing in integratorSettings.ConstructionalSettings.Drawings)
      serverData.AddObjectType(drawing.DocumentType.Guid);
  }

  protected override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    AcadIntegratorSettings settings = (AcadIntegratorSettings) settingsObject;
    if (formatVersion == 1 || formatVersion == 2)
      this.DecodeV2(settingsBuilder, settings);
    else
      base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
  }

  private void DecodeV2(SettingsXmlBuilder settingsBuilder, AcadIntegratorSettings settings)
  {
    this.DecodeStartupConfigurations(settingsBuilder, settings);
    this.DecodeMechanicalSettings(settingsBuilder, settings);
    this.DecodeConstructionalSettings(settingsBuilder, settings);
  }

  private void DecodeStartupConfigurations(
    SettingsXmlBuilder settingsBuilder,
    AcadIntegratorSettings settings)
  {
    XmlNodeList xmlNodeList = settingsBuilder.SelectNodes("StartupConfigurations/Item");
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (XmlNode xmlNode in xmlNodeList)
      {
        AcadStartupConfiguration startupConfiguration = new AcadStartupConfiguration();
        XmlNode parentNode1 = xmlNode.SelectSingleNode("UserRole[@id and @name]");
        if (parentNode1 != null)
        {
          Guid guid = settingsBuilder.ReadAttribute<Guid>(parentNode1, "id", Guid.Empty);
          if (guid != Guid.Empty)
          {
            IDBObject dbObject = sessionKeeper.Session.GetObject(guid, false);
            if (dbObject != null)
              startupConfiguration.UserRole = new UserRoleMarker(guid, dbObject.Caption);
          }
        }
        XmlNode parentNode2 = xmlNode.SelectSingleNode("Profile[@use and @name]");
        if (parentNode2 != null)
        {
          startupConfiguration.UseSpecificProfile = settingsBuilder.ReadAttribute<bool>(parentNode2, "use", startupConfiguration.UseSpecificProfile);
          startupConfiguration.ProfileName = SettingsUtils.TrimStringValue(settingsBuilder.ReadAttribute(parentNode2, "name", startupConfiguration.ProfileName));
        }
        settings.StartupConfigurations.Add(startupConfiguration);
      }
    }
  }

  private void DecodeMechanicalSettings(
    SettingsXmlBuilder settingsBuilder,
    AcadIntegratorSettings settings)
  {
    XmlNode xmlNode = settingsBuilder.SelectSingleNode("MechanicalSettings[@EnableSupport]");
    if (xmlNode == null)
      return;
    settings.MechanicalSettings.IsEnabled = settingsBuilder.ReadAttribute<bool>(xmlNode, "EnableSupport", settings.MechanicalSettings.IsEnabled);
    settings.MechanicalSettings.AssemblyDrawings.AddRange((IEnumerable<DrawingTypeSettings>) this.DecodeDrawingTypeList(xmlNode, "AssemblyDrawings", settingsBuilder));
    settings.MechanicalSettings.PartDrawings.AddRange((IEnumerable<DrawingTypeSettings>) this.DecodeDrawingTypeList(xmlNode, "PartDrawings", settingsBuilder));
  }

  private void DecodeConstructionalSettings(
    SettingsXmlBuilder settingsBuilder,
    AcadIntegratorSettings settings)
  {
    XmlNode xmlNode = settingsBuilder.SelectSingleNode("ConstructionalSettings[@EnableSupport]");
    if (xmlNode == null)
      return;
    settings.ConstructionalSettings.IsEnabled = settingsBuilder.ReadAttribute<bool>(xmlNode, "EnableSupport", settings.ConstructionalSettings.IsEnabled);
    settings.ConstructionalSettings.Drawings.AddRange((IEnumerable<DrawingTypeSettings>) this.DecodeDrawingTypeList(xmlNode, "Drawings", settingsBuilder));
  }

  private List<DrawingTypeSettings> DecodeDrawingTypeList(
    XmlNode rootElem,
    string listElemName,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlNodeList xmlNodeList = rootElem.SelectNodes($"{listElemName}/DocumentType[@guid and XRef/@mode]");
    List<DrawingTypeSettings> drawingTypeSettingsList = new List<DrawingTypeSettings>(xmlNodeList.Count);
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    foreach (XmlElement parentNode1 in xmlNodeList)
    {
      Guid guid = settingsBuilder.ReadAttribute<Guid>((XmlNode) parentNode1, "guid", Guid.Empty);
      if (!(guid == Guid.Empty))
      {
        IDBObjectTypeInfo objectType = service.GetObjectType(guid, false);
        if (objectType != null)
        {
          DrawingTypeSettings drawingTypeSettings = new DrawingTypeSettings(new GlobalId<int>(guid, objectType.ObjectType, objectType.ObjectTypeName));
          XmlNode parentNode2 = parentNode1.SelectSingleNode("XRef[@mode]");
          drawingTypeSettings.XRefMode = (XRefMode) Enum.Parse(typeof (XRefMode), settingsBuilder.ReadAttribute(parentNode2, "mode", string.Empty));
          drawingTypeSettings.StmName = SettingsUtils.TrimStringValue(settingsBuilder.DecodeText((XmlNode) parentNode1, "StmName", string.Empty));
          drawingTypeSettingsList.Add(drawingTypeSettings);
        }
      }
    }
    return drawingTypeSettingsList;
  }
}
