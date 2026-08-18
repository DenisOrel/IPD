
// Type: Intermech.UI.Wpf.Controls.BooleanToVisibilityConverter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Intermech.UI.Wpf.Controls;

[ValueConversion(typeof (bool), typeof (Visibility))]
public sealed class BooleanToVisibilityConverter : IValueConverter
{
  private bool isInverted;

  public bool IsInverted
  {
    get => this.isInverted;
    set
    {
      if (this.isInverted == value)
        return;
      this.isInverted = value;
    }
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (!(value is bool flag))
      return DependencyProperty.UnsetValue;
    if (this.IsInverted)
      flag = !flag;
    return (object) (Visibility) (flag ? 0 : 2);
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
