
// Type: Intermech.UI.Wpf.Controls.CollectionToVisibilityConverter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Intermech.UI.Wpf.Controls;

[ValueConversion(typeof (ICollection), typeof (Visibility))]
public sealed class CollectionToVisibilityConverter : IValueConverter
{
  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is ICollection collection ? (object) (Visibility) (collection.Count != 0 ? 0 : 2) : DependencyProperty.UnsetValue;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
