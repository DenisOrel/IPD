
// Type: Intermech.Tools.Integrators.ApplicationApiService`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Tools.Integrators;

public abstract class ApplicationApiService<TApplication> : 
  ExternalApiService,
  IApplicationApiService,
  IExternalApiService,
  IIntegratorService
  where TApplication : class
{
  private readonly string applicationName;
  private TApplication applicationObject;
  private ApplicationApiResourceManager apiResourceManager;

  public ApplicationApiService(IIntegrator owner, string applicationName)
    : base(owner)
  {
    this.applicationName = !string.IsNullOrEmpty(applicationName) ? applicationName : throw new ArgumentException();
  }

  /// <summary>
  /// Возвращает название приложения, с которым осуществляется интеграция.
  /// </summary>
  public string ApplicationName => this.applicationName;

  /// <summary>
  /// Возвращает true, если версия приложения, указанная в настройках интегратора, установлена на компьютере.
  /// Если интеграция не настроена, либо настройки интегратора содержат ошибки, то метод возвращает false.
  /// Ошибки, связанные с определением наличия приложения на компьютере, подавляются и отображаются на закладке
  /// "Вывод".
  /// </summary>
  public bool IsApplicationInstalled
  {
    get
    {
      this.RequireReadyState();
      if (!IntegratorServices.Exists(this.Integrator.Id))
        return false;
      lock (this.Integrator.SyncRoot)
      {
        try
        {
          return this.IsInstalled();
        }
        catch (IntegratorNotInstalledException ex)
        {
          return false;
        }
        catch (BadIntegratorSettingsException ex)
        {
          return false;
        }
        catch (Exception ex)
        {
          this.ProcessSilentException(ex);
          return false;
        }
      }
    }
  }

  /// <summary>
  /// Возвращает true, если версия приложения, указанная в настройках интегратора, выполняется в данный момент.
  /// Если интеграция не настроена, либо настройки интегратора содержат ошибки, то метод возвращает false.
  /// Ошибки, связанные с определением наличия/работоспособности приложения, подавляются и отображаются на закладке
  /// "Вывод".
  /// </summary>
  public bool IsApplicationRunning
  {
    get
    {
      this.RequireReadyState();
      if (!this.IsApplicationInstalled)
        return false;
      lock (this.Integrator.SyncRoot)
      {
        try
        {
          return this.IsRunning();
        }
        catch (IntegratorNotInstalledException ex)
        {
          return false;
        }
        catch (BadIntegratorSettingsException ex)
        {
          return false;
        }
        catch (Exception ex)
        {
          this.ProcessSilentException(ex);
          return false;
        }
      }
    }
  }

  /// <summary>
  /// Выполняет проверку, что приложение установлено на компьютере. Если это не так, то метод сбрасывает исключение.
  /// </summary>
  /// <exception cref="T:Intermech.Tools.Integrators.ApplicationNotInstalledException">Приложение не установлено на компьютере</exception>
  protected void CheckApplicationInstalled()
  {
    if (!this.IsApplicationInstalled)
      throw new ApplicationNotInstalledException(this.Integrator.DisplayName, $"Приложение {this.ApplicationName} не установлено на компьютере.");
  }

  /// <summary>
  /// Закрывает подключение к приложению и освобождает API-объект приложения. Этот метод можно использовать только тогда, когда нет открытых сессий доступа к API интегрируемого приложения.
  /// </summary>
  /// <exception cref="T:System.InvalidOperationException">Невозможно освободить API-объект приложения, пока он используется</exception>
  public void ReleaseApplicationObject()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      if (this.IsApiSessionOpen)
        throw new InvalidOperationException("Can't release the application object while it's in use.");
      if ((object) this.applicationObject == null)
        return;
      this.DoReleaseApplicationObject(this.applicationObject);
      this.applicationObject = default (TApplication);
    }
  }

  /// <summary>
  /// Возвращает API-объект приложения. Этот метод требует предварительного открытия сессии доступа к API интегрируемого приложения.
  /// </summary>
  /// <returns>API-объект приложения</returns>
  /// <exception cref="T:System.InvalidOperationException">Требуется предварительное открытие сессии доступа к API интегрируемого приложению</exception>
  public TApplication GetApplicationObject()
  {
    this.RequireReadyState();
    lock (this.Integrator.SyncRoot)
    {
      this.CheckApiSessionOpen();
      return this.applicationObject;
    }
  }

  /// <summary>
  /// Возвращает API-объект приложения. Этот метод требует предварительного открытия сессии доступа к API интегрируемого приложения.
  /// </summary>
  /// <returns>API-объект приложения</returns>
  /// <exception cref="T:System.InvalidOperationException">Требуется предварительное открытие сессии доступа к API интегрируемого приложению</exception>
  object IApplicationApiService.GetApplicationObject() => (object) this.GetApplicationObject();

  /// <summary>
  /// Открывает сессию доступа к API интегрируемого приложения и конфигурирует приложение для работы в паре с IPS.
  /// </summary>
  /// <param name="topLevelSession">true - если это сессия верхнего уровня, false - если это вложенная сессия</param>
  /// <exception cref="T:Intermech.Tools.Integrators.IntegratorNotInstalledException">Интеграция с приложением не настроена</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadIntegratorSettingsException">Настройки интегратора содержат ошибки, препятствующие его использованию</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.AppNotInstalledException">Не удалось найти приложение на компьютере</exception>
  /// <exception cref="T:Intermech.Tools.Integrators.BadAppSettingsException">Не удалось настроить приложение на работу в паре с IPS</exception>
  protected override void DoOpenApiSession(bool topLevelSession)
  {
    this.CheckApplicationInstalled();
    base.DoOpenApiSession(topLevelSession);
    if (!topLevelSession)
      return;
    if ((object) this.applicationObject != null)
      this.ValidateApplicationObject();
    bool newApplicationObject = (object) this.applicationObject == null;
    if (newApplicationObject)
    {
      this.applicationObject = this.DoCreateApplicationObject();
      if ((object) this.applicationObject == null)
        throw new InvalidOperationException("The application object can't be null.");
    }
    this.DoAttachApplicationToApiSession(this.applicationObject, newApplicationObject);
  }

  /// <summary>
  /// Закрывает сессию доступа к API интегрируемого приложения.
  /// </summary>
  /// <param name="topLevelSession">true - если это сессия верхнего уровня, false - если это вложенная сессия</param>
  protected override void DoCloseApiSession(bool topLevelSession)
  {
    if (topLevelSession)
      this.DoDetachApplicationFromApiSession(this.applicationObject);
    base.DoCloseApiSession(topLevelSession);
  }

  private void ValidateApplicationObject()
  {
    try
    {
      this.DoTestApplicationObject(this.applicationObject);
    }
    catch
    {
      this.OutputService.WriteLine($"Приложение {this.ApplicationName} не отвечает через API-интерфейс. Выполняем переподключение...");
      this.ReleaseApplicationObject();
    }
  }

  protected virtual void ProcessSilentException(Exception x)
  {
    this.OutputService.WriteLine(x.Message);
  }

  protected abstract bool IsInstalled();

  protected abstract bool IsRunning();

  /// <summary>
  /// Выполняет подключение к интегрируемому приложению. Метод возвращает API-объект приложения, через который осуществляется всё взаимодействие с приложением.
  /// </summary>
  /// <returns>API-объект приложения</returns>
  protected abstract TApplication DoCreateApplicationObject();

  /// <summary>
  /// Проверяет работоспособность кэшированного API-объекта приложения. Он может стать нерабочим, если приложение было закрытом пользователем или упало.
  /// В этом случае метод должен сбросить исключение.
  /// </summary>
  /// <param name="applicationObject">API-объект приложения</param>
  /// <exception cref="T:System.Exception">API-объект приложения не работает. Требуется переподключение к приложению</exception>
  protected abstract void DoTestApplicationObject(TApplication applicationObject);

  /// <summary>Освобождает кэшированный API-объект приложения.</summary>
  /// <param name="applicationObject">API-объект приложения</param>
  protected virtual void DoReleaseApplicationObject(TApplication applicationObject)
  {
  }

  /// <summary>
  /// Присоединяет API-объект приложения к открытой сессии доступа к API интегрируемого приложения. Этот метод вызывается в процессе открытия сессии доступа верхнего уровня,
  /// для вложенных сессий он не вызывается.
  /// </summary>
  /// <param name="applicationObject">API-объект приложения</param>
  /// <param name="newApplicationObject">Признак, что это новый API-объект приложения. Он создается при первом подключении, а также при переподключении к приложению, если оно было завершено или упало</param>
  protected virtual void DoAttachApplicationToApiSession(
    TApplication applicationObject,
    bool newApplicationObject)
  {
    this.StartApiResourceManager(applicationObject);
  }

  /// <summary>
  /// Отсоединяет API-объект приложения от сессии доступа к API интегрируемого приложения. Этот метод вызывается в процессе закрытия сессии верхнего уровня, для вложенных
  /// сессий он не вызывается. API-объект приложения не освобождается, он будет повторно использоваться для открытия всех последующих сессий.
  /// </summary>
  /// <param name="applicationObject">API-объект приложения</param>
  protected virtual void DoDetachApplicationFromApiSession(TApplication applicationObject)
  {
    this.StopApiResourceManager(applicationObject);
  }

  private void StartApiResourceManager(TApplication applicationObject)
  {
    if (IntegratorVars.ConserveAppResources.Value)
    {
      this.apiResourceManager = this.TryCreateApiResourceManager(applicationObject);
      if (this.apiResourceManager == null)
        return;
      try
      {
        this.apiResourceManager.Start();
      }
      catch
      {
        this.apiResourceManager = (ApplicationApiResourceManager) null;
        throw;
      }
      this.apiResourceManager.ErrorReporter = (IErrorReporter) new ErrorReporterAdapter((IMessageReporter) new MultilineMessageReporter((IMessageReporter) new OutputViewMessageReporter(ServiceUtils.GetService<IOutputView>((object) ServicesManager.ServiceContainer, true), "Ошибки")))
      {
        CaptionGenerator = new Func<ICollection<ErrorInfo>, string>(this.MakeApiResourceManagerErrorCaption)
      };
      this.UpdateApiResourceManagerReferences(applicationObject);
    }
    else
      this.EnsureApplicationWindowIsAvailableToUser(applicationObject);
  }

  private void StopApiResourceManager(TApplication applicationObject)
  {
    if (this.apiResourceManager == null)
      return;
    this.apiResourceManager.ReleaseResourcesAndStop();
    this.apiResourceManager = (ApplicationApiResourceManager) null;
    this.UpdateApiResourceManagerReferences(applicationObject);
  }

  private string MakeApiResourceManagerErrorCaption(ICollection<ErrorInfo> errors)
  {
    return errors.Count == 1 ? $"При освобождении ресурсов приложения '{this.ApplicationName}' произошла ошибка." : $"При освобождении ресурсов приложения '{this.ApplicationName}' произошли ошибки.";
  }

  /// <summary>
  /// Создает менеджер ресурсов приложения, использованных интегратором в сессии подключения к API приложения.
  /// Если управление ресурсами приложения не требуется, то метод может вернуть null.
  /// </summary>
  /// <param name="applicationObject">API-объект приложения</param>
  /// <returns>Менеджер ресурсов приложения или null, если управление ресурсами приложения не требуется</returns>
  protected virtual ApplicationApiResourceManager TryCreateApiResourceManager(
    TApplication applicationObject)
  {
    return (ApplicationApiResourceManager) null;
  }

  /// <summary>
  /// Метод вызывается после создания и удаления менеджера ресурсов приложения. Он используется для создания/удаления объектов, связанных с менеджером ресурсов
  /// (например, трекера ресурсов приложения).
  /// </summary>
  /// <param name="applicationObject">API-объект приложения</param>
  protected virtual void UpdateApiResourceManagerReferences(TApplication applicationObject)
  {
  }

  /// <summary>
  /// Обеспечивает отображение окна приложения на экране. Метод вызывается в том случае, если управление ресурсами приложения не используется или не реализовано.
  /// Это делается для того, чтобы пользователь мог управлять ресурсами приложения вручную.
  /// </summary>
  /// <param name="applicationObject">API-объект приложения</param>
  protected virtual void EnsureApplicationWindowIsAvailableToUser(TApplication applicationObject)
  {
  }

  /// <summary>
  /// Возвращает менеджер ресурсов приложения, использованных интегратором в сессии подключения к API приложения.
  /// Менеджер позволяет сохранить информацию о ресурсах приложения (COM-объекты и др.), открытых интегратором, а при
  /// закрытии сессии подключения к API приложения - освободить записанные ресурсы.
  /// </summary>
  /// <returns>Менеджер ресурсов приложения или null, если управление ресурсами приложения не требуется</returns>
  protected ApplicationApiResourceManager ApiResourceManager
  {
    [DebuggerStepThrough] get => this.apiResourceManager;
  }
}
