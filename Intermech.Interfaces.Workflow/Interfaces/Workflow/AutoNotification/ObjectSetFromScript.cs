// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.ObjectSetFromScript
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>
/// Настройки для определения набора объектов через скрипт
/// </summary>
[Serializable]
public class ObjectSetFromScript : ObjectSetSource
{
  /// <summary>ИД скрипта</summary>
  private long _scriptID;

  /// <summary>ИД скрипта</summary>
  public long ScriptID => this._scriptID;

  /// <summary>Конструктор</summary>
  internal ObjectSetFromScript(ObjectsCollectMethod collectMethod)
    : base(collectMethod)
  {
    this._scriptID = 0L;
  }

  /// <summary>Конструктор</summary>
  /// <param name="collectMethod">Способ определения набора объектов</param>
  /// <param name="scriptID">ИД скрипта.</param>
  public ObjectSetFromScript(ObjectsCollectMethod collectMethod, long scriptID)
    : base(collectMethod)
  {
    this._scriptID = scriptID;
  }

  /// <summary>Создать xml-узел Способа набора объектов.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Способа набора объектов</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("ScriptIdAttr");
    attribute.Value = this._scriptID.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
    return xml;
  }

  /// <summary>Прочитать данные из XML.</summary>
  /// <param name="parentNode">Родительский узел.</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    base.ReadFromXml(parentNode);
    this._scriptID = Convert.ToInt64(parentNode.Attributes["ScriptIdAttr"].Value);
  }
}
