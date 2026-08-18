// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.PluginContext
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>
/// Реализует сервисный контекст для электрических интеграторов. Он предоставляет доступ к экземпляру интегратора и
/// сервисам, реализуемым в плагине. Инициализация сервисного контекста выполняется в процессе загрузки плагина.
/// </summary>
public sealed class PluginContext
{
  private IIntegrator integratorInstance;

  /// <summary>Предоставляет доступ к экземпляру интегратора</summary>
  public IIntegrator IntegratorInstance
  {
    get => this.integratorInstance;
    set => this.integratorInstance = value;
  }
}
