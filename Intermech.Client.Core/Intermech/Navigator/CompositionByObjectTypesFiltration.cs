
// Type: Intermech.Navigator.CompositionByObjectTypesFiltration
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator;

/// <summary>
/// Если узел "Навигатора" поддерживает данный интерфейс, а также этот узел
/// является корневым узлом в окне "Навигатора", то окно отобразит элементы
/// управления фильтрацией составов по типам объектов и связей
/// </summary>
[Serializable]
public class CompositionByObjectTypesFiltration : ICompositionByObjectTypesFiltration
{
  /// <summary>Коллекция фильтров</summary>
  protected ICompositionByObjectTypesFilters _filters;
  /// <summary>
  /// Guid активного фильтра по родительским и дочерним типам объектов. Если
  /// значение равно Guid.Empty, то фильтрация отключена
  /// </summary>
  protected Guid _activeFilterGuid = Guid.Empty;

  /// <summary>Создать экземпляр класса</summary>
  public CompositionByObjectTypesFiltration()
  {
  }

  /// <summary>
  /// Создать экземпляр класса, связать его с указанной коллекцией фильтров
  /// </summary>
  /// <param name="filters">Коллекция фильтров</param>
  public CompositionByObjectTypesFiltration(ICompositionByObjectTypesFilters filters)
  {
    this.Filters = filters;
  }

  /// <summary>
  /// Создать экземпляр класса, связать его с указанной коллекцией фильтров, задать активный фильтр
  /// </summary>
  /// <param name="filters">Коллекция фильтров</param>
  /// <param name="activeFilterGuid">Guid активного фильтра</param>
  public CompositionByObjectTypesFiltration(
    ICompositionByObjectTypesFilters filters,
    Guid activeFilterGuid)
  {
    this.Filters = filters;
    this.ActiveFilterGuid = activeFilterGuid;
  }

  /// <summary>Коллекция фильтров</summary>
  public ICompositionByObjectTypesFilters Filters
  {
    get => this._filters;
    set => this._filters = value;
  }

  /// <summary>
  /// Guid активного фильтра по родительским и дочерним типам объектов. Если
  /// значение равно Guid.Empty, то фильтрация отключена
  /// </summary>
  public Guid ActiveFilterGuid
  {
    get => this._activeFilterGuid;
    set
    {
      if (this._filters == null)
        this._activeFilterGuid = Guid.Empty;
      else if (this._filters[value] == null)
        this._activeFilterGuid = Guid.Empty;
      else
        this._activeFilterGuid = value;
    }
  }
}
