// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.ImportConfig.XmlExchangeImportXmlExportSettings
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.ImportConfig;

internal class XmlExchangeImportXmlExportSettings : XmlExchangeImportItem
{
  public XmlExchangeImportXmlExportSettings()
  {
  }

  public XmlExchangeImportXmlExportSettings(XmlImportBase owner)
    : base("xmlexportsettings", owner)
  {
  }

  public XmlExchangeExportSettings ExportSettings { get; set; }

  public override bool LoadData(XmlImportBase xmlImportBase)
  {
    base.LoadData(xmlImportBase);
    if (string.IsNullOrEmpty(this.XmlImportItemSetting.Text))
      return true;
    XDocument doc = new XDocument();
    XmlConfigEditorExtension.SaveXmlDocument(doc, this.XmlImportItemSetting, (XElement) null);
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.Load(doc.CreateReader());
    this.ExportSettings = XmlExchangeExportSettings.LoadData(xmlDoc);
    return string.Equals(this.ItemName, "xmlexportsettings", StringComparison.CurrentCultureIgnoreCase);
  }

  public override void SaveData()
  {
    base.SaveData();
    if (this.ExportSettings == null)
      return;
    MemoryStream memoryStream = new MemoryStream();
    if (!XmlExchangeExportHelper.SaveSettings((Stream) memoryStream, this.ExportSettings))
      return;
    memoryStream.Position = 0L;
    this.XmlImportItemSetting.Load(XDocument.Load((Stream) memoryStream).Root);
  }
}
