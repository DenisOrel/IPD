// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportImbase
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportImbase : XmlExchangeImportItem
{
  private XmlExchangeImportImbaseItem _catalog;
  private XmlExchangeImportImbaseItem _folder;
  private XmlExchangeImportImbaseItem _table;

  public XmlExchangeImportImbaseItem Catalog
  {
    get
    {
      if (this._catalog == null)
        this._catalog = new XmlExchangeImportImbaseItem("CommonCatalog", this.ImportItemSetting);
      return this._catalog;
    }
    set
    {
      if (value == null || this._catalog.СommonGuid != value.СommonGuid)
        this._folder = new XmlExchangeImportImbaseItem("CommonFolder", this.ImportItemSetting);
      this._catalog = value;
    }
  }

  public XmlExchangeImportImbaseItem Folder
  {
    get
    {
      return this._folder ?? (this._folder = new XmlExchangeImportImbaseItem("CommonFolder", this.ImportItemSetting));
    }
    set => this._folder = value;
  }

  public XmlExchangeImportImbaseItem Table
  {
    get
    {
      return this._table ?? (this._table = new XmlExchangeImportImbaseItem("CommonTable", this.ImportItemSetting));
    }
    set => this._table = value;
  }

  public XmlExchangeImportImbase()
  {
  }

  public XmlExchangeImportImbase(XmlImportBase owner)
    : base("ImBase", owner)
  {
  }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    if (this.XmlImportItemSetting.Items != null)
    {
      foreach (XmlImportBase xmlImportBase1 in this.XmlImportItemSetting.Items)
      {
        if (xmlImportBase1.Name == "CommonCatalog")
        {
          this._catalog = new XmlExchangeImportImbaseItem();
          this._catalog.LoadData(xmlImportBase1);
        }
        if (xmlImportBase1.Name == "CommonFolder")
        {
          this._folder = new XmlExchangeImportImbaseItem();
          this._folder.LoadData(xmlImportBase1);
        }
        if (xmlImportBase1.Name == "CommonTable")
        {
          this._table = new XmlExchangeImportImbaseItem();
          this._table.LoadData(xmlImportBase1);
        }
      }
    }
    return true;
  }

  public override void SaveData()
  {
    base.SaveData();
    if (this.XmlImportItemSetting.Items != null)
      this.XmlImportItemSetting.Items.Clear();
    else
      this.XmlImportItemSetting.Items = new List<XmlImportBase>();
    if (this._catalog != null && this._catalog.СommonGuid != Guid.Empty)
    {
      this._catalog.SaveData();
      this.XmlImportItemSetting.Items.Add(this._catalog.ImportItemSetting);
    }
    if (this._folder != null && this._folder.СommonGuid != Guid.Empty)
    {
      this._folder.SaveData();
      this.XmlImportItemSetting.Items.Add(this._folder.ImportItemSetting);
    }
    if (this._table == null || !(this._table.СommonGuid != Guid.Empty))
      return;
    this._table.SaveData();
    this.XmlImportItemSetting.Items.Add(this._table.ImportItemSetting);
  }
}
