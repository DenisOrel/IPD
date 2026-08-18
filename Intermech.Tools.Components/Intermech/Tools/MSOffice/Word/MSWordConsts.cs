// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.MSOffice.Word.MSWordConsts
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Tools.Integrators;
using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.MSOffice.Word;

/// <summary>Константы интегратора с MS Word.</summary>
public static class MSWordConsts
{
  /// <summary>Имя приложения</summary>
  public const string ApplicationName = "Microsoft Word";
  /// <summary>Имя интегратора</summary>
  public const string IntegratorName = "Интегратор с Microsoft Word";
  private static readonly Guid s_integratorId = new Guid("DECC2371-F25F-4EDA-8157-51682EF8C4F4");
  private static readonly IntegratorObject s_integratorRef = new IntegratorObject(MSWordConsts.s_integratorId, "Интегратор с Microsoft Word");

  /// <summary>Возвращает глобальный идентификатор интегратора.</summary>
  public static Guid IntegratorId
  {
    [DebuggerStepThrough] get => MSWordConsts.s_integratorId;
  }

  /// <summary>Возвращает именованную ссылку на объект интегратора.</summary>
  public static IntegratorObject IntegratorRef
  {
    [DebuggerStepThrough] get => MSWordConsts.s_integratorRef;
  }
}
