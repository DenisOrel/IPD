// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.EnumerationCapacityWrapper
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public class EnumerationCapacityWrapper : IEnumerable, ICapacity
{
  [NotNull]
  private readonly IEnumerable _enumeration;
  private readonly int _capacity;

  public EnumerationCapacityWrapper([NotNull] IEnumerable enumeration, [ZeroOrPositiveNumber] int capacity)
  {
    this._enumeration = enumeration;
    this._capacity = capacity;
  }

  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  IEnumerator IEnumerable.GetEnumerator() => this._enumeration.GetEnumerator();

  public int Capacity
  {
    [DebuggerHidden, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._capacity;
  }
}
