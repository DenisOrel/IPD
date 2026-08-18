
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServer
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.Collections;
using Intermech.Configuration;
using Intermech.IO;
using Intermech.Pools;
using Intermech.Text;
using Intermech.Win32;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// COM-сервер для приложений Windows. Реализация является thread-safe.
    /// </summary>
    public class ComServer
    {
      private static readonly ICollection<Type> emptyComClasses = (ICollection<Type>) new Type[0];
      private object syncRoot;
      private int creationThreadId;
      private IHostApplication hostApplication;
      private ComPluginManager comPluginManager;
      private RegistrationServices systemRegistrationService;
      private ComServerCommandLine commandLine;
      private ComServerRunMode runMode;
      private bool isInitialized;
      private bool isAllowed;
      private bool hasLastRegisteredHost;
      private bool isLastRegisteredHost;
      private bool isActive;
      private bool isClientRequestBlocked;
      private ComProcessReferenceCounter processRefCounter;
      private LiveComObjectsTracker liveComObjectsTracker;
      private Dictionary<Type, ComClassData> comClassTable;
      private ComObjectFactory normalComObjectFactory;
      private ComObjectFactory missingComObjectFactory;
      private ComPluginRegistrationService comPluginRegistrationService;
      private Wow64RegistrationServices wow64Helper;

      /// <summary>
      /// Создает объект. Метод должен быть вызван в потоке, чей COM Apartment = STA.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Для создания объекта требуется поток с COM Apartment = STA</exception>
      /// <remarks>
      /// Требование STA-потока вызвано тем, что активация и деактивация каждого COM-класса должны выполняться в одном и том же COM Apartment.
      /// При этом допускается активировать free threaded COM-классы в STA, они все равно будут работать как free threaded.
      /// Данное требование трудно невыполнимо в случае, если приложение COM-сервера использует плагины, где каждый плагин может самостоятельно активировать свои COM-классы.
      /// Поэтому приложение должно выбрать один из своих потоков и использовать его и для создания объекта COM-сервера, и для активации/деактивации всех COM-классов.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </remarks>
      public ComServer()
      {
        Thread currentThread = Thread.CurrentThread;
        if (currentThread.GetApartmentState() != ApartmentState.STA)
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_STAThreadRequired, (object) this.GetType()));
        this.syncRoot = new object();
        this.creationThreadId = currentThread.ManagedThreadId;
      }

      private void CheckForCreationThread()
      {
        if (Thread.CurrentThread.ManagedThreadId != this.creationThreadId)
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_CreationThreadRequired, (object) this.GetType()));
      }

      /// <summary>Выполняет инициализацию COM-сервера.</summary>
      /// <returns>Результат инициализации СОМ-сервера</returns>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера уже была выполнена</exception>
      public ComServerInitializationResult Initialize()
      {
        lock (this.syncRoot)
        {
          this.CheckNotInitialized();
          this.ValidateSetupProperties();
          bool exitRequested = false;
          try
          {
            this.InitializeComServerProperties();
            exitRequested = this.commandLine.Command != 0;
            switch (this.commandLine.Command)
            {
              case ComServerInitializationCommand.Register:
                this.RegisterComClasses();
                break;
              case ComServerInitializationCommand.Unregister:
                this.UnregisterComClasses();
                break;
              default:
                string registeredHostApplication = ComServer.GetLastRegisteredHostApplication(this.hostApplication.HostId);
                this.hasLastRegisteredHost = !string.IsNullOrEmpty(registeredHostApplication);
                this.isLastRegisteredHost = PathUtils.IsSamePath(registeredHostApplication, this.hostApplication.ExecutablePath);
                this.isActive = this.IsActiveModeRequired();
                if (this.isActive)
                {
                  this.InitializeActiveMode();
                  break;
                }
                break;
            }
            ComServerInitializationResult initializationResult = new ComServerInitializationResult(exitRequested: exitRequested);
            this.isInitialized = true;
            if (TraceSwitches.General.TraceVerbose && this.isActive)
              Trace.WriteLine(ComServerResources.Trace_ComServerInitializedAndActive);
            return initializationResult;
          }
          catch (Exception ex)
          {
            int num = exitRequested ? 1 : 0;
            ComServerInitializationResult initializationResult = new ComServerInitializationResult(ex, num != 0);
            this.ResetComServerProperties();
            return initializationResult;
          }
          finally
          {
            this.ReleaseInitializationOnlyServices();
          }
        }
      }

      private void InitializeActiveMode()
      {
        if (TraceSwitches.General.TraceVerbose && !this.isAllowed)
          Trace.WriteLine(ComServerResources.Trace_CantDisableComServerUntilUnregisterCommand);
        this.comClassTable = new Dictionary<Type, ComClassData>();
        if (this.normalComObjectFactory == null)
          this.normalComObjectFactory = (ComObjectFactory) new DefaultComObjectFactory();
        this.missingComObjectFactory = (ComObjectFactory) new MissingComObjectFactory();
        this.processRefCounter = ComProcess.Instance.ProcessRefCounter;
        this.processRefCounter.Released += new EventHandler(this.OnComProcessReleased);
        this.liveComObjectsTracker = ComProcess.Instance.LiveComObjectsTracker;
        this.liveComObjectsTracker.EnsureActive();
      }

      private void OnComProcessReleased(object sender, EventArgs e) => this.RaiseReleased();

      private void ValidateSetupProperties()
      {
        if (this.hostApplication == null)
          throw PropertyExceptions.PropertyNotSetException((object) this, "HostApplication");
        if (this.hostApplication.HostId == Guid.Empty)
          throw new InvalidOperationException(ComServerResources.Arg_HostApplicationIdRequired);
        if (string.IsNullOrEmpty(this.hostApplication.ExecutablePath))
          throw new InvalidOperationException(ComServerResources.Arg_HostApplicationExecutablePathRequired);
        if (!Path.IsPathRooted(this.hostApplication.ExecutablePath))
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Arg_HostApplicationExecutablePathMustBeAbsolute, (object) this.hostApplication.ExecutablePath));
        if (this.comPluginManager == null)
          throw PropertyExceptions.PropertyNotSetException((object) this, "PluginManager");
      }

      private void InitializeComServerProperties()
      {
        this.isAllowed = AppSettingsHelper.GetBoolean(nameof (ComServer), true);
        this.commandLine = new ComServerCommandLineReader().Read(this.hostApplication.GetCommandLineArguments());
        this.runMode = this.commandLine.RunMode;
      }

      private void ResetComServerProperties()
      {
        this.commandLine = (ComServerCommandLine) null;
        this.runMode = ComServerRunMode.Normal;
        this.isAllowed = false;
        this.hasLastRegisteredHost = false;
        this.isLastRegisteredHost = false;
        this.isActive = false;
      }

      private bool IsActiveModeRequired()
      {
        return this.commandLine.RunMode == ComServerRunMode.Embedding || this.isLastRegisteredHost || this.isAllowed;
      }

      private RegistrationServices SystemRegistrationService
      {
        get
        {
          if (this.systemRegistrationService == null)
            this.systemRegistrationService = new RegistrationServices();
          return this.systemRegistrationService;
        }
      }

      private ComPluginRegistrationService PluginRegistrationService
      {
        get
        {
          if (this.comPluginRegistrationService == null)
            this.comPluginRegistrationService = new ComPluginRegistrationService();
          return this.comPluginRegistrationService;
        }
      }

      private Wow64RegistrationServices Wow64Helper
      {
        get
        {
          if (this.wow64Helper == null)
            this.wow64Helper = new Wow64RegistrationServices();
          return this.wow64Helper;
        }
      }

      private void ReleaseInitializationOnlyServices()
      {
        this.comPluginRegistrationService = (ComPluginRegistrationService) null;
        this.wow64Helper = (Wow64RegistrationServices) null;
      }

      private void CheckNotInitialized()
      {
        if (this.isInitialized)
          throw new InvalidOperationException(ComServerResources.SR_ComServerIsAlreadyInitialized);
      }

      private void CheckInitialized()
      {
        if (!this.isInitialized)
          throw new InvalidOperationException(ComServerResources.SR_ComServerIsNotInitialized);
      }

      private void CheckInitializedAndActive()
      {
        this.CheckInitialized();
        if (!this.isActive)
          throw new InvalidOperationException(ComServerResources.SR_ComServerIsDisabled);
      }

      /// <summary>
      /// Возвращает или задает объект для связи COM-сервера с приложением.
      /// Свойство должно быть задано до инициализации COM-сервера.
      /// </summary>
      public IHostApplication HostApplication
      {
        get
        {
          lock (this.syncRoot)
            return this.hostApplication;
        }
        set
        {
          lock (this.syncRoot)
          {
            this.CheckNotInitialized();
            this.hostApplication = value;
          }
        }
      }

      /// <summary>
      /// Возвращает или задает менеджер плагинов COM-сервера.
      /// Свойство должно быть задано до инициализации COM-сервера.
      /// </summary>
      public ComPluginManager ComPluginManager
      {
        get
        {
          lock (this.syncRoot)
            return this.comPluginManager;
        }
        set
        {
          lock (this.syncRoot)
          {
            this.CheckNotInitialized();
            this.comPluginManager = value;
          }
        }
      }

      /// <summary>
      /// Возвращает или задает фабрику COM-объектов.
      /// Свойство может быть не задано, в этом случае будет использоваться фабрика COM-объектов по умолчанию.
      /// </summary>
      public ComObjectFactory ComObjectFactory
      {
        get
        {
          lock (this.syncRoot)
            return this.normalComObjectFactory;
        }
        set
        {
          lock (this.syncRoot)
          {
            this.CheckNotInitialized();
            this.normalComObjectFactory = value;
          }
        }
      }

      /// <summary>
      /// Возвращает признак, что инициализация COM-сервера была выполнена.
      /// </summary>
      public bool IsInitialized
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.isInitialized;
        }
      }

      /// <summary>
      /// Возвращает признак, что COM-сервер активен, и приложение может принимать клиентские запросы на подключение и создание COM-объектов.
      /// </summary>
      public bool IsActive
      {
        get
        {
          lock (this.syncRoot)
            return this.isActive;
        }
      }

      /// <summary>Возвращает режим работы COM-сервера.</summary>
      public ComServerRunMode RunMode
      {
        [DebuggerStepThrough] get
        {
          lock (this.syncRoot)
            return this.runMode;
        }
      }

      /// <summary>
      /// Событие, срабатывающее после создания COM-объекта по запросу от клиента.
      /// </summary>
      public event EventHandler<ComObjectEventArgs> ComObjectCreated;

      /// <summary>
      /// Событие, срабатывающее после освобождения всех COM-объектов приложения в режиме запуска по запросу COM-клиента.
      /// </summary>
      public event EventHandler Released;

      private void RaiseReleased()
      {
        EventHandler released = this.Released;
        if (released == null)
          return;
        released((object) this, EventArgs.Empty);
      }

      internal void RaiseComObjectCreated(object comObject)
      {
        EventHandler<ComObjectEventArgs> comObjectCreated = this.ComObjectCreated;
        if (comObjectCreated == null)
          return;
        comObjectCreated((object) this, new ComObjectEventArgs(comObject));
      }

      /// <summary>
      /// Регистрирует все COM-классы приложения в реестре Windows.
      /// </summary>
      /// <exception cref="T:System.Exception">В процессе регистрации COM-классов приложения произошла ошибка</exception>
      private void RegisterComClasses()
      {
        if (!this.isAllowed)
          throw new ComServerException(ComServerResources.SR_RegisterCommandRejected);
        if (TraceSwitches.General.TraceInfo)
          Trace.WriteLine(ComServerResources.Trace_RegisterCommandStarted);
        ErrorList errorList = new ErrorList();
        foreach (ComPluginInfo plugin in (IEnumerable<ComPluginInfo>) this.comPluginManager.FindPlugins(this, (IErrorList) errorList))
        {
          List<Guid> typeLibIdList = this.RegisterTypeLibraries(plugin, (IErrorList) errorList);
          RegisterComPluginContext pluginContext = new RegisterComPluginContext(this, plugin.AssemblyPath, (ICollection<Guid>) typeLibIdList, (IErrorList) errorList);
          this.RegisterComObjects(plugin, (IErrorList) errorList, pluginContext);
        }
        this.SaveLastRegisteredHostApplication((IErrorList) errorList);
        if (!errorList.Successful)
          throw this.CreateComPluginRegistrationException(ComServerResources.SR_RegisterCommandError, errorList);
      }

      private List<Guid> RegisterTypeLibraries(ComPluginInfo pluginInfo, IErrorList errorList)
      {
        List<Guid> guidList = new List<Guid>(pluginInfo.TypeLibPathList.Count);
        foreach (string typeLibPath in (IEnumerable<string>) pluginInfo.TypeLibPathList)
        {
          Guid guid = this.RegisterTypeLibrary(typeLibPath, errorList);
          if (guid != Guid.Empty)
            guidList.Add(guid);
        }
        return guidList;
      }

      private void RegisterComObjects(
        ComPluginInfo pluginInfo,
        IErrorList errorList,
        RegisterComPluginContext pluginContext)
      {
        RegisterCommandContext.Global = new RegisterCommandContext(this.PluginRegistrationService, pluginContext);
        try
        {
          this.SystemRegistrationService.RegisterAssembly(Assembly.LoadFrom(pluginInfo.AssemblyPath), AssemblyRegistrationFlags.SetCodeBase);
          if (!TraceSwitches.General.TraceVerbose)
            return;
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_FileProcessed, (object) pluginInfo.AssemblyPath));
        }
        catch (TypeLoadException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        catch (FileNotFoundException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        catch (FileLoadException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        catch (BadImageFormatException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        finally
        {
          RegisterCommandContext.Global = (RegisterCommandContext) null;
        }
      }

      private Guid RegisterTypeLibrary(string typeLibPath, IErrorList errorList)
      {
        try
        {
          Guid guid = TypeLibServices.RegisterLibrary(typeLibPath).guid;
          if (TraceSwitches.General.TraceVerbose)
            Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_FileProcessed, (object) typeLibPath));
          return guid;
        }
        catch (COMException ex)
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            stringBuilder.AppendFormat((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_RegisterTypeLibError, (object) Path.GetFileName(typeLibPath));
            stringBuilder.Append(' ');
            stringBuilder.Append(ex.Message);
            errorList.AddError(stringBuilder.ToString());
            return Guid.Empty;
          }
        }
      }

      /// <summary>
      /// Отменяет регистрацию всех COM-классов приложения в реестре Windows.
      /// </summary>
      /// <exception cref="T:System.Exception">В процессе отмены регистрации COM-классов приложения произошла ошибка</exception>
      private void UnregisterComClasses()
      {
        string registeredHostApplication = ComServer.GetLastRegisteredHostApplication(this.hostApplication.HostId);
        if (!string.IsNullOrEmpty(registeredHostApplication) && !PathUtils.IsSamePath(registeredHostApplication, this.hostApplication.ExecutablePath))
        {
          if (!TraceSwitches.General.TraceInfo)
            return;
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, "COM: {0}", (object) ComServerResources.SR_UnregisterCommandRejected));
        }
        else
        {
          if (TraceSwitches.General.TraceInfo)
            Trace.WriteLine(ComServerResources.Trace_UnregisterCommandStarted);
          ErrorList errorList = new ErrorList();
          foreach (ComPluginInfo plugin in (IEnumerable<ComPluginInfo>) this.comPluginManager.FindPlugins(this, (IErrorList) errorList))
          {
            UnregisterComPluginContext pluginContext = new UnregisterComPluginContext(this, plugin.AssemblyPath, (IErrorList) errorList);
            this.UnregisterComObjects(plugin, (IErrorList) errorList, pluginContext);
            this.UnregisterTypeLibraries(plugin, (IErrorList) errorList);
          }
          this.ClearLastRegisteredHostApplication((IErrorList) errorList);
          if (!errorList.Successful)
            throw this.CreateComPluginRegistrationException(ComServerResources.SR_UnregisterCommandError, errorList);
        }
      }

      private void UnregisterTypeLibraries(ComPluginInfo pluginInfo, IErrorList errorList)
      {
        foreach (string typeLibPath in (IEnumerable<string>) pluginInfo.TypeLibPathList)
          this.UnregisterTypeLibrary(typeLibPath, errorList);
      }

      private void UnregisterComObjects(
        ComPluginInfo pluginInfo,
        IErrorList errorList,
        UnregisterComPluginContext pluginContext)
      {
        UnregisterCommandContext.Global = new UnregisterCommandContext(this.PluginRegistrationService, pluginContext);
        try
        {
          this.SystemRegistrationService.UnregisterAssembly(Assembly.LoadFrom(pluginInfo.AssemblyPath));
          if (!TraceSwitches.General.TraceVerbose)
            return;
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_FileProcessed, (object) pluginInfo.AssemblyPath));
        }
        catch (FileNotFoundException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        catch (FileLoadException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        catch (BadImageFormatException ex)
        {
          errorList.AddError(this.CreateComPluginAssemblyLoadError(Path.GetFileName(pluginInfo.AssemblyPath), (Exception) ex));
        }
        finally
        {
          UnregisterCommandContext.Global = (UnregisterCommandContext) null;
        }
      }

      private void UnregisterTypeLibrary(string typeLibPath, IErrorList errorList)
      {
        try
        {
          TypeLibServices.UnregisterLibrary(typeLibPath);
          if (!TraceSwitches.General.TraceVerbose)
            return;
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_FileProcessed, (object) typeLibPath));
        }
        catch (COMException ex)
        {
          using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
          {
            StringBuilder stringBuilder = objectPoolScope.Object;
            stringBuilder.AppendFormat((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_UnregisterTypeLibError, (object) Path.GetFileName(typeLibPath));
            stringBuilder.Append(' ');
            stringBuilder.Append(ex.Message);
            errorList.AddError(stringBuilder.ToString());
          }
        }
      }

      private void ClearLastRegisteredHostApplication(IErrorList errorList)
      {
        try
        {
          string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CLSID\\{0}", (object) this.hostApplication.HostId.ToString("B"));
          RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(str, RegistryKeyPermissionCheck.ReadWriteSubTree);
          if (registryKey != null)
          {
            using (registryKey)
              registryKey.SetValue((string) null, (object) string.Empty);
          }
          if (this.Wow64Helper.ActiveRegistryView == this.Wow64Helper.OppositeRegistryView)
            return;
          using (RegistryBuilder registryBuilder = new RegistryBuilder(new RegistryKeyLocation(RegistryHive.ClassesRoot, str, this.Wow64Helper.OppositeRegistryView), true))
            registryBuilder.DeleteKey();
        }
        catch (SecurityException ex)
        {
          errorList.AddError(this.CreateGeneralError(ComServerResources.SR_CantClearLastRegisteredHostApplication, (Exception) ex));
        }
        catch (UnauthorizedAccessException ex)
        {
          errorList.AddError(this.CreateGeneralError(ComServerResources.SR_CantClearLastRegisteredHostApplication, (Exception) ex));
        }
        catch (IOException ex)
        {
          errorList.AddError(this.CreateGeneralError(ComServerResources.SR_CantClearLastRegisteredHostApplication, (Exception) ex));
        }
      }

      private void SaveLastRegisteredHostApplication(IErrorList errorList)
      {
        try
        {
          string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CLSID\\{0}", (object) this.hostApplication.HostId.ToString("B"));
          RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(str, RegistryKeyPermissionCheck.ReadWriteSubTree) ?? Registry.ClassesRoot.CreateSubKey(str, RegistryKeyPermissionCheck.ReadWriteSubTree);
          using (registryKey)
            registryKey.SetValue((string) null, (object) this.hostApplication.ExecutablePath);
          if (this.Wow64Helper.ActiveRegistryView == this.Wow64Helper.OppositeRegistryView)
            return;
          RegistryKeyLocation sourceKey = new RegistryKeyLocation(RegistryHive.ClassesRoot, str, this.Wow64Helper.ActiveRegistryView);
          new CopyRegistryKeyTask(sourceKey, sourceKey.GetDifferentView(this.Wow64Helper.OppositeRegistryView)).Perform();
        }
        catch (SecurityException ex)
        {
          errorList.AddError(this.CreateGeneralError(ComServerResources.SR_CantSaveLastRegisteredHostApplication, (Exception) ex));
        }
        catch (UnauthorizedAccessException ex)
        {
          errorList.AddError(this.CreateGeneralError(ComServerResources.SR_CantSaveLastRegisteredHostApplication, (Exception) ex));
        }
        catch (IOException ex)
        {
          errorList.AddError(this.CreateGeneralError(ComServerResources.SR_CantSaveLastRegisteredHostApplication, (Exception) ex));
        }
      }

      /// <summary>
      /// Возвращает путь к последнему зарегистрированному приложению COM-сервера.
      /// </summary>
      /// <param name="hostId">Идентификатор приложения COM-сервера</param>
      /// <returns>Путь к последнему зарегистрированному приложению COM-сервера или null</returns>
      /// <exception cref="T:System.Exception">Параметр <paramref name="hostId" /> не задан</exception>
      public static string GetLastRegisteredHostApplication(Guid hostId)
      {
        if (hostId == Guid.Empty)
          throw new ArgumentException(ComServerResources.Arg_HostApplicationIdRequired, nameof (hostId));
        try
        {
          string registeredHostApplication = (string) null;
          string name = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CLSID\\{0}", (object) hostId.ToString("B"));
          using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(name, RegistryKeyPermissionCheck.ReadSubTree))
          {
            if (registryKey != null)
              registeredHostApplication = Convert.ToString(registryKey.GetValue((string) null));
          }
          return registeredHostApplication;
        }
        catch
        {
          return (string) null;
        }
      }

      private string CreateGeneralError(string errorMessage, Exception exception)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.Append(errorMessage);
          stringBuilder.Append(' ');
          stringBuilder.Append(exception.Message);
          return stringBuilder.ToString();
        }
      }

      private string CreateComPluginAssemblyLoadError(string assemblyPath, Exception exception)
      {
        using (ObjectPoolScope<StringBuilder> objectPoolScope = TextServices.StringBuilderPool.Allocate(2048 /*0x0800*/))
        {
          StringBuilder stringBuilder = objectPoolScope.Object;
          stringBuilder.AppendFormat((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_ComPluginAssemblyLoadError, (object) Path.GetFileName(assemblyPath));
          stringBuilder.Append(' ');
          stringBuilder.Append(exception.Message);
          return stringBuilder.ToString();
        }
      }

      private ComServerRegistrationException CreateComPluginRegistrationException(
        string errorMessage,
        ErrorList errorList)
      {
        ComServerRegistrationException registrationException = new ComServerRegistrationException(errorMessage);
        foreach (string error in errorList.Errors)
          registrationException.Problems.Add(error);
        foreach (string warning in errorList.Warnings)
          registrationException.Problems.Add(warning);
        return registrationException;
      }

      /// <summary>Проверяет, был ли активирован указанный COM-класс.</summary>
      /// <param name="comClass">COM-класс</param>
      /// <returns>Если указанный COM-класс был активирован - true; иначе - false</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comClass" /> не должен быть равен null</exception>
      public bool IsComClassActive(Type comClass)
      {
        if (comClass == (Type) null)
          throw new ArgumentNullException(nameof (comClass));
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          return this.IsComClassActiveInternal(comClass);
        }
      }

      private bool IsComClassActiveInternal(Type comClass) => this.comClassTable.ContainsKey(comClass);

      private bool IsComClassRegisteredToThisHost(Type comClass)
      {
        string firstPath = RegistryHelper.GetValue(RegistryHive.ClassesRoot, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "CLSID\\{0}\\LocalServer32", (object) comClass.GUID.ToString("B")), string.Empty, (string) null);
        if (!string.IsNullOrEmpty(firstPath))
          return PathUtils.IsSamePath(firstPath, this.hostApplication.ExecutablePath);
        return this.isLastRegisteredHost || !this.hasLastRegisteredHost;
      }

      /// <summary>
      /// Активирует COM-класс, делая его доступным для COM-клиентов.
      /// Клиенты смогут создавать COM-объекты, являющиеся экземплярами этого класса.
      /// Метод должен быть вызван в потоке, который использовался для создания объекта COM-сервера.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </summary>
      /// <param name="comClass">COM-класс</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comClass" /> не должен быть равен null</exception>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера не была выполнена, либо поддержка COM отключена; метод вызван на потоке, отличном от потока, использованного для создания объекта COM-сервера</exception>
      /// <exception cref="T:Intermech.Runtime.ComInterop.LocalServer.ComServerException">Тип COM-объекта не является корректным COM-классом, подлежащим активации; повторная активация COM-класса не допустима</exception>
      /// <exception cref="T:System.Exception">Другие ошибки активации COM-класса</exception>
      public void ActivateComClass(Type comClass)
      {
        if (comClass == (Type) null)
          throw new ArgumentNullException(nameof (comClass));
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          this.CheckForCreationThread();
          List<ComClassActivationParameters> activationCollection = new List<ComClassActivationParameters>(1);
          if (this.IsComClassRegisteredToThisHost(comClass))
            activationCollection.Add(new ComClassActivationParameters(comClass, this.normalComObjectFactory));
          if (activationCollection.Count == 0)
            return;
          this.ActivateComClassesInternal(activationCollection);
        }
      }

      /// <summary>
      /// Активирует указанные COM-классы, делая их доступными для COM-клиентов.
      /// Клиенты смогут создавать COM-объекты, являющиеся экземплярами этих классов.
      /// Метод должен быть вызван в потоке, который использовался для создания объекта COM-сервера.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </summary>
      /// <param name="comClasses">Коллекция COM-классов</param>
      /// <param name="skipAlreadyActiveComClasses">Признак, что нужно пропустить все уже активированные COM-классы</param>
      /// <returns>Коллекция COM-классов, которые были активированы</returns>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comClasses" /> не должен быть равен null</exception>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера не была выполнена, либо поддержка COM отключена; метод вызван на потоке, отличном от потока, использованного для создания объекта COM-сервера</exception>
      /// <exception cref="T:Intermech.Runtime.ComInterop.LocalServer.ComServerException">Тип COM-объекта не является корректным COM-классом, подлежащим активации; повторная активация COM-класса не допустима</exception>
      /// <exception cref="T:System.Exception">Другие ошибки активации COM-класса</exception>
      public ICollection<Type> ActivateComClasses(
        ICollection<Type> comClasses,
        bool skipAlreadyActiveComClasses = false)
      {
        if (comClasses == null)
          throw new ArgumentNullException(nameof (comClasses));
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          this.CheckForCreationThread();
          List<ComClassActivationParameters> activationCollection = new List<ComClassActivationParameters>(comClasses.Count);
          foreach (Type comClass in (IEnumerable<Type>) comClasses)
          {
            if (comClass != (Type) null && this.IsComClassRegisteredToThisHost(comClass) && (!skipAlreadyActiveComClasses || !this.IsComClassActiveInternal(comClass)))
              activationCollection.Add(new ComClassActivationParameters(comClass, this.normalComObjectFactory));
          }
          if (activationCollection.Count == 0)
            return ComServer.emptyComClasses;
          this.ActivateComClassesInternal(activationCollection);
          return (ICollection<Type>) activationCollection.ConvertAll((Converter<ComClassActivationParameters, Type>) (item => item.ComClass));
        }
      }

      private void ActivateComClassesInternal(
        List<ComClassActivationParameters> activationCollection)
      {
        List<Type> typeList = new List<Type>(activationCollection.Count);
        try
        {
          foreach (ComClassActivationParameters activation in activationCollection)
          {
            this.ActivateComClassInternal(activation);
            typeList.Add(activation.ComClass);
          }
        }
        catch
        {
          foreach (Type comClass in typeList)
            this.DeactivateComClassInternal(comClass);
          throw;
        }
        if (typeList.Count == 0)
          return;
        Intermech.Runtime.ComInterop.NativeMethods.CoResumeClassObjects();
        if (!TraceSwitches.General.TraceVerbose)
          return;
        foreach (Type type in typeList)
          Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_ComClassActivated, (object) Marshal.GenerateProgIdForType(type), (object) type.AssemblyQualifiedName));
      }

      private void ActivateComClassInternal(ComClassActivationParameters activationParameters)
      {
        Type comClass = activationParameters.ComClass;
        if (!this.IsComClassActivatable(comClass))
          throw new ComServerException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_TypeIsNotComClass, (object) comClass));
        if (this.comClassTable.ContainsKey(comClass))
          throw new ComServerException(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.SR_ComClassIsAlreadyActivated, (object) Marshal.GenerateProgIdForType(comClass), (object) comClass.AssemblyQualifiedName));
        object comClassObject = this.CreateComClassObject(comClass, activationParameters.ComObjectFactory);
        uint cookie = Intermech.Runtime.ComInterop.NativeMethods.CoRegisterClassObject(comClass.GUID, comClassObject, 4U, 5U);
        this.comClassTable.Add(comClass, new ComClassData(cookie, comClassObject));
      }

      private object CreateComClassObject(Type comClass, ComObjectFactory comObjectFactory)
      {
        ComClassObject comClassObject = new ComClassObject(this, comClass, comObjectFactory, (IReferenceCounter) this.processRefCounter);
        return comClass.IsSubclassOf(typeof (StandardOleMarshalObject)) ? (object) new SingleThreadedClassObjectWrapper(comClassObject) : (object) new FreeThreadedClassObjectWrapper(comClassObject);
      }

      /// <summary>
      /// Активирует COM-классы приложения, для которых активация не была выполнена приложением явно.
      /// При попытке создания экземпляров таких COM-классов будет возвращена ошибка E_ABORT.
      /// Метод должен быть вызван в потоке, который использовался для создания объекта COM-сервера.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </summary>
      /// <returns>Коллекция COM-классов, которые были активированы</returns>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера не была выполнена, либо поддержка COM отключена; метод вызван на потоке, отличном от потока, использованного для создания объекта COM-сервера</exception>
      public ICollection<Type> ActivateMissingComClasses()
      {
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          this.CheckForCreationThread();
          List<string> comClasses = this.GetComClasses();
          List<string> activeComClasses = this.GetActiveComClasses();
          comClasses.RemoveAll(new Predicate<string>(activeComClasses.Contains));
          if (comClasses.Count != 0)
          {
            List<ComClassActivationParameters> activationCollection = new List<ComClassActivationParameters>(comClasses.Count);
            foreach (string typeName in comClasses)
            {
              Type type = Type.GetType(typeName, false);
              if (type != (Type) null && this.IsComClassActivatable(type) && this.IsComClassRegisteredToThisHost(type))
                activationCollection.Add(new ComClassActivationParameters(type, this.missingComObjectFactory));
            }
            if (activationCollection.Count != 0)
            {
              this.ActivateComClassesInternal(activationCollection);
              return (ICollection<Type>) activationCollection.ConvertAll((Converter<ComClassActivationParameters, Type>) (item => item.ComClass));
            }
          }
          return ComServer.emptyComClasses;
        }
      }

      private bool IsComClassActivatable(Type comClass)
      {
        return comClass.IsClass && this.SystemRegistrationService.TypeRequiresRegistration(comClass);
      }

      /// <summary>Возвращает коллекцию всех COM-классов приложения.</summary>
      /// <returns>Коллекция COM-классов</returns>
      private List<string> GetComClasses()
      {
        return new ComClassSearchHelper().GetComClasses((ICollection<string>) CollectionUtils.ConvertAsList(this.comPluginManager.FindPlugins(this, (IErrorList) new ErrorList()), (Converter<ComPluginInfo, string>) (item => item.AssemblyPath)));
      }

      /// <summary>Возвращает коллекцию активных COM-классов приложения.</summary>
      /// <returns>Коллекция COM-классов</returns>
      private List<string> GetActiveComClasses()
      {
        List<string> activeComClasses = new List<string>(this.comClassTable.Count);
        foreach (Type key in this.comClassTable.Keys)
          activeComClasses.Add(key.AssemblyQualifiedName);
        return activeComClasses;
      }

      /// <summary>
      /// Деактивирует COM-класс, делая его недоступным для COM-клиентов.
      /// Клиенты больше не смогут создавать COM-объекты, являющиеся экземплярами этого класса.
      /// Метод должен быть вызван в потоке, который использовался для создания объекта COM-сервера.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </summary>
      /// <param name="comClass">COM-класс</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comClass" /> не должен быть равен null</exception>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера не была выполнена, либо поддержка COM отключена; метод вызван на потоке, отличном от потока, использованного для создания объекта COM-сервера</exception>
      /// <remarks>
      /// Если после деактивации COM-класса приложения COM-клиент затребует создание COM-объекта этого класса, то Windows запустит новый экземпляр приложения.
      /// При разработке приложения COM-сервера следует учитывать эту возможность параллельного запуска нескольких экземпляров приложения.
      /// </remarks>
      public void DeactivateComClass(Type comClass)
      {
        if (comClass == (Type) null)
          throw new ArgumentNullException(nameof (comClass));
        this.DeactivateComClasses((ICollection<Type>) new Type[1]
        {
          comClass
        });
      }

      /// <summary>
      /// Деактивирует указанные COM-классы, делая их недоступными для COM-клиентов.
      /// Клиенты больше не смогут создавать COM-объекты, являющиеся экземплярами этих классов.
      /// Метод должен быть вызван в потоке, который использовался для создания объекта COM-сервера.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </summary>
      /// <param name="comClasses">Коллекция COM-классов</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="comClasses" /> не должен быть равен null</exception>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера не была выполнена, либо поддержка COM отключена; метод вызван на потоке, отличном от потока, использованного для создания объекта COM-сервера</exception>
      /// <remarks>
      /// Если после деактивации COM-класса приложения COM-клиент затребует создание COM-объекта этого класса, то Windows запустит новый экземпляр приложения.
      /// При разработке приложения COM-сервера следует учитывать эту возможность параллельного запуска нескольких экземпляров приложения.
      /// </remarks>
      public void DeactivateComClasses(ICollection<Type> comClasses)
      {
        if (comClasses == null)
          throw new ArgumentNullException(nameof (comClasses));
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          this.CheckForCreationThread();
          List<Type> typeList = new List<Type>(comClasses.Count);
          foreach (Type comClass in (IEnumerable<Type>) comClasses)
          {
            if (comClass != (Type) null && this.DeactivateComClassInternal(comClass))
              typeList.Add(comClass);
          }
          if (typeList.Count == 0 || !TraceSwitches.General.TraceVerbose)
            return;
          foreach (Type type in typeList)
            Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.CurrentUICulture, ComServerResources.Trace_ComClassDeactivated, (object) Marshal.GenerateProgIdForType(type), (object) type.AssemblyQualifiedName));
        }
      }

      private bool DeactivateComClassInternal(Type comClass)
      {
            ComClassData comClassData;
        if (!this.comClassTable.TryGetValue(comClass, out comClassData))
          return false;
        this.comClassTable.Remove(comClass);
        Intermech.Runtime.ComInterop.NativeMethods.CoRevokeClassObject(comClassData.Cookie);
        return true;
      }

      /// <summary>
      /// Деактивирует все COM-классы, делая их недоступными для COM-клиентов.
      /// Клиенты больше не смогут создавать COM-объекты, являющиеся экземплярами этих классов.
      /// Метод должен быть вызван в потоке, который использовался для создания объекта COM-сервера.
      /// Как правило, для этих целей используется основной поток приложения.
      /// </summary>
      /// <exception cref="T:System.InvalidOperationException">Инициализация COM-сервера не была выполнена, либо поддержка COM отключена; метод вызван на потоке, отличном от потока, использованного для создания объекта COM-сервера</exception>
      /// <remarks>
      /// Если после деактивации COM-класса приложения COM-клиент затребует создание COM-объекта этого класса, то Windows запустит новый экземпляр приложения.
      /// При разработке приложения COM-сервера следует учитывать эту возможность параллельного запуска нескольких экземпляров приложения.
      /// </remarks>
      public void DeactivateComClasses()
      {
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          this.CheckForCreationThread();
          this.DeactivateComClasses((ICollection<Type>) new List<Type>((IEnumerable<Type>) this.comClassTable.Keys));
        }
      }

      /// <summary>
      /// Блокирует новые клиентские запросы на подключение к процессу приложения для создания новых COM-объектов.
      /// При этом, уже подключенные клиенты и используемые ими COM-объекты сохраняют свою работоспособность.
      /// Метод используется в процессе подготовки приложения к завершению работы. После вызова этого метода приложение должно
      /// как можно быстрее завершить работу. Блокировка не может быть отменена, она действует до завершения работы процесса приложения.
      /// </summary>
      /// <remarks>
      /// Если после получения блокировки COM-клиент затребует создание COM-объекта приложения, то Windows запустит новый экземпляр приложения.
      /// При разработке приложения COM-сервера следует учитывать эту возможность параллельного запуска нескольких экземпляров приложения.
      /// </remarks>
      public void BlockClientRequests()
      {
        lock (this.syncRoot)
        {
          this.CheckInitializedAndActive();
          if (this.isClientRequestBlocked)
            return;
          Intermech.Runtime.ComInterop.NativeMethods.CoSuspendClassObjects();
          this.isClientRequestBlocked = true;
        }
      }

      private sealed class ComClassData
      {
        public ComClassData(uint cookie, object comClassObject)
        {
          this.Cookie = cookie;
          this.ComClassObjectRef = new WeakReference(comClassObject);
        }

        public uint Cookie { get; private set; }

        public WeakReference ComClassObjectRef { get; private set; }
      }
    }
}
