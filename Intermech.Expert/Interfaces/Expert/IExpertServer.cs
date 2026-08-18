// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.IExpertServer
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using Intermech.Expert;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

#nullable disable
namespace Intermech.Interfaces.Expert;

public interface IExpertServer
{
  /// <summary>Start an expert server task</summary>
  /// <param name="sessionGUID">User Session GUID</param>
  /// <returns>Task ID</returns>
  int StartTask(Guid sessionGUID);

  /// <summary>Start an expert server task with specific trace flags</summary>
  /// <param name="sessionGUID">User Session GUID</param>
  /// <param name="traceFlags">Expert system trace flags</param>
  /// <returns>Task ID</returns>
  int StartTask(Guid sessionGUID, ExpertTraceFlags traceFlags);

  /// <summary>
  /// End a task that was started by StartTask (use try-finally block)
  /// </summary>
  /// <param name="taskId">Task ID</param>
  void EndTask(int taskId);

  /// <summary>
  /// Abort the process currently performed without closing the task
  /// </summary>
  /// <param name="taskId">Task ID</param>
  void AbortProcess(int taskId);

  /// <summary>Change user session GUID to avoid</summary>
  /// <param name="taskId"></param>
  /// <param name="sessionGUID"></param>
  [Obsolete("Задача должна всегда создаваться в рамках одной сессии!", true)]
  void ChangeUserSession(int taskId, Guid sessionGUID);

  /// <summary>Включить режим тестирования для данной задачи</summary>
  /// <param name="taskId"></param>
  void SetDebugMode(int taskId);

  /// <summary>Задать необходимость проверки на отваливание клиента</summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="ti">Интервал времени</param>
  void SetTimeInterval(int taskId, TimeSpan ti);

  /// <summary>Сообщить серверу, что клиент жив</summary>
  /// <param name="taskId">Идентификатор задачи</param>
  void IAmAlive(int taskId);

  /// <summary>Проверить, можно ли обращаться к задаче</summary>
  /// <param name="taskId">Ид задачи</param>
  /// <returns>true, если задача еще активна</returns>
  bool IsTaskValid(int taskId);

  void SetTrace(int taskId, bool enabled);

  bool GetTrace(int taskId);

  void SetLog(int taskId, bool enabled);

  bool GetLog(int taskId);

  void SetDateTimeFormat(int taskId, DateTimeFormatInfo dfi);

  void SetNumberFormat(int taskId, NumberFormatInfo nfi);

  HybridTableExp GetGlobalObjectsTable(int taskId);

  HybridTableExp GetGlobalLinksTable(int taskId);

  /// <summary>
  /// Set trace flags to include different types of messages
  /// </summary>
  /// <param name="taskId"></param>
  /// <param name="traceFlags">Trace flag set</param>
  void SetTraceFlags(int taskId, ExpertTraceFlags traceFlags);

  /// <summary>Get current trace flags for the task</summary>
  /// <param name="taskId">Task ID</param>
  /// <returns>Current trace flag set</returns>
  ExpertTraceFlags GetTraceFlags(int taskId);

  /// <summary>Get trace info messages for a task</summary>
  /// <param name="taskId">Task ID</param>
  /// <returns>Trace messages in zipped XML</returns>
  byte[] GetTraceInfo(int taskId);

  /// <summary>Get last trace string - for Background tasks</summary>
  /// <param name="taskId"></param>
  /// <returns></returns>
  string GetLastInfo(int taskId);

  /// <summary>Очистка трассировки задачи</summary>
  /// <param name="taskId">ИД задачи</param>
  void DestroyTraceInfo(int taskId);

  /// <summary>
  /// Получить значение параметра (может быть рассчитано в процессе чего-нибудь другого)
  /// </summary>
  /// <param name="taskId">Ид задачи</param>
  /// <param name="objID">Ид объекта</param>
  /// <param name="attrTypeID">Ид типа атрибута</param>
  /// <returns>Значение параметра</returns>
  object GetParmValue(int taskId, long objID, int attrTypeID);

  /// <summary>
  /// Установить значение параметра (НЕ в базе! Только в контексте задачи)
  /// </summary>
  /// <param name="taskId">Ид задачи</param>
  /// <param name="objID">Ид объекта</param>
  /// <param name="attrTypeID">Тип атрибута</param>
  /// <param name="Value">Устанавливаемое значение</param>
  void SetParmValue(int taskId, long objID, int attrTypeID, object Value);

