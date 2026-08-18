// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.TechDocumentFilter
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>
/// Сервис, позволяющий фильтровать документы на закладке ОТД
/// </summary>
public class TechDocumentFilter : IConditionsProvider
{
  /// <summary>Дополнительные условия</summary>
  private List<ConditionStructure> additionalConditions = new List<ConditionStructure>();

  /// <summary>Добавить условия</summary>
  /// <param name="addCS"></param>
  public void SetConditions(List<ConditionStructure> addCS)
  {
    this.additionalConditions = new List<ConditionStructure>((IEnumerable<ConditionStructure>) addCS);
  }

  /// <summary>
  /// Возвращает набор условий запроса к базе данных, актуальный на момент
  /// вызова метода.
  /// </summary>
  /// <returns>Массив условий запроса к базе данных.</returns>
  public ConditionStructure[] GetConditions() => this.additionalConditions.ToArray();

  /// <summary>
  /// Возвращает признак того, что набор условий изменился с момента последнего
  /// вызова <see cref="M:Intermech.Archives.Copies.TechDocumentFilter.GetConditions" />. До первого вызова указанного метода
  /// значение этого свойства = true.
  /// </summary>
  /// <returns></returns>
  public bool ConditionsChanged => true;
}
