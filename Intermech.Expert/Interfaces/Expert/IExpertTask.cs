// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.IExpertTask
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.Collections.Generic;
using System.Xml;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Интерфейс для работы с задачей экспертной системы</summary>
public interface IExpertTask : IDisposable
{
  /// <summary>
  /// Старт новой задачи - только для низкоуровневых операций
  /// </summary>
  /// <returns>Идентификатор задачи ЭС (TaskId)</returns>
  int StartTask();

  /// <summary>Прерывает работу, выполняемую текущей задачей</summary>
  void Abort();

  /// <summary>Завершение задачи ЭС</summary>
  void EndTask();

  /// <summary>Идентификатор задачи ЭС - только чтение</summary>
  int TaskId { get; }

  /// <summary>Рассчитать значение атрибута без типа объекта</summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="Value">Возвращаемое значение или null</param>
  /// <returns>Код результата ЭС</returns>
  ExpertResult Calculate(int attrTypeID, long objId, out object Value);

  /// <summary>Рассчитать значение атрибута с типом объекта</summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="Value">Рассчитанное значение или null</param>
  /// <returns>Код результата ЭС</returns>
  ExpertResult Calculate(int objTypeID, int attrTypeID, long objId, out object Value);

  /// <summary>
  /// Рассчитать значение атрибута с типом объекта и дополнительными объектами
  /// </summary>
  /// <param name="objTypeID">Тип объекта - образует пару с типом атрибута</param>
  /// <param name="attrTypeID">Attr Type ID</param>
  /// <param name="objId">Идентификатор объекта, для которого проводится расчет</param>
  /// <param name="moreIDs">Дополнительные объекты, чтобы не искать их по правилам поиска</param>
  /// <param name="Value">Result of the calculation</param>
  /// <returns>Код результата ЭС</returns>
  ExpertResult Calculate(
    int objTypeID,
    int attrTypeID,
    long objId,
    long[] moreIDs,
    out object Value);

  /// <summary>
  /// Рассчитать значение атрибута без типа объекта асинхронно, результат вернется в событии EndCalculate
  /// </summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <param name="objId">Идентификатор объекта</param>
  void Calculate(int attrTypeID, long objId);

  /// <summary>
  /// Рассчитать значение атрибута с типом объекта асинхронно, результат вернется в событии EndCalculate
  /// </summary>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="moreIDs">Дополнительные объекты, чтобы не искать их по правилам поиска</param>
  void Calculate(int objTypeID, int attrTypeID, long objId, long[] moreIDs = null);

  /// <summary>Вернуть список недостающих параметров</summary>
  /// <returns>Список недостающих параметров</returns>
  List<CalcAttrPair> GetMissingParms();

  /// <summary>
  /// Показывает юзеру атрибуты, изменившиеся при расчете, и позволяет применить изменения
  /// </summary>
  /// <returns>true, если изменения были проведены</returns>
  bool ApplyCalcResults();

  /// <summary>Рассчитать значение формулы</summary>
  /// <param name="tf">Объект формулы (TempFormula)</param>
  /// <param name="objId">Идентификатор объекта контекста, для которого считается формула</param>
  /// <param name="Value">Результат расчета или null</param>
  /// <returns>Код результата ЭС</returns>
  ExpertResult CalcFormula(object tf, long objId, out object Value);

  /// <summary>
  /// Рассчитать значение формулы, если объект формулы сохранен в базе
  /// </summary>
  /// <param name="formId">Идентификатор объекта формулы</param>
  /// <param name="objId">Идентификатор объекта контекста, для которого считается формула</param>
  /// <param name="Value">Результат расчета или null</param>
  /// <returns>Код результата ЭС</returns>
  ExpertResult CalcFormula(long formId, long objId, out object Value);

  /// <summary>Сгенерировать документ и вернуть его</summary>
  /// <param name="docScriptID">Ид скрипта генерации документа</param>
  /// <param name="context">Набор объектов контекста</param>
  /// <param name="doc">Сгенерированный документ</param>
  /// <returns>Код результата ЭС</returns>
  ExpertResult GenerateDocument(long docScriptID, long[] context, out object doc);

  /// <summary>
  /// Сгенерировать документ и записать его в атрибут "Файлы" заданного объекта.
  /// </summary>
  /// <param name="docScriptID">Ид скрипта генерации документа</param>
  /// <param name="context">Набор объектов контекста</param>
  /// <param name="docObjId">Идентификатор объекта, в который будет записан документ</param>
  /// <returns></returns>
  ExpertResult GenerateDocument(long docScriptID, long[] context, long docObjId);

