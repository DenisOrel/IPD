// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.CADInterfaceServiceBase
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.CADInterface.Proxies;
using Intermech.Data;
using Intermech.Localization;
using Intermech.Runtime;
using Intermech.Runtime.ComInterop;
using Intermech.Runtime.ComInterop.Proxies;
using Interop.CADInterface;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Реализует основу сервиса для работы с API CAD-системы. Класс является thread-safe.
/// </summary>
public abstract class CADInterfaceServiceBase : 
  ApplicationApiService<CADSystemProxy>,
  IDocumentApiService,
  IExternalApiService,
  IIntegratorService
{
  private const int MinSupportedVersion = 3;
  private readonly ComObjectProvider cadInterfaceProvider;
  private readonly CADApiOperations apiOperations;
  private IApplicationFileTypes fileTypeService;
  private OpenDocumentsApi openDocumentsApi;
  private ICADSystem2 lastCreatedRawCADSystem;
  private bool firstTimeSession;
  private bool reconfigureCADSystemOnNextSession;
  private CADSystemProxyBuilder proxyBuilder;

  /// <summary>Создает сервис.</summary>
  /// <param name="owner">Владелец сервиса</param>
  /// <param name="applicationName">Название приложения</param>
  /// <param name="cadInterfaceProvider">Провайдер типа COM-объекта CAD-интерфейса</param>
  /// <exception cref="T:System.ArgumentNullException">Ссылка на объект провайдера типа COM-объекта не может быть null</exception>
  public CADInterfaceServiceBase(
    IIntegrator owner,
    string applicationName,
    ComObjectProvider cadInterfaceProvider)
    : base(owner, applicationName)
  {
    this.cadInterfaceProvider = cadInterfaceProvider != null ? cadInterfaceProvider : throw new ArgumentNullException(nameof (cadInterfaceProvider));
    this.apiOperations = new CADApiOperations(owner, (IApplicationApiService) this);
    this.firstTimeSession = true;
  }

  /// <summary>
  /// Возвращает или задает ссылку на сервис типов файлов интегратора. Свойство должно быть заполнено до начала использования текущего сервиса.
  /// </summary>
  public IApplicationFileTypes FileTypeService
  {
    [DebuggerStepThrough] get
    {
      lock (this.Integrator.SyncRoot)
        return this.fileTypeService;
    }
    [DebuggerStepThrough] set
    {
      lock (this.Integrator.SyncRoot)
      {
        this.RequireNotInitialized();
        this.fileTypeService = value;
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
    if (this.FileTypeService == null)
      throw PropertyExceptions.PropertyNotSetException((object) this, "FileTypeService");
    this.openDocumentsApi = new OpenDocumentsApi(this.fileTypeService, (IExternalApiService) this);
    this.openDocumentsApi.OnFindOpenDocument += new Func<string, IOpenDocument>(this.FindOpenDocument);
    this.openDocumentsApi.OnOpenDocument += new Func<string, IOpenDocument>(this.OpenDocument);
    this.openDocumentsApi.OnValidateDocument += new Action<IOpenDocument>(this.ValidateDocument);
    this.openDocumentsApi.OnGetDocumentCodec += new Func<IOpenDocument, IAttributeCodec>(this.GetDocumentCodec);
    this.openDocumentsApi.OnGetDocumentAttributeContainer += new Func<IOpenDocument, IValueBagContainer>(this.GetDocumentAttributeContainer);
    this.openDocumentsApi.OnSaveDocument += new Action<IOpenDocument>(this.SaveDocument);
    this.openDocumentsApi.OnCloseDocument += new Action<IOpenDocument>(this.CloseDocument);
  }

  public IOpenDocumentsApi OpenDocuments
  {
    [DebuggerStepThrough] get
    {
      this.RequireReadyState();
      return (IOpenDocumentsApi) this.openDocumentsApi;
    }
  }

  private IOpenDocument FindOpenDocument(string fullPath)
  {
    CADDocumentProxy openDocument = this.GetApplicationObject().FindOpenDocument(fullPath);
    return openDocument != null ? (IOpenDocument) CADInterfaceAdapters.AsOpenDocument(openDocument) : (IOpenDocument) null;
  }

  private IOpenDocument OpenDocument(string fullPath)
  {
    return (IOpenDocument) CADInterfaceAdapters.AsOpenDocument(this.GetApplicationObject().OpenDocument(fullPath, false));
  }

  /// <summary>
  /// Возвращает кодек атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов документа</returns>
  private IAttributeCodec GetDocumentCodec(IOpenDocument document)
  {
    return this.DoGetDocumentCodec(((CADOpenDocumentAdapter) document).Document);
  }

  /// <summary>
  /// Возвращает кодек атрибутов документа для указанного документа CAD-системы.
  /// </summary>
  /// <param name="document">Открытый документ CAD-системы</param>
  /// <returns>Кодек атрибутов документа</returns>
  protected abstract IAttributeCodec DoGetDocumentCodec(CADDocumentProxy document);

  private IValueBagContainer GetDocumentAttributeContainer(IOpenDocument document)
  {
    return ((CADOpenDocumentAdapter) document).Properties;
  }

  private void ValidateDocument(IOpenDocument openDocument)
  {
    if (!(openDocument is CADOpenDocumentAdapter))
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_550"));
  }

  private void SaveDocument(IOpenDocument openDocument)
  {
    CADDocumentProxy document = ((CADOpenDocumentAdapter) openDocument).Document;
    if (!document.Modified || document.ReadOnly)
      return;
    document.Save();
  }

  private void CloseDocument(IOpenDocument openDocument)
  {
    ((CADOpenDocumentAdapter) openDocument).Document.Close();
  }

  protected override bool IsInstalled() => this.cadInterfaceProvider.IsRegistered();

  protected override bool IsRunning()
  {
    ICADSystem2 cadSystemObject = this.GetOrCreateCADSystemObject();
    return this.RawGetVersion(cadSystemObject) >= 3 && this.RawIsCADLoaded(cadSystemObject);
  }

  /// <summary>
  /// Проверяет работоспособность кэшированного API-объекта приложения. Он может стать нерабочим, если приложение было закрытом пользователем или упало.
  /// В этом случае метод должен сбросить исключение.
  /// </summary>
  /// <param name="cadSystem">API-объект приложения</param>
  /// <exception cref="T:System.Exception">API-объект приложения не работает. Требуется переподключение к приложению</exception>
  protected override void DoTestApplicationObject(CADSystemProxy cadSystem)
  {
    this.CheckCADSystemConnection(cadSystem.RawObject);
    if (!this.RawIsCADLoaded(cadSystem.RawObject))
      throw new COMException("CAD system is dead.");
  }

  /// <summary>
  /// Выполняет подключение к интегрируемому приложению. Метод возвращает API-объект приложения, через который осуществляется всё взаимодействие с приложением.
  /// </summary>
  /// <returns>API-объект приложения</returns>
  protected override CADSystemProxy DoCreateApplicationObject()
  {
    CADSystemProxy cadSystemProxy = this.CreateCADSystemProxy(this.GetOrCreateCADSystemObject(), this.GetOrCreateProxyBuilder());
    this.CheckCADSystemVersion(cadSystemProxy);
    cadSystemProxy.SetLocalizer((AttributeLocalizer) new AttributeLocalizerComAdapter((IAttributeLocalizer) new IPSAttributeLocalizer()));
    return cadSystemProxy;
  }

  /// <summary>Освобождает кэшированный API-объект приложения.</summary>
  /// <param name="cadSystem">API-объект приложения</param>
  protected override void DoReleaseApplicationObject(CADSystemProxy cadSystem)
  {
    base.DoReleaseApplicationObject(cadSystem);
    this.lastCreatedRawCADSystem = (ICADSystem2) null;
  }

  private void CheckCADSystemVersion(CADSystemProxy cadSystem)
  {
    int version = cadSystem.GetVersion();
    if (version < 3)
      throw new ApplicationNotInstalledException(this.Integrator.DisplayName, string.Format(LocalizationHolder.rm.GetString("Tools.Components_375"), (object) version, (object) this.ApplicationName));
  }

  /// <summary>
  /// Создает и возвращает прокси-объект для CAD-интерфейса.
  /// </summary>
  /// <param name="rawCADSystem">Сырой COM-объект CAD-интерфейса</param>
  /// <param name="builder">Построитель прокси-объектов для CAD-интерфейса</param>
  /// <returns>Прокси-объект для CAD-интерфейса</returns>
  protected virtual CADSystemProxy CreateCADSystemProxy(
    ICADSystem2 rawCADSystem,
    CADSystemProxyBuilder builder)
  {
    return new CADSystemProxy(rawCADSystem, builder);
  }

  /// <summary>
  /// Возвращает построитель частей прокси-объекта для CAD-интерфейса.
  /// </summary>
  /// <returns>Построитель частей</returns>
  protected CADSystemProxyBuilder GetOrCreateProxyBuilder()
  {
    if (this.proxyBuilder == null)
      this.proxyBuilder = this.CreateProxyBuilder();
    return this.proxyBuilder;
  }

  /// <summary>
  /// Создает и возвращает построитель частей прокси-объекта для CAD-интерфейса.
  /// </summary>
  /// <returns>Построитель частей</returns>
  protected virtual CADSystemProxyBuilder CreateProxyBuilder() => new CADSystemProxyBuilder();

  private void CreateStdLibFolder()
  {
    if (StandardLibraryServices.GetMode((IServiceProvider) this.Integrator) == StandardLibraryMode.NotSupported)
      return;
    string modelFolderPath = StandardLibraryServices.GetModelFolderPath((IServiceProvider) this.Integrator);
    try
    {
      if (Directory.Exists(modelFolderPath))
        return;
      Directory.CreateDirectory(modelFolderPath);
    }
    catch (Exception ex)
    {
      throw new BadApplicationSettingsException(this.Integrator.DisplayName, this.ApplicationName, ex.Message);
    }
  }

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
    if (this.firstTimeSession)
    {
      this.CreateStdLibFolder();
      this.firstTimeSession = false;
    }
    base.DoOpenApiSession(topLevelSession);
  }

  /// <summary>
  /// Присоединяет API-объект приложения к открытой сессии доступа к API интегрируемого приложения. Этот метод вызывается в процессе открытия сессии доступа верхнего уровня,
  /// для вложенных сессий он не вызывается.
  /// </summary>
  /// <param name="cadSystem">API-объект приложения</param>
  /// <param name="newCADSystemInstance">Признак, что это новый API-объект приложения. Он создается при первом подключении, а также при переподключении к приложению, если оно было завершено или упало</param>
  protected override void DoAttachApplicationToApiSession(
    CADSystemProxy cadSystem,
    bool newCADSystemInstance)
  {
    base.DoAttachApplicationToApiSession(cadSystem, newCADSystemInstance);
    this.PrepareApplication(cadSystem, newCADSystemInstance);
    cadSystem.Cache = new CADSystemCache();
  }

  private void PrepareApplication(CADSystemProxy cadSystem, bool newCADSystemInstance)
  {
    if (IntegratorVars.NakedApiSessions.Value)
    {
      this.reconfigureCADSystemOnNextSession = true;
    }
    else
    {
      if (newCADSystemInstance && !this.reconfigureCADSystemOnNextSession)
        this.reconfigureCADSystemOnNextSession = true;
      if (!this.reconfigureCADSystemOnNextSession)
        return;
      this.apiOperations.ReconfigureApplication(cadSystem);
      this.reconfigureCADSystemOnNextSession = false;
    }
  }

  /// <summary>
  /// Отсоединяет API-объект приложения от сессии доступа к API интегрируемого приложения. Этот метод вызывается в процессе закрытия сессии верхнего уровня, для вложенных
  /// сессий он не вызывается. API-объект приложения не освобождается, он будет повторно использоваться для открытия всех последующих сессий.
  /// </summary>
  /// <param name="cadSystem">API-объект приложения</param>
  protected override void DoDetachApplicationFromApiSession(CADSystemProxy cadSystem)
  {
    base.DoDetachApplicationFromApiSession(cadSystem);
    if (cadSystem.GroupOperationStarted)
      cadSystem.EndGroupOperation();
    if (cadSystem.Cache == null)
      return;
    cadSystem.Cache = (CADSystemCache) null;
  }

  /// <summary>
  /// Создает менеджер ресурсов приложения, использованных интегратором в сессии подключения к API приложения.
  /// </summary>
  /// <param name="cadSystem">API-объект приложения</param>
  /// <returns>Менеджер ресурсов приложения</returns>
  protected override ApplicationApiResourceManager TryCreateApiResourceManager(
    CADSystemProxy cadSystem)
  {
    return (ApplicationApiResourceManager) new CADInterfaceApiResourceManager(cadSystem);
  }

  private ICADSystem2 GetOrCreateCADSystemObject()
  {
    try
    {
      if (this.lastCreatedRawCADSystem != null)
      {
        try
        {
          this.CheckCADSystemConnection(this.lastCreatedRawCADSystem);
        }
        catch (InvalidComObjectException ex)
        {
          this.lastCreatedRawCADSystem = (ICADSystem2) null;
        }
        catch (COMException ex)
        {
          this.lastCreatedRawCADSystem = (ICADSystem2) null;
        }
      }
      if (this.lastCreatedRawCADSystem == null)
      {
        // ISSUE: reference to a compiler-generated field
        if (CADInterfaceServiceBase.\u003C\u003Eo__30.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          CADInterfaceServiceBase.\u003C\u003Eo__30.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, ICADSystem2>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (ICADSystem2), typeof (CADInterfaceServiceBase)));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.lastCreatedRawCADSystem = CADInterfaceServiceBase.\u003C\u003Eo__30.\u003C\u003Ep__0.Target((CallSite) CADInterfaceServiceBase.\u003C\u003Eo__30.\u003C\u003Ep__0, this.cadInterfaceProvider.CreateInstance());
      }
      return this.lastCreatedRawCADSystem;
    }
    catch (COMException ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat(LocalizationHolder.rm.GetString("Tools.Components_374"), (object) this.ApplicationName);
      stringBuilder.Append(' ');
      stringBuilder.Append(ex.Message);
      throw new ApplicationProxyException(stringBuilder.ToString());
    }
  }

  private void CheckCADSystemConnection(ICADSystem2 rawCADSystem)
  {
    if (this.RawGetVersion(rawCADSystem) == int.MinValue)
      throw new COMException("CAD interface is dead.");
  }

  private int RawGetVersion(ICADSystem2 rawCADSystem)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem2.GetVersion()");
    return rawCADSystem.GetVersion();
  }

  private bool RawIsCADLoaded(ICADSystem2 rawCADSystem)
  {
    if (CADInterfaceTracing.ExternalCallTracer.Enabled)
      CADInterfaceTracing.ExternalCallTracer.AddToTrace("ICADSystem2.get_IsCADLoaded()");
    return rawCADSystem.IsCADLoaded;
  }
}
