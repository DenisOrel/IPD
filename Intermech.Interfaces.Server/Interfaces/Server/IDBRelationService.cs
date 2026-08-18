// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Server.IDBRelationService
// Assembly: Intermech.Interfaces.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 25BF5CAD-94E4-401A-9DAC-C4D5AE12A515
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Interfaces.Server.dll

using System;
using System.Data;

#nullable disable
namespace Intermech.Interfaces.Server;

public interface IDBRelationService
{
  IDBRelation GetRelation(IUserSession uSession, long relationID, bool failIfNotFound);

  IDBRelation GetRelation(
    IUserSession uSession,
    Guid guid,
    long prjID,
    bool failIfNotFound,
    bool getActualCopy);

  IDBRelation GetRelation(IUserSession uSession, Guid guid, long prjID);

  IDBRelation GetRelation(
    IUserSession uSession,
    long projectID,
    long partID,
    int relationType,
    long partObjectID);

  IDBRelation GetRelation(IUserSession uSession, DataTable tbl, int index);

  IDBRelation[] GetRelations(IUserSession uSession, long[] relationIDs, bool failIfNotFound);
}
