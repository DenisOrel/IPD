// Decompiled with JetBrains decompiler
// Type: Intermech.Calendars.WorkPeriodsList
// Assembly: Intermech.Calendars, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8CDCC38D-5D33-4955-8468-4C0264D69139
// Assembly location: D:\IPS\Client\Intermech.Calendars.dll
// XML documentation location: D:\IPS\Client\Intermech.Calendars.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Calendars;

[Serializable]
public class WorkPeriodsList : 
  List<WorkTime>,
  IXmlReaderSupport,
  IEquatable<ICollection<WorkTime>>,
  IList<WorkTime>,
  ICollection<WorkTime>,
  IEnumerable<WorkTime>,
  IEnumerable,
  IList,
  ICollection,
  IReadOnlyList<WorkTime>,
  IReadOnlyCollection<WorkTime>
{
  public WorkPeriodsList()
    : base(2)
  {
  }

  public WorkPeriodsList([NotNull] XmlReader reader) => this.ReadFromXml(reader);

  /// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
  /// <param name="obj">The object to compare with the current object.</param>
  /// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    return obj is ICollection<WorkTime> other && this.Equals(other);
  }

  public override int GetHashCode() => base.GetHashCode();

  public string XmlNodeName => throw new NotImplementedException("WorkPeriodsList.XmlNodeName");

  public void ReadFromXml([NotNull] XmlReader reader)
  {
    if (this.Count > 0)
      this.Clear();
    reader.ReadEnumeration((XmlReaderExtensions.LoadEnumerationItemMethod) (() => this.Add(new WorkTime(reader))));
  }

  /// <summary>Tests if this WorkTimePeriodList is considered equal to another</summary>
  /// <param name="otherCollection">The work time period list to compare to this object</param>
  /// <returns>true if the objects are considered equal, false if they are not</returns>
  public bool Equals([CanBeNull] ICollection<WorkTime> otherCollection)
  {
    if (otherCollection == null)
      return false;
    if (this == otherCollection)
      return true;
    return this.Count == otherCollection.Count && this.All<WorkTime, WorkTime>((IEnumerable<WorkTime>) otherCollection, (Func<WorkTime, WorkTime, bool>) ((thisListElement, otherCollectionElement) => thisListElement.Equals(otherCollectionElement)));
  }
}
