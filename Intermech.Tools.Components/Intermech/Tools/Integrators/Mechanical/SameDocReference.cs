// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.SameDocReference
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data.SectionEntities;
using System;

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

internal class SameDocReference : IEquatable<SameDocReference>
{
  private readonly object value;

  public SameDocReference(long objectId)
  {
    this.value = objectId != 0L ? (object) objectId : throw new ArgumentException();
  }

  public SameDocReference(SectionEntity docItem)
  {
    this.value = docItem != null ? (object) docItem : throw new ArgumentNullException(nameof (docItem));
  }

  public object Value => this.value;

  public bool Equals(SameDocReference other)
  {
    if (other != null && other.value.GetType() == this.value.GetType())
    {
      if (other.value.GetType() == typeof (long))
        return (long) this.value == (long) other.value;
      if (other.value.GetType() == typeof (SectionEntity))
        this.value.Equals(other.value);
    }
    return false;
  }

  public override bool Equals(object obj)
  {
    return !(obj is SameDocReference other) ? base.Equals(obj) : this.Equals(other);
  }

  public override int GetHashCode() => this.value.GetHashCode();
}
