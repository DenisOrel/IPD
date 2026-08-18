// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.LCLevelAutoNotificationSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки автоуведомлений для изменения уровня продвижения объекта
/// </summary>
[Serializable]
public class LCLevelAutoNotificationSettings : AttributableAutoNotificationSettings
{
  /// <summary>ID уровня продвижения.</summary>
  public int LCLevelID { get; set; }

  public LCLevelAutoNotificationSettings(NotificationEventType eventType, long notifID)
    : base(eventType, notifID)
  {
    this.LCLevelID = 0;
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
    XmlAttribute attribute = xmlDoc.CreateAttribute("LcLevelId");
    attribute.Value = this.LCLevelID.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
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
    this.LCLevelID = Convert.ToInt32(mainNode.Attributes["LcLevelId"].Value);
  }
}
