// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.wfRelationCreator
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Workflow.Server;

internal class wfRelationCreator : IDBRelationCreator
{
  public IDBRelation CreateRelation(IUserSession uSession, Guid guid, DataTable relationParams)
  {
    if (guid == wfConsts.AttachmentRelationGuid)
      return (IDBRelation) new DBAttachmentRelation((UserSession) uSession, relationParams);
    return guid == wfConsts.ScriptRelationGuid ? (IDBRelation) new DBScriptRelation((UserSession) uSession, relationParams) : (IDBRelation) null;
  }
}
