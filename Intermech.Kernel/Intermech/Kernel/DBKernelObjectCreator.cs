// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBKernelObjectCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Kernel.Compositions;
using Intermech.Kernel.Projects;
using Intermech.Kernel.Versions;
using Intermech.Workspace;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBKernelObjectCreator : DBObjectCreator
{
  public override IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    UserSession uSession1 = uSession as UserSession;
    if (MetaDataHelper.IsObjectTypeChildOf(guid, new Guid("cad0146b-306c-11d8-b4e9-00304f19f545")))
      return (IDBObject) new DBEditingContextsObject(uSession1, objectParams);
    if (MetaDataHelper.IsObjectTypeChildOf(guid, new Guid("cadd940b-306c-11d8-b4e9-00304f19f545")))
      return (IDBObject) new DBMasterArticleObject(uSession1, objectParams);
    switch (guid.ToString().ToLower())
    {
      case "cad00002-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBUserObject(uSession1, objectParams);
      case "cad00003-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBUsersGroupObject(uSession1, objectParams);
      case "cad00007-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBRoleObject(uSession1, objectParams);
      case "cad0000b-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBMeasureObject(uSession1, objectParams);
      case "cad00014-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBStorageObject(uSession1, objectParams);
      case "cad0004a-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new ServerWorkspace(uSession1, objectParams);
      case "cad0013b-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBAttributeContainer(uSession1, objectParams);
      case "cad001b3-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBVersionRuleObject(uSession1, objectParams);
      case "cad00342-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBFilePrototype(uSession1, objectParams);
      case "cad00812-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBProjectObject(uSession1, objectParams);
      case "cad00822-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBObjectTemplate(uSession1, objectParams);
      case "cad0088e-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBArchitectDocumentSet(uSession1, objectParams);
      case "cad0088f-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBArchitectDocumentSet(uSession1, objectParams);
      case "cad0148c-306c-11d8-b4e9-00304f19f545":
        return (IDBObject) new DBSiteObject(uSession1, objectParams);
      default:
        return base.CreateObject(uSession, guid, objectParams);
    }
  }
}
