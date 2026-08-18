// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.DBConfiguratorObjectsCreator
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal class DBConfiguratorObjectsCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return guid.ToString() == "cad015b0-306c-11d8-b4e9-00304f19f545" ? (IDBObject) new DBConfiguratorOption((UserSession) uSession, objectParams) : (IDBObject) new DBObject((UserSession) uSession, objectParams);
  }
}
