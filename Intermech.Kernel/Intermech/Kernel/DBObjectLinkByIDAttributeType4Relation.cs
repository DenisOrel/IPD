// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkByIDAttributeType4Relation
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class DBObjectLinkByIDAttributeType4Relation(UserSession uSession, DataRow row) : 
  DBAttributeType4Relation(uSession, row),
  IDBObjectLinkByIDAttributeType,
  IDBObjectLinkAttributeType
{
  public void ValidateObjectType(int objectTypeID)
  {
    (this._AttributeType as IDBObjectLinkByIDAttributeType).ValidateObjectType(objectTypeID);
  }

  public int[] GetValidObjectTypes()
  {
    return (this._AttributeType as IDBObjectLinkByIDAttributeType).GetValidObjectTypes();
  }
}
