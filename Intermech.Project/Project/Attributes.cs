// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Attributes
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Metadata;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Системные типы атрибутов IPS.Project</summary>
public abstract class Attributes : Intermech.Metadata.Attributes
{
  /// <summary>Ссылка на проект IMProject</summary>
  [NotNull]
  public static readonly SystemAttribute Project = Attributes.Create("cad00ea2-306c-11d8-b4e9-00304f19f545", nameof (Project));
  /// <summary>Плановая длительность</summary>
  [NotNull]
  public static readonly SystemAttribute PlanDuration = Attributes.Create("cad00e98-306c-11d8-b4e9-00304f19f545", nameof (PlanDuration));
  /// <summary>Плановые трудозатраты</summary>
  [NotNull]
  public static readonly SystemAttribute PlanWork = Attributes.Create("cad00e97-306c-11d8-b4e9-00304f19f545", nameof (PlanWork));
  /// <summary>Фактическое начало</summary>
  [NotNull]
  public static readonly SystemAttribute FactStart = Attributes.Create("cad015d9-306c-11d8-b4e9-00304f19f545", nameof (FactStart));
  /// <summary>Фактическое окончание</summary>
  [NotNull]
  public static readonly SystemAttribute FactFinish = Attributes.Create("cad015da-306c-11d8-b4e9-00304f19f545", nameof (FactFinish));
  /// <summary>Данные проекта</summary>
  [NotNull]
  public static readonly SystemAttribute ProjectData = Attributes.Create("cad00ea0-306c-11d8-b4e9-00304f19f545", nameof (ProjectData));
  /// <summary>Тип ограничения</summary>
  [NotNull]
  public static readonly SystemAttribute ConstraintType = Attributes.Create("cad00ea3-306c-11d8-b4e9-00304f19f545", nameof (ConstraintType));
  /// <summary>Дата ограничения</summary>
  [NotNull]
  public static readonly SystemAttribute ConstraintDate = Attributes.Create("cad00ea4-306c-11d8-b4e9-00304f19f545", nameof (ConstraintDate));
  /// <summary>Шаблон согласования</summary>
  [NotNull]
  public static readonly SystemAttribute VerifyScheme = Attributes.Create("cadd92bb-306c-11d8-b4e9-00304f19f545", nameof (VerifyScheme));
  /// <summary>Процент выполнения</summary>
  [NotNull]
  public static readonly SystemAttribute PercentCompleted = Attributes.Create("cad014a1-306c-11d8-b4e9-00304f19f545", nameof (PercentCompleted));
  /// <summary>Флаги</summary>
  [NotNull]
  public static readonly SystemAttribute Flags = Attributes.Create("cad00072-306c-11d8-b4e9-00304f19f545", nameof (Flags));
  /// <summary>Приоритет задачи</summary>
  [NotNull]
  public static readonly SystemAttribute TaskPriority = Attributes.Create("cad015e4-306c-11d8-b4e9-00304f19f545", nameof (TaskPriority));
  /// <summary>Исходит из задачи</summary>
  [NotNull]
  public static readonly SystemAttribute FromTask = Attributes.Create("cad00e9b-306c-11d8-b4e9-00304f19f545", nameof (FromTask));
  /// <summary>Входит в задачу</summary>
  [NotNull]
  public static readonly SystemAttribute ToTask = Attributes.Create("cad00e9c-306c-11d8-b4e9-00304f19f545", nameof (ToTask));
  /// <summary>Тип зависимости</summary>
  [NotNull]
  public static readonly SystemAttribute DependencyType = Attributes.Create("cad00e9e-306c-11d8-b4e9-00304f19f545", nameof (DependencyType));
  /// <summary>Запаздывание</summary>
  [NotNull]
  public static readonly SystemAttribute Lag = Attributes.Create("cadd93fa-306c-11d8-b4e9-00304f19f545", nameof (Lag));
  /// <summary>Число единиц ресурса</summary>
  [NotNull]
  public static readonly SystemAttribute ResourceUnits = Attributes.Create("cad00e9f-306c-11d8-b4e9-00304f19f545", nameof (ResourceUnits));
  /// <summary>Признак руководителя</summary>
  [NotNull]
  public static readonly SystemAttribute ResourceIsChief = Attributes.Create("cadd92b6-306c-11d8-b4e9-00304f19f545", nameof (ResourceIsChief));
  /// <summary>Вид вложения ImProject</summary>
  [NotNull]
  public static readonly SystemAttribute AttachKind = Attributes.Create("cadd9385-306c-11d8-b4e9-00304f19f545", nameof (AttachKind));
  /// <summary>Ответ руководителя</summary>
  [NotNull]
  public static readonly SystemAttribute ManagerAnswer = Attributes.Create("cadd9b36-306c-11d8-b4e9-00304f19f545", nameof (ManagerAnswer));
  /// <summary>Цвет на диаграмме</summary>
  [NotNull]
  public static readonly SystemAttribute TaskColor = Attributes.Create("cadd9b94-306c-11d8-b4e9-00304f19f545", nameof (TaskColor));
  /// <summary>Глобальный идентификатор корневого импортированного в проект объекта</summary>
  [NotNull]
  public static readonly SystemAttribute ImportedRootObjectGuid = Attributes.Create("cadd95ae-306c-11d8-b4e9-00304f19f545", nameof (ImportedRootObjectGuid));
  /// <summary>Guid связи в составе импортированного в проект корневого объекта, по которой импортированных объект входит в состав корневого</summary>
  [NotNull]
  public static readonly SystemAttribute ImportedRelationGuid = Attributes.Create("cadd95a6-306c-11d8-b4e9-00304f19f545", nameof (ImportedRelationGuid));
  /// <summary>Ссылка на объект по которому была создана задача в процессе импорта</summary>
  [NotNull]
  public static readonly SystemAttribute ImportedObject = Attributes.Create("cadd95a7-306c-11d8-b4e9-00304f19f545", nameof (ImportedObject));
  /// <summary>Дата последней синхронизации состава</summary>
  [NotNull]
  public static readonly SystemAttribute LastSyncDate = Attributes.Create("cadd95ab-306c-11d8-b4e9-00304f19f545", nameof (LastSyncDate));
  /// <summary>Шаблон для новых задач</summary>
  [NotNull]
  public static readonly SystemAttribute Prototype = Attributes.Create("cadd95ac-306c-11d8-b4e9-00304f19f545", nameof (Prototype));
  /// <summary>Сценарий инициализации задач</summary>
  [NotNull]
  public static readonly SystemAttribute InitScript = Attributes.Create("cadd95ad-306c-11d8-b4e9-00304f19f545", nameof (InitScript));
  [NotNull]
  private static readonly HashSet<int> _protectedIDsHash = new HashSet<int>();
  [CanBeNull]
  private static List<int> _protectedIDsList;

