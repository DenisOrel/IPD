// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SimpleEqualityComparer`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class SimpleEqualityComparer<T> : IEqualityComparer<T>
{
  private bool? _referenceType;
  [NotNull]
  private readonly Func<T, T, bool> _compareMethod;

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private bool IsReferenceType()
  {
    return this._referenceType ?? (this._referenceType = new bool?(!typeof (T).IsValueType)).Value;
  }

  public SimpleEqualityComparer([NotNull] Func<T, T, bool> compareMethod)
  {
    this._compareMethod = compareMethod;
  }

  public bool Equals([CanBeNull] T first, [CanBeNull] T second)
  {
    if (this.IsReferenceType())
    {
      bool flag1 = (object) first == null;
      bool flag2 = (object) second == null;
      if (flag1 & flag2)
        return true;
      if (flag1 | flag2)
        return false;
    }
    return this._compareMethod(first, second);
  }

  public int GetHashCode([NotNull] T obj)
  {
    return !this.IsReferenceType() || (object) obj != null ? obj.GetHashCode() : throw new ArgumentNullException(nameof (obj));
  }

  public override string ToString()
  {
    return $"Intermech.Extensions.SimpleEqualityComparer<{typeof (T).FullName}>";
  }
}
