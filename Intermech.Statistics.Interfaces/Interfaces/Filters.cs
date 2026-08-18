// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.Filters
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using Intermech.Collections;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

/// <summary>
/// Класс, описывающий схемы поиска и выборки команды сбора статистики
/// </summary>
[Serializable]
public class Filters : ICloneable
{
  /// <summary>Выборки</summary>
  [XmlArray("Selections")]
  [XmlArrayItem("Selection")]
  public List<long> Selections { get; set; }

  /// <summary>
  /// Схемы поиска и соответствующие им корневые объекты для поиска
  /// </summary>
  [XmlElement(ElementName = "SearchSchemes")]
  public XmlSerializableDictionary<long, List<long>> SearchSchemes { get; set; }

  public Filters()
  {
    this.Selections = new List<long>();
    this.SearchSchemes = new XmlSerializableDictionary<long, List<long>>();
  }

  public Filters(
    List<long> selections,
    XmlSerializableDictionary<long, List<long>> searchSchemes)
  {
    this.Selections = selections;
    this.SearchSchemes = searchSchemes;
  }

  /// <summary>
  /// Конструктор для инициализации значениями из устаревших полей настроек
  /// </summary>
  /// <param name="filterObjects"></param>
  /// <param name="schemeFilters"></param>
  public Filters(List<ListItem> filterObjects, List<SchemeFilter> schemeFilters)
  {
    this.Selections = new List<long>();
    this.SearchSchemes = new XmlSerializableDictionary<long, List<long>>();
    foreach (ListItem filterObject in filterObjects)
    {
      ListItem filter = filterObject;
      if (!schemeFilters.Any<SchemeFilter>((Func<SchemeFilter, bool>) (x => x.FilterObject.ObjID == filter.ObjID)))
        this.Selections.SafeAdd<long>(filter.ObjID);
    }
    foreach (SchemeFilter schemeFilter in schemeFilters)
    {
      List<long> rootObjects = new List<long>();
      schemeFilter.RootObjects.ForEach((Action<ListItem>) (item => rootObjects.SafeAdd<long>(item.ObjID)));
      List<long> collection;
      if (!this.SearchSchemes.TryGetValue(schemeFilter.FilterObject.ObjID, out collection))
        this.SearchSchemes.Add(schemeFilter.FilterObject.ObjID, rootObjects);
      else
        collection.SafeAddRange<long>((IEnumerable<long>) rootObjects);
    }
  }

  object ICloneable.Clone() => (object) this.Clone();

  public Filters Clone()
  {
    List<long> selections = new List<long>();
    this.Selections.ForEach((Action<long>) (item => selections.Add(item)));
    XmlSerializableDictionary<long, List<long>> searchSchemes = new XmlSerializableDictionary<long, List<long>>();
    foreach (KeyValuePair<long, List<long>> searchScheme in (Dictionary<long, List<long>>) this.SearchSchemes)
    {
      List<long> rootObjects = new List<long>();
      searchScheme.Value.ForEach((Action<long>) (item => rootObjects.Add(item)));
      searchSchemes.Add(searchScheme.Key, rootObjects);
    }
    return new Filters(selections, searchSchemes);
  }
}
