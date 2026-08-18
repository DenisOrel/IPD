// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportBackgroundTask
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Client;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>
/// Базовый интерфейс фоновой задачи генерации комплекта документов
/// </summary>
public interface IReportBackgroundTask : IBackgroundTask
{
  /// <summary>Опции  задачи</summary>
  ReportTaskOptions Options { get; set; }

  /// <summary>Параметры задачи генерации</summary>
  IReportTaskParams Params { get; }

  /// <summary>Параметры задачи генерации комплекта документов</summary>
  IReportsBaseTask Task { get; }

  /// <summary>Запуск фоновой задачи</summary>
  void Execute();
}
