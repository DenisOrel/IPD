// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Reports.IColumnHandler
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

#nullable disable
namespace Intermech.Document.Client.Reports;

/// <summary>
/// Обработчик данных для специфических колонок в табличном отчете
/// </summary>
internal interface IColumnHandler
{
  /// <summary>Зарпашиваются данные для вставки в отчет</summary>
  /// <param name="value">Значение, которое вернул запрос</param>
  /// <returns></returns>
  object GetValue(ReportItemInfo itemInfo, object value);
}
