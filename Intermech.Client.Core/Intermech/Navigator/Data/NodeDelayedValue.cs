
// Type: Intermech.Navigator.Data.NodeDelayedValue
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Navigator.Data;

/// <summary>Значение узла навигатора с "отложенным" заполнением</summary>
public class NodeDelayedValue
{
  /// <summary>
  /// 
  /// </summary>
  private static readonly string EmptyValueCaption = LocalizationHolder.rm.GetString("Client.Core_NodeDelayedValueCaption");
  /// <summary>
  /// 
  /// </summary>
  public static readonly NodeDelayedValue EmptyValue = new NodeDelayedValue();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="value"></param>
  public NodeDelayedValue(object value = null) => this.Value = value;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    if (this.Value != null)
      return Convert.ToString(this.Value);
    return this.DefaultCaption ?? throw new InvalidOperationException();
  }

  /// <summary>
  /// 
  /// </summary>
  public object Value { get; set; }

  /// <summary>Строковое представление по-умолчанию</summary>
  public string DefaultCaption { get; set; } = NodeDelayedValue.EmptyValueCaption;
}
