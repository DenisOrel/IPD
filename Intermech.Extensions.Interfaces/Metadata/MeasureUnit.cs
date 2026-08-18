// Decompiled with JetBrains decompiler
// Type: Intermech.Metadata.MeasureUnit
// Assembly: Intermech.Extensions.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 622A8610-2161-43A4-8678-C2C2D5469500
// Assembly location: D:\IPS\Client\Intermech.Extensions.Interfaces.dll

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Metadata;

public abstract class MeasureUnit
{
  public const string EntityInstanceNameInGenitiveCase = "единицы измерения";
  [NotNull]
  public const long UnknownID = 0;
  [NotNull]
  public static SystemMeasureUnit Minutes = MeasureUnit.Create("cad007db-306c-11d8-b4e9-00304f19f545", nameof (Minutes));
  [NotNull]
  public static SystemMeasureUnit Hours = MeasureUnit.Create("cad007dc-306c-11d8-b4e9-00304f19f545", nameof (Hours));
  [NotNull]
  public static SystemMeasureUnit Days = MeasureUnit.Create("cad00e99-306c-11d8-b4e9-00304f19f545", nameof (Days));
  [NotNull]
  public static SystemMeasureUnit Weeks = MeasureUnit.Create("cadd920e-306c-11d8-b4e9-00304f19f545", nameof (Weeks));
  [NotNull]
  public static SystemMeasureUnit Months = MeasureUnit.Create("cadd9210-306c-11d8-b4e9-00304f19f545", nameof (Months));
  public const string EntityName = "Единицы измерения";
  public const string EntityInstanceName = "Единица измерения";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemMeasureUnit Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return MeasureUnit.Create<MeasureUnit>(guid, true, idName);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  protected static SystemMeasureUnit Create<THolder>([NotNull, NotWhitespace] string guid, bool obligatory, [NotNull, NotWhitespace] string idName) where THolder : MeasureUnit
  {
    Guid guid1 = new Guid(guid);
    return new SystemMeasureUnit(MeasureHelper.Instance.GetMeasureID(guid1), guid1, typeof (THolder), obligatory, idName);
  }

  public abstract class Consts
  {
    public const string MinutesGuid = "cad007db-306c-11d8-b4e9-00304f19f545";
    public const string HoursGuid = "cad007dc-306c-11d8-b4e9-00304f19f545";
    public const string DaysGuid = "cad00e99-306c-11d8-b4e9-00304f19f545";
    public const string WeeksGuid = "cadd920e-306c-11d8-b4e9-00304f19f545";
    public const string MonthsGuid = "cadd9210-306c-11d8-b4e9-00304f19f545";
  }
}
