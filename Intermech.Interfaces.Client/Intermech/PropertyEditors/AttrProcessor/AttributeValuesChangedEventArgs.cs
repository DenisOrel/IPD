// Decompiled with JetBrains decompiler
// Type: Intermech.PropertyEditors.AttrProcessor.AttributeValuesChangedEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.PropertyEditors.AttrProcessor;

public class AttributeValuesChangedEventArgs : EventArgs
{
  private AttributeValuesAction action;
  private int attributeId;
  private object info;

  public AttributeValuesAction Action => this.action;

  public int AttributeId => this.attributeId;

  public object Info => this.info;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="attributeId"></param>
  /// <param name="action"></param>
  /// <param name="info">дополнительная информация:
  /// 
  /// 1.для AttributeValuesAction.Remove info = удаляемый AttributeValues
  /// 
  /// 2.для всех остальных AttributeValuesAction info=object[]{ index, value },
  /// 
  /// при этом для AttributeValuesAction.ModifyValue и AttributeValuesAction.RemoveValue
  /// при множественном изменении значений index=-1, value=object[]{};
  /// при одиночном index=индекс, value=object
  /// 
  /// для AttributeValuesAction.InsertValue info=object[]{ index, value }
  /// при вставке index=индекс вставки, при добавлении index=-1,
  /// value=object в любом случае. </param>
  public AttributeValuesChangedEventArgs(
    int attributeId,
    AttributeValuesAction action,
    object info)
  {
    this.attributeId = attributeId;
    this.action = action;
    this.info = info;
  }
}
