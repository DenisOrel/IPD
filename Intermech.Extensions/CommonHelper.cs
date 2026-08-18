// Decompiled with JetBrains decompiler
// Type: Intermech.CommonHelper
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech;

public static class CommonHelper
{
  public static void SafeDisposeAndNull<T>([NotNull] object lockObject, [CanBeNull] ref T obj) where T : class, IDisposable
  {
    if ((object) obj == null)
      return;
    lock (lockObject)
    {
      if ((object) obj == null)
        return;
      obj.Dispose();
      obj = default (T);
    }
  }
}
