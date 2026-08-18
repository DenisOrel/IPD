
// Type: Intermech.UI.Wpf.Controls.StringIsNullOrEmptyConverter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Globalization;
using System.Windows.Data;


namespace Intermech.UI.Wpf.Controls;

public sealed class StringIsNullOrEmptyConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value == null ? (object) true : (object) string.IsNullOrEmpty(value.ToString());
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new InvalidOperationException();
  }
}
