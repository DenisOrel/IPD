// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADApplicationLauncherService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.ControlFlow;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует сервис интегратора, позволяющий настроить приложение для работы в паре c IPS.
/// </summary>
/// <summary>Создает объект.</summary>
/// <param name="owner">Владелец компонента</param>
/// <exception cref="T:System.ArgumentNullException">Ссылка на владельца компонента не может быть null</exception>
internal sealed class CADApplicationLauncherService(IIntegrator owner) : 
  ApplicationLauncherService(owner),
  IApplicationLauncherService
{
  /// <summary>
  /// <para>
  /// Запускает приложение, если она не запущено, и настраивает его на работу в паре с IPS.
  /// Если приложению специальная настройка не требуется, то метод просто запускает приложение.</para>
  /// <para>
  /// Если же метод не может настроить приложение, то он должен бросить исключение и сообщить пользователю,
  /// какие действия необходимо выполнить, чтобы приложение корректно работало в паре с IPS.</para>
  /// </summary>
  /// <exception cref="T:Intermech.Tools.Integrators.BadAppSettingsException">Не удалось настроить приложение на работу в паре с IPS</exception>
  protected override void DoLaunchApplication()
  {
    using (new DynamicScope())
    {
      IntegratorVars.NakedApiSessions.Declare(true);
      using (CADApiSession cadApiSession = new CADApiSession(this.Integrator))
      {
        CADSystemProxy application = cadApiSession.Application;
        cadApiSession.ApiOperations.ReconfigureApplication(application);
        application.SwitchToApp();
      }
    }
  }
}
