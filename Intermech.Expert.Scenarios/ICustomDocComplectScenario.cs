// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.ICustomDocComplectScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>
/// Интерфейс, который должен реализовыватся пользовательскими сценариями генерации комплекта документов
/// </summary>
public interface ICustomDocComplectScenario
{
  /// <summary>Метод выполнения сценария</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="complectTemplateID">Идентификатор шаблона генерации комплекта документов</param>
  /// <param name="objectIDs">Объекты для которых формируется документ</param>
  /// <returns></returns>
  bool Execute(IUserSession session, long complectTemplateID, long[] objectIDs);
}
