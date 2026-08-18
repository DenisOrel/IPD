// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.IProductionListReportService
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>
/// сервис для взаимодействия закладки отчета по составу эс пв со скриптом
/// </summary>
public interface IProductionListReportService
{
  /// <summary>Добавить источник данных для отчета на ПВ</summary>
  /// <param name="objectID">идентификатор версии объекта ПВ</param>
  /// <param name="dataSource">источник данных</param>
  void AddReportDataSource(long objectID, object dataSource);

  /// <summary>получить источник данных для отчета на ПВ</summary>
  /// <param name="objectID">идентификатор версии объекта ПВ</param>
  /// <returns>источник данных</returns>
  object GetReportDataSource(long objectID);
}
