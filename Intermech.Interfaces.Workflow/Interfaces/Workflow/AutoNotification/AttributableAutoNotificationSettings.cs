// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AttributableAutoNotificationSettings
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки автоуведомлений для следующих событий объектов и связей:
/// NotificationEventType.DeleteObjectVersion:
/// NotificationEventType.Restore:
/// NotificationEventType.AddLink:
/// NotificationEventType.Cancel:
/// NotificationEventType.CheckIn:
/// NotificationEventType.CheckOut:
/// NotificationEventType.Create:
/// NotificationEventType.CreateVersion:
/// NotificationEventType.NextLCStep:
/// NotificationEventType.NextLCLevel:
/// NotificationEventType.Write:
/// </summary>
[Serializable]
public class AttributableAutoNotificationSettings : AutoNotificationSettings
{
  /// <summary>Условия срабатывания</summary>
  public ActuationCondition _actuationCondition;

  /// <summary>Условия срабатывания</summary>
  public ActuationCondition ActuationCondition => this._actuationCondition;

  public AttributableAutoNotificationSettings(NotificationEventType eventType, long notifID)
    : base(eventType, notifID)
  {
    this._actuationCondition = new ActuationCondition(new FormulaForAttribute(), 0L);
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
    XmlNode xmlNode = this.ActuationCondition.CreateXmlNode(xml.OwnerDocument);
    xml.AppendChild(xmlNode);
    return xml;
  }

  /// <summary>Установить настройки из хмл.</summary>
  /// <param name="mainNode">Узел xml с настройками.</param>
  /// <returns></returns>
  protected override void ReadSettingsFromXml(XmlNode mainNode)
  {
    base.ReadSettingsFromXml(mainNode);
    XmlNodeList xmlNodeList = mainNode.SelectNodes("ActuationCondition");
    if (xmlNodeList == null)
      return;
    foreach (XmlNode xmlNode in xmlNodeList)
      this._actuationCondition.ReadFromXml(xmlNode);
  }

  /// <summary>Получить формулу на атрибуты.</summary>
  /// <returns>Формула на атрибуты</returns>
  /// <exception cref="T:System.NotImplementedException"></exception>
  public override FormulaForAttribute GetFormula() => this.ActuationCondition.FormulaForAttribute;

  /// <summary>Есть ли формула в условиях срабатывания.</summary>
  /// <returns>True - если есть</returns>
  public bool HasActuationConditionFormula() => this.ActuationCondition.HasFormula();
}
