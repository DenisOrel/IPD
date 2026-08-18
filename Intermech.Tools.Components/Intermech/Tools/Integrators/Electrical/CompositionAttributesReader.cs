// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.CompositionAttributesReader
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal class CompositionAttributesReader
{
  public static bool CheckRelationAttributes(
    Dictionary<string, List<IElectricalComponent>> components,
    ECADIntegratorSettings settings)
  {
    if (components.Count > 1 && settings.RelationPartAttributesTable != null && settings.RelationPartAttributesTable.Count > 0)
    {
      foreach (Tuple<StringKey, StringKey, bool> tuple in settings.RelationPartAttributesTable)
      {
        object objA = (object) null;
        bool flag = true;
        foreach (KeyValuePair<string, List<IElectricalComponent>> component in components)
        {
          object propertyValue = component.Value[0].GetPropertyValue((string) tuple.Item2);
          if (flag)
          {
            objA = propertyValue;
            flag = false;
          }
          else if (!object.Equals(objA, propertyValue))
            return false;
        }
      }
    }
    return true;
  }

  public static void CreateRelationAttributes(
    CompositionItem relation,
    IElectricalComponent component,
    ECADIntegratorSettings settings)
  {
    List<Tuple<string, object>> tupleList = CompositionAttributesReader.ReadAttributes(component, settings.RelationPartAttributesTable);
    if (tupleList == null)
      return;
    foreach (Tuple<string, object> tuple in tupleList)
      relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) tuple.Item1, tuple.Item2));
  }

  public static List<Tuple<string, object>> ReadAttributes(
    IElectricalComponent component,
    ECADIntegratorSettings settings)
  {
    List<Tuple<string, object>> tupleList = new List<Tuple<string, object>>();
    List<Tuple<string, object>> collection1 = CompositionAttributesReader.ReadAttributes(component, settings.RelationPartAttributesTable);
    if (collection1 != null && collection1.Count > 0)
      tupleList.AddRange((IEnumerable<Tuple<string, object>>) collection1);
    List<Tuple<string, object>> collection2 = CompositionAttributesReader.ReadAttributes(component, settings.PartAttributesTable);
    if (collection2 != null && collection2.Count > 0)
      tupleList.AddRange((IEnumerable<Tuple<string, object>>) collection2);
    return tupleList;
  }

  private static List<Tuple<string, object>> ReadAttributes(
    IElectricalComponent component,
    List<Tuple<StringKey, StringKey, bool>> attributes)
  {
    if (attributes == null || attributes.Count == 0)
      return (List<Tuple<string, object>>) null;
    List<Tuple<string, object>> tupleList = new List<Tuple<string, object>>();
    foreach (Tuple<StringKey, StringKey, bool> attribute in attributes)
    {
      IComponentProperty property = component.GetProperty((string) attribute.Item2);
      if (property != null && property.Value != null)
        tupleList.Add(new Tuple<string, object>((string) attribute.Item1, property.Value));
    }
    return tupleList;
  }
}
