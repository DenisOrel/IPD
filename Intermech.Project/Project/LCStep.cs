// Decompiled with JetBrains decompiler
// Type: Intermech.Project.LCStep
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Системные шаги жизненного цикла IPS.Project</summary>
public abstract class LCStep : Intermech.Metadata.LCStep
{
  /// <summary>Проектирование</summary>
  [NotNull]
  public static SystemLCStep Designing = LCStep.Create("cad015e0-306c-11d8-b4e9-00304f19f545", nameof (Designing));
  /// <summary>Выполнение</summary>
  [NotNull]
  public static SystemLCStep Executing = LCStep.Create("cad015dc-306c-11d8-b4e9-00304f19f545", nameof (Executing));
  /// <summary>Разослано исполнителям</summary>
  [NotNull]
  public static SystemLCStep Sent = LCStep.Create("cadd92b1-306c-11d8-b4e9-00304f19f545", nameof (Sent));
  /// <summary>Проверка руководителем</summary>
  [NotNull]
  public static SystemLCStep Validating = LCStep.Create("cadd92ae-306c-11d8-b4e9-00304f19f545", nameof (Validating));
  /// <summary>Выполнено</summary>
  [NotNull]
  public static SystemLCStep Completed = LCStep.Create("cad015e2-306c-11d8-b4e9-00304f19f545", nameof (Completed));
  /// <summary>Прервано</summary>
  [NotNull]
  public static SystemLCStep Terminated = LCStep.Create("cad015e1-306c-11d8-b4e9-00304f19f545", nameof (Terminated));
  /// <summary>Ожидание</summary>
  [NotNull]
  public static SystemLCStep Waiting = LCStep.Create("cadd93a0-306c-11d8-b4e9-00304f19f545", nameof (Waiting));
  /// <summary>Импортировано</summary>
  [NotNull]
  public static SystemLCStep Imported = LCStep.Create("cadd96d0-306c-11d8-b4e9-00304f19f545", nameof (Imported));

  /// <summary>Импортировано</summary>
  [NotNull]
  public static SystemLCStep DirectEdit
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => LCStep.Imported;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static TaskStatus ToTaskStatus([NotEmpty] int lcStep)
  {
    return LCStepVsTaskStatusConnection.LCStepToTaskStatus(lcStep);
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemLCStep Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.LCStep.Create<LCStep>(guid, true, idName);
  }

  /// <summary>Guid-ы системных шагов жизненного цикла IPS.Project (строковое представление Guid-ов)</summary>
  public new abstract class Consts : Intermech.Metadata.LCStep.Consts
  {
    /// <summary>Проектирование</summary>
    public const string DesigningGuid = "cad015e0-306c-11d8-b4e9-00304f19f545";
    /// <summary>Выполнение</summary>
    public const string ExecutingGuid = "cad015dc-306c-11d8-b4e9-00304f19f545";
    /// <summary>Разослано исполнителям</summary>
    public const string SentGuid = "cadd92b1-306c-11d8-b4e9-00304f19f545";
    /// <summary>Проверка руководителем</summary>
    public const string ValidatingGuid = "cadd92ae-306c-11d8-b4e9-00304f19f545";
    /// <summary>Выполнено</summary>
    public const string CompletedGuid = "cad015e2-306c-11d8-b4e9-00304f19f545";
    /// <summary>Прервано</summary>
    public const string TerminatedGuid = "cad015e1-306c-11d8-b4e9-00304f19f545";
    /// <summary>Ожидание</summary>
    public const string WaitingGuid = "cadd93a0-306c-11d8-b4e9-00304f19f545";
    /// <summary>Импортировано</summary>
    public const string ImportedGuid = "cadd96d0-306c-11d8-b4e9-00304f19f545";
  }
}
