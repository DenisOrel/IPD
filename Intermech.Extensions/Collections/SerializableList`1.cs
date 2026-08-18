// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.SerializableList`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[DebuggerDisplay("Count = {Count}")]
[DefaultMember("Item")]
[Serializable]
public class SerializableList<T> : 
  List<T>,
  IList<T>,
  ICollection<T>,
  IEnumerable<T>,
  IEnumerable,
  IReadOnlyList<T>,
  IReadOnlyCollection<T>,
  IList,
  ICollection,
  ISerializable,
  IDeserializationCallback
{
  private const string ListSerializeName = "AsArray";

  public SerializableList(int capacity, [CanBeNull] IEnumerable<T> enumeration = null)
    : base(Math.Max(enumeration.TryGetCountOrCapacity<T>() ?? 16 /*0x10*/, capacity))
  {
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  public SerializableList([CanBeNull] IEnumerable<T> enumeration = null, int capacity = 16 /*0x10*/)
    : base(Math.Max(enumeration.TryGetCountOrCapacity<T>() ?? 16 /*0x10*/, capacity))
  {
    if (enumeration == null)
      return;
    this.AddRange(enumeration);
  }

  protected SerializableList([NotNull] SerializationInfo info, StreamingContext context)
    : this()
  {
    T[] collection = (T[]) info.GetValue("AsArray", typeof (T[]));
    if (collection != null && collection.Length != 0)
      this.AddRange((IEnumerable<T>) collection);
    this.OnDeserialization((object) this);
  }

  public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    T[] array = new T[this.Count];
    this.CopyTo(array, 0);
    info.AddValue("AsArray", (object) array);
  }

  public virtual void OnDeserialization([CanBeNull] object sender)
  {
  }
}
