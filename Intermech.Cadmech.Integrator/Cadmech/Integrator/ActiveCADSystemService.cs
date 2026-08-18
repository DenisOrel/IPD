// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.ActiveCADSystemService
// Assembly: Intermech.Cadmech.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FE1650F6-4A62-4271-BCAB-1BBCBCB3092C
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Cadmech.Integrator;

internal sealed class ActiveCADSystemService
{
  private IIntegratorRegistry integratorRegistry;
  private IntegratorObject acadRef;
  private IntegratorObject bricscadRef;
  private IntegratorObject nanocadRef;

  public ActiveCADSystemService(IIntegratorRegistry integratorRegistry)
  {
    this.integratorRegistry = integratorRegistry != null ? integratorRegistry : throw new ArgumentNullException(nameof (integratorRegistry));
    this.acadRef = new IntegratorObject(AcadConsts.IntegratorId, AcadConsts.IntegratorName);
    this.bricscadRef = new IntegratorObject(BricscadConsts.IntegratorId, BricscadConsts.IntegratorName);
    this.nanocadRef = new IntegratorObject(NanocadConsts.IntegratorId, NanocadConsts.IntegratorName);
  }

  public IntegratorObject GetActiveCADSystem()
  {
    bool flag1 = this.IsCADSystemRunning(this.acadRef);
    bool flag2 = this.IsCADSystemRunning(this.bricscadRef);
    bool flag3 = this.IsCADSystemRunning(this.nanocadRef);
    if (flag1 && !flag2 && !flag3)
      return this.acadRef;
    if (flag2 && !flag1 && !flag3)
      return this.bricscadRef;
    if (flag3 && !flag1 && !flag2)
      return this.nanocadRef;
    if (flag1 | flag2 | flag3)
    {
      List<string> values = new List<string>();
      if (flag1)
        values.Add(AcadConsts.ApplicationName);
      if (flag2)
        values.Add(BricscadConsts.ApplicationName);
      if (flag3)
        values.Add(NanocadConsts.ApplicationName);
      throw new FaultException($"Не удалось определить используемую CAD-систему, так как {string.Join(", ", (IEnumerable<string>) values)} запущены одновременно. Закройте все CAD-системы, кроме одной, и повторите операцию.");
    }
    bool flag4 = this.IsCADSystemInstalled(this.acadRef);
    bool flag5 = this.IsCADSystemInstalled(this.bricscadRef);
    bool flag6 = this.IsCADSystemInstalled(this.nanocadRef);
    if (flag4 && !flag5 && !flag6)
      return this.acadRef;
    if (flag5 && !flag4 && !flag6)
      return this.bricscadRef;
    if (flag6 && !flag4 && !flag5)
      return this.nanocadRef;
    if (flag4 | flag5 | flag6)
    {
      List<string> values = new List<string>();
      if (flag4)
        values.Add(AcadConsts.ApplicationName);
      if (flag5)
        values.Add(BricscadConsts.ApplicationName);
      if (flag6)
        values.Add(NanocadConsts.ApplicationName);
      throw new FaultException($"Не удалось определить используемую CAD-систему. Запустите {string.Join(", ", (IEnumerable<string>) values)} и повторите попытку.");
    }
    return this.acadRef;
  }

  private bool IsCADSystemRunning(IntegratorObject integratorRef)
  {
    IIntegrator integrator = this.integratorRegistry.GetIntegrator(integratorRef, false);
    return integrator != null && ServiceUtils.GetService<IApplicationApiService>((object) integrator, true).IsApplicationRunning;
  }

  private bool IsCADSystemInstalled(IntegratorObject integratorRef)
  {
    IIntegrator integrator = this.integratorRegistry.GetIntegrator(integratorRef, false);
    return integrator != null && ServiceUtils.GetService<IApplicationApiService>((object) integrator, true).IsApplicationInstalled;
  }
}
