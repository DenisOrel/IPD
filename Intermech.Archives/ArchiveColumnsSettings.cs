// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.ArchiveColumnsSettings
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Настройки колонок отображения по умолчанию архива
/// Используется класс с пустым списком, если настроек нет
/// </summary>
internal class ArchiveColumnsSettings
{
  public long ArchiveID;
  public List<RolesColumnsSettings> RolesColumnSettings;

  public ArchiveColumnsSettings()
  {
    this.ArchiveID = 0L;
    this.RolesColumnSettings = new List<RolesColumnsSettings>();
  }

  public void SaveToXmlDoc(XmlDocument xmlDoc)
  {
    XmlNode element1 = (XmlNode) xmlDoc.CreateElement("ArchiveSettings");
    xmlDoc.AppendChild(element1);
    XmlNode element2 = (XmlNode) xmlDoc.CreateElement("ArchiveId");
    element2.AppendChild((XmlNode) xmlDoc.CreateTextNode(this.ArchiveID.ToString()));
    element1.AppendChild(element2);
    if (this.RolesColumnSettings.Count == 0)
      return;
    XmlNode element3 = (XmlNode) xmlDoc.CreateElement("RolesSettings");
    element1.AppendChild(element3);
    foreach (RolesColumnsSettings rolesColumnSetting in this.RolesColumnSettings)
    {
      XmlNode element4 = (XmlNode) xmlDoc.CreateElement("RoleSettings");
      element3.AppendChild(element4);
      XmlNode element5 = (XmlNode) xmlDoc.CreateElement("RoleId");
      element5.AppendChild((XmlNode) xmlDoc.CreateTextNode(rolesColumnSetting.RoleID.ToString()));
      element4.AppendChild(element5);
      XmlNode element6 = (XmlNode) xmlDoc.CreateElement("Columns");
      rolesColumnSetting.Columns.SaveData(element6);
      element4.AppendChild(element6);
    }
  }

  public void LoadSettingsFromXmlDoc(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = xmlDoc.DocumentElement.SelectSingleNode("RolesSettings");
    if (xmlNode == null)
      return;
    foreach (XmlNode childNode in xmlNode.ChildNodes)
    {
      RolesColumnsSettings rolesColumnsSettings = new RolesColumnsSettings();
      rolesColumnsSettings.LoadFromNode(childNode);
      this.RolesColumnSettings.Add(rolesColumnsSettings);
    }
  }
}
