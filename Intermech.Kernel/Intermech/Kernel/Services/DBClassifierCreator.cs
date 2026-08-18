// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.DBClassifierCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Data;


namespace Intermech.Kernel.Services;

internal class DBClassifierCreator : IDBObjectCreator
{
  public static Guid ClassifFolderKeyGuid = new Guid("cad0014d-306c-11d8-b4e9-00304f19f545");
  public static Guid ClassifCommonGuid = new Guid("cad0014e-306c-11d8-b4e9-00304f19f545");
  public static Guid ClassifPersonGuid = new Guid("cad0014f-306c-11d8-b4e9-00304f19f545");
  public static Guid ClassifFolderGuid = new Guid("cad00150-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImbaseCatalogTypeGUID = new Guid("cad00221-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImbaseFolderTypeGUID = new Guid("cad00222-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImbaseTableRefTypeGUID = new Guid("cad00227-306c-11d8-b4e9-00304f19f545");
  public static readonly Guid ImbaseCatalogRecordTypeGUID = new Guid("cad00223-306c-11d8-b4e9-00304f19f545");

  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return (IDBObject) new DBClassifier((UserSession) uSession, objectParams);
  }
}
