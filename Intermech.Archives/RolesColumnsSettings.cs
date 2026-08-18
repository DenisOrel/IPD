// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.RolesColumnsSettings
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.Interfaces;
using System.Xml;

#nullable disable
namespace Intermech.Archives;

/// <summary>
/// Назначенные колонки на роль.
/// Для колонок по умолчанию: RoleId = -1 ArchivesConsts.DefaultRoleId
/// </summary>
internal class RolesColumnsSettings
{
  public long RoleID;
  public NodeColumnCollection Columns;

  public RolesColumnsSettings()
  {
    this.RoleID = 0L;
    this.Columns = new NodeColumnCollection();
  }

  /// <summary>Загрузка данных с xml-узла</summary>
  /// <param name="childNode"></param>
  public void LoadFromNode(XmlNode childNode)
  {
    XmlNode xmlNode1 = childNode.SelectSingleNode("RoleId");
    if (xmlNode1 != null)
      this.RoleID = XmlConvert.ToInt64(xmlNode1.InnerText);
    XmlNode xmlNode2 = childNode.SelectSingleNode("Columns");
    if (xmlNode2 == null)
      return;
    this.Columns.LoadData(xmlNode2);
  }
}
