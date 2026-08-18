// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBArticleCollectionCreator
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Pdm.Server;

internal class DBArticleCollectionCreator : IDBObjectCollectionCreator
{
  public IDBObjectCollection CreateObjectCollection(
    IUserSession uSession,
    Guid guid,
    int objectTypeID)
  {
    return (IDBObjectCollection) new DBArticleCollection(uSession as UserSession, objectTypeID);
  }
}
