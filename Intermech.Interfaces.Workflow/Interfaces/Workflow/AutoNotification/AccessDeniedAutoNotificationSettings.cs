// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AccessDeniedAutoNotificationSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки автоуведомлений для оповещения о событиях отказа доступа к объектам
/// </summary>
[Serializable]
public class AccessDeniedAutoNotificationSettings : AutoNotificationSettings
{
  /// <summary>Тип действия</summary>
  public ActionType AccessActionType { get; set; }

  public AccessDeniedAutoNotificationSettings(NotificationEventType eventType, long notifID)
    : base(eventType, notifID)
  {
    this.AccessActionType = ActionType.Any;
  }

  /// <summary>
  /// Записывает настройки в xml-документ.
  /// Обязательно вызывать в оверрайд-методе потомка
  /// </summary>
  /// <param name="xmlDoc">xml-документ.</param>
  /// <returns></returns>
  protected override XmlNode WriteSettingsToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteSettingsToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("AccessType");
    attribute.Value = this.AccessActionType.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
    return xml;
  }

  /// <summary>Читает и пишет в поля настройки из хмл.</summary>
  /// <param name="mainNode">Узел xml с настройками.</param>
  protected override void ReadSettingsFromXml(XmlNode mainNode)
  {
    base.ReadSettingsFromXml(mainNode);
    this.AccessActionType = (ActionType) Enum.Parse(typeof (ActionType), mainNode.Attributes["AccessType"].Value);
  }

  /// <summary>Получить формулу на атрибуты.</summary>
  /// <returns>Формула на атрибуты</returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public override FormulaForAttribute GetFormula() => (FormulaForAttribute) null;
}
