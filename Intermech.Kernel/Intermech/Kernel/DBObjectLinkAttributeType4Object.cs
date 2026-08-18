// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkAttributeType4Object
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class DBObjectLinkAttributeType4Object(UserSession uSession, DataRow row) : 
  DBAttributeType4Object(uSession, row),
  IDBObjectLinkAttributeType
{
  public void ValidateObjectType(int objectTypeID)
  {
    (this._AttributeType as IDBObjectLinkAttributeType).ValidateObjectType(objectTypeID);
  }

  public int[] GetValidObjectTypes()
  {
    return (this._AttributeType as IDBObjectLinkAttributeType).GetValidObjectTypes();
  }
}
