// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.NullableExtensions
// Assembly: Intermech.Extensions, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 412E7A14-75DD-4B05-B0B0-85953DB2EF77
// Assembly location: D:\IPS\Client\Intermech.Extensions.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class NullableExtensions
{
  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CheckInitializedIn<T>([CanBeNull] this T? nullable, [NotNull, NotWhitespace] string containerName) where T : struct
  {
    return nullable.HasValue ? nullable.Value : throw new NotYetInitializedException(containerName, (string) null, (Exception) new NullReferenceException());
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CheckInitializedIn<T>([CanBeNull] this T? nullable, [NotNull] Type staticContainerType) where T : struct
  {
    return nullable.HasValue ? nullable.Value : throw NotYetInitializedException.ForContainer(staticContainerType.FullName ?? string.Empty, (Exception) new NullReferenceException());
  }

  [Pure]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static T CheckInitializedIn<T>([CanBeNull] this T? nullable, [NotNull] object container) where T : struct
  {
    return nullable.HasValue ? nullable.Value : throw NotYetInitializedException.ForContainer(container.GetType().Name, (Exception) new NullReferenceException());
  }
}
