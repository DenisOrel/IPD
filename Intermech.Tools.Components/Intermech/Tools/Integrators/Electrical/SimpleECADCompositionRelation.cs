// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.SimpleECADCompositionRelation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public class SimpleECADCompositionRelation(
  ECADIntegratorSettings settings,
  ComponentsGroup group,
  ElectricalArticleCache assembly) : ECADCompositionRelation(settings, group, assembly)
{
  public override void Handle(List<Guid> posGuidsCache)
  {
    int index = 0;
    foreach (KeyValuePair<string, List<IElectricalComponent>> component in this.group.Components)
    {
      this.CreateRelation(this.CreatePosGuid(posGuidsCache, index, component.Value), component.Key, component.Value[0]);
      ++index;
    }
  }

  protected override MeasuredValue GetQuantity()
  {
    return new MeasuredValue(1.0, IDCache.Default.ItemsMeasure.Id);
  }

  private Guid CreatePosGuid(
    List<Guid> posGuidsCache,
    int index,
    List<IElectricalComponent> componentParts)
  {
    Guid empty = Guid.Empty;
    Guid posGuid = this.group.PosGuids.Count <= 0 || posGuidsCache.Contains(this.group.PosGuids[index]) ? Guid.NewGuid() : this.group.PosGuids[index];
    posGuidsCache.Add(posGuid);
    foreach (IPropertiesCollection componentPart in componentParts)
      componentPart.SetPropertyValue(ElectricalConsts.PosGuidAttribute, (object) posGuid.ToString());
    return posGuid;
  }
}
