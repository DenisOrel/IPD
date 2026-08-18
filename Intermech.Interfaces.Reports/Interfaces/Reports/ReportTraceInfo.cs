// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportTraceInfo
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Expert;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// Информация о трассировке задачи генерации комплектов документов
/// </summary>
[Serializable]
public class ReportTraceInfo
{
  /// <summary>Информация об изменениях в структуре комплекта</summary>
  protected List<ChangeInfo> _changeLog;
  /// <summary>Информация о трассировке</summary>
  protected byte[] _traceInfo;
  /// <summary>Доп. информация о ходе выполнения задачи</summary>
  protected string[] _reportInfo;

  /// <summary>Конструктор</summary>
  /// <param name="changeLog">Информация об изменениях в структуре комплекта</param>
  /// <param name="traceInfo">Информация о трассировке</param>
  /// <param name="reportInfo">Доп. информация о ходе выполнения задачи</param>
  /// <param name="objectGlobalTable">Глобальная таблица объектов</param>
  /// <param name="linkGlobalTable">Глобальная таблица связей</param>
  /// <param name="modificationLog"></param>
  public ReportTraceInfo(
    List<ChangeInfo> changeLog,
    byte[] traceInfo,
    string[] reportInfo,
    HybridTableExp objectGlobalTable = null,
    HybridTableExp linkGlobalTable = null,
    IList<CategoryValue> modificationLog = null)
  {
    this._changeLog = changeLog;
    this._traceInfo = traceInfo;
    this._reportInfo = reportInfo;
    this.ObjectGlobalTable = objectGlobalTable;
    this.LinkGlobalTable = linkGlobalTable;
    this.ModificationLog = modificationLog;
  }

  /// <summary>Информация об изменениях в структуре комплекта</summary>
  public List<ChangeInfo> ChangeLog => this._changeLog;

  /// <summary>Информация о трассировке</summary>
  /// <remarks>Запакованный XML с отладочной информацией</remarks>
  public byte[] TraceInfo => this._traceInfo;

  /// <summary>Доп. информация о ходе выполнения задачи</summary>
  public string[] ReportInfo => this._reportInfo;

  /// <summary>Глобальная таблица объектов</summary>
  public HybridTableExp ObjectGlobalTable { get; }

  /// <summary>Глобальная таблица связей</summary>
  public HybridTableExp LinkGlobalTable { get; }

  /// <summary>
  /// 
  /// </summary>
  public IList<CategoryValue> ModificationLog { get; }
}
