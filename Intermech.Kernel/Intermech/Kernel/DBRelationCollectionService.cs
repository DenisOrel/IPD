// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationCollectionService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel;

public class DBRelationCollectionService : CreatorContainer, IDBRelationCollectionService
{
  public IDBRelationCollection GetRelationCollection(IUserSession uSession, int relationType)
  {
    if (relationType > -1)
    {
      Guid guid = (uSession.GetRelationType(relationType) as IDBGuid).GUID;
      if (this.GetCreator((object) guid) is IDBRelationCollectionCreator creator)
        return creator.CreateRelationCollection(uSession, guid, relationType);
    }
    return (IDBRelationCollection) new DBRelationCollection(uSession as UserSession, relationType, "cad001e2-306c-11d8-b4e9-00304f19f545");
  }

  public IDBRelationCollection GetRelationCollection(
    IUserSession uSession,
    int relationType,
    string FiltrationOwnerID)
  {
    if (relationType > -1)
    {
      Guid guid = (uSession.GetRelationType(relationType) as IDBGuid).GUID;
      if (this.GetCreator((object) guid) is IDBRelationCollectionCreator creator)
      {
        IDBRelationCollection relationCollection1 = creator.CreateRelationCollection(uSession, guid, relationType);
        if (!(relationCollection1 is DBRelationCollection relationCollection2))
          return relationCollection1;
        relationCollection2.FiltrationOwnerID = FiltrationOwnerID;
        return relationCollection1;
      }
    }
    return (IDBRelationCollection) new DBRelationCollection(uSession as UserSession, relationType, FiltrationOwnerID);
  }

  public IDBRelationCollection GetRelationCollection(
    IUserSession uSession,
    int relationType,
    VersionsRule rule)
  {
    if (relationType > -1)
    {
      Guid guid = (uSession.GetRelationType(relationType) as IDBGuid).GUID;
      if (this.GetCreator((object) guid) is IDBRelationCollectionCreator creator)
      {
        IDBRelationCollection relationCollection1 = creator.CreateRelationCollection(uSession, guid, relationType);
        if (!(relationCollection1 is DBRelationCollection relationCollection2))
          return relationCollection1;
        relationCollection2.FiltrationRule = rule;
        return relationCollection1;
      }
    }
    return (IDBRelationCollection) new DBRelationCollection(uSession as UserSession, relationType)
    {
      FiltrationRule = rule
    };
  }
}
