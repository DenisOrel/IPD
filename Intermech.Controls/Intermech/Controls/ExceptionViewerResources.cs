
// Type: Intermech.Controls.ExceptionViewerResources
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using Intermech.UI.Wpf.WinformsInterop;
using System.Drawing;
using System.Windows.Media;


namespace Intermech.Controls;

internal static class ExceptionViewerResources
{
  public static ImageSource ErrorIcon { get; } = (ImageSource) WpfBitmapSources.FromIcon(SystemIcons.Error);
}
