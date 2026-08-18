// Decompiled with JetBrains decompiler
// Type: Intermech.ExternalSystemIntegration.Server.ResponceObjectCreator
// Assembly: Intermech.ExternalSystemIntegration.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: DA51A3A9-E549-4754-B561-351EB1444903
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ExternalSystemIntegration.Server.dll

using Intermech.ExternalSystemIntegration.Server.Settings;
using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.ExternalSystemIntegration.Server;

internal class ResponceObjectCreator : DBObjectCreator
{
  public override IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return (IDBObject) new ResponceObject(uSession as UserSession, objectParams);
  }
}
