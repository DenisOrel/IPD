// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AutoNotificationSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Настройки автоуведомлений</summary>
[Serializable]
public abstract class AutoNotificationSettings
{
  /// <summary>Событие, на которое срабатывает уведомление.</summary>
  public NotificationEventType NotifEventType { get; set; }

  /// <summary>ИД объекта, к которому относится данная настройка</summary>
  public long AutoNotificationID { get; set; }

  /// <summary>Способ уведомления адресата о событии</summary>
  public WayOfNotificationEnum WayOfNotification { get; set; }

  /// <summary>
  /// Для каких типов объектов/связей срабатывает уведомление.
  /// </summary>
  public List<int> FilterTypes { get; set; }

  /// <summary>Адресат</summary>
  public Adressee Adressee { get; set; }

  /// <summary>Текст сообщения</summary>
  public string Message { get; set; }

  /// <summary>Конструктор по умолчанию</summary>
  private AutoNotificationSettings()
  {
    this.WayOfNotification = WayOfNotificationEnum.InternalMail;
    this.FilterTypes = new List<int>();
  }

  /// <summary>Конструктор.</summary>
  /// <param name="eventType">Действие, на которое должно срабатывать уведомление.</param>
  /// <param name="notifID">ИД автоуведомления, для которого создаются настройки</param>
  protected AutoNotificationSettings(NotificationEventType eventType, long notifID)
    : this()
  {
    this.NotifEventType = eventType;
    this.AutoNotificationID = notifID;
    switch (this.NotifEventType)
    {
      case NotificationEventType.AddLink:
        this.Message = NotificationEventType.AddLink.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.RelationMessageBody;
        break;
      case NotificationEventType.DeleteLink:
        this.Message = NotificationEventType.DeleteLink.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.RelationMessageBody;
        break;
      case NotificationEventType.Create:
        this.Message = NotificationEventType.Create.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.ObjectMessageBody;
        break;
      case NotificationEventType.CreateVersion:
        this.Message = NotificationEventType.CreateVersion.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.VersionMessageBody;
        break;
      case NotificationEventType.Delete:
        this.Message = NotificationEventType.Delete.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.ObjectMessageBody;
        break;
      case NotificationEventType.NextLCStep:
        this.Message = NotificationEventType.NextLCStep.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.LCMessageBody;
        break;
      case NotificationEventType.NextLCLevel:
        this.Message = NotificationEventType.NextLCLevel.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.LСLevelMessageBody;
        break;
      case NotificationEventType.Cancel:
        this.Message = NotificationEventType.Cancel.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.ObjectMessageBody;
        break;
      case NotificationEventType.CheckIn:
        this.Message = NotificationEventType.CheckIn.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.ObjectMessageBody;
        break;
      case NotificationEventType.CheckOut:
        this.Message = NotificationEventType.CheckOut.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.ObjectMessageBody;
        break;
      case NotificationEventType.Restore:
        this.Message = NotificationEventType.Restore.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.ObjectMessageBody;
        break;
      case NotificationEventType.Write:
        this.Message = NotificationEventType.Write.GetDescription<NotificationEventType>() + AutoNotificationMessageHelper.AttrMessageBody;
        break;
      case NotificationEventType.GetAccess:
        this.Message = $"{NotificationEventType.GetAccess.GetDescription<NotificationEventType>()}<br>{AutoNotificationMessageHelper.AccessMessageBody}";
        break;
    }
  }

  /// <summary>Прочитать поле Адресат.</summary>
  /// <param name="node">Узел хмл.</param>
  /// <exception cref="T:Intermech.Interfaces.Workflow.AutoNotification.AutoNotificationSettingsException"></exception>
  private void ReadAdresseeFromXmlNode(XmlNode node)
  {
    Adressee adressee = Adressee.CreateAdressee(node.Attributes["Adressee"].Value);
    adressee.ReadFromXml(node);
    this.Adressee = adressee;
  }

  /// <summary>Прочитать поле Способ уведомления.</summary>
  /// <param name="mainNode">Узел хмл</param>
  private void ReadWayOfNotificationFromXmlNode(XmlNode mainNode)
  {
    this.WayOfNotification = (WayOfNotificationEnum) Enum.Parse(typeof (WayOfNotificationEnum), mainNode.Attributes["WayOfNotification"].Value);
  }

