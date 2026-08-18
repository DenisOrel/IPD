// Decompiled with JetBrains decompiler
// Type: Intermech.ConnectionBroker.BrokerRemotingConfigurator
// Assembly: Intermech.ConnectionBroker.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0BC7C3AD-D0E0-4C57-9DE7-799988ABDB14
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ConnectionBroker.Server.dll

using Intermech.Remoting;
using System;
using System.Diagnostics;
using System.Runtime.Remoting;

#nullable disable
namespace Intermech.ConnectionBroker;

public sealed class BrokerRemotingConfigurator
{
  private string originalFilename;
  private bool ensureSecurity;
  private string imserverUri;

  public BrokerRemotingConfigurator()
    : this(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false)
  {
  }

  public BrokerRemotingConfigurator(string filename, bool ensureSecurity)
  {
    this.originalFilename = filename != null ? filename : throw new ArgumentNullException(nameof (filename));
    this.ensureSecurity = ensureSecurity;
  }

  public void Configure()
  {
    this.ClearInternal();
    try
    {
      RemotingXmlDataHack remotingXmlDataHack = new RemotingXmlDataHack(this.originalFilename);
      string serviceType = "Intermech.ConnectionBroker.ConnectionBrokerServer, Intermech.Interfaces.ConnectionBroker.IConnectionBroker";
      this.imserverUri = remotingXmlDataHack.TryGetWellknownServiceUri(serviceType);
      remotingXmlDataHack.RemoveWellknownService(serviceType);
      RemotingConfiguration.Configure(remotingXmlDataHack.ToFile(), this.ensureSecurity);
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
