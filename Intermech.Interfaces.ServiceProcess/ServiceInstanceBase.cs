// Decompiled with JetBrains decompiler
// Type: Intermech.ApplicationModel.ServiceInstanceBase
// Assembly: Intermech.Interfaces.ServiceProcess, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B7815DB0-27BA-4236-9871-0983141542BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.ServiceProcess.dll

using Intermech.Configuration;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Runtime;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;

#nullable disable
namespace Intermech.ApplicationModel;

public class ServiceInstanceBase : ServiceBase
{
  private bool isCoreServicesInitialized;
  private bool isStarted;
  private SilentActionInvoker silentActions;
  private EventLogWriterSyncWrapper fileEventLogWriter;
  private EventLogWriterSyncWrapper systemEventLogWriter;
  private IApplicationEventLogService eventLogService;
  private IAlertMessageService alertService;
  private IExceptionDisplayService exceptionService;
  private IContainer components;

  public ServiceInstanceBase()
  {
    this.InitializeComponent();
    this.silentActions = SilentActionInvoker.Default;
  }

  private void CreateCoreServices()
  {
    this.CreateEventLogService();
    this.CreateAlertMessageService();
    this.CreateExceptionService();
  }

  private void RemoveCoreServices()
  {
    this.RemoveExceptionService();
    this.RemoveAlertMessageService();
    this.RemoveEventLogService();
  }

  protected IApplicationEventLogService EventLogService
  {
    [DebuggerStepThrough] get => this.eventLogService;
  }

  protected IAlertMessageService AlertMessageService
  {
    [DebuggerStepThrough] get => this.alertService;
  }

  protected IExceptionDisplayService ExceptionDisplayService
  {
    [DebuggerStepThrough] get => this.exceptionService;
  }

  protected sealed override void OnStart(string[] args)
  {
    if (!this.isCoreServicesInitialized)
    {
      this.CreateCoreServices();
      this.isCoreServicesInitialized = true;
    }
    base.OnStart(args);
    this.LogServiceStarting();
    this.isStarted = true;
    try
    {
      if (this.DoStartService())
      {
        this.LogServiceStarted();
      }
      else
      {
        this.InvokeSilently((Action) (() => this.DoStopService(true)), "DoStopService(true)");
        this.isStarted = false;
        this.Stop();
      }
    }
    catch (Exception ex)
    {
      this.InvokeSilently((Action) (() => this.DoReportUnhandledException(ex)), "DoReportUnhandledException(exception)");
      this.InvokeSilently((Action) (() => this.DoStopService(true)), "DoStopService(true)");
      this.isStarted = false;
      this.Stop();
    }
  }

  protected sealed override void OnStop()
  {
    if (this.isStarted)
    {
      this.InvokeSilently((Action) (() => this.DoStopService(false)), "DoStopService(false)");
      this.isStarted = false;
    }
    base.OnStop();
    this.LogServiceStopped();
  }

  protected sealed override void OnShutdown()
  {
    this.Stop();
    base.OnShutdown();
  }

  protected virtual bool DoStartService() => true;

  protected virtual void DoStopService(bool errorMode)
  {
  }

  protected virtual void DoReportUnhandledException(Exception exception)
  {
    if (this.exceptionService == null)
      return;
    this.exceptionService.ShowException(exception);
  }

  private void LogServiceStarting()
  {
    string location = this.GetType().Assembly.Location;
    StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
    stringBuilder.AppendLine($"Служба '{this.ServiceName}' приступила к запуску.");
    stringBuilder.AppendLine($"Исполняемый файл: {location}");
    if (File.Exists(location))
    {
      FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(location);
      stringBuilder.AppendLine($"Версия: {versionInfo.FileVersion}");
    }
    this.eventLogService.AllLogs.Write(stringBuilder.ToString());
  }

  private void LogServiceStarted()
  {
    this.eventLogService.AllLogs.Write("Служба успешно запушена.");
  }

  private void LogServiceStopped()
  {
    if (this.eventLogService == null)
      return;
    this.eventLogService.AllLogs.Write("Служба остановлена.");
  }

  [Conditional("DEBUG")]
  private void WaitForDebugger()
  {
    if (!AppSettingsHelper.GetBoolean(nameof (WaitForDebugger), false))
      return;
    int num1 = 30;
    int num2 = num1 * 1000;
    int millisecondsTimeout = 300;
    int num3 = millisecondsTimeout;
    int num4 = num2 / num3;
    this.eventLogService.FileLog.Write($"Ожидание подключения отладчика в течение {num1}с ...");
    while (!Debugger.IsAttached && num4 > 0)
    {
      --num4;
      Thread.Sleep(millisecondsTimeout);
    }
    this.eventLogService.FileLog.Write("Ожидание подключения отладчика завершено.");
  }

  protected void InvokeSilently(Action action, string exceptionLocation = null)
  {
    this.silentActions.Invoke(action, exceptionLocation);
  }

  private void CreateEventLogService()
  {
    this.fileEventLogWriter = EventLogWriters.Synchronized(this.CreateFileEventLogWriter());
    this.systemEventLogWriter = EventLogWriters.Synchronized(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, this.ServiceName));
    this.eventLogService = (IApplicationEventLogService) new ApplicationEventLogService((IEventLogWriter) this.fileEventLogWriter, (IEventLogWriter) this.systemEventLogWriter, (IEventLogWriter) this.fileEventLogWriter);
    ApplicationServices.Container.AddService(typeof (IApplicationEventLogService), (object) this.eventLogService);
  }

  private void RemoveEventLogService()
  {
    if (this.eventLogService != null)
    {
      ApplicationServices.Container.RemoveService(typeof (IApplicationEventLogService));
      this.eventLogService = (IApplicationEventLogService) null;
    }
    if (this.systemEventLogWriter != null)
      this.systemEventLogWriter = (EventLogWriterSyncWrapper) null;
    if (this.fileEventLogWriter == null)
      return;
    DisposeUtils.TryDispose((object) this.fileEventLogWriter.Unwrap());
    this.fileEventLogWriter = (EventLogWriterSyncWrapper) null;
  }

  protected virtual IEventLogWriter CreateFileEventLogWriter() => EventLogWriters.Null;

  private void CreateAlertMessageService()
  {
    this.alertService = (IAlertMessageService) new Intermech.ApplicationModel.AlertMessageService(this.eventLogService);
    ApplicationServices.Container.AddService(typeof (IAlertMessageService), (object) this.alertService);
  }

  private void RemoveAlertMessageService()
  {
    if (this.alertService == null)
      return;
    ApplicationServices.Container.RemoveService(typeof (IAlertMessageService));
    this.alertService = (IAlertMessageService) null;
  }

  private void CreateExceptionService()
  {
    this.exceptionService = (IExceptionDisplayService) new Intermech.ApplicationModel.ExceptionDisplayService(this.alertService);
    ApplicationServices.Container.AddService(typeof (IExceptionDisplayService), (object) this.exceptionService);
  }

  private void RemoveExceptionService()
  {
    if (this.exceptionService == null)
      return;
    ApplicationServices.Container.RemoveService(typeof (IExceptionDisplayService));
    this.exceptionService = (IExceptionDisplayService) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (this.isCoreServicesInitialized)
    {
      this.isCoreServicesInitialized = false;
      this.RemoveCoreServices();
    }
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.AutoLog = false;
    this.CanShutdown = true;
    this.CanPauseAndContinue = false;
    this.ServiceName = nameof (ServiceInstanceBase);
  }
}
