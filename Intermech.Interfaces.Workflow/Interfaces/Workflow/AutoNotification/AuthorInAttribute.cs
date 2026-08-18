// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AuthorInAttribute
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Автор указан в атрибуте</summary>
[Serializable]
public class AuthorInAttribute : AdresseeSource
{
  /// <summary>Атрибут, в котором записан автор</summary>
  private int _attrID;

  /// <summary>Атрибут, в котором записан автор</summary>
  public int AttrID => this._attrID;

  /// <summary>Конструктор.</summary>
  /// <param name="adresseeSourceType">Источник адресата</param>
  /// <param name="attrID">Идентификатор атрибута, в котором записан адресат.</param>
  public AuthorInAttribute(AdresseeSourceType adresseeSourceType, int attrID)
    : base(adresseeSourceType)
  {
    this._attrID = attrID;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="adresseeSourceType">Источник адресата</param>
  public AuthorInAttribute(AdresseeSourceType adresseeSourceType)
    : base(adresseeSourceType)
  {
    this._attrID = 0;
  }

  public override List<long> GetAdresseeIds(long initiatorId, List<long> collectedObjects)
  {
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service == null || collectedObjects.Count == 0 ? new List<long>(0) : service.GetAdresseesFromAttribute(collectedObjects, this._attrID);
  }

  /// <summary>Создать xml-узел Источника адресата.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Источника адресата</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("AttributeId");
    attribute.Value = this._attrID.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
    return xml;
  }

  /// <summary>Зачитать данные из хмл.</summary>
  /// <param name="parentNode">Родительский узел</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    base.ReadFromXml(parentNode);
    this._attrID = Convert.ToInt32(parentNode.Attributes["AttributeId"].Value);
  }
}
