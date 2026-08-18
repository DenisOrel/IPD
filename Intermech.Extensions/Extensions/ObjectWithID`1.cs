// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.ObjectWithID`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Extensions;

[DebuggerDisplay("ID={ID}")]
[Serializable]
public abstract class ObjectWithID<TId> : 
  IObjectWithID<TId>,
  IEquatable<ObjectWithID<TId>>,
  IEquatable<IObjectWithID<TId>>,
  ISerializable
  where TId : struct
{
  [CanBeEmpty]
  public TId ID { get; protected set; }

  protected ObjectWithID()
  {
  }

  protected ObjectWithID(TId id) => this.ID = id;

  protected ObjectWithID([NotNull] SerializationInfo info, StreamingContext context)
  {
    this.ID = info.GetValue<TId>(nameof (ID));
  }

  public virtual void GetObjectData([NotNull] SerializationInfo info, StreamingContext context)
  {
    info.AddValue("ID", (object) this.ID);
  }

  public override int GetHashCode() => this.ID.GetHashCode();

  public override bool Equals([CanBeNull] object obj)
  {
    if (obj == null)
      return false;
    if (this == obj)
      return true;
    switch (obj)
    {
      case ObjectWithID<TId> other1:
        return this.Equals(other1);
      case IObjectWithID<TId> other2:
        return this.Equals(other2);
      default:
        return false;
    }
  }

  public bool Equals([CanBeNull] IObjectWithID<TId> other)
  {
    if (other == null)
      return false;
    return this == other || this.ID.Equals((object) other.ID);
  }

  public bool Equals([CanBeNull] ObjectWithID<TId> other)
  {
    if (other == null)
      return false;
    return this == other || this.ID.Equals((object) other.ID);
  }
}
