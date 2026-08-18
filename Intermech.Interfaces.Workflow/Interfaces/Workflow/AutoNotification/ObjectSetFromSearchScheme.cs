// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ObjectSetFromSearchScheme
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки для определения набора объектов через схему поиска
/// </summary>
[Serializable]
public class ObjectSetFromSearchScheme : ObjectSetSource
{
  /// <summary>Схема поиска</summary>
  private long _searchSchemeID;

  /// <summary>Схема поиска</summary>
  public long SearchSchemeID => this._searchSchemeID;

  /// <summary>Конструктор.</summary>
  internal ObjectSetFromSearchScheme(ObjectsCollectMethod collectMethod)
    : base(collectMethod)
  {
    this._searchSchemeID = 0L;
  }

  /// <summary>Конструктор.</summary>
  /// <param name="collectMethod">Способ определения набора объектов</param>
  /// <param name="searchSchemeID">ИД схемы поиска</param>
  public ObjectSetFromSearchScheme(ObjectsCollectMethod collectMethod, long searchSchemeID)
    : base(collectMethod)
  {
    this._searchSchemeID = searchSchemeID;
  }

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public override List<long> CollectObjectsForFindingAdresseeIds(long initiatorId)
  {
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    return service != null ? service.GetObjectsWithSearchScheme(initiatorId, this._searchSchemeID) : new List<long>(0);
  }

  /// <summary>Создать xml-узел Способа набора объектов.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Способа набора объектов</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("SearchSchemeId");
    attribute.Value = this._searchSchemeID.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
    return xml;
  }

  /// <summary>Прочитать данные из XML.</summary>
  /// <param name="parentNode">Родительский узел.</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    base.ReadFromXml(parentNode);
    this._searchSchemeID = Convert.ToInt64(parentNode.Attributes["SearchSchemeId"].Value);
  }
}
