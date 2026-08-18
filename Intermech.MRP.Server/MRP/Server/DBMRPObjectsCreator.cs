// Decompiled with JetBrains decompiler
// Type: Intermech.MRP.Server.DBMRPObjectsCreator
// Assembly: Intermech.MRP.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 90CF20BA-CEDA-4320-95C8-661A6AE661C2
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.MRP.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.MRP.Server;

internal class DBMRPObjectsCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    switch (guid.ToString())
    {
      case "cadd92e9-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBMRPProductionOrder((UserSession) uSession, objectParams);
      case "cadd9a5d-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBMRPProductionCopy((UserSession) uSession, objectParams);
      default:
        return (IDBObject) new DBObject((UserSession) uSession, objectParams);
    }
  }
}
