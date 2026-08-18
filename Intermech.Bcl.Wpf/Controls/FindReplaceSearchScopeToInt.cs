
// Type: Intermech.UI.Wpf.Controls.FindReplaceSearchScopeToInt
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Globalization;
using System.Windows.Data;


namespace Intermech.UI.Wpf.Controls;

internal sealed class FindReplaceSearchScopeToInt : IValueConverter
{
  object IValueConverter.Convert(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
  {
    return (object) (int) value;
  }

  object IValueConverter.ConvertBack(
    object value,
    Type targetType,
    object parameter,
    CultureInfo culture)
  {
    return (object) (FindReplaceSearchScope) value;
  }
}
