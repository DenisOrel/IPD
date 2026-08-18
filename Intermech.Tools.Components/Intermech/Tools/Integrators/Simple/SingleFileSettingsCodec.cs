// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Simple.SingleFileSettingsCodec
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.PropertyEditors.ChangeHighlighting;
using Intermech.Tools.Settings;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Tools.Integrators.Simple;

public class SingleFileSettingsCodec(string integratorName) : IntegratorSettingsCodec(integratorName)
{
  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    SingleFileSettings settings = (SingleFileSettings) settingsObject;
    settingsBuilder.AppendElement((XmlNode) this.EncodeDocumentTypes(settings, settingsBuilder));
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeObjectAttributes("DocumentAttributes", (IEnumerable<GlobalId<int>>) settings.DocumentAttributes.Items));
  }

  private XmlElement EncodeDocumentTypes(
    SingleFileSettings settings,
    SettingsXmlBuilder settingsBuilder)
  {
    return settingsBuilder.EncodeObjectTypes("DocumentTypes", "DocumentType", (IEnumerable<GlobalId<int>>) settings.DocumentTypes);
  }

  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    IntegratorServerDataBuilder serverData)
  {
    base.EncodeServerData(settingsObject, serverData);
    foreach (GlobalId<int> documentType in ((SingleFileSettings) settingsObject).DocumentTypes)
      serverData.AddObjectType(documentType.Guid);
  }

  protected override int GetEncoderFormatVersion() => 1;

  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new SingleFileSettings();
  }

  protected override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    if (formatVersion == 1)
      this.DecodeV1(settingsBuilder, settingsObject);
    else
      base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
  }

  protected virtual void DecodeV1(
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    SingleFileSettings settings = (SingleFileSettings) settingsObject;
    this.DecodeDocumentType(settingsBuilder, settings);
    settings.DocumentAttributes = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) settingsBuilder.DecodeObjectAttributes("DocumentAttributes"));
  }

  private void DecodeDocumentType(SettingsXmlBuilder settingsBuilder, SingleFileSettings settings)
  {
    settings.DocumentTypes = new ChangeTrackingListAdapter<GlobalId<int>>((IEnumerable<GlobalId<int>>) settingsBuilder.DecodeObjectTypes("DocumentTypes", "DocumentType", (XmlNode) null));
  }
}
