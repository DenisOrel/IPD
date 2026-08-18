// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.ADComponentsListFilter
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Tools.Integrators.Electrical;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal sealed class ADComponentsListFilter(
  ADIntegratorSettings settings,
  ComponentsListFilterType type) : ComponentsListFilter((ECADIntegratorSettings) settings, type)
{
  protected override bool CheckTable(
    IElectricalComponent component,
    out CompositionVariants variant)
  {
    ComponentsFilterSettings<ADComponentsCompositionVariants> componentsFilter = ((ADIntegratorSettings) this.settings).ComponentsFilter;
    if (this.CheckOnlyELParameter(component, componentsFilter))
    {
      variant = CompositionVariants.ElementsList;
    }
    else
    {
      object propertyValue = component.GetPropertyValue("ComponentKind");
      if (propertyValue == null)
      {
        variant = CompositionVariants.SpecificationAndElementsList;
        return true;
      }
      variant = CompositionVariants.NoUsed;
      switch ((int) propertyValue)
      {
        case 0:
          variant = componentsFilter.Table.Standard.Value;
          break;
        case 1:
          variant = componentsFilter.Table.Mechanical.Value;
          break;
        case 2:
          variant = componentsFilter.Table.Graphical.Value;
          break;
        case 3:
          variant = componentsFilter.Table.NetTie_BOM.Value;
          break;
        case 4:
          variant = componentsFilter.Table.NetTie_NoBOM.Value;
          break;
        case 5:
          variant = componentsFilter.Table.Standard_NoBOM.Value;
          break;
      }
    }
    return this.enabledVariants.Contains(variant);
  }

  private bool CheckOnlyELParameter(
    IElectricalComponent component,
    ComponentsFilterSettings<ADComponentsCompositionVariants> filterSettings)
  {
    if (string.IsNullOrEmpty((string) filterSettings.OnlyElementListCondition.Item1))
      return false;
    object propertyValue = component.GetPropertyValue(filterSettings.OnlyElementListCondition.Item1.ToString());
    return propertyValue != null && propertyValue.ToString().Equals(filterSettings.OnlyElementListCondition.Item2);
  }
}