  /// <summary>Плановое начало</summary>
  [NotNull]
  public static SystemAttribute PlanStart
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Workflow.Attributes.Started;
  }

  /// <summary>Плановое окончание</summary>
  [NotNull]
  public static SystemAttribute PlanFinish
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Workflow.Attributes.DueDate;
  }

  /// <summary>Проверка возможно ли редактировать атрибут, в обход редактора</summary>
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static bool IsProtected([NotEmpty] int attributeID)
  {
    return Attributes._protectedIDsHash.Contains(attributeID);
  }

  /// <summary>Инициализация списка атрибутов, которые невозможно редактировать в обход редактора</summary>
  internal static void InitProtectedIDs([NotNull] IUserSession session)
  {
    Attributes._protectedIDsHash.AddRange<int>((IEnumerable<int>) new int[15]
    {
      (int) (IpsMetadataEntityBase<int>) Attributes.Project,
      (int) (IpsMetadataEntityBase<int>) Attributes.PlanStart,
      (int) (IpsMetadataEntityBase<int>) Attributes.FactStart,
      (int) (IpsMetadataEntityBase<int>) Attributes.FactFinish,
      (int) (IpsMetadataEntityBase<int>) Attributes.PlanFinish,
      (int) (IpsMetadataEntityBase<int>) Attributes.PlanDuration,
      (int) (IpsMetadataEntityBase<int>) Attributes.PlanWork,
      (int) (IpsMetadataEntityBase<int>) Attributes.ProjectData,
      (int) (IpsMetadataEntityBase<int>) Attributes.ConstraintType,
      (int) (IpsMetadataEntityBase<int>) Attributes.ConstraintDate,
      (int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted,
      (int) (IpsMetadataEntityBase<int>) Attributes.Flags,
      (int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject,
      (int) (IpsMetadataEntityBase<int>) Attributes.ImportedRelationGuid,
      (int) (IpsMetadataEntityBase<int>) Attributes.ImportedRootObjectGuid
    });
    Attributes._protectedIDsList = Attributes._protectedIDsHash.ToList<int>(Attributes._protectedIDsHash.Count);
  }

  /// <summary>Список атрибутов, которые невозможно редактировать в обход редактора</summary>
  [NotNull]
  [ItemNotEmpty]
  public static IReadOnlyList<int> ProtectedIDsList
  {
    get => (IReadOnlyList<int>) Attributes._protectedIDsList;
  }

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemAttribute Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.Attributes.Create<Attributes>(guid, true, idName);
  }

  /// <summary>Guid-ы и идентификаторы системных атрибутов IPS (строковое представление Guid-ов)</summary>
  public new abstract class Consts : Intermech.Metadata.Attributes.Consts
  {
    /// <summary>Ссылка на проект IMProject</summary>
    public const string ProjectGuid = "cad00ea2-306c-11d8-b4e9-00304f19f545";
    /// <summary>Плановая длительность</summary>
    public const string PlanDurationGuid = "cad00e98-306c-11d8-b4e9-00304f19f545";
    /// <summary>Плановые трудозатраты</summary>
    public const string PlanWorkGuid = "cad00e97-306c-11d8-b4e9-00304f19f545";
    /// <summary>Фактическое начало</summary>
    public const string FactStartGuid = "cad015d9-306c-11d8-b4e9-00304f19f545";
    /// <summary>Фактическое окончание</summary>
    public const string FactFinishGuid = "cad015da-306c-11d8-b4e9-00304f19f545";
    /// <summary>Данные проекта</summary>
    public const string ProjectDataGuid = "cad00ea0-306c-11d8-b4e9-00304f19f545";
    /// <summary>Тип ограничения</summary>
    public const string ConstraintTypeGuid = "cad00ea3-306c-11d8-b4e9-00304f19f545";
    /// <summary>Дата ограничения</summary>
    public const string ConstraintDateGuid = "cad00ea4-306c-11d8-b4e9-00304f19f545";
    /// <summary>Шаблон согласования</summary>
    public const string VerifySchemeGuid = "cadd92bb-306c-11d8-b4e9-00304f19f545";
    /// <summary>Процент выполнения</summary>
    public const string PercentCompletedGuid = "cad014a1-306c-11d8-b4e9-00304f19f545";
    /// <summary>Флаги</summary>
    public const string FlagsGuid = "cad00072-306c-11d8-b4e9-00304f19f545";
    /// <summary>Приоритет задачи</summary>
    public const string TaskPriorityGuid = "cad015e4-306c-11d8-b4e9-00304f19f545";
    /// <summary>Исходит из задачи</summary>
    public const string FromTaskGuid = "cad00e9b-306c-11d8-b4e9-00304f19f545";
    /// <summary>Входит в задачу</summary>
    public const string ToTaskGuid = "cad00e9c-306c-11d8-b4e9-00304f19f545";
    /// <summary>Тип зависимости</summary>
    public const string DependencyTypeGuid = "cad00e9e-306c-11d8-b4e9-00304f19f545";
    /// <summary>Запаздывание</summary>
    public const string LagGuid = "cadd93fa-306c-11d8-b4e9-00304f19f545";
    /// <summary>Число единиц ресурса</summary>
    public const string ResourceUnitsGuid = "cad00e9f-306c-11d8-b4e9-00304f19f545";
    /// <summary>Признак руководителя</summary>
    public const string ResourceIsChiefGuid = "cadd92b6-306c-11d8-b4e9-00304f19f545";
    /// <summary>Вид вложения ImProject</summary>
    public const string AttachKindGuid = "cadd9385-306c-11d8-b4e9-00304f19f545";
    /// <summary>Ответ руководителя</summary>
    public const string ManagerAnswerGuid = "cadd9b36-306c-11d8-b4e9-00304f19f545";
    /// <summary>Цвет на диаграмме</summary>
    public const string TaskColorGuid = "cadd9b94-306c-11d8-b4e9-00304f19f545";
    /// <summary>Guid связи в составе импортированного в проект корневого объекта, по которой импортированных объект входит в состав корневого</summary>
    public const string ImportedRelationGuidStr = "cadd95a6-306c-11d8-b4e9-00304f19f545";
    /// <summary>Глобальный идентификатор корневого импортированного в проект объекта</summary>
    public const string ImportedRootObjectGuidStr = "cadd95ae-306c-11d8-b4e9-00304f19f545";
    /// <summary>Ссылка на объект по которому была создана задача в процессе импорта</summary>
    public const string ImportedObjectGuid = "cadd95a7-306c-11d8-b4e9-00304f19f545";
    /// <summary>Дата последней синхронизации состава</summary>
    public const string LastSyncDateGuid = "cadd95ab-306c-11d8-b4e9-00304f19f545";
    /// <summary>Шаблон для новых задач</summary>
    public const string PrototypeGuid = "cadd95ac-306c-11d8-b4e9-00304f19f545";
    /// <summary>Сценарий инициализации задач</summary>
    public const string InitScriptGuid = "cadd95ad-306c-11d8-b4e9-00304f19f545";
  }
}
