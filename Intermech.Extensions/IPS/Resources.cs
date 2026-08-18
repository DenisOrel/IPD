// Decompiled with JetBrains decompiler
// Type: IPS.Resources
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

#nullable disable
namespace IPS;

public abstract class Resources
{
  [NotNull]
  private static readonly ConcurrentDictionary<(string Assembly, string ResourceName), object> _cache = new ConcurrentDictionary<(string, string), object>();

  [ContractAnnotation("throwExceptionIfNotFound:true => NotNull; throwExceptionIfNotFound:false => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CachedGet<T, TResourceNotFoundException>(
    [NotNull] Assembly assembly,
    [NotNull, NotWhitespace] string resourceName,
    [NotNull] Func<Stream, T> loadFromStream,
    bool throwExceptionIfNotFound = true)
    where T : class
    where TResourceNotFoundException : ResourceNotFoundException
  {
    string assemblyName = assembly.GetName().Name;
    return (T) Resources._cache.GetOrAdd((assemblyName, resourceName), (Func<(string, string), object>) (_ =>
    {
      Stream manifestResourceStream = assembly.GetManifestResourceStream(assembly.GetName().Name + resourceName);
      if (manifestResourceStream != null && manifestResourceStream.Length > 0L)
      {
        T obj = loadFromStream(manifestResourceStream);
        if ((object) obj != null)
          return (object) obj;
      }
      if (throwExceptionIfNotFound)
        throw (object) Helper.CreateInstance<TResourceNotFoundException, string, string, string>(assemblyName, resourceName, (string) null);
      return (object) null;
    }));
  }
}
