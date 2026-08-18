// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ObjectSetFromApplicabilityOrComposition
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using Intermech.Workflow;
using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки для получения набора обрабатываемых объектов по составу или входимости объекта-инициатора
/// </summary>
[Serializable]
public class ObjectSetFromApplicabilityOrComposition : ObjectSetSource
{
  /// <summary>
  /// Типы объектов в которые входит/из которых состоит объект-инициатор
  /// </summary>
  private List<int> _objTypesIDs;
  /// <summary>
  /// Типы связей, которыми входит/включает в себя объект инициатор
  /// </summary>
  private List<int> _relTypesIDs;
  /// <summary>Правило подбора версий</summary>
  private long _versionRuleID;

  /// <summary>
  /// Типы объектов в которые входит/из которых состоит объект-инициатор
  /// </summary>
  public List<int> ObjTypesIDs => this._objTypesIDs;

  /// <summary>
  /// Типы связей, которыми входит/включает в себя объект инициатор
  /// </summary>
  public List<int> RelTypesIDs => this._relTypesIDs;

  /// <summary>Правило подбора версий</summary>
  public long VersionRuleID => this._versionRuleID;

  /// <summary>Конструктор</summary>
  internal ObjectSetFromApplicabilityOrComposition(ObjectsCollectMethod collectMethod)
    : base(collectMethod)
  {
    this._objTypesIDs = new List<int>();
    this._relTypesIDs = new List<int>();
    this._versionRuleID = wfConsts.FiltrationBaseVersionsID;
  }

  /// <summary>Конструктор</summary>
  /// <param name="collectMethod">Способ определения набора объектов</param>
  /// <param name="objTypesIDs">Список типов объектов.</param>
  /// <param name="relTypesIDs">Список типов связей</param>
  /// <param name="versionRuleID">Правило подбора версий</param>
  public ObjectSetFromApplicabilityOrComposition(
    ObjectsCollectMethod collectMethod,
    List<int> objTypesIDs,
    List<int> relTypesIDs,
    long versionRuleID)
    : base(collectMethod)
  {
    this._objTypesIDs = new List<int>((IEnumerable<int>) objTypesIDs);
    this._relTypesIDs = new List<int>((IEnumerable<int>) relTypesIDs);
    this._versionRuleID = versionRuleID;
  }

  /// <summary>Создать xml-узел Способа набора объектов.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Способа набора объектов</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("VersionRuleID");
    attribute.Value = this._versionRuleID.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
    XmlNode node1 = xmlDoc.CreateNode(XmlNodeType.Element, "ObjectTypesIds", string.Empty);
    xml.AppendChild(node1);
    AutoNotificationSettings.WriteListToXml<int>(node1, this._objTypesIDs);
    XmlNode node2 = xmlDoc.CreateNode(XmlNodeType.Element, "RelationTypesIds", string.Empty);
    xml.AppendChild(node2);
    AutoNotificationSettings.WriteListToXml<int>(node2, this._relTypesIDs);
    return xml;
  }

  /// <summary>Прочитать данные из XML.</summary>
  /// <param name="parentNode">Родительский узел.</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    base.ReadFromXml(parentNode);
    this._versionRuleID = Convert.ToInt64(parentNode.Attributes["VersionRuleID"].Value);
    for (int i = 0; i < parentNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = parentNode.ChildNodes[i];
      switch (childNode.Name)
      {
        case "ObjectTypesIds":
          this._objTypesIDs = AutoNotificationSettings.GetListFromXml<int>(childNode);
          break;
        case "RelationTypesIds":
          this._relTypesIDs = AutoNotificationSettings.GetListFromXml<int>(childNode);
          break;
      }
    }
  }

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public override List<long> CollectObjectsForFindingAdresseeIds(long initiatorId)
  {
    switch (this.ObjectsCollectMethod)
    {
      case ObjectsCollectMethod.InitiatorApplicability:
        return this.GetObjectApplicability(initiatorId);
      case ObjectsCollectMethod.InitiatorComposition:
        return this.GetObjectComposition(initiatorId);
      default:
        return new List<long>(0);
    }
  }

  /// <summary>Получить состав объекта.</summary>
  /// <param name="initiatorId">Ид объекта-инициатора.</param>
  /// <returns>Состав объекта</returns>
  private List<long> GetObjectComposition(long initiatorId)
  {
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service != null ? service.GetObjectComposition(initiatorId, this._objTypesIDs, this._relTypesIDs, this._versionRuleID) : new List<long>(0);
  }

  /// <summary>Получить применяемость объекта.</summary>
  /// <param name="initiatorId">Ид объекта-инициатора.</param>
  /// <returns>Применяемость объекта</returns>
  private List<long> GetObjectApplicability(long initiatorId)
  {
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service != null ? service.GetObjectApplicability(initiatorId, this._objTypesIDs, this._relTypesIDs, this._versionRuleID) : new List<long>(0);
  }
}
