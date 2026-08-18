// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SessionGuardContextProperty
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting;
using System;
using System.Runtime.Remoting.Contexts;


namespace Intermech.Kernel;

[Serializable]
internal sealed class SessionGuardContextProperty : 
  IContextProperty,
  IDynamicProperty,
  IContributeDynamicSink
{
  private readonly string name;
  private readonly IRemotingObjectResolver mbrResolver;
  private volatile SessionGuardDynamicSink sink;

  public SessionGuardContextProperty(IRemotingObjectResolver mbrResolver)
  {
    this.name = "SessionGuard";
    this.mbrResolver = mbrResolver;
  }

  public string Name => this.name;

  public bool IsNewContextOK(Context newContext) => true;

  public void Freeze(Context newContext)
  {
  }

  public IDynamicMessageSink GetDynamicSink()
  {
    if (this.sink == null)
    {
      lock (this)
      {
        if (this.sink == null)
          this.sink = new SessionGuardDynamicSink(this.mbrResolver);
      }
    }
    return (IDynamicMessageSink) this.sink;
  }
}
