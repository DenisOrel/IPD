// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.Service.ProjectInstaller
// Assembly: Intermech.ConnectionBroker.Service, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D4CD0278-1F75-45CE-84EB-6440D3E7C8F8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Service.exe

using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

#nullable disable
namespace Intermech.ConnectionBroker.Service;

[RunInstaller(true)]
public class ProjectInstaller : Installer
{
  private IContainer components;
  private ServiceProcessInstaller serviceProcessInstaller;
  private ServiceInstaller serviceInstaller;

  public ProjectInstaller()
  {
    this.InitializeComponent();
    this.serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.serviceProcessInstaller = new ServiceProcessInstaller();
    this.serviceInstaller = new ServiceInstaller();
    this.serviceProcessInstaller.Password = (string) null;
    this.serviceProcessInstaller.Username = (string) null;
    this.serviceInstaller.Description = "Обеспечивает балансировку нагрузки на серверы приложений IPS";
    this.serviceInstaller.DisplayName = "Брокер подключений IPS";
    this.serviceInstaller.ServiceName = "IPSbroker1";
    this.serviceInstaller.StartType = ServiceStartMode.Automatic;
    this.Installers.AddRange(new Installer[2]
    {
      (Installer) this.serviceProcessInstaller,
      (Installer) this.serviceInstaller
    });
  }
}
