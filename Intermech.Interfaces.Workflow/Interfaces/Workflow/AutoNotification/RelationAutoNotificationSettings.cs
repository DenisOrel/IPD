// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.RelationAutoNotificationSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Настройки автоуведомлений создания и удаления связи</summary>
[Serializable]
public class RelationAutoNotificationSettings : AttributableAutoNotificationSettings
{
  /// <summary>
  /// Каким типом может быть дочерний либо родительский объект на связи.
  /// </summary>
  public List<int> ObjectTypeIds { get; set; }

  public RelationAutoNotificationSettings(NotificationEventType eventType, long notifID)
    : base(eventType, notifID)
  {
    this.ObjectTypeIds = new List<int>();
  }

  /// <summary>
  /// Записывает настройки в xml-документ.
  /// Обязательно вызывать в оверрайд-методе потомка
  /// </summary>
  /// <param name="xmlDoc">xml-документ.</param>
  /// <returns>Узел xml с основными настройками</returns>
  protected override XmlNode WriteSettingsToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteSettingsToXml(xmlDoc);
    XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, "ObjectTypesIds", string.Empty);
    xml.AppendChild(node);
    AutoNotificationSettings.WriteListToXml<int>(node, this.ObjectTypeIds);
    return xml;
  }

  /// <summary>
  /// Читает и пишет в поля настройки из хмл.
  /// Обязательно вызывать в оверрайд-методе потомка
  /// </summary>
  /// <param name="mainNode">Узел xml с настройками.</param>
  protected override void ReadSettingsFromXml(XmlNode mainNode)
  {
    base.ReadSettingsFromXml(mainNode);
    XmlNodeList xmlNodeList = mainNode.SelectNodes("ObjectTypesIds");
    if (xmlNodeList == null)
      return;
    foreach (XmlNode node in xmlNodeList)
      this.ObjectTypeIds = new List<int>((IEnumerable<int>) AutoNotificationSettings.GetListFromXml<int>(node));
  }
}