  /// <summary>Записать хранимое значение параметра в базу</summary>
  /// <param name="taskId">Ид задачи</param>
  /// <param name="objID">Ид объекта</param>
  /// <param name="attrTypeID">Тип атрибута</param>
  /// <remarks>Внимание! Метод вызывается в основной сессии задачи!! </remarks>
  void ApplyParmValue(int taskId, long objID, int attrTypeID);

  /// <summary>Удалить рассчитанное значение из контекста задачи</summary>
  /// <param name="taskId">Ид задачи</param>
  /// <param name="objID">Ид объекта</param>
  /// <param name="attrTypeID">Ид типа атрибута</param>
  void DeleteParmValue(int taskId, long objID, int attrTypeID);

  /// <summary>Получить все сохраненные параметры в виде Hashtable</summary>
  /// <param name="taskId">Ид задачи</param>
  /// <returns>Таблица с параметрами. Keys are CalcAttrPairs, Values are CalculatedAttrs</returns>
  Dictionary<CalcAttrPair, CalculatedAttr> GetCalcParms(int taskId);

  /// <summary>
  /// Получить все рассчитанные/измененные параметры в виде Hashtable
  /// </summary>
  /// <param name="taskId">Ид задачи</param>
  /// <returns>Таблица с параметрами. Keys are CalcAttrPairs, Values are CalculatedAttrs</returns>
  Dictionary<CalcAttrPair, CalculatedAttr> GetModifiedParms(int taskId);

  /// <summary>Записать параметры в виде Hashtable</summary>
  /// <param name="taskId">Ид задачи</param>
  /// <param name="parms">Таблица с параметрами (Keys are CalcAttrPairs, Values are CalculatedAttrs)</param>
  void SetCalcParms(int taskId, Dictionary<CalcAttrPair, CalculatedAttr> parms);

  /// <summary>Записать ВСЕ рассчитанные параметры в базу</summary>
  /// <param name="taskId">Ид задачи</param>
  void ApplyCalcParms(int taskId);

  /// <summary>Записать ВСЕ рассчитанные параметры в базу</summary>
  /// <param name="taskId"></param>
  /// <param name="list"></param>
  /// <remarks>Внимание! Метод вызывается в основной сессии задачи!! </remarks>
  void ApplyCalcParms(int taskId, List<CalculatedAttr> list);

  /// <summary>Очистить таблицу рассчитанных параметров</summary>
  /// <param name="taskId">Ид задачи</param>
  void ClearCalcParms(int taskId);

  /// <summary>Очистить один параметр</summary>
  /// <param name="taskId">Ид. задачи</param>
  /// <param name="key">Параметр, который надо очистить</param>
  void ClearCalcParm(int taskId, CalcAttrPair key);

  /// <summary>
  /// Получить список изменений, рассчитанных экспертной системой
  /// (для показа в форме ES_ApplyRes)
  /// </summary>
  /// <param name="taskId">ИД задачи экспертной системы</param>
  /// <returns>Список изменений</returns>
  List<ObjChangedList> GetAttrChangedList(int taskId);

  /// <summary>Применить список изменений к объектам</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <param name="changedList"></param>
  /// <returns>true, если все изменения были применены</returns>
  bool ApplyChangesList(int taskId, List<ObjChangedList> changedList);

  /// <summary>Получить тип данных ЭС для заданного атрибута</summary>
  /// <param name="attrType">ИД атрибута</param>
  /// <returns>Тип данных ЭС</returns>
  DataType GetAttrDataType(int attrType);

  /// <summary>Получить тип данных ЭС для заданного атрибута</summary>
  /// <param name="g">Guid атрибута</param>
  /// <returns>Тип данных ЭС</returns>
  DataType GetAttrDataType(Guid g);

  /// <summary>Calculate attribute value depending on the context</summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="objTypeID">Тип объекта - образует пару с типом атрибута</param>
  /// <param name="attrTypeID">Attr Type ID</param>
  /// <param name="objId">Идентификатор объекта, для которого проводится расчет</param>
  /// <param name="Value">Result of the calculation</param>
  /// <returns>Expert System result code</returns>
  ExpertResult Calculate(int taskId, int objTypeID, int attrTypeID, long objId, out object Value);

  /// <summary>Calculate attribute value depending on the context</summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="objTypeID">Тип объекта - образует пару с типом атрибута</param>
  /// <param name="attrTypeID">Attr Type ID</param>
  /// <param name="objId">Идентификатор объекта, для которого проводится расчет</param>
  /// <param name="moreIDs">Дополнительные объекты, чтобы не искать их по правилам поиска</param>
  /// <param name="Value">Result of the calculation</param>
  /// <returns>Expert System result code</returns>
  ExpertResult Calculate(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    long[] moreIDs,
    out object Value);

