
// Type: Intermech.Tools.Integrators.EmptySettingsCodec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators;

internal sealed class EmptySettingsCodec(string integratorName) : IntegratorSettingsCodec(integratorName)
{
  public override ISettingsObject CreateEmptySettings()
  {
    return (ISettingsObject) new EmptySettingsCodec.EmptySettings();
  }

  protected override int GetEncoderFormatVersion() => 1;

  protected override void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder)
  {
  }

  protected override void EncodeServerData(
    ISettingsObject settingsObject,
    IntegratorServerDataBuilder serverData)
  {
    base.EncodeServerData(settingsObject, serverData);
    EmptySettingsCodec.EmptySettings emptySettings = (EmptySettingsCodec.EmptySettings) settingsObject;
    serverData.SpecialFileManagement = emptySettings.SpecialFileManagement;
    foreach (Guid documentType in (IEnumerable<Guid>) emptySettings.DocumentTypes)
      serverData.AddObjectType(documentType);
  }

  internal sealed class EmptySettings : ISettingsObject
  {
    private bool skipFileManagement;
    private readonly LinkedList<Guid> documentTypes = new LinkedList<Guid>();

    public bool SpecialFileManagement
    {
      get => this.skipFileManagement;
      set => this.skipFileManagement = value;
    }

    public ICollection<Guid> DocumentTypes => (ICollection<Guid>) this.documentTypes;
  }
}
