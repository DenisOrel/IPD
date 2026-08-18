
// Type: Intermech.Remoting.Ipc.IpcConnector`1
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using Intermech.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Threading;


namespace Intermech.Remoting.Ipc
{
    /// <summary>
    /// Вспомогательный объект, который позволяет установить подключение к другому приложению с помощью .NET Remoting.
    /// </summary>
    /// <typeparam name="TApplicationObject">Интерфейс головного объекта приложения</typeparam>
    /// <remarks>Реализация не является thread safe.</remarks>
    public class IpcConnector<TApplicationObject> where TApplicationObject : class, IReliableIpcObject
    {
      private const int LaunchWaitStep = 50;
      private TApplicationObject cachedApplicationObject;
      private IpcConnectionInfo connectionInfo;
      private bool enableCommandLineSeparation;
      private string fullExecutablePath;
      private Uri applicationObjectLocalUri;

      /// <summary>
      /// Возвращает признак, что подключение к приложению выполнено.
      /// </summary>
      public bool IsConnected
      {
        [DebuggerStepThrough] get
        {
          return (object) this.cachedApplicationObject != null && this.TestConnection(this.cachedApplicationObject);
        }
      }

      /// <summary>
      /// Возвращает или задает параметры подключения к приложению.
      /// Значение свойства должно быть задано до вызова метода <see cref="M:Intermech.Remoting.Ipc.IpcConnector`1.GetOrConnect" />.
      /// </summary>
      public IpcConnectionInfo ConnectionInfo
      {
        [DebuggerStepThrough] get => this.connectionInfo;
        set
        {
          if (this.connectionInfo == value)
            return;
          this.connectionInfo = value;
          this.ResetCacheOnPropertyChange();
        }
      }

      /// <summary>
      /// Включает и выключает фильтрацию по аргументам командной строки при поиске уже запущенного приложения.
      /// По умолчанию фильтрация выключена.
      /// </summary>
      public bool EnableCommandLineSeparation
      {
        [DebuggerStepThrough] get => this.enableCommandLineSeparation;
        set
        {
          if (this.enableCommandLineSeparation == value)
            return;
          this.enableCommandLineSeparation = value;
          this.ResetCacheOnPropertyChange();
        }
      }

      /// <summary>
      /// Очищает кэшированное внутреннее состояние текущего объекта.
      /// Метод вызывается при изменении значений свойств текущего объекта.
      /// </summary>
      private void ResetCacheOnPropertyChange()
      {
        this.fullExecutablePath = (string) null;
        this.applicationObjectLocalUri = (Uri) null;
        this.ResetConnectionCache();
      }

      /// <summary>
      /// Возвращает объект подключенного приложения.
      /// При необходимости, этот метод выполняет запуск приложения и установление нового подключения к нему.
      /// </summary>
      /// <returns>Объект подключенного приложения</returns>
      /// <exception cref="T:System.Exception">Не удалось установить подключение к приложению</exception>
      public TApplicationObject GetOrConnect()
      {
        if ((object) this.cachedApplicationObject != null)
        {
          if (this.TestConnection(this.cachedApplicationObject))
            return this.cachedApplicationObject;
          this.ResetConnectionCache();
        }
        this.cachedApplicationObject = this.Connect();
        return this.cachedApplicationObject;
      }

      private TApplicationObject Connect()
      {
        this.ValidateProperties();
        if (this.fullExecutablePath == null)
          this.fullExecutablePath = Path.GetFullPath(this.connectionInfo.ExecutablePath);
        if (this.applicationObjectLocalUri == (Uri) null)
          this.applicationObjectLocalUri = new Uri(this.connectionInfo.ApplicationObjectUri, UriKind.Relative);
        Process applicationProcess = (this.enableCommandLineSeparation ? this.FindExistingApplicationByCommandLine(this.fullExecutablePath) : this.FindExistingApplicationByExecutablePath(this.fullExecutablePath)) ?? this.LaunchNewApplication(this.fullExecutablePath);
        TApplicationObject applicationObject = (TApplicationObject) RemotingServices.Connect(typeof (TApplicationObject), new Uri(new Uri($"ipc://{applicationProcess.Id.ToString()}"), this.applicationObjectLocalUri).AbsoluteUri);
        this.WaitForConnection(applicationObject, applicationProcess);
        return applicationObject;
      }

      /// <summary>Проверяет корректность свойств текущего объекта.</summary>
      /// <exception cref="T:System.Exception">Одно или более свойство не задано или содержит некорректные значения</exception>
      private void ValidateProperties()
      {
        if (this.ConnectionInfo == null)
          throw new Exception($"Не заданы параметры подключения к другому приложению. Заполните свойство {"ConnectionInfo"}.");
      }

