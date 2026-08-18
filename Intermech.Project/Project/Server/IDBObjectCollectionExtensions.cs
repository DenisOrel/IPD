// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Server.IDBObjectCollectionExtensions
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
namespace Intermech.Project.Server;

public static class IDBObjectCollectionExtensions
{
  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TInterface CastToInterface<TInterface>([NotNull] this IDBObjectCollection dbObjectCollection) where TInterface : class, IDBObjectCollection
  {
    return dbObjectCollection.CastInterfaceToOtherInterface<IDBObjectCollection, TInterface>();
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static IDBProjectTaskCollection AsDBTasksCollection(
    [NotNull] this IDBObjectCollection dbObjectCollection)
  {
    return dbObjectCollection.CastInterfaceToOtherInterface<IDBObjectCollection, IDBProjectTaskCollection>();
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static IDBProjectCollection AsDBProjectsCollection(
    [NotNull] this IDBObjectCollection dbObjectCollection)
  {
    return dbObjectCollection.CastInterfaceToOtherInterface<IDBObjectCollection, IDBProjectCollection>();
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static IDBProjectMessageCollection AsDBProjectMessagesCollection(
    [NotNull] this IDBObjectCollection dbObjectCollection)
  {
    return dbObjectCollection.CastInterfaceToOtherInterface<IDBObjectCollection, IDBProjectMessageCollection>();
  }
}
