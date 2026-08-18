// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.LaunchHandlers.OpenDwgWithProfileSettingCodec
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Tools.LaunchActions;
using Intermech.Tools.Settings;
using System.Xml;

#nullable disable
namespace Intermech.Cadmech.Integrator.LaunchHandlers;

internal sealed class OpenDwgWithProfileSettingCodec : LaunchActionSettingsCodec
{
  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new OpenDwgWithProfileSettings();
  }

  protected override int GetEncoderFormatVersion() => 1;

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    OpenDwgWithProfileSettings withProfileSettings = (OpenDwgWithProfileSettings) settingsObject;
    settingsBuilder.AppendElement((XmlNode) settingsBuilder.EncodeText("ProfileName", withProfileSettings.ProfileName));
  }

  protected override void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    OpenDwgWithProfileSettings withProfileSettings = (OpenDwgWithProfileSettings) settingsObject;
    if (formatVersion == 1)
      withProfileSettings.ProfileName = settingsBuilder.DecodeText("ProfileName", string.Empty);
    else
      base.DecodeSettings(formatVersion, settingsBuilder, settingsObject);
  }

  protected override string GetActionDisplayName(ISettingsObject settingsObject)
  {
    return $"Открыть в AutoCAD с профилем '{((OpenDwgWithProfileSettings) settingsObject).ProfileName}'";
  }
}
