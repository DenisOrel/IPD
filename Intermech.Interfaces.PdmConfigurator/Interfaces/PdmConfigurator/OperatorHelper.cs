// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.OperatorHelper
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Вспомогательный статический класс для преобразования Operator в строки и обратно
/// </summary>
public static class OperatorHelper
{
  /// <summary>Преобразовать строку в значение типа Operator</summary>
  /// <param name="value">Строка</param>
  /// <returns>Значение типа Operator</returns>
  public static Operator FromString(string value)
  {
    switch (value)
    {
      case "a":
        return Operator.Less;
      case "b":
        return Operator.LessEquals;
      case "c":
        return Operator.Equals;
      case "d":
        return Operator.GreaterEquals;
      case "e":
        return Operator.Greater;
      default:
        return Operator.NotEquals;
    }
  }

  /// <summary>Преобразовать значнение типа Operator в строку</summary>
  /// <param name="value">Значение типа Operator</param>
  /// <returns>Строковое представление</returns>
  public static string ToString(Operator value)
  {
    switch (value)
    {
      case Operator.Less:
        return "a";
      case Operator.LessEquals:
        return "b";
      case Operator.Equals:
        return "c";
      case Operator.GreaterEquals:
        return "d";
      case Operator.Greater:
        return "e";
      default:
        return "f";
    }
  }
}
