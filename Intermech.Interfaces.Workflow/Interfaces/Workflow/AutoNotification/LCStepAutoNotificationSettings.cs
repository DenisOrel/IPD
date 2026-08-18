// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.LCStepAutoNotificationSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки автоуведомлений для изменения шага жц объекта
/// </summary>
[Serializable]
public class LCStepAutoNotificationSettings : AttributableAutoNotificationSettings
{
  /// <summary>ID шага жц.</summary>
  public int LCStepID { get; set; }

  /// <summary>ID схемы, к которой относится шаг жц.</summary>
  public int SchemeID { get; set; }

  public LCStepAutoNotificationSettings(NotificationEventType eventType, long notifID)
    : base(eventType, notifID)
  {
    this.LCStepID = -1;
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
    XmlAttribute attribute1 = xmlDoc.CreateAttribute("LcSchemeId");
    attribute1.Value = this.SchemeID.ToString();
    XmlAttribute attribute2 = xmlDoc.CreateAttribute("LcStepId");
    attribute2.Value = this.LCStepID.ToString();
    if (xml.Attributes != null)
    {
      xml.Attributes.Append(attribute2);
      xml.Attributes.Append(attribute1);
    }
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
    this.LCStepID = Convert.ToInt32(mainNode.Attributes["LcStepId"].Value);
    this.SchemeID = Convert.ToInt32(mainNode.Attributes["LcSchemeId"].Value);
  }
}
