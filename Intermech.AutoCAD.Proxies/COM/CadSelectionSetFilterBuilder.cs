// Decompiled with JetBrains decompiler
// Type: Intermech.AutoCAD.Proxies.COM.CadSelectionSetFilterBuilder
// Assembly: Intermech.AutoCAD.Proxies, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 36C988AC-B8D8-43EC-AA22-521DF7E8A085
// Assembly location: D:\IPS\Client\Intermech.AutoCAD.Proxies.dll
// XML documentation location: D:\IPS\Client\Intermech.AutoCAD.Proxies.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace Intermech.AutoCAD.Proxies.COM;

/// <summary>
/// Построитель запросов по содержимому документа CAD-системы.
/// Он позволяет абстрагироваться от "жутких" особенностей реализации этого механизма в CAD-системе.
/// Реализация следует общему поведению AutoCAD, BricsCAD, nanoCAD.
/// </summary>
/// <remarks>Реализация не является thread safe.</remarks>
public class CadSelectionSetFilterBuilder
{
  private HashSet<DxfEntityType> entityTypeFilter;

  /// <summary>Создает объект.</summary>
  public CadSelectionSetFilterBuilder() => this.entityTypeFilter = new HashSet<DxfEntityType>();

  /// <summary>Очищает построитель.</summary>
  public void Clear() => this.entityTypeFilter.Clear();

  /// <summary>Возвращает фильтр по типам элементов документов.</summary>
  public ICollection<DxfEntityType> EntityTypeFilter
  {
    [DebuggerStepThrough] get => (ICollection<DxfEntityType>) this.entityTypeFilter;
  }

  /// <summary>
  /// Возвращает построенный фильтр, пригодный для использования в API CAD-системы.
  /// </summary>
  /// <returns>Объект фильтра</returns>
  public CadSelectionSetFilter ToFilter()
  {
    List<(short, object)> filterItems = new List<(short, object)>();
    if (this.entityTypeFilter.Count != 0)
      this.ApplyEntityTypeFilter(filterItems);
    return this.ToFilter(filterItems);
  }

  private void ApplyEntityTypeFilter(List<(short, object)> filterItems)
  {
    IEnumerable<string> values = this.entityTypeFilter.Select<DxfEntityType, string>((Func<DxfEntityType, string>) (item => this.ConvertEntityTypeToFilterString(item)));
    short num = 0;
    string str = string.Join(",", values);
    filterItems.Add((num, (object) str));
  }

  private CadSelectionSetFilter ToFilter(List<(short, object)> filterItems)
  {
    short[] ids = new short[filterItems.Count];
    object[] values = new object[filterItems.Count];
    for (int index = 0; index < filterItems.Count; ++index)
    {
      ids[index] = filterItems[index].Item1;
      values[index] = filterItems[index].Item2;
    }
    return new CadSelectionSetFilter(ids, values);
  }

  /// <summary>
  /// Преобразует DXF entity type во внутреннее строковое представление,
  /// принятое в CAD-системе.
  /// </summary>
  /// <param name="entityType">DXF entity type</param>
  /// <returns>Строковое представление, принятое в CAD-системе</returns>
  protected virtual string ConvertEntityTypeToFilterString(DxfEntityType entityType)
  {
    return entityType.ToString();
  }
}
