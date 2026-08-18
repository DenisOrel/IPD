// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.DBSelectionCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.NotifySamples;
using System;
using System.Data;


namespace Intermech.Kernel.Services;

public sealed class DBSelectionCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return guid.Equals(NotifySamplesConst.NotifySamplesTypeGuid) ? (IDBObject) new DBNotifySelection((UserSession) uSession, objectParams) : (IDBObject) new DBSelection((UserSession) uSession, objectParams);
  }
}
