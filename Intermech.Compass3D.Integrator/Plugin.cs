// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Plugin
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.ApplicationModel;
using Intermech.Interfaces.Plugins;
using Intermech.Tools.Integrators.CADInterface;
using System;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal class Plugin : ClientModularPackage
{
  internal static readonly Guid IntegratorId = new Guid("70B6DFB2-5CEF-4247-9B11-D83653144E52");
  internal static readonly string StandardLibrary = "Compass 3D Library";
  internal static readonly string IntegratorName = Localization.rm.GetString("Compass3D.Integrator_2");

  public Plugin()
    : base(Plugin.IntegratorName)
  {
  }

  protected override void CreateSubModules(InitializerModuleGroup subModules)
  {
    base.CreateSubModules(subModules);
    CADIntegratorModule<K3DIntegrator> module = new CADIntegratorModule<K3DIntegrator>();
    module.EnableLaunchHandler(CompassConsts.IntegratorAppName);
    subModules.Add((InitializerModule) module);
    subModules.Add((InitializerModule) new Drawing2DArticleAttributesGuardModule());
  }
}
