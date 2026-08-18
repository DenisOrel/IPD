// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyListCastAdapter`2
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

[Serializable]
internal sealed class ReadOnlyListCastAdapter<T, TMapped> : 
  ReadOnlyListAdapterBase<T>,
  IReadOnlyList<TMapped>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  IEquatable<IReadOnlyList<T>>,
  ISerializable
  where T : TMapped
{
  public ReadOnlyListCastAdapter([NotNull] IReadOnlyList<T> list)
    : base(list)
  {
  }

  private ReadOnlyListCastAdapter([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.List.Select<T, TMapped>((Func<T, TMapped>) (item => (TMapped) item)).GetEnumerator();
  }

  [DebuggerStepThrough]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator()
  {
    return (IEnumerator) this.List.Select<T, TMapped>((Func<T, TMapped>) (item => (TMapped) item)).GetEnumerator();
  }

  [CanBeNull]
  TMapped IReadOnlyList<TMapped>.this[int index]
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (TMapped) this.List[index];
    }
  }
}
