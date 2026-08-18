// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.IMDocEditorToolSettingsCodec
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Settings;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Document.Client;

internal sealed class IMDocEditorToolSettingsCodec
{
  /// <summary>Получить настройки</summary>
  /// <returns></returns>
  public static IMDocEditorToolSettings GetSettings()
  {
    IMDocEditorToolSettings settings = (IMDocEditorToolSettings) null;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IIntegratorServer service = ServiceUtils.GetService<IIntegratorServer>((object) sessionKeeper.Session, true);
      if (service.IsIntegratorExists(DocumentEditorIntegrator.IntegratorId))
      {
        XmlDocument data = new XmlDocument();
        data.LoadXml(service.GetIntegratorData(DocumentEditorIntegrator.IntegratorId));
        settings = IMDocEditorToolSettingsCodec.Decode(data);
      }
    }
    return settings;
  }

  public static void Encode(XmlDocument data, IMDocEditorToolSettings toolSettings)
  {
    if (data == null)
      throw new ArgumentNullException();
    if (toolSettings == null)
      throw new ArgumentNullException();
    XmlElement newChild = data.DocumentElement == null ? data.CreateElement("AVS") : throw new InvalidOperationException();
    newChild.AppendChild(IMDocEditorToolSettingsCodec.EncodeSettingsBlob(toolSettings, data));
    data.AppendChild((XmlNode) newChild);
    IntegratorServerDataBuilder serverData = new IntegratorServerDataBuilder();
    IMDocEditorToolSettingsCodec.EmitServerData(toolSettings, serverData);
    SettingsXmlBuilder settingsBuilder = new SettingsXmlBuilder(data);
    serverData.UpdateXml(settingsBuilder);
  }

  private static XmlNode EncodeSettingsBlob(IMDocEditorToolSettings toolSettings, XmlDocument data)
  {
    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        toolSettings.SaveToStream(sessionKeeper.Session, (Stream) memoryStream, true);
      XmlElement element = data.CreateElement("Blob");
      element.AppendChild((XmlNode) data.CreateTextNode(Convert.ToBase64String(memoryStream.ToArray())));
      return (XmlNode) element;
    }
  }

  private static void EmitServerData(
    IMDocEditorToolSettings toolSettings,
    IntegratorServerDataBuilder serverData)
  {
    serverData.IntegratorName = DocumentEditorIntegrator.IntegratorName;
    serverData.SpecialFileManagement = true;
    serverData.AddObjectType(new Guid("cad00348-306c-11d8-b4e9-00304f19f545"));
    serverData.AddObjectType(new Guid("cad00251-306c-11d8-b4e9-00304f19f545"));
    serverData.AddObjectType(new Guid("cad00134-306c-11d8-b4e9-00304f19f545"));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      foreach (int supportedTypeId in toolSettings.SupportedTypeIDs)
      {
        Guid guid = ((IDBGuid) sessionKeeper.Session.GetObjectType(supportedTypeId, true)).GUID;
        serverData.AddObjectType(guid);
      }
    }
  }

  private static XmlNode EmitObjectType(Guid objTypeGuid, XmlDocument data)
  {
    XmlElement element = data.CreateElement("ObjectType");
    element.Attributes.Append(IMDocEditorToolSettingsCodec.CreateAttribute("guid", XmlConvert.ToString(objTypeGuid), data));
    return (XmlNode) element;
  }

  private static XmlAttribute CreateAttribute(string attrName, string attrValue, XmlDocument data)
  {
    XmlAttribute attribute = data.CreateAttribute(attrName);
    attribute.Value = attrValue;
    return attribute;
  }

  public static IMDocEditorToolSettings Decode(XmlDocument data)
  {
    if (data == null)
      throw new ArgumentNullException();
    IMDocEditorToolSettings toolSettings = new IMDocEditorToolSettings();
    IMDocEditorToolSettingsCodec.DecodeSettingsBlob(toolSettings, data);
    return toolSettings;
  }

  private static void DecodeSettingsBlob(IMDocEditorToolSettings toolSettings, XmlDocument data)
  {
    XmlNode xmlNode = data.SelectSingleNode("/AVS/Blob");
    if (xmlNode == null || string.IsNullOrEmpty(xmlNode.InnerText))
      return;
    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(xmlNode.InnerText)))
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        toolSettings.LoadFromStream(sessionKeeper.Session, (Stream) memoryStream, false);
    }
  }
}
