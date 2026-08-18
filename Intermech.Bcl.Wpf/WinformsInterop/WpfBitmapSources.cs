
// Type: Intermech.UI.Wpf.WinformsInterop.WpfBitmapSources
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;


namespace Intermech.UI.Wpf.WinformsInterop;

/// <summary>
/// Предоставляет методы для получения WPF-совместимых изображений из ресурсов System.Windows.Forms.
/// </summary>
public static class WpfBitmapSources
{
  /// <summary>
  /// Создает <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> из внедренного ресурса растрового изображения.
  /// </summary>
  /// <param name="winformsResourceType">Сгенерированный компилятором тип для доступа к ресурсам сборки</param>
  /// <param name="resourceStreamName">Имя файла растрового изображения в ресурсах сборки</param>
  /// <returns>Объект <see cref="T:System.Windows.Media.Imaging.BitmapSource" /></returns>
  public static BitmapSource FromManifestResourceStream(
    Type winformsResourceType,
    string resourceStreamName)
  {
    if (winformsResourceType == (Type) null)
      throw new ArgumentNullException(nameof (winformsResourceType));
    if (string.IsNullOrEmpty(resourceStreamName))
      throw new ArgumentException("Не задано имя файла в ресурсах сборки.", nameof (resourceStreamName));
    using (Stream manifestResourceStream = winformsResourceType.Assembly.GetManifestResourceStream(winformsResourceType, resourceStreamName))
    {
      BitmapImage bitmapImage = new BitmapImage();
      bitmapImage.BeginInit();
      try
      {
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.StreamSource = manifestResourceStream;
      }
      finally
      {
        bitmapImage.EndInit();
      }
      bitmapImage.Freeze();
      return (BitmapSource) bitmapImage;
    }
  }

  /// <summary>
  /// Создает <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> из <see cref="T:System.Drawing.Bitmap" />.
  /// </summary>
  /// <param name="bitmap">Растровое изображение System.Windows.Forms</param>
  /// <returns>Объект <see cref="T:System.Windows.Media.Imaging.BitmapSource" /></returns>
  public static BitmapSource FromBitmap(Bitmap bitmap)
  {
    IntPtr num = bitmap != null ? bitmap.GetHbitmap() : throw new ArgumentNullException(nameof (bitmap));
    try
    {
      BitmapSource sourceFromHbitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(num, IntPtr.Zero, new Int32Rect(0, 0, bitmap.Width, bitmap.Height), BitmapSizeOptions.FromEmptyOptions());
      sourceFromHbitmap.Freeze();
      return sourceFromHbitmap;
    }
    finally
    {
      WpfBitmapSources.NativeMethods.DeleteObject(num);
    }
  }

  /// <summary>
  /// Создает <see cref="T:System.Windows.Media.Imaging.BitmapSource" /> из <see cref="T:System.Drawing.Icon" />.
  /// </summary>
  /// <param name="icon">Иконка System.Windows.Forms</param>
  /// <returns>Объект <see cref="T:System.Windows.Media.Imaging.BitmapSource" /></returns>
  public static BitmapSource FromIcon(Icon icon)
  {
    if (icon == null)
      throw new ArgumentNullException(nameof (icon));
    BitmapSource bitmapSourceFromHicon = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(icon.Handle, new Int32Rect(0, 0, icon.Width, icon.Height), BitmapSizeOptions.FromEmptyOptions());
    bitmapSourceFromHicon.Freeze();
    return bitmapSourceFromHicon;
  }

  private static class NativeMethods
  {
    [DllImport("gdi32.dll")]
    public static extern bool DeleteObject(IntPtr hObject);
  }
}
