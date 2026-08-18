// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Server.ExpertObjectCreator
// Assembly: Intermech.Expert.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8532AAAD-1C72-4C22-AA34-A49C95D2B71F
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Expert.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Expert.Server;

internal class ExpertObjectCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    string str = guid.ToString();
    IDBObject dbObject = (IDBObject) null;
    if (str == ExpertObjGUIDs.ExpertCond)
      dbObject = (IDBObject) new ExpertCond((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ExpertFormula)
      dbObject = (IDBObject) new ExpertFormula((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ExpertFunction)
      dbObject = (IDBObject) new ExpertFunction((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ExpertScript)
      dbObject = (IDBObject) new ExpertScript((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ExpertAttrRules)
      dbObject = (IDBObject) new ExpertAttrRules((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ExpertObjRules)
      dbObject = (IDBObject) new ExpertObjRules((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ExpertTable)
      dbObject = (IDBObject) new ExpertTable((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.DocScript)
      dbObject = (IDBObject) new DocScript((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.RecalcScript)
      dbObject = (IDBObject) new RecalcScript((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.SimpleFormula)
      dbObject = (IDBObject) new SimpleFormula((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.ComplectTemplate)
      dbObject = (IDBObject) new ComplectTemplate((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.objESFolder)
      dbObject = (IDBObject) new ExpertFolder((UserSession) uSession, objectParams);
    if (str == ExpertObjGUIDs.CommandScript)
      dbObject = (IDBObject) new CommandScript((UserSession) uSession, objectParams);
    return dbObject;
  }
}
