// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeValuesAction
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>действие по изменению</summary>
public enum AttributeValuesAction
{
  None,
  /// <summary>AttributeValues удален // info = AttributeValues</summary>
  Remove,
  /// <summary>
  /// добавлено значение в простом/многозначном AttributeValues, вызывается также при добавлении AttributeValues
  /// info = new object[] { index, value } || info = new object[] { -1, value[] }
  /// </summary>
  ModifyValue,
  /// <summary>
  /// добавлено значение в многозначном AttributeValues ; info = new object[] { index, value }
  /// </summary>
  InsertValue,
  /// <summary>
  /// удалено значение в многозначном AttributeValues	; info = new object[] { index, value }
  /// </summary>
  RemoveValue,
}
