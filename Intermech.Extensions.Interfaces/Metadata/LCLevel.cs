// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.LCLevel
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public abstract class LCLevel
{
  public const string EntityInstanceNameInGenitiveCase = "шага жизненного цикла";
  [NotNull]
  public const int UnknownID = 0;
  [NotNull]
  public static readonly SystemLCLevel Personal = LCLevel.Create("cad00049-306c-11d8-b4e9-00304f19f545", nameof (Personal));
  [NotNull]
  public static readonly SystemLCLevel Created = LCLevel.Create("cad00013-306c-11d8-b4e9-00304f19f545", nameof (Created));
  [NotNull]
  public static readonly SystemLCLevel Signing = LCLevel.Create("cad003be-306c-11d8-b4e9-00304f19f545", nameof (Signing));
  [NotNull]
  public static readonly SystemLCLevel Imported = LCLevel.Create("cad0069a-306c-11d8-b4e9-00304f19f545", nameof (Imported));
  [NotNull]
  public static readonly SystemLCLevel Annulment = LCLevel.Create("cad00012-306c-11d8-b4e9-00304f19f545", nameof (Annulment));
  [NotNull]
  public static readonly SystemLCLevel Manufacturing = LCLevel.Create("cad00011-306c-11d8-b4e9-00304f19f545", nameof (Manufacturing));
  [NotNull]
  public static readonly SystemLCLevel Deleted = LCLevel.Create("cad0000e-306c-11d8-b4e9-00304f19f545", nameof (Deleted));
  [NotNull]
  public static readonly SystemLCLevel Keeping = LCLevel.Create("cad009de-306c-11d8-b4e9-00304f19f545", nameof (Keeping));
  public const string EntityName = "Шаги жизненного цикла";
  public const string EntityInstanceName = "Шаг жизненного цикла";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemLCLevel Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return LCLevel.Create<LCLevel>(guid, false, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemLCLevel Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [NotNull, NotWhitespace] string idName) where THolder : LCLevel
  {
    Guid guid1 = new Guid(guid);
    IMSLifeCycleLevel lcLevel = MetaDataHelperService.Instance.GetLCLevel(guid1);
    return new SystemLCLevel(guid1, lcLevel, typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string PersonalGuid = "cad00049-306c-11d8-b4e9-00304f19f545";
    public const string CreatedGuid = "cad00013-306c-11d8-b4e9-00304f19f545";
    public const string SigningGuid = "cad003be-306c-11d8-b4e9-00304f19f545";
    public const string ImportedGuid = "cad0069a-306c-11d8-b4e9-00304f19f545";
    public const string AnnulmentGuid = "cad00012-306c-11d8-b4e9-00304f19f545";
    public const string ManufacturingGuid = "cad00011-306c-11d8-b4e9-00304f19f545";
    public const string DeletedGuid = "cad0000e-306c-11d8-b4e9-00304f19f545";
    public const string KeepingGuid = "cad009de-306c-11d8-b4e9-00304f19f545";
  }
}
