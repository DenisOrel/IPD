// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADSettingsCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Tools.Integrators.Mechanical;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Базовый тип для кодеков настроек интеграторов, созданных на основе CAD-интерфейса.
/// </summary>
public class CADSettingsCodec : IntegratorSettingsCodec
{
  private readonly ISettingsObjectFactory factory;

  /// <summary>Создает объект.</summary>
  /// <param name="integratorName">Название интегратора</param>
  /// <param name="factory">Фабрика объектов настроек</param>
  public CADSettingsCodec(string integratorName, ISettingsObjectFactory factory)
    : base(integratorName)
  {
    this.factory = factory != null ? factory : throw new ArgumentNullException(nameof (factory), LocalizationHolder.rm.GetString("Tools.Components_351"));
  }

  protected sealed override int GetEncoderFormatVersion() => 2;

  /// <summary>
  /// Выполняет преобразование объекта с настройками интегратора в xml-документ.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  /// <param name="settingsBuilder">Настройки интегратора в форме xml-документа</param>
  protected sealed override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    CADSettings cadSettings = (CADSettings) settingsObject;
    this.EncodeV2(cadSettings, settingsBuilder);
    this.EncodeCustomSettings(cadSettings, settingsBuilder);
  }

  /// <summary>
  /// Сохраняет в xml-документе настройки, специфические для конкретного интегратора.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  /// <param name="settingsBuilder">Настройки интегратора в форме xml-документа</param>
  protected virtual void EncodeCustomSettings(
    CADSettings settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
  }

  private void EncodeV2(CADSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    this.EncodeV1(settings, settingsBuilder);
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeTextList("DrawingSuffixes", "Suffix", (ICollection<string>) settings.DrawingSuffixes));
    settingsBuilder.AppendElement((XmlNode) this.EncodeSubstitutions(settings, settingsBuilder));
    settingsBuilder.AppendElement((XmlNode) this.EncodeCADLinkTypeSettings(settings, settingsBuilder));
    settingsBuilder.AppendElement((XmlNode) this.EncodeUnpairedDocumentTypes(settings, settingsBuilder));
    settingsBuilder.AppendElement((XmlNode) this.EncodeNeutralDocumentTypes(settings, settingsBuilder));
    settingsBuilder.AppendElement(this.EncodeIMViewerSupport(settings, settingsBuilder));
    settingsBuilder.AppendElement(this.EncodeCheckinSettings(settings, settingsBuilder));
  }

  private void EncodeV1(CADSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    settingsBuilder.AppendElement((XmlNode) this.EncodeDrawingMode(settingsBuilder, settings.NewDrawingMode));
    settingsBuilder.AppendElement((XmlNode) this.EncodeObjectAttributes(settingsBuilder, settings.CustomDocumentAttributes, "document"));
    settingsBuilder.AppendElement((XmlNode) this.EncodeObjectAttributes(settingsBuilder, settings.CustomArticleAttributes, "article"));
    foreach (DocumentGroup fileDocumentGroup in (Collection<DocumentGroup>) settings.FileDocumentGroups)
      settingsBuilder.AppendElement((XmlNode) this.EncodeDocumentGroup(settingsBuilder, fileDocumentGroup));
    if (settings.StandardPartType == null)
      return;
    settingsBuilder.AppendElement((XmlNode) this.EncodeStandardPart(settingsBuilder, settings.StandardPartType));
  }

  private XmlElement EncodeDrawingMode(
    SettingsXmlBuilder settingsBuilder,
    NewDrawingMode newDrawingMode)
  {
    XmlElement element = settingsBuilder.CreateElement("NewDrawings");
    settingsBuilder.AppendAttribute((XmlNode) element, "mode", newDrawingMode == NewDrawingMode.Document ? (object) "document" : (object) "file");
    return element;
  }

  private XmlElement EncodeObjectAttributes(
    SettingsXmlBuilder settingsBuilder,
    List<GlobalId<int>> list,
    string listOwner)
  {
    XmlElement parentNode = settingsBuilder.EncodeObjectAttributes("CustomAttributes", (IEnumerable<GlobalId<int>>) list);
    settingsBuilder.AppendAttribute((XmlNode) parentNode, "owner", (object) listOwner);
    return parentNode;
  }

  private XmlElement EncodeDocumentGroup(SettingsXmlBuilder settingsBuilder, DocumentGroup docGroup)
  {
    XmlElement element1 = settingsBuilder.CreateElement("DocumentTemplate");
    settingsBuilder.AppendAttribute((XmlNode) element1, "type", (object) docGroup.Name);
    for (int index = 0; index < docGroup.DocumentTypes.Count; ++index)
    {
      XmlElement element2 = settingsBuilder.CreateElement("DocumentType");
      settingsBuilder.AppendAttribute((XmlNode) element2, "guid", (object) docGroup.DocumentTypes[index].Guid);
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  private XmlElement EncodeStandardPart(
    SettingsXmlBuilder settingsBuilder,
    GlobalId<int> standardPartType)
  {
    XmlElement element = settingsBuilder.CreateElement("StandardParts");
    settingsBuilder.AppendAttribute((XmlNode) element, "guid", (object) standardPartType.Guid);
    return element;
  }

  private XmlElement EncodeSubstitutions(CADSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element1 = settingsBuilder.CreateElement("Substitutions");
    XmlElement element2 = settingsBuilder.CreateElement("Synchronization");
    settingsBuilder.AppendAttribute((XmlNode) element2, "enabled", (object) settings.SynchronizeSubstitutions);
    element1.AppendChild((XmlNode) element2);
    return element1;
  }

  private XmlElement EncodeCADLinkTypeSettings(
    CADSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element1 = settingsBuilder.CreateElement("CADLinkTypeSettings");
    XmlElement element2 = settingsBuilder.CreateElement("FillRelationAttribute");
    settingsBuilder.AppendAttribute((XmlNode) element2, "enabled", (object) settings.EnableCADLinkTypeAttribute);
    element1.AppendChild((XmlNode) element2);
    return element1;
  }

  private XmlElement EncodeUnpairedDocumentTypes(
    CADSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    return this.EncodeDocumentGroup(settingsBuilder, settings.UnpairedDocumentTypes);
  }

  private XmlElement EncodeNeutralDocumentTypes(
    CADSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    return this.EncodeDocumentGroup(settingsBuilder, settings.NeutralDocumentTypes);
  }

  private XmlNode EncodeIMViewerSupport(CADSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("IMViewerSupport");
    settingsBuilder.AppendAttribute((XmlNode) element, "enabled", (object) settings.EnableIMViewerFiles);
    return (XmlNode) element;
  }

  private XmlNode EncodeCheckinSettings(CADSettings settings, SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("CheckinSettings");
    settingsBuilder.AppendAttribute((XmlNode) element, "updateModelAuthenticFilesOnCheckin", (object) settings.UpdateModelAuthenticFilesOnCheckin);
    settingsBuilder.AppendAttribute((XmlNode) element, "updateDrawingAuthenticFilesOnCheckin", (object) settings.UpdateDrawingAuthenticFilesOnCheckin);
    return (XmlNode) element;
  }

  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    IntegratorServerDataBuilder serverData)
  {
    base.EncodeServerData(settingsObject, serverData);
    CADSettings settings = (CADSettings) settingsObject;
    foreach (DocumentGroup fileDocumentGroup in (Collection<DocumentGroup>) settings.FileDocumentGroups)
    {
      if (this.CanEncodeServerData(settingsObject, fileDocumentGroup))
      {
        foreach (GlobalId<int> documentType in fileDocumentGroup.DocumentTypes)
          serverData.AddObjectType(documentType.Guid, this.GetDocumentTypeFlags(settings, fileDocumentGroup, documentType));
      }
    }
    serverData.AddObjectType(settings.StandardPartType.Guid, "readonlyModel");
    if (!settings.JTDerivativesEnabled)
      return;
    serverData.AddObjectType(settings.JTDerivedDocumentType.Guid);
  }

  protected virtual bool CanEncodeServerData(
    ISettingsObject settingsObject,
    DocumentGroup documentGroup)
  {
    return true;
  }

  private IEnumerable<string> GetDocumentTypeFlags(
    CADSettings settings,
    DocumentGroup documentGroup,
    GlobalId<int> documentType)
  {
    bool flag = settings.UnpairedDocumentTypes.ContainsType(documentType.Id);
    if (!flag)
      return (IEnumerable<string>) documentGroup.Flags;
    List<string> documentTypeFlags = new List<string>((IEnumerable<string>) documentGroup.Flags);
    if (flag)
      documentTypeFlags.Add("unpairedVersionCreation");
    return (IEnumerable<string>) documentTypeFlags;
  }

  public override ISettingsObject CreateEmptySettings() => this.factory.CreateSettingsObject();

  /// <summary>
  /// Выполняет преобразование xml-документа в объект с настройками интегратора.
  /// </summary>
  /// <param name="settingsBuilder">Настройки интегратора в форме xml-документа</param>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  protected sealed override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    CADSettings cadSettings = (CADSettings) settingsObject;
    if (formatVersion == 1)
    {
      this.DecodeV1(settingsBuilder, cadSettings);
      this.DecodeCustomSettings(settingsBuilder, cadSettings);
    }
    else if (formatVersion == 2)
    {
      this.DecodeV2(settingsBuilder, cadSettings);
      this.DecodeCustomSettings(settingsBuilder, cadSettings);
    }
    else
      base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
  }

  /// <summary>
  /// Восстанавливает из xml-документа настройки, специфические для конкретного интегратора.
  /// </summary>
  /// <param name="settingsBuilder">Настройки интегратора в форме xml-документа</param>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  protected virtual void DecodeCustomSettings(
    SettingsXmlBuilder settingsBuilder,
    CADSettings settingsObject)
  {
  }

  private void DecodeV2(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    this.DecodeV1(settingsBuilder, settings);
    settings.DrawingSuffixes.AddRange((IEnumerable<string>) settingsBuilder.DecodeTextList("DrawingSuffixes", "Suffix"));
    this.DecodeSubstitutions(settingsBuilder, settings);
    this.DecodeCADLinkTypeSettings(settingsBuilder, settings);
    this.DecodeUnpairedDocumentTypes(settingsBuilder, settings);
    this.DecodeNeutralDocumentTypes(settingsBuilder, settings);
    this.DecodeIMViewerSupport(settingsBuilder, settings);
    this.DecodeCheckinSettings(settingsBuilder, settings);
  }

  private void DecodeV1(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    settings.NewDrawingMode = this.DecodeDrawingMode(settingsBuilder);
    settings.CustomDocumentAttributes.AddRange((IEnumerable<GlobalId<int>>) this.DecodeObjectAttributes(settingsBuilder, "document"));
    settings.CustomArticleAttributes.AddRange((IEnumerable<GlobalId<int>>) this.DecodeObjectAttributes(settingsBuilder, "article"));
    this.DecodeDocumentGroups(settingsBuilder, settings);
    this.DecodeStandardPart(settingsBuilder, settings);
  }

  private NewDrawingMode DecodeDrawingMode(SettingsXmlBuilder settingsBuilder)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("NewDrawings[@mode]");
    string strA = settingsBuilder.ReadAttribute(parentNode, "mode", (string) null);
    return !string.IsNullOrEmpty(strA) && string.Compare(strA, "document", true) != 0 ? NewDrawingMode.AdditionalModelFile : NewDrawingMode.Document;
  }

  private void DecodeDocumentGroups(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    foreach (XmlElement selectNode in settingsBuilder.SelectNodes("DocumentTemplate[@type]"))
    {
      string groupName = selectNode.Attributes["type"].Value;
      if (!string.IsNullOrEmpty(groupName))
      {
        DocumentGroup byName = settings.FileDocumentGroups.FindByName(groupName, false);
        if (byName != null)
          this.DecodeDocumentGroup(settingsBuilder, selectNode, byName);
      }
    }
  }

  private void DecodeDocumentGroup(
    SettingsXmlBuilder settingsBuilder,
    XmlElement groupNode,
    DocumentGroup group)
  {
    XmlNodeList xmlNodeList = groupNode.SelectNodes("DocumentType[@guid]");
    IClientMetadataCache service = ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache;
    for (int i = 0; i < xmlNodeList.Count; ++i)
    {
      Guid guid = settingsBuilder.ReadAttribute<Guid>(xmlNodeList[i], "guid", Guid.Empty);
      if (!(guid == Guid.Empty))
      {
        IDBObjectTypeInfo objectType = service.GetObjectType(guid, false);
        if (objectType != null)
          group.DocumentTypes.Add(new GlobalId<int>(guid, objectType.ObjectType, objectType.ObjectTypeName));
      }
    }
  }

  private void DecodeStandardPart(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("StandardParts[@guid]");
    if (parentNode == null)
      return;
    Guid guid = settingsBuilder.ReadAttribute<Guid>(parentNode, "guid", Guid.Empty);
    if (guid == Guid.Empty)
      return;
    IDBObjectTypeInfo objectType = (ServicesManager.GetService(typeof (IClientMetadataCache)) as IClientMetadataCache).GetObjectType(guid, false);
    if (objectType == null)
      return;
    settings.StandardPartType = new GlobalId<int>(guid, objectType.ObjectType, objectType.ObjectTypeName);
  }

  private List<GlobalId<int>> DecodeObjectAttributes(
    SettingsXmlBuilder settingsBuilder,
    string listOwner)
  {
    return settingsBuilder.DecodeObjectAttributes($"CustomAttributes[@owner='{listOwner}']");
  }

  private void DecodeSubstitutions(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("Substitutions/Synchronization[@enabled]");
    if (parentNode != null)
      settings.SynchronizeSubstitutions = settingsBuilder.ReadAttribute<bool>(parentNode, "enabled", true);
    else
      settings.SynchronizeSubstitutions = true;
  }

  private void DecodeCADLinkTypeSettings(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("CADLinkTypeSettings/FillRelationAttribute[@enabled]");
    if (parentNode != null)
      settings.EnableCADLinkTypeAttribute = settingsBuilder.ReadAttribute<bool>(parentNode, "enabled", true);
    else
      settings.EnableCADLinkTypeAttribute = false;
  }

  private void DecodeUnpairedDocumentTypes(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlElement groupNode = (XmlElement) settingsBuilder.SelectSingleNode($"DocumentTemplate[@type = '{settings.UnpairedDocumentTypes.Name}']");
    if (groupNode == null)
      return;
    this.DecodeDocumentGroup(settingsBuilder, groupNode, settings.UnpairedDocumentTypes);
  }

  private void DecodeNeutralDocumentTypes(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlElement groupNode = (XmlElement) settingsBuilder.SelectSingleNode($"DocumentTemplate[@type = '{settings.NeutralDocumentTypes.Name}']");
    if (groupNode == null)
      return;
    this.DecodeDocumentGroup(settingsBuilder, groupNode, settings.NeutralDocumentTypes);
  }

  private void DecodeIMViewerSupport(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("IMViewerSupport[@enabled]");
    if (parentNode != null)
      settings.EnableIMViewerFiles = settingsBuilder.ReadAttribute<bool>(parentNode, "enabled", false);
    else
      settings.EnableIMViewerFiles = false;
  }

  private void DecodeCheckinSettings(SettingsXmlBuilder settingsBuilder, CADSettings settings)
  {
    XmlNode parentNode = settingsBuilder.SelectSingleNode("CheckinSettings");
    if (parentNode != null)
    {
      settings.UpdateModelAuthenticFilesOnCheckin = settingsBuilder.ReadAttribute<bool>(parentNode, "updateModelAuthenticFilesOnCheckin", false);
      settings.UpdateDrawingAuthenticFilesOnCheckin = settingsBuilder.ReadAttribute<bool>(parentNode, "updateDrawingAuthenticFilesOnCheckin", false);
    }
    else
    {
      settings.UpdateModelAuthenticFilesOnCheckin = false;
      settings.UpdateDrawingAuthenticFilesOnCheckin = false;
    }
  }
}
