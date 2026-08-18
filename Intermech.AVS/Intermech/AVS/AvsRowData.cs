// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.AvsRowData
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

internal class AvsRowData
{
  private AVSRow avsRow;
  private AttributeValuesCache attributeValues;

  public AVSRow AvsRow
  {
    get => this.avsRow;
    set => this.avsRow = value;
  }

  public AttributeValuesCache AttributeValues
  {
    get => this.attributeValues;
    set => this.attributeValues = value;
  }

  public AvsRowData(AVSRow avsRow, AttributeValuesCache attributeValues)
  {
    if (avsRow == null)
      throw new ArgumentNullException(nameof (avsRow));
    if (attributeValues == null)
      throw new ArgumentNullException(nameof (attributeValues));
    this.avsRow = avsRow;
    this.attributeValues = attributeValues;
  }

  public AvsRowData(AVSRow avsRow)
  {
    this.avsRow = avsRow != null ? avsRow : throw new ArgumentNullException(nameof (avsRow));
  }

  public AvsRowData(AttributeValuesCache attributeValues)
  {
    this.attributeValues = attributeValues != null ? attributeValues : throw new ArgumentNullException(nameof (attributeValues));
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attr">Информация об атрибуте</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="replaceDBNull">Заменять DBNull на null</param>
  /// <returns></returns>
  public void SetFieldValue(
    AvsRowAttributeInfo attr,
    object value,
    bool saveToDB,
    bool forceSaveDB,
    bool updateDocNode,
    bool updateListNode,
    bool failIfNotFound,
    bool exceptionIfFail)
  {
    if (this.attributeValues != null)
    {
      if (this.attributeValues is RelationAttributeValuesCache attributeValues && this.avsRow != null)
      {
        List<RelationAttributeValuesCache> relationList;
        int relationIndex = this.avsRow.GetRelationIndex(attributeValues.RelationId, out relationList);
        if (relationIndex == -1)
          return;
        this.avsRow.SetFieldValue(attr, relationIndex, -1, relationList, value, saveToDB, forceSaveDB, updateDocNode, updateListNode, failIfNotFound, exceptionIfFail);
      }
      else
        this.attributeValues.SetValue(attr, value, false);
    }
    else
    {
      if (this.avsRow == null)
        return;
      this.avsRow.SetFieldValue(attr, -1, -1, value, saveToDB, forceSaveDB, updateDocNode, updateListNode, failIfNotFound, exceptionIfFail);
    }
  }

  /// <summary>Получить значение атрибута</summary>
  /// <param name="attr">Информация об атрибуте</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="replaceDBNull">Заменять DBNull на null</param>
  /// <returns></returns>
  public object GetFieldValue(AvsRowAttributeInfo attr, bool failIfNotFound, bool replaceDBNull = false)
  {
    if (this.attributeValues != null && (attr.AttrSrc == FieldSource.Relation && this.attributeValues is RelationAttributeValuesCache || attr.AttrSrc == FieldSource.Object))
      return this.attributeValues.GetValue(attr, failIfNotFound, replaceDBNull);
    return this.avsRow != null ? this.avsRow.GetFieldValue(attr, -1, -1, replaceDBNull, failIfNotFound, true) : (object) null;
  }

  /// <summary>Получить значение булевского атрибута</summary>
  /// <param name="attr">Информация об атрибуте</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="defaultValue">Значение по умолчанию, для Null</param>
  public bool GetFieldBoolValue(AvsRowAttributeInfo attr, bool failIfNotFound, bool defaultValue = false)
  {
    object fieldValue = this.GetFieldValue(attr, failIfNotFound, true);
    return fieldValue == null ? defaultValue : Convert.ToBoolean(fieldValue);
  }

  /// <summary>Получить целочисленное значение атрибута</summary>
  /// <param name="attr">Информация об атрибуте</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="defaultValue">Значение по умолчанию, для Null</param>
  public long GetFieldInt64Value(AvsRowAttributeInfo attr, bool failIfNotFound, long defaultValue = -1)
  {
    return AvsIDCache.ConvertDbValueToInt64(this.GetFieldValue(attr, failIfNotFound, true), defaultValue);
  }

  /// <summary>Получить значение текстового атрибута</summary>
  /// <param name="attr">Информация об атрибуте</param>
  /// <param name="failIfNotFound">Генерировать исключение если атрибут не найден</param>
  /// <param name="replaceNull">Заменять DBNull и null на пустую строку</param>
  /// <returns></returns>
  public string GetFieldStringValue(
    AvsRowAttributeInfo attr,
    bool failIfNotFound,
    bool replaceNull = true)
  {
    object fieldValue = this.GetFieldValue(attr, failIfNotFound, true);
    if (fieldValue != null)
      return Convert.ToString(fieldValue);
    return !replaceNull ? (string) null : "";
  }

  public bool HasObject => this.ObjectID != -1L;

  public long ObjectID
  {
    get
    {
      if (this.attributeValues != null)
        return this.attributeValues.ObjectId;
      return this.avsRow != null ? this.avsRow.ObjectId : -1L;
    }
  }

  public int RelationType
  {
    get
    {
      if (this.attributeValues is RelationAttributeValuesCache attributeValues)
        return attributeValues.RelationType;
      return this.avsRow != null ? this.avsRow.RelType : -1;
    }
  }

  public long SectionID
  {
    get
    {
      if (this.attributeValues != null)
        return this.attributeValues.GetValueInt64(AvsIDCache.Attr_SpecificationSection, false);
      return this.avsRow != null ? this.avsRow.SectionID : -1L;
    }
  }

  internal long ProductID
  {
    get
    {
      return this.attributeValues is RelationAttributeValuesCache attributeValues ? attributeValues.ProjectId : 0L;
    }
  }

  public override string ToString()
  {
    if (this.AttributeValues != null)
      return this.AttributeValues.ToString();
    return this.avsRow != null ? this.avsRow.ToString() : base.ToString();
  }
}
