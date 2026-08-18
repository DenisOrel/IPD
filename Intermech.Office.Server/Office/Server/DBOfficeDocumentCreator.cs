// Decompiled with JetBrains decompiler
// Type: Intermech.Office.Server.DBOfficeDocumentCreator
// Assembly: Intermech.Office.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 414402D9-801C-4C77-86BA-4C6FCAC834BE
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Office.Server.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Office.Server;

internal class DBOfficeDocumentCreator : IDBObjectCreator
{
  [NotNull]
  public IDBObject CreateObject([NotNull] IUserSession uSession, [NotEmpty] Guid guid, [NotNull] DataTable objectParams)
  {
    return (IDBObject) new DBOfficeDocument((UserSession) uSession, objectParams);
  }
}
