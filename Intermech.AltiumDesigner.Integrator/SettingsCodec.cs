// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SettingsCodec
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class SettingsCodec(string integratorName) : ECADSettingsCodec<ADIntegratorSettings>(integratorName)
{
  protected override int GetEncoderFormatVersion() => 1;

  private string GetListGuidsForXml(List<GlobalId<int>> items)
  {
    string empty = string.Empty;
    for (int index = 0; index < items.Count; ++index)
    {
      if (index > 0)
        empty += ";";
      empty += items[index].Guid.ToString();
    }
    return empty;
  }

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    base.EncodeSettings(settingsObject, settingsBuilder);
    ADIntegratorSettings integratorSettings = (ADIntegratorSettings) settingsObject;
    XmlElement element1 = settingsBuilder.CreateElement("DocumentTypes");
    element1.AppendChild((XmlNode) settingsBuilder.EncodeText("Project", integratorSettings.ProjectType != null ? integratorSettings.ProjectType.Guid.ToString() : string.Empty));
    if (integratorSettings.SchemaDocumentTypes != null && integratorSettings.SchemaDocumentTypes.Count > 0)
      element1.AppendChild((XmlNode) settingsBuilder.EncodeText("ElectricSchema", this.GetListGuidsForXml(integratorSettings.SchemaDocumentTypes)));
    if (integratorSettings.PCBDocumentTypes != null && integratorSettings.PCBDocumentTypes.Count > 0)
      element1.AppendChild((XmlNode) settingsBuilder.EncodeText("PCBDocumentTypes", this.GetListGuidsForXml(integratorSettings.PCBDocumentTypes)));
    settingsBuilder.AppendElement((XmlNode) element1);
    XmlElement element2 = settingsBuilder.CreateElement("Attributes");
    if (integratorSettings.ProjectAttributes != null)
      element2.AppendChild((XmlNode) settingsBuilder.EncodeText("Project", this.GetListAttributesForXml(integratorSettings.ProjectAttributes)));
    settingsBuilder.AppendElement((XmlNode) element2);
    XmlElement element3 = settingsBuilder.CreateElement("General");
    if (!string.IsNullOrEmpty(integratorSettings.AdditionalFilesExt))
      element3.AppendChild((XmlNode) settingsBuilder.EncodeText("AdditionalFilesExt", integratorSettings.AdditionalFilesExt));
    if (!string.IsNullOrEmpty(integratorSettings.PartTypeParameter))
      element3.AppendChild((XmlNode) settingsBuilder.EncodeText("PartTypeParameter", integratorSettings.PartTypeParameter));
    if (integratorSettings.ComponentsFilter != null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string format = "{0},";
      stringBuilder.AppendFormat(format, (object) (int) integratorSettings.ComponentsFilter.Table.Standard.Value);
      stringBuilder.AppendFormat(format, (object) (int) integratorSettings.ComponentsFilter.Table.Graphical.Value);
      stringBuilder.AppendFormat(format, (object) (int) integratorSettings.ComponentsFilter.Table.Mechanical.Value);
      stringBuilder.AppendFormat(format, (object) (int) integratorSettings.ComponentsFilter.Table.NetTie_BOM.Value);
      stringBuilder.AppendFormat(format, (object) (int) integratorSettings.ComponentsFilter.Table.NetTie_NoBOM.Value);
      stringBuilder.Append((int) integratorSettings.ComponentsFilter.Table.Standard_NoBOM.Value);
      element3.AppendChild((XmlNode) settingsBuilder.EncodeText("ComponentsCompositionVariants", stringBuilder.ToString()));
      element3.AppendChild((XmlNode) settingsBuilder.EncodeText("OnlyElementListCondition_Parameter", Convert.ToString((string) integratorSettings.ComponentsFilter.OnlyElementListCondition.Item1)));
      element3.AppendChild((XmlNode) settingsBuilder.EncodeText("OnlyElementListCondition_Value", Convert.ToString(integratorSettings.ComponentsFilter.OnlyElementListCondition.Item2.ToString())));
    }
    if (!string.IsNullOrEmpty(integratorSettings.GerberFiles))
      element3.AppendChild((XmlNode) settingsBuilder.EncodeText("GerberFilesFolders", integratorSettings.GerberFiles));
    element3.AppendChild((XmlNode) settingsBuilder.EncodeText("QuantityParameter", integratorSettings.QuantityParameter));
    settingsBuilder.AppendElement((XmlNode) element3);
  }

  protected override List<XmlNode> GetCustomParameterValuePairs(
    SettingsXmlBuilder settingsBuilder,
    ADIntegratorSettings settings)
  {
    return new List<XmlNode>()
    {
      this.EncodeParameterValuePair(settingsBuilder, settings.VariantsFilter, "VariantsFilter")
    };
  }

  protected override void SetCustomParameterValuePairs(
    string tableType,
    List<Tuple<StringKey, StringKey>> pairs,
    ADIntegratorSettings settings)
  {
    if (!tableType.Equals("VariantsFilter"))
      return;
    settings.VariantsFilter = pairs;
  }

  private string GetListAttributesForXml(List<Tuple<StringKey, StringKey, bool>> list)
  {
    string empty = string.Empty;
    for (int index = 0; index < list.Count; ++index)
    {
      if (index > 0)
        empty += ";";
      Tuple<StringKey, StringKey, bool> tuple = list[index];
      empty += $"{tuple.Item1}={tuple.Item2}";
    }
    return empty;
  }

  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    IntegratorServerDataBuilder serverData)
  {
    base.EncodeServerData(settingsObject, serverData);
    ADIntegratorSettings integratorSettings = (ADIntegratorSettings) settingsObject;
    if (integratorSettings.ProjectType != null)
      serverData.AddObjectType(integratorSettings.ProjectType.Guid);
    if (integratorSettings.SchemaDocumentTypes != null)
    {
      foreach (GlobalId<int> schemaDocumentType in integratorSettings.SchemaDocumentTypes)
        serverData.AddObjectType(schemaDocumentType.Guid);
    }
    if (integratorSettings.PCBDocumentTypes == null)
      return;
    foreach (GlobalId<int> pcbDocumentType in integratorSettings.PCBDocumentTypes)
      serverData.AddObjectType(pcbDocumentType.Guid);
  }

  protected override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
    if (formatVersion != 1)
      return;
    this.DecodeV1(settingsBuilder, (ADIntegratorSettings) settingsObject);
  }

  private void DecodeV1(SettingsXmlBuilder settingsBuilder, ADIntegratorSettings settings)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      string str = this.TrimStringValue(settingsBuilder.DecodeText("DocumentTypes/Project", (string) null));
      settings.ProjectType = this.DecodeType(sessionKeeper.Session, !string.IsNullOrEmpty(str) ? str : AltiumObjectTypeGuids.ProjectTypeGuid);
      string[] listGuidsFromNode1 = this.GetListGuidsFromNode(settingsBuilder, "DocumentTypes/ElectricSchema");
      settings.SchemaDocumentTypes = new List<GlobalId<int>>();
      if (listGuidsFromNode1 != null)
      {
        foreach (string guid in listGuidsFromNode1)
          settings.SchemaDocumentTypes.Add(this.DecodeType(sessionKeeper.Session, guid));
      }
      else
        settings.SchemaDocumentTypes.AddRange((IEnumerable<GlobalId<int>>) new List<GlobalId<int>>()
        {
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE0),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE1),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE2),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE3),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE4),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE5),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE6),
          this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.ElectricCircuitE7)
        });
      string[] listGuidsFromNode2 = this.GetListGuidsFromNode(settingsBuilder, "DocumentTypes/PCBDocumentTypes");
      settings.PCBDocumentTypes = new List<GlobalId<int>>();
      if (listGuidsFromNode2 != null)
      {
        foreach (string guid in listGuidsFromNode2)
          settings.PCBDocumentTypes.Add(this.DecodeType(sessionKeeper.Session, guid));
      }
      else
        settings.PCBDocumentTypes.Add(this.DecodeType(sessionKeeper.Session, AltiumObjectTypeGuids.PCBDocumentType));
    }
    settings.ProjectAttributes = new List<Tuple<StringKey, StringKey, bool>>();
    this.SetListAttributesFromXml(settingsBuilder, "Attributes/Project", settings.ProjectAttributes);
    settings.AdditionalFilesExt = this.TrimStringValue(settingsBuilder.DecodeText("General/AdditionalFilesExt", string.Empty));
    settings.PartTypeParameter = this.TrimStringValue(settingsBuilder.DecodeText("General/PartTypeParameter", string.Empty));
    settings.GerberFiles = this.TrimStringValue(settingsBuilder.DecodeText("General/GerberFilesFolders", string.Empty));
    settings.QuantityParameter = this.TrimStringValue(settingsBuilder.DecodeText("General/QuantityParameter", string.Empty));
    string[] strArray = this.TrimStringValue(settingsBuilder.DecodeText("General/ComponentsCompositionVariants", string.Empty)).Split(',');
    settings.ComponentsFilter = new ComponentsFilterSettings<ADComponentsCompositionVariants>();
    if (strArray.Length == 6)
    {
      settings.ComponentsFilter.Table.Standard = this.MakeCompositionVariantsProxy(strArray[0]);
      settings.ComponentsFilter.Table.Graphical = this.MakeCompositionVariantsProxy(strArray[1]);
      settings.ComponentsFilter.Table.Mechanical = this.MakeCompositionVariantsProxy(strArray[2]);
      settings.ComponentsFilter.Table.NetTie_BOM = this.MakeCompositionVariantsProxy(strArray[3]);
      settings.ComponentsFilter.Table.NetTie_NoBOM = this.MakeCompositionVariantsProxy(strArray[4]);
      settings.ComponentsFilter.Table.Standard_NoBOM = this.MakeCompositionVariantsProxy(strArray[5]);
    }
    settings.ComponentsFilter.OnlyElementListCondition = new Tuple<StringKey, string>(new StringKey(this.TrimStringValue(settingsBuilder.DecodeText("General/OnlyElementListCondition_Parameter", string.Empty))), this.TrimStringValue(settingsBuilder.DecodeText("General/OnlyElementListCondition_Value", string.Empty)));
  }

  private CompositionVariantsProxy MakeCompositionVariantsProxy(string strValue)
  {
    return new CompositionVariantsProxy((CompositionVariants) Convert.ToInt32(strValue));
  }

  private void SetListAttributesFromXml(
    SettingsXmlBuilder settingsBuilder,
    string nodeName,
    List<Tuple<StringKey, StringKey, bool>> list)
  {
    string str1 = this.TrimStringValue(settingsBuilder.DecodeText(nodeName, (string) null));
    if (str1 != string.Empty)
    {
      string str2 = str1;
      char[] chArray1 = new char[1]{ ';' };
      foreach (string str3 in str2.Split(chArray1))
      {
        char[] chArray2 = new char[1]{ '=' };
        string[] strArray = str3.Split(chArray2);
        list.Add(new Tuple<StringKey, StringKey, bool>(new StringKey(strArray[0]), new StringKey(strArray[1]), true));
      }
    }
    if (list.Count != 0)
      return;
    list = (List<Tuple<StringKey, StringKey, bool>>) null;
  }

  private string[] GetListGuidsFromNode(SettingsXmlBuilder settingsBuilder, string nodeName)
  {
    string str = this.TrimStringValue(settingsBuilder.DecodeText(nodeName, (string) null));
    if (string.IsNullOrEmpty(str))
      return (string[]) null;
    return str.Split(';');
  }

  private GlobalId<int> DecodeType(IUserSession session, Guid typeGuid)
  {
    IDBObjectType objectType = session.GetObjectType(typeGuid, false);
    return objectType == null ? (GlobalId<int>) null : new GlobalId<int>(typeGuid, objectType.ObjectType, objectType.ObjectTypeName);
  }

  private GlobalId<int> DecodeType(IUserSession session, string guid)
  {
    return GuidHelper.IsGuid(guid) ? this.DecodeType(session, new Guid(guid)) : (GlobalId<int>) null;
  }
}
