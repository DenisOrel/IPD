// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExchangeExportTypedItem
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>
/// Базовый класс для хранения "типизированных" настроек экспорта
/// </summary>
[Serializable]
public abstract class XmlExchangeExportTypedItem : XmlExchangeExportUserItem
{
  /// <summary>Конструктор</summary>
  protected XmlExchangeExportTypedItem()
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId">Ид. типа</param>
  protected XmlExchangeExportTypedItem(int typeId)
    : this(typeId, Guid.Empty, string.Empty)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="typeId">Ид. типа</param>
  /// <param name="typeGuid"></param>
  /// <param name="typeName"></param>
  protected XmlExchangeExportTypedItem(int typeId, Guid typeGuid, string typeName)
  {
    this.TypeID = typeId;
    this.TypeGuid = typeGuid;
    this.TypeName = typeName;
  }

  /// <summary>Загрузка данных из XML</summary>
  /// <param name="xmlNode"></param>
  /// <returns></returns>
  public override bool LoadData(XmlNode xmlNode)
  {
    if (!base.LoadData(xmlNode))
      return false;
    XmlAttribute attribute1 = xmlNode.Attributes?["id"];
    if (attribute1 != null)
    {
      int result;
      if (!int.TryParse(attribute1.Value, out result))
        result = -1;
      this.TypeID = result;
    }
    XmlAttribute attribute2 = xmlNode.Attributes?["guid"];
    if (attribute2 != null && GuidHelper.IsGuid(attribute2.Value))
      this.TypeGuid = new Guid(attribute2.Value);
    XmlAttribute attribute3 = xmlNode.Attributes?["name"];
    if (attribute3 != null)
      this.TypeName = attribute3.Value;
    return true;
  }

  /// <summary>Сохранение данных в XML</summary>
  /// <param name="xmlDoc"></param>
  /// <returns></returns>
  public override XmlNode SaveData(XmlDocument xmlDoc)
  {
    if (xmlDoc == null)
      return (XmlNode) null;
    XmlNode xmlNode = base.SaveData(xmlDoc);
    if (xmlNode?.Attributes == null)
      return (XmlNode) null;
    XmlAttribute attribute1 = xmlNode.Attributes.Count > 0 ? xmlNode.Attributes[0] : (XmlAttribute) null;
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("id");
    attribute2.Value = this.TypeID.ToString();
    xmlNode.Attributes.InsertBefore(attribute2, attribute1);
    XmlAttribute attribute3 = xmlDoc.CreateAttribute("guid");
    attribute3.Value = this.TypeGuid.ToString();
    xmlNode.Attributes.InsertBefore(attribute3, attribute1);
    XmlAttribute attribute4 = xmlDoc.CreateAttribute("name");
    attribute4.Value = this.TypeName;
    xmlNode.Attributes.InsertBefore(attribute4, attribute1);
    return xmlNode;
  }

  /// <summary>Ид. текущего элемента</summary>
  public virtual int ID => this.TypeID;

  /// <summary>Ид. типа</summary>
  public int TypeID { get; set; }

  /// <summary>Гл. ид. типа</summary>
  public Guid TypeGuid { get; set; }

  /// <summary>Наименование типа</summary>
  public string TypeName { get; set; }
}
