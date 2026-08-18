// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportSettings
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.XmlExchange.Settings.Export.Common;
using Intermech.Interfaces.XmlExchange.Settings.Export.Extensions;
using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Настройки экспорта данных в XML</summary>
[Serializable]
public sealed class XmlExchangeExportSettings
{
  /// <summary>Получение настроек атрибутов для указанного типа</summary>
  /// <param name="itemKind"></param>
  /// <param name="itemType"></param>
  /// <returns></returns>
  public XmlExchangeExportAttributable GetAttributable(AttributableElements itemKind, int itemType)
  {
    if (itemKind == AttributableElements.Object)
      return (XmlExchangeExportAttributable) this.ObjSettings.GetItemByID(itemType);
    return itemKind == AttributableElements.Relation ? (XmlExchangeExportAttributable) this.RelSettings.GetItemByID(itemType) : (XmlExchangeExportAttributable) null;
  }

  /// <summary>
  /// Получение ид. пользовательского идентификатора для атрибута объекта / связи
  /// </summary>
  /// <param name="itemKind"></param>
  /// <param name="itemType"></param>
  /// <param name="attrType"></param>
  /// <returns></returns>
  public XmlExchangeExportAttr GetAttribute(
    AttributableElements itemKind,
    int itemType,
    int attrType)
  {
    XmlExchangeExportAttr exchangeExportAttr = (XmlExchangeExportAttr) null;
    XmlExchangeExportAttributable attributable = this.GetAttributable(itemKind, itemType);
    if (attributable != null)
      exchangeExportAttr = attributable.AttrList.GetItemByID(attrType);
    return exchangeExportAttr ?? this.AttrSettings.GetItemByID(attrType);
  }

  /// <summary>Формат имени файла для пакета (задачи)</summary>
  public string LogFileFormat { get; set; } = string.Empty;

  /// <summary>Формат имени файла для пакета (задачи)</summary>
  public string PacketFileFormat { get; set; } = XmlExchangeConsts.Common.XmlPacketFileName;

  /// <summary>Размер пакета входных данных</summary>
  public int PacketChunkSize { get; set; } = XmlExchangeConsts.Common.PacketChunkSize;

  /// <summary>
  ///  Шаблон для формирования имени директории для "подзадачи"
  /// </summary>
  public string PacketChunkDirFormat { get; set; } = XmlExchangeConsts.Common.PacketChunkDirFormat;

  /// <summary>Формат имени файла для метаданных</summary>
  public string MetaFileFormat { get; set; } = XmlExchangeConsts.Common.XmlMetaBriedFileName;

  /// <summary>Формат имени файла для объектов</summary>
  public string ObjFileFormat { get; set; } = "Objects.xml";

  /// <summary>Формат имени файла для связей</summary>
  public string RelFileFormat { get; set; } = "Relations.xml";

  /// <summary>Формат папки для файлов с данными</summary>
  public string DataDirFormat { get; set; } = "Blob";

  /// <summary>Формат выгрузки даты</summary>
  /// <remarks>Если значение не задано - выгружается в виде 2017-07-10T08:52:58.857</remarks>
  public string DateTimeFormat { get; set; } = string.Empty;

  /// <summary>Наименование TimeZone для выгрузки данных с временем</summary>
  /// <remarks>Если значение не задано - выгружается в виде UTC+0</remarks>
  public string TimeZoneName { get; set; } = string.Empty;

  /// <summary>Режим архивации экспортируемых данных</summary>
  public XmlExportCompressMode CompressMode { get; set; } = XmlExportCompressMode.Zip;

  /// <summary>Режим выгрузки данных в XML</summary>
  public XmlExportTaskMode TaskMode { get; set; }

  /// <summary>Режим выгрузки контрольной суммы файлов в XML</summary>
  public XmlExportChecksumAlgorithm? ChecksumMode { get; set; }

  /// <summary>Режим выгрузки доп. данных</summary>
  public XmlExportExtraDataMode ExtraDataMode { get; set; } = XmlExportExtraDataMode.RefObj4Attributes;

  /// <summary>Режим выгрузки атрибутов объектов по умолчанию</summary>
  /// <remarks>Значение режима игнорируется, если установлен флаг XmlExportExtraDataMode.UserDataOnly</remarks>
  public XmlExportAttrsMode DefObjAttrMode { get; set; } = XmlExportAttrsMode.DefinedAttributes;

  /// <summary>
  /// Гл. идентификатор правила подбора версий объектов при получении состава / применяемости.
  /// Если параметр не задан - берутся тек. настройки пользователя в навигаторе.
  /// </summary>
  public string ObjVerRule { get; set; } = string.Empty;

  /// <summary>Параметры атрибутов</summary>
  /// <remarks>Данные о псевдонимах</remarks>
  public XmlExchangeExportAttrList AttrSettings { get; } = new XmlExchangeExportAttrList();

