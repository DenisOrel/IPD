// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SpecRowAttributeInfoClient
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Client.Core;
using Intermech.Interfaces.Attributes;
using Intermech.Interfaces.AVS;

#nullable disable
namespace Intermech.AVS;

/// <summary> Атрибут строки спецификации</summary>
public class SpecRowAttributeInfoClient : AvsRowAttributeInfo
{
  /// <summary> Конструктор по-умолчанию </summary>
  public SpecRowAttributeInfoClient()
  {
  }

  /// <summary> Конструктор из общего дескриптора атрибута </summary>
  /// <param name="attributeDescriptor"> Общий дескриптор атрибута </param>
  public SpecRowAttributeInfoClient(AttributeDescriptor attributeDescriptor)
    : this()
  {
    this.AttrSrc = attributeDescriptor.IsRelationAttribute ? FieldSource.Relation : FieldSource.Object;
    this.AttributeId = attributeDescriptor.AttributeID;
    this.Name = attributeDescriptor.AttributeName;
  }

  /// <summary> Конструктор </summary>
  /// <param name="attributeId"> Идентификатор атрибута </param>
  /// <param name="isRelationAttribute"> Признак того, что значение атрибута надо читать у связи </param>
  public SpecRowAttributeInfoClient(int attributeId, bool isRelationAttribute)
    : this(attributeId, isRelationAttribute, string.Empty)
  {
  }

  /// <summary> Конструктор </summary>
  /// <param name="attributeId"> Идентификатор атрибута </param>
  /// <param name="isRelationAttribute"> Признак того, что значение атрибута надо читать у связи </param>
  /// <param name="attributeName"> Наименование атрибута </param>
  public SpecRowAttributeInfoClient(
    int attributeId,
    bool isRelationAttribute,
    string attributeName)
  {
    this.AttrSrc = isRelationAttribute ? FieldSource.Relation : FieldSource.Object;
    this.AttributeId = attributeId;
    this.Name = attributeName == null || attributeName == string.Empty ? DBHelper.GetAttributeName(this.AttributeId) : attributeName;
  }
}
