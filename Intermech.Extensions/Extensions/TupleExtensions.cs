// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.TupleExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Extensions;

public static class TupleExtensions
{
  [CanBeNull]
  public static T First<T>([NotNull] this Tuple<T, T> tuple, [NotNull, InstantHandle] Func<T, bool> filter)
  {
    if (filter(tuple.Item1))
      return tuple.Item1;
    if (filter(tuple.Item2))
      return tuple.Item2;
    throw new InvalidOperationException();
  }

  public static bool TryGetFirst<T>([NotNull] this Tuple<T, T> tuple, [NotNull, InstantHandle] Func<T, bool> filter, [CanBeNull] out T result)
  {
    if (filter(tuple.Item1))
    {
      result = tuple.Item1;
      return true;
    }
    if (filter(tuple.Item2))
    {
      result = tuple.Item2;
      return true;
    }
    result = default (T);
    return false;
  }

  [CanBeNull]
  public static T First<T>([NotNull] this Tuple<T, T, T> tuple, [NotNull, InstantHandle] Func<T, bool> filter)
  {
    if (filter(tuple.Item1))
      return tuple.Item1;
    if (filter(tuple.Item2))
      return tuple.Item2;
    if (filter(tuple.Item3))
      return tuple.Item3;
    throw new InvalidOperationException();
  }

  public static bool TryGetFirst<T>([NotNull] this Tuple<T, T, T> tuple, [NotNull, InstantHandle] Func<T, bool> filter, [CanBeNull] out T result)
  {
    if (filter(tuple.Item1))
    {
      result = tuple.Item1;
      return true;
    }
    if (filter(tuple.Item2))
    {
      result = tuple.Item2;
      return true;
    }
    if (filter(tuple.Item3))
    {
      result = tuple.Item3;
      return true;
    }
    result = default (T);
    return false;
  }

  [NotNull]
  public static T FirstNotNull<T>([NotNull] this Tuple<T, T> tuple, [CanBeNull, InstantHandle] Func<T, bool> filter = null) where T : class
  {
    if (filter == null)
    {
      if ((object) tuple.Item1 != null)
        return tuple.Item1;
      if ((object) tuple.Item2 != null)
        return tuple.Item2;
    }
    else
    {
      if ((object) tuple.Item1 != null && filter(tuple.Item1))
        return tuple.Item1;
      if ((object) tuple.Item2 != null && filter(tuple.Item2))
        return tuple.Item2;
    }
    throw new InvalidOperationException();
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  public static bool TryGetFirstNotNull<T>([NotNull] this Tuple<T, T> tuple, out T result) where T : class
  {
    return tuple.TryGetFirstNotNull<T>((TupleExtensions.NotNullFilter<T>) null, out result);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  public static bool TryGetFirstNotNull<T>(
    [NotNull] this Tuple<T, T> tuple,
    [CanBeNull, InstantHandle] TupleExtensions.NotNullFilter<T> filter,
    out T result)
    where T : class
  {
    if (filter == null)
    {
      if ((object) tuple.Item1 == null)
      {
        result = default (T);
        return false;
      }
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) tuple.Item2;
      result = default (T);
      return false;
    }
    if ((object) tuple.Item1 == null)
    {
      result = default (T);
      return false;
    }
    if (filter(tuple.Item1))
    {
      result = tuple.Item1;
      return true;
    }
    if ((object) tuple.Item2 == null)
    {
      result = default (T);
      return false;
    }
    if (filter(tuple.Item2))
    {
      result = tuple.Item2;
      return true;
    }
    result = default (T);
    return false;
  }

  public static bool IsEmpty<T>([NotNull] this Tuple<T, T> tuple) where T : class
  {
    return (object) tuple.Item1 == null;
  }

  public static bool NotEmpty<T>([NotNull] this Tuple<T, T> tuple) where T : class
  {
    return (object) tuple.Item1 != null;
  }

  public static int GetNotNullCount<T>([NotNull] this Tuple<T, T> tuple) where T : class
  {
    if ((object) tuple.Item1 == null)
      return 0;
    return (object) tuple.Item2 == null ? 1 : 2;
  }

  [NotNull]
  public static T FirstNotNull<T>(
    [NotNull] this Tuple<T, T, T> tuple,
    [CanBeNull, InstantHandle] TupleExtensions.NotNullFilter<T> filter = null)
    where T : class
  {
    if (filter == null)
    {
      if ((object) tuple.Item1 != null)
        return tuple.Item1;
      if ((object) tuple.Item2 != null)
        return tuple.Item2;
      if ((object) tuple.Item3 != null)
        return tuple.Item3;
    }
    else
    {
      if ((object) tuple.Item1 != null && filter(tuple.Item1))
        return tuple.Item1;
      if ((object) tuple.Item2 != null && filter(tuple.Item2))
        return tuple.Item2;
      if ((object) tuple.Item3 != null && filter(tuple.Item3))
        return tuple.Item3;
    }
    throw new InvalidOperationException();
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  public static bool TryGetFirstNotNull<T>([NotNull] this Tuple<T, T, T> tuple, out T result) where T : class
  {
    return tuple.TryGetFirstNotNull<T>((TupleExtensions.NotNullFilter<T>) null, out result);
  }

  [ContractAnnotation("=> true, result: notnull; => false, result: null")]
  public static bool TryGetFirstNotNull<T>(
    [NotNull] this Tuple<T, T, T> tuple,
    [CanBeNull, InstantHandle] TupleExtensions.NotNullFilter<T> filter,
    out T result)
    where T : class
  {
    if (filter == null)
    {
      if ((object) tuple.Item1 == null)
      {
        result = default (T);
        return false;
      }
      if ((object) tuple.Item2 == null)
      {
        result = default (T);
        return false;
      }
      // ISSUE: variable of a boxed type
      __Boxed<T> local = (object) tuple.Item3;
      result = default (T);
      return false;
    }
    if ((object) tuple.Item1 == null)
    {
      result = default (T);
      return false;
    }
    if (filter(tuple.Item1))
    {
      result = tuple.Item1;
      return true;
    }
    if ((object) tuple.Item2 == null)
    {
      result = default (T);
      return false;
    }
    if (filter(tuple.Item2))
    {
      result = tuple.Item2;
      return true;
    }
    if ((object) tuple.Item3 == null)
    {
      result = default (T);
      return false;
    }
    if (filter(tuple.Item3))
    {
      result = tuple.Item3;
      return true;
    }
    result = default (T);
    return false;
  }

  public static bool IsEmpty<T>([NotNull] this Tuple<T, T, T> tuple) where T : class
  {
    return (object) tuple.Item1 == null;
  }

  public static bool NotEmpty<T>([NotNull] this Tuple<T, T, T> tuple) where T : class
  {
    return (object) tuple.Item1 != null;
  }

  public static int GetNotNullCount<T>([NotNull] this Tuple<T, T, T> tuple) where T : class
  {
    if ((object) tuple.Item1 == null)
      return 0;
    if ((object) tuple.Item2 == null)
      return 1;
    return (object) tuple.Item3 == null ? 2 : 3;
  }

  public delegate bool NotNullFilter<T>(T item) where T : class;
}
