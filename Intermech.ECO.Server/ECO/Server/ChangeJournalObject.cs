// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.ChangeJournalObject
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

public class ChangeJournalObject(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable)
{
  public static readonly long cjVersion = 100;

  protected override void DoBeforeCommitCreation()
  {
    base.DoBeforeCommitCreation();
    if (this.GetType() == typeof (CJRecord))
      return;
    if (VerIzvObject.attrIzvVerId == 0)
      VerIzvObject.attrIzvVerId = MetaDataHelper.GetAttributeID((object) new Guid(VerIzvObject.attrIzvVersionGuid));
    IDBAttribute dbAttribute = this.Attributes.AddAttribute(VerIzvObject.attrIzvVerId, false);
    if (dbAttribute == null)
      return;
    dbAttribute.AsInteger = ChangeJournalObject.cjVersion;
  }
}
