// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Records
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics;

[XmlRoot(ElementName = "Record")]
public class Records
{
  /// <summary>Периодичность сбора заданная в планировщике задач</summary>
  [XmlAttribute(AttributeName = "TaskCollectPeriod")]
  public string TaskCollectPeriod { get; set; }

  /// <summary>Дата и время сбора этих данных</summary>
  [XmlAttribute(AttributeName = "DateTime")]
  public string DateTime { get; set; }

  /// <summary>
  /// Список фильтрующих объектов по которым проводилась статистика
  /// </summary>
  [XmlElement(ElementName = "FilterObject")]
  public List<FilteringObject> FilterObject { get; set; }
}
