using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Runtime;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Services
{
    /// <summary>
    /// Реализация сервиса приложения, предоставляющего доступ к лог-файлам сервера приложений.
    /// </summary>
    public sealed class ServerEventLogService : IServerEventLogService
    {
      private IMServerService imserverService;
      private IUserSessionLoginService userSessionLoginService;
      private string computerName;
      private ConcurrentQueue<AddToTraceRecord> eventQueue;
      private ManualResetEventSlim eventQueueUpdated;
      private Thread eventQueueWriter;

      /// <summary>Создает объект.</summary>
      /// <param name="imserverService">Сервис для доступа к основному объекту сервера приложений</param>
      /// <exception cref="T:ArgumentNullException">
      /// Параметр <paramref name="imserverService" /> не должен быть равен null;
      /// Параметр <paramref name="userSessionLoginService" /> не должен быть равен null.
      /// </exception>
      public ServerEventLogService(
        IMServerService imserverService,
        IUserSessionLoginService userSessionLoginService)
      {
        if (imserverService == null)
          throw new ArgumentNullException(nameof (imserverService));
        if (userSessionLoginService == null)
          throw new ArgumentNullException(nameof (userSessionLoginService));
        this.imserverService = imserverService;
        this.userSessionLoginService = userSessionLoginService;
        this.computerName = EnvironmentConsts.MachineName;
        this.eventQueue = new ConcurrentQueue<AddToTraceRecord>();
        this.eventQueueUpdated = new ManualResetEventSlim(false);
        this.eventQueueWriter = new Thread(new ThreadStart(this.EventQueueWriterBackgroundRoutine));
        this.eventQueueWriter.Name = nameof (ServerEventLogService);
        this.eventQueueWriter.IsBackground = true;
        this.eventQueueWriter.Start();
      }

      /// <summary>Записывает сообщение в лог-файл сервера приложений.</summary>
      /// <param name="text">Текст сообщения</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      public void AddToTrace(string text, string traceFileName = null)
      {
        this.AddToTrace(text, Consts.traceAlways, traceFileName);
      }

      /// <summary>Записывает сообщение в лог-файл сервера приложений.</summary>
      /// <param name="text">Текст сообщения</param>
      /// <param name="traceLevel">Уровень трассировки, при котором сообщение будет записано в файл</param>
      /// <param name="traceFileName">Имя файла трассировки</param>
      public void AddToTrace(string text, int traceLevel, string traceFileName = null)
      {
        if (string.IsNullOrEmpty(text))
          return;
        string userName = this.userSessionLoginService.GetLoginInfo().UserName;
        this.eventQueue.Enqueue(new AddToTraceRecord(text, traceLevel, traceFileName, this.computerName, userName));
        this.eventQueueUpdated.Set();
      }

      /// <summary>
      /// Фоновый поток для записи событий на сервер приложений.
      /// </summary>
      private void EventQueueWriterBackgroundRoutine()
      {
        try
        {
          List<AddToTraceRecord> eventRecordList = new List<AddToTraceRecord>(16 /*0x10*/);
          while (true)
          {
            this.eventQueueUpdated.Wait();
            this.eventQueueUpdated.Reset();
            Thread.Sleep(10);
            AddToTraceRecord result;
            while (this.eventQueue.TryDequeue(out result))
              eventRecordList.Add(result);
            while (!this.TryAddToTrace(eventRecordList))
              Thread.Sleep(30000);
            eventRecordList.Clear();
          }
        }
        catch (ThreadAbortException ex)
        {
          throw;
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (EventQueueWriterBackgroundRoutine));
          SuppressedExceptions.TraceException(ex, currentMethodName);
        }
      }

      private bool TryAddToTrace(List<AddToTraceRecord> eventRecordList)
      {
        if (!this.imserverService.IsConnected)
          return false;
        try
        {
          this.imserverService.ServerObject.AddToTrace((ICollection<AddToTraceRecord>) eventRecordList);
          return true;
        }
        catch (Exception ex)
        {
          string currentMethodName = this.GetCurrentMethodName(nameof (TryAddToTrace));
          SuppressedExceptions.TraceException(ex, currentMethodName);
          return false;
        }
      }
    }
}