  ExpertResult GetOrCalc(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    bool DisableTrace,
    out object Value);

  /// <summary>Calculate simple formula (known by formId)</summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="formId">Formula ID</param>
  /// <param name="objId"></param>
  /// <param name="Value">Result value</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcFormula(int taskId, long formId, long objId, out object Value);

  /// <summary>Calculate simple formula</summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="tf">TempFormula to calculate</param>
  /// <param name="objId">Array of Object IDs</param>
  /// <param name="Value">Result value</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcFormula(int taskId, object tf, long objId, out object Value);

  ExpertResult CalcFormula(int taskId, object tf, long[] objIds, out object Value, long relId = 0);

  /// <summary>
  /// Calculate simple formula WITH attr.value = null =&gt; "" or 0
  /// </summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="tf">TempFormula to calculate</param>
  /// <param name="objId">Array of Object IDs</param>
  /// <param name="Value">Result value</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcFormulaSimpleMode(int taskId, object tf, long objId, out object Value);

  /// <summary>
  /// Рассчитать результат формулы, хранящейся в атрибуте объекта
  /// </summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="objId">Идентификатор объекта c формулой</param>
  /// <param name="formAttrGuid">GUID атрибута с формулой в этом объекте</param>
  /// <param name="contextId">Идент. объекта контекста для формулы</param>
  /// <param name="Value">Результат</param>
  /// <returns>Код ошибки или OK если все в порядке</returns>
  ExpertResult CalcFormula(
    int taskId,
    long objId,
    Guid formAttrGuid,
    long contextId,
    out object Value);

  /// <summary>Расчет таблиц</summary>
  /// <param name="taskId">идентификатор задачи</param>
  /// <param name="tableId">идентификатор таблицы</param>
  /// <param name="objId">иденитификатор объекта</param>
  /// <param name="Values">Список значений</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcTable(int taskId, long tableId, long objId, out object[] Values);

  /// <summary>Расчет таблиц</summary>
  /// <param name="taskId">идентификатор задачи</param>
  /// <param name="tableCollection">коллекция таблиц</param>
  /// <param name="objId">иденитификатор объекта</param>
  /// <param name="Values">Список значений</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcTable(int taskId, object tableCollection, long objId, out object[] Values);

  /// <summary>Calculate simple condition (known by condId)</summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="condId">Condition ID</param>
  /// <param name="objId"></param>
  /// <param name="Value">Result value</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcCondition(int taskId, long condId, long objId, out bool Value);

  /// <summary>
  /// Рассчитать условие, лежащее в атрибуте attrId объекта objId, но с контекстом contextId
  /// </summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="attrId">Attribute ID</param>
  /// <param name="contextId">context ID</param>
  /// <param name="Value">Result value</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcCondition(int taskId, long objId, int attrId, long contextId, out bool Value);

  /// <summary>
  /// Рассчитать условие, лежащее в атрибуте attrId объекта objId, но с контекстом contextId
  /// </summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="attrId">Attribute ID</param>
  /// <param name="contextIds">Array of Context IDs</param>
  /// <param name="Value">Result value</param>
  /// <returns>Expert System result code</returns>
  ExpertResult CalcCondition(
    int taskId,
    long objId,
    int attrId,
    long[] contextIds,
    out bool Value);

  /// <summary>
  /// Получить список атрибутов, которых не хватило при расчете
  /// </summary>
  /// <param name="taskId">ИД задачи ЭС</param>
  /// <returns>Список CalcAttrPair, определяющих недостающие атрибуты</returns>
  ArrayList GetNeededAttrs(int taskId);

  /// <summary>Тест внутреннего расчета</summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="objTypeID">Идентификатор типа объекта</param>
  /// <param name="attrTypeID">Идентификатор типа атрибута</param>
  /// <param name="objId">Идентификатор объекта</param>
  /// <param name="Value">Результат расчета</param>
  /// <returns>Код ЭС</returns>
  ExpertResult InnerCalculate(
    int taskId,
    int objTypeID,
    int attrTypeID,
    long objId,
    out object Value);

