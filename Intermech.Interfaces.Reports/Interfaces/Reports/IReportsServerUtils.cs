// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Reports.IReportsServerUtils
// Assembly: Intermech.Interfaces.Reports, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3A40A7D8-A018-4590-B8F9-C63911182943
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Reports.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Reports.xml

using Intermech.Interfaces.Document;
using System;

#nullable disable
namespace Intermech.Interfaces.Reports;

/// <summary>Интерфейс для серверных утилит</summary>
public interface IReportsServerUtils
{
  /// <summary>Восстановление / генерация данных документа</summary>
  /// <remarks>Внимание! Содержимое внешних документов не обновляется!</remarks>
  /// <param name="sessionGuid">Guid пользовательской сессии</param>
  /// <param name="reportsDoc">Базовый класс для передачи документов со стороны сервера / другого приложения</param>
  /// <param name="complect">Визуальный узел документа / комплекта</param>
  bool RestoreComplectData(
    Guid sessionGuid,
    ReportsBaseDoc reportsDoc,
    out DocumentsComplect complect);
}
