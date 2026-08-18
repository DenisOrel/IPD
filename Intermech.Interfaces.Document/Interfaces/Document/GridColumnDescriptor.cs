// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.GridColumnDescriptor
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Описатель атрибута из AdditionalAttributeCollection для преставления его в PropertyGrid</summary>
[Serializable]
public class GridColumnDescriptor : CustomPropertyDescriptor
{
  private int columnIndex = -1;

  /// <summary>Конструктор</summary>
  public GridColumnDescriptor(string displayName, int index, Attribute[] attrs)
    : base(displayName, attrs)
  {
    this.columnIndex = index;
  }

  /// <summary>Атрибуты свойства</summary>
  public override AttributeCollection Attributes
  {
    [DebuggerStepThrough] get
    {
      if (base.Attributes.Contains((Attribute) RefreshPropertiesAttribute.All))
        return base.Attributes;
      Attribute[] attributeArray = new Attribute[base.Attributes.Count + 1];
      base.Attributes.CopyTo((Array) attributeArray, 0);
      attributeArray[attributeArray.Length - 1] = (Attribute) RefreshPropertiesAttribute.All;
      return new AttributeCollection(attributeArray);
    }
  }

  /// <summary>Получить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Значение свойства</returns>
  public override object GetValue(object component)
  {
    return component is List<RowColParams> rowColParamsList && this.columnIndex < rowColParamsList.Count ? (object) rowColParamsList[this.columnIndex] : (object) null;
  }

  /// <summary>Установить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <param name="value">Значение свойства</param>
  public override void SetValue(object component, object value)
  {
    if (!(component is List<RowColParams> rowColParamsList) || this.columnIndex >= rowColParamsList.Count)
      return;
    RowColParams rowColParams = value as RowColParams;
    if (rowColParamsList[this.columnIndex] == rowColParams)
      return;
    rowColParams.SetOwnerTable(rowColParamsList[this.columnIndex].OwnerTable);
    rowColParams.SetIsColumn(rowColParamsList[this.columnIndex].IsColumn);
    rowColParamsList[this.columnIndex].SetOwnerTable((TableData) null);
    rowColParamsList[this.columnIndex] = rowColParams;
  }

  /// <summary>Тип владельца свойства</summary>
  public override Type ComponentType
  {
    [DebuggerStepThrough] get => typeof (List<RowColParams>);
  }

  /// <summary>Тип свойства</summary>
  public override Type PropertyType
  {
    [DebuggerStepThrough] get => typeof (RowColParams);
  }

  /// <summary>Можно ли сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Можно ли сбросить значение свойства</returns>
  public override bool CanResetValue(object component) => false;

  /// <summary>Сбросить значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  public override void ResetValue(object component)
  {
  }

  /// <summary>Нужно ли сохранить данное значение свойства</summary>
  /// <param name="component">Владелец свойства</param>
  /// <returns>Нужно ли сохранить данное значение свойства</returns>
  public override bool ShouldSerializeValue(object component) => false;

  /// <summary>Только для чтения</summary>
  public override bool IsReadOnly
  {
    [DebuggerStepThrough] get => false;
  }

  /// <summary>Категория свойства</summary>
  public override string Category
  {
    [DebuggerStepThrough] get
    {
      string category = base.Category;
      if (category == "Misc")
        category = LocalizationHolder.rm.GetString("Interfaces.Document_130");
      return category;
    }
  }
}