  /// <summary>
  /// This should be called after creating/changing of any expert object that MIGHT return
  /// some attrubutes as results
  /// </summary>
  /// <param name="sessionGuid">Current session GUID</param>
  /// <param name="objId">The updated object</param>
  /// <param name="traceFlags">Trace flags</param>
  /// <param name="branchCond">Условие для ветви скрипто расчета атрибутов</param>
  /// <param name="traceInfo">Resulting trace info - should be presented to user</param>
  /// <param name="folderGuid">Guid папки ЭС для нового объекта</param>
  /// <returns>true if the object was added to some attrib rules</returns>
  bool ReflectObjUpdate(
    Guid sessionGuid,
    long objId,
    ExpertTraceFlags traceFlags,
    TempFormula branchCond,
    out byte[] traceInfo);

  /// <summary>Register new formula with the possible condition</summary>
  /// <param name="sessionGuid">Current session GUID</param>
  /// <param name="resObjTypeGuid">GUID of the result object type</param>
  /// <param name="resAttrTypeGuid">GUID of the result attribute type</param>
  /// <param name="tf">The formula itself (TempFormula)</param>
  /// <param name="cond">Condition for the formula (TempFormula)</param>
  /// <param name="folderGuid">Guid папки ЭС для новой формулы</param>
  /// <returns>True if the object was added to some attrib rules</returns>
  bool CreateExpertFormula(
    Guid sessionGuid,
    string resObjTypeGuid,
    string resAttrTypeGuid,
    object tf,
    object cond);

  /// <summary>
  /// Run the recalc script after changing attributes objTypeID and attrTypeID
  /// </summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="attrTypeID">attribute type ID</param>
  /// <param name="objId">Object ID</param>
  /// <returns></returns>
  bool RecalcForAttr(int taskId, long objId, int attrTypeID, long relId = -1);

  /// <summary>
  /// Присвоить VersionRuleOwnerId (не должно использоваться!)
  /// </summary>
  /// <param name="taskId">Expert System Task Id</param>
  /// <param name="versionRule">version rule (owner Id)</param>
  void SetVersionRuleOwnerId(int taskId, string versionRule);

  /// <summary>
  /// Получить VersionRuleOwnerId (не должно использоваться)
  /// </summary>
  /// <param name="taskId">Expert System Task Id</param>
  /// <returns>version rule (owner Id)</returns>
  string GetVersionRuleOwnerId(int taskId);

  /// <summary>Присвоить VersionsRule</summary>
  /// <param name="taskId">Expert System Task Id</param>
  /// <param name="rule">version rule</param>
  void SetVersionRule(int taskId, VersionsRule rule);

  /// <summary>Получить VersionsRule</summary>
  /// <param name="taskId">Expert System Task Id</param>
  /// <returns>version rule</returns>
  VersionsRule GetVersionRule(int taskId);

  /// <summary>Установить контекст редактирования для задачи</summary>
  /// <param name="taskId">ИД задачи ЭС</param>
  /// <param name="editingContextId">ИД контекста редактирования</param>
  void SetEditingContext(int taskId, long editingContextId);

  /// <summary>
  /// Узнать, какой контекст редактирования установлен для задачи ЭС.
  /// </summary>
  /// <param name="taskId">ИД задачи ЭС</param>
  /// <returns>ИД контекста редактирования для этой задачи, или 0</returns>
  long GetEditingContext(int taskId);

  /// <summary>Set filtration rule for the current window</summary>
  /// <param name="taskId">Expert System Task Id</param>
  /// <param name="filtration">Serialized Hybrid Dictionary</param>
  void SetWindowFiltration(int taskId, byte[] filtration);

  /// <summary>Create new Expert Formula</summary>
  /// <param name="sessionGuid"></param>
  /// <param name="ap">AttribPair</param>
  /// <param name="resAttrGuid"></param>
  /// <param name="resObjTypeGuid"></param>
  /// <param name="Name"></param>
  /// <param name="cond">TempFormula</param>
  /// <param name="formTF">TempFormula</param>
  /// <returns></returns>
  IDBObject CreateExpertFormula(
    Guid sessionGuid,
    object ap,
    string resAttrGuid,
    string resObjTypeGuid,
    string Name,
    object cond,
    object formTF);

  List<string> GetUserReport(int taskId);

  List<long> GetAttrRulesForObject(Guid sessionGuid, long expertObjId);

  /// <summary>
  /// Проверить объекты экспертной системы на ошибочное содержимое атрибутов
  /// </summary>
  /// <returns></returns>
  List<string> CheckExpertObjects();

  void ClearCaches();

  bool FillExpObjInfo(ref ExpObjInfo eoi, Guid sessionGuid);