  /// <summary>Настройки экспорта объектов</summary>
  public XmlExchangeExportObjList ObjSettings { get; } = new XmlExchangeExportObjList();

  /// <summary>Настройки экспорта связей</summary>
  public XmlExchangeExportRelList RelSettings { get; } = new XmlExchangeExportRelList();

  /// <summary>
  /// Настройки экспорта применяемости объектов / разворота состава
  /// </summary>
  public XmlExchangeExportApplicabilityList ApplSettings { get; } = new XmlExchangeExportApplicabilityList();

  /// <summary>Настройки расширений экспорта</summary>
  public XmlExchangeExportExtensions ExportExtensions { get; } = new XmlExchangeExportExtensions();

  /// <summary>
  /// 
  /// </summary>
  public XmlExchangeExportScripts ExportScripts { get; } = new XmlExchangeExportScripts();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public static XmlExchangeExportSettings LoadData(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlExchangeExportSettings) null;
    XmlExchangeExportSettings exchangeExportSettings = new XmlExchangeExportSettings();
    return !exchangeExportSettings.LoadData(xmlDoc.FirstChild) ? (XmlExchangeExportSettings) null : exchangeExportSettings;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public bool LoadData(XmlNode xmlNode)
  {
    if (xmlNode?.Attributes == null || !xmlNode.Name.ToLower().Equals("xmlexportsettings") || xmlNode.ChildNodes.Count == 0)
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes["packetfileformat"];
    if (attribute1 != null)
      this.PacketFileFormat = attribute1.Value;
    XmlAttribute attribute2 = xmlNode.Attributes["logfileformat"];
    if (attribute2 != null)
      this.LogFileFormat = attribute2.Value;
    XmlAttribute attribute3 = xmlNode.Attributes["metafileformat"];
    if (attribute3 != null)
      this.MetaFileFormat = attribute3.Value;
    XmlAttribute attribute4 = xmlNode.Attributes["objfileformat"];
    if (attribute4 != null)
      this.ObjFileFormat = attribute4.Value;
    XmlAttribute attribute5 = xmlNode.Attributes["relfileformat"];
    if (attribute5 != null)
      this.RelFileFormat = attribute5.Value;
    XmlAttribute attribute6 = xmlNode.Attributes["datadirformat"];
    if (attribute6 != null)
      this.DataDirFormat = attribute6.Value;
    XmlAttribute attribute7 = xmlNode.Attributes["chunkitemsize"];
    int result1;
    if (attribute7 != null && int.TryParse(attribute7.Value, out result1))
      this.PacketChunkSize = result1;
    XmlAttribute attribute8 = xmlNode.Attributes["chunkdirformat"];
    if (attribute8 != null)
      this.PacketChunkDirFormat = attribute8.Value;
    XmlAttribute attribute9 = xmlNode.Attributes["timezone"];
    if (attribute9 != null)
      this.TimeZoneName = attribute9.Value;
    XmlAttribute attribute10 = xmlNode.Attributes["datetimeformat"];
    if (attribute10 != null)
      this.DateTimeFormat = attribute10.Value;
    XmlAttribute attribute11 = xmlNode.Attributes["compress"];
    int result2;
    if (attribute11 != null && int.TryParse(attribute11.Value, out result2))
      this.CompressMode = (XmlExportCompressMode) result2;
    XmlAttribute attribute12 = xmlNode.Attributes["task"];
    int result3;
    if (attribute12 != null && int.TryParse(attribute12.Value, out result3))
      this.TaskMode = (XmlExportTaskMode) result3;
    XmlAttribute attribute13 = xmlNode.Attributes["extradata"];
    int result4;
    if (attribute13 != null && int.TryParse(attribute13.Value, out result4))
      this.ExtraDataMode = (XmlExportExtraDataMode) result4;
    XmlAttribute attribute14 = xmlNode.Attributes["defattr"];
    int result5;
    if (attribute14 != null && int.TryParse(attribute14.Value, out result5))
      this.DefObjAttrMode = (XmlExportAttrsMode) result5;
    XmlAttribute attribute15 = xmlNode.Attributes["objverrule"];
    if (attribute15 != null && GuidHelper.IsGuid(attribute15.Value))
      this.ObjVerRule = attribute15.Value;
    XmlAttribute attribute16 = xmlNode.Attributes["checksum"];
    int result6;
    if (attribute16 != null && int.TryParse(attribute16.Value, out result6))
      this.ChecksumMode = new XmlExportChecksumAlgorithm?((XmlExportChecksumAlgorithm) result6);
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      if (childNode != null && !this.AttrSettings.LoadData(childNode) && !this.ObjSettings.LoadData(childNode) && !this.RelSettings.LoadData(childNode) && !this.ApplSettings.LoadData(childNode) && !this.ExportExtensions.LoadData(childNode))
        this.ExportScripts.LoadData(childNode);
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="fixMode"></param>
  /// <returns></returns>
  public bool ValidateData(bool fixMode = true)
  {
    return this.ApplSettings.TrueForAll((Predicate<XmlExchangeExportAppl>) (item => item.ValidateData(fixMode))) && this.AttrSettings.TrueForAll((Predicate<XmlExchangeExportAttr>) (item => item.ValidateData(fixMode))) && this.ObjSettings.TrueForAll((Predicate<XmlExchangeExportObj>) (item => item.ValidateData(fixMode))) && this.RelSettings.TrueForAll((Predicate<XmlExchangeExportRel>) (item => item.ValidateData(fixMode))) && this.ExportExtensions.TrueForAll((Predicate<XmlExchangeExportExtension>) (item => item.ValidateData(fixMode))) && this.ExportScripts.TrueForAll((Predicate<XmlExchangeExportScript>) (item => item.ValidateData(fixMode)));
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public bool SaveData(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return false;
    XmlNode element = (XmlNode) xmlDoc.CreateElement("xmlexportsettings");
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("packetfileformat");
    attribute1.Value = this.PacketFileFormat;
    element.Attributes.Append(attribute1);
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("logfileformat");
    attribute2.Value = this.LogFileFormat;
    element.Attributes.Append(attribute2);
    XmlAttribute attribute3 = xmlDoc.CreateAttribute("metafileformat");
    attribute3.Value = this.MetaFileFormat;
    element.Attributes.Append(attribute3);
    XmlAttribute attribute4 = xmlDoc.CreateAttribute("objfileformat");
    attribute4.Value = this.ObjFileFormat;
    element.Attributes.Append(attribute4);
    XmlAttribute attribute5 = xmlDoc.CreateAttribute("relfileformat");
    attribute5.Value = this.RelFileFormat;
    element.Attributes.Append(attribute5);
    XmlAttribute attribute6 = xmlDoc.CreateAttribute("datadirformat");
    attribute6.Value = this.DataDirFormat;
    element.Attributes.Append(attribute6);
    XmlAttribute attribute7 = xmlDoc.CreateAttribute("chunkitemsize");
    attribute7.Value = this.PacketChunkSize.ToString();
    element.Attributes.Append(attribute7);
    xmlDoc.CreateAttribute("chunkdirformat").Value = this.PacketChunkDirFormat;
    XmlAttribute attribute8 = xmlDoc.CreateAttribute("timezone");
    attribute8.Value = this.TimeZoneName;
    element.Attributes.Append(attribute8);
    XmlAttribute attribute9 = xmlDoc.CreateAttribute("datetimeformat");
    attribute9.Value = this.DateTimeFormat;
    element.Attributes.Append(attribute9);
    XmlAttribute attribute10 = xmlDoc.CreateAttribute("compress");
    int compressMode = (int) this.CompressMode;
    attribute10.Value = compressMode.ToString();
    element.Attributes.Append(attribute10);
    XmlAttribute attribute11 = xmlDoc.CreateAttribute("task");
    int taskMode = (int) this.TaskMode;
    attribute11.Value = taskMode.ToString();
    element.Attributes.Append(attribute11);
    XmlAttribute attribute12 = xmlDoc.CreateAttribute("extradata");
    int extraDataMode = (int) this.ExtraDataMode;
    attribute12.Value = extraDataMode.ToString();
    element.Attributes.Append(attribute12);
    XmlAttribute attribute13 = xmlDoc.CreateAttribute("defattr");
    int defObjAttrMode = (int) this.DefObjAttrMode;
    attribute13.Value = defObjAttrMode.ToString();
    element.Attributes.Append(attribute13);
    XmlAttribute attribute14 = xmlDoc.CreateAttribute("objverrule");
    attribute14.Value = this.ObjVerRule;
    element.Attributes.Append(attribute14);
    XmlExportChecksumAlgorithm? checksumMode = this.ChecksumMode;
    if (checksumMode.HasValue)
    {
      XmlAttribute attribute15 = xmlDoc.CreateAttribute("checksum");
      checksumMode = this.ChecksumMode;
      int num = (int) checksumMode.Value;
      attribute15.Value = num.ToString();
      element.Attributes.Append(attribute15);
    }
    element.AppendChild(this.AttrSettings.SaveData(xmlDoc));
    element.AppendChild(this.ObjSettings.SaveData(xmlDoc));
    element.AppendChild(this.RelSettings.SaveData(xmlDoc));
    element.AppendChild(this.ApplSettings.SaveData(xmlDoc));
    element.AppendChild(this.ExportExtensions.SaveData(xmlDoc));
    element.AppendChild(this.ExportScripts.SaveData(xmlDoc));
    xmlDoc.AppendChild(element);
    return true;
  }
}
