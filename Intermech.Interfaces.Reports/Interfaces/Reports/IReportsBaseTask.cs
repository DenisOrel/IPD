// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportsBaseTask
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Expert;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// Базовый интерфейс задачи генератора комплекта документов
/// </summary>
public interface IReportsBaseTask
{
  /// <summary>Выполнение задачи</summary>
  /// <param name="changeLog">Список изменений</param>
  /// <returns>Код выполнения</returns>
  ExpertResult Execute(out List<ChangeInfo> changeLog);

  /// <summary>Выполнение задачи</summary>
  /// <param name="traceEnable">Флаг трассировки</param>
  /// <param name="logEnable"></param>
  /// <param name="traceFlags">Режим трассировки</param>
  /// <param name="changeLog">Список изменений</param>
  /// <param name="traceInfo">Запакованный XML с отладочной информацией</param>
  /// <param name="reportInfo">Доп. информация о ходе выполнения задачи</param>
  /// <returns>Код выполнения</returns>
  ExpertResult Execute(
    bool traceEnable,
    bool logEnable,
    ExpertTraceFlags traceFlags,
    out List<ChangeInfo> changeLog,
    out byte[] traceInfo,
    out string[] reportInfo);

  /// <summary>Выполнение задачи</summary>
  /// <param name="traceEnable">Флаг трассировки</param>
  /// <param name="logEnable"></param>
  /// <param name="traceFlags">Режим трассировки</param>
  /// <param name="reportTraceInfo">Информация о трассировке задачи</param>
  /// <returns>Код выполнения</returns>
  ExpertResult Execute(
    bool traceEnable,
    bool logEnable,
    ExpertTraceFlags traceFlags,
    out ReportTraceInfo reportTraceInfo);

  /// <summary>Параметры задачи генерации</summary>
  IReportTaskParams Params { get; }

  /// <summary>Состояние задачи генерации</summary>
  ReportTaskState State { get; }

  DocRecord[] DocList { get; }

  /// <summary>
  /// Получение списка генерируемых документов / комплектов от ЭС
  /// </summary>
  event ReportDocEventHandler AfterGenerateDocList;

  /// <summary>Генерация документа</summary>
  event ReportDocEventHandler GenerateDocument;

  /// <summary>Разбиение документа</summary>
  event ReportDocEventHandler AfterRealignDocument;

  /// <summary>Вывод сообщений об ошибках</summary>
  event ValueObjChangedHandler ErrorOutput;
}