  /// <summary>Прочитать поле списка фильтра объектов.</summary>
  /// <param name="node">Узел хмл</param>
  private void ReadFilterTypesFromXmlNode(XmlNode node)
  {
    this.FilterTypes = new List<int>((IEnumerable<int>) AutoNotificationSettings.GetListFromXml<int>(node));
  }

  /// <summary>Прочитать поле Текст сообщения.</summary>
  /// <param name="node">Узел хмл</param>
  private void ReadMessageFromXmlNode(XmlNode node) => this.Message = node.InnerText;

  /// <summary>
  /// Записывает настройки в xml-документ.
  /// Обязательно вызывать в оверрайд-методе потомка
  /// </summary>
  /// <param name="xmlDoc">xml-документ.</param>
  /// <returns>Корневой узел xml с основными настройками</returns>
  protected virtual XmlNode WriteSettingsToXml(XmlDocument xmlDoc)
  {
    XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, nameof (AutoNotificationSettings), string.Empty);
    xmlDoc.AppendChild(node1);
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("AutoNotificationID");
    attribute1.Value = this.AutoNotificationID.ToString();
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("ActionType");
    attribute2.Value = this.NotifEventType.ToString();
    XmlAttribute attribute3 = xmlDoc.CreateAttribute("WayOfNotification");
    attribute3.Value = this.WayOfNotification.ToString();
    if (node1.Attributes != null)
    {
      node1.Attributes.Append(attribute2);
      node1.Attributes.Append(attribute1);
      node1.Attributes.Append(attribute3);
    }
    XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "FilterTypes", string.Empty);
    node1.AppendChild(node2);
    AutoNotificationSettings.WriteListToXml<int>(node2, this.FilterTypes);
    XmlNode node3 = xmlDoc.CreateNode(XmlNodeType.Element, "Message", string.Empty);
    node3.InnerText = this.Message;
    node1.AppendChild(node3);
    XmlNode xml = this.Adressee.WriteToXml(node1.OwnerDocument);
    node1.AppendChild(xml);
    return node1;
  }

  /// <summary>
  /// Читает и пишет в поля настройки из хмл.
  /// Обязательно вызывать в оверрайд-методе потомка
  /// </summary>
  /// <param name="mainNode">Корневой узел xml с настройками.</param>
  protected virtual void ReadSettingsFromXml(XmlNode mainNode)
  {
    this.ReadWayOfNotificationFromXmlNode(mainNode);
    for (int i = 0; i < mainNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = mainNode.ChildNodes[i];
      switch (childNode.Name)
      {
        case "Message":
          this.ReadMessageFromXmlNode(childNode);
          break;
        case "FilterTypes":
          this.ReadFilterTypesFromXmlNode(childNode);
          break;
        case "Adressee":
          this.ReadAdresseeFromXmlNode(childNode);
          break;
      }
    }
  }

  /// <summary>
  /// Создает объект с пустыми настройками для определенного т`ипа уведомления.
  /// </summary>
  /// <param name="notifEventType">Тип события срабатывания уведомления.</param>
  /// <param name="objId">Идентификатор объекта, для которого создаются настройки.</param>
  /// <returns>Настройки автоуведомления</returns>
  public static AutoNotificationSettings CreateEmptyNotifSettings(
    NotificationEventType notifEventType,
    long objId)
  {
    AutoNotificationSettings emptyNotifSettings = (AutoNotificationSettings) null;
    switch (notifEventType)
    {
      case NotificationEventType.AddLink:
      case NotificationEventType.DeleteLink:
        emptyNotifSettings = (AutoNotificationSettings) new RelationAutoNotificationSettings(notifEventType, objId);
        break;
      case NotificationEventType.Create:
      case NotificationEventType.CreateVersion:
      case NotificationEventType.Delete:
      case NotificationEventType.Cancel:
      case NotificationEventType.CheckIn:
      case NotificationEventType.CheckOut:
      case NotificationEventType.Restore:
        emptyNotifSettings = (AutoNotificationSettings) new AttributableAutoNotificationSettings(notifEventType, objId);
        break;
      case NotificationEventType.NextLCStep:
        emptyNotifSettings = (AutoNotificationSettings) new LCStepAutoNotificationSettings(notifEventType, objId);
        break;
      case NotificationEventType.NextLCLevel:
        emptyNotifSettings = (AutoNotificationSettings) new LCLevelAutoNotificationSettings(notifEventType, objId);
        break;
      case NotificationEventType.Write:
        emptyNotifSettings = (AutoNotificationSettings) new AttrChangingAutoNotificationSettings(notifEventType, objId);
        break;
      case NotificationEventType.GetAccess:
        emptyNotifSettings = (AutoNotificationSettings) new AccessDeniedAutoNotificationSettings(notifEventType, objId);
        break;
    }
    return emptyNotifSettings;
  }

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// Метод для общего случая, когда не нужны дополнительные данные из уведомления.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public List<long> CollectObjectsForSearchingAdresseeIds(long initiatorId)
  {
    return this.Adressee.CollectObjectsForSearchingAdresseeIds(initiatorId);
  }

  /// <summary>
  /// Собирает набор объектов для уведомлений об удалении связи, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="projId">Ид. версии родительского объекта.</param>
  /// <param name="partId">Ид. дочернего объекта.</param>
  /// <param name="partObjectId">Ид. версии дочернего объекта (если не известна, то 0).</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public List<long> CollectObjectsForSearchingAdresseeIdsForRelation(
    long projId,
    long partId,
    long partObjectId)
  {
    return this.Adressee.CollectObjectsForSearchingAdresseeIdsForRelation(projId, partId, partObjectId);
  }

  /// <summary>Список адресатов.</summary>
  /// <param name="instanceID">Инициатор.</param>
  /// <param name="collectedObjects">Набор объектов для вычисления адресатов.</param>
  /// <returns>Список адресатов</returns>
  public List<long> GetAdresseeIds(long instanceID, List<long> collectedObjects)
  {
    return this.Adressee.GetIds(instanceID, collectedObjects);
  }

  /// <summary>Получить указанный в настройках емэйл адресата.</summary>
  /// <returns>Указанный непосредственно в настройках емэйл адресата</returns>
  public List<string> GetSpecificEmails() => this.Adressee.GetSpecificEmails();

  /// <summary>Загрузить настройки из xml-документа.</summary>
  /// <param name="mainNode">Корневой узел с настройками.</param>
  public void LoadSettingsFromXml(XmlNode mainNode) => this.ReadSettingsFromXml(mainNode);

  /// <summary>Пишет список в XML-док.</summary>
  /// <typeparam name="T">Для типа int или long</typeparam>
  /// <param name="parentNode">Родительский узел.</param>
  /// <param name="idList">Список идентификаторов</param>
  public static void WriteListToXml<T>(XmlNode parentNode, List<T> idList)
  {
    XmlAttribute attribute1 = parentNode.OwnerDocument.CreateAttribute("ListCount");
    attribute1.Value = idList.Count.ToString();
    parentNode.Attributes.Append(attribute1);
    foreach (T id in idList)
    {
      XmlNode node = parentNode.OwnerDocument.CreateNode(XmlNodeType.Element, "ListItem", string.Empty);
      parentNode.AppendChild(node);
      XmlAttribute attribute2 = parentNode.OwnerDocument.CreateAttribute("ListItemId");
      attribute2.Value = id.ToString();
      node.Attributes.Append(attribute2);
    }
  }

  /// <summary>Получить список из xml.</summary>
  /// <param name="node">Узел, содержащий значения.</param>
  /// <returns>Список</returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public static List<T> GetListFromXml<T>(XmlNode node)
  {
    List<T> listFromXml = new List<T>();
    foreach (XmlNode childNode in node.ChildNodes)
    {
      T obj = (T) Convert.ChangeType((object) childNode.Attributes["ListItemId"].Value, typeof (T));
      listFromXml.Add(obj);
    }
    return listFromXml;
  }

  /// <summary>Создает xml-документ и заполняет его настройками.</summary>
  /// <returns>Xml-документ с настройками</returns>
  public XmlDocument CreateXmlDocWithSettings()
  {
    XmlDocument xmlDoc = new XmlDocument();
    xmlDoc.AppendChild((XmlNode) xmlDoc.CreateXmlDeclaration("1.0", (string) null, (string) null));
    this.WriteSettingsToXml(xmlDoc);
    return xmlDoc;
  }

  /// <summary>Получить формулу на атрибуты.</summary>
  /// <returns>Формула на атрибуты</returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public abstract FormulaForAttribute GetFormula();
}
