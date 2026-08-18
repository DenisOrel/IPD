// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Classes.IgnoredSessionsBag
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Pdm.Server.Classes;

internal sealed class IgnoredSessionsBag
{
  private readonly List<Guid> sessionGuids;
  private readonly object syncRoot;

  public IgnoredSessionsBag()
  {
    this.sessionGuids = new List<Guid>();
    this.syncRoot = new object();
  }

  public void Add(Guid sessionGuid)
  {
    lock (this.syncRoot)
      this.sessionGuids.Add(sessionGuid);
  }

  public void Remove(Guid sessionGuid)
  {
    lock (this.syncRoot)
      this.sessionGuids.Remove(sessionGuid);
  }

  public bool Contains(Guid sessionGuid)
  {
    lock (this.syncRoot)
      return this.sessionGuids.Contains(sessionGuid);
  }
}
