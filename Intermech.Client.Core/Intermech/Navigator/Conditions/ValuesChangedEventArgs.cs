
// Type: Intermech.Navigator.Conditions.ValuesChangedEventArgs
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Conditions;

public sealed class ValuesChangedEventArgs
{
  public object Value1;
  public object Value2;

  public ValuesChangedEventArgs(object value1, object value2)
  {
    this.Value1 = value1;
    this.Value2 = value2;
  }
}
