
// Type: Intermech.Tools.LaunchActions.LaunchActionSettingsCodec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;


namespace Intermech.Tools.LaunchActions;

public abstract class LaunchActionSettingsCodec : SettingsCodec
{
  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    LaunchActionServerDataBuilder serverData = new LaunchActionServerDataBuilder();
    this.EncodeServerData(settingsObject, serverData);
    serverData.UpdateXml(settingsBuilder);
  }

  protected virtual void EncodeServerData(
    ISettingsObject settingsObject,
    LaunchActionServerDataBuilder serverData)
  {
    serverData.ActionDisplayName = this.GetActionDisplayName(settingsObject);
  }

  protected abstract string GetActionDisplayName(ISettingsObject settingsObject);
}
