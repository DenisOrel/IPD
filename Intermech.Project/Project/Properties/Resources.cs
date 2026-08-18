// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Properties.Resources
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project.Properties;

/// <summary>
///   A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  /// <summary>
  ///   Returns the cached ResourceManager instance used by this class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (Intermech.Project.Properties.Resources.resourceMan == null)
        Intermech.Project.Properties.Resources.resourceMan = new ResourceManager("Intermech.Project.Properties.Resources", typeof (Intermech.Project.Properties.Resources).Assembly);
      return Intermech.Project.Properties.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => Intermech.Project.Properties.Resources.resourceCulture;
    set => Intermech.Project.Properties.Resources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized string similar to Дополнительно.
  /// </summary>
  internal static string AddData
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (AddData), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Допустимый диапазон значений параметра.
  /// </summary>
  internal static string AllowedParameterRange
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (AllowedParameterRange), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Как можно позже.
  /// </summary>
  internal static string AsLateAsPossible
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (AsLateAsPossible), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Как можно раньше.
  /// </summary>
  internal static string AsSoonAsPossible
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (AsSoonAsPossible), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Можно выполнять задачу "{0}".
  /// </summary>
  internal static string CanStartTaskSubject
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CanStartTaskSubject), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Все предыдущие задачи, от которых зависела задача "&lt;a href="#view={0}"&gt;{1}&lt;/a&gt;" проекта "&lt;a href="#view={2}"&gt;{3}&lt;/a&gt;" (руководитель: &lt;a href="#object={4}"&gt;{5}&lt;/a&gt;) были выполнены, можно приступать к её выполнению..
  /// </summary>
  internal static string CanStartTaskTemplate
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CanStartTaskTemplate), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///    Looks up a localized string similar to Текущий проект невозможно разместить на указанном числе страниц из-за его размера.
  /// 
  /// Автоматически выбран масштаб печати 10 процентов от нормального размера. Проект будет напечатан в этом масштабе..
  ///  </summary>
  internal static string Cant_Set_AutoZoom
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (Cant_Set_AutoZoom), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Невозможно добавить зависимость.
  /// </summary>
  internal static string CantAddDependency
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CantAddDependency), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Невозможно задать "{0}": не разрешено для вех..
  /// </summary>
  internal static string CantSetMilestoneProperty
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CantSetMilestoneProperty), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Невозможно задать "{0}": не разрешено для суммарных задач..
  /// </summary>
  internal static string CantSetSummaryProperty
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CantSetSummaryProperty), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Взятие на редактирование....
  /// </summary>
  internal static string CheckoutProgress
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CheckoutProgress), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Для изменения свойств действия "{0}" нужно взять его на редактирование!.
  /// </summary>
  internal static string CheckoutTaskNeeded
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CheckoutTaskNeeded), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Выбор проекта.
  /// </summary>
  internal static string ChooseProject
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ChooseProject), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Выбор ресурсов.
  /// </summary>
  internal static string ChooseResources
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ChooseResources), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Завершение редактирования....
  /// </summary>
  internal static string ClosingProgress
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ClosingProgress), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Согласовать.</summary>
  internal static string CmdVerifyResults
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CmdVerifyResults), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Запустить процесс согласования результатов.
  /// </summary>
  internal static string CmdVerifyResultsHint
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (CmdVerifyResultsHint), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to По-умолчанию для всех типов объектов.
  /// </summary>
  internal static string DefaultForAllObjectTypes
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (DefaultForAllObjectTypes), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Удалить настройки для типа объекта "{0}"?.
  /// </summary>
  internal static string DeleteSettingsForObjectType
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (DeleteSettingsForObjectType), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Зависимость задач "{0}"=&gt;"{1}".
  /// </summary>
  internal static string DependencyCaption
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (DependencyCaption), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Двойная связь задачи-предшественника ({0}) с одной задачей-последователем ({1}) не допускается..
  /// </summary>
  internal static string DoubleDependencyNotAllowed
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (DoubleDependencyNotAllowed), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Данная команда может быть выполнена только руководителем проекта ({0})!.
  /// </summary>
  internal static string ErrChiefNeeded
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrChiefNeeded), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Взаимные зависимости не допускаются..
  /// </summary>
  internal static string ErrCircularDependency
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrCircularDependency), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Нельзя вставлять проект в самого себя..
  /// </summary>
  internal static string ErrCircularSubProject
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrCircularSubProject), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Не найден предшественник ("{0}").
  /// </summary>
  internal static string ErrDepTaskNotFound
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrDepTaskNotFound), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Дублирование, вставка подпроекта в проект допускается только один раз..
  /// </summary>
  internal static string ErrDupSubProject
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrDupSubProject), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Недопустимое значение поля..
  /// </summary>
  internal static string ErrIncorrectFieldValue
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrIncorrectFieldValue), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Невозможно добавить ресурс: вехи не могут содержать ресурсов..
  /// </summary>
  internal static string ErrMilestoneResource
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrMilestoneResource), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Не заданы исполнители/ресурсы для задачи "{0}"!.
  /// </summary>
  internal static string ErrNoTaskResources
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrNoTaskResources), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Текущий узел не имеет прав владения свойств опубликованного объекта..
  /// </summary>
  internal static string ErrNotPropertiesOwner
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrNotPropertiesOwner), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Редактирование запущенного проекта разрешено только его руководителю!.
  /// </summary>
  internal static string ErrOnlyChiefCanEdit
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrOnlyChiefCanEdit), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Связи между суммарными задачами и их подчиненными не допускаются..
  /// </summary>
  internal static string ErrParentChildDependency
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrParentChildDependency), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Служба портала не найдена, удаленный запуск проекта "{0}" невозможен. Проект должен выполняться на "домашнем" узле его руководителя ("{1}")!.
  /// </summary>
  internal static string ErrPortalNeededForExec
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrPortalNeededForExec), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Т.к. проект "{0}" имеет статус "{1}", для успешного сохранения требуется, чтобы он был корректным..
  /// </summary>
  internal static string ErrProjectShouldBeValid
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrProjectShouldBeValid), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Ресурс "{0}" не найден среди использующихся ресурсов проекта. Для первичного добавления ресурса в проект используйте закладку "Ресурсы" в окне свойств задачи..
  /// </summary>
  internal static string ErrResByNameNotFound
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrResByNameNotFound), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Невозможно отправить сообщение: серверная служба маршрутизатора не загружена!.
  /// </summary>
  internal static string ErrWorkflowServiceNotFound
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrWorkflowServiceNotFound), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///    Looks up a localized string similar to Недопустимая длительность ({0}).
  /// Введите длительность в правильном формате, например 4 часа (или 4ч), 12 дней (или 12д), 3 недели (или 3н) или 2мес для месяцев..
  ///  </summary>
  internal static string ErrWrongDurationFormat
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ErrWrongDurationFormat), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Все задачи.</summary>
  internal static string FilterAllTasks
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterAllTasks), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Выполненные задачи.
  /// </summary>
  internal static string FilterCompleted
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterCompleted), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Критические задачи.
  /// </summary>
  internal static string FilterCritical
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterCritical), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Задачи с оценкой длительности.
  /// </summary>
  internal static string FilterEstimation
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterEstimation), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Выполняющиеся задачи.
  /// </summary>
  internal static string FilterExecuted
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterExecuted), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Вехи.</summary>
  internal static string FilterMilestones
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterMilestones), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Не выполненные в срок.
  /// </summary>
  internal static string FilterOverdueTasks
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterOverdueTasks), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Использование ресурса.
  /// </summary>
  internal static string FilterResource
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterResource), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Показать задачи, использующие:.
  /// </summary>
  internal static string FilterResourceText
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterResourceText), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Суммарные задачи.
  /// </summary>
  internal static string FilterSummary
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterSummary), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Задачи с крайними сроками.
  /// </summary>
  internal static string FilterWithConstraints
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterWithConstraints), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Задачи с результатами.
  /// </summary>
  internal static string FilterWithResults
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterWithResults), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Задачи с исходными данными.
  /// </summary>
  internal static string FilterWithSrcData
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FilterWithSrcData), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Окончание-Окончание (ОО).
  /// </summary>
  internal static string FinishFinish
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FinishFinish), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Окончание не ранее.
  /// </summary>
  internal static string FinishNoEarlierThan
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FinishNoEarlierThan), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Окончание не позднее.
  /// </summary>
  internal static string FinishNoLaterThan
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FinishNoLaterThan), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Окончание-Начало (ОН).
  /// </summary>
  internal static string FinishStart
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FinishStart), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to От даты окончания проекта.
  /// </summary>
  internal static string FromEnd
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FromEnd), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to От даты начала проекта.
  /// </summary>
  internal static string FromStart
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (FromStart), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Импортированные в проект объекты.
  /// </summary>
  internal static string ImportedInProjectObjects
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ImportedInProjectObjects), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Импорт большого числа объектов может занять длительное время..
  /// </summary>
  internal static string ImportTooMuchObjects
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ImportTooMuchObjects), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Условие ({0}) некорректно, невозможно вычислить результат..
  /// </summary>
  internal static string InvalidConditionErr
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (InvalidConditionErr), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Сохранить измненения внесённые список импортированных в проект объектов?.
  /// </summary>
  internal static string LoseChangesDeletedImportedObjects
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (LoseChangesDeletedImportedObjects), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///    Looks up a localized string similar to Сообщение от руководителя:
  /// {0}.
  ///  </summary>
  internal static string ManagerAnswer
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ManagerAnswer), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Ручное планирование.
  /// </summary>
  internal static string ManualPlanning
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ManualPlanning), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///    Looks up a localized string similar to Следующие объекты уже импортированы в проект:
  /// {0} Повторить выбор?.
  ///  </summary>
  internal static string ManyObjectAlreadyImported
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ManyObjectAlreadyImported), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Результаты выполнения задачи должны пройти процесс согласования "{0}".{1}Проверку не прошли следующие вложения:.
  /// </summary>
  internal static string MustBeVerifiedByProcess
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (MustBeVerifiedByProcess), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Т.к. задан процесс согласования результатов, задача должна содержать результаты выполнения!.
  /// </summary>
  internal static string MustHaveResultsErr
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (MustHaveResultsErr), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Выбранный объект уже импортирован в проект. Повторить выбор?.
  /// </summary>
  internal static string ObjectAlreadyImported
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ObjectAlreadyImported), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Объект {0} уже импортирован в проект. Повторить выбор?.
  /// </summary>
  internal static string ObjectWithCaptionAlreadyImported
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ObjectWithCaptionAlreadyImported), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to повторно.</summary>
  internal static string OnceAgain
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OnceAgain), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to равно.</summary>
  internal static string OpE
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpE), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to больше.</summary>
  internal static string OpG
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpG), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to больше или равно.
  /// </summary>
  internal static string OpGE
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpGE), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to содержит.</summary>
  internal static string OpIn
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpIn), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to в диапазоне.</summary>
  internal static string OpInr
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpInr), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to меньше.</summary>
  internal static string OpL
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpL), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to меньше или равно.
  /// </summary>
  internal static string OpLE
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpLE), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to не равно.</summary>
  internal static string OpNE
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpNE), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to не содержит.</summary>
  internal static string OpOut
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpOut), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to вне диапазона.
  /// </summary>
  internal static string OpOutr
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OpOutr), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Задача "{0}" не выполнена в срок.
  /// </summary>
  internal static string OverdueTaskMailSubject
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OverdueTaskMailSubject), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Задача "&lt;a href="#view={0}"&gt;{1}&lt;/a&gt;" (исполнитель: "{2}") проекта "&lt;a href="#view={3}"&gt;{4}&lt;/a&gt;" не выполнена в срок!.
  /// </summary>
  internal static string OverdueTaskMailTemplate
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (OverdueTaskMailTemplate), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Задача "{0}" выполнена.
  /// </summary>
  internal static string PendingTaskMailSubject
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (PendingTaskMailSubject), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Исполнитель задачи "{0}" выполнил задачу "&lt;a href="#view={1}"&gt;{2}&lt;/a&gt;" проекта "&lt;a href="#view={3}"&gt;{4}&lt;/a&gt;". Как руководитель, вы должны подтвердить факт выполнения задачи. Откройте свойства задачи и подтвердите/отклоните указанный процент выполнения..
  /// </summary>
  internal static string PendingTaskMailTemplate
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (PendingTaskMailTemplate), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Конфликт планирования.
  /// </summary>
  internal static string PlanningConflict
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (PlanningConflict), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Проект.</summary>
  internal static string Project
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (Project), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Участники проекта "{0}".
  /// </summary>
  internal static string ProjectMembers
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ProjectMembers), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Свойства проекта.
  /// </summary>
  internal static string ProjectProps
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ProjectProps), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to При проверке проекта "{0}" были обнаружены следующие ошибки:.
  /// </summary>
  internal static string ProjectValidationErrors
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ProjectValidationErrors), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Исключить из списка.
  /// </summary>
  internal static string RemoveFromList
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (RemoveFromList), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Удалить из проекта задачи, импортированные из выбранных объектов?.
  /// </summary>
  internal static string RemoveLinkWithSelectedImportedObjectAndDeleteTasks
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (RemoveLinkWithSelectedImportedObjectAndDeleteTasks), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Загрузка ресурсов.
  /// </summary>
  internal static string ResourceAssignments
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ResourceAssignments), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Процент загрузки.
  /// </summary>
  internal static string ResourcesCalcMode_Load
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ResourcesCalcMode_Load), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Пиковые единицы.
  /// </summary>
  internal static string ResourcesCalcMode_PeakLoad
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ResourcesCalcMode_PeakLoad), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Ресурсы.</summary>
  internal static string ResourcesTitle
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ResourcesTitle), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Сохранение....
  /// </summary>
  internal static string SavingProgress
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (SavingProgress), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Дни.</summary>
  internal static string ScaleTypeDays
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ScaleTypeDays), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Месяцы.</summary>
  internal static string ScaleTypeMonths
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ScaleTypeMonths), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Кварталы.</summary>
  internal static string ScaleTypeQuarters
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ScaleTypeQuarters), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Недели.</summary>
  internal static string ScaleTypeWeeks
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ScaleTypeWeeks), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Годы.</summary>
  internal static string ScaleTypeYears
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ScaleTypeYears), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Начало-Окончание (НО).
  /// </summary>
  internal static string StartFinish
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (StartFinish), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Начало не ранее.
  /// </summary>
  internal static string StartNoEarlierThan
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (StartNoEarlierThan), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Начало не позднее.
  /// </summary>
  internal static string StartNoLaterThan
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (StartNoLaterThan), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Начало-Начало (НН).
  /// </summary>
  internal static string StartStart
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (StartStart), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Вставка подпроекта в проект допускается только один раз..
  /// </summary>
  internal static string SubProjectAlreadyExists
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (SubProjectAlreadyExists), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Для объекта типа {0} должна быть создана суммарная задача, однако шаблон для суммарных задач для этого типа не указан.
  /// </summary>
  internal static string SubTaskTemplateNotAssigned
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (SubTaskTemplateNotAssigned), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Суммарные задачи не могут быть вехами!.
  /// </summary>
  internal static string SummaryCantBeMilestone
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (SummaryCantBeMilestone), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Требуется синхронизация.
  /// </summary>
  internal static string SyncPending
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (SyncPending), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Исходные данные.
  /// </summary>
  internal static string TaskData
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskData), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Для задачи установлено ограничение '{0}' с датой '{1}'.
  /// </summary>
  internal static string TaskHasConstraint
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskHasConstraint), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Вам назначена задача "{0}".
  /// </summary>
  internal static string TaskMailSubject
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskMailSubject), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Вы {6}назначены исполнителем задачи "&lt;a href="#view={0}"&gt;{1}&lt;/a&gt;" проекта "&lt;a href="#view={2}"&gt;{3}&lt;/a&gt;" (руководитель: &lt;a href="#object={4}"&gt;{5}&lt;/a&gt;).&lt;br /&gt;Установить процент выполнения можно из окна почты или органайзера на закладке свойств задачи..
  /// </summary>
  internal static string TaskMailTemplate
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskMailTemplate), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Идентификаторы ресурсов.
  /// </summary>
  internal static string TaskParamAssignments
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamAssignments), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Ресурсы.</summary>
  internal static string TaskParamAssignmentsString
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamAssignmentsString), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Руководитель.
  /// </summary>
  internal static string TaskParamChiefString
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamChiefString), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Руководитель.
  /// </summary>
  internal static string TaskParamChiefString1
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamChiefString1), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Выполнено.</summary>
  internal static string TaskParamCompleted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamCompleted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Фактические трудозатраты.
  /// </summary>
  internal static string TaskParamCompletedWork
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamCompletedWork), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Дата ограничения.
  /// </summary>
  internal static string TaskParamConstraintDate
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamConstraintDate), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Тип ограничения.
  /// </summary>
  internal static string TaskParamConstraintType
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamConstraintType), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Индекс.</summary>
  internal static string TaskParamDispIndex
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamDispIndex), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Длительность.
  /// </summary>
  internal static string TaskParamDuration
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamDuration), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Предварительная оценка.
  /// </summary>
  internal static string TaskParamEstimation
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamEstimation), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Фактическое окончание.
  /// </summary>
  internal static string TaskParamFactFinish
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamFactFinish), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Фактическое окончание.
  /// </summary>
  internal static string TaskParamFactFinish1
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamFactFinish1), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Фактическое начало.
  /// </summary>
  internal static string TaskParamFactStart
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamFactStart), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Фактическое начало.
  /// </summary>
  internal static string TaskParamFactStart1
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamFactStart1), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Окончание.</summary>
  internal static string TaskParamFinish
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamFinish), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Имеет подзадачи.
  /// </summary>
  internal static string TaskParamHasSubTasks
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamHasSubTasks), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Уровень вложенности.
  /// </summary>
  internal static string TaskParamIndentLevel
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamIndentLevel), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Критическая задача.
  /// </summary>
  internal static string TaskParamIsCritical
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamIsCritical), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Выполняется.</summary>
  internal static string TaskParamIsExecuted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamIsExecuted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Веха.</summary>
  internal static string TaskParamMilestone
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamMilestone), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Название.</summary>
  internal static string TaskParamName
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamName), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Заметки.</summary>
  internal static string TaskParamNotes
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamNotes), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Заметки.</summary>
  internal static string TaskParamNotes1
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamNotes1), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Процент выполнения.
  /// </summary>
  internal static string TaskParamPercentCompleted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamPercentCompleted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Плановый процент выполнения.
  /// </summary>
  internal static string TaskParamPlannedPercentCompleted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamPlannedPercentCompleted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Приоритет.</summary>
  internal static string TaskParamPriority
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamPriority), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Количество результатов.
  /// </summary>
  internal static string TaskParamResultsCount
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamResultsCount), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Количество исходных данных.
  /// </summary>
  internal static string TaskParamSrcDataCount
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamSrcDataCount), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Начало.</summary>
  internal static string TaskParamStart
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamStart), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Статус задачи.
  /// </summary>
  internal static string TaskParamStatus
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamStatus), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Статус задачи.
  /// </summary>
  internal static string TaskParamStatus1
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamStatus1), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Код СДР.</summary>
  internal static string TaskParamWbsCode
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamWbsCode), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Трудозатраты.
  /// </summary>
  internal static string TaskParamWork
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskParamWork), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Свойства задачи.
  /// </summary>
  internal static string TaskProps
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskProps), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Результаты.</summary>
  internal static string TaskResults
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskResults), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Выполнено.</summary>
  internal static string TaskStatusCompleted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusCompleted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Выполняется.</summary>
  internal static string TaskStatusExecuted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusExecuted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Проектируется.
  /// </summary>
  internal static string TaskStatusNotStarted
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusNotStarted), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Ожидает проверки руководителем.
  /// </summary>
  internal static string TaskStatusPending
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusPending), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Разослано исполнителям.
  /// </summary>
  internal static string TaskStatusSent
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusSent), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Выполнение прервано.
  /// </summary>
  internal static string TaskStatusTerminated
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusTerminated), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Ожидает запуска.
  /// </summary>
  internal static string TaskStatusWaiting
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskStatusWaiting), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Выполнение задачи "{0}" было отклонено руководителем.
  /// </summary>
  internal static string TaskVerifyRejected
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskVerifyRejected), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Общие.</summary>
  internal static string TaskViewPageCommon
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageCommon), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Дополнительно.
  /// </summary>
  internal static string TaskViewPageExtra
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageExtra), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Исходные данные.
  /// </summary>
  internal static string TaskViewPageInitialData
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageInitialData), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Неопределённая закладка.
  /// </summary>
  internal static string TaskViewPageNone
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageNone), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Заметки.</summary>
  internal static string TaskViewPageNotes
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageNotes), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Предшественники.
  /// </summary>
  internal static string TaskViewPagePrecursors
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPagePrecursors), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Ресурсы.</summary>
  internal static string TaskViewPageResources
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageResources), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Результаты.</summary>
  internal static string TaskViewPageResults
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TaskViewPageResults), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to дней,д,дн,день,дня.
  /// </summary>
  internal static string TimeUnitD
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TimeUnitD), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to часов,ч,час.</summary>
  internal static string TimeUnitH
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TimeUnitH), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to минут,мин,минута,минуты.
  /// </summary>
  internal static string TimeUnitM
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TimeUnitM), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to месяцев,м,мес,месяц,месяца.
  /// </summary>
  internal static string TimeUnitMon
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TimeUnitMon), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to недель,нед,н,неделя,недели.
  /// </summary>
  internal static string TimeUnitW
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (TimeUnitW), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Шаблон для суммарных задач.
  /// </summary>
  internal static string UsePrototypeForSubprojects
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (UsePrototypeForSubprojects), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>
  ///   Looks up a localized string similar to Использовать параметры.
  /// </summary>
  internal static string UsePrototypeForTask
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (UsePrototypeForTask), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }

  /// <summary>Looks up a localized string similar to Нет.</summary>
  internal static string ValFalse
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ValFalse), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Минимальная дата.
  /// </summary>
  internal static string ValMinDate
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ValMinDate), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Текущая дата.
  /// </summary>
  internal static string ValNow
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ValNow), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>Looks up a localized string similar to Да.</summary>
  internal static string ValTrue
  {
    get => Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (ValTrue), Intermech.Project.Properties.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to Обновление окон.
  /// </summary>
  internal static string WinUpdatingProgress
  {
    get
    {
      return Intermech.Project.Properties.Resources.ResourceManager.GetString(nameof (WinUpdatingProgress), Intermech.Project.Properties.Resources.resourceCulture);
    }
  }
}
