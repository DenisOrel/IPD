// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.IDBScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>Сценарий в контексте объекта базы данных</summary>
public interface IDBScenario
{
  /// <summary>Идентификатор версии объекта</summary>
  long ScenarioID { get; }

  /// <summary>Текст сценария</summary>
  string Code { get; }

  /// <summary>Язык сценария</summary>
  ScenarioLangs Language { get; }

  /// <summary>Сторона выполнения</summary>
  ExecSides ExecSide { get; }

  /// <summary>Метод выполнения сценария</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="document">Формируемый документ</param>
  /// <param name="objectIDs">Объекты для которых формируется документ</param>
  /// <returns></returns>
  bool Execute(object session, long[] objectIDs);
}
