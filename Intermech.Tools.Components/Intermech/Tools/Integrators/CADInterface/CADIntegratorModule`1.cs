// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADIntegratorModule`1
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует модуль, обеспечивающий создание, инициализацию и регистрацию интегратора в системе.
/// </summary>
/// <typeparam name="TIntegrator">Тип интегратора</typeparam>
public class CADIntegratorModule<TIntegrator> : IntegratorModule<TIntegrator> where TIntegrator : CADIntegrator, new()
{
  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    this.ApplyMultiCADSettingsPatch();
  }

  private void ApplyMultiCADSettingsPatch()
  {
    if (!ServiceUtils.IsServiceAvailable((object) this.Integrator, typeof (IMultiCADSupport)))
      return;
    new MultiCADIntergratorSettingsPatch((IIntegrator) this.Integrator).Perform();
  }
}
