// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.SchemeFilter
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>
/// Класс для Схем поиска данных содержит сам объект фильтра и список корневых элементов
/// </summary>
[Serializable]
public class SchemeFilter
{
  /// <summary>Фильтрующий объект (схема поиска)</summary>
  [XmlElement(ElementName = "FilterObject")]
  public ListItem FilterObject { get; set; }

  /// <summary>Список относящихся к нему корневых объектов</summary>
  [XmlArray("RootObjects")]
  [XmlArrayItem("RootObject")]
  public List<ListItem> RootObjects { get; set; }

  /// <summary>Является ли схемой данных</summary>
  [XmlElement(ElementName = "IsSheme")]
  public bool IsSheme { get; set; }

  public override string ToString() => this.FilterObject.Caption;
}
