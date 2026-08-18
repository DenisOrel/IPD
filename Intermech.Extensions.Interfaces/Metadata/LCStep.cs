// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.LCStep
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

[InitFieldsWithSession]
public abstract class LCStep
{
  public const int UnknownID = -1;
  [NotNull]
  public static readonly SystemLCStep UserExternal = LCStep.Create("cadd9502-306c-11d8-b4e9-00304f19f545", nameof (UserExternal));
  [NotNull]
  public static readonly SystemLCStep UserFired = LCStep.Create("cadd9504-306c-11d8-b4e9-00304f19f545", nameof (UserFired));
  [NotNull]
  public static readonly SystemLCStep UserCreated = LCStep.Create("cadd9503-306c-11d8-b4e9-00304f19f545", nameof (UserCreated));
  public const string EntityName = "Шаги жизненного цикла";
  public const string EntityInstanceName = "Шаг жизненного цикла";
  public const string EntityInstanceNameInGenitiveCase = "шага жизненного цикла";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemLCStep Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return LCStep.Create<LCStep>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemLCStep Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [NotNull, NotWhitespace] string idName) where THolder : LCStep
  {
    return new SystemLCStep(new Guid(guid), typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string UserExternalGuid = "cadd9502-306c-11d8-b4e9-00304f19f545";
    public const string UserFiredGuid = "cadd9504-306c-11d8-b4e9-00304f19f545";
    public const string UserCreatedGuid = "cadd9503-306c-11d8-b4e9-00304f19f545";
  }
}
