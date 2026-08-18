
// Type: Intermech.Client.Core.SelectDialog
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Localization;
using Intermech.Navigator.Controls;
using Intermech.Navigator.CustomNode;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;


namespace Intermech.Client.Core;

/// <summary>Статический класс для методов вызова диалогов выбора объектов
/// 
/// Отличия:
///   1) Можно указать имя операции и будет сохраняться/восстанавливаться — выбранный тип объекта для данной операции (удобнее, быстрее)
///   2) Можно указать коллекцию идентификаторов типов объектов и для выбора будут доступны только они
///   3) После демонстрации окна автоматом фокусируется вьюшка выбранного типа объекта (список версий объектов)
///   4) Диалогу назначены свойства AcceptButton и CancelButton, а значит работают кнопки ESC и Enter</summary>
public static class SelectDialog
{
  /// <summary>Имя параметра, пользовательских настроек, в который будет сохранён тип объекта который должен быть выбран для именованной
  /// операции</summary>
  private const string PreSelectObjectTypeParamName = "PreSelectObjectType";
  /// <summary>Имя операции, выполняемой в данный момент. Если null, то операция не именованная</summary>
  [CanBeNull]
  private static string _operationName;
  /// <summary>кэш типов объектов, которые надо автоматически фокусировать для именованных операций</summary>
  [NotNull]
  private static readonly Dictionary<string, int?> _operationObjectTypes = new Dictionary<string, int?>();
  /// <summary>Тип объекта, который надо автоматически выбрать при демонстрации окна выбора объекта</summary>
  private static int? _preSelectObjectType;
  private static int _lockResoterObjectTypeCounter;

