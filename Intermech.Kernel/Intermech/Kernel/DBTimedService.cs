// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTimedService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel;

public class DBTimedService : IDBTimedService, IDBGuid
{
  public IDBTimedEvents _TimedEventService;

  public IDBTimedEvents TimedEventService
  {
    get => this._TimedEventService;
    set => this._TimedEventService = value;
  }

  public virtual string ServiceName => nameof (DBTimedService);

  public virtual bool ProcessEvent(TimedEventProperties properties) => false;

  public virtual bool IsSystemGUID => true;

  public virtual Guid GUID => Guid.Empty;
}
