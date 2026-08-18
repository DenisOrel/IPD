// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IFiltrationService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Bars;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс сервиса по управлению тулбаром "Фильтрация состава" в главной форме приложения
/// </summary>
public interface IFiltrationService
{
  /// <summary>
  /// Событие вызывается после смены настроек фильтрации состава на новое значение
  /// </summary>
  event FiltrationChanged OnFiltrationChanged;

  /// <summary>
  /// Свойство позволяет разрешить или запретить пользователю вносить изменения в настройки фильтрации
  /// </summary>
  bool Enabled { get; set; }

  /// <summary>
  /// Текущие настройки фильтрации для владельца OwnerID.
  /// Изменения настроек сразу вносятся в базу данных.
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  IFiltrationSettings Filtration { get; }

  /// <summary>
  /// OBJECT_ID текущего правила подбора версий. Поле считывается из свойства Filtration.
  /// </summary>
  long FiltrationRuleID { get; }

  /// <summary>
  /// Уникальный ID владельца текущих настроек фильтрации.
  /// Изменение владельца ведёт к перечитыванию настроек фильтрации состава.
  /// Автоматически будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  string FiltrationServiceOwnerID { get; set; }

  /// <summary>
  /// Текущее правило фильтрации состава, информация по которому отображена в тулбаре "Фильтрация состава"
  /// Причём если у правила есть переменные, то они уже заданы согласно текущей настройке фильтрации.
  /// Можно задавать свои экземпляры правил подбора версий, причём не связанные с реальными объектами
  /// из базы данных. Например, правила подбора из группирующих объектов.
  /// 
  /// Если нет текущего окна, то данное значение будет представять собой текущее правило редактирования по умолчанию.
  /// </summary>
  VersionsRule RuleClass { get; set; }

  /// <summary>
  /// Если выбранное правило является вариантом значений переменных (т.е. создано на базе родительского правила),
  /// то это поле отражает, совместимо ли правило с родительским вариантом (на случай, если были изменения
  /// в родительском правиле после создания вариантов его значений переменных)
  /// </summary>
  bool RuleCompatible { get; }

  /// <summary>
  /// Валидно ли выбранное правило подбора версий
  /// (для проверки выполняется метод Valid правила, а также проверяется наличие у него переменных значений)
  /// Если _FSRuleValid = false, правило применять нельзя
  /// </summary>
  bool RuleValid { get; }

  /// <summary>
  /// Код ошибки для текущего правила:
  /// 0 - правило не выбрано,
  /// 1 - настройки недействительны - правило было изменено,
  /// 2 - нет ошибок, правило настроено,
  /// 3 - нет вариантов значений переменных для правила,
  /// 4 - фильтрация состава выключена (obsolete),
  /// 5 - не указан основной вариант значений переменных,
  /// 6 - правило является некорректным
  /// </summary>
  CurrentRuleErrors RuleErrorCode { get; }

  /// <summary>
  /// Свойство, определяющее видимость тулбара "Фильтрация состава"
  /// </summary>
  bool FiltrationToolbarVisible { get; set; }

  /// <summary>
  /// Свойство, определяющее "скрытость" тулбара "Фильтрация состава"
  /// </summary>
  bool FiltrationToolbarHidden { get; set; }

  /// <summary>
  /// Применить все изменения в настройках фильтрации состава к тулбару "Фильтрация состава"
  /// (с их сохранением в базе данных).
  /// При необходимости будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава.
  /// </summary>
  /// <param name="fireEvent">true - генерировать событие</param>
  void FiltrationApplyUpdates(bool fireEvent);

  /// <summary>
  /// Перечитать настройки фильтрации состава, обновить тулбар "Фильтрация состава"
  /// </summary>
  /// <param name="FireEvent">true - будет вызвано событие, уведомляеющее о смене в настройках фильтрации состава</param>
  void FiltrationUpdate(bool FireEvent);

  /// <summary>Добавить новую кнопку на панель "Фильтрация состава"</summary>
  /// <returns></returns>
  ButtonItem AddNewButton();

  /// <summary>
  /// Добавить новый выпадающий список на панель "Фильтрация состава"
  /// </summary>
  /// <returns></returns>
  ComboBoxItem AddNewCombobox();

  /// <summary>Начать обновления в сервисе</summary>
  void BeginUpdates();

  /// <summary>Завершить обновления в сервисе</summary>
  void EndUpdates();

  ToolBar ToolBar { get; }
}
