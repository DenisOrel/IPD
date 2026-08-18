// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportUserItem
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Базовый класс для хранения настроек экспорта с поддержкой пользовательских типов
/// </summary>
[Serializable]
public abstract class XmlExchangeExportUserItem : XmlExchangeExportItem
{
  /// <summary>Конструктор</summary>
  protected XmlExchangeExportUserItem()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="userId">Ид. пользовательского типа</param>
  protected XmlExchangeExportUserItem(int userId)
    : this(userId.ToString())
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="userId">Ид. пользовательского типа</param>
  /// <param name="userName"></param>
  /// <param name="userAlias"></param>
  protected XmlExchangeExportUserItem(string userId, string userName = null, string userAlias = null)
  {
    this.UserID = userId;
    this.UserName = userName;
    this.UserAlias = userAlias;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes?["user_id"];
    if (attribute1 != null)
      this.UserID = attribute1.Value;
    XmlAttribute attribute2 = xmlNode.Attributes?["user_alias"];
    if (attribute2 != null)
      this.UserAlias = attribute2.Value;
    XmlAttribute attribute3 = xmlNode.Attributes?["user_name"];
    if (attribute3 != null)
      this.UserName = attribute3.Value;
    return true;
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode == null)
      return (XmlNode) null;
    if (this.UserID != null)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("user_id");
      attribute.Value = this.UserID;
      xmlNode.Attributes?.Append(attribute);
    }
    if (this.UserName != null)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("user_name");
      attribute.Value = this.UserName;
      xmlNode.Attributes?.Append(attribute);
    }
    if (this.UserAlias != null)
    {
      XmlAttribute attribute = xmlDoc.CreateAttribute("user_alias");
      attribute.Value = this.UserAlias;
      xmlNode.Attributes?.Append(attribute);
    }
    return xmlNode;
  }

  /// <summary>Пользовательский идентификатор</summary>
  public string UserID { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public int UserID2Int
  {
    get
    {
      int result;
      int.TryParse(this.UserID, out result);
      return result;
    }
  }

  /// <summary>Пользовательский псевдоним / alias</summary>
  public string UserAlias { get; set; }

  /// <summary>Пользовательское наименование</summary>
  public string UserName { get; set; }
}
