// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ListWithCast`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[DebuggerDisplay("Count = {Count}")]
[Serializable]
public class ListWithCast<T, TMapped> : 
  SerializableList<T>,
  IEnumerable,
  IList,
  ICollection,
  ISerializable,
  IDeserializationCallback,
  IReadOnlyList<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEquatable<IList<TMapped>>
  where T : TMapped
{
  public ListWithCast(int capacity, [CanBeNull] IEnumerable<T> enumeration = null)
    : base(capacity, enumeration)
  {
  }

  public ListWithCast([CanBeNull] IEnumerable<T> enumeration = null, int capacity = 16 /*0x10*/)
    : base(enumeration, capacity)
  {
  }

  protected ListWithCast([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.Select<T, TMapped>((Func<T, TMapped>) (item => (TMapped) item)).GetEnumerator();
  }

  [CanBeNull]
  TMapped IReadOnlyList<TMapped>.this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (TMapped) this[index];
    }
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public bool Equals([CanBeNull] IList<TMapped> other)
  {
    if (other == null)
      return false;
    return this == other || this.Equals((object) other);
  }
}
