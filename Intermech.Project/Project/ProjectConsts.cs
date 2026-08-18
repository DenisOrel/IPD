// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectConsts
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Calendars;
using Intermech.Metadata;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Project;

[Obsolete("Class will be removed in future releases!")]
public static class ProjectConsts
{
  [Obsolete("Use SystemGUIDs.objtypeProjects")]
  public const string ProjectsTypeGuidStr = "cad00812-306c-11d8-b4e9-00304f19f545";

  [NotNull]
  [Obsolete("Use ObjectTypes.Consts.ProjectGuid")]
  public static string ProjectObjectsTypeGuidStr => "cad00e91-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use ObjectTypes.ProjectObjects.Guid")]
  public static Guid ProjectObjectsTypeGuid => ObjectTypes.ProjectObjects.Guid;

  [Obsolete("Use ObjectTypes.ProjectObjects.ID")]
  public static int ProjectObjectsTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectObjects;
  }

  [Obsolete("Use ObjectTypes.Project.Guid")]
  public static Guid ProjectsTypeGuid => ObjectTypes.Project.Guid;

  [Obsolete("Use ObjectTypes.Project.ID")]
  public static int ProjectsTypeID => (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project;

  [Obsolete("Use Helper.ProjectsIsLocalType")]
  public static bool ProjectsIsLocalType => Helper.ProjectsIsLocalType;

  [NotNull]
  [Obsolete("Use ObjectTypes.Consts.TaskGuid")]
  public static string TasksTypeGuidStr => "cad00e92-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use ObjectTypes.Task.Guid")]
  public static Guid TasksTypeGuid => ObjectTypes.Task.Guid;

  [Obsolete("Use ObjectTypes.Task.ID")]
  public static int TasksTypeID => (int) (IpsMetadataEntityBase<int>) ObjectTypes.Task;

  [Obsolete("Use Attributes.Project.Guid")]
  public static Guid AttrProjectGuid => Attributes.Project.Guid;

  [Obsolete("Use Attributes.Project.ID")]
  public static int AttrProjectID => (int) (IpsMetadataEntityBase<int>) Attributes.Project;

  [Obsolete("Use Attributes.PlanStart.ID")]
  public static int AttrStartID => (int) (IpsMetadataEntityBase<int>) Attributes.PlanStart;

  [Obsolete("Use Attributes.FactStart.Guid")]
  public static Guid AttrFactStartGuid => Attributes.FactStart.Guid;

  [Obsolete("Use Attributes.FactStart.ID")]
  public static int AttrFactStartID => (int) (IpsMetadataEntityBase<int>) Attributes.FactStart;

  [Obsolete("Use Attributes.FactFinish.Guid")]
  public static Guid AttrFactFinishGuid => Attributes.FactFinish.Guid;

  [Obsolete("Use Attributes.FactFinish.ID")]
  public static int AttrFactFinishID => (int) (IpsMetadataEntityBase<int>) Attributes.FactFinish;

  [Obsolete("Use Attributes.PlanFinish.ID")]
  public static int AttrDueDateID => (int) (IpsMetadataEntityBase<int>) Attributes.PlanFinish;

  [Obsolete("Use Attributes.PlanDuration.Guid")]
  public static Guid AttrDurationGuid => Attributes.PlanDuration.Guid;

  [Obsolete("Use Attributes.PlanDuration.ID")]
  public static int AttrDurationID => (int) (IpsMetadataEntityBase<int>) Attributes.PlanDuration;

  [Obsolete("Use Attributes.PlanWork.Guid")]
  public static Guid AttrWorkGuid => Attributes.PlanWork.Guid;

  [Obsolete("Use Attributes.PlanWork.ID")]
  public static int AttrWorkID => (int) (IpsMetadataEntityBase<int>) Attributes.PlanWork;

  [Obsolete("Use Attributes.ProjectData.Guid")]
  public static Guid AttrProjectDataGuid => Attributes.ProjectData.Guid;

  [Obsolete("Use Attributes.ProjectData.ID")]
  public static int AttrProjectDataID => (int) (IpsMetadataEntityBase<int>) Attributes.ProjectData;

  [Obsolete("Use Attributes.ConstraintType.Guid")]
  public static Guid AttrConstraintTypeGuid => Attributes.ConstraintType.Guid;

  [Obsolete("Use Attributes.ConstraintType.ID")]
  public static int AttrConstraintTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ConstraintType;
  }

  [Obsolete("Use Attributes.ConstraintDate.Guid")]
  public static Guid AttrConstraintDateGuid => Attributes.ConstraintDate.Guid;

  [Obsolete("Use Attributes.ConstraintDate.ID")]
  public static int AttrConstraintDateID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ConstraintDate;
  }

  [Obsolete("Use Attributes.Calendar.ID")]
  public static int AttrCalendarID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Calendar;

  [Obsolete("Use Attributes.VerifyScheme.Guid")]
  public static Guid AttrVerifySchemeGuid => Attributes.VerifyScheme.Guid;

  [Obsolete("Use Attributes.VerifyScheme.ID")]
  public static int AttrVerifySchemeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.VerifyScheme;
  }

  [Obsolete("Use PhysicalQuantity.Time.Guid")]
  public static Guid TimeMeasureObjectGuid => PhysicalQuantity.Time.Guid;

  [Obsolete("Use PhysicalQuantity.Time.ID")]
  public static long TimeMeasureObjectID
  {
    get => (long) (IpsMetadataEntityBase<long>) PhysicalQuantity.Time;
  }

  [Obsolete("Use MeasureUnit.Minutes.Guid")]
  public static Guid MeasureUnitMinutesGuid => MeasureUnit.Minutes.Guid;

  [Obsolete("Use MeasureUnit.Minutes.ID")]
  public static long MeasureUnitMinutesID
  {
    get => (long) (IpsMetadataEntityBase<long>) MeasureUnit.Minutes;
  }

  [Obsolete("Use MeasureUnit.Hours.Guid")]
  public static Guid MeasureUnitHoursGuid => MeasureUnit.Hours.Guid;

  [Obsolete("Use MeasureUnit.Hours.ID")]
  public static long MeasureUnitHoursID => (long) (IpsMetadataEntityBase<long>) MeasureUnit.Hours;

  [Obsolete("Use MeasureUnit.Days.Guid")]
  public static Guid MeasureUnitDaysGuid => MeasureUnit.Days.Guid;

  [Obsolete("Use MeasureUnit.Days.ID")]
  public static long MeasureUnitDaysID => (long) (IpsMetadataEntityBase<long>) MeasureUnit.Days;

  [NotNull]
  [Obsolete("Use MeasureUnit.Days.Descriptor")]
  public static MeasureDescriptor MeasureUnitDays => MeasureUnit.Days.Descriptor;

  [Obsolete("Use MeasureUnit.Weeks.Guid")]
  public static Guid MeasureUnitWeeksGuid => MeasureUnit.Weeks.Guid;

  [Obsolete("Use MeasureUnit.Weeks.ID")]
  public static long MeasureUnitWeeksID => (long) (IpsMetadataEntityBase<long>) MeasureUnit.Weeks;

  [Obsolete("Use MeasureUnit.Months.Guid")]
  public static Guid MeasureUnitMonthsGuid => MeasureUnit.Months.Guid;

  [Obsolete("Use MeasureUnit.Months.ID")]
  public static long MeasureUnitMonthsID => (long) (IpsMetadataEntityBase<long>) MeasureUnit.Months;

  [Obsolete("Use Attributes.Description.Guid")]
  public static Guid AttrDescriptionGuid => Intermech.Metadata.Attributes.Description.Guid;

  [Obsolete("Use Attributes.Description.ID")]
  public static int AttrDescriptionID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Description;

  [Obsolete("Use Attributes.PercentCompleted.Guid")]
  public static Guid AttrPercentCompletedGuid => Attributes.PercentCompleted.Guid;

  [Obsolete("Use Attributes.PercentCompleted.ID")]
  public static int AttrPercentCompletedID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.PercentCompleted;
  }

  [Obsolete("Use Attributes.Flags.Guid")]
  public static Guid AttrFlagsGuid => Attributes.Flags.Guid;

  [Obsolete("Use Attributes.Flags.ID")]
  public static int AttrFlagsID => (int) (IpsMetadataEntityBase<int>) Attributes.Flags;

  [Obsolete("Use Attributes.TaskPriority.Guid")]
  public static Guid AttrTaskPriorityGuid => Attributes.TaskPriority.Guid;

  [Obsolete("Use Attributes.TaskPriority.ID")]
  public static int AttrTaskPriorityID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.TaskPriority;
  }

  [NotNull]
  [Obsolete("Use RelationTypes.Consts.TaskCompositionGuid")]
  public static string TaskCompositionRelTypeGuidStr => "cad00e93-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use RelationTypes.TaskComposition.Guid")]
  public static Guid TaskCompositionRelTypeGuid => RelationTypes.TaskComposition.Guid;

  [Obsolete("Use RelationTypes.TaskComposition.ID")]
  public static int TaskCompositionRelTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) RelationTypes.TaskComposition;
  }

  [Obsolete("Use Metadata.Attributes.SortIndex.ID")]
  public static int AttrSortIndexID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.SortIndex;

  [NotNull]
  [Obsolete("Use ObjectTypes.Consts.DependencyGuid")]
  public static string DependenciesTypeGuidStr => "cad00e9a-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use ObjectTypes.Dependency.Guid")]
  public static Guid DependenciesTypeGuid => ObjectTypes.Dependency.Guid;

  [Obsolete("Use ObjectTypes.Dependency.ID")]
  public static int DependenciesTypeID => (int) (IpsMetadataEntityBase<int>) ObjectTypes.Dependency;

  [Obsolete("Use Attributes.FromTask.Guid")]
  public static Guid AttrFromTaskGuid => Attributes.FromTask.Guid;

  [Obsolete("Use Attributes.FromTask.ID")]
  public static int AttrFromTaskID => (int) (IpsMetadataEntityBase<int>) Attributes.FromTask;

  [Obsolete("Use Attributes.ToTask.Guid")]
  public static Guid AttrToTaskGuid => Attributes.ToTask.Guid;

  [Obsolete("Use Attributes.ToTask.ID")]
  public static int AttrToTaskID => (int) (IpsMetadataEntityBase<int>) Attributes.ToTask;

  [Obsolete("Use Attributes.DependencyType.Guid")]
  public static Guid AttrDependencyTypeGuid => Attributes.DependencyType.Guid;

  [Obsolete("Use Attributes.DependencyType.ID")]
  public static int AttrDependencyTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.DependencyType;
  }

  [Obsolete("Use Attributes.Lag.Guid")]
  public static Guid AttrLagGuid => Attributes.Lag.Guid;

  [Obsolete("Use Attributes.Lag.ID")]
  public static int AttrLagID => (int) (IpsMetadataEntityBase<int>) Attributes.Lag;

  [Obsolete("Use RelationTypes.Resources.Guid")]
  public static Guid ResourcesRelTypeGuid => RelationTypes.Resources.Guid;

  [Obsolete("Use RelationTypes.Resources.ID")]
  public static int ResourcesRelTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) RelationTypes.Resources;
  }

  [Obsolete("Use Attributes.ResourceUnits.Guid")]
  public static Guid AttrResourceUnitsGuid => Attributes.ResourceUnits.Guid;

  [Obsolete("Use Attributes.ResourceUnits.ID")]
  public static int AttrResourceUnitsID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ResourceUnits;
  }

  [Obsolete("Use Attributes.ResourceIsChief.Guid")]
  public static Guid AttrIsChiefGuid => Attributes.ResourceIsChief.Guid;

  [Obsolete("Use Attributes.ResourceIsChief.ID")]
  public static int AttrIsChiefID => (int) (IpsMetadataEntityBase<int>) Attributes.ResourceIsChief;

  [Obsolete("Use RelationTypes.Attachment.Guid")]
  public static Guid PrjAttachRelTypeGuid => RelationTypes.TaskAttachment.Guid;

  [Obsolete("Use RelationTypes.Attachment.ID")]
  public static int PrjAttachRelTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) RelationTypes.TaskAttachment;
  }

  [Obsolete("Use Attributes.AttachKind.Guid")]
  public static Guid AttrPrjAttachKindGuid => Attributes.AttachKind.Guid;

  [Obsolete("Use Attributes.AttachKind.ID")]
  public static int AttrPrjAttachKindID => (int) (IpsMetadataEntityBase<int>) Attributes.AttachKind;

  [Obsolete("Use ObjectTypes.Project.ID")]
  public static int SumProjectsTypeID => (int) (IpsMetadataEntityBase<int>) ObjectTypes.Project;

  [Obsolete("Use ObjectTypes.Calendar.ID")]
  public static int CalendarsTypeID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.Calendar;

  [Obsolete("Use SystemObject.StandardCalendar.ObjectID")]
  public static long StandardCalendarObjectID => Intermech.Metadata.SystemObject.StandardCalendar.ObjectID;

  [Obsolete("Use ObjectTypes.IncompleteObject.ID")]
  public static int IncompleteObjectTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.IncompleteObject;
  }

  [Obsolete("Use ObjectTypes.UserGroup.ID")]
  public static int GroupTypeID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.UserGroup;

  [Obsolete("Use ObjectTypes.User.ID")]
  public static int UserTypeID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User;

  /// <summary>Сценарий обработки всех импортированных задач IMProject по окончании импорта в проект структуры объекта</summary>
  [NotNull]
  [Obsolete("Use ObjectTypes.Consts.ScriptInitAfterImportTasksGuid")]
  public static string ScriptInitAfterImportTasksGuidStr => "cadd94b8-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use ObjectTypes.ScriptInitAfterImportTasks.Guid")]
  public static Guid ScriptInitAfterImportTasksGuid => ObjectTypes.ScriptInitAfterImportTasks.Guid;

  [Obsolete("Use ObjectTypes.ScriptInitAfterImportTasks.ID")]
  public static int ScriptInitAfterImportTasksID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.ScriptInitAfterImportTasks;
  }

  /// <summary>Сценарий инициализации каждой отдельной задачи сразу после создания в процессе импорта</summary>
  [NotNull]
  [Obsolete("Use ObjectTypes.Consts.InitTasksScriptTypeGuid")]
  public static string InitTasksScriptTypeGuidStr => "cadd94b8-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use ObjectTypes.ScriptInitTaskAfterImport.Guid")]
  public static Guid InitTasksScriptTypeGuid => ObjectTypes.ScriptInitTaskAfterImport.Guid;

  [Obsolete("Use ObjectTypes.ScriptInitTaskAfterImport.ID")]
  public static int InitTasksScriptTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.ScriptInitTaskAfterImport;
  }

  [NotNull]
  [Obsolete("Use Services.Calendars")]
  public static ICalendarsService Calendars => Services.Calendars;

  [Obsolete("Use LCStep.Designing.Guid")]
  public static Guid LCDesigningStatusGuid => LCStep.Designing.Guid;

  [Obsolete("Use LCStep.Designing.ID")]
  public static int LCDesigningStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Designing;

  [Obsolete("Use LCStep.Executing.Guid")]
  public static Guid LCExecStatusGuid => LCStep.Executing.Guid;

  [Obsolete("Use LCStep.Executing.ID")]
  public static int LCExecStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Executing;

  [Obsolete("Use LCStep.Sent.Guid")]
  public static Guid LCSentSentGuid => LCStep.Sent.Guid;

  [Obsolete("Use LCStep.Sent.ID")]
  public static int LCSentStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Sent;

  [Obsolete("Use LCStep.Validating.Guid")]
  public static Guid LCPendingStatusGuid => LCStep.Validating.Guid;

  [Obsolete("Use LCStep.Validating.ID")]
  public static int LCPendingStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Validating;

  [Obsolete("Use LCStep.Completed.Guid")]
  public static Guid LCCompletedStatusGuid => LCStep.Completed.Guid;

  [Obsolete("Use LCStep.Completed.ID")]
  public static int LCCompletedStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Completed;

  [Obsolete("Use LCStep.Terminated.Guid")]
  public static Guid LCTerminatedStatusGuid => LCStep.Terminated.Guid;

  [Obsolete("Use LCStep.Terminated.ID")]
  public static int LCTerminatedStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Terminated;

  [Obsolete("Use LCStep.Waiting.Guid")]
  public static Guid LCWaitingStatusGuid => LCStep.Waiting.Guid;

  [Obsolete("Use LCStep.Waiting.ID")]
  public static int LCWaitingStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Waiting;

  [Obsolete("Use LCStep.Imported.Guid")]
  public static Guid LCDirectEditStatusGuid => LCStep.Imported.Guid;

  [Obsolete("Use LCStep.Imported.ID")]
  public static int LCDirectEditStatusID => (int) (IpsMetadataEntityBase<int>) LCStep.Imported;

  [NotNull]
  [Obsolete("Use ObjectTypes.Consts.ProjectMessageGuid")]
  public static string ProjectMessageTypeGuidStr => "cadd91f6-306c-11d8-b4e9-00304f19f545";

  [Obsolete("Use ObjectTypes.ProjectMessage.Guid")]
  public static Guid ProjectMessageTypeGuid => ObjectTypes.ProjectMessage.Guid;

  [Obsolete("Use ObjectTypes.ProjectMessage.ID")]
  public static int ProjectMessageTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.ProjectMessage;
  }

  public static Guid AssignmentsReportTemplate2Guid
  {
    get => SystemObject.AssignmentsReportTemplate2ID.Guid;
  }

  public static long AssignmentsReportTemplate2ID
  {
    get => (long) (IpsMetadataEntityBase<long>) SystemObject.AssignmentsReportTemplate2ID;
  }

  [Obsolete("Use Attributes.IterationID.Guid")]
  public static Guid IterationID_AttributeGuid => Intermech.Metadata.Attributes.IterationID.Guid;

  [Obsolete("Use Attributes.IterationID.ID")]
  public static int IterationID_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.IterationID;
  }

  [Obsolete("Use Attributes.Data.Guid")]
  public static Guid Data_AttributeGuid => Intermech.Metadata.Attributes.Data.Guid;

  [Obsolete("Use Attributes.Data.ID")]
  public static int Data_AttributeID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Data;

  [Obsolete("Use Attributes.ImportedObject.Guid")]
  public static Guid ImportedObjectVersionID_AttributeGuid => Attributes.ImportedObject.Guid;

  [Obsolete("Use Attributes.ImportedObject.ID")]
  public static int ImportedObjectVersionID_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ImportedObject;
  }

  [Obsolete("Use Attributes.ImportedRelationGuid.Guid")]
  public static Guid ImportedRelationGuid_AttributeGuid => Attributes.ImportedRelationGuid.Guid;

  [Obsolete("Use Attributes.ImportedRelationGuid.ID")]
  public static int ImportedRelationGuid_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ImportedRelationGuid;
  }

  [Obsolete("Use RelationTypes.ImportedObjects.Guid")]
  public static Guid ImportedObjectsInProject_RelationTypeGuid
  {
    get => RelationTypes.ImportedObjects.Guid;
  }

  [Obsolete("Use RelationTypes.ImportedObjects.ID")]
  public static int ImportedObjectsInProject_RelationTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) RelationTypes.ImportedObjects;
  }

  [Obsolete("Use Attributes.LastSyncDate.Guid")]
  public static Guid LastSyncDate_AttributeGuid => Attributes.LastSyncDate.Guid;

  [Obsolete("Use Attributes.LastSyncDate.ID")]
  public static int LastSyncDate_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.LastSyncDate;
  }

  [Obsolete("Use Attributes.Prototype.Guid")]
  public static Guid Prototype_AttributeGuid => Attributes.Prototype.Guid;

  [Obsolete("Use Attributes.Prototype.ID")]
  public static int Prototype_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.Prototype;
  }

  [Obsolete("Use Attributes.InitScript.Guid")]
  public static Guid InitScript_AttributeGuid => Attributes.InitScript.Guid;

  [Obsolete("Use Attributes.InitScript.ID")]
  public static int InitScript_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.InitScript;
  }

  [Obsolete("Use Attributes.ImportedRootObjectGuid.Guid")]
  public static Guid ImportedToProjectObjectGUID_AttributeGuid
  {
    get => Attributes.ImportedRootObjectGuid.Guid;
  }

  [Obsolete("Use Attributes.ImportedRootObjectGuid.ID")]
  public static int ImportedToProjectObjectGUID_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ImportedRootObjectGuid;
  }

  [Obsolete("Use ObjectTypes.PrintScheme.Guid")]
  public static Guid PrintScheme_ObjTypeGuid => ObjectTypes.PrintScheme.Guid;

  [Obsolete("Use ObjectTypes.PrintScheme.ID")]
  public static int PrintScheme_ObjTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.PrintScheme;
  }

  [Obsolete("Use ObjectTypes.RouteElement.Guid")]
  public static Guid RouteElement_ObjTypeGuid => ObjectTypes.RouteElement.Guid;

  [Obsolete("Use ObjectTypes.RouteElement.ID")]
  public static int RouteElement_ObjTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.RouteElement;
  }

  [Obsolete("Use ObjectTypes.BasicTechprocess.Guid")]
  public static Guid BasicTechprocess_ObjTypeGuid => ObjectTypes.BasicTechprocess.Guid;

  [Obsolete("Use ObjectTypes.BasicTechprocess.ID")]
  public static int BasicTechprocess_ObjTypeID
  {
    get => (int) (IpsMetadataEntityBase<int>) ObjectTypes.BasicTechprocess;
  }

  [Obsolete("Use ObjectTypes.TechRoute.Guid")]
  public static Guid TechRoute_ObjTypeGuid => ObjectTypes.TechRoute.Guid;

  [Obsolete("Use ObjectTypes.TechRoute.ID")]
  public static int TechRoute_ObjTypeID => (int) (IpsMetadataEntityBase<int>) ObjectTypes.TechRoute;

  [Obsolete("Use Attributes.ManagerAnswer.Guid")]
  public static Guid ManagerAnswer_AttributeGuid => Attributes.ManagerAnswer.Guid;

  [Obsolete("Use Attributes.ManagerAnswer.ID")]
  public static int ManagerAnswer_AttributeID
  {
    get => (int) (IpsMetadataEntityBase<int>) Attributes.ManagerAnswer;
  }

  [NotNull]
  [Obsolete("Use Attributes.ProtectedIDsList")]
  public static IReadOnlyList<int> ProtectedAttributeTypes => Attributes.ProtectedIDsList;

  /// <summary>Атрибут "Дата модификации содержимого объекта"</summary>
  [Obsolete("Use Attributes.Modified.ID")]
  public static int ModifyContentDateID => (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.Attributes.Modified;

  [Obsolete("Заменено на Intermech.Project.Library.Init()")]
  public static void Init([NotNull] IServiceProvider serviceProvider, [CanBeNull] IUserSession session = null)
  {
    Library.Init(serviceProvider, session);
  }
}
