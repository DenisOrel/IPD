// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SystemLazy2DebugView`1
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace Intermech.Extensions;

internal sealed class SystemLazy2DebugView<T>
{
  [NotNull]
  private readonly Lazy2<T> _lazy;

  public SystemLazy2DebugView([NotNull] Lazy2<T> lazy) => this._lazy = lazy;

  public bool IsValueCreated => this._lazy.IsValueCreated;

  [CanBeNull]
  public T Value
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._lazy.ValueForDebugDisplay;
  }

  public LazyThreadSafetyMode Mode
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._lazy.Mode;
  }

  public bool IsValueFaulted
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => this._lazy.IsValueFaulted;
  }
}
