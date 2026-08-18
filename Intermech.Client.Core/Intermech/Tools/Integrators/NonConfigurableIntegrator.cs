
// Type: Intermech.Tools.Integrators.NonConfigurableIntegrator
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;


namespace Intermech.Tools.Integrators;

public abstract class NonConfigurableIntegrator : IntegratorBase
{
  public override string GetServerObjectTemplate()
  {
    EmptySettingsCodec.EmptySettings emptySettings = new EmptySettingsCodec.EmptySettings();
    emptySettings.SpecialFileManagement = this.HasSpecialFileManagement();
    emptySettings.DocumentTypes.AddRange<Guid>((IEnumerable<Guid>) this.GetDocumentTypes());
    return new EmptySettingsCodec(this.DisplayName).Encode((ISettingsObject) emptySettings).OuterXml;
  }

  protected virtual bool HasSpecialFileManagement() => false;

  protected virtual ICollection<Guid> GetDocumentTypes()
  {
    return (ICollection<Guid>) new LinkedList<Guid>();
  }

  public override DataEditorControl CreateSettingsEditor()
  {
    return (DataEditorControl) new DumbDataEditor();
  }
}
