// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.K3DIntegratorSettingsFactory
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Settings;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class K3DIntegratorSettingsFactory(CADIntegrator integrator) : CADSettingsFactory(integrator)
{
  protected override CADSettings DoCreateSettingsObject()
  {
    return (CADSettings) new K3DIntegratorSettings();
  }

  protected override CADSettingsViewModel DoCreateSettingsViewModel()
  {
    return (CADSettingsViewModel) new K3DSettingsViewModel(this);
  }

  protected override CADSettingsCodec DoCreateCodec(
    string integratorName,
    ISettingsObjectFactory factory)
  {
    return (CADSettingsCodec) new K3DIntegratorSettingsCodec(integratorName, factory);
  }

  protected override void DoCreateValidatorChecks(
    CADIntegrator integrator,
    List<ISettingsValidatorCheck> checkList)
  {
    base.DoCreateValidatorChecks(integrator, checkList);
    checkList.Add((ISettingsValidatorCheck) new Drawing2DDocumentGroupsCheck((IEnumerable<string>) Drawing2DGroups.All));
    checkList.Add((ISettingsValidatorCheck) new Drawing2DDocumentRootsCheck());
    checkList.Add((ISettingsValidatorCheck) new Drawing2DArticleRootsCheck());
  }

  protected override CADSettingsService DoCreateSettingsService(
    CADIntegrator integrator,
    bool sharedModelAttributes)
  {
    return (CADSettingsService) new K3DSettingsService((IIntegrator) integrator, this, sharedModelAttributes);
  }
}
