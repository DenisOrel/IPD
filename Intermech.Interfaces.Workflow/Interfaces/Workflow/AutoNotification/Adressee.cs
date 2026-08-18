// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.Adressee
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Адресат</summary>
[Serializable]
public abstract class Adressee
{
  /// <summary>Создает объект-болванку адресата.</summary>
  /// <param name="adresseeTypeName">Тип создаваемого адресата.</param>
  /// <returns>Адресат</returns>
  public static Adressee CreateAdressee(string adresseeTypeName)
  {
    Adressee adressee = (Adressee) null;
    switch (adresseeTypeName)
    {
      case "SpecificAdressee":
        adressee = (Adressee) new SpecificAdressee();
        break;
      case "ComputeAdressee":
        adressee = (Adressee) new ComputeAdressee();
        break;
    }
    return adressee;
  }

  /// <summary>Создать xml-узел Адресата.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Адресата</returns>
  public virtual XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    return xmlDoc.CreateNode(XmlNodeType.Element, nameof (Adressee), string.Empty);
  }

  /// <summary>Читает адресат из xml.</summary>
  /// <param name="parentNode">Xml-узел.</param>
  public abstract void ReadFromXml(XmlNode parentNode);

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты.
  /// </summary>
  /// <param name="initiatorId">ИД объекта-инициатора.</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public abstract List<long> CollectObjectsForSearchingAdresseeIds(long initiatorId);

  /// <summary>
  /// Собирает набор объектов, на основании которых будут искаться адресаты, для уведомлений о действиях со связью.
  /// </summary>
  /// <param name="projId">Ид. версии родительского объекта.</param>
  /// <param name="partId">Ид. дочернего объекта.</param>
  /// <param name="partObjectId">Ид. версии дочернего объекта (если не известна, то 0).</param>
  /// <returns>Набор объектов, на основании которых будут искаться адресаты</returns>
  public virtual List<long> CollectObjectsForSearchingAdresseeIdsForRelation(
    long projId,
    long partId,
    long partObjectId)
  {
    return new List<long>(0);
  }

  /// <summary>Получить ИД адресатов.</summary>
  /// <param name="initiatorId">Ид инициатора.</param>
  /// <param name="collectedObjects">Набор объектов, на основании которых будут искаться адресаты.</param>
  /// <returns></returns>
  public abstract List<long> GetIds(long initiatorId, List<long> collectedObjects);

  /// <summary>
  /// Получает указанный непосредственно в настройках емэйл адресата.
  /// </summary>
  /// <returns>Указанный непосредственно в настройках емэйл адресата</returns>
  public abstract List<string> GetSpecificEmails();
}
