
// Type: Intermech.Runtime.ComInterop.LocalServer.ComServerResources
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class ComServerResources
    {
      private static ResourceManager resourceMan;
      private static CultureInfo resourceCulture;

      internal ComServerResources()
      {
      }

      /// <summary>
      ///   Returns the cached ResourceManager instance used by this class.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static ResourceManager ResourceManager
      {
        get
        {
          if (ComServerResources.resourceMan == null)
            ComServerResources.resourceMan = new ResourceManager("Intermech.Runtime.ComInterop.LocalServer.ComServerResources", typeof (ComServerResources).Assembly);
          return ComServerResources.resourceMan;
        }
      }

      /// <summary>
      ///   Overrides the current thread's CurrentUICulture property for all
      ///   resource lookups using this strongly typed resource class.
      /// </summary>
      [EditorBrowsable(EditorBrowsableState.Advanced)]
      internal static CultureInfo Culture
      {
        get => ComServerResources.resourceCulture;
        set => ComServerResources.resourceCulture = value;
      }

      /// <summary>
      ///   Looks up a localized string similar to Путь к исполняемому файлу приложения COM-сервера '{0}' должен быть задан в абсолютной форме..
      /// </summary>
      internal static string Arg_HostApplicationExecutablePathMustBeAbsolute
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Arg_HostApplicationExecutablePathMustBeAbsolute), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не задан путь к исполняемому файлу приложения COM-сервера..
      /// </summary>
      internal static string Arg_HostApplicationExecutablePathRequired
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Arg_HostApplicationExecutablePathRequired), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не задан уникальный идентификатор приложения COM-сервера..
      /// </summary>
      internal static string Arg_HostApplicationIdRequired
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Arg_HostApplicationIdRequired), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Конфигурационный файл '{0}' не является валидным xml-файлом..
      /// </summary>
      internal static string SR_BadComXmlFileFormat
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_BadComXmlFileFormat), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось удалить из реестра путь к приложению COM-сервера..
      /// </summary>
      internal static string SR_CantClearLastRegisteredHostApplication
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_CantClearLastRegisteredHostApplication), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось создать раздел реестра '{0}'..
      /// </summary>
      internal static string SR_CantCreateRegistryKey
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_CantCreateRegistryKey), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось открыть раздел реестра '{0}'..
      /// </summary>
      internal static string SR_CantOpenRegistryKey
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_CantOpenRegistryKey), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось сохранить в реестре путь к приложению COM-сервера..
      /// </summary>
      internal static string SR_CantSaveLastRegisteredHostApplication
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_CantSaveLastRegisteredHostApplication), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Активация COM-класса '{0}', реализуемого типом '{1}', уже была выполнена..
      /// </summary>
      internal static string SR_ComClassIsAlreadyActivated
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_ComClassIsAlreadyActivated), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Невозможно создать экземпляр COM-объекта '{0}', реализуемого типом '{1}', так как его активация не была выполнена приложением. Возможно, соответствующий плагин приложения не был загружен. .
      /// </summary>
      internal static string SR_ComClassIsUnknown
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_ComClassIsUnknown), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось загрузить сборку '{0}' для обработки содержащихся в ней COM-классов..
      /// </summary>
      internal static string SR_ComPluginAssemblyLoadError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_ComPluginAssemblyLoadError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Инициализация COM-сервера уже была выполнена..
      /// </summary>
      internal static string SR_ComServerIsAlreadyInitialized
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_ComServerIsAlreadyInitialized), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Возможности приложения, предоставляемые с помощью COM, отключены..
      /// </summary>
      internal static string SR_ComServerIsDisabled
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_ComServerIsDisabled), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Инициализация COM-сервера не была выполнена..
      /// </summary>
      internal static string SR_ComServerIsNotInitialized
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_ComServerIsNotInitialized), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Активация и деактивация COM-классов разрешены только в том потоке, который использовался для создания объекта '{0}'..
      /// </summary>
      internal static string SR_CreationThreadRequired
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_CreationThreadRequired), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to .NET-сборка приложения должна содержать атрибут типа GuidAttribute..
      /// </summary>
      internal static string SR_HostApplicationAssemblyGuidRequired
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_HostApplicationAssemblyGuidRequired), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Приложение завершает работу и не может обработать запрос..
      /// </summary>
      internal static string SR_HostApplicationIsExiting
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_HostApplicationIsExiting), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to В конфигурационном файле '{0}' не задано имя приложения COM-сервера. Поэтому .NET-сборка '{1}' не будет обработана, так как невозможно установить ее принадлежность к приложению..
      /// </summary>
      internal static string SR_HostApplicationNameIsNotSpecified
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_HostApplicationNameIsNotSpecified), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Трекер используемых COM-объектов уже был активирован..
      /// </summary>
      internal static string SR_LiveComObjectsTrackerIsAlreadyActive
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_LiveComObjectsTrackerIsAlreadyActive), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Трекер используемых COM-объектов не был активирован..
      /// </summary>
      internal static string SR_LiveComObjectsTrackerIsNotActive
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_LiveComObjectsTrackerIsNotActive), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось зарегистрировать COM-класс '{0}', реализуемый типом '{1}'..
      /// </summary>
      internal static string SR_RegisterComClassError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_RegisterComClassError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Регистрация COM-классов приложения выполнена с ошибками..
      /// </summary>
      internal static string SR_RegisterCommandError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_RegisterCommandError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Регистрация COM-классов приложения не может быть выполнена, так как поддержка COM отключена..
      /// </summary>
      internal static string SR_RegisterCommandRejected
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_RegisterCommandRejected), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось зарегистрировать библиотеку типов '{0}'..
      /// </summary>
      internal static string SR_RegisterTypeLibError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_RegisterTypeLibError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Для создания объекта '{0}' требуется поток с COM Apartment = STA..
      /// </summary>
      internal static string SR_STAThreadRequired
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_STAThreadRequired), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Указанный тип '{0}' не является COM-классом..
      /// </summary>
      internal static string SR_TypeIsNotComClass
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_TypeIsNotComClass), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM-класс использует неизвестную библиотеку типов с LIBID = {0} версии {1}..
      /// </summary>
      internal static string SR_UnknownTypeLibSpecified
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnknownTypeLibSpecified), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Для конфигурационного файла '{0}' не найдена соответствующая ему .NET-сборка..
      /// </summary>
      internal static string SR_UnlinkedComXmlFile
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnlinkedComXmlFile), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось отменить регистрацию COM-класса '{0}', реализуемого типом '{1}'..
      /// </summary>
      internal static string SR_UnregisterComClassError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnregisterComClassError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Отмена регистрации COM-классов приложения выполнена с ошибками..
      /// </summary>
      internal static string SR_UnregisterCommandError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnregisterCommandError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Отмена регистрации COM-классов приложения не может быть выполнена, так как приложение не было зарегистрировано как COM-сервер..
      /// </summary>
      internal static string SR_UnregisterCommandRejected
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnregisterCommandRejected), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Не удалось отменить регистрацию библиотеки типов '{0}'..
      /// </summary>
      internal static string SR_UnregisterTypeLibError
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnregisterTypeLibError), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to Неподдерживаемый тип приложения COM-сервера. Приложение должно быть исполняемым файлом..
      /// </summary>
      internal static string SR_UnsupportedHostApplicationType
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (SR_UnsupportedHostApplicationType), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: установлен обработчик для автоматического завершения работы приложения, запущенного по запросу COM-клиента.
      /// </summary>
      internal static string Trace_AutoExitHandlerIsInstalled
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_AutoExitHandlerIsInstalled), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: завершение работы приложения, запущенного по запросу COM-клиента.
      /// </summary>
      internal static string Trace_AutoExitIsInvoked
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_AutoExitIsInvoked), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: запланировано завершение работы приложения, так как все созданные COM-объекты освобождены.
      /// </summary>
      internal static string Trace_AutoExitIsRequested
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_AutoExitIsRequested), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: поддержка COM не может быть отключена, так как регистрация приложения не была отменена.
      /// </summary>
      internal static string Trace_CantDisableComServerUntilUnregisterCommand
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_CantDisableComServerUntilUnregisterCommand), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: создана фабрика объектов для COM-класса '{0}', реализуемого типом '{1}'.
      /// </summary>
      internal static string Trace_ComClassActivated
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_ComClassActivated), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: удалена фабрика объектов для COM-класса '{0}', реализуемого типом '{1}'.
      /// </summary>
      internal static string Trace_ComClassDeactivated
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_ComClassDeactivated), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: приложение успешно инициализировано и ожидает подключения клиентов....
      /// </summary>
      internal static string Trace_ComServerInitializedAndActive
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_ComServerInitializedAndActive), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: файл {0} обработан.
      /// </summary>
      internal static string Trace_FileProcessed
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_FileProcessed), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: теперь приложение контролирует {0} используемых COM-объектов.
      /// </summary>
      internal static string Trace_LiveComObjectsCountChanged
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_LiveComObjectsCountChanged), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: создан новый экземпляр COM-объекта, реализуемого типом '{0}'.
      /// </summary>
      internal static string Trace_NewObjectInstanceCreated
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_NewObjectInstanceCreated), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: выполняется регистрация COM-классов приложения.
      /// </summary>
      internal static string Trace_RegisterCommandStarted
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_RegisterCommandStarted), ComServerResources.resourceCulture);
        }
      }

      /// <summary>
      ///   Looks up a localized string similar to COM: выполняется отмена регистрации COM-классов приложения.
      /// </summary>
      internal static string Trace_UnregisterCommandStarted
      {
        get
        {
          return ComServerResources.ResourceManager.GetString(nameof (Trace_UnregisterCommandStarted), ComServerResources.resourceCulture);
        }
      }
    }
}
