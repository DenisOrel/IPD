// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadSelectionSetFilter
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Объект фильтра для выполнения запросов по содержимому документа CAD-системы.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
/// <remarks>Реализация является immutable.</remarks>
public sealed class CadSelectionSetFilter
{
  private readonly short[] ids;
  private readonly object[] values;

  /// <summary>Создает объект.</summary>
  /// <param name="ids">Массив идентификаторов атрибутов/логических операторов</param>
  /// <param name="values">Массив идентификатор значений</param>
  internal CadSelectionSetFilter(short[] ids, object[] values)
  {
    this.ids = ids;
    this.values = values;
  }

  /// <summary>
  /// Возвращает массив идентификаторов атрибутов/логических операторов.
  /// </summary>
  public short[] Ids
  {
    [DebuggerStepThrough] get => this.ids;
  }

  /// <summary>Возвращает массив идентификатор значений.</summary>
  public object[] Values
  {
    [DebuggerStepThrough] get => this.values;
  }
}
