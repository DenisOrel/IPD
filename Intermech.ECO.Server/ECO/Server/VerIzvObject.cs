// Decompiled with JetBrains decompiler
// Type: Intermech.ECO.Server.VerIzvObject
// Assembly: Intermech.ECO.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E6459663-BB12-41FD-949A-3849B46AE118
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.ECO.Server.dll

using Intermech.Interfaces;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.ECO.Server;

public class VerIzvObject(UserSession uSession, DataTable objectsTable) : DBEditingContextsObject(uSession, objectsTable)
{
  public static readonly long curVersion = 100;
  public static readonly string attrIzvVersionGuid = "cadd9598-306c-11d8-b4e9-00304f19f545";
  public static int attrIzvVerId = 0;

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
    dbAttribute.AsInteger = VerIzvObject.curVersion;
  }
}
