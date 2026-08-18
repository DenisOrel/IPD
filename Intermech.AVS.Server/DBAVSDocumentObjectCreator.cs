// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.Server.DBAVSDocumentObjectCreator
// Assembly: Intermech.AVS.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DD9587A9-B8FC-4A8A-AB7E-E4D2C61BABE8
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AVS.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.AVS.Server;

public class DBAVSDocumentObjectCreator : DBObjectCreator
{
  public override IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return (IDBObject) new DBAVSDocumentObject(uSession as UserSession, objectParams);
  }
}
