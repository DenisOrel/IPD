// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.MaterialQuantityHelper
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Interfaces;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal static class MaterialQuantityHelper
{
  public static bool IsMaterial(ADIntegratorSettings settings, ComponentsGroup group)
  {
    Guid componentObjectType = ADArticleTypesService.GetComponentObjectType(settings, group.Components.First<KeyValuePair<string, List<IElectricalComponent>>>().Value[0]);
    return componentObjectType != new Guid("cad0038d-306c-11d8-b4e9-00304f19f545") && MetaDataHelper.GetObjectTypeParentsGuid(componentObjectType).Contains(new Guid("cad00170-306c-11d8-b4e9-00304f19f545"));
  }

  public static MeasuredValue SummMaterials(
    string quantityParameter,
    ComponentsGroup group,
    IIntegratorOutput outputSvc)
  {
    if (string.IsNullOrEmpty(quantityParameter))
      return (MeasuredValue) null;
    try
    {
      MeasuredValue operand1 = (MeasuredValue) null;
      foreach (KeyValuePair<string, List<IElectricalComponent>> component in group.Components)
      {
        MeasuredValue measuredValue = MaterialQuantityHelper.GetMeasuredValue(component.Value[0], quantityParameter);
        if (measuredValue == null)
          return (MeasuredValue) null;
        operand1 = operand1 != null ? MeasureHelper.Add(operand1, measuredValue, true) : measuredValue;
      }
      return operand1;
    }
    catch (Exception ex)
    {
      outputSvc.WriteLine($"Ошибка при суммировании количества {group.PartName}: {ex.Message}");
      return (MeasuredValue) null;
    }
  }

  private static MeasuredValue GetMeasuredValue(
    IElectricalComponent component,
    string quantityParameter)
  {
    object propertyValue = component.GetPropertyValue(quantityParameter);
    return propertyValue == null ? (MeasuredValue) null : MeasureHelper.ConvertToMeasuredValue(Convert.ToString(propertyValue));
  }
}
