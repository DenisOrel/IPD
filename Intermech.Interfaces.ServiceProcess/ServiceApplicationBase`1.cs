// Decompiled with JetBrains decompiler
// Type: Intermech.ApplicationModel.ServiceApplicationBase`1
// Assembly: Intermech.Interfaces.ServiceProcess, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B7815DB0-27BA-4236-9871-0983141542BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.ServiceProcess.dll

using Intermech.Globalization;
using System;
using System.Collections.Generic;
using System.ServiceProcess;

#nullable disable
namespace Intermech.ApplicationModel;

public class ServiceApplicationBase<T>(string[] arguments) : ApplicationBase((IList<string>) arguments)
  where T : ServiceInstanceBase, new()
{
  protected override void DoRun()
  {
    base.DoRun();
    UICultureHelper.ApplySettingsFromConfigurationFile();
    this.InitializeCurrentDirectory();
    this.RunServices();
  }

  private void InitializeCurrentDirectory()
  {
    Environment.CurrentDirectory = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
  }

  private void RunServices()
  {
    ServiceBase.Run(new ServiceBase[1]
    {
      (ServiceBase) new T()
    });
  }
}
