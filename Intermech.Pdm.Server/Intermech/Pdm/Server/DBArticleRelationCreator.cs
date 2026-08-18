// Decompiled with JetBrains decompiler
// Type: Intermech.Pdm.Server.DBArticleRelationCreator
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Pdm.Server;

internal class DBArticleRelationCreator : IDBRelationCreator
{
  public IDBRelation CreateRelation(IUserSession uSession, Guid guid, DataTable relationParams)
  {
    return guid == new Guid("cad00023-306c-11d8-b4e9-00304f19f545") ? (IDBRelation) new DBArticleRelation((UserSession) uSession, relationParams) : (IDBRelation) null;
  }
}
