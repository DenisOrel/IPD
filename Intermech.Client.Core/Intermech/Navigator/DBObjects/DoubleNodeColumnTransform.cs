
// Type: Intermech.Navigator.DBObjects.DoubleNodeColumnTransform
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Globalization;


namespace Intermech.Navigator.DBObjects;

public sealed class DoubleNodeColumnTransform : INodeColumnTransform
{
  public Type DataType => typeof (string);

  public object Apply(object sourceValue, NodeColumn column, object adapter, object[] allValues)
  {
    switch (sourceValue)
    {
      case double _:
      case Decimal _:
        return (object) ((IConvertible) sourceValue).ToDouble((IFormatProvider) CultureInfo.CurrentCulture).ToString((IFormatProvider) CultureInfo.CurrentCulture);
      default:
        return (object) string.Empty;
    }
  }
}
