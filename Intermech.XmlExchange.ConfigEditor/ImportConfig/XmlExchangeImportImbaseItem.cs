// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportImbaseItem
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportImbaseItem : XmlExchangeImportItem
{
  private Guid _commonGuid = Guid.Empty;

  public XmlExchangeImportImbaseItem()
  {
  }

  public XmlExchangeImportImbaseItem(Guid newGuid, XmlExchangeImportImbaseItem oldImbaseItem)
    : base(oldImbaseItem.ItemName, oldImbaseItem.XmlImportItemSetting)
  {
    this.СommonGuid = newGuid;
  }

  public XmlExchangeImportImbaseItem(string itemName, XmlImportBase owner)
    : base(itemName, owner)
  {
  }

  public XmlExchangeImportImbaseItem(
    Guid guid,
    string name,
    string path,
    string itemName,
    XmlImportBase owner)
    : base(itemName, owner)
  {
    this._commonGuid = guid;
    this.CommonName = name;
    this.CommonPath = path;
  }

  public Guid СommonGuid
  {
    get => this._commonGuid;
    set
    {
      if (!(value != Guid.Empty))
        return;
      this._commonGuid = value;
      this.GetObjectData(this._commonGuid);
    }
  }

  public string CommonPath { get; private set; } = string.Empty;

  public string CommonName { get; private set; } = string.Empty;

  public string Caption => !(this.CommonPath != string.Empty) ? this.CommonName : this.CommonPath;

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    this._commonGuid = this.XmlImportItemSetting.GetAsGuid("guid", Guid.Empty);
    this.CommonPath = this.XmlImportItemSetting.GetAsString("path", string.Empty);
    this.CommonName = this.XmlImportItemSetting.GetAsString("name", string.Empty);
    return true;
  }

  public override void SaveData()
  {
    base.SaveData();
    if (!(this.СommonGuid != Guid.Empty))
      return;
    this.XmlImportItemSetting.SetAsGuid("guid", this._commonGuid);
    if (this.CommonPath != string.Empty)
      this.XmlImportItemSetting.SetAsString("path", this.CommonPath);
    if (!(this.CommonName != string.Empty))
      return;
    this.XmlImportItemSetting.SetAsString("name", this.CommonName);
  }

  private void GetObjectData(Guid guid)
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IUserSession session = sessionKeeper.Session;
      if (session == null)
        return;
      IDBObject dbObject1 = session.GetObject(guid);
      if (dbObject1 == null)
        return;
      this.CommonName = dbObject1.Caption;
      IDBObject dbObject2 = (IDBObject) null;
      string str = string.Empty;
      if (dbObject1.TypeID == Intermech.Imbase.Consts.ImbaseCatalogTypeID)
      {
        dbObject2 = dbObject1;
        str = dbObject1.Caption;
      }
      else
      {
        if (!(session.GetCustomService(typeof (IImbaseServer)) is IImbaseServer customService))
          return;
        Guid sessionGuid = session.SessionGUID;
        long[] objectList = new long[1]
        {
          dbObject1.ObjectID
        };
        DataTable foldersForObjects = customService.GetFoldersForObjects(sessionGuid, objectList, (long[]) null);
        if (foldersForObjects.Rows.Count == 0)
          return;
        foreach (DataRow dataRow in (IEnumerable<DataRow>) foldersForObjects.Rows.OfType<DataRow>().OrderBy<DataRow, int>((System.Func<DataRow, int>) (a => a["F_PATH"].ToString().Length)))
        {
          if (dataRow["F_OBJECT_TYPE"].ToString() == Intermech.Imbase.Consts.ImbaseCatalogTypeID.ToString())
            dbObject2 = session.GetObject(Convert.ToInt64(dataRow["F_OBJECT_ID"]));
          str = !string.IsNullOrEmpty(str) ? $"{str}\\{dataRow["CAPTION"]}" : str + dataRow["CAPTION"];
        }
      }
      if (string.IsNullOrEmpty(str) || dbObject2 == null)
        return;
      IDBAttribute attributeByGuid = dbObject2.GetAttributeByGuid(new Guid("cad00200-306c-11d8-b4e9-00304f19f545"), false);
      if (attributeByGuid != null && attributeByGuid.AsString != string.Empty)
        this.CommonPath = $"{attributeByGuid.AsString}\\{str}";
      else
        this.CommonPath = str;
    }
  }
}
