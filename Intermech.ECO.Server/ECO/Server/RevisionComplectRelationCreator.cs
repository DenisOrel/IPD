// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevisionComplectRelationCreator
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

internal class RevisionComplectRelationCreator : IDBRelationCreator
{
  public IDBRelation CreateRelation(IUserSession uSession, Guid guid, DataTable relationParams)
  {
    IDBRelation relation = (IDBRelation) null;
    if (guid == RevisionComplect.RevisionComplectRelation_TypeGuid)
      relation = (IDBRelation) new RevisionComplectRelation((UserSession) uSession, relationParams);
    return relation;
  }
}
