// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Data.Repositories.RelationRepositoryServerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Search.Data.Repositories;

public sealed class RelationRepositoryServerService : IRelationRepositoryServerService
{
  public DataTable Select(Guid userSessionGuid, List<long> projectVersionIds)
  {
    if (userSessionGuid == Guid.Empty)
      throw new ArgumentException();
    if (projectVersionIds == null)
      throw new ArgumentNullException(nameof (projectVersionIds));
    return this.GetUserSession(userSessionGuid).DataManager.ExecuteDataTable($"select * from {"IMS_RELATIONS"} where F_PROJ_ID in ({string.Join<long>(", ", (IEnumerable<long>) projectVersionIds)})");
  }

  private UserSession GetUserSession(Guid userSessionGuid)
  {
    return UserSession.GetSessionByID(userSessionGuid) as UserSession;
  }
}
