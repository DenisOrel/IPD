
// Type: Intermech.Remoting.Ipc.IpcConnectorProcessTable
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace Intermech.Remoting.Ipc
{
    /// <summary>
    /// Таблица процессов, запущенных на выполнение с помощью <see cref="T:Intermech.Remoting.Ipc.IpcConnector`1" />.
    /// Используется для хранение и последующего получения информации о процессах, которую нельзя получить
    /// стандартными средствами .NET
    /// </summary>
    /// <remarks>Реализация является thread safe.</remarks>
    internal sealed class IpcConnectorProcessTable
    {
      private ConcurrentDictionary<int, IpcProcessInfo> knownProcesses;

      /// <summary>Создает объект.</summary>
      public IpcConnectorProcessTable()
      {
        this.knownProcesses = new ConcurrentDictionary<int, IpcProcessInfo>();
      }

      /// <summary>Регистрирует новый запущенный процесс.</summary>
      /// <param name="processId">Идентификатор процесса</param>
      /// <param name="executablePath">Путь к исполняемому файлу процесса</param>
      /// <param name="commandLineArgs">Аргументы запуска процесса</param>
      public void Register(int processId, string executablePath, string commandLineArgs)
      {
            IpcProcessInfo newProcessInfo = new IpcProcessInfo(executablePath, commandLineArgs);
        this.knownProcesses.AddOrUpdate(processId, newProcessInfo, (Func<int, IpcProcessInfo, IpcProcessInfo>) ((existingProcessId, existingInfo) => newProcessInfo));
      }

      /// <summary>Отменяет регистрацию запущенного ранее процесса.</summary>
      /// <param name="processId">Идентификатор процесса</param>
      public void Unregister(int processId)
      {
        this.knownProcesses.TryRemove(processId, out IpcProcessInfo _);
      }

      /// <summary>
      /// Возвращает список идентификаторов запущенных ранее процессов по пути к исполняемому файлу и аргументам запуска.
      /// </summary>
      /// <param name="executablePath">Путь к исполняемому файлу процесса</param>
      /// <param name="commandLineArgs">Аргументы запуска процесса</param>
      /// <returns>Список идентификаторов процессов</returns>
      public List<int> FindByCommandLine(string executablePath, string commandLineArgs)
      {
        List<int> byCommandLine = new List<int>();
        foreach (KeyValuePair<int, IpcProcessInfo> knownProcess in this.knownProcesses)
        {
          int key = knownProcess.Key;
                IpcProcessInfo ipcProcessInfo = knownProcess.Value;
          if (string.Equals(ipcProcessInfo.ExecutablePath, executablePath, StringComparison.CurrentCultureIgnoreCase) && ipcProcessInfo.CommandLineArgs == commandLineArgs)
            byCommandLine.Add(key);
        }
        return byCommandLine;
      }

      private sealed class IpcProcessInfo
      {
        public IpcProcessInfo(string executablePath, string commandLineArgs)
        {
          this.ExecutablePath = executablePath;
          this.CommandLineArgs = commandLineArgs;
        }

        public string ExecutablePath { get; }

        public string CommandLineArgs { get; }
      }
    }
}
