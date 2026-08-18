// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SessionGuardInstaller
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Remoting;
using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Services;


namespace Intermech.Kernel;

public static class SessionGuardInstaller
{
  public static void Install(RemotingXmlDataHack configurationHack)
  {
    TrackerBasedObjectResolver basedObjectResolver = new TrackerBasedObjectResolver();
    TrackingServices.RegisterTrackingHandler((ITrackingHandler) basedObjectResolver);
    Context.RegisterDynamicProperty((IDynamicProperty) new SessionGuardContextProperty((IRemotingObjectResolver) basedObjectResolver), (ContextBoundObject) null, (Context) null);
  }
}
