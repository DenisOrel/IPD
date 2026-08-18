// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeValuesValidationEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

public class AttributeValuesValidationEventArgs : EventArgs
{
  private AttributeValuesList attributeValues;
  private List<Intermech.PropertyEditors.AttrProcessor.ValidationResult> validationResult;

  public AttributeValuesList AttributeValues => this.attributeValues;

  public List<Intermech.PropertyEditors.AttrProcessor.ValidationResult> ValidationResult
  {
    get => this.validationResult;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeValues">Валидируемые значения атрибутов</param>
  /// <param name="validationResult">Предварительные результаты валидации, дополняются в случае необходимости</param>
  public AttributeValuesValidationEventArgs(
    AttributeValuesList attributeValues,
    List<Intermech.PropertyEditors.AttrProcessor.ValidationResult> validationResult)
  {
    this.attributeValues = attributeValues;
    this.validationResult = validationResult;
  }
}
