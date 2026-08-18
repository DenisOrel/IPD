// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyListWithCast`2
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
public class ReadOnlyListWithCast<T, TMapped> : 
  ReadOnlyList<T>,
  IReadOnlyList<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  ISerializable,
  IDeserializationCallback
  where T : TMapped
{
  public ReadOnlyListWithCast(int capacity, [CanBeNull] IEnumerable<T> enumeration = null)
    : base(capacity, enumeration)
  {
  }

  public ReadOnlyListWithCast([CanBeNull] IEnumerable<T> enumeration, int capacity = 16 /*0x10*/)
    : base(capacity, enumeration)
  {
  }

  protected ReadOnlyListWithCast([NotNull] IReadOnlyList<T> list)
    : base(list)
  {
  }

  protected ReadOnlyListWithCast([NotNull] IList<T> list)
    : base(list)
  {
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ReadOnlyListWithCast<T, TMapped> Wrap([NotNull] IReadOnlyList<T> list)
  {
    return new ReadOnlyListWithCast<T, TMapped>(list);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ReadOnlyListWithCast<T, TMapped> Wrap([NotNull] IList<T> list)
  {
    return new ReadOnlyListWithCast<T, TMapped>(list);
  }

  protected ReadOnlyListWithCast([NotNull] SerializationInfo info, StreamingContext context)
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
}
