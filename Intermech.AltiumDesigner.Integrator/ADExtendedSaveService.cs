// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADExtendedSaveService
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADExtendedSaveService(IIntegrator owner) : 
  ECADExtendedSaveService<Intermech.AltiumDesigner.Integrator.SettingsService>(owner)
{
  protected override AppMechanicalDriver CreateMechanicalDriver()
  {
    return (AppMechanicalDriver) new ADMechanicalDriver(this.Integrator);
  }

  protected override IList<LocalId<int>> supportedDocumentTypes
  {
    get
    {
      List<LocalId<int>> supportedDocumentTypes = new List<LocalId<int>>();
      ADIntegratorSettings settings = this.SettingsService.GetSettings();
      if (settings.ProjectType != null)
        supportedDocumentTypes.Add((LocalId<int>) settings.ProjectType);
      if (settings.SchemaDocumentTypes != null)
      {
        foreach (GlobalId<int> schemaDocumentType in settings.SchemaDocumentTypes)
          supportedDocumentTypes.Add((LocalId<int>) schemaDocumentType);
      }
      return (IList<LocalId<int>>) supportedDocumentTypes;
    }
  }
}
