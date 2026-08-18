// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.IDBReportScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>Сценарий генерации документов и ведомостей</summary>
public interface IDBReportScenario : IDBScenario
{
  /// <summary>Шаблон  генерируемого документа</summary>
  long DocTemplateID { get; }

  /// <summary>Тип генерируемого документа</summary>
  int CreateObjectType { get; }

  /// <summary>Тип связей для включения документа в состав</summary>
  int CompositionRelType { get; }

  /// <summary>Открыть документ после его формирования</summary>
  bool CreateInViewer { get; }

  /// <summary>
  /// Генерировать один документ для всех объектов в сценарии
  /// </summary>
  bool OneDocument { get; }

  /// <summary>Документ</summary>
  Stream Document { get; }

  /// <summary>
  /// Атрибуты, которые необходимо установить создаваемому документу
  /// </summary>
  Dictionary<Guid, string> DocumentAttributes { get; }
}
