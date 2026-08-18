
// Type: Intermech.Navigator.Conditions.Int64ValueConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Conditions;

internal sealed class Int64ValueConverter : InputOperatorValueConverter<long>
{
  protected override long Convert(object value) => System.Convert.ToInt64(value);
}
