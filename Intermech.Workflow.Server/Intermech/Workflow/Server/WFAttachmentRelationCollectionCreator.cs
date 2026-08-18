// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.WFAttachmentRelationCollectionCreator
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public class WFAttachmentRelationCollectionCreator : IDBRelationCollectionCreator
{
  public IDBRelationCollection CreateRelationCollection(
    IUserSession uSession,
    Guid guid,
    int relationTypeID)
  {
    return (IDBRelationCollection) new WFAttachmentRelationCollection((UserSession) uSession, relationTypeID);
  }
}
