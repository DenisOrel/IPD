
// Type: Intermech.UI.Wpf.Controls.ValuePresenceToVisibilityConverter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Intermech.UI.Wpf.Controls;

[ValueConversion(typeof (object), typeof (Visibility))]
public sealed class ValuePresenceToVisibilityConverter : IValueConverter
{
  private bool emptyStringIsPresentValue;

  public ValuePresenceToVisibilityConverter() => this.emptyStringIsPresentValue = true;

  public bool EmptyStringIsPresentValue
  {
    [DebuggerStepThrough] get => this.emptyStringIsPresentValue;
    set
    {
      if (this.emptyStringIsPresentValue == value)
        return;
      this.emptyStringIsPresentValue = value;
    }
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value == null)
      return (object) Visibility.Collapsed;
    return !this.EmptyStringIsPresentValue && object.Equals(value, (object) string.Empty) ? (object) Visibility.Collapsed : (object) Visibility.Visible;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotSupportedException();
  }
}
