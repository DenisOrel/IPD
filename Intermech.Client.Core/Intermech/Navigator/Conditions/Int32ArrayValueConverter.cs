
// Type: Intermech.Navigator.Conditions.Int32ArrayValueConverter
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Conditions;

internal sealed class Int32ArrayValueConverter : InputOperatorValueConverter<int[]>
{
  protected override int[] Convert(object value)
  {
    return value is int[] ? (int[]) value : Array.ConvertAll<object, int>((object[]) value, (Converter<object, int>) (x => (int) x));
  }
}
