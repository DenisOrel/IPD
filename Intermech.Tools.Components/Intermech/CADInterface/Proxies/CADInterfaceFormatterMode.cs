// Decompiled with JetBrains decompiler
// Type: Intermech.CADInterface.Proxies.CADInterfaceFormatterMode
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.CADInterface.Proxies;

/// <summary>
/// Описывает режимы работы форматировщика параметров в документах CAD-интерфейса.
/// </summary>
public enum CADInterfaceFormatterMode
{
  /// <summary>
  /// Традиционный, исторически сложившийся режима работы форматировщика.
  /// Перед чтением значений параметров форматировщик проверяет существование параметра с помощью
  /// метода CAD-интерфейса IParametersContainer.GetParameterNames()
  /// </summary>
  Default,
  /// <summary>
  /// Оптимизированный режим работы форматировщика.
  /// Перед чтением значений параметров форматировщик не проверяет существование параметра,
  /// вместо этого отсутствие параметров определяется по null в массиве значений параметров
  /// </summary>
  UncheckedRead,
}
