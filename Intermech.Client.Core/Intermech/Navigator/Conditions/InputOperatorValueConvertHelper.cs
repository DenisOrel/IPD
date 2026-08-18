
// Type: Intermech.Navigator.Conditions.InputOperatorValueConvertHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System.Collections;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

internal static class InputOperatorValueConvertHelper
{
  public static object Convert(object value, bool isObjectInputOperator)
  {
    IInputOperatorValueConverter operatorValueConverter;
    object obj;
    if (value is IList)
    {
      if (((ICollection) value).Count == 1)
      {
        operatorValueConverter = !isObjectInputOperator ? (IInputOperatorValueConverter) new Int32ValueConverter() : (IInputOperatorValueConverter) new Int64ValueConverter();
        obj = ((IList) value)[0];
      }
      else
      {
        operatorValueConverter = !isObjectInputOperator ? (IInputOperatorValueConverter) new Int32ArrayValueConverter() : (IInputOperatorValueConverter) new Int64ArrayValueConverter();
        obj = !(value is List<object>) ? value : (object) ((List<object>) value).ToArray();
      }
    }
    else
    {
      operatorValueConverter = !isObjectInputOperator ? (IInputOperatorValueConverter) new Int32ValueConverter() : (IInputOperatorValueConverter) new Int64ValueConverter();
      obj = value;
    }
    return obj == null ? (object) null : operatorValueConverter.ConvertValue(obj);
  }
}
