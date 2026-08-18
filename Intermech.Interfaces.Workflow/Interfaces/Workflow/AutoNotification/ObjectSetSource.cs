// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ObjectSetSource
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Источник определения набора объектов</summary>
[Serializable]
public class ObjectSetSource
{
  /// <summary>Способ определения набора объектов</summary>
  private ObjectsCollectMethod objectsCollectMethod;

  /// <summary>Способ определения набора объектов</summary>
  public ObjectsCollectMethod ObjectsCollectMethod => this.objectsCollectMethod;

  /// <summary>Конструктор по умолчанию</summary>
  internal ObjectSetSource() => this.objectsCollectMethod = ObjectsCollectMethod.None;

  /// <summary>Конструктор.</summary>
  /// <param name="collectMethod">Способ определения набора объектов.</param>
  public ObjectSetSource(ObjectsCollectMethod collectMethod)
  {
    this.objectsCollectMethod = collectMethod;
  }

  /// <summary>Создает объект Источник определения набора объектов.</summary>
  /// <param name="objectSetSourceAttrValue">The object set source attribute value.</param>
  /// <returns>Объект Источник определения набора объектов</returns>
  public static ObjectSetSource CreateObjSetSource(string objectSetSourceAttrValue)
  {
    ObjectsCollectMethod collectMethod = (ObjectsCollectMethod) Enum.Parse(typeof (ObjectsCollectMethod), objectSetSourceAttrValue);
    ObjectSetSource objSetSource;
    switch (collectMethod)
    {
      case ObjectsCollectMethod.InitiatorApplicability:
      case ObjectsCollectMethod.InitiatorComposition:
        objSetSource = (ObjectSetSource) new ObjectSetFromApplicabilityOrComposition(collectMethod);
        break;
      case ObjectsCollectMethod.FindByScriptObjects:
        objSetSource = (ObjectSetSource) new ObjectSetFromScript(collectMethod);
        break;
      case ObjectsCollectMethod.GetBySearchSchemeObjects:
        objSetSource = (ObjectSetSource) new ObjectSetFromSearchScheme(collectMethod);
        break;
      default:
        objSetSource = new ObjectSetSource(collectMethod);
        break;
    }
    return objSetSource;
  }

  /// <summary>Создать xml-узел Способа набора объектов.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Способа набора объектов</returns>
  public virtual XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, nameof (ObjectSetSource), string.Empty);
    XmlAttribute attribute = xmlDoc.CreateAttribute("ObjectSetObtainMethod");
    attribute.Value = this.ObjectsCollectMethod.ToString();
    if (node.Attributes != null)
      node.Attributes.Append(attribute);
    return node;
  }

  /// <summary>Прочитать данные из XML.</summary>
  /// <param name="parentNode">Родительский узел.</param>
  public virtual void ReadFromXml(XmlNode parentNode)
  {
  }

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// Не для событий, связанных со связями.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>
  /// Набор объектов, на основании которых будут искаться адресаты.
  /// Пустой список, если ничего не найдено.
  /// </returns>
  public virtual List<long> CollectObjectsForFindingAdresseeIds(long initiatorId)
  {
    List<long> longList = new List<long>();
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    if (service == null)
      return longList;
    switch (this.objectsCollectMethod)
    {
      case ObjectsCollectMethod.Initiator:
        longList.Add(initiatorId);
        break;
      case ObjectsCollectMethod.InitiatorArticles:
        List<long> articles = service.GetArticles(initiatorId);
        longList.AddRange((IEnumerable<long>) articles);
        break;
      default:
        return longList;
    }
    return longList;
  }

  /// <summary>
  /// Для уведомлений об удалении связи. Собирает набор объектов, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="projId">Ид. версии родительского объекта.</param>
  /// <param name="partId">Ид. дочернего объекта.</param>
  /// <param name="partObjectId">Ид. версии дочернего объекта (если не известна, то 0).</param>
  /// <returns>
  /// Набор объектов, на основании которых будут искаться адресаты.
  /// Пустой список, если ничего не найдено.
  /// </returns>
  public List<long> CollectObjectsForFindingAdresseeIdsForRelation(
    long projId,
    long partId,
    long partObjectId)
  {
    List<long> longList = new List<long>();
    IAutoNotificationsService service = (IAutoNotificationsService) ApplicationServices.Container.GetService(typeof (IAutoNotificationsService));
    if (service == null)
      return longList;
    switch (this.objectsCollectMethod)
    {
      case ObjectsCollectMethod.RelationPart:
        List<long> relationPartIds1 = service.GetRelationPartIds(partId, partObjectId);
        longList.AddRange((IEnumerable<long>) relationPartIds1);
        break;
      case ObjectsCollectMethod.RelationProject:
        if (projId != 0L)
        {
          longList.Add(projId);
          break;
        }
        break;
      case ObjectsCollectMethod.RelationPartAndProjects:
        if (projId != 0L)
          longList.Add(projId);
        List<long> relationPartIds2 = service.GetRelationPartIds(partId, partObjectId);
        longList.AddRange((IEnumerable<long>) relationPartIds2);
        break;
      default:
        return longList;
    }
    return longList;
  }
}
