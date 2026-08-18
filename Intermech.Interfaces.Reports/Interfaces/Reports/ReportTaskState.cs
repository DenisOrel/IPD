// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.ReportTaskState
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Статус задачи генерации комплектов документов</summary>
public enum ReportTaskState
{
  /// <summary>Не определен</summary>
  None = 0,
  /// <summary>Выполняется</summary>
  Executing = 1,
  /// <summary>Прерван</summary>
  Terminated = 10, // 0x0000000A
  /// <summary>Завершен</summary>
  Completed = 20, // 0x00000014
}
