// Decompiled with JetBrains decompiler
// Type: Intermech.BugReports.Server.BugDBObject
// Assembly: Intermech.BugReports.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D5496885-D5AE-45E1-887A-E42A46AB4DD0
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.BugReports.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System.Data;

#nullable disable
namespace Intermech.BugReports.Server;

internal class BugDBObject(IUserSession uSession, DataTable objectsTable) : DBObject(uSession as UserSession, objectsTable)
{
  public override void DoAfterCreate()
  {
    base.DoAfterCreate();
    IDBAttribute attributeByGuid = this.GetAttributeByGuid(BugReportsHolder.AT.FindUser, false);
    if (attributeByGuid == null)
      return;
    attributeByGuid.AsInteger = this.UserSession.UserID;
  }
}
