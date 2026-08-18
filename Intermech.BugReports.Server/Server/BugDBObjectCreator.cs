// Decompiled with JetBrains decompiler
// Type: Intermech.BugReports.Server.BugDBObjectCreator
// Assembly: Intermech.BugReports.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D5496885-D5AE-45E1-887A-E42A46AB4DD0
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.BugReports.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;

#nullable disable
namespace Intermech.BugReports.Server;

internal class BugDBObjectCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return (IDBObject) new BugDBObject(uSession, objectParams);
  }
}
