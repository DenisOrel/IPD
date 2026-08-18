// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportUtils
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Interfaces.Reports;

public interface IReportUtils
{
  /// <summary>Восстановление / генерация данных документа</summary>
  /// <param name="reportsDoc">Базовый класс для передачи документов со стороны сервера / другого приложения</param>
  /// <param name="complect">Визуальный узел документа / комплекта</param>
  bool RestoreComplectData(ReportsBaseDoc reportsDoc, out DocumentsComplect complect);
}
