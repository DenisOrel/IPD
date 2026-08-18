// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.Interfaces.CommandSettings
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using Intermech.Extensions;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

#nullable disable
namespace Intermech.Statistics.Interfaces;

[Serializable]
public class CommandSettings : ICloneable
{
  private string _lcStep = string.Empty;
  /// <summary>Фильтры, схемы поиска и их корневые объекты</summary>
  private Filters _filters;
  /// <summary>
  /// Подпериоды сбора статистики.
  /// Инициализация один раз при зачитывании значений из хмл-файла
  /// Индекс сохраняется в точках собранной статистики.
  /// Первый период всегда будет считаться не от StartDate а от даты предыдущего периода, т.к. этого требует отображение на линейных графиках.
  /// </summary>
  [XmlArray("Periods")]
  [XmlArrayItem("Period")]
  private List<Period> _periods = new List<Period>();

  /// <summary>
  /// Идентификатор объекта, к которому относятся настройки.
  /// </summary>
  [XmlElement(ElementName = "ObjectID")]
  public long ObjectID { get; set; }

  /// <summary>
  /// УСТАРЕЛ! не пользоваться
  /// Нельзя помечать как Obsolet из-за проблем с совместимостью версий
  /// Фильтрующие объекты
  /// </summary>
  [XmlArray("FilterObjects")]
  [XmlArrayItem("FilterObject")]
  public List<ListItem> FilterObjects { get; set; }

  /// <summary>Общий класс для фильтрующих объектов</summary>
  /// 
  ///             Свойство было добавлено в настройки в целях уменьшения бреда и технического долга в модуле.
  ///             Объединяет в себя данные из FilterObjects и SchemeFilters.
  ///             Часть данных SchemeFilters прежде дублировалась в FilterObjects, не использовалась, и вызывала бесконечные ненужные проверки на тип объекта и путаницу в данных.
  [XmlElement(ElementName = "Filters")]
  public Filters Filters
  {
    get
    {
      if (this._filters.Selections.Count == 0 && this._filters.SearchSchemes.Count == 0 && (this.FilterObjects != null && this.FilterObjects.Count != 0 || this.SchemeFilters != null && this.SchemeFilters.Count != 0))
        this._filters = new Filters(this.FilterObjects, this.SchemeFilters);
      return this._filters;
    }
    set => this._filters = value;
  }

  /// <summary>Настройки подсчета трудоемкости</summary>
  [XmlElement(ElementName = "LaborInput")]
  public LaborInput LaborInput { get; set; }

  /// <summary>Типы анализируемых объектов</summary>
  [XmlArray("AnalizedObjectsTypes")]
  [XmlArrayItem("ObjectType")]
  public List<ObjectTypesListItem> AnalizedObjectsTypes { get; set; }

  /// <summary>Время начала подсчета статистики</summary>
  [XmlElement(ElementName = "StartDateTime")]
  public DateTime StartDateTime { get; set; }

  /// <summary>Время окончания подсчета статистики</summary>
  [XmlElement(ElementName = "EndDateTime")]
  public DateTime EndDateTime { get; set; }

  /// <summary>Тип объекта статистики</summary>
  [XmlElement(ElementName = "StatisticsObjectType")]
  public StatisticsObjectsTypeEnum StatisticsObjectType { get; set; }

  /// <summary>Период сбора статистики</summary>
  [XmlElement(ElementName = "CollectPeriod")]
  public CollectPeriodsEnum CollectPeriod { get; set; }

  /// <summary>Тип команды сбора статистики</summary>
  [XmlElement(ElementName = "CommandType")]
  public CommandStatisticsTypesEnum CommandType { get; set; }

  /// <summary>Индекс периода сбора статистики</summary>
  [XmlElement(ElementName = "CollectPeriodIndex")]
  public int CollectPeriodIndex { get; set; }

  /// <summary>Уровень продвижения</summary>
  [XmlElement(ElementName = "LCLevel")]
  public ListItem LCLevel { get; set; }

  /// <summary>Шаг ЖЦ</summary>
  [XmlElement(ElementName = "LCStep")]
  public string LCStep
  {
    get => this._lcStep == null ? string.Empty : this._lcStep;
    set => this._lcStep = value;
  }

  /// <summary>Атрибут типа Дата</summary>
  [XmlElement(ElementName = "AttrData")]
  public ListItem AttrData { get; set; }

  /// <summary>
  /// УСТАРЕЛ! не пользоваться
  /// Нельзя помечать как Obsolet из-за проблем с совместимостью версий
  /// Класс для Схем поиска данных содержит сам объект и список корневых элементов
  /// </summary>
  [XmlArray("SchemeFilters")]
  [XmlArrayItem("SchemeFilter")]
  public List<SchemeFilter> SchemeFilters { get; set; }

  /// <summary>Список пользователей для задачи статистики</summary>
  [XmlArray("StatisticsUsers")]
  [XmlArrayItem("StatisticsUser")]
  public List<StatisticsUsers> ListUsers { get; set; }

  /// <summary>
  /// Тип пользователей для которых нужно провести статистику (т.к нельзя сравниввать 1 пользователя с подразделением или группой пользователей и т.д)
  /// </summary>
  [XmlElement(ElementName = "StatisticsUsersType")]
  public UsersEnum StatisticsUsersType { get; set; }

