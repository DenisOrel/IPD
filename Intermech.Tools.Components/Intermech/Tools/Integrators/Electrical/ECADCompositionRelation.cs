// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ECADCompositionRelation
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Data;
using Intermech.Interfaces;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

public abstract class ECADCompositionRelation : IECADCompositionRelation
{
  protected ComponentsGroup group;
  protected ElectricalArticleCache assembly;
  protected ECADIntegratorSettings settings;
  protected ECADCompositionRelationType type;

  public ECADCompositionRelation(
    ECADIntegratorSettings settings,
    ComponentsGroup group,
    ElectricalArticleCache assembly)
  {
    this.group = group;
    this.assembly = assembly;
    this.settings = settings;
    this.SetType();
  }

  protected void SetType()
  {
    this.type = ECADCompositionRelationType.None;
    if (this.group.GroupID.StartsWith("$"))
      this.type = ECADCompositionRelationType.Tuning | ECADCompositionRelationType.Replace;
    if (this.group.GroupID.StartsWith("@"))
      this.type = ECADCompositionRelationType.Replace;
    if (!this.group.GroupID.StartsWith("&"))
      return;
    this.type = ECADCompositionRelationType.Tuning;
  }

  protected abstract MeasuredValue GetQuantity();

  /// <summary>Создание связи</summary>
  /// <param name="posGuid">Guid позиции</param>
  /// <param name="posDesignation">Позиционное обозначение</param>
  /// <param name="component">Компонент у которого берутся значения для атрибутов связи</param>
  protected void CreateRelation(
    Guid posGuid,
    string posDesignation,
    IElectricalComponent component)
  {
    CompositionItem relation = new CompositionItem(this.group.PartName, posGuid);
    relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) IDCache.Default.PosDesignation.Text, (object) posDesignation));
    MeasuredValue quantity = this.GetQuantity();
    if (quantity != null)
      relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) IDCache.Default.Count.Text, (object) quantity));
    else
      relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) IDCache.Default.Count.Text, (object) TypedNull.Instance(typeof (MeasuredValue))));
    FunctionalGroup functionalGroup = this.group.Components.First<KeyValuePair<string, List<IElectricalComponent>>>().Value[0].FunctionalGroup;
    if (functionalGroup != null)
    {
      relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeFGPosDesignation), (object) functionalGroup.PosDesignation));
      if (!string.IsNullOrEmpty(functionalGroup.Designation))
        relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeFGDesignation), (object) functionalGroup.Designation));
      if (!string.IsNullOrEmpty(functionalGroup.Name))
        relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeFGName), (object) functionalGroup.Name));
    }
    if (this.group.CompositionVariant == CompositionVariants.SpecificationAndElementsList)
      relation.AdditionalAttributes.Add(new Tuple<StringKey, object>((StringKey) MetaDataHelper.GetAttributeTypeName(ElectricalConsts.attributeElementEL), (object) true));
    CompositionAttributesReader.CreateRelationAttributes(relation, component, this.settings);
    this.CreateAdditionalAttributes(relation);
    this.assembly.Composition.Add(relation);
  }

  public abstract void Handle(List<Guid> posGuids);

  private void CreateAdditionalAttributes(CompositionItem relation)
  {
    if ((this.type & ECADCompositionRelationType.Replace) == ECADCompositionRelationType.Replace)
      ECADCompositionReplaceRelation.CreateAdditionalAttributes(relation);
    if ((this.type & ECADCompositionRelationType.Tuning) != ECADCompositionRelationType.Tuning)
      return;
    ECADCompositionTuningRelation.CreateAdditionalAttributes(this.settings, this.group, relation);
  }
}
