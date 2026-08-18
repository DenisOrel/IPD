// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.CompositionItemAttribute
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Pdm;

/// <summary>Атрибут позиции</summary>
public sealed class CompositionItemAttribute : List<CompositionItemAttributeValue>, ICloneable
{
  private string _attributeName = string.Empty;

  /// <summary>Идентификатор атрибута</summary>
  public int AttributeID { get; private set; }

  /// <summary>Принадлежность атрибута</summary>
  public AttributeSourceTypes SourceType { get; private set; }

  /// <summary>Значение</summary>
  public object Value { get; private set; }

  /// <summary>Состояние сравниваемого атрибута</summary>
  public CompositionAttributeState State { get; set; }

  /// <summary>Строковое представление значения</summary>
  public string Description { get; set; }

  /// <summary>Наименование атрибута</summary>
  public string AttributeName
  {
    get
    {
      if (this._attributeName == string.Empty)
        this._attributeName = MetaDataHelper.GetAttributeTypeName(this.AttributeID);
      return this._attributeName;
    }
  }

  public string AttributeValueText
  {
    get
    {
      if (this.Description != null)
        return this.Description;
      return this.Value?.ToString();
    }
  }

  /// <summary>Конструктор</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="sourceType">Принадлежность атрибута</param>
  /// <param name="val">Значение</param>
  public CompositionItemAttribute(int attributeID, AttributeSourceTypes sourceType, object val)
    : this(attributeID, sourceType, val, (string) null, CompositionAttributeState.Equal)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="sourceType">Принадлежность атрибута</param>
  /// <param name="val">Значение</param>
  /// <param name="description">Строковое представление значения</param>
  public CompositionItemAttribute(
    int attributeID,
    AttributeSourceTypes sourceType,
    object val,
    string description)
    : this(attributeID, sourceType, val, description, CompositionAttributeState.Equal)
  {
  }

  /// <summary>Конструктор</summary>
  /// <param name="attributeID">Идентификатор атрибута</param>
  /// <param name="sourceType">Принадлежность атрибута</param>
  /// <param name="val">Значение</param>
  /// <param name="description">Строковое представление значения</param>
  /// <param name="state">Состояние сравниваемого атрибута</param>
  public CompositionItemAttribute(
    int attributeID,
    AttributeSourceTypes sourceType,
    object val,
    string description,
    CompositionAttributeState state)
  {
    this.AttributeID = attributeID;
    this.SourceType = sourceType;
    this.Value = val;
    this.Description = description;
    this.State = state;
    if (!(val is object[] objArray))
      return;
    for (int index = 0; index < objArray.Length; ++index)
      this.Add(new CompositionItemAttributeValue(this, index, objArray[index]));
  }

  /// <summary>Клонирование текущего экземпляра</summary>
  /// <returns></returns>
  public object Clone()
  {
    return (object) new CompositionItemAttribute(this.AttributeID, this.SourceType, this.Value, this.Description);
  }
}
