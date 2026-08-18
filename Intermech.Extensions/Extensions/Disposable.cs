// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Disposable
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class Disposable
{
  [NotNull]
  public static IDisposable Empty
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IDisposable) new Disposable.EmptyStub();
    }
  }

  private class EmptyStub : IDisposable
  {
    public void Dispose()
    {
    }
  }
}
