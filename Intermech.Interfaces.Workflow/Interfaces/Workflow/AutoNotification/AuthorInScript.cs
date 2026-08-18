// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Workflow.AutoNotification.AuthorInScript
// Assembly: Intermech.Interfaces.Workflow, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2DC6A606-08B5-470B-B668-CAC7730D0728
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Workflow.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Workflow.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Workflow.AutoNotification;

/// <summary>Автор высчитывается скриптом</summary>
[Serializable]
public class AuthorInScript : AdresseeSource
{
  /// <summary>ИД скрипта, который вычисляет автора</summary>
  private long _scriptID;

  public long ScriptID => this._scriptID;

  public AuthorInScript(AdresseeSourceType adresseeSourceType, long scriptID)
    : base(adresseeSourceType)
  {
    this._scriptID = scriptID;
  }

  public AuthorInScript(AdresseeSourceType adresseeSourceType)
    : base(adresseeSourceType)
  {
    this._scriptID = 0L;
  }

  public override List<long> GetAdresseeIds(long initiatorId, List<long> collectedObjects)
  {
    return base.GetAdresseeIds(initiatorId, collectedObjects);
  }

  /// <summary>Создать xml-узел Источника адресата.</summary>
  /// <param name="xmlDoc">Документ, к которому будет прикрепляться узел</param>
  /// <returns>Заполненный данными узел Источника адресата</returns>
  public override XmlNode WriteToXml(XmlDocument xmlDoc)
  {
    XmlNode xml = base.WriteToXml(xmlDoc);
    XmlAttribute attribute = xmlDoc.CreateAttribute("ScriptIdAttr");
    attribute.Value = this._scriptID.ToString();
    if (xml.Attributes != null)
      xml.Attributes.Append(attribute);
    return xml;
  }

  /// <summary>Зачитать данные из хмл.</summary>
  /// <param name="parentNode">Узел хмл.</param>
  public override void ReadFromXml(XmlNode parentNode)
  {
    base.ReadFromXml(parentNode);
    this._scriptID = Convert.ToInt64(parentNode.Attributes["ScriptIdAttr"].Value);
  }
}
