// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportsService
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Интерфейс управления комплектами</summary>
public interface IReportsService
{
  /// <summary>Получить интерфейс задачи генерации комплектов</summary>
  /// <param name="mode">Режим генерации</param>
  /// <param name="taskParams">Параметры задачи</param>
  /// <returns></returns>
  IReportsBaseTask GetReportTask(ReportMode mode, IReportTaskParams taskParams);

  /// <summary>
  /// Получить интерфейс задачи генерации комплектов в фоновом потоке
  /// </summary>
  /// <param name="mode">Режим генерации</param>
  /// <param name="taskParams">Параметры задачи</param>
  /// <returns></returns>
  IReportBackgroundTask GetReportBackgroundTask(ReportMode mode, IReportTaskParams taskParams);
}