  DataTable GetAttrTypesTable(SortedDictionary<int, GuidAndName> attrTypeIds, out DataTable Groups);

  DataTable GetObjTypesTable(SortedDictionary<int, GuidAndName> objTypeIds);

  void ShowExpertInfo();

  /// <summary>
  /// Присвоить объекту документа все атрибуты, которые были в группе
  /// </summary>
  /// <param name="taskId">ИД задачи</param>
  /// <param name="docObjectId">ИД объекта документа</param>
  void SetDocAttributes(int taskId, long docObjectId);

  /// <summary>Задать значение параметра</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <param name="Key">Название параметра (ExpertTaskParams)</param>
  /// <param name="Value">Значение параметра</param>
  void SetTaskParm(int taskId, string Key, object Value);

  /// <summary>Получить значение параметра</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <param name="Key">Название параметра (ExpertTaskParams)</param>
  /// <returns>Значение параметра</returns>
  object GetTaskParm(int taskId, string Key);

  /// <summary>Задать значение нескольких параметров сразу</summary>
  /// <param name="taskId">ИД задачи</param>
  /// <param name="parms">Словарь с параметрами</param>
  void SetTaskParms(int taskId, Dictionary<string, object> parms);

  /// <summary>
  /// Получить список условий от папок на объект ЭС в строковом виде (для показа)
  /// </summary>
  /// <param name="esObjectId">ИД объекта экспертной системы (напр. формулы), для которого надо получить условия</param>
  /// <returns>Строка со списком условий (через символы перевода строки) или пустую, если условий нет</returns>
  string GetFolderConds(long esObjectId);

  /// <summary>Запустить командный скрипт</summary>
  /// <param name="taskId">ИД задачи ЭС</param>
  /// <param name="docScriptID">ИД командного скрипта</param>
  /// <param name="context">Контекст (список ИД объектов)</param>
  /// <returns>Код результата</returns>
  ExpertResult RunCommandScript(int taskId, long docScriptID, long[] context);

  /// <summary>
  /// Generate document by document script based on the passed object(s)
  /// </summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="docScriptID">ObjectID of document generaion script</param>
  /// <param name="context">Array of Object IDs</param>
  /// <param name="zippedDoc">Resulting ImDocument in zipped XML or null</param>
  /// <returns>Expert System result code</returns>
  ExpertResult GenerateDocument(
    int taskId,
    long docScriptID,
    long[] context,
    out byte[] zippedDoc);

  /// <summary>
  /// Generate document by document script based on the passed object(s)
  /// </summary>
  /// <param name="taskId">Expert System task ID</param>
  /// <param name="docScriptID">ObjectID of document generaion script</param>
  /// <param name="context">Array of Object IDs</param>
  /// <param name="docObjId">Doc object ID to store the document</param>
  /// <returns>Expert System result code</returns>
  ExpertResult GenerateDocument(int taskId, long docScriptID, long[] context, long docObjId);

  /// <summary>Генерировать НОВЫЙ комплект документов</summary>
  /// <param name="taskId">Идентификатор задачи (получать через StartTask)</param>
  /// <param name="compScriptID">Идентификатор скрипта генерации комплекта</param>
  /// <param name="contextID">Идентификатор ТП (или другого объекта), на который создается комплект</param>
  /// <param name="changed"></param>
  /// <param name="dopComplects">True, если нужно сгенерировать дополнительные комплекты</param>
  /// <returns>Код ошибки (или ОК если все в порядке)</returns>
  ExpertResult GenerateComplect(
    int taskId,
    long compScriptID,
    long contextID,
    out List<ChangeInfo> changed,
    bool dopComplects = false);

  /// <summary>Генерировать НОВЫЙ комплект документов</summary>
  /// <param name="cgp">Параметры для генерации комплекта</param>
  /// <param name="changed"></param>
  /// <returns>Код ошибки (или ОК если все в порядке)</returns>
  ExpertResult GenerateComplect(CompGenParms cgp, out List<ChangeInfo> changed);

  /// <summary>Создать ВЕРСИЮ комплекта документов</summary>
  /// <param name="taskId">Идентификатор задачи (получать через StartTask)</param>
  /// <param name="compScriptID">Идентификатор скрипта генерации комплекта</param>
  /// <param name="contextID">Идентификатор ТП (или другого объекта), на который создается комплект</param>
  /// <param name="complectID">Идентификатор предыдущего комплекта</param>
  /// <param name="changed"></param>
  /// <param name="dopComplects">True, если нужно сгенерировать дополнительные комплекты</param>
  /// <returns>Код ошибки (или ОК если все в порядке)</returns>
  ExpertResult CreateComplectVersion(
    int taskId,
    long compScriptID,
    long contextID,
    long complectID,
    out List<ChangeInfo> changed,
    bool dopComplects = false);

