// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.FilteringObject
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics;

[XmlRoot(ElementName = "FilterObject")]
public class FilteringObject
{
  /// <summary>Имя фильтрующего объекта для отображения на графике</summary>
  [XmlAttribute(AttributeName = "name")]
  public string Name { get; set; }

  /// <summary>Идентификатор фильтрующего объекта в базе данных</summary>
  [XmlAttribute(AttributeName = "id")]
  public string ObjectID { get; set; }

  /// <summary>Значение колонки (recordCount)</summary>
  public string Value { get; set; }
}
