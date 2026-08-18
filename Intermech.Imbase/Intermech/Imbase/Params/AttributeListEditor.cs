// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Params.AttributeListEditor
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Interfaces.Imbase.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;

#nullable disable
namespace Intermech.Imbase.Params;

internal class AttributeListEditor(Type type) : CollectionEditor(type)
{
  protected override bool CanSelectMultipleInstances() => false;

  protected override object CreateInstance(Type itemType)
  {
    return (object) new ItemProxy(new AttributeForObjectTypeInfo());
  }

  protected override string GetDisplayText(object value)
  {
    return value is ItemProxy itemProxy ? itemProxy.Name : base.GetDisplayText(value);
  }

  protected override CollectionEditor.CollectionForm CreateCollectionForm()
  {
    CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();
    collectionForm.Text = "Список атрибутов";
    collectionForm.Width = 700;
    collectionForm.Height = 450;
    return collectionForm;
  }

  protected override object[] GetItems(object editValue)
  {
    if (editValue is IEnumerable<AttributeForObjectTypeInfo> source)
      editValue = (object) source.Select<AttributeForObjectTypeInfo, ItemProxy>((Func<AttributeForObjectTypeInfo, ItemProxy>) (x => new ItemProxy(x))).ToArray<ItemProxy>();
    return base.GetItems(editValue);
  }

  protected override object SetItems(object editValue, object[] value)
  {
    value = value.OfType<ItemProxy>().Where<ItemProxy>((Func<ItemProxy, bool>) (x => x.AttributeTypeId != 0)).Select<ItemProxy, AttributeForObjectTypeInfo>((Func<ItemProxy, AttributeForObjectTypeInfo>) (x => new AttributeForObjectTypeInfo(x.ObjectTypeId, x.AttributeTypeId))).Distinct<AttributeForObjectTypeInfo>().Select<AttributeForObjectTypeInfo, object>((Func<AttributeForObjectTypeInfo, object>) (x => (object) x)).ToArray<object>();
    return base.SetItems(editValue, value);
  }

  public override object EditValue(
    ITypeDescriptorContext context,
    IServiceProvider provider,
    object value)
  {
    if (!(value is List<AttributeForObjectTypeInfo> forObjectTypeInfoList))
      return base.EditValue(context, provider, value);
    List<AttributeForObjectTypeInfo> second = new List<AttributeForObjectTypeInfo>((IEnumerable<AttributeForObjectTypeInfo>) forObjectTypeInfoList);
    base.EditValue(context, provider, (object) second);
    return forObjectTypeInfoList.Count != second.Count || forObjectTypeInfoList.Except<AttributeForObjectTypeInfo>((IEnumerable<AttributeForObjectTypeInfo>) second).Any<AttributeForObjectTypeInfo>() ? (object) second : (object) forObjectTypeInfoList;
  }
}
