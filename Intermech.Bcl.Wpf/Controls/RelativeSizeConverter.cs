
// Type: Intermech.UI.Wpf.Controls.RelativeSizeConverter
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


namespace Intermech.UI.Wpf.Controls;

[ValueConversion(typeof (double), typeof (double))]
public class RelativeSizeConverter : IValueConverter
{
  private double ratio;

  public RelativeSizeConverter() => this.ratio = 1.0;

  public double Ratio
  {
    get => this.ratio;
    set => this.ratio = value > 0.0 ? value : throw new ArgumentNullException(nameof (value));
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is double num ? (object) (num * this.ratio) : DependencyProperty.UnsetValue;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value is double num ? (object) (num / this.ratio) : DependencyProperty.UnsetValue;
  }
}
