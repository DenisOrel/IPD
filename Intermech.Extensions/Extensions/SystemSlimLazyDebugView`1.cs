// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SystemSlimLazyDebugView`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

internal sealed class SystemSlimLazyDebugView<T>
{
  [NotNull]
  private readonly SlimLazy<T> _lazy;

  public SystemSlimLazyDebugView([NotNull] SlimLazy<T> lazy) => this._lazy = lazy;

  public bool IsValueCreated => this._lazy.IsValueCreated;

  [CanBeNull]
  public T Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._lazy.ValueForDebugDisplay;
  }

  public bool IsValueFaulted
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._lazy.IsValueFaulted;
  }
}
