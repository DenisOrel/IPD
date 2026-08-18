// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.CollectioncExtensionMethods
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Interfaces.Document;

public static class CollectioncExtensionMethods
{
  [CollectionAccess(CollectionAccessType.None)]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsEmpty<T>(this IEnumerable<T> genericEnumerable)
  {
    return genericEnumerable == null || !genericEnumerable.Any<T>();
  }

  [CollectionAccess(CollectionAccessType.None)]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsEmpty<T>(this ICollection<T> genericCollection)
  {
    return genericCollection == null || genericCollection.Count < 1;
  }

  [CollectionAccess(CollectionAccessType.None)]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsEmpty<T>(this List<T> list) => list == null || list.Count < 1;
}
