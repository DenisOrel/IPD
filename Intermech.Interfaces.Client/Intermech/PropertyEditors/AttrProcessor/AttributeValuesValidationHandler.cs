// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeValuesValidationHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>
/// Делегат валидации значений атрибутов.
/// Информация по несвалидированным атрибутам AttributeValuesValidationEventArgs.AttributeValues добавляется в список AttributeValuesValidationEventArgs.ValidationResult
/// </summary>
/// <param name="sender"></param>
/// <param name="args"></param>
public delegate void AttributeValuesValidationHandler(
  object sender,
  AttributeValuesValidationEventArgs args);
