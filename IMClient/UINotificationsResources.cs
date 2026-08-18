
// Type: IMClient.UINotificationsResources




using IMClient.Properties;
using Intermech.UI.Wpf.WinformsInterop;
using System.Drawing;
using System.Windows.Media;


namespace IMClient
{
    internal static class UINotificationsResources
    {
      public static ImageSource InfoIcon { get; } = (ImageSource) WpfBitmapSources.FromIcon(SystemIcons.Information);

      public static ImageSource WarningIcon { get; } = (ImageSource) WpfBitmapSources.FromIcon(SystemIcons.Warning);

      public static ImageSource ErrorIcon { get; } = (ImageSource) WpfBitmapSources.FromIcon(SystemIcons.Error);

      public static ImageSource CloseButton { get; } = (ImageSource) WpfBitmapSources.FromBitmap(Resources.CloseButton_20x20);
    }
}
