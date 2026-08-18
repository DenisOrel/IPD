
// Type: Intermech.Navigator.Conditions.ComboBoxItem
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;
using System.Globalization;


namespace Intermech.Navigator.Conditions;

/// <summary>Item в combo box для допустимых значений</summary>
internal sealed class ComboBoxItem
{
  /// <summary>Значение</summary>
  public object Value;
  /// <summary>Описание</summary>
  public string Description;

  public ComboBoxItem(object value, string description)
  {
    this.Value = value;
    this.Description = description;
  }

  public override string ToString()
  {
    return !(this.Description != string.Empty) ? Convert.ToString(this.Value, (IFormatProvider) CultureInfo.CurrentCulture) : this.Description;
  }
}
