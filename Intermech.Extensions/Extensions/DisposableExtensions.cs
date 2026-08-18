// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.DisposableExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Extensions;

public static class DisposableExtensions
{
  [NotNull]
  [MustUseReturnValue]
  public static IDisposable MergeWith([NotNull] this IDisposable disposable, [NotNull, NotEmpty, ItemNotNull] params IDisposable[] mergedArray)
  {
    return (IDisposable) new DisposableExtensions.MergedDisposables(Enumeration.Create<IDisposable>(disposable).Concat<IDisposable>((IEnumerable<IDisposable>) mergedArray));
  }

  internal class MergedDisposables : IDisposable
  {
    private bool _disposed;
    [NotNull]
    [NotEmpty]
    [ItemNotNull]
    private readonly IEnumerable<IDisposable> _mergedArray;

    internal MergedDisposables([NotNull, NotEmpty, ItemNotNull] IEnumerable<IDisposable> mergedArray)
    {
      this._mergedArray = mergedArray;
    }

    public void Dispose()
    {
      Intermech.Diagnostics.Check.Assert(!this._disposed);
      foreach (IDisposable merged in this._mergedArray)
        DisposeUtils.SafelyDispose(merged);
      this._disposed = true;
    }
  }
}
