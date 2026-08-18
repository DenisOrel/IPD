// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADApiOperations
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Interfaces.Client;
using Intermech.Runtime.ComInterop.Proxies;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

public sealed class CADApiOperations
{
  private readonly IIntegrator integrator;
  private readonly IApplicationApiService apiService;

  internal CADApiOperations(IIntegrator integrator, IApplicationApiService apiService)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (apiService == null)
      throw new ArgumentNullException(nameof (apiService));
    this.integrator = integrator;
    this.apiService = apiService;
  }

  internal void ReconfigureApplication(CADSystemProxy cadObject)
  {
    if (cadObject == null)
      throw new ArgumentNullException(nameof (cadObject));
    try
    {
      string areaPath = ClientContext.FileVault.WorkArea.AreaPath;
      cadObject.SetWorkingFolder(areaPath);
    }
    catch (ApplicationProxyException ex)
    {
      throw new BadApplicationSettingsException(this.integrator.DisplayName, this.apiService.ApplicationName, ex.Message);
    }
  }
}
