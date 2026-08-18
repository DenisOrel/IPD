// Decompiled with JetBrains decompiler
// Type: Intermech.Collections.IDictionaryReadOnlyWrap
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Collections;

public static class IDictionaryReadOnlyWrap
{
  [NotNull]
  public static IReadOnlyDictionary<TKey, TValue> WrapAsReadOnly<TKey, TValue>(
    [NotNull] this IDictionary<TKey, TValue> dictionary)
  {
    return dictionary is IReadOnlyDictionary<TKey, TValue> readOnlyDictionary ? readOnlyDictionary : (IReadOnlyDictionary<TKey, TValue>) new IDictionary2IReadOnlyDictionaryAdapter<TKey, TValue>(dictionary);
  }
}
