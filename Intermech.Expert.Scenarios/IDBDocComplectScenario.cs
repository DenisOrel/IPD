// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Scenarios.IDBDocComplectScenario
// Assembly: Intermech.Expert.Scenarios, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 67A596D2-F145-4D6C-A4AA-0257621BF410
// Assembly location: D:\IPS\Client\Intermech.Expert.Scenarios.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Scenarios.xml

#nullable disable
namespace Intermech.Expert.Scenarios;

/// <summary>Сценарий генерации комплекта документов</summary>
public interface IDBDocComplectScenario : IDBScenario
{
  /// <summary>Шаблон  комплекта документов</summary>
  long ComplectTemplateID { get; }
}
