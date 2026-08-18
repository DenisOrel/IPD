// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.Console.ConnectionBrokerConsole
// Assembly: Intermech.ConnectionBroker.Console, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A08DABC1-7E47-4A9D-942E-034F64136665
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Console.exe

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using Intermech.Globalization;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Remoting;
using System.Text;

#nullable disable
namespace Intermech.ConnectionBroker.Console;

internal sealed class ConnectionBrokerConsole : ConsoleApplicationBase
{
  private IConsoleService consoleService;
  private IConsoleCommandRegistry commandsRegistryService;
  private string brokerUri;
  private ConnectionBrokerServer brokerServer;
  private EventLogWriterSyncWrapper fileEventLogWriter;
  private EventLogWriterSyncWrapper systemEventLogWriter;
  private ApplicationEventLogService eventLogService;
  private IPSFatalExceptionLogger fatalExceptionHandler;

  private static void Main(string[] args) => new ConnectionBrokerConsole(args).Run();

  public ConnectionBrokerConsole(string[] arguments)
    : base(arguments)
  {
    this.consoleService = (IConsoleService) new ConsoleService();
  }

  protected override void DoRun()
  {
    base.DoRun();
    UICultureHelper.ApplySettingsFromConfigurationFile();
    this.CreateEventLogService();
    this.LogApplicationRunning();
    this.DisplayBanner();
    this.InitializeExceptionHandlers();
    this.commandsRegistryService = (IConsoleCommandRegistry) new ConsoleCommandRegistry();
    this.brokerServer = new ConnectionBrokerServer((IApplicationEventLogService) this.eventLogService);
    if (!this.TryStartRemoting())
      return;
    RemotingServices.Marshal((MarshalByRefObject) this.brokerServer, this.brokerUri);
    if (this.brokerServer.Initialize())
    {
      this.brokerServer.Run();
      this.commandsRegistryService.Add(new ConsoleCommandInfo("servers", string.Empty, "Список зарегистрированных серверов приложений IPS", new ConsoleCommandMethod(this.ServersCommand)));
      this.commandsRegistryService.Add(new ConsoleCommandInfo("ping", string.Empty, "Принудительно опросить сервера приложений IPS", new ConsoleCommandMethod(this.PingCommand)));
      this.RunCommandLoop();
    }
    else
    {
      this.consoleService.WriteLine("Инициализация приложения завершилась с ошибками. См. файл ConnectionBroker.Console.log.", ConsoleColor.Red);
      this.consoleService.WriteLine("Нажмите Enter для завершения приложения.", ConsoleColor.Red);
      this.consoleService.ReadLine();
      throw new AbortException();
    }
  }

  private void CreateEventLogService()
  {
    this.fileEventLogWriter = EventLogWriters.Synchronized((IEventLogWriter) ApplicationEventLogWriters.CreateTextFileWriter("ConnectionBroker.Console.log"));
    this.systemEventLogWriter = EventLogWriters.Synchronized(EventLogWriters.CreateSystemLogWriter(SystemEventLogType.Application, "Брокер подключений IPS"));
    this.eventLogService = new ApplicationEventLogService((IEventLogWriter) this.fileEventLogWriter, (IEventLogWriter) this.systemEventLogWriter, (IEventLogWriter) this.fileEventLogWriter);
  }

  private void RemoveEventLogService()
  {
    if (this.eventLogService != null)
      this.eventLogService = (ApplicationEventLogService) null;
    if (this.systemEventLogWriter != null)
      this.systemEventLogWriter = (EventLogWriterSyncWrapper) null;
    if (this.fileEventLogWriter == null)
      return;
    ((TextFileEventLogWriter) this.fileEventLogWriter.Unwrap()).Dispose();
    this.fileEventLogWriter = (EventLogWriterSyncWrapper) null;
  }

  private void InitializeExceptionHandlers()
  {
    ExceptionServices.StackTraceBuilderFactory = (Func<StackTraceBuilder>) (() => (StackTraceBuilder) new IPSStackTraceBuilder());
    this.fatalExceptionHandler = new IPSFatalExceptionLogger(this.eventLogService.AllLogs);
    this.fatalExceptionHandler.Activate();
  }

