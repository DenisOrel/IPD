// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.Service.IntermechConnectionBrokerService
// Assembly: Intermech.ConnectionBroker.Service, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D4CD0278-1F75-45CE-84EB-6440D3E7C8F8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Service.exe

using Intermech.ApplicationModel;
using Intermech.Diagnostics;
using System;
using System.ComponentModel;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.ConnectionBroker.Service;

internal class IntermechConnectionBrokerService : ServiceInstanceBase
{
  private ConnectionBrokerServer brokerServer;
  private IPSFatalExceptionLogger fatalExceptionHandler;
  private string brokerUri;
  private IContainer components;

  public IntermechConnectionBrokerService() => this.InitializeComponent();

  protected override IEventLogWriter CreateFileEventLogWriter()
  {
    return (IEventLogWriter) ApplicationEventLogWriters.CreateTextFileWriter("Intermech.ConnectionBroker.Service.log");
  }

  protected override bool DoStartService()
  {
    if (!base.DoStartService())
      return false;
    this.InitializeExceptionHandlers();
    this.brokerServer = new ConnectionBrokerServer(this.EventLogService);
    if (!this.TryStartRemoting())
      return false;
    RemotingServices.Marshal((MarshalByRefObject) this.brokerServer, this.brokerUri);
    if (!this.brokerServer.Initialize())
      return false;
    this.brokerServer.Run();
    return true;
  }

  protected override void DoStopService(bool errorMode)
  {
    RemotingServices.Disconnect((MarshalByRefObject) this.brokerServer);
    this.brokerServer = (ConnectionBrokerServer) null;
    this.RemoveExceptionHandlers();
    base.DoStopService(errorMode);
  }

  private bool TryStartRemoting()
  {
    BrokerRemotingConfigurator remotingConfigurator = new BrokerRemotingConfigurator();
    remotingConfigurator.Configure();
    this.brokerUri = remotingConfigurator.IMServerUri;
    return true;
  }

  private void InitializeExceptionHandlers()
  {
    ExceptionServices.StackTraceBuilderFactory = (Func<StackTraceBuilder>) (() => (StackTraceBuilder) new IPSStackTraceBuilder());
    this.fatalExceptionHandler = new IPSFatalExceptionLogger(this.EventLogService.AllLogs);
    this.fatalExceptionHandler.Activate();
  }

  private void RemoveExceptionHandlers()
  {
    if (this.fatalExceptionHandler == null)
      return;
    this.fatalExceptionHandler.Deactivate();
    this.fatalExceptionHandler = (IPSFatalExceptionLogger) null;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.CanPauseAndContinue = false;
    this.CanShutdown = true;
    this.CanHandlePowerEvent = true;
    this.ServiceName = "Брокер подключений IPS";
    this.AutoLog = false;
  }
}
