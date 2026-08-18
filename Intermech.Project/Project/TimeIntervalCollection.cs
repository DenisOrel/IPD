// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TimeIntervalCollection
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;

#nullable disable
namespace Intermech.Project;

[Serializable]
public class TimeIntervalCollection : Collection<TimeInterval>, ISerializable
{
  internal bool _AllowDuplicates;

  public TimeIntervalCollection()
  {
  }

  protected TimeIntervalCollection([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    this.EntityType = info.GetType("EntityType");
    this.AddRange((IEnumerable<TimeInterval>) info.GetValue<TimeInterval[]>("Items"));
  }

  [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    string str = this.EntityType.Assembly.FullName;
    char ch = ',';
    int length = str.IndexOf(ch);
    if (length >= 0)
      str = str.Substring(0, length);
    info.AddValue("EntityType", (object) $"{this.EntityType.FullName}, {str}");
    TimeInterval[] timeIntervalArray = new TimeInterval[this.Count];
    for (int index = 0; index < this.Count; ++index)
      timeIntervalArray[index] = this[index];
    info.AddValue("Items", (object) timeIntervalArray);
  }

  protected override void OnListChanged(ListChangedEventArgs e)
  {
    base.OnListChanged(e);
    if (e.ListChangedType != ListChangedType.ItemAdded && e.ListChangedType != ListChangedType.ItemChanged || this._AllowDuplicates)
      return;
    double num = 0.0;
    for (int index = 0; index < this.Count; ++index)
    {
      if (index != e.NewIndex)
      {
        TimeInterval timeInterval = this[index];
        if (timeInterval.Start < num)
        {
          this.RemoveAt(e.NewIndex);
          throw new ArgumentOutOfRangeException("Cannot update time interval collection: intervals must be sorted based on their start time.");
        }
        num = timeInterval.Start;
      }
    }
  }

  public double Duration
  {
    get => this.Sum<TimeInterval>((Func<TimeInterval, double>) (interval => interval.Duration));
  }

  public void Merge([NotNull] TimeIntervalCollection src)
  {
    this.AddRange(src.Where<TimeInterval>((Func<TimeInterval, bool>) (srcTi => this.All<TimeInterval>((Func<TimeInterval, bool>) (ti => ti.Merge(srcTi))))));
  }

  public override int GetHashCode()
  {
    int count = this.Count;
    foreach (TimeInterval timeInterval in (System.Collections.ObjectModel.Collection<TimeInterval>) this)
    {
      count *= 17;
      if (timeInterval != null)
        count += timeInterval.GetHashCode();
    }
    return count;
  }

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    if (!(obj is TimeIntervalCollection intervalCollection) || this.Count != intervalCollection.Count)
      return false;
    for (int index = this.Count - 1; index >= 0; --index)
    {
      if (!intervalCollection[index].Equals((object) this[index]))
        return false;
    }
    return true;
  }
}
