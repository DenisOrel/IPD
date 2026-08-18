// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DExtendedSaveService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Runtime;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DExtendedSaveService(IIntegrator owner, CADCaptureChangesFactory factory) : 
  CADExtendedSaveService(owner, factory)
{
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.SettingsService != null && !(this.SettingsService is K3DSettingsService))
      throw PropertyExceptions.PropertyBadValueException((object) this, "SettingsService", $"Значение свойства 'SettingsService' должны иметь тип '{typeof (K3DSettingsService)}'.");
  }

  private K3DSettingsService K3DSettingsService
  {
    [DebuggerStepThrough] get => (K3DSettingsService) this.SettingsService;
  }

  protected override IList<LocalId<int>> CollectSupportedDocumentTypes()
  {
    IList<LocalId<int>> collection = base.CollectSupportedDocumentTypes();
    K3DIntegratorSettings settings = this.K3DSettingsService.GetSettings();
    if (settings.EnableDrawings2DSupport)
    {
      collection.AddRange<LocalId<int>>((IEnumerable<LocalId<int>>) settings.AssemblyDrawings2D.DocumentTypes);
      collection.AddRange<LocalId<int>>((IEnumerable<LocalId<int>>) settings.PartDrawings2D.DocumentTypes);
    }
    return collection;
  }
}
