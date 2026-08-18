// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ComponentsListFilter
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Фильтр компонентов для типа состава (СП, ПЭ...)</summary>
public abstract class ComponentsListFilter : IComponentsListFilter
{
  /// <summary>Допустимые варианты</summary>
  protected List<CompositionVariants> enabledVariants;
  /// <summary>Настройки интегратора</summary>
  protected ECADIntegratorSettings settings;

  /// <summary>Конструктор</summary>
  /// <param name="settings">Настройки интегратора</param>
  /// <param name="type">Тип фильтра</param>
  public ComponentsListFilter(ECADIntegratorSettings settings, ComponentsListFilterType type)
  {
    switch (type)
    {
      case ComponentsListFilterType.Composition:
        this.enabledVariants = new List<CompositionVariants>((IEnumerable<CompositionVariants>) new CompositionVariants[2]
        {
          CompositionVariants.Specification,
          CompositionVariants.SpecificationAndElementsList
        });
        break;
      case ComponentsListFilterType.ElementsList:
        this.enabledVariants = new List<CompositionVariants>((IEnumerable<CompositionVariants>) new CompositionVariants[2]
        {
          CompositionVariants.ElementsList,
          CompositionVariants.SpecificationAndElementsList
        });
        break;
      case ComponentsListFilterType.CompositionAndElementsList:
        this.enabledVariants = new List<CompositionVariants>((IEnumerable<CompositionVariants>) new CompositionVariants[3]
        {
          CompositionVariants.Specification,
          CompositionVariants.ElementsList,
          CompositionVariants.SpecificationAndElementsList
        });
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof (type));
    }
    this.settings = settings;
  }

  /// <summary>Фильтруется ли компонент</summary>
  /// <param name="component">Компонент</param>
  /// <param name="variant">Вариант состава, в который попадает компонент</param>
  public bool InFilter(IElectricalComponent component, out CompositionVariants variant)
  {
    variant = CompositionVariants.SpecificationAndElementsList;
    return !this.CheckTable(component, out variant);
  }

  /// <summary>
  /// Проверка фильтрации компонента по пользовательским настройкам
  /// </summary>
  /// <param name="component">Компонент</param>
  /// <param name="variant">Вариант состава, в который попадает компонент</param>
  protected abstract bool CheckTable(
    IElectricalComponent component,
    out CompositionVariants variant);
}
