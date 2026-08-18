// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.LogicalFunctionHelper
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Вспомогательный статический класс для преобразования LogicalFunction в строки и обратно
/// </summary>
public static class LogicalFunctionHelper
{
  /// <summary>Преобразовать строку в значение типа LogicalFunction</summary>
  /// <param name="value">Строка</param>
  /// <returns>Значение типа LogicalFunction</returns>
  public static LogicalFunction FromString(string value)
  {
    return value == "a" ? LogicalFunction.And : LogicalFunction.Or;
  }

  /// <summary>Преобразовать значнение типа LogicalFunction в строку</summary>
  /// <param name="value">Значение типа LogicalFunction</param>
  /// <returns>Строковое представление</returns>
  public static string ToString(LogicalFunction value) => value == LogicalFunction.And ? "a" : "b";
}
