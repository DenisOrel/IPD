// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.MultiCADIntergratorSettingsPatch
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

internal sealed class MultiCADIntergratorSettingsPatch(IIntegrator integrator) : 
  PatchIntegratorSettingsAction<CADSettings>(integrator, true)
{
  protected override CADSettings TryGetSettingsToPatch()
  {
    if (ServiceUtils.GetService<IMultiCADSupport>((object) this.Integrator, false) == null)
      return (CADSettings) null;
    CADSettings settingsToPatch = base.TryGetSettingsToPatch();
    if (settingsToPatch == null)
      return (CADSettings) null;
    if (settingsToPatch.JTDerivativesEnabled)
    {
      IntegratorObject integratorObject = IntegratorServices.Find(settingsToPatch.JTDerivedDocumentType.Id);
      if (integratorObject == null || integratorObject.Id != this.Integrator.Id)
        return settingsToPatch;
    }
    return (CADSettings) null;
  }

  protected override bool PatchSettings(CADSettings settingsToPatch)
  {
    base.PatchSettings(settingsToPatch);
    return true;
  }
}
