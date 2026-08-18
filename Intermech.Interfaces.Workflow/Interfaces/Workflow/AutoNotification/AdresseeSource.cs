// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AdresseeSource
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Источник адресата</summary>
[Serializable]
public class AdresseeSource
{
  /// <summary>Источник адресата</summary>
  private AdresseeSourceType _adresseeSourceType;

  /// <summary>Источник адресата</summary>
  public AdresseeSourceType AdresseeSourceType => this._adresseeSourceType;

  /// <summary>Конструктор</summary>
  internal AdresseeSource() => this._adresseeSourceType = AdresseeSourceType.None;

  /// <summary>Конструктор</summary>
  /// <param name="adresseeSourceType">Источник адресата</param>
  public AdresseeSource(AdresseeSourceType adresseeSourceType)
  {
    this._adresseeSourceType = adresseeSourceType;
  }

  /// <summary>Создать xml-узел Источника адресата.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Источника адресата</returns>
  public virtual XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, nameof (AdresseeSource), string.Empty);
    XmlAttribute attribute = xmlDoc.CreateAttribute("AdresseeSourceType");
    attribute.Value = this._adresseeSourceType.ToString();
    if (node.Attributes != null)
      node.Attributes.Append(attribute);
    return node;
  }

  /// <summary>Зачитать данные из хмл.</summary>
  /// <param name="parentNode">Узел хмл.</param>
  public virtual void ReadFromXml(XmlNode parentNode)
  {
  }

  /// <summary>Создает объект Источник адресата.</summary>
  /// <param name="adresseeSourceAttrValue">Значение атрибута источника адресата.</param>
  /// <returns>Объект Источник адресата</returns>
  public static AdresseeSource CreateAdresseeSource(string adresseeSourceAttrValue)
  {
    AdresseeSourceType adresseeSourceType = (AdresseeSourceType) Enum.Parse(typeof (AdresseeSourceType), adresseeSourceAttrValue);
    AdresseeSource adresseeSource;
    switch (adresseeSourceType)
    {
      case AdresseeSourceType.AuthorInAttribute:
        adresseeSource = (AdresseeSource) new AuthorInAttribute(adresseeSourceType);
        break;
      case AdresseeSourceType.GetByScript:
        adresseeSource = (AdresseeSource) new AuthorInScript(adresseeSourceType);
        break;
      default:
        adresseeSource = new AdresseeSource(adresseeSourceType);
        break;
    }
    return adresseeSource;
  }

  /// <summary>Получает ИД адресатов.</summary>
  /// <param name="initiatorId">Инициатор события.</param>
  /// <param name="collectedObjects">Собранный набор объектов.</param>
  /// <returns>Ид адресатов</returns>
  public virtual List<long> GetAdresseeIds(long initiatorId, List<long> collectedObjects)
  {
    List<long> adresseeIds = new List<long>();
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    if (service == null || collectedObjects.Count == 0)
      return adresseeIds;
    switch (this._adresseeSourceType)
    {
      case AdresseeSourceType.RelationAuthor:
        adresseeIds = service.GetRelationAuthor(initiatorId);
        break;
      case AdresseeSourceType.ObjectAuthor:
        adresseeIds = service.GetAuthors(collectedObjects);
        break;
      case AdresseeSourceType.ObjectOwner:
        adresseeIds = service.GetOwners(collectedObjects);
        break;
      case AdresseeSourceType.ProjectManager:
        adresseeIds = service.GetProjectManagers(collectedObjects);
        break;
      case AdresseeSourceType.AuthorsDepartmentChief:
        adresseeIds = service.GetAuthorsOrganizationUnitsChiefs(collectedObjects);
        break;
      case AdresseeSourceType.OwnersDepartmentChief:
        adresseeIds = service.GetOwnersDeparmentChiefs(collectedObjects);
        break;
    }
    return adresseeIds;
  }
}
