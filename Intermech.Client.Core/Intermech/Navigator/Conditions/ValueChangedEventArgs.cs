
// Type: Intermech.Navigator.Conditions.ValueChangedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Conditions;

public sealed class ValueChangedEventArgs
{
  public object Value { get; private set; }

  public bool IsFirstValue { get; private set; }

  public ValueChangedEventArgs(object value, bool isFirstValue)
  {
    this.Value = value;
    this.IsFirstValue = isFirstValue;
  }
}
