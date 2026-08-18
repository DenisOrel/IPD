// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Server.ArchiveDBObjectCreator
// Assembly: Intermech.Archives.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2799C6CB-9B1D-4DB5-A12D-8C5FBFCAD6E5
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Archives.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;

#nullable disable
namespace Intermech.Archives.Server;

internal class ArchiveDBObjectCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return (IDBObject) new ArchiveDBObject(uSession, objectParams);
  }
}
