// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DSettingsService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DSettingsService(
  IIntegrator owner,
  K3DIntegratorSettingsFactory settingsFactory,
  bool sharedModelAttributes) : CADSettingsService(owner, (CADSettingsFactory) settingsFactory, sharedModelAttributes)
{
  public K3DIntegratorSettings GetSettings() => (K3DIntegratorSettings) this.GetCADSettings();

  public override List<LocalId<int>> GetNewFileDocumentTypes()
  {
    List<LocalId<int>> fileDocumentTypes = base.GetNewFileDocumentTypes();
    K3DIntegratorSettings settings = this.GetSettings();
    if (settings.EnableDrawings2DSupport)
    {
      fileDocumentTypes.AddRange((IEnumerable<LocalId<int>>) settings.PartDrawings2D.DocumentTypes);
      fileDocumentTypes.AddRange((IEnumerable<LocalId<int>>) settings.AssemblyDrawings2D.DocumentTypes);
    }
    return fileDocumentTypes;
  }

  public override CADDocumentType? MapDocumentTypeToCADDocumentType(int documentType)
  {
    K3DIntegratorSettings settings = this.GetSettings();
    if (settings.EnableDrawings2DSupport)
    {
      if (settings.PartDrawings2D.ContainsType(documentType))
        return new CADDocumentType?(CADDocumentType.Drawing);
      if (settings.AssemblyDrawings2D.ContainsType(documentType))
        return new CADDocumentType?(CADDocumentType.Drawing);
    }
    return base.MapDocumentTypeToCADDocumentType(documentType);
  }
}
