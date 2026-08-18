// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.SpecificAdressee
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Конкретный адресат</summary>
[Serializable]
public class SpecificAdressee : Adressee
{
  /// <summary>Пользователи</summary>
  private List<long> _usersIDs;
  /// <summary>Роли</summary>
  private List<long> _rolesIDs;
  /// <summary>Группы</summary>
  private List<long> _groupsIDs;
  /// <summary>Почта</summary>
  private string _emails;

  /// <summary>Пользователи</summary>
  public List<long> UsersIDs => this._usersIDs;

  /// <summary>Роли</summary>
  public List<long> RolesIDs => this._rolesIDs;

  /// <summary>Группы</summary>
  public List<long> GroupsIDs => this._groupsIDs;

  /// <summary>Почта</summary>
  public string Emails => this._emails;

  /// <summary>Конструктор по умолчанию.</summary>
  internal SpecificAdressee()
  {
    this._usersIDs = new List<long>();
    this._rolesIDs = new List<long>();
    this._groupsIDs = new List<long>();
    this._emails = string.Empty;
  }

  /// <summary>Конструктор</summary>
  public SpecificAdressee(
    List<long> usersID,
    List<long> rolesID,
    List<long> groupsID,
    string emails)
  {
    this._usersIDs = usersID;
    this._rolesIDs = rolesID;
    this._groupsIDs = groupsID;
    this._emails = emails;
  }

  /// <summary>Читает адресат из xml.</summary>
  /// <param name="parentNode">Xml-узел.</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    this._emails = parentNode.Attributes["Email"].Value;
    for (int i = 0; i < parentNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = parentNode.ChildNodes[i];
      switch (childNode.Name)
      {
        case "UserIds":
          this._usersIDs = AutoNotificationSettings.GetListFromXml<long>(childNode);
          break;
        case "RoleIds":
          this._rolesIDs = AutoNotificationSettings.GetListFromXml<long>(childNode);
          break;
        case "GroupIds":
          this._groupsIDs = AutoNotificationSettings.GetListFromXml<long>(childNode);
          break;
      }
    }
  }

  /// <summary>Создать xml-узел Адресата.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Адресата</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteToXml(xmlDoc);
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("Adressee");
    attribute1.Value = nameof (SpecificAdressee);
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("Email");
    attribute2.Value = this._emails;
    if (xml.Attributes != null)
    {
      xml.Attributes.Append(attribute1);
      xml.Attributes.Append(attribute2);
    }
    XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "UserIds", string.Empty);
    xml.AppendChild(node1);
    AutoNotificationSettings.WriteListToXml<long>(node1, this._usersIDs);
    XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "GroupIds", string.Empty);
    xml.AppendChild(node2);
    AutoNotificationSettings.WriteListToXml<long>(node2, this._groupsIDs);
    XmlNode node3 = xmlDoc.CreateNode(XmlNodeType.Element, "RoleIds", string.Empty);
    xml.AppendChild(node3);
    AutoNotificationSettings.WriteListToXml<long>(node3, this._rolesIDs);
    return xml;
  }

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>
  /// Набор объектов, на основании которых будут искаться адресаты
  /// </returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public override List<long> CollectObjectsForSearchingAdresseeIds(long initiatorId)
  {
    return new List<long>(1) { initiatorId };
  }

  /// <summary>Получить ИД адресатов.</summary>
  /// <param name="initiatorId">Ид инициатора.</param>
  /// <param name="collectedObjects">Набор объектов, на основании которых будут искаться адресаты.</param>
  /// <returns></returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public override List<long> GetIds(long initiatorId, List<long> collectedObjects)
  {
    List<long> collection = new List<long>();
    collection.SafeAddRange<long>((IEnumerable<long>) this._usersIDs);
    collection.SafeAddRange<long>(this.GetIdsFromRoles());
    collection.SafeAddRange<long>(this.GetIdsFromGroups());
    return collection;
  }

  /// <summary>
  /// Получает указанный непосредственно в настройках емэйл адресата.
  /// </summary>
  /// <returns>Указанный непосредственно в настройках емэйл адресата</returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public override List<string> GetSpecificEmails()
  {
    List<string> specificEmails = new List<string>();
    if (string.IsNullOrWhiteSpace(this._emails))
      return specificEmails;
    this._emails = this._emails.Replace("  ", string.Empty);
    this._emails = this._emails.Trim().Replace(" ", string.Empty);
    return ((IEnumerable<string>) this._emails.Split(';')).ToList<string>();
  }

  /// <summary>Получить список ИД пользователей указанных групп.</summary>
  /// <returns>Список ИД пользователей указанных групп</returns>
  private IEnumerable<long> GetIdsFromGroups()
  {
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service == null || this._groupsIDs.Count == 0 ? (IEnumerable<long>) new List<long>(0) : (IEnumerable<long>) service.GetUserIdsFromGroups(this._groupsIDs);
  }

  /// <summary>Получить список ИД пользователей указанных ролей.</summary>
  /// <returns>Список ИД пользователей указанных ролей</returns>
  private IEnumerable<long> GetIdsFromRoles()
  {
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service == null || this._rolesIDs.Count == 0 ? (IEnumerable<long>) new List<long>(0) : (IEnumerable<long>) service.GetUserIdsFromRoles(this._rolesIDs);
  }
}
