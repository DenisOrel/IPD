// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ComputeAdressee
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Вычисляемый адресат</summary>
[Serializable]
public class ComputeAdressee : Adressee
{
  /// <summary>Способ определения набора обрабатываемых объектов</summary>
  private ObjectSetSource _objectSetSource;
  /// <summary>Источник адресата</summary>
  private AdresseeSource _adresseeSource;

  /// <summary>Способ определения набора обрабатываемых объектов</summary>
  public ObjectSetSource ObjectSetSource => this._objectSetSource;

  /// <summary>Источник адресата</summary>
  public AdresseeSource AdresseeSource => this._adresseeSource;

  /// <summary>Конструктор по умолчанию</summary>
  internal ComputeAdressee()
  {
    this._adresseeSource = (AdresseeSource) null;
    this._objectSetSource = (ObjectSetSource) null;
  }

  /// <summary>Конструктор</summary>
  /// <param name="adresseeSource">Источник адресата.</param>
  /// <param name="objectSetSource">Источник определения набора объектов.</param>
  public ComputeAdressee(AdresseeSource adresseeSource, ObjectSetSource objectSetSource)
  {
    this._adresseeSource = adresseeSource;
    this._objectSetSource = objectSetSource;
  }

  /// <summary>Создать xml-узел Адресата.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Адресата</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml1 = base.WriteToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("Adressee");
    attribute.Value = nameof (ComputeAdressee);
    if (xml1.Attributes != null)
      xml1.Attributes.Append(attribute);
    XmlNode xml2 = this.ObjectSetSource.WriteToXml(xmlDoc);
    xml1.AppendChild(xml2);
    XmlNode xml3 = this.AdresseeSource.WriteToXml(xmlDoc);
    xml1.AppendChild(xml3);
    return xml1;
  }

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>
  /// Набор объектов, на основании которых будут искаться адресаты
  /// </returns>
  public override List<long> CollectObjectsForSearchingAdresseeIds(long initiatorId)
  {
    return this.ObjectSetSource.CollectObjectsForFindingAdresseeIds(initiatorId);
  }

  /// <summary>
  /// Собирает набор объектов для уведомлений об удалении связи, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="projId">Ид. версии родительского объекта.</param>
  /// <param name="partId">Ид. дочернего объекта.</param>
  /// <param name="partObjectId">Ид. версии дочернего объекта (если не известна, то 0).</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public override List<long> CollectObjectsForSearchingAdresseeIdsForRelation(
    long projId,
    long partId,
    long partObjectId)
  {
    return this.ObjectSetSource.CollectObjectsForFindingAdresseeIdsForRelation(projId, partId, partObjectId);
  }

  /// <summary>Получить ИД адресатов.</summary>
  /// <param name="initiatorId">Ид инициатора.</param>
  /// <param name="collectedObjects">Набор объектов, на основании которых будут искаться адресаты.</param>
  /// <returns></returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public override List<long> GetIds(long initiatorId, List<long> collectedObjects)
  {
    return this.AdresseeSource.GetAdresseeIds(initiatorId, collectedObjects);
  }

  /// <summary>
  /// Получает указанный непосредственно в настройках емэйл адресата.
  /// </summary>
  /// <returns>Указанный непосредственно в настройках емэйл адресата</returns>
  public override List<string> GetSpecificEmails() => new List<string>();

  /// <summary>Читает адресат из xml.</summary>
  /// <param name="parentNode">Xml-узел.</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    for (int i = 0; i < parentNode.ChildNodes.Count; ++i)
    {
      XmlNode childNode = parentNode.ChildNodes[i];
      switch (childNode.Name)
      {
        case "ObjectSetSource":
          this._objectSetSource = ObjectSetSource.CreateObjSetSource(childNode.Attributes["ObjectSetObtainMethod"].Value);
          this._objectSetSource.ReadFromXml(childNode);
          break;
        case "AdresseeSource":
          this._adresseeSource = AdresseeSource.CreateAdresseeSource(childNode.Attributes["AdresseeSourceType"].Value);
          this._adresseeSource.ReadFromXml(childNode);
          break;
      }
    }
  }
}
