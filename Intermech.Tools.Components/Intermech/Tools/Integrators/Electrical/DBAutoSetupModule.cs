// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.DBAutoSetupModule
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.ApplicationModel;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Модуль обеспечивает автоматическое создание в базе данных IPS объекта электрического интегратора, а также
/// связанных с интегратором настроек.
/// </summary>
public class DBAutoSetupModule : InitializerModule
{
  private readonly PluginContext pluginCtx;

  /// <summary>Создает объект.</summary>
  /// <param name="pluginCtx">Сервисный контекст</param>
  public DBAutoSetupModule(PluginContext pluginCtx) => this.pluginCtx = pluginCtx;

  /// <summary>
  /// Выполняет инициализацию объектов и сервисов, предоставляемых модулем.
  /// </summary>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.pluginCtx.IntegratorInstance == null)
      return;
    this.CreateIntegratorObjectIfNotExists(this.pluginCtx.IntegratorInstance);
    this.CreateStandaloneViewSettings(this.pluginCtx.IntegratorInstance);
  }

  protected virtual void CreateStandaloneViewSettings(IIntegrator integrator)
  {
  }

  private void CreateIntegratorObjectIfNotExists(IIntegrator integrator)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      if (service.IsIntegratorExists(this.pluginCtx.IntegratorInstance.Id) || (ServiceUtils.GetService<IToolSecurity>((object) sessionKeeper.Session, true).GetUserRights() & ToolSecurityRights.EditPublicSettings) == ToolSecurityRights.None)
        return;
      service.CreateIntegrator(this.pluginCtx.IntegratorInstance.Id, integrator.GetServerObjectTemplate());
    }
  }
}
