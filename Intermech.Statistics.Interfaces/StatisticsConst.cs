// Decompiled with JetBrains decompiler
// Type: Intermech.Statistics.StatisticsConst
// Assembly: Intermech.Statistics.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BE126060-F77F-4F0A-893B-FA8B66A88C31
// Assembly location: D:\IPS\Client\Intermech.Statistics.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Statistics.Interfaces.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Statistics;

public class StatisticsConst
{
  public static string ImageKeyName = "imgStatistics";
  public static string ModuleName = "Статистика";
  public static string CommandName = "View.Statistics";
  public static Guid StatisticsDockControlGuid = new Guid("0C45A149-2E75-4175-820B-D7E1B9685254");
  public static string AllTypes = "Все типы объектов";
  public static string CHART_ABSENCE_MESSAGE = "Отсутствуют данные для отображения графика.";
  public static string TOO_MUCH_DATA_MESSAGE = "Слишком много данных для отображения. Откорректируйте запрос.";
  public static int MAX_RESULT_VALUE_AMOUNT_ON_CHART = 100;
  /// <summary>имя модуля в настройках</summary>
  public static string MODULE_NAME = "Statistics";
  /// <summary>имя секции настроек</summary>
  public static string SETTINGS = "Settings";
  /// <summary>имя параметра - Видеть все задачи статистики</summary>
  public static string CANSHOWALLOBJECTS = "CanShowAllObjects";
  /// <summary>Гуид типа Объекты сбора статистики</summary>
  public static Guid StatisticsObjectsTypeGuid = new Guid("cadd967a-306c-11d8-b4e9-00304f19f545");
  /// <summary>ИД типа Объекты сбора статистики</summary>
  public static int StatisticsObjectsTypeID = -1;
  /// <summary>Гуид типа Задачи сбора статистики</summary>
  public static Guid StatisticsTasksObjectsTypeGuid = new Guid("cadd967b-306c-11d8-b4e9-00304f19f545");
  /// <summary>ИД типа задачи сбора статистики</summary>
  public static int StatisticsTasksObjectsTypeID = -1;
  /// <summary>Гуид типа Команды сбора статистики</summary>
  public static Guid StatisticsCommandTypeGuid = new Guid("cadd967c-306c-11d8-b4e9-00304f19f545");
  /// <summary>ИД типа Команды сбора статистики</summary>
  public static int StatisticsCommandTypeID = -1;
  /// <summary>ИД типа пользователи</summary>
  public static int UserTypeID = -1;
  /// <summary>ИД типа группы пользователей</summary>
  public static int GroupTypeID = -1;
  /// <summary>Guid типа Подразделения</summary>
  public static readonly Guid DepartmentTypeGuid = new Guid("cadd9232-306c-11d8-b4e9-00304f19f545");
  /// <summary>Guid типа Отчеты</summary>
  public static readonly Guid ReportObjectsTypeGuid = new Guid("cad00293-306c-11d8-b4e9-00304f19f545");
  /// <summary>ИД типа простая вертикальная связь</summary>
  public static int SimpleRelationTypeID = -1;
  /// <summary>Guid шага ЖЦ Создание объекта статистики</summary>
  public static string StatisticsObjectsCreated = "cadd967e-306c-11d8-b4e9-00304f19f545";
  /// <summary>Guid шага ЖЦ Сбор статистики</summary>
  public static string StatisticsObjectsInProcess = "cadd967f-306c-11d8-b4e9-00304f19f545";
  /// <summary>Guid шага ЖЦ Удаление объекта статистики</summary>
  public static string StatisticsObjectsDeleted = "cadd9680-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// GUID Атрибута часто используемые (Содержит пользователей и количество запусков этого объекта
  /// конкретным пользователем)
  /// </summary>
  public static string OftenUsed = "cadd9682-306c-11d8-b4e9-00304f19f545";
  /// <summary>GUID Атрибута Дата начала сбора статистики</summary>
  public static string CollectStartDate = "cadd9683-306c-11d8-b4e9-00304f19f545";
  /// <summary>GUID Атрибута Дата последнего сбора статистики</summary>
  public static string CollectLastDate = "cadd9684-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// GUID Атрибута Метод сбора статистики (Способ получения временной характеристики объектов,
  /// относительно которой нужно производить сбор статистики)
  /// </summary>
  public static string CollectMethod = "cadd9686-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// GUID Атрибута Настройки сбора статистики (Может содержать гуиды шага ЖЦ, уровня продвижения,
  /// атрибута и т.п. в зависимости от способа сбора статистики)
  /// </summary>
  public static string CollectionSettings = "cadd9687-306c-11d8-b4e9-00304f19f545";
  /// <summary>Шаблон для статистики вертикальный А4</summary>
  public static Guid VerticalA4TemplateGuid = new Guid("cadd9aa3-306c-11d8-b4e9-00304f19f545");
  /// <summary>Шаблон для статистики горизонтальный А4</summary>
  public static Guid HorizontalA4TemplateGuid = new Guid("cadd9aa9-306c-11d8-b4e9-00304f19f545");
  /// <summary>Шаблон для статистики вертикальный А3</summary>
  public static Guid VerticalA3TemplateGuid = new Guid("cadd9ab9-306c-11d8-b4e9-00304f19f545");
  /// <summary>Шаблон для статистики горизонтальный А3</summary>
  public static Guid HorizontalA3TemplateGuid = new Guid("cadd9ab7-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Шаблон для статистики по датам горизонтальный
  /// Не используется
  /// </summary>
  public static Guid HorizontalByDateTemplateGuid = new Guid("cadd9aab-306c-11d8-b4e9-00304f19f545");
  /// <summary>Многоуровневый шаблон</summary>
  public static Guid MultilevelHorizontalTemplateGuid = new Guid("cadd9ab0-306c-11d8-b4e9-00304f19f545");
  /// <summary>
  /// Процент по умолчанию от среднеквадратичного отклонения, на который может отличаться значение от среднего значения выборки
  /// </summary>
  public static readonly uint DefaultDeviationPercentage = 200;
  /// <summary>
  /// Какую долю значений отсекать при вычислении усеченного среднего
  /// </summary>
  public static double DefaultTrimmedValues = 0.2;
  /// <summary>
  /// Список всех возможных типов объектов выборок и классификаторов
  /// </summary>
  public static List<int> AllSelectionsTypes;
  /// <summary>
  /// Список всех возможных типов объектов типа схема поиска данных
  /// </summary>
  public static List<int> AllSchemeTypes;
  /// <summary>
  /// Начальное время инициализации контрола назначения времени в конфигураторах команд и задач сбора статистики
  /// </summary>
  public static DateTime StartTimeInitial = new DateTime(2019, 9, 16 /*0x10*/, 0, 0, 0, 0);
  /// <summary>
  /// Начальное время инициализации контрола назначения времени в конфигураторах команд и задач сбора статистики
  /// </summary>
  public static DateTime EndTimeInitial = new DateTime(2019, 9, 16 /*0x10*/, 23, 59, 0, 0);

  /// <summary>ID типа Подразделения</summary>
  public static int DepartmentTypeId { get; set; }

  /// <summary>Инициализация констант модуля статистики.</summary>
  /// <param name="session">The session.</param>
  public static void Init(IUserSession session)
  {
    StatisticsConst.AllSelectionsTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00119-306c-11d8-b4e9-00304f19f545"));
    StatisticsConst.AllSchemeTypes = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid("cad00129-306c-11d8-b4e9-00304f19f545"));
    StatisticsConst.GroupTypeID = session.IdentHelper.GroupsTypeID;
    StatisticsConst.UserTypeID = session.IdentHelper.UsersTypeID;
    StatisticsConst.SimpleRelationTypeID = session.IdentHelper.SimpleRelationTypeID;
    StatisticsConst.DepartmentTypeId = session.GetObjectType(StatisticsConst.DepartmentTypeGuid).ObjectType;
    StatisticsConst.StatisticsObjectsTypeID = session.GetObjectType(StatisticsConst.StatisticsObjectsTypeGuid).ObjectType;
    StatisticsConst.StatisticsCommandTypeID = session.GetObjectType(StatisticsConst.StatisticsCommandTypeGuid).ObjectType;
  }
}