      /// <summary>
      /// Находит и возвращает уже работающий экземпляр приложения.
      /// Для поиска используется только путь к файлу приложения.
      /// </summary>
      /// <param name="executablePath">Абсолютный путь к файлу приложения</param>
      /// <returns>Процесс приложения</returns>
      private Process FindExistingApplicationByExecutablePath(string executablePath)
      {
        List<Process> processList = new List<Process>((IEnumerable<Process>) Intermech.Diagnostics.ProcessManager.GetProcessesByName(Path.GetFileNameWithoutExtension(executablePath)));
        processList.RemoveAll((Predicate<Process>) (x => !string.Equals(this.TryGetMainModulePath(x), executablePath, StringComparison.CurrentCultureIgnoreCase)));
        return processList.Count != 0 ? processList[0] : (Process) null;
      }

      /// <summary>
      /// Находит и возвращает уже работающий экземпляр приложения.
      /// Для поиска используется путь к файлу приложения и командная строка приложения.
      /// </summary>
      /// <param name="executablePath">Абсолютный путь к файлу приложения</param>
      /// <returns>Процесс приложения</returns>
      private Process FindExistingApplicationByCommandLine(string executablePath)
      {
        IpcConnectorProcessTable processTable = IpcConnectorContext.ProcessTable;
        foreach (int processId in processTable.FindByCommandLine(executablePath, this.connectionInfo.CommandLineArgs))
        {
          Process processById = this.TryGetProcessById(processId);
          if (processById != null && PathUtils.IsSamePath(this.TryGetMainModulePath(processById), executablePath) && !processById.HasExited)
            return processById;
          processTable.Unregister(processId);
        }
        return (Process) null;
      }

      private Process TryGetProcessById(int processId)
      {
        try
        {
          return Process.GetProcessById(processId);
        }
        catch
        {
          return (Process) null;
        }
      }

      /// <summary>Запускает новый экземпляр приложения.</summary>
      /// <param name="executablePath">Абсолютный путь к файлу приложения</param>
      /// <returns>Процесс приложения</returns>
      /// <exception cref="T:System.Exception">При запуске приложения произошла ошибка</exception>
      private Process LaunchNewApplication(string executablePath)
      {
        if (!File.Exists(executablePath))
          throw new Exception(string.Format("Не удалось найти исполняемый файл '{1}' приложения '{0}'.", (object) this.connectionInfo.ApplicationName, (object) executablePath));
        Process process = Process.Start(executablePath, this.connectionInfo.CommandLineArgs);
        if (process.WaitForExit(50) || process.HasExited)
          throw new Exception($"Не удалось запустить приложение '{this.connectionInfo.ApplicationName}'.");
        IpcConnectorContext.ProcessTable.Register(process.Id, executablePath, this.connectionInfo.CommandLineArgs);
        return process;
      }

      private void WaitForConnection(TApplicationObject applicationObject, Process applicationProcess)
      {
        int launchWaitTime = this.connectionInfo.LaunchWaitTime;
        int millisecondsTimeout = Math.Min(50, launchWaitTime);
        for (; launchWaitTime >= 0; launchWaitTime -= millisecondsTimeout)
        {
          if (this.TestConnection(applicationObject))
            return;
          if (applicationProcess.HasExited)
            throw new Exception($"Не удалось подключиться к приложению '{this.connectionInfo.ApplicationName}', так как оно неожиданно завершило выполнение.");
          Thread.Sleep(millisecondsTimeout);
        }
        throw new Exception($"Не удалось подключиться к приложению '{this.connectionInfo.ApplicationName}', так как не удалось дождаться готовности приложения.");
      }

      /// <summary>Проверяет работоспособность подключения к приложению.</summary>
      /// <param name="applicationObject">Объект приложения</param>
      /// <returns>Признак работоспособности подключения</returns>
      private bool TestConnection(TApplicationObject applicationObject)
      {
        try
        {
          applicationObject.KnockKnock();
          return true;
        }
        catch
        {
          return false;
        }
      }

      /// <summary>
      /// Очищает кэшированное внутреннее состояние, связанное с подключением к приложению.
      /// </summary>
      private void ResetConnectionCache()
      {
        this.cachedApplicationObject = default (TApplicationObject);
      }

      /// <summary>Пытается получить у процесса путь к основному модулю.</summary>
      /// <param name="process"></param>
      /// <returns>Путь к основному модулю процесса либо пустая строка в случае неудачи</returns>
      private string TryGetMainModulePath(Process process)
      {
        try
        {
          return process.MainModule.FileName;
        }
        catch (Win32Exception ex)
        {
          return "";
        }
      }
    }
}