  /// <summary>Список задач для анализа</summary>
  [XmlArray("Activities")]
  [XmlArrayItem("ActivityItem")]
  public List<ActivityItem> Activities { get; set; }

  /// <summary>Список шаблонов Workflow для анализа</summary>
  [XmlArray("Templates")]
  [XmlArrayItem("Template")]
  public List<ListItem> Templates { get; set; }

  /// <summary>
  /// Настройки для исключения аномальных значений из графика
  /// </summary>
  [XmlElement(ElementName = "ExcludeValuesSettings")]
  public ExcludeAbnormalValuesSettings ExcludeAbnormalValuesSettings { get; set; }

  /// <summary>
  /// Учитывать ли в расчетах нерабочие дни (праздники, выходные)
  /// Может быть тру только для команд сбора с шагом расчета в день
  /// </summary>
  [XmlElement(ElementName = "IgnoreNotWorkingDays")]
  public bool IgnoreNotWorkingDays { get; set; }

  public CommandSettings()
  {
    this.FilterObjects = new List<ListItem>();
    this.Filters = new Filters();
    this.AnalizedObjectsTypes = new List<ObjectTypesListItem>();
    this.SchemeFilters = new List<SchemeFilter>();
    this.ListUsers = new List<StatisticsUsers>();
    this.Activities = new List<ActivityItem>();
    this.Templates = new List<ListItem>();
    this.StartDateTime = DateTime.Now;
    this.EndDateTime = DateTime.Now;
    this.ExcludeAbnormalValuesSettings = new ExcludeAbnormalValuesSettings();
    this.IgnoreNotWorkingDays = false;
    this.LaborInput = new LaborInput();
  }

  public object Clone()
  {
    CommandSettings newSettings = new CommandSettings()
    {
      ObjectID = this.ObjectID,
      AttrData = this.AttrData,
      CollectPeriod = this.CollectPeriod,
      CollectPeriodIndex = this.CollectPeriodIndex,
      CommandType = this.CommandType,
      EndDateTime = this.EndDateTime,
      LCLevel = this.LCLevel,
      LCStep = this.LCStep,
      StartDateTime = this.StartDateTime,
      StatisticsUsersType = this.StatisticsUsersType,
      StatisticsObjectType = this.StatisticsObjectType,
      Filters = this.Filters.Clone(),
      FilterObjects = new List<ListItem>(this.FilterObjects.Count),
      AnalizedObjectsTypes = new List<ObjectTypesListItem>(this.AnalizedObjectsTypes.Count),
      SchemeFilters = new List<SchemeFilter>(this.SchemeFilters.Count),
      ListUsers = new List<StatisticsUsers>(this.ListUsers.Count),
      Activities = new List<ActivityItem>(this.Activities.Count),
      Templates = new List<ListItem>(this.Templates.Count),
      ExcludeAbnormalValuesSettings = new ExcludeAbnormalValuesSettings(this.ExcludeAbnormalValuesSettings.NeedExcludeAbnormalValues, this.ExcludeAbnormalValuesSettings.Percentage),
      IgnoreNotWorkingDays = this.IgnoreNotWorkingDays,
      LaborInput = this.LaborInput.Clone()
    };
    this.FilterObjects.ForEach((Action<ListItem>) (item => newSettings.FilterObjects.Add(item)));
    this.AnalizedObjectsTypes.ForEach((Action<ObjectTypesListItem>) (item => newSettings.AnalizedObjectsTypes.Add(item)));
    this.SchemeFilters.ForEach((Action<SchemeFilter>) (item => newSettings.SchemeFilters.Add(item)));
    this.ListUsers.ForEach((Action<StatisticsUsers>) (item => newSettings.ListUsers.Add(item)));
    this.Activities.ForEach((Action<ActivityItem>) (item => newSettings.Activities.Add(item)));
    this.Templates.ForEach((Action<ListItem>) (item => newSettings.Templates.Add(item)));
    return (object) newSettings;
  }

  /// <summary>
  /// Подпериоды сбора статистики.
  /// Учитывается и предыдущий период. См. _periods.
  /// Не забываем инициализировать _periods после чтения настроек из хмл
  /// </summary>
  [XmlIgnore]
  public List<Period> Periods => this._periods;

  /// <summary>
  /// Получить полный список анализируемых типов совместно с дочерними
  /// </summary>
  /// <returns>Полный список анализируемых типов совместно с дочерними</returns>
  public List<int> AnalizedTypesIncludingChildTypes()
  {
    List<int> collection = new List<int>();
    foreach (int parentTypeID in this.AnalizedObjectsTypes.Select<ObjectTypesListItem, int>((Func<ObjectTypesListItem, int>) (x => x.ObjectTypeID)).ToArray<int>())
      collection.SafeAddRange<int>((IEnumerable<int>) MetaDataHelper.GetObjectTypeChildrenIDRecursive(parentTypeID));
    return collection;
  }

  /// <summary>Проинициализировать подпериоды сбора статистики</summary>
  public void InitPeriods(List<Period> periods) => this._periods = periods;
}
