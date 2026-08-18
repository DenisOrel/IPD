
// Type: IMClient.UINotificationIconToImageSourceConverter




using Intermech.Interfaces.Client;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


namespace IMClient
{
    [ValueConversion(typeof (UINotificationIcon), typeof (ImageSource))]
    internal sealed class UINotificationIconToImageSourceConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        object obj;
        if ((obj = value) is UINotificationIcon)
        {
          switch ((UINotificationIcon) obj)
          {
            case UINotificationIcon.Info:
              return (object) UINotificationsResources.InfoIcon;
            case UINotificationIcon.Warning:
              return (object) UINotificationsResources.WarningIcon;
            case UINotificationIcon.Error:
              return (object) UINotificationsResources.ErrorIcon;
          }
        }
        return DependencyProperty.UnsetValue;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotSupportedException();
      }
    }
}
