// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ICustomReportScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Document;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>
/// Интерфейс, который должен реализовыватся пользовательскими сценариями генерации документов и ведомостей
/// </summary>
public interface ICustomReportScenario
{
  /// <summary>Метод выполнения сценария</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="document">Формируемый документ</param>
  /// <param name="objectIDs">Объекты для которых формируется документ</param>
  /// <returns></returns>
  bool Execute(IUserSession session, ImDocumentData document, long[] objectIDs);
}
