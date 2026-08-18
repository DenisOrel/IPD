
// Type: Intermech.Tools.Integrators.ApplicationLauncherService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Intermech.Tools.Integrators;

/// <summary>
/// Реализует базовый класс для сервиса интегратора, позволяющего настроить приложение для работы в паре c IPS.
/// Все методы и свойства этого класса являются thread-safe.
/// </summary>
public abstract class ApplicationLauncherService : IntegratorService, IApplicationLauncherService
{
  private IApplicationApiService apiService;
  private IIntegratorSettingsService settingsService;
  private IntegratorSettingsCache<bool> integratorEnabled;

  /// <summary>Создает объект.</summary>
  /// <param name="owner">Владелец сервиса</param>
  public ApplicationLauncherService(IIntegrator owner)
    : base(owner)
  {
  }

  /// <summary>
  /// Возвращает или задает сервис для работы с API приложения. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IApplicationApiService ApiService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.apiService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.apiService = value;
      }
    }
  }

  /// <summary>
  /// Возвращает или задает сервис для работы с настройками интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IIntegratorSettingsService SettingsService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.settingsService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.settingsService = value;
      }
    }
  }

  /// <summary>
  /// Проверяет конфигурацию сервиса и выполняет его окончательную инициализацию.
  /// После успешного выполнения этого метода сервис интегратора можно использовать.
  /// </summary>
  /// <exception cref="T:InvalidOperationException">Конфигурация сервиса некорректна</exception>
  protected override void DoInitialize()
  {
    base.DoInitialize();
    if (this.ApiService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "ApiService");
    this.integratorEnabled = this.SettingsService != null ? new IntegratorSettingsCache<bool>(this.SettingsService, new Func<bool>(this.TestIntegrationIsEnabled)) : throw PropertyExceptions.PropertyNotSetException((object) this, "SettingsService");
  }

  /// <summary>
  /// Возвращает список команд для запуска приложения и настройки его для работы в паре с IPS.
  /// Как правило, список содержит только одну команду, чье название совпадает с названием приложения.
  /// </summary>
  /// <returns>Список команд запуска приложения</returns>
  public List<MenuCommand> GetCommands()
  {
    this.RequireReadyState();
    List<MenuCommand> commands = new List<MenuCommand>();
    if (this.integratorEnabled.Value)
    {
      MenuCommand menuCommand = new MenuCommand(this.apiService.ApplicationName, LocalizationHolder.rm.GetString("SR_1616"), this.Integrator.GetApplicationImage(AppImageSize.Image16x16), new Action(this.LaunchApplication));
      commands.Add(menuCommand);
    }
    return commands;
  }

  private bool TestIntegrationIsEnabled()
  {
    try
    {
      return IntegratorServices.Exists(this.Integrator.Id);
    }
    catch (Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(LocalizationHolder.rm.GetString("SR_1617"));
      stringBuilder.Append(' ');
      stringBuilder.Append(ex.Message);
      this.OutputService.WriteLine(stringBuilder.ToString());
      return false;
    }
  }

  private void LaunchApplication()
  {
    lock (this.Integrator.SyncRoot)
      this.DoLaunchApplication();
  }

  /// <summary>
  /// <para>
  /// Запускает приложение, если она не запущено, и настраивает его на работу в паре с IPS.
  /// Если приложению специальная настройка не требуется, то метод просто запускает приложение.</para>
  /// <para>
  /// Если же метод не может настроить приложение, то он должен бросить исключение и сообщить пользователю,
  /// какие действия необходимо выполнить, чтобы приложение корректно работало в паре с IPS.</para>
  /// </summary>
  /// <exception cref="T:Intermech.Tools.Integrators.BadAppSettingsException">Не удалось настроить приложение на работу в паре с IPS</exception>
  protected abstract void DoLaunchApplication();
}
