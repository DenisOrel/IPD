// Decompiled with JetBrains decompiler
// Type: Intermech.Project.TaskObjectType
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Metadata;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Тип объекта "Задачи IMProject"</summary>
public class TaskObjectType : SystemObjectType
{
  /// <summary>Guid типа объекта</summary>
  public const string TypeGuid = "cad00e92-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static TaskObjectType Create([CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    Guid guid = new Guid("cad00e92-306c-11d8-b4e9-00304f19f545");
    return new TaskObjectType(MetaDataHelperService.Instance.GetObjectTypeID(guid), guid, idName);
  }

  private TaskObjectType([NotEmpty] int id, Guid guid, [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, typeof (ObjectTypes), true, idPropertyName)
  {
  }

  protected internal TaskObjectType(
    [NotEmpty] int id,
    [NotEmpty] Guid guid,
    [NotNull] Type holderType,
    bool obligatory,
    [NotNull, NotWhitespace] string idPropertyName)
    : base(id, guid, holderType, obligatory, idPropertyName)
  {
  }

  /// <summary>Системные атрибуты типа объекта</summary>
  public new abstract class Attributes : SystemObjectType.Attributes
  {
    /// <summary>Ссылка на проект IMProject</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Project = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.Project);
    /// <summary>Плановое начало</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType PlanStart = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.PlanStart);
    /// <summary>Плановое окончание</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType PlanFinish = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.PlanFinish);
    /// <summary>Плановый срок выполнения</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType DueDate = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.PlanFinish);
    /// <summary>Плановая длительность</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType PlanDuration = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.PlanDuration);
    /// <summary>Плановые трудозатраты</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Work = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.PlanWork);
    /// <summary>Фактическое начало</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType FactStart = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.FactStart);
    /// <summary>Фактическое окончание</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType FactFinish = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.FactFinish);
    /// <summary>Тип ограничения</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ConstraintType = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.ConstraintType);
    /// <summary>Дата ограничения</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ConstraintDate = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.ConstraintDate);
    /// <summary>Шаблон согласования</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType VerifyScheme = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.VerifyScheme);
    /// <summary>Процент выполнения</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType PercentCompleted = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.PercentCompleted);
    /// <summary>Флаги</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType Flags = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.Flags);
    /// <summary>Приоритет задачи</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType TaskPriority = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.TaskPriority);
    /// <summary>Цвет на диаграмме</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType TaskColor = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.TaskColor);
    /// <summary>Глобальный идентификатор корневого импортированного в проект объекта</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ImportedRootObjectGuid = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.ImportedRootObjectGuid);
    /// <summary>Guid связи в составе импортированного в проект корневого объекта, по которой импортированных объект входит в состав корневого</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ImportedRelationGuid = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.ImportedRelationGuid);
    /// <summary>Ссылка на объект по которому была создана задача в процессе импорта</summary>
    [NotNull]
    public static readonly SystemAttribute4ObjectType ImportedObject = TaskObjectType.Attributes.Create(Intermech.Project.Attributes.ImportedObject);

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static SystemAttribute4ObjectType Create([NotNull] SystemAttribute attribute)
    {
      return SystemObjectType.Attributes.Create("cad00e92-306c-11d8-b4e9-00304f19f545", attribute);
    }
  }

  /// <summary>Возможные способы формирования состава</summary>
  public new abstract class ConsistOf : SystemObjectType.ConsistOf
  {
    /// <summary>Состав задачи</summary>
    [NotNull]
    public static readonly RelationApplicability TaskComposition = TaskObjectType.ConsistOf.LinkedRelation((SystemRelationType) RelationTypes.TaskComposition, "cad00e92-306c-11d8-b4e9-00304f19f545");

    /// <summary>Состав задачи</summary>
    [NotNull]
    public static SystemRelationType Attachments
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return (SystemRelationType) RelationTypes.TaskAttachment;
      }
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string nestedTypeGuid)
    {
      return SystemObjectType.ConsistOf.LinkedRelation(relationType, "cad00e92-306c-11d8-b4e9-00304f19f545", nestedTypeGuid);
    }
  }

  /// <summary>Возможные способы формирования входимости</summary>
  public new abstract class UsedIn : SystemObjectType.UsedIn
  {
    /// <summary>Ссылка на проект IMProject</summary>
    [NotNull]
    public static SystemAttribute4ObjectType Project
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get => TaskObjectType.Attributes.Project;
    }

    /// <summary>Состав задачи</summary>
    [NotNull]
    public static RelationApplicability TaskComposition
    {
      [MethodImpl(MethodImplOptions.AggressiveInlining)] get
      {
        return TaskObjectType.ConsistOf.TaskComposition;
      }
    }

    [NotNull]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static RelationApplicability LinkedRelation(
      [NotNull] SystemRelationType relationType,
      [NotNull, NotEmptyGuid] string parentTypeGuid)
    {
      return SystemObjectType.UsedIn.LinkedRelation(relationType, parentTypeGuid, "cad00e92-306c-11d8-b4e9-00304f19f545");
    }
  }
}
