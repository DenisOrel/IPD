// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.DependencyListEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Imbase.Editors;
using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Imbase;

internal class DependencyListEditor : ModalEditor
{
  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (context == null || !(context.Instance is StructureEditorPropGridDescriptor instance))
      return value;
    Tuple<string, List<Tuple<object, object>>> tuple = value as Tuple<string, List<Tuple<object, object>>>;
    List<AttributeTypeProperties> attTypePropsList = StructureEditorPropGridDescriptor.AttTypePropsList;
    AttributeTypeProperties masterAtp = new AttributeTypeProperties();
    if (tuple != null)
    {
      masterAtp = this.FindAtp(tuple.Item1, attTypePropsList);
      if (masterAtp.AttributeID == 0)
        tuple = (Tuple<string, List<Tuple<object, object>>>) null;
    }
    if (tuple == null)
    {
      List<AttributeTypeProperties> attList = new List<AttributeTypeProperties>();
      int count = attTypePropsList.Count;
      for (int index = 0; index < count; ++index)
      {
        if (!attTypePropsList[index].AttributeGuid.Equals(instance.AttributeGuid) && attTypePropsList[index].MultiValueMode == MultiValueModes.SingleValueFromList)
        {
          string columnName = attTypePropsList[index].AttributeGuid.ToString();
          if (instance.HasColumn(columnName))
            attList.Add(attTypePropsList[index]);
        }
      }
      masterAtp = SelectAttributeForm.SelectAttribute(attList);
    }
    if (masterAtp.AttributeID != 0)
    {
      List<Tuple<object, object>> result = new List<Tuple<object, object>>();
      if (tuple != null)
        result.AddRange((IEnumerable<Tuple<object, object>>) tuple.Item2);
      AttributeTypeProperties attrTypeProps = instance.AttrTypeProps;
      if (DependencyEditor.EditDependency(ref masterAtp, ref attrTypeProps, ref result))
        value = result == null || result.Count <= 0 ? (object) null : (object) new Tuple<string, List<Tuple<object, object>>>(masterAtp.AttributeGuid.ToString(), result);
    }
    return value;
  }

  private AttributeTypeProperties FindAtp(string value, List<AttributeTypeProperties> allList)
  {
    int count = allList.Count;
    AttributeTypeProperties atp;
    for (int index = 0; index < count; ++index)
    {
      atp = allList[index];
      if (atp.AttributeGuid.ToString().Equals(value))
        return allList[index];
    }
    atp = new AttributeTypeProperties();
    return atp;
  }
}