  /// <summary>Вызывать диалог выбора объекта</summary>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="description">Описание действия</param>
  /// <param name="rootNodeCaption">Заголовок ноды, объединяющей типы объектов. Если null или пустой, то будет использовано стандартное значение "Типы объектов"</param>
  /// <param name="options">Настройки вида окна</param>
  /// <param name="operationName">Имя операции. Используется для сохранения и чтения из пользовательских настроек типа объекта,
  /// который был выбран в дереве в последний раз, когда производился выбор объекта в контексте данной операции.
  /// 
  /// Например, когда диалог выбора объекта объекта вызывался для выбора объекта для импорта в структуру задач IMProject,
  /// то пользователь может выбрать любой тип объекта, однако первоначально ему будут предложены объекты того типа,
  /// который был использован именно при последнем импорте объектов в IMProject.
  /// 
  /// Так во-первых быстрее - в 99% процентов случаев тип объекта будет использован повторно,
  /// во-вторых если до этого пользователь где-то вызывал окно выбора объекта в контексте иной операции, то там может быть другой тип
  ///   объекта (а формы выбора кэшируются и по-умолчанию будет показана последняя использованная)
  /// И наконец в-третьих - при первом вызове формы выбора по умолчанию будет сфокусирована нода "Все типы объектов"
  ///   загрузка вьюшки которой длится секунд 5, что лично мне делает больно</param>
  /// <param name="nodesContext">Контейнер сервисов контекста</param>
  /// <param name="dynamicHandler">Обработчик выбора, позволяющий указывать какие объекты можно выбирать, а какие нет</param>
  /// <param name="conditions">Набор условий фильтрации объектов</param>
  /// <param name="disableGlobalContextMenuCommands">Отключить ли в контекстном меню списка объектов все глобальные команды</param>
  /// <returns>Список идентификаторов выбранных объектов. Если операция выбора будет отменена, то null</returns>
  [CanBeNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<IDBObjectID> Objects(
    [CanBeNull] string caption = null,
    [CanBeNull] string description = null,
    [CanBeNull] string rootNodeCaption = null,
    SelectionOptions options = SelectionOptions.SelectObjects,
    [CanBeNull] string operationName = null,
    [CanBeNull] System.IServiceProvider nodesContext = null,
    [CanBeNull] DynamicSelectionEventHandler dynamicHandler = null,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool disableGlobalContextMenuCommands = false)
  {
    return SelectDialog.Objects((IReadOnlyCollection<int>) null, (IReadOnlyCollection<long>) null, caption, description, rootNodeCaption, options, operationName, nodesContext, dynamicHandler, conditions, disableGlobalContextMenuCommands);
  }

  /// <summary>Вызывать диалог выбора объекта</summary>
  /// <param name="objectType">Идентификатор типа объектов, объекты которого доступны для выбора</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="description">Описание действия</param>
  /// <param name="rootNodeCaption">Заголовок ноды, объединяющей типы объектов. Если null или пустой, то будет использовано стандартное значение "Типы объектов"</param>
  /// <param name="options">Настройки вида окна</param>
  /// <param name="operationName">Имя операции. Используется для сохранения и чтения из пользовательских настроек типа объекта,
  /// который был выбран в дереве в последний раз, когда производился выбор объекта в контексте данной операции.
  /// 
  /// Например, когда диалог выбора объекта объекта вызывался для выбора объекта для импорта в структуру задач IMProject,
  /// то пользователь может выбрать любой тип объекта, однако первоначально ему будут предложены объекты того типа,
  /// который был использован именно при последнем импорте объектов в IMProject.
  /// 
  /// Так во-первых быстрее - в 99% процентов случаев тип объекта будет использован повторно,
  /// во-вторых если до этого пользователь где-то вызывал окно выбора объекта в контексте иной операции, то там может быть другой тип
  ///   объекта (а формы выбора кэшируются и по-умолчанию будет показана последняя использованная)
  /// И наконец в-третьих - при первом вызове формы выбора по умолчанию будет сфокусирована нода "Все типы объектов"
  ///   загрузка вьюшки которой длится секунд 5, что лично мне делает больно</param>
  /// <param name="nodesContext">Контейнер сервисов контекста</param>
  /// <param name="dynamicHandler">Обработчик выбора, позволяющий указывать какие объекты можно выбирать, а какие нет</param>
  /// <param name="conditions">Набор условий фильтрации объектов</param>
  /// <param name="disableGlobalContextMenuCommands">Отключить ли в контекстном меню списка объектов все глобальные команды</param>
  /// <returns>Список идентификаторов выбранных объектов. Если операция выбора будет отменена, то null</returns>
  [CanBeNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<IDBObjectID> Objects(
    int objectType,
    [CanBeNull] string caption = null,
    [CanBeNull] string description = null,
    [CanBeNull] string rootNodeCaption = null,
    SelectionOptions options = SelectionOptions.SelectObjects,
    [CanBeNull] string operationName = null,
    [CanBeNull] System.IServiceProvider nodesContext = null,
    [CanBeNull] DynamicSelectionEventHandler dynamicHandler = null,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool disableGlobalContextMenuCommands = false)
  {
    return SelectDialog.Objects((IReadOnlyCollection<int>) new int[1]
    {
      objectType
    }, (IReadOnlyCollection<long>) null, caption, description, rootNodeCaption, options, operationName, nodesContext, dynamicHandler, conditions, disableGlobalContextMenuCommands);
  }

  /// <summary>Вызывать диалог выбора объекта</summary>
  /// <param name="objectTypes">Коллекция идентификаторов типов объектов, объекты которых доступны для выбора</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="description">Описание действия</param>
  /// <param name="rootNodeCaption">Заголовок ноды, объединяющей типы объектов. Если null или пустой, то будет использовано стандартное значение "Типы объектов"</param>
  /// <param name="options">Настройки вида окна</param>
  /// <param name="operationName">Имя операции. Используется для сохранения и чтения из пользовательских настроек типа объекта, который был
  /// выбран в дереве в последний раз, когда производился выбор объекта в контексте данной операции.
  /// 
  /// Например, когда диалог выбора объекта объекта вызывался для выбора объекта для импорта в структуру задач
  /// IMProject, то пользователь может выбрать любой тип объекта, однако первоначально ему будут предложены
  /// объекты того типа, который был использован именно при последнем импорте объектов в IMProject.
  /// 
  /// Так во-первых быстрее - в 99% процентов случаев тип объекта будет использован повторно, во-вторых если до
  /// этого пользователь где-то вызывал окно выбора объекта в контексте иной операции, то там может быть другой
  /// тип объекта (а формы выбора кэшируются и по-умолчанию будет показана последняя использованная)
  /// И наконец в-третьих - при первом вызове формы выбора по умолчанию будет сфокусирована нода "Все типы
  /// объектов" загрузка вьюшки которой длится секунд 5, что лично мне делает больно</param>
  /// <param name="nodesContext">Контейнер сервисов контекста</param>
  /// <param name="dynamicHandler">Обработчик выбора, позволяющий указывать какие объекты можно выбирать, а какие нет</param>
  /// <param name="conditions">Набор условий фильтрации объектов</param>
  /// <param name="disableGlobalContextMenuCommands">Отключить ли в контекстном меню списка объектов все глобальные команды</param>
  /// <returns>Список интерфейсов идентификаторов выбранных объектов. Если операция выбора будет отменена, то null</returns>
  [CanBeNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IReadOnlyList<IDBObjectID> Objects(
    [CanBeNull] IReadOnlyCollection<int> objectTypes,
    [CanBeNull] string caption = null,
    [CanBeNull] string description = null,
    [CanBeNull] string rootNodeCaption = null,
    SelectionOptions options = SelectionOptions.SelectObjects,
    [CanBeNull] string operationName = null,
    [CanBeNull] System.IServiceProvider nodesContext = null,
    [CanBeNull] DynamicSelectionEventHandler dynamicHandler = null,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool disableGlobalContextMenuCommands = false)
  {
    IReadOnlyCollection<int> objectTypes1 = objectTypes;
    string caption1 = caption;
    string str = rootNodeCaption;
    string description1 = description;
    string rootNodeCaption1 = str;
    long options1 = (long) options;
    string operationName1 = operationName;
    System.IServiceProvider nodesContext1 = nodesContext;
    DynamicSelectionEventHandler dynamicHandler1 = dynamicHandler;
    IReadOnlyCollection<ConditionStructure> conditions1 = conditions;
    int num = disableGlobalContextMenuCommands ? 1 : 0;
    return SelectDialog.Objects(objectTypes1, (IReadOnlyCollection<long>) null, caption1, description1, rootNodeCaption1, (SelectionOptions) options1, operationName1, nodesContext1, dynamicHandler1, conditions1, num != 0);
  }

  /// <summary>Вызывать диалог выбора объекта</summary>
  /// <param name="objectVersionIDs">Коллекция идентификаторов версий объектов, которые доступны для выбора</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="description">Описание действия</param>
  /// <param name="rootNodeCaption">Заголовок ноды, объединяющей типы объектов. Если null или пустой, то будет использовано стандартное значение "Типы объектов"</param>
  /// <param name="options">Настройки вида окна</param>
  /// <param name="operationName">Имя операции. Используется для сохранения и чтения из пользовательских настроек типа объекта, который был
  /// выбран в дереве в последний раз, когда производился выбор объекта в контексте данной операции.
  /// 
  /// Например, когда диалог выбора объекта объекта вызывался для выбора объекта для импорта в структуру задач
  /// IMProject, то пользователь может выбрать любой тип объекта, однако первоначально ему будут предложены
  /// объекты того типа, который был использован именно при последнем импорте объектов в IMProject.
  /// 
  /// Так во-первых быстрее - в 99% процентов случаев тип объекта будет использован повторно, во-вторых если до
  /// этого пользователь где-то вызывал окно выбора объекта в контексте иной операции, то там может быть другой
  /// тип объекта (а формы выбора кэшируются и по-умолчанию будет показана последняя использованная)
  /// И наконец в-третьих - при первом вызове формы выбора по умолчанию будет сфокусирована нода "Все типы
  /// объектов" загрузка вьюшки которой длится секунд 5, что лично мне делает больно</param>
  /// <param name="nodesContext">Контейнер сервисов контекста</param>
  /// <param name="dynamicHandler">Обработчик выбора, позволяющий указывать какие объекты можно выбирать, а какие нет</param>
  /// <param name="conditions">Набор условий фильтрации объектов</param>
  /// <param name="disableGlobalContextMenuCommands">Отключить ли в контекстном меню списка объектов все глобальные команды</param>
  /// <returns>Список интерфейсов идентификаторов выбранных объектов. Если операция выбора будет отменена, то null</returns>
  [CanBeNull]
  [ItemNotNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal static IReadOnlyList<IDBObjectID> Objects(
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<long> objectVersionIDs,
    [CanBeEmpty] int objectType = -1,
    [CanBeNull] string caption = null,
    [CanBeNull] string description = null,
    [CanBeNull] string rootNodeCaption = null,
    SelectionOptions options = SelectionOptions.SelectObjects,
    [CanBeNull] string operationName = null,
    [CanBeNull] System.IServiceProvider nodesContext = null,
    [CanBeNull] DynamicSelectionEventHandler dynamicHandler = null,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions = null,
    bool disableGlobalContextMenuCommands = false)
  {
    IReadOnlyCollection<long> objectVersionIDs1 = (IReadOnlyCollection<long>) ((object) objectVersionIDs ?? (object) Array.Empty<long>());
    return SelectDialog.Objects((IReadOnlyCollection<int>) new int[1]
    {
      objectType != -1 ? objectType : MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<long>) objectVersionIDs1)
    }, objectVersionIDs1, caption, description, rootNodeCaption, options, operationName, nodesContext, dynamicHandler, conditions, disableGlobalContextMenuCommands);
  }

  /// <summary>Вызывать диалог выбора объекта</summary>
  /// <param name="objectTypes">Последовательность идентификаторов типов объектов, объекты которых доступны для выбора</param>
  /// <param name="caption">Заголовок окна</param>
  /// <param name="description">Описание действия</param>
  /// <param name="rootNodeCaption">Заголовок ноды, объединяющей типы объектов. Если null или пустой, то будет использовано стандартное значение "Типы объектов"</param>
  /// <param name="options">Настройки вида окна</param>
  /// <param name="operationName">Имя операции. Используется для сохранения и чтения из пользовательских настроек типа объекта, который был
  /// выбран в дереве в последний раз, когда производился выбор объекта в контексте данной операции.
  /// 
  /// Например, когда диалог выбора объекта объекта вызывался для выбора объекта для импорта в структуру задач
  /// IMProject, то пользователь может выбрать любой тип объекта, однако первоначально ему будут предложены
  /// объекты того типа, который был использован именно при последнем импорте объектов в IMProject.
  /// 
  /// Так во-первых быстрее - в 99% процентов случаев тип объекта будет использован повторно, во-вторых если до
  /// этого пользователь где-то вызывал окно выбора объекта в контексте иной операции, то там может быть другой
  /// тип объекта (а формы выбора кэшируются и по-умолчанию будет показана последняя использованная)
  /// И наконец в-третьих - при первом вызове формы выбора по умолчанию будет сфокусирована нода "Все типы
  /// объектов" загрузка вьюшки которой длится секунд 5, что лично мне делает больно</param>
  /// <param name="nodesContext">Контейнер сервисов контекста</param>
  /// <param name="dynamicHandler">Обработчик выбора, позволяющий указывать какие объекты можно выбирать, а какие нет</param>
  /// <param name="conditions">Набор условий фильтрации объектов</param>
  /// <param name="disableGlobalContextMenuCommands">Отключить ли в контекстном меню списка объектов все глобальные команды</param>
  /// <returns>Список интерфейсов идентификаторов выбранных объектов. Если операция выбора будет отменена, то null</returns>
  [CanBeNull]
  [ItemNotNull]
  internal static IReadOnlyList<IDBObjectID> Objects(
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<int> objectTypes,
    [CanBeNull, ItemNotEmpty] IReadOnlyCollection<long> objectVersionIDs,
    [CanBeNull] string caption,
    [CanBeNull] string description,
    [CanBeNull] string rootNodeCaption,
    SelectionOptions options,
    [CanBeNull] string operationName,
    [CanBeNull] System.IServiceProvider nodesContext,
    [CanBeNull] DynamicSelectionEventHandler dynamicHandler,
    [CanBeNull] IReadOnlyCollection<ConditionStructure> conditions,
    bool disableGlobalContextMenuCommands)
  {
    objectTypes = (IReadOnlyCollection<int>) ((object) objectTypes ?? (object) Array.Empty<int>());
    int parentObjectTypeId = MetaDataHelper.GetCommonParentObjectTypeID((IEnumerable<int>) objectTypes);
    caption = !string.IsNullOrEmpty(caption) ? caption : (options.HasFlag((Enum) SelectionOptions.DisableMultiselect) ? (parentObjectTypeId != -1 ? string.Format(LocalizationHolder.rm.GetString("Choice_0"), (object) SelectDialog.FirstCharToLower(MetaDataHelper.GetObjectType(parentObjectTypeId)?.ObjectName ?? string.Empty)) : LocalizationHolder.rm.GetString("Client.Core_1130")) : (parentObjectTypeId != -1 ? string.Format(LocalizationHolder.rm.GetString("Choice_0"), (object) SelectDialog.FirstCharToLower(MetaDataHelper.GetObjectType(parentObjectTypeId)?.ObjectTypeName ?? string.Empty)) : LocalizationHolder.rm.GetString("Client.Core_1633")));
    IDescriptor rootDescriptor = objectVersionIDs != null ? (IDescriptor) new ObjectsSelectionDescriptor(parentObjectTypeId, !string.IsNullOrWhiteSpace(rootNodeCaption) ? rootNodeCaption : caption, objectVersionIDs, conditions) : (conditions != null ? (IDescriptor) new ObjectsSelectionDescriptor(parentObjectTypeId, !string.IsNullOrWhiteSpace(rootNodeCaption) ? rootNodeCaption : caption, conditions) : Intermech.Navigator.DBObjectTypes.Descriptor.CreateComposition((IEnumerable<int>) objectTypes, rootNodeCaption));
    description = !string.IsNullOrEmpty(description) ? description : caption;
    if (!string.IsNullOrEmpty(operationName))
    {
      int length = operationName.Length;
      if (length > 32 /*0x20*/)
        operationName = operationName.Substring(length - 32 /*0x20*/);
    }
    SelectDialog._operationName = operationName;
    SelectDialog._preSelectObjectType = options.HasFlag((Enum) SelectionOptions.HideTree) ? new int?() : SelectDialog.GetOperationObjectType();
    if (conditions != null)
      options |= SelectionOptions.HideTree;
    if (disableGlobalContextMenuCommands)
    {
      ServiceContainer serviceContainer = new ServiceContainer(nodesContext);
      serviceContainer.AddService<ChildrenView.GetMenuServiceContainerDelegate>(new ChildrenView.GetMenuServiceContainerDelegate(DialogChildrenView.DisableGlobalCommandProviders));
      nodesContext = (System.IServiceProvider) serviceContainer;
    }
    Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow += new SelectionWindowBeforeShow(SelectDialog.SelectionWindow_OnSelectionWindowBeforeShow);
    Intermech.Navigator.SelectionWindow.OnSelectionWindowAfterClose += new SelectionWindowAfterClose(SelectDialog.SelectionWindow_OnSelectionWindowAfterClose);
    object[] source;
    try
    {
      source = Intermech.Navigator.SelectionWindow.Select(caption, description, rootDescriptor, typeof (IDBObjectID), dynamicHandler, nodesContext, options, (int[]) null);
    }
    finally
    {
      Intermech.Navigator.SelectionWindow.OnSelectionWindowAfterClose -= new SelectionWindowAfterClose(SelectDialog.SelectionWindow_OnSelectionWindowAfterClose);
      Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow -= new SelectionWindowBeforeShow(SelectDialog.SelectionWindow_OnSelectionWindowBeforeShow);
    }
    if (source == null)
      return (IReadOnlyList<IDBObjectID>) null;
    return (IReadOnlyList<IDBObjectID>) source.OfType<IDBObjectID>().AsList<IDBObjectID>(source.Length);
  }

  [NotNull]
  public static string FirstCharToLower([NotNull] string input)
  {
    if (string.IsNullOrEmpty(input))
      throw new ArgumentException("input is null or empty");
    return input.First<char>().ToString().ToLower() + input.Substring(1);
  }

  /// <summary>Обработчик события SelectionWindow.OnSelectionWindowBeforeShow, будет вызван перед показом диалога выбора объекта
  /// Первоначально написано для того, чтобы после показа окна выбора объекта автоматом выбирать в нём тот тип объекта,
  /// который был выбран при последнем выборе объекта в рамках именованной операции</summary>
  private static void SelectionWindow_OnSelectionWindowBeforeShow([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Intermech.Navigator.Controls.SelectionWindow selectionWindow = sender as Intermech.Navigator.Controls.SelectionWindow;
    if (SelectDialog._preSelectObjectType.HasValue && !SelectDialog.SelectObjectType(selectionWindow, SelectDialog._preSelectObjectType.Value))
      SelectDialog._preSelectObjectType = new int?();
    selectionWindow.AcceptButton = (IButtonControl) selectionWindow.btOK;
    selectionWindow.CancelButton = (IButtonControl) selectionWindow.btCancel;
    selectionWindow.Shown += new EventHandler(SelectDialog.selectionWindow_Shown);
  }

  /// <summary>Обработчик события Shown окна выбора объекта
  /// Сделано для того, чтобы сфокусироваться по-умолчанию на гриде, т.к. пользователь выбирает всё-таки объекта, которые у нас в гриде,
  /// а не в дереве, где типы объектов, а так же для того, чтобы скрывать верхнюю панель с описанием если описания нет</summary>
  private static void selectionWindow_Shown([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    (sender as Intermech.Navigator.Controls.SelectionWindow).ViewsManager.ViewsUpdated += new EventHandler(SelectDialog.ViewsManager_ViewsUpdated);
  }

  /// <summary>Обработчик события, срабатывающего после обновления закладок</summary>
  private static void ViewsManager_ViewsUpdated([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    PageViewsManager pageViewsManager = sender as PageViewsManager;
    pageViewsManager.ViewsUpdated -= new EventHandler(SelectDialog.ViewsManager_ViewsUpdated);
    IViewPage activeViewPage = pageViewsManager.ActiveViewPage;
    int num;
    if (activeViewPage == null)
    {
      num = 0;
    }
    else
    {
      bool? canFocus = activeViewPage.Control?.CanFocus;
      bool flag = true;
      num = canFocus.GetValueOrDefault() == flag & canFocus.HasValue ? 1 : 0;
    }
    if (num == 0)
      return;
    pageViewsManager.ActiveViewPage.Control.Focus();
  }

  /// <summary>Загрузить из кэша, или, в случае отсутствия в кеше - из пользовательских настроек настроек идентификатор последнего
  /// использованного пользователем при выбора объекта в контексте именованной операции () типа объектов
  /// Если нужной настройки нет, то вернёт null</summary>
  private static int? GetOperationObjectType()
  {
    if (SelectDialog._lockResoterObjectTypeCounter > 0)
      return new int?();
    if (SelectDialog._operationName == null)
      return new int?();
    if (SelectDialog._operationObjectTypes.ContainsKey(SelectDialog._operationName))
      return SelectDialog._operationObjectTypes[SelectDialog._operationName];
    int? nullable1 = new int?(Session.Invoke<int>((Session.SessionHandler<int>) (session => (int) session.Configurations.ReadInteger("CLIENT", SelectDialog._operationName, "PreSelectObjectType", -1L, DBConfigMode.UserAndGlobal))));
    int? nullable2 = nullable1;
    int num = -1;
    int? nullable3;
    if (nullable2.GetValueOrDefault() == num & nullable2.HasValue)
    {
      nullable2 = new int?();
      nullable3 = nullable2;
    }
    else
      nullable3 = nullable1;
    int? operationObjectType = nullable3;
    SelectDialog._operationObjectTypes[SelectDialog._operationName] = operationObjectType;
    return operationObjectType;
  }

  /// <summary>Обработчик события SelectionWindow.OnSelectionWindowAfterClose, будет вызван после закрытия диалога выбора объекта
  /// Первоначально написано для того, чтобы после закрытия окна выбора объекта автоматически сохранять настройки выбранного в
  /// дереве типа объекта, для последующего восстановления при показе диалога выбора в рамках той же именованной операции</summary>
  private static void SelectionWindow_OnSelectionWindowAfterClose([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Intermech.Navigator.Controls.SelectionWindow selectionWindow = sender as Intermech.Navigator.Controls.SelectionWindow;
    if (selectionWindow.DialogResult != DialogResult.OK)
      return;
    int inTreeObjectType = SelectDialog.GetSelectedInTreeObjectType(selectionWindow);
    int num1 = inTreeObjectType;
    int? nullable1 = SelectDialog._preSelectObjectType;
    int valueOrDefault = nullable1.GetValueOrDefault();
    if (num1 == valueOrDefault & nullable1.HasValue)
      return;
    SelectDialog._preSelectObjectType = new int?(inTreeObjectType);
    nullable1 = SelectDialog._preSelectObjectType;
    int num2 = -1;
    int? nullable2;
    if (nullable1.GetValueOrDefault() == num2 & nullable1.HasValue)
    {
      nullable1 = new int?();
      nullable2 = nullable1;
    }
    else
      nullable2 = SelectDialog._preSelectObjectType;
    SelectDialog._preSelectObjectType = nullable2;
    if (SelectDialog._operationName == null)
      return;
    SelectDialog._operationObjectTypes[SelectDialog._operationName] = SelectDialog._preSelectObjectType;
    SelectDialog.SaveLastTimeUsedObjectType();
  }

  /// <summary>Сохранить в настройки идентификатор последнего использованного пользователем при выборе импортируемого объекта типа объектов
  /// сохранённого ранее в статическом поле _preSelectObjectType</summary>
  private static void SaveLastTimeUsedObjectType()
  {
    Session.Invoke<int>((Session.SessionHandler<int>) (session => session.Configurations.WriteInteger("CLIENT", SelectDialog._operationName, "PreSelectObjectType", SelectDialog._preSelectObjectType.HasValue ? (long) SelectDialog._preSelectObjectType.Value : -1L)));
  }

  /// <summary>Выбрать в дереве навигатора тип объекта с переданным навигатором
  /// предназначенного для использования в случаях когда типов объектов для выбора объектов много, однако желательно пользователю подсовывать тот,
  /// который он выбрал в последний раз, оставляя возможность выбрать любой другой тип объекта. Например при импорте структуры объектов в
  /// структуру задач IMProject пользователь чаще всего выберет Сборочные единицы и ему лучше сразу показывать диалог выбора объектов с этим типом объекта,
  /// однако ему надо оставить возможность выбора чего-нибудь "экзотического"
  /// 
  /// Use as is, ответственность за неправильное использование лежит на том, кто использует, писать защиту от дурака и неправильного использования метода
  /// не планируется</summary>
  /// <param name="selectionWindow">Окно выбора объекта, получить подписавшись на статическое событие Intermech.Navigator.SelectionWindow.OnSelectionWindowBeforeShow
  /// перед вызовом Intermech.Navigator.SelectionWindow.Select (не забыть потом отписаться)</param>
  /// <param name="objectType">Тип объекта, который должен быть выбран.
  /// Если передать Intermech.Consts.NavigatorUndefinedObjectTypeID ( = -1 ), то будет выбран узел "Все объекты", что может быть актуально
  ///   т.к. окна SelectionWindow кэшируются, там может быть выбран тип объекта, использованный в рамках клиентской сессии в другой операции.
  /// Если передать 0, то выбор не поменяется</param>
  /// <returns>True если всё ок, False - если тип объекта не найден (не доступен пользователю, либо вообще не существует)</returns>
  private static bool SelectObjectType([NotNull] Intermech.Navigator.Controls.SelectionWindow selectionWindow, int objectType)
  {
    if (objectType == 0)
      return false;
    int objType = objectType;
    if (objectType != -1 && Session.Invoke<bool>((Session.SessionHandler<bool>) (session => session.GetObjectType(objType, false) == null)))
      objectType = -1;
    NavigatorTreeView navTreeView = selectionWindow.NavTreeView;
    NavigatorTreeNode parentNode = navTreeView.RootNode;
    if (objectType != -1)
    {
      foreach (int objectType1 in DBHelper.GetObjectTypeParentsEnumeration(objectType).Reverse<int>().Append<int>(objectType))
      {
        NavigatorTreeNode childNode = SelectDialog.FindChildObjectTypeNode(parentNode, objectType1);
        if (childNode != null)
        {
          if (childNode.NodeID != null && childNode.NodeID.TypeID != objectType)
            navTreeView.ExpandNodeAndWaitForFull(childNode);
          else
            navTreeView.SetNodeExpanded(childNode, false);
          if (parentNode.Children != null)
          {
            foreach (NavigatorTreeNode node in parentNode.Children.Where<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (otherChildNode => otherChildNode != childNode && otherChildNode.Expanded)))
              navTreeView.SetNodeExpanded(node, false);
          }
          parentNode = childNode;
        }
      }
    }
    if (navTreeView.FocusedNode != parentNode)
      navTreeView.FocusedNode = parentNode;
    if (parentNode != null && navTreeView.TopRow != parentNode.Handle)
      navTreeView.TopRow = parentNode.Handle;
    return true;
  }

  /// <summary>Найти среди дочерних нод данной ноду, представляющую тип объекта с переданным идентификатором</summary>
  [CanBeNull]
  private static NavigatorTreeNode FindChildObjectTypeNode(
    [NotNull] NavigatorTreeNode parentNode,
    [NotEmpty] int objectType)
  {
    NavigatorTreeNodes children = parentNode.Children;
    return children != null ? children.FirstOrDefault<NavigatorTreeNode>((Func<NavigatorTreeNode, bool>) (childNode => childNode.NodeID != null && childNode.NodeID.CategoryID == 4 && childNode.NodeID.TypeID == objectType)) : (NavigatorTreeNode) null;
  }

  /// <summary>Раскрыть узел дерева навигатора и дождаться завершения всех работ, связанных с раскрытием узла (там многопоточность)</summary>
  public static void ExpandNodeAndWaitForFull(
    [NotNull] this NavigatorTreeView treeView,
    [NotNull] NavigatorTreeNode treeNode)
  {
    if (treeNode.Expanded)
      return;
    treeView.SetNodeExpanded(treeNode, true);
    int num = 0;
    while (!treeNode.Full)
    {
      if (!treeNode.Expanded)
        treeView.SetNodeExpanded(treeNode, true);
      Thread.Sleep(50);
      if (num++ > 200)
      {
        treeNode.Full = true;
        break;
      }
    }
  }

  /// <summary>Позволяет получить выбранный в дереве тип объекта Написано для того, чтобы при повторении операции, в ходе
  /// которой показывается стандартный диалог выбора объекта, сразу показывать пользователю тот тип объекта, который был
  /// выбран в последний раз при выполнении той же самой операции. Например при импорте структуры объектов в структуру
  /// задач IMProject пользователь чаще всего выберет Сборочные единицы и ему лучше сразу показывать диалог выбора
  /// объектов с этим типом объекта, однако ему надо оставить возможность выбора чего-нибудь "экзотического"</summary>
  /// <param name="selectionWindow">Окно выбора объекта, получить подписавшись на статическое событие
  /// Intermech.Navigator.SelectionWindow.OnSelectionWindowAfterClose перед вызовом
  /// Intermech.Navigator.SelectionWindow.Select (не забыть потом отписаться)</param>
  /// <returns>Выбранный в окне тип объекта. Intermech.Consts.NavigatorUndefinedObjectTypeID ( = -1 ) если выбран узел "Все
  /// объекты"</returns>
  private static int GetSelectedInTreeObjectType([NotNull] Intermech.Navigator.Controls.SelectionWindow selectionWindow)
  {
    NavigatorTreeNode focusedNode = selectionWindow.NavTreeView.FocusedNode;
    return focusedNode?.NodeID != null && (focusedNode.NodeID.CategoryID == 4 || focusedNode.NodeID.CategoryID == Intermech.Navigator.Consts.CategoryMultipleObjectsNode) ? focusedNode.NodeID.TypeID : -1;
  }

  public static void LockResoterObjectType() => ++SelectDialog._lockResoterObjectTypeCounter;

  public static void UnlockResoterObjectType() => --SelectDialog._lockResoterObjectTypeCounter;
}
