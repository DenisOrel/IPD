// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.AcadApiOperations
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.AutoCAD.Proxies;
using Intermech.IO;
using Intermech.Runtime.ComInterop.Proxies;
using Intermech.Tools.Integrators;
using System;
using System.IO;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class AcadApiOperations
{
  private readonly IIntegrator integrator;
  private readonly IApplicationApiService apiService;

  public AcadApiOperations(IIntegrator integrator, IApplicationApiService apiService)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (apiService == null)
      throw new ArgumentNullException(nameof (apiService));
    this.integrator = integrator;
    this.apiService = apiService;
  }

  public void ReconfigureApplication(ICadProxy cadObject, AcadSetupSettings cadConfiguration)
  {
    if (cadObject == null)
      throw new ArgumentNullException(nameof (cadObject));
    if (cadConfiguration == null)
      throw new ArgumentNullException(nameof (cadConfiguration));
    try
    {
      if (cadConfiguration.UseSpecificProfile && cadObject.ActiveProfile != cadConfiguration.ProfileName)
        cadObject.ActiveProfile = cadConfiguration.ProfileName;
      if (string.IsNullOrEmpty(cadConfiguration.WorkDirectory) || !Directory.Exists(cadConfiguration.WorkDirectory) || PathUtils.IsSamePath(cadObject.WorkspacePath, cadConfiguration.WorkDirectory))
        return;
      cadObject.WorkspacePath = cadConfiguration.WorkDirectory;
    }
    catch (ApplicationProxyException ex)
    {
      throw new BadApplicationSettingsException(this.integrator.DisplayName, this.apiService.ApplicationName, ex.Message);
    }
  }
}
