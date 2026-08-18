// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ServerRemotingConfigurator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting;
using Intermech.Remoting.Optimized;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;


namespace Intermech.Kernel;

public sealed class ServerRemotingConfigurator
{
  private string originalFilename;
  private bool ensureSecurity;
  private string[] supportedChannels;
  private string imserverUri;

  public ServerRemotingConfigurator()
    : this(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false)
  {
  }

  public ServerRemotingConfigurator(string filename, bool ensureSecurity)
  {
    this.originalFilename = filename != null ? filename : throw new ArgumentNullException(nameof (filename));
    this.ensureSecurity = ensureSecurity;
    this.supportedChannels = new string[2]{ "tcp", "http" };
  }

  public void Configure()
  {
    this.ClearInternal();
    try
    {
      RemotingXmlDataHack configurationHack = new RemotingXmlDataHack(this.originalFilename);
      string serviceType = "Intermech.Server.IntermechServer, Intermech.Server";
      this.imserverUri = configurationHack.TryGetWellknownServiceUri(serviceType);
      ServerConsts.RemotingServerPort = configurationHack.Port;
      configurationHack.RemoveWellknownService(serviceType);
      foreach (string supportedChannel in this.supportedChannels)
      {
        if (configurationHack.HasChannelDefinition(supportedChannel))
        {
          configurationHack.ReplaceServerFormatter(supportedChannel, "binary", typeof (BinaryServerFormatterSinkPatcherProvider));
          configurationHack.ReplaceClientFormatter(supportedChannel, "binary", typeof (OptimizedBinaryClientFormatterSinkProvider));
        }
      }
      SessionGuardInstaller.Install(configurationHack);
      RemotingConfiguration.Configure(configurationHack.ToFile(), this.ensureSecurity);
    }
    catch
    {
      this.ClearInternal();
      throw;
    }
  }

  private void ClearInternal() => this.imserverUri = (string) null;

  public string IMServerUri
  {
    [DebuggerStepThrough] get => this.imserverUri;
  }
}