  private void RemoveExceptionHandlers()
  {
    if (this.fatalExceptionHandler == null)
      return;
    this.fatalExceptionHandler.Deactivate();
    this.fatalExceptionHandler = (IPSFatalExceptionLogger) null;
  }

  private void RunCommandLoop()
  {
    new ConsoleCommandDispatcher(this.consoleService, this.commandsRegistryService).Run();
  }

  protected override void DoCleanup(bool errorMode)
  {
    this.brokerServer.Close();
    this.brokerServer = (ConnectionBrokerServer) null;
    this.LogApplicationStopped();
    this.RemoveExceptionHandlers();
    this.RemoveEventLogService();
    base.DoCleanup(errorMode);
  }

  private bool TryStartRemoting()
  {
    try
    {
      BrokerRemotingConfigurator remotingConfigurator = new BrokerRemotingConfigurator();
      remotingConfigurator.Configure();
      this.brokerUri = remotingConfigurator.IMServerUri;
      return true;
    }
    catch (Exception ex)
    {
      string text = ex.Message;
      int length = text.IndexOf("\r\n");
      if (length > 0)
        text = text.Substring(0, length);
      this.consoleService.WriteLine("Во время инициализации системы удаленного доступа произошла ошибка.", ConsoleColor.Yellow);
      this.consoleService.WriteLine("Возможно, что занят порт или уже запущен другой экземпляр программы, который этот порт использует.", ConsoleColor.Yellow);
      this.consoleService.WriteLine(string.Empty);
      this.consoleService.WriteLine("Текст ошибки:", ConsoleColor.Red);
      this.consoleService.WriteLine(text, ConsoleColor.Red);
      this.consoleService.WriteLine(string.Empty);
      this.consoleService.WriteLine("Нажмите enter для завершения работы приложения.");
      this.consoleService.ReadLine();
      return false;
    }
  }

  private void DisplayBanner()
  {
    try
    {
      object[] customAttributes1 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyVersionString), true);
      object[] customAttributes2 = typeof (XMLSettingsStorage).Assembly.GetCustomAttributes(typeof (AssemblyBuildDate), true);
      AssemblyVersionString assemblyVersionString = customAttributes1 == null || customAttributes1.Length == 0 ? (AssemblyVersionString) null : customAttributes1[0] as AssemblyVersionString;
      AssemblyBuildDate assemblyBuildDate = customAttributes2 == null || customAttributes2.Length == 0 ? (AssemblyBuildDate) null : customAttributes2[0] as AssemblyBuildDate;
      if (assemblyVersionString == null || assemblyBuildDate == null)
        return;
      string[] strArray = assemblyVersionString.Description.Split('.');
      string str = strArray == null || strArray.Length != 4 || DataSetProcessor.GetInt64Value((object) strArray[2], 0L) <= 0L ? string.Empty : $"Service Pack {strArray[2]} ";
      this.consoleService.WriteLine(string.Format("IPS Connection Broker v{0} {3}({1}){4}Copyright (c) 2003-{2} Intermech\n", (object) assemblyVersionString.Description, (object) assemblyBuildDate.Description, (object) assemblyBuildDate.AssemblyBuildYear, (object) str, !string.IsNullOrEmpty(str) ? (object) "\n" : (object) " "), ConsoleColor.White);
    }
    catch
    {
    }
  }

  private void LogApplicationRunning()
  {
    string location = this.GetType().Assembly.Location;
    StringBuilder stringBuilder = new StringBuilder(512 /*0x0200*/);
    stringBuilder.AppendLine($"{"Брокер подключений IPS"} запущен.");
    stringBuilder.AppendLine($"Исполняемый файл: {location}");
    if (File.Exists(location))
    {
      FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(location);
      stringBuilder.AppendLine($"Версия: {versionInfo.FileVersion}");
    }
    this.eventLogService.DefaultLog.Write(stringBuilder.ToString());
  }

  private void LogApplicationStopped()
  {
    if (this.eventLogService == null)
      return;
    this.eventLogService.DefaultLog.Write($"{"Брокер подключений IPS"} завершил выполнение.");
  }

  private void ServersCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    foreach (string serversOutput in this.brokerServer.GetServersOutputList())
      System.Console.WriteLine(serversOutput);
  }

  private void PingCommand(IConsoleService consoleService, List<string> commandArgs)
  {
    this.brokerServer.InitForcePing();
  }
}
