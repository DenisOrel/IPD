// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Server.DBProcessCollection
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;

#nullable disable
namespace Intermech.Workflow.Server;

public class DBProcessCollection(UserSession uSession, int objectType) : DBObjectCollection(uSession, objectType)
{
  protected override IDBObject CreateObject(
    long id,
    int objectType,
    IDBObject prototype,
    Guid versionGuid)
  {
    if (prototype is WFScheme wfScheme)
    {
      // ISSUE: explicit non-virtual call
      __nonvirtual (wfScheme.CheckAccess(ActionType.wfLaunchProcess));
    }
    IDBObject dbObject = base.CreateObject(id, objectType, prototype, versionGuid);
    if (objectType == wfConsts.SchemesTypeID)
    {
      IDBAttribute byId = dbObject.Attributes.FindByID(wfConsts.AttrIsDebugID);
      if (byId != null)
        byId.AsBoolean = true;
    }
    (dbObject as WFScheme).CopyFromPrototype(prototype);
    if (prototype != null)
    {
      IDBAttribute attributeById1 = prototype.GetAttributeByID(wfConsts.AttrNameID);
      IDBAttribute attributeById2 = dbObject.GetAttributeByID(wfConsts.AttrNameID);
      if (id == prototype.ID || dbObject is WFProcess)
      {
        if (attributeById1 != null && attributeById2 != null)
          attributeById2.AsString = attributeById1.AsString;
      }
      else if (attributeById1 != null && attributeById2 != null)
        attributeById2.AsString = attributeById1.AsString + "_прототип";
    }
    if (dbObject is WFProcess wfProcess)
      wfProcess.ClearVariable();
    return dbObject;
  }
}
