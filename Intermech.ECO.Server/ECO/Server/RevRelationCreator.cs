// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.RevRelationCreator
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

internal class RevRelationCreator : IDBRelationCreator
{
  public IDBRelation CreateRelation(IUserSession uSession, Guid guid, DataTable relationParams)
  {
    IDBRelation relation = (IDBRelation) null;
    if (guid.ToString() == "cad0036b-306c-11d8-b4e9-00304f19f545")
      relation = (IDBRelation) new RevRelation((UserSession) uSession, relationParams);
    return relation;
  }
}
