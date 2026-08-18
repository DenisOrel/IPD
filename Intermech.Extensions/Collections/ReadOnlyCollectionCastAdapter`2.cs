// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.ReadOnlyCollectionCastAdapter`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

#nullable disable
namespace Intermech.Collections;

[Serializable]
internal sealed class ReadOnlyCollectionCastAdapter<T, TMapped> : 
  ReadOnlyCollectionAdapterBase<T>,
  IReadOnlyCollection<TMapped>,
  IEnumerable<TMapped>,
  IEnumerable,
  ICapacity,
  ISerializable
  where T : TMapped
{
  public ReadOnlyCollectionCastAdapter([NotNull] IReadOnlyCollection<T> collection)
    : base(collection)
  {
  }

  private ReadOnlyCollectionCastAdapter([NotNull] SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  IEnumerator<TMapped> IEnumerable<TMapped>.GetEnumerator()
  {
    return this.Collection.Select<T, TMapped>((Func<T, TMapped>) (item => (TMapped) item)).GetEnumerator();
  }
}
