// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.IndexNotNull`2
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;

#nullable disable
namespace Intermech.Extensions;

public class IndexNotNull<TKey, TValue> where TValue : class
{
  [NotNull]
  protected readonly IndexNotNull<TKey, TValue>.GetterFunc Getter;

  public IndexNotNull([NotNull] IndexNotNull<TKey, TValue>.GetterFunc getter)
  {
    this.Getter = getter;
  }

  [NotNull]
  public virtual TValue this[[NotNull] TKey key] => Check.Result.NotNull<TValue>(this.Getter(key));

  [NotNull]
  public delegate TValue GetterFunc(TKey key) where TValue : class;
}
