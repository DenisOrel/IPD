// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.ComInterop.ComApplicationApiSession
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.ComInterop;

/// <summary>
/// Реализует сессию доступа к API приложений на базе технологии COM. После открытия сессии оно будет доступно через свойство Application у объекта сессии.
/// </summary>
public class ComApplicationApiSession : ApplicationApiSession<object>
{
  /// <summary>Создает объект сессии.</summary>
  /// <param name="integrator">Интегратор с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">integrator</exception>
  public ComApplicationApiSession(IIntegrator integrator)
    : base(integrator)
  {
  }

  /// <summary>Создает объект сессии.</summary>
  /// <param name="apiService">Сервис внешнего API интегратора с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">apiService</exception>
  public ComApplicationApiSession(IApplicationApiService apiService)
    : base(apiService)
  {
  }
}
