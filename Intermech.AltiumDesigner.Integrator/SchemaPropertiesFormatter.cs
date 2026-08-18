// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Integrator.SchemaPropertiesFormatter
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.AltiumDesigner.Interfaces;
using Intermech.Data;
using Intermech.Tools.Integrators.Electrical;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AltiumDesigner.Integrator;

internal class SchemaPropertiesFormatter : OpenMetadataValueBagFormatter
{
  protected IAttributeCodec codec;

  public override bool IsContainerSupported(IValueBagContainer container)
  {
    return container is ParametersContainer;
  }

  private SchemaComponent GetSchemaComponent(IValueBagContainer container)
  {
    return (SchemaComponent) container;
  }

  protected override ValueBag DoRead(IValueBagContainer container, ICollection<StringKey> valueKeys)
  {
    ValueBag valueBag = new ValueBag(valueKeys.Count);
    Parameter[] parameters = ((ParametersContainer) container).Parameters;
    foreach (StringKey valueKey in (IEnumerable<StringKey>) valueKeys)
    {
      if (valueBag.Find(valueKey) == null)
      {
        object obj = !CompoundHelper.isCompound((string) valueKey) ? this.FindValue(valueKey, parameters) : (object) ParametrableCompoundValue.HandleValue(parameters, (string) valueKey);
        if (obj != null)
          valueBag.Add(new ValueRecord(valueKey, this.HandleValue(obj, (string) valueKey)));
      }
    }
    return valueBag;
  }

  protected virtual object HandleValue(object value, string parameterName) => value;

  private object FindValue(StringKey key, Parameter[] parameters)
  {
    return Array.Find<Parameter>(parameters, (Predicate<Parameter>) (element => (StringKey) element.Name == key))?.Value;
  }

  protected override void DoWrite(
    IValueBagContainer container,
    ContainerValues values,
    ICollection<StringKey> changedValues)
  {
    if (container == null)
      throw new ArgumentNullException(nameof (container));
    if (values == null)
      throw new ArgumentNullException(nameof (values));
    if (changedValues == null)
      throw new ArgumentNullException(nameof (changedValues));
    ParametersContainer parametersContainer = (ParametersContainer) container;
    Parameter[] parameterArray = parametersContainer.Parameters;
    List<Parameter> collection = (List<Parameter>) null;
    foreach (ValueRecord valueRecord in values.Bag.FindAll((Predicate<ValueRecord>) (record => changedValues.Contains(record.Key))))
    {
      ValueRecord item = valueRecord;
      if (!CompoundHelper.isCompound((string) item.Key))
      {
        string key = (string) item.Key;
        object obj = item.Value;
        Parameter parameter = Array.Find<Parameter>(parameterArray, (Predicate<Parameter>) (element => (StringKey) element.Name == item.Key));
        if (parameter != null)
        {
          if (!parameter.IsReadOnly && (obj == null || !obj.Equals(parameter.Value)))
          {
            parameter.Value = obj;
            parameter.Modified = ModifiedTypes.Changed;
          }
        }
        else
        {
          if (collection == null)
            collection = new List<Parameter>();
          collection.Add(new Parameter(key, obj, false, item.DataType, ModifiedTypes.Added));
        }
      }
    }
    if (collection != null)
    {
      List<Parameter> parameterList = new List<Parameter>((IEnumerable<Parameter>) parameterArray);
      parameterList.AddRange((IEnumerable<Parameter>) collection);
      parameterArray = parameterList.ToArray();
    }
    parametersContainer.Parameters = parameterArray;
  }
}
