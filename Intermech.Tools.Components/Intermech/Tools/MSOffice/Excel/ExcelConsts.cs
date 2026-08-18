// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Excel.ExcelConsts
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.MSOffice.Excel;

/// <summary>Константы интегратора с MS Excel.</summary>
public static class ExcelConsts
{
  /// <summary>Имя приложения</summary>
  public const string ApplicationName = "Microsoft Excel";
  /// <summary>Имя интегратора</summary>
  public const string IntegratorName = "Интегратор с Microsoft Excel";
  private static readonly Guid s_integratorId = new Guid("9474544F-BE27-487E-8810-4B6CBF5E4583");
  private static readonly IntegratorObject s_integratorRef = new IntegratorObject(ExcelConsts.s_integratorId, "Интегратор с Microsoft Excel");

  /// <summary>Возвращает глобальный идентификатор интегратора.</summary>
  public static Guid IntegratorId
  {
    [DebuggerStepThrough] get => ExcelConsts.s_integratorId;
  }

  /// <summary>Возвращает именованную ссылку на объект интегратора.</summary>
  public static IntegratorObject IntegratorRef
  {
    [DebuggerStepThrough] get => ExcelConsts.s_integratorRef;
  }
}
