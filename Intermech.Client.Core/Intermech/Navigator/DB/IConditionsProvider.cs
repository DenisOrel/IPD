
// Type: Intermech.Navigator.DB.IConditionsProvider
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;


namespace Intermech.Navigator.DB;

/// <summary>
/// Провайдер условий запроса к базе данных для классов, чей набор условий
/// изменяется динамически.
/// </summary>
public interface IConditionsProvider
{
  /// <summary>
  /// Возвращает набор условий запроса к базе данных, актуальный на момент
  /// вызова метода.
  /// </summary>
  /// <returns>Массив условий запроса к базе данных.</returns>
  ConditionStructure[] GetConditions();

  /// <summary>
  /// Возвращает признак того, что набор условий изменился с момента последнего
  /// вызова <see cref="M:Intermech.Navigator.DB.IConditionsProvider.GetConditions" />. До первого вызова указанного метода
  /// значение этого свойства = true.
  /// </summary>
  /// <returns></returns>
  bool ConditionsChanged { get; }
}
