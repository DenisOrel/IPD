// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.IXmlExchangeExportTask
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Interfaces.Briefcase;
using System;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Задача выгрузки данных в XML</summary>
public interface IXmlExchangeExportTask : IXmlExchangeTask, IDisposable
{
  /// <summary>Экспорт данных</summary>
  /// <param name="exportData">Данные для экспорта</param>
  /// <param name="exportParams">Параметры экспорта</param>
  /// <param name="errorMsg">Сообщение об ошибке</param>
  /// <returns></returns>
  bool ExportData(ExportAttribute[] exportData, object[] exportParams, out string errorMsg);

  /// <summary>Получение файлов экспорта</summary>
  /// <param name="exportDataFiles"></param>
  /// <returns></returns>
  bool GetExportFiles(out string[] exportDataFiles);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="exportDataFile"></param>
  /// <returns></returns>
  IBlobReader GetExportData(string exportDataFile);
}
