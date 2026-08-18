// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.PhysicalQuantity
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
public abstract class PhysicalQuantity
{
  public const string EntityName = "Физические величины";
  public const string EntityInstanceName = "Физическая величина";
  public const string EntityInstanceNameInGenitiveCase = "физической величины";

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemPhysicalQuantity Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return PhysicalQuantity.Create<PhysicalQuantity>(guid, false, idName);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemPhysicalQuantity CreateObligatory([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return PhysicalQuantity.Create<PhysicalQuantity>(guid, true, idName);
  }

  [NotNull]
  [DebuggerHidden]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemPhysicalQuantity Create<THolder>(
    [NotNull, NotWhitespace] string guid,
    bool obligatory,
    [NotNull, NotWhitespace] string idName)
    where THolder : PhysicalQuantity
  {
    return new SystemPhysicalQuantity(new Guid(guid), typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
  }
}
