// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBArchitectDocumentSet
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBArchitectDocumentSet(UserSession uSession, DataTable objectsTable) : DBObject(uSession, objectsTable)
{
  public override void DoAfterCreateRelation(IDBRelation newrelation)
  {
    base.DoAfterCreateRelation(newrelation);
    IDBObject dbObject = newrelation.PartObjectID == 0L ? this.UserSession.GetObjectByVersionsRule(newrelation.PartID, "cad005aa-306c-11d8-b4e9-00304f19f545", false) : this.UserSession.GetObject(newrelation.PartObjectID, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeByGuid1 = dbObject.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid1 != null && !attributeByGuid1.ReadOnly)
    {
      IDBAttribute attributeByGuid2 = this.GetAttributeByGuid(new Guid("cad0001f-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid2 != null)
        attributeByGuid1.Values = attributeByGuid2.Values;
    }
    IDBAttribute attributeByGuid3 = dbObject.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid3 == null || attributeByGuid3.ReadOnly)
      return;
    IDBAttribute attributeByGuid4 = this.GetAttributeByGuid(new Guid("cad00020-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid4 == null)
      return;
    attributeByGuid3.Values = attributeByGuid4.Values;
  }
}
