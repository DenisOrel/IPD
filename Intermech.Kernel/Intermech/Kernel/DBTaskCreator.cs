// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBTaskCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBTaskCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return guid.Equals(new Guid("cad0149e-306c-11d8-b4e9-00304f19f545")) ? (IDBObject) new DBTask(uSession as UserSession, objectParams) : (IDBObject) null;
  }
}
