
// Type: Intermech.Scripting.CSharp.IpcClientProcessesManager
// Assembly: Intermech.Bcl, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5BB0E990-612C-4C26-901F-7AFCB5D0C798
:\IPS\Client\PdfPrintCenter\Intermech.Bcl.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.xml

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Scripting.CSharp
{
    /// <summary>
    /// Менеджер подключенных по ipc-каналу клиентских процессов.
    /// Он используется для автоматического завершения серверного процесса, если не осталось выполняющихся клиентских процессов.
    /// Реализация является thread safe.
    /// </summary>
    public sealed class IpcClientProcessesManager
    {
      private ConcurrentDictionary<int, Process> processTable;

      /// <summary>Создает объект.</summary>
      public IpcClientProcessesManager()
      {
        this.processTable = new ConcurrentDictionary<int, Process>();
      }

      /// <summary>
      /// Регистрирует указанный процесс в качестве клиента.
      /// Метод может вызываться несколько раз для одного и того же процесса.
      /// </summary>
      /// <param name="pid">Идентификатор процесса</param>
      public void Register(int pid)
      {
        if (this.processTable.ContainsKey(pid))
          return;
        Process processById = this.TryGetProcessById(pid);
        if (processById == null)
          return;
        this.processTable.TryAdd(pid, processById);
      }

      private void UnregisterInternal(int pid)
      {
        Process process;
        if (!this.processTable.TryRemove(pid, out process))
          return;
        DisposeUtils.SafelyDispose((IDisposable) process);
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

      /// <summary>Проверяет наличие работающих клиентских процессов.</summary>
      /// <returns>Признак наличия работающих клиентских процессов</returns>
      public bool HasRunningProcesses()
      {
        int num = 0;
        List<int> intList = (List<int>) null;
        foreach (KeyValuePair<int, Process> keyValuePair in this.processTable)
        {
          int key = keyValuePair.Key;
          if (this.HasProcessExited(keyValuePair.Value))
          {
            if (intList == null)
              intList = new List<int>();
            intList.Add(key);
          }
          else
            ++num;
        }
        if (intList != null)
        {
          foreach (int pid in intList)
            this.UnregisterInternal(pid);
        }
        return num != 0;
      }

      private bool HasProcessExited(Process clientProcess)
      {
        try
        {
          return clientProcess.HasExited;
        }
        catch
        {
          return true;
        }
      }
    }
}
