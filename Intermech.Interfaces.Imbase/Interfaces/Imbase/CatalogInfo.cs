// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.CatalogInfo
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

[Serializable]
public struct CatalogInfo : ICatalogInfo
{
  public long _id;
  public string _guid;
  public string _internalName;
  public string _classifierPath;
  public string _catalogDef;

  public CatalogInfo(IDBObject dbObject, string catalogDef)
  {
    this._id = dbObject.ObjectID;
    this._classifierPath = string.Empty;
    this._internalName = string.Empty;
    IDBAttributeCollection attributes = dbObject.Attributes;
    IDBAttribute byId1 = attributes.FindByID(Intermech.Imbase.Consts.ImbaseInternalTableNameAttID);
    if (byId1 != null)
      this._internalName = Convert.ToString(byId1.Values[0]);
    IDBAttribute byId2 = attributes.FindByID(Intermech.Imbase.Consts.ClassifFolderKeyAttId);
    if (byId2 != null)
      this._classifierPath = Convert.ToString(byId2.Values[0]);
    this._catalogDef = catalogDef;
    this._guid = dbObject.ObjectGUID.ToString();
  }

  public long Id => this._id;
}
