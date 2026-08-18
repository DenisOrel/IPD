// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.Services.GroupInstanceService
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Pdm;
using Intermech.Pdm.Server.Classes;
using System;

#nullable disable
namespace Intermech.Pdm.Server.Services;

internal sealed class GroupInstanceService : LongLifeObject, IGroupInstanceService
{
  private readonly IgnoredSessionsBag _disablePairedArticlesSwitch;

  public GroupInstanceService(IgnoredSessionsBag disablePairedArticlesSwitch)
  {
    this._disablePairedArticlesSwitch = disablePairedArticlesSwitch;
  }

  public void ArticleVersionCreated(
    IUserSession session,
    IDBObject dbObject,
    IDBObject parentObject)
  {
    new ArticleVersionProcess().Run(session, dbObject, parentObject);
  }

  public void AddIgnoreSessionGuid(Guid ignoreSessionGuid)
  {
    this._disablePairedArticlesSwitch.Add(ignoreSessionGuid);
  }

  public void RemoveIgnoreSessionGuid(Guid ignoreSessionGuid)
  {
    this._disablePairedArticlesSwitch.Remove(ignoreSessionGuid);
  }
}
