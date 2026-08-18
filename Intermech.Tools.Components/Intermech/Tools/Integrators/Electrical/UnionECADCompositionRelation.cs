// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.UnionECADCompositionRelation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public class UnionECADCompositionRelation(
  ECADIntegratorSettings settings,
  ComponentsGroup group,
  ElectricalArticleCache assembly) : ECADCompositionRelation(settings, group, assembly)
{
  public override void Handle(List<Guid> posGuids)
  {
    this.CreateRelation(this.CreatePosGuid(posGuids), this.SummPosDesignations(), this.group.Components.First<KeyValuePair<string, List<IElectricalComponent>>>().Value[0]);
  }

  protected override MeasuredValue GetQuantity()
  {
    return new MeasuredValue((double) this.group.Components.Count, IDCache.Default.ItemsMeasure.Id);
  }

  /// <summary>Определяем и устанавливаем позиционный идентификатор</summary>
  /// <returns></returns>
  private Guid CreatePosGuid(List<Guid> posGuids)
  {
    Guid empty = Guid.Empty;
    Guid posGuid = this.group.PosGuids.Count <= 0 || posGuids.Contains(this.group.PosGuids[0]) ? Guid.NewGuid() : this.group.PosGuids[0];
    posGuids.Add(posGuid);
    foreach (KeyValuePair<string, List<IElectricalComponent>> component in this.group.Components)
    {
      foreach (IPropertiesCollection propertiesCollection in component.Value)
        propertiesCollection.SetPropertyValue(ElectricalConsts.PosGuidAttribute, (object) posGuid.ToString());
    }
    return posGuid;
  }

  /// <summary>
  /// Суммирование позиционных обозначений внутри одной группы
  /// </summary>
  /// <returns></returns>
  private string SummPosDesignations()
  {
    List<string> posDesignations = new List<string>();
    foreach (KeyValuePair<string, List<IElectricalComponent>> component in this.group.Components)
      posDesignations.Add(component.Key);
    return posDesignations.Count == 1 ? posDesignations[0] : PosDesignationHelper.Summ(posDesignations);
  }
}
