// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectExtensions
// Assembly: Intermech.Extensions.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A1017829-B851-420B-83EC-75723A20702A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Extensions.Server.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Kernel;

public static class DBObjectExtensions
{
  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObject CastToObjectType<TDBObject>([NotNull] this DBObject dbObject) where TDBObject : DBObject
  {
    return dbObject.CastClassToClass<TDBObject>();
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TDBObject CastToObjectType<TDBObject, TInvalidCastException>([NotNull] this DBObject dbObject)
    where TDBObject : DBObject
    where TInvalidCastException : InvalidCastException
  {
    return dbObject.CastClassToClass<TDBObject, TInvalidCastException>();
  }
}
