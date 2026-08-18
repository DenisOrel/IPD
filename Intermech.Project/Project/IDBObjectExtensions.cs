// Decompiled with JetBrains decompiler
// Type: Intermech.Project.IDBObjectExtensions
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

public static class IDBObjectExtensions
{
  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IDBProjectTask AsIDBTask([NotNull] this IDBObject dbObject)
  {
    return ObjectExtensions.CastToInterface<IDBProjectTask>(dbObject);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IProject AsIProject([NotNull] this IDBObject dbObject)
  {
    return ObjectExtensions.CastToInterface<IProject>(dbObject);
  }
}
