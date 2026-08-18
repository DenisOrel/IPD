// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectCollectionService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;


namespace Intermech.Kernel;

public class DBObjectCollectionService : CreatorContainer, IDBObjectCollectionService
{
  public IDBObjectCollection GetObjectCollection(IUserSession uSession, int objectType)
  {
    if (objectType > -1)
    {
      Guid objectTypeGuid = (uSession as UserSession).DBCache.GetObjectTypeGuid(objectType, true);
      IDBObjectCollectionCreator creator = this.GetCreator((object) objectTypeGuid) as IDBObjectCollectionCreator;
      int objectTypeID = objectType;
      for (; creator == null; creator = this.GetCreator((object) (uSession as UserSession).DBCache.GetObjectTypeGuid(objectTypeID, true)) as IDBObjectCollectionCreator)
      {
        objectTypeID = (uSession as UserSession).DBCache.GetObjectTypeParentID(objectTypeID);
        if (objectTypeID <= -1)
          break;
      }
      if (creator != null)
        return creator.CreateObjectCollection(uSession, objectTypeGuid, objectType);
    }
    return (IDBObjectCollection) new DBObjectCollection(uSession as UserSession, objectType);
  }
}
