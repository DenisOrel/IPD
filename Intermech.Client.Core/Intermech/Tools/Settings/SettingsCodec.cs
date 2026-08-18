
// Type: Intermech.Tools.Settings.SettingsCodec
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Xml;


namespace Intermech.Tools.Settings;

/// <summary>
/// Реализует кодек настроек интегратора для преобразования их в форму xml-документа и обратно. Класс
/// не является thread-safe.
/// </summary>
public abstract class SettingsCodec
{
  public abstract ISettingsObject CreateEmptySettings();

  /// <summary>
  /// Выполняет преобразование объекта с настройками интегратора в xml-документ.
  /// </summary>
  /// <param name="settingsObject">Объект с настройками интегратора</param>
  /// <returns>Настройки интегратора в форме xml-документа</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на объект с настройками не может быть null</exception>
  public XmlDocument Encode(ISettingsObject settingsObject)
  {
    if (settingsObject == null)
      throw new ArgumentNullException(nameof (settingsObject));
    XmlDocument xml = new XmlDocument();
    xml.AppendChild((XmlNode) xml.CreateXmlDeclaration("1.0", "UTF-16", (string) null));
    xml.AppendChild((XmlNode) xml.CreateElement("Settings"));
    SettingsXmlBuilder settingsBuilder = new SettingsXmlBuilder(xml);
    this.EncodeSettings(settingsObject, settingsBuilder);
    this.EncodeServerData(settingsObject, settingsBuilder);
    XmlAttribute attribute = xml.CreateAttribute("version");
    attribute.AppendChild((XmlNode) xml.CreateTextNode(XmlConvert.ToString(this.GetEncoderFormatVersion())));
    XmlElement element = xml.CreateElement("FileFormat");
    element.Attributes.Append(attribute);
    xml.DocumentElement.InsertBefore((XmlNode) element, xml.DocumentElement.FirstChild);
    return xml;
  }

  protected abstract void EncodeSettings(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder);

  protected abstract void EncodeServerData(
    ISettingsObject settingsObject,
    SettingsXmlBuilder settingsBuilder);

  protected abstract int GetEncoderFormatVersion();

  /// <summary>
  /// Выполняет преобразование xml-документа в объект с настройками интегратора.
  /// </summary>
  /// <param name="settingsXml">Настройки интегратора в форме xml-документа</param>
  /// <returns>Объект с настройками интегратора</returns>
  /// <exception cref="T:System.ArgumentNullException">Ссылки на xml-документ не могжет быть null</exception>
  /// <exception cref="T:System.NotSupportedException">Неизвестная версия формата xml-документа</exception>
  public ISettingsObject Decode(XmlDocument settingsXml)
  {
    XmlAttribute xmlAttribute = settingsXml != null ? (XmlAttribute) settingsXml.DocumentElement.SelectSingleNode("FileFormat/@version") : throw new ArgumentNullException(nameof (settingsXml));
    int formatVersion = xmlAttribute != null ? XmlConvert.ToInt32(xmlAttribute.Value) : 1;
    ISettingsObject emptySettings = this.CreateEmptySettings();
    SettingsXmlBuilder settingsBuilder = new SettingsXmlBuilder(settingsXml);
    this.DecodeSettings(formatVersion, settingsBuilder, emptySettings);
    return emptySettings;
  }

  protected virtual void DecodeSettings(
    int formatVersion,
    SettingsXmlBuilder settingsBuilder,
    ISettingsObject settingsObject)
  {
    throw new NotSupportedException(string.Format(LocalizationHolder.rm.GetString("SR_1643"), (object) formatVersion));
  }
}
