// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADSettingsCodec`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public abstract class ECADSettingsCodec<TSettings> : IntegratorSettingsCodec where TSettings : ECADIntegratorSettings, new()
{
  public ECADSettingsCodec(string integratorName)
    : base(integratorName)
  {
  }

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    TSettings settings = (TSettings) settingsObject;
    settingsBuilder.AppendElement(this.EncodeSyncImbase(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeCheckImbaseApplicability(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeSyncImbaseAttribute(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeProjectFolder(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeFuncGroupAttributes(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeMiscParameters(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeReplaceAttributes(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeParameterValuePairs(settingsBuilder, settings));
    settingsBuilder.AppendElement(this.EncodeAttributeTables(settingsBuilder, settings));
  }

  public override ISettingsObject CreateEmptySettings()
  {
    TSettings settings = new TSettings();
    this.OnCreateEmptySettings(settings);
    return (ISettingsObject) settings;
  }

  protected virtual void OnCreateEmptySettings(TSettings settings)
  {
  }

  protected override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    TSettings settings = (TSettings) settingsObject;
    this.DecodeSyncImbase(settingsBuilder, settings);
    this.DecodeCheckImbaseApplicability(settingsBuilder, settings);
    this.DecodeSyncImbaseAttribute(settingsBuilder, settings);
    this.DecodeProjectFolder(settingsBuilder, settings);
    this.DecodeFuncGroupAttributes(settingsBuilder, settings);
    this.DecodeMiscParameters(settingsBuilder, settings);
    this.DecodeParameterValuePairs(settingsBuilder, settings);
    this.DecodeReplaceAttributes(settingsBuilder, settings);
    this.DecodeAttributeTables(settingsBuilder, settings);
  }

  private void DecodeMiscParameters(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.ASPosDesignation = this.TrimStringValue(settingsBuilder.DecodeText("Misc/ASPosDesignation", (string) null));
  }

  private void DecodeAttributeTables(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.AssemblyAttributesTable = (List<Tuple<StringKey, StringKey, bool>>) null;
    settings.DocumentAttributesTable = (List<Tuple<StringKey, StringKey, bool>>) null;
    settings.PartAttributesTable = (List<Tuple<StringKey, StringKey, bool>>) null;
    settings.RelationPartAttributesTable = (List<Tuple<StringKey, StringKey, bool>>) null;
    foreach (XmlNode selectNode in settingsBuilder.SelectNodes("AttributeTables/AttributeTable[@type]"))
    {
      XmlNodeList xmlNodeList = selectNode.SelectNodes("Attribute[@obligatory and DataBaseName and CADName]");
      List<Tuple<StringKey, StringKey, bool>> tupleList = new List<Tuple<StringKey, StringKey, bool>>(xmlNodeList.Count);
      foreach (XmlNode parentNode in xmlNodeList)
      {
        string str1 = this.TrimStringValue(settingsBuilder.DecodeText(parentNode, "DataBaseName", string.Empty));
        string str2 = this.TrimStringValue(settingsBuilder.DecodeText(parentNode, "CADName", string.Empty));
        bool flag = settingsBuilder.ReadAttribute<bool>(parentNode, "obligatory", false);
        if (str2 != string.Empty && str1 != string.Empty)
          tupleList.Add(new Tuple<StringKey, StringKey, bool>((StringKey) str1, (StringKey) str2, flag));
      }
      switch (settingsBuilder.ReadAttribute(selectNode, "type", string.Empty))
      {
        case "Document":
          settings.DocumentAttributesTable = tupleList;
          continue;
        case "Assembly":
          settings.AssemblyAttributesTable = tupleList;
          continue;
        case "Part":
          settings.PartAttributesTable = tupleList;
          continue;
        case "PartRelation":
          settings.RelationPartAttributesTable = tupleList;
          continue;
        default:
          continue;
      }
    }
  }

  private void DecodeReplaceAttributes(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.NominalsParameter = this.TrimStringValue(settingsBuilder.DecodeText("ReplaceAttributes/Nominals", (string) null));
  }

  protected List<Tuple<StringKey, StringKey>> DecodeListParameters(
    SettingsXmlBuilder settingsBuilder,
    XmlNode folderNode)
  {
    XmlNodeList xmlNodeList = folderNode.SelectNodes("Parameter");
    List<Tuple<StringKey, StringKey>> tupleList = new List<Tuple<StringKey, StringKey>>(xmlNodeList.Count);
    foreach (XmlNode parentNode in xmlNodeList)
    {
      string str1 = this.TrimStringValue(settingsBuilder.DecodeText(parentNode, "Name", string.Empty));
      string str2 = this.TrimStringValue(settingsBuilder.DecodeText(parentNode, "Value", string.Empty));
      if (str2 != string.Empty && str1 != string.Empty)
        tupleList.Add(new Tuple<StringKey, StringKey>((StringKey) str1, (StringKey) str2));
    }
    return tupleList;
  }

  private void DecodeParameterValuePairs(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.TuningParameters = (List<Tuple<StringKey, StringKey>>) null;
    settings.ReplaceParameters = (List<Tuple<StringKey, StringKey>>) null;
    foreach (XmlNode selectNode in settingsBuilder.SelectNodes("ParameterValuePairs/Pairs[@type]"))
    {
      List<Tuple<StringKey, StringKey>> pairs = this.DecodeListParameters(settingsBuilder, selectNode);
      string tableType = settingsBuilder.ReadAttribute(selectNode, "type", string.Empty);
      switch (tableType)
      {
        case "TuningParameters":
          settings.TuningParameters = pairs;
          continue;
        case "ReplaceParameters":
          settings.ReplaceParameters = pairs;
          continue;
        default:
          this.SetCustomParameterValuePairs(tableType, pairs, settings);
          continue;
      }
    }
  }

  protected virtual void SetCustomParameterValuePairs(
    string tableType,
    List<Tuple<StringKey, StringKey>> pairs,
    TSettings settings)
  {
  }

  private void DecodeFuncGroupAttributes(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.FGName = this.TrimStringValue(settingsBuilder.DecodeText("FuncGroupAttributes/Name", (string) null));
    settings.FGDesignation = this.TrimStringValue(settingsBuilder.DecodeText("FuncGroupAttributes/Designation", (string) null));
  }

  private void DecodeProjectFolder(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.NotImportingDir = new List<string>();
    settings.NotImportingDir.AddRange((IEnumerable<string>) settingsBuilder.DecodeTextList("ProjectFolder/NotImportingFolders", "Folder"));
  }

  private void DecodeSyncImbase(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    settings.ImbaseSync = Convert.ToBoolean(this.TrimStringValue(settingsBuilder.DecodeText("SyncImbase/Enable", false.ToString())));
  }

  private void DecodeCheckImbaseApplicability(
    SettingsXmlBuilder settingsBuilder,
    TSettings settings)
  {
    settings.ImbaseSyncCheckApplicability = Convert.ToBoolean(this.TrimStringValue(settingsBuilder.DecodeText("SyncImbase/CheckApplicability", false.ToString())));
  }

  private void DecodeSyncImbaseAttribute(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    IMSAttributeType attributeType = MetaDataHelper.GetAttributeType(new Guid(this.TrimStringValue(settingsBuilder.DecodeText("SyncImbase/Attribute", Guid.Empty.ToString()))));
    settings.ImbaseSyncAttribute = attributeType != null ? new GlobalId<int>(attributeType.AttributeGuid, attributeType.AttributeID, attributeType.Name) : (GlobalId<int>) null;
  }

  private XmlNode EncodeSyncImbase(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("SyncImbase");
    element.AppendChild((XmlNode) settingsBuilder.EncodeText("Enable", settings.ImbaseSync.ToString()));
    return (XmlNode) element;
  }

  private XmlNode EncodeCheckImbaseApplicability(
    SettingsXmlBuilder settingsBuilder,
    TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("SyncImbase");
    element.AppendChild((XmlNode) settingsBuilder.EncodeText("CheckApplicability", settings.ImbaseSyncCheckApplicability.ToString()));
    return (XmlNode) element;
  }

  private XmlNode EncodeSyncImbaseAttribute(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("SyncImbase");
    if (settings.ImbaseSyncAttribute != null)
      element.AppendChild((XmlNode) settingsBuilder.EncodeText("Attribute", settings.ImbaseSyncAttribute.Guid.ToString()));
    return (XmlNode) element;
  }

  private XmlNode EncodeProjectFolder(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("ProjectFolder");
    if (settings.NotImportingDir != null)
      element.AppendChild((XmlNode) settingsBuilder.EncodeTextList("NotImportingFolders", "Folder", (ICollection<string>) settings.NotImportingDir));
    return (XmlNode) element;
  }

  private XmlNode EncodeFuncGroupAttributes(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("FuncGroupAttributes");
    element.AppendChild((XmlNode) settingsBuilder.EncodeText("Name", settings.FGName));
    element.AppendChild((XmlNode) settingsBuilder.EncodeText("Designation", settings.FGDesignation));
    return (XmlNode) element;
  }

  private XmlNode EncodeMiscParameters(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("Misc");
    element.AppendChild((XmlNode) settingsBuilder.EncodeText("ASPosDesignation", settings.ASPosDesignation));
    return (XmlNode) element;
  }

  private XmlNode EncodeReplaceAttributes(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("ReplaceAttributes");
    element.AppendChild((XmlNode) settingsBuilder.EncodeText("Nominals", settings.NominalsParameter));
    return (XmlNode) element;
  }

  private XmlNode EncodeParameterValuePairs(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("ParameterValuePairs");
    element.AppendChild(this.EncodeParameterValuePair(settingsBuilder, settings.TuningParameters, "TuningParameters"));
    element.AppendChild(this.EncodeParameterValuePair(settingsBuilder, settings.ReplaceParameters, "ReplaceParameters"));
    List<XmlNode> parameterValuePairs = this.GetCustomParameterValuePairs(settingsBuilder, settings);
    if (parameterValuePairs != null)
    {
      foreach (XmlNode newChild in parameterValuePairs)
        element.AppendChild(newChild);
    }
    return (XmlNode) element;
  }

  protected virtual List<XmlNode> GetCustomParameterValuePairs(
    SettingsXmlBuilder settingsBuilder,
    TSettings settings)
  {
    return (List<XmlNode>) null;
  }

  private XmlNode EncodeAttributeTables(SettingsXmlBuilder settingsBuilder, TSettings settings)
  {
    XmlElement element = settingsBuilder.CreateElement("AttributeTables");
    element.AppendChild(this.EncodeAttributeTable(settingsBuilder, settings.PartAttributesTable, "Part"));
    element.AppendChild(this.EncodeAttributeTable(settingsBuilder, settings.DocumentAttributesTable, "Document"));
    element.AppendChild(this.EncodeAttributeTable(settingsBuilder, settings.AssemblyAttributesTable, "Assembly"));
    element.AppendChild(this.EncodeAttributeTable(settingsBuilder, settings.RelationPartAttributesTable, "PartRelation"));
    return (XmlNode) element;
  }

  protected XmlNode EncodeAttributeTable(
    SettingsXmlBuilder settingsBuilder,
    List<Tuple<StringKey, StringKey, bool>> table,
    string type)
  {
    XmlElement element1 = settingsBuilder.CreateElement("AttributeTable");
    settingsBuilder.AppendAttribute((XmlNode) element1, nameof (type), (object) type);
    if (table != null)
    {
      foreach (Tuple<StringKey, StringKey, bool> tuple in table)
      {
        XmlElement element2 = settingsBuilder.CreateElement("Attribute");
        settingsBuilder.AppendAttribute((XmlNode) element2, "obligatory", (object) tuple.Item3);
        element2.AppendChild((XmlNode) settingsBuilder.EncodeText("DataBaseName", (string) tuple.Item1));
        element2.AppendChild((XmlNode) settingsBuilder.EncodeText("CADName", (string) tuple.Item2));
        element1.AppendChild((XmlNode) element2);
      }
    }
    return (XmlNode) element1;
  }

  protected XmlNode EncodeParameterValuePair(
    SettingsXmlBuilder settingsBuilder,
    List<Tuple<StringKey, StringKey>> pairs,
    string type)
  {
    XmlElement element1 = settingsBuilder.CreateElement("Pairs");
    settingsBuilder.AppendAttribute((XmlNode) element1, nameof (type), (object) type);
    if (pairs != null)
    {
      foreach (Tuple<StringKey, StringKey> pair in pairs)
      {
        XmlElement element2 = settingsBuilder.CreateElement("Parameter");
        element2.AppendChild((XmlNode) settingsBuilder.EncodeText("Name", (string) pair.Item1));
        element2.AppendChild((XmlNode) settingsBuilder.EncodeText("Value", (string) pair.Item2));
        element1.AppendChild((XmlNode) element2);
      }
    }
    return (XmlNode) element1;
  }

  /// <summary>
  /// Удаление лишних пробелов из строки.
  /// Если входная строка null возвращает пустую строку
  /// </summary>
  protected string TrimStringValue(string value) => value == null ? string.Empty : value.Trim();
}
