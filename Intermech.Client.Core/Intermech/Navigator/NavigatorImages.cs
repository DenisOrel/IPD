
// Type: Intermech.Navigator.NavigatorImages
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Castle.Core.Resource;
using Intermech.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;


namespace Intermech.Navigator;

/// <summary>Картинки в ресурсах навигатора</summary>
public static class NavigatorImages
{
  [CanBeNull]
  private static Image _horizontalDottedTreeLine;
  [NotNull]
  private static readonly ConcurrentDictionary<string, Image> _images = new ConcurrentDictionary<string, Image>((IEqualityComparer<string>) StringComparer.InvariantCulture);

  /// <summary>Иконка статуса сравнения структур объектов "Не изменён" (объект присутствует и в итерации, и в актуальном
  /// составе, его параметры не изменились)</summary>
  [NotNull]
  public static Image HorizontalDottedTreeLine
  {
    get
    {
      return NavigatorImages._horizontalDottedTreeLine ?? NavigatorImages.GetImage(ref NavigatorImages._horizontalDottedTreeLine, "HorizontalDottedLine.bmp");
    }
  }

  /// <summary>"Ленивый" метод получения картинки. Проверяет загружена ли, если нет - загружает</summary>
  /// <param name="image">[in,out] картинка, метод проверяет проинициализирована ли она</param>
  /// <param name="iconName">Имя иконки в ресурсах навигатора</param>
  /// <returns>Картинка</returns>
  [ContractAnnotation("=> NotNull, image:NotNull")]
  public static Image GetImage(ref Image image, [NotNull, NotWhitespace] string iconName)
  {
    image = image ?? NavigatorImages._images.GetOrAdd(iconName, (Func<string, Image>) (name =>
    {
      using (Stream resourceStream = Services.GetResourceStream(iconName))
      {
        Bitmap image1 = resourceStream != null && resourceStream.CanRead ? new Bitmap(resourceStream) : throw new ResourceException("Can`t load navigator resource stream: " + iconName);
        image1.MakeTransparent();
        return (Image) image1;
      }
    }));
    return image;
  }
}
