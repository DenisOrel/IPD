// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelApiSession
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.ComInterop;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

/// <summary>
/// Реализует сессию доступа к API MS Excel. После открытия сессии оно будет доступно через
/// свойство <see cref="P:Intermech.Tools.Integrators.ApplicationApiSession`1.Application" /> у объекта сессии.
/// Реализация не является thread safe.
/// </summary>
public class ExcelApiSession : ComApplicationApiSession
{
  /// <summary>Создает объект сессии.</summary>
  /// <param name="integrator">Интегратор с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">integrator</exception>
  public ExcelApiSession(IIntegrator integrator)
    : base(integrator)
  {
  }

  /// <summary>Создает объект сессии.</summary>
  /// <param name="apiService">Сервис внешнего API интегратора с приложением</param>
  /// <exception cref="T:System.ArgumentNullException">apiService</exception>
  public ExcelApiSession(IApplicationApiService apiService)
    : base(apiService)
  {
  }

  /// <summary>Создает сессию с параметрами по умолчанию.</summary>
  /// <returns>Сессия доступа к API MS Excel</returns>
  public static ExcelApiSession CreateDefault()
  {
    return new ExcelApiSession(IntegratorServices.GetService<IApplicationApiService>(ExcelConsts.IntegratorRef, true));
  }
}
