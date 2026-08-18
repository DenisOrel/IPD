// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTableReportCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBTableReportCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return guid == new Guid("cad00289-306c-11d8-b4e9-00304f19f545") || guid == new Guid("cad0028a-306c-11d8-b4e9-00304f19f545") ? (IDBObject) new DBTableReport(uSession as UserSession, objectParams) : (IDBObject) null;
  }
}
