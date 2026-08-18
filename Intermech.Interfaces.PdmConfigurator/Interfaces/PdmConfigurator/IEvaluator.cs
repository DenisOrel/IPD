// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IEvaluator
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Интерфейс позволяет вызвать вычисление значения критерия или коллекции критериев
/// </summary>
public interface IEvaluator
{
  /// <summary>
  /// Выполнить вычисление значения элемента согласно указанному контексту конфигуратора составов IPS
  /// </summary>
  /// <param name="context">Контекст конфигуратора составов IPS</param>
  /// <returns>Результат вычисления значения элемента, исключение, если значение опции/критерия не найдено в контексте,
  /// либо произошла другая критическая ошибка</returns>
  PdmConfiguratorResult Evalute(PdmConfiguratorContext context);

  /// <summary>
  /// Логическая функция для объединения данного элемента со следующим элементом
  /// </summary>
  LogicalFunction Function { get; set; }

  bool Not { get; set; }

  /// <summary>Результат последнего вычисления</summary>
  TraceEntry EvaluateTrace { get; }
}
