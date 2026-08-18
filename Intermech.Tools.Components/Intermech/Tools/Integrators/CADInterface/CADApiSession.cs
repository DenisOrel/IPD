// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADApiSession
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сессию доступа к API CAD-системы на основе CAD-интерфейса. После открытия сессии оно будет доступно через свойство Application у объекта сессии.
/// </summary>
public sealed class CADApiSession : ApplicationApiSession<CADSystemProxy>
{
  private CADApiOperations apiOperations;

  /// <summary>Создает объект сессии.</summary>
  /// <param name="integrator">Интегратор с CAD-системой</param>
  /// <exception cref="T:System.ArgumentNullException">integrator</exception>
  public CADApiSession(IIntegrator integrator)
    : base(integrator)
  {
  }

  /// <summary>Создает объект сессии.</summary>
  /// <param name="apiService">Сервис внешнего API интегратора с CAD-системой</param>
  /// <exception cref="T:System.ArgumentNullException">apiService</exception>
  public CADApiSession(IApplicationApiService apiService)
    : base(apiService)
  {
  }

  public CADApiOperations ApiOperations
  {
    [DebuggerStepThrough] get
    {
      this.CheckNotDisposed();
      if (this.apiOperations == null)
        this.apiOperations = this.CreateApiOperations();
      return this.apiOperations;
    }
  }

  private CADApiOperations CreateApiOperations()
  {
    return new CADApiOperations(((IntegratorService) this.ApplicationApiService).Integrator, this.ApplicationApiService);
  }
}
