
// Type: Intermech.Runtime.ComInterop.LocalServer.DefaultHostApplication
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;


namespace Intermech.Runtime.ComInterop.LocalServer
{
    /// <summary>
    /// Реализация по умолчанию объекта для связи COM-сервера с приложением.
    /// </summary>
    public class DefaultHostApplication : IHostApplication
    {
      private Assembly entryAssembly;
      private Lazy<Guid> hostId;
      private Lazy<string> executablePath;

      /// <summary>Создает объект.</summary>
      public DefaultHostApplication()
        : this(DefaultHostApplication.GetEntryAssemblySafely())
      {
      }

      /// <summary>Создает объект.</summary>
      /// <param name="entryAssembly">Сборка с точкой входа приложения</param>
      /// <exception cref="T:System.ArgumentNullException">Параметр <paramref name="entryAssembly" /> не должен быть равен null</exception>
      public DefaultHostApplication(Assembly entryAssembly)
      {
        if (entryAssembly == (Assembly) null)
          throw new ArgumentNullException(nameof (entryAssembly));
        this.entryAssembly = !string.IsNullOrEmpty(entryAssembly.Location) ? entryAssembly : throw new ArgumentException("The Location property of entry assembly is empty.", "entryAssembly.");
        this.hostId = new Lazy<Guid>(new Func<Guid>(this.DoGetHostId));
        this.executablePath = new Lazy<string>(new Func<string>(this.DoGetExecutablePath));
      }

      private static Assembly GetEntryAssemblySafely()
      {
        Assembly entryAssembly = Assembly.GetEntryAssembly();
        return entryAssembly != (Assembly) null && !string.IsNullOrEmpty(entryAssembly.Location) ? entryAssembly : throw new ComServerException(ComServerResources.SR_UnsupportedHostApplicationType);
      }

      private Guid DoGetHostId()
      {
        GuidAttribute[] customAttributes = (GuidAttribute[]) this.entryAssembly.GetCustomAttributes(typeof (GuidAttribute), false);
        return customAttributes.Length == 1 ? new Guid(customAttributes[0].Value) : throw new ComServerException(ComServerResources.SR_HostApplicationAssemblyGuidRequired);
      }

      private string DoGetExecutablePath() => this.entryAssembly.Location;

      private ICollection<string> DoGetCommandLineArguments()
      {
        return (ICollection<string>) Environment.GetCommandLineArgs();
      }

      /// <summary>
      /// Возвращает идентификатор приложения COM-сервера, в качестве которого используется Guid сборки с точкой входа приложения.
      /// </summary>
      public Guid HostId
      {
        [DebuggerStepThrough] get => this.hostId.Value;
      }

      /// <summary>
      /// Возвращает путь к исполняемому файлу приложения COM-сервера в абсолютной форме, в качестве которого используется путь сборки с точкой входа приложения.
      /// </summary>
      public string ExecutablePath
      {
        [DebuggerStepThrough] get => this.executablePath.Value;
      }

      /// <summary>
      /// Возвращает коллекцию аргументов запуска приложения COM-сервера.
      /// </summary>
      public ICollection<string> GetCommandLineArguments() => this.DoGetCommandLineArguments();
    }
}
