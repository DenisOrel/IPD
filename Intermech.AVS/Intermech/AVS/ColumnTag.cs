// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ColumnTag
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;
using System;

#nullable disable
namespace Intermech.AVS;

/// <summary> Свойства колонки </summary>
public class ColumnTag
{
  private AvsRowAttributeInfo _specRowAttributeInfo;
  private int _productIndex;

  /// <summary> Конструктор </summary>
  public ColumnTag(AvsRowAttributeInfo specRowAttributeInfo)
  {
    this._specRowAttributeInfo = specRowAttributeInfo;
  }

  /// <summary> Конструктор </summary>
  public ColumnTag(AvsRowAttributeInfo specRowAttributeInfo, int productIndex)
    : this(specRowAttributeInfo)
  {
    this._productIndex = productIndex;
  }

  /// <summary> Идентификатор атрибута </summary>
  public int AttributeID
  {
    get => this._specRowAttributeInfo == null ? -1 : this._specRowAttributeInfo.AttributeId;
  }

  /// <summary> Guid атрибута </summary>
  public Guid AttributeGuid
  {
    get
    {
      return this._specRowAttributeInfo == null ? Guid.Empty : this._specRowAttributeInfo.AttributeGuid;
    }
  }

  /// <summary> Признак того, что значение надо брать у связи </summary>
  public bool IsRelation
  {
    get => this._specRowAttributeInfo != null && this._specRowAttributeInfo.IsRelationAttribute;
  }

  /// <summary> Признак того, что значение надо брать у связи </summary>
  public FieldSource AttrSrc
  {
    get
    {
      return this._specRowAttributeInfo == null ? FieldSource.DocumentRowField : this._specRowAttributeInfo.AttrSrc;
    }
  }

  /// <summary> Ссылка на дескриптор атрибута </summary>
  public AvsRowAttributeInfo SpecRowAttributeInfo
  {
    get => this._specRowAttributeInfo;
    set => this._specRowAttributeInfo = value;
  }

  /// <summary> Номер исполнения (например у колонки "количество" в спецификации формы B) </summary>
  public int ProductIndex
  {
    get => this._productIndex;
    set => this._productIndex = value;
  }
}
