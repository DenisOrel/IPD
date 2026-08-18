// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportSettings
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.List;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportSettings
{
  private XmlImportBase _rootXmlImportBase;
  private XmlExchangeImportRulesImport _rulesImport;
  private XmlExchangeImportRulesCreate _rulesCreate;
  private XmlExchangeImportRulesSearch _rulesSearch;
  private XmlExchangeImportImbase _imBaseImportSetting;
  private XmlExchangeImportScripts _importScripts;
  private XmlExchangeImportActionsScripts _importActionsScripts;
  private XmlExchangeImportExtensions _importExtensions;
  private XmlExchangeImportXmlExportSettings _exportSettings;

  public XmlExchangeImportSettings()
  {
  }

  public XmlExchangeImportSettings(string name, out XmlImportBase xmlImportSettings)
  {
    this._rootXmlImportBase = new XmlImportBase()
    {
      Name = name,
      Items = new System.Collections.Generic.List<XmlImportBase>()
    };
    xmlImportSettings = this._rootXmlImportBase;
  }

  public string Name { get; private set; }

  public bool LoadData(XmlImportBase rootXmlImportBase)
  {
    bool flag = true;
    this._rootXmlImportBase = rootXmlImportBase;
    this.Name = rootXmlImportBase.Name;
    if (this.Name != "XMLImportSettings" || this._rootXmlImportBase.Items == null)
      return false;
    foreach (XmlImportBase xmlImportBase in this._rootXmlImportBase.Items)
    {
      switch (xmlImportBase.Name)
      {
        case "ACTIONS_SCRIPTS":
          this._importActionsScripts = new XmlExchangeImportActionsScripts();
          flag = flag && this._importActionsScripts.LoadData(xmlImportBase);
          continue;
        case "Extentions":
          this._importExtensions = new XmlExchangeImportExtensions();
          flag = flag && this._importExtensions.LoadData(xmlImportBase);
          continue;
        case "ImBase":
          this._imBaseImportSetting = new XmlExchangeImportImbase();
          flag = flag && this._imBaseImportSetting.LoadData(xmlImportBase);
          continue;
        case "ObjectCreationRules":
          this._rulesCreate = new XmlExchangeImportRulesCreate();
          flag = flag && this._rulesCreate.LoadData(xmlImportBase);
          continue;
        case "ObjectImportRules":
          this._rulesImport = new XmlExchangeImportRulesImport();
          flag = flag && this._rulesImport.LoadData(xmlImportBase);
          continue;
        case "SCRIPTS":
          this._importScripts = new XmlExchangeImportScripts();
          flag = flag && this._importScripts.LoadData(xmlImportBase);
          continue;
        case "SearchRules":
          this._rulesSearch = new XmlExchangeImportRulesSearch();
          flag = flag && this._rulesSearch.LoadData(xmlImportBase);
          continue;
        case "xmlexportsettings":
          this._exportSettings = new XmlExchangeImportXmlExportSettings();
          flag = flag && this._exportSettings.LoadData(xmlImportBase);
          continue;
        default:
          continue;
      }
    }
    return flag;
  }

  public void SaveData()
  {
    this.RulesSearch.SaveData();
    this.RulesCreate.SaveData();
    this.RulesImport.SaveData();
    this.ImbaseImportSettings.SaveData();
    this.ImportScripts.SaveData();
    this.ImportActionsScripts.SaveData();
    this.ImportExtensions.SaveData();
    this.ExportSettings.SaveData();
  }

  public XmlExchangeImportRulesSearch RulesSearch
  {
    get
    {
      return this._rulesSearch ?? (this._rulesSearch = new XmlExchangeImportRulesSearch(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportRulesImport RulesImport
  {
    get
    {
      return this._rulesImport ?? (this._rulesImport = new XmlExchangeImportRulesImport(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportRulesCreate RulesCreate
  {
    get
    {
      return this._rulesCreate ?? (this._rulesCreate = new XmlExchangeImportRulesCreate(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportXmlExportSettings ExportSettings
  {
    get
    {
      return this._exportSettings ?? (this._exportSettings = new XmlExchangeImportXmlExportSettings(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportImbase ImbaseImportSettings
  {
    get
    {
      return this._imBaseImportSetting ?? (this._imBaseImportSetting = new XmlExchangeImportImbase(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportScripts ImportScripts
  {
    get
    {
      return this._importScripts ?? (this._importScripts = new XmlExchangeImportScripts(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportActionsScripts ImportActionsScripts
  {
    get
    {
      return this._importActionsScripts ?? (this._importActionsScripts = new XmlExchangeImportActionsScripts(this._rootXmlImportBase));
    }
  }

  public XmlExchangeImportExtensions ImportExtensions
  {
    get
    {
      return this._importExtensions ?? (this._importExtensions = new XmlExchangeImportExtensions(this._rootXmlImportBase));
    }
  }
}