  /// <summary>Создать ВЕРСИЮ комплекта документов</summary>
  /// <param name="cgp">Параметры для генерации комплекта</param>
  /// <param name="changed"></param>
  /// <returns>Код ошибки (или ОК если все в порядке)</returns>
  ExpertResult CreateComplectVersion(CompGenParms cgp, out List<ChangeInfo> changed);

  /// <summary>ОБНОВИТЬ комплект документов</summary>
  /// <param name="taskId">Идентификатор задачи (получать через StartTask)</param>
  /// <param name="compScriptID">Идентификатор скрипта генерации комплекта</param>
  /// <param name="contextID">Идентификатор ТП (или другого объекта), на который создается комплект</param>
  /// <param name="complectID">Идентификатор предыдущего комплекта</param>
  /// <param name="changed"></param>
  /// <param name="dopComplects">True, если нужно сгенерировать дополнительные комплекты</param>
  /// <returns>Код ошибки (или ОК если все в порядке)</returns>
  ExpertResult RefreshComplect(
    int taskId,
    long compScriptID,
    long contextID,
    long complectID,
    out List<ChangeInfo> changed,
    bool dopComplects = false);

  /// <summary>ОБНОВИТЬ комплект документов</summary>
  /// <param name="cgp">Параметры для генерации комплекта</param>
  /// <param name="changed"></param>
  /// <returns>Код ошибки (или ОК если все в порядке)</returns>
  ExpertResult RefreshComplect(CompGenParms cgp, out List<ChangeInfo> changed);

  FuncData GetFuncData(int index);

  List<int> GetFuncIds();

  List<string> GetFuncNames();

  List<string> GetComparerNames();

  List<string> GetProcNames();

  void RegUserFunction(
    int Id,
    string Name,
    DataType[] parmTypes,
    DataType result,
    string description,
    FuncHandler handler);

  void RegComparer(string Name, CompareFuncHandler cfh);

  void RegUserProc(string Name, ScriptProcHandler handler);

  void UnregUserFunction(string Name);

  void UnregComparer(string Name);

  void UnregUserProc(string Name);

  object InvokeFunc(string funcName, ArrayList parms);

  object InvokeFunc(int id, ArrayList parms);

  string GetDocName(int taskId, long scriptId, long contId);

  List<string> FixIdentsComplete();

  List<string> FixIdentsOne(long objId);

  List<string> CreateGUIDs();

  List<string> CreateGIUDsOne(long objId);

  /// <summary>
  /// Получить массив информации о документах, которые сейчас генерятся в комплекте
  /// </summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <returns>Массив информации о документах</returns>
  DocRecord[] GetDocArray(int taskId);

  /// <summary>
  /// Получить информацию о документе по номеру (обычно запускается именно это, пока документ не будет сгенерирован)
  /// </summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="Num">Номер документа</param>
  /// <returns>Информацию о документе</returns>
  DocRecord GetDocRecord(int taskId, int Num);

  /// <summary>Получить сгенерированный документ (ЗАПАКОВАННЫЙ)</summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="Num">Номер документа</param>
  /// <returns>Запакованный документ</returns>
  byte[] GetDocument(int taskId, int Num);

  /// <summary>
  /// Заменить документ (используется для сохранения разбитого документа)
  /// ВНИМАНИЕ! Именно этот метод обычно создает объект документа (чтобы все записать сразу)
  /// </summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="sessionGuid"></param>
  /// <param name="Num">Номер документа</param>
  /// <param name="doc">Запакованный документ</param>
  /// <param name="pageCount"></param>
  void SetDocument(int taskId, Guid sessionGuid, int Num, byte[] doc, int pageCount);

  /// <summary>Получить отладочную информацию от генерации документа</summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="Num">Номер документа</param>
  /// <returns>Запакованный XML с отладочной информацией</returns>
  byte[] GetTraceInfo(int taskId, int Num);

  /// <summary>Подтвердить завершение разбиения документа</summary>
  /// <param name="taskId">Идентификатор задачи</param>
  /// <param name="Num">Номер документа</param>
  void ConfirmDocAligned(int taskId, int Num);
}
