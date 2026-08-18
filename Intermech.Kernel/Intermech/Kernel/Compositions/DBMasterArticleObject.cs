// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Compositions.DBMasterArticleObject
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using System;
using System.Data;


namespace Intermech.Kernel.Compositions;

internal class DBMasterArticleObject(UserSession uSession, DataTable objectParams) : 
  DBObject(uSession, objectParams),
  IDBMasterArticleObject,
  IDBObject,
  IDBAttributable,
  IDBSessionable,
  IPluginsData
{
  public string Description
  {
    get
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), false);
      return attributeByGuid != null ? DataSetProcessor.GetStringValue(attributeByGuid.Value, string.Empty) : string.Empty;
    }
    set
    {
      IDBAttribute attributeByGuid = this.GetAttributeByGuid(new Guid("cad00021-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid == null)
        return;
      if (value == null)
        attributeByGuid.Value = (object) DBNull.Value;
      else
        attributeByGuid.Value = (object) value;
    }
  }
}
