// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.SystemObject
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

[InitFieldsWithSession]
public abstract class SystemObject
{
  [NotNull]
  public static readonly SystemObjectDescriptor StandardCalendar = SystemObject.Create("cad01582-306c-11d8-b4e9-00304f19f545", nameof (StandardCalendar));
  public const string EntityName = "Системные объекты";
  public const string EntityInstanceName = "Системный объект";
  public const string EntityInstanceNameInGenitiveCase = "объекты";

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectDescriptor Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return SystemObject.Create<SystemObject>(guid, false, idName);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectDescriptor CreateObligatory([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return SystemObject.Create<SystemObject>(guid, true, idName);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemObjectDescriptor Create<THolder>(
    [NotNull, NotWhitespace] string guid,
    bool obligatory,
    [NotNull, NotWhitespace] string idName)
    where THolder : SystemObject
  {
    return new SystemObjectDescriptor(new Guid(guid), typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string StandardCalendarGuid = "cad01582-306c-11d8-b4e9-00304f19f545";
  }
}
