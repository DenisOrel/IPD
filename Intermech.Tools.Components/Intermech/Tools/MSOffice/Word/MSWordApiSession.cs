// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordApiSession
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

/// <summary>
/// Реализует сессию доступа к API MS Word. После открытия сессии оно будет доступно через
/// свойство <see cref="P:Intermech.Tools.Integrators.ApplicationApiSession`1.Application" /> у объекта сессии.
/// Реализация не является thread safe.
/// </summary>
public class MSWordApiSession : ComApplicationApiSession
{
  /// <summary>Создает объект сессии.</summary>
  /// <param name="integrator">Интегратор с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">integrator</exception>
  public MSWordApiSession(IIntegrator integrator)
    : base(integrator)
  {
  }

  /// <summary>Создает объект сессии.</summary>
  /// <param name="apiService">Сервис внешнего API интегратора с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">apiService</exception>
  public MSWordApiSession(IApplicationApiService apiService)
    : base(apiService)
  {
  }

  /// <summary>Создает сессию с параметрами по умолчанию.</summary>
  /// <returns>Сессия доступа к API MS Word</returns>
  public static MSWordApiSession CreateDefault()
  {
    return new MSWordApiSession(IntegratorServices.GetService<IApplicationApiService>(MSWordConsts.IntegratorRef, true));
  }
}
