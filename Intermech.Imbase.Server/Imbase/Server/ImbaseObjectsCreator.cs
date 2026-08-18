// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseObjectsCreator
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

internal class ImbaseObjectsCreator : IDBObjectCreator
{
  public IDBObject CreateObject(IUserSession uSession, Guid guid, DataTable objectParams)
  {
    return guid == Intermech.Imbase.Consts.ImbaseCatalogTypeGUID || guid == Intermech.Imbase.Consts.ImbaseFolderTypeGUID || guid == Intermech.Imbase.Consts.ImbaseTableRefTypeGUID || guid == Intermech.Imbase.Consts.ImbaseTableTypeGUID || guid == Intermech.Imbase.Consts.ImbaseCatalogRecordTypeGUID || guid == Intermech.Imbase.Consts.ImbaseTableMixTypeGUID ? (IDBObject) new ImbaseDBObject(uSession as UserSession, objectParams) : (IDBObject) new DBObject(uSession as UserSession, objectParams);
  }
}
