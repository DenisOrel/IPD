
// Type: Intermech.Tools.Integrators.IntegratorSettingsCodec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;


namespace Intermech.Tools.Integrators;

public abstract class IntegratorSettingsCodec : SettingsCodec
{
  private readonly string integratorName;

  public IntegratorSettingsCodec(string integratorName)
  {
    this.integratorName = !string.IsNullOrEmpty(integratorName) ? integratorName : throw new ArgumentException();
  }

  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
    IntegratorServerDataBuilder serverData = new IntegratorServerDataBuilder();
    this.EncodeServerData(settingsObject, serverData);
    serverData.UpdateXml(settingsBuilder);
  }

  protected virtual void EncodeServerData(
    ISettingsObject settingsObject,
    IntegratorServerDataBuilder serverData)
  {
    serverData.IntegratorName = this.integratorName;
  }
}