  /// <summary>
  /// Сгенерировать документ и после этого вызвать событие OnEndGenerate
  /// </summary>
  /// <param name="docScriptID">Ид скрипта генерации документа</param>
  /// <param name="context">Набор объектов контекста</param>
  void GenerateDocument(long docScriptID, long[] context);

  /// <summary>Флаги трассировки</summary>
  ExpertTraceFlags TraceFlags { get; set; }

  /// <summary>Интервал срабатывания события TimerTraceinfo</summary>
  int InfoInterval { get; set; }

  /// <summary>Получить текущую трассировку задачи</summary>
  /// <returns>Информация трассировки в XML</returns>
  /// <remarks>Использовать только при необходимости. При вызове распаковываются данные,
  /// размер которых можен достигать сотен мегабайт для больших документов</remarks>
  XmlDocument GetTraceInfo();

  /// <summary>Показать окно с текущей трассировкой</summary>
  void ShowTraceDialog();

  /// <summary>Получить значение атрибута в контексте задачи</summary>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="attrTypeID">Идентификатор типа объекта</param>
  /// <returns>Значение атрибута</returns>
  object GetParmValue(long objID, int attrTypeID);

  /// <summary>Установить значение атрибута в контексте задачи</summary>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="attrTypeID">Идентификатор атрибута</param>
  /// <param name="Value">Значение атрибута</param>
  void SetParmValue(long objID, int attrTypeID, object Value);

  /// <summary>Получить все значения атрибутов в контексте задачи</summary>
  /// <returns>Таблица значений (ключи типа CalcAttrPair), значения CalculatedAttr</returns>
  Dictionary<CalcAttrPair, CalculatedAttr> GetCalcParms();

  /// <summary>
  /// Получить ИЗМЕНЕННЫЕ значения атрибутов в контексте задачи
  /// </summary>
  /// <returns>Таблица значений (ключи типа CalcAttrPair), значения CalculatedAttr</returns>
  Dictionary<CalcAttrPair, CalculatedAttr> GetModifiedParms();

  /// <summary>Очистить значение параметра</summary>
  void ClearParmValue(long objID, int attrTypeID);

  /// <summary>Установить все значения атрибутов в контексте задачи</summary>
  /// <param name="parms">Таблица значений (ключи типа CalcAttrPair), значения CalculatedAttr</param>
  void SetCalcParms(Dictionary<CalcAttrPair, CalculatedAttr> parms);

  /// <summary>
  /// Записать все значения атрибутов в контексте задачи в базу
  /// </summary>
  void ApplyCalcParms();

  /// <summary>Записать в базу значения атрибутов по списку</summary>
  /// <param name="list">Список сохраняемых атрибутов</param>
  void ApplyCalcParms(List<CalculatedAttr> list);

  /// <summary>
  /// Событие фильтрации объектов для формы применения рассчитанных атрибутов
  /// </summary>
  event FilterObjsEventHandler FilterObjects;

  /// <summary>Показать форму применения рассчитанных атрибутов</summary>
  bool ShowApplyForm();

  /// <summary>Показать форму применения рассчитанных атрибутов</summary>
  /// <param name="excludeObjId">ИД объекта, который надо исключить из измененных</param>
  /// <param name="excludeRelId">ИД связи, которую надо исключить из измененных</param>
  bool ShowApplyForm(long excludeObjId, long excludeRelId);

  /// <summary>Тестирование сервера таблиц</summary>
  /// <param name="objID">ид. версии объекта</param>
  /// <param name="tableID">ид. версии таблицы</param>
  /// <returns>Список результатов расчета</returns>
  object[] CalcTableTest(long objID, long tableID);

  /// <summary>Пересчитать все атрибуты, зависящие от attrTypeId</summary>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <param name="objId">Идентификатор объекта контекста</param>
  void Recalculate(int attrTypeID, long objId, long relId = -1);

  /// <summary>Задать значение параметра</summary>
  /// <param name="Key">Название параметра</param>
  /// <param name="Value">Значение параметра</param>
  void SetTaskParm(string Key, object Value);

  /// <summary>Получить значение параметра</summary>
  /// <param name="Key">Название параметра</param>
  /// <returns>Значение параметра</returns>
  object GetTaskParm(string Key);

  /// <summary>Задать значение нескольких параметров сразу</summary>
  /// <param name="parms">Словарь с параметрами</param>
  void SetTaskParms(Dictionary<string, object> parms);

  /// <summary>Событие, срабатывающее при завершении расчета</summary>
  event EndCalculateEventHandler EndCalculate;

  /// <summary>
  /// Событие, срабатывающее по таймеру и возвращающее информацию трассировки
  /// </summary>
  event GetTraceInfoEventHandler TimerTraceInfo;
}
