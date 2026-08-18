// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ObjectTypes
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Metadata;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Системные типы объектов IPS.Project</summary>
public abstract class ObjectTypes : Intermech.Metadata.ObjectTypes
{
  /// <summary>Сценарий инициализации каждой отдельной задачи сразу после создания в процессе импорта</summary>
  [NotNull]
  public static readonly SystemObjectType ScriptInitTaskAfterImport = ObjectTypes.Create("cadd94b8-306c-11d8-b4e9-00304f19f545", nameof (ScriptInitTaskAfterImport));
  /// <summary>Сценарий обработки всех импортированных задач IMProject по окончании импорта в проект структуры объекта</summary>
  [NotNull]
  public static readonly SystemObjectType ScriptInitAfterImportTasks = ObjectTypes.Create("cadd94b8-306c-11d8-b4e9-00304f19f545", nameof (ScriptInitAfterImportTasks));
  /// <summary>Абстрактный тип "Объекты IMProject"</summary>
  [NotNull]
  public static readonly SystemObjectType ProjectObjects = ObjectTypes.Create("cad00e90-306c-11d8-b4e9-00304f19f545", nameof (ProjectObjects));
  /// <summary>Проект IMProject</summary>
  [NotNull]
  public static readonly ProjectObjectType Project = ProjectObjectType.Create(nameof (Project));
  /// <summary>Задача IMProject</summary>
  [NotNull]
  public static readonly TaskObjectType Task = TaskObjectType.Create(nameof (Task));
  /// <summary>Взаимосвязь IMProject</summary>
  [NotNull]
  public static readonly DependencyObjectType Dependency = DependencyObjectType.Create(nameof (Dependency));
  /// <summary>Сообщение IMProject</summary>
  [NotNull]
  public static readonly ProjectMessageObjectType ProjectMessage = ProjectMessageObjectType.Create(nameof (ProjectMessage));
  /// <summary>Расцеховочный элемент</summary>
  [NotNull]
  public static readonly SystemObjectType RouteElement = ObjectTypes.Create("cad001e8-306c-11d8-b4e9-00304f19f545", nameof (RouteElement));
  /// <summary>Базовый техпроцесс</summary>
  [NotNull]
  public static readonly SystemObjectType BasicTechprocess = ObjectTypes.Create("cad00185-306c-11d8-b4e9-00304f19f545", nameof (BasicTechprocess));
  /// <summary>Расцеховочный маршрут</summary>
  [NotNull]
  public static readonly SystemObjectType TechRoute = ObjectTypes.Create("cad001e5-306c-11d8-b4e9-00304f19f545", nameof (TechRoute));
  /// <summary>Сохранённые настройки вывода на печать проекта IMProject</summary>
  [NotNull]
  public static readonly SystemObjectType PrintScheme = ObjectTypes.Create("cadd961e-306c-11d8-b4e9-00304f19f545", nameof (PrintScheme));

  [NotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  private static SystemObjectType Create([NotNull, NotWhitespace] string guid, [CallerMemberName, NotNull, NotWhitespace] string idName = "")
  {
    return Intermech.Metadata.ObjectTypes.Create<ObjectTypes>(guid, true, idName);
  }

  /// <summary>Guid-ы и идентификаторы системных типов объектов IPS (строковое представление Guid-ов)</summary>
  public new abstract class Consts : Intermech.Metadata.ObjectTypes.Consts
  {
    /// <summary>Объекты IMProject</summary>
    public const string IMProjectObjectGuid = "cad00e90-306c-11d8-b4e9-00304f19f545";
    /// <summary>Проект IMProject</summary>
    public const string ProjectGuid = "cad00e91-306c-11d8-b4e9-00304f19f545";
    /// <summary>Задача IMProject</summary>
    public const string TaskGuid = "cad00e92-306c-11d8-b4e9-00304f19f545";
    /// <summary>Взаимосвязь IMProject</summary>
    public const string DependencyGuid = "cad00e9a-306c-11d8-b4e9-00304f19f545";
    /// <summary>Сценарий инициализации каждой отдельной задачи сразу после создания в процессе импорта</summary>
    public const string InitTasksScriptTypeGuid = "cadd94b8-306c-11d8-b4e9-00304f19f545";
    /// <summary>Сценарий обработки всех импортированных задач IMProject по окончании импорта в проект структуры объекта</summary>
    public const string ScriptInitAfterImportTasksGuid = "cadd94b8-306c-11d8-b4e9-00304f19f545";
    /// <summary>Сообщение IMProject</summary>
    public const string ProjectMessageGuid = "cadd91f6-306c-11d8-b4e9-00304f19f545";
    /// <summary>Расцеховочный элемент</summary>
    public const string RouteElementGuid = "cad001e8-306c-11d8-b4e9-00304f19f545";
    /// <summary>Базовый техпроцесс</summary>
    public const string BasicTechprocessGuid = "cad00185-306c-11d8-b4e9-00304f19f545";
    /// <summary>Расцеховочный маршрут</summary>
    public const string TechRouteGuid = "cad001e5-306c-11d8-b4e9-00304f19f545";
    /// <summary>Сохранённые настройки вывода на печать проекта IMProject</summary>
    public const string PrintSchemeGuid = "cadd961e-306c-11d8-b4e9-00304f19f545";
  }
}
