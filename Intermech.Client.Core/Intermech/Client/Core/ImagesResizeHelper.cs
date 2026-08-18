
// Type: Intermech.Client.Core.ImagesResizeHelper
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;


namespace Intermech.Client.Core;

/// <summary>
/// Вспомогательный статический класс для изменения размеров и форматов изображений
/// </summary>
public static class ImagesResizeHelper
{
  /// <summary>Прозрачный цвет в IPS</summary>
  public static Color IPSTransparentColor = Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue);

  [DllImport("user32.dll", CharSet = CharSet.Auto)]
  public static extern bool DestroyIcon(IntPtr handle);

  /// <summary>
  /// Изменить при необходимости размерность значка и вернуть новый значок 16 x 16
  /// </summary>
  /// <param name="ico">Исходный значок</param>
  /// <param name="ControlBkColor">Текущий цвет фона контрола, для которого рисуется значок</param>
  /// <returns>Значок с размерностью 16 x 16</returns>
  public static Icon ResizeIconTo16x16(Icon ico, Color ControlBkColor)
  {
    if (ico == null || ico.Width == 16 /*0x10*/ && ico.Height == 16 /*0x10*/)
      return ico;
    Icon icon1 = new Icon(ico, 16 /*0x10*/, 16 /*0x10*/);
    if (icon1.Width == 16 /*0x10*/ && icon1.Height == 16 /*0x10*/)
      return icon1;
    Icon icon2;
    using (Bitmap bmp = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
    {
      using (SolidBrush solidBrush = new SolidBrush(ControlBkColor))
      {
        using (Graphics graphics = Graphics.FromImage((Image) bmp))
        {
          Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
          graphics.FillRectangle((Brush) solidBrush, rectangle);
          graphics.DrawIcon(ico, rectangle);
        }
      }
      bmp.MakeTransparent(ControlBkColor);
      icon2 = ImageHelper.BitmapToIcon(bmp);
    }
    ImagesResizeHelper.DestroyIcon(icon1.Handle);
    icon1.Dispose();
    return icon2;
  }

  /// <summary>
  /// Изменить при необходимости размерность значка и вернуть новый значок 32 x 16
  /// </summary>
  /// <param name="ico">Исходный значок</param>
  /// <param name="ControlBkColor">Текущий цвет фона контрола, для которого рисуется значок</param>
  /// <returns>Значок с размерностью 32 x 16</returns>
  public static Icon ResizeIconTo32x16(Icon ico, Color ControlBkColor)
  {
    int x = 0;
    if (ico == null || ico.Width == 32 /*0x20*/ && ico.Height == 16 /*0x10*/)
      return ico;
    Icon icon1;
    using (Icon icon2 = new Icon(ico, 16 /*0x10*/, 16 /*0x10*/))
    {
      using (Bitmap bmp = new Bitmap(32 /*0x20*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
      {
        using (Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
        {
          using (SolidBrush solidBrush = new SolidBrush(ImagesResizeHelper.IPSTransparentColor))
          {
            using (Graphics graphics1 = Graphics.FromImage((Image) bmp))
            {
              Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
              graphics1.FillRectangle((Brush) solidBrush, rect);
              using (Graphics graphics2 = Graphics.FromImage((Image) bitmap))
              {
                Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                graphics2.FillRectangle((Brush) solidBrush, rectangle);
                if (icon2.Width == bitmap.Width && icon2.Height == bitmap.Height)
                  graphics2.DrawIconUnstretched(icon2, rectangle);
                else
                  graphics2.DrawIcon(ico, rectangle);
              }
              graphics1.DrawImageUnscaled((Image) bitmap, x, 0);
            }
          }
          bmp.MakeTransparent(ImagesResizeHelper.IPSTransparentColor);
          icon1 = ImageHelper.BitmapToIcon(bmp);
        }
      }
      ImagesResizeHelper.DestroyIcon(icon2.Handle);
    }
    return icon1;
  }

  /// <summary>
  /// Изменить при необходимости размерность изображения и вернуть новый значок 32 x 16
  /// </summary>
  /// <param name="image">Исходное изображение</param>
  /// <param name="ControlBkColor">Текущий цвет фона контрола, для которого рисуется значок</param>
  /// <returns>Значок с размерностью 32 x 16</returns>
  public static Icon ResizeIconTo32x16(Image image, Color ControlBkColor)
  {
    int x = 0;
    if (image == null)
      return (Icon) null;
    using (Bitmap bmp = new Bitmap(32 /*0x20*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
    {
      using (Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
      {
        using (SolidBrush solidBrush = new SolidBrush(ControlBkColor))
        {
          using (Graphics graphics1 = Graphics.FromImage((Image) bmp))
          {
            Rectangle rect1 = new Rectangle(0, 0, bmp.Width, bmp.Height);
            graphics1.FillRectangle((Brush) solidBrush, rect1);
            using (Graphics graphics2 = Graphics.FromImage((Image) bitmap))
            {
              Rectangle rect2 = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
              graphics2.FillRectangle((Brush) solidBrush, rect2);
              if (image.Width == bitmap.Width && image.Height == bitmap.Height)
                graphics2.DrawImageUnscaled(image, rect2);
              else
                graphics2.DrawImage(image, rect2);
            }
            graphics1.DrawImageUnscaled((Image) bitmap, x, 0);
          }
        }
        bmp.MakeTransparent(ControlBkColor);
        bmp.MakeTransparent(ControlBkColor);
        return ImageHelper.BitmapToIcon(bmp);
      }
    }
  }

  /// <summary>
  /// Изменить при необходимости размерность изображения и вернуть новое изображение 16 x 16
  /// </summary>
  /// <param name="image">Исходное изображение</param>
  /// <param name="ControlBkColor">Текущий цвет фона контрола, для которого рисуется значок</param>
  /// <returns>Изображение с размерностью 16 x 16</returns>
  public static Image ResizeImageTo16x16(Image image, Color ControlBkColor)
  {
    if (image == null)
      return (Image) null;
    if (image.Width == 16 /*0x10*/ && image.Height == 16 /*0x10*/)
      return image;
    Bitmap bitmap = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb);
    using (SolidBrush solidBrush = new SolidBrush(ControlBkColor))
    {
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
      {
        Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
        graphics.FillRectangle((Brush) solidBrush, rectangle);
        if (image.Height == 16 /*0x10*/ && image.Width > image.Height)
          graphics.DrawImage(image, rectangle, rectangle, GraphicsUnit.Pixel);
        else
          graphics.DrawImage(image, rectangle);
      }
    }
    bitmap.MakeTransparent(ControlBkColor);
    return (Image) bitmap;
  }

  /// <summary>
  /// Изменить при необходимости размерность изображения и вернуть новое изображение 32 x 16
  /// </summary>
  /// <param name="image">Исходное изображение</param>
  /// <param name="ControlBkColor">Текущий цвет фона контрола, для которого рисуется значок</param>
  /// <returns>Изображение с размерностью 32 x 16</returns>
  public static Image ResizeImageTo32x16(Image image, Color ControlBkColor)
  {
    int x = 0;
    if (image == null)
      return (Image) null;
    if (image.Width == 32 /*0x20*/ && image.Height == 16 /*0x10*/)
      return image;
    using (Bitmap bitmap1 = new Bitmap(32 /*0x20*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
    {
      using (Bitmap bitmap2 = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
      {
        using (SolidBrush solidBrush = new SolidBrush(ControlBkColor))
        {
          using (Graphics graphics1 = Graphics.FromImage((Image) bitmap1))
          {
            Rectangle rect1 = new Rectangle(0, 0, bitmap1.Width, bitmap1.Height);
            graphics1.FillRectangle((Brush) solidBrush, rect1);
            using (Graphics graphics2 = Graphics.FromImage((Image) bitmap2))
            {
              Rectangle rect2 = new Rectangle(0, 0, bitmap2.Width, bitmap2.Height);
              graphics2.FillRectangle((Brush) solidBrush, rect2);
              if (image.Width == bitmap2.Width && image.Height == bitmap2.Height)
                graphics2.DrawImageUnscaled(image, rect2);
              else
                graphics2.DrawImage(image, rect2);
            }
            graphics1.DrawImageUnscaled((Image) bitmap2, x, 0);
          }
        }
        bitmap1.MakeTransparent(ControlBkColor);
        return (Image) bitmap1;
      }
    }
  }

  /// <summary>
  /// Изменить при необходимости размерность изображения и вернуть новый значок 16 x 16
  /// </summary>
  /// <param name="image">Исходное изображение</param>
  /// <param name="ControlBkColor">Текущий цвет фона контрола, для которого рисуется значок</param>
  /// <returns>Значок с размерностью 16 x 16</returns>
  public static Icon ResizeIconTo16x16(Image image, Color ControlBkColor)
  {
    if (image == null)
      return (Icon) null;
    using (Bitmap bmp = new Bitmap(16 /*0x10*/, 16 /*0x10*/, PixelFormat.Format24bppRgb))
    {
      using (SolidBrush solidBrush = new SolidBrush(ControlBkColor))
      {
        using (Graphics graphics = Graphics.FromImage((Image) bmp))
        {
          Rectangle rectangle = new Rectangle(0, 0, bmp.Width, bmp.Height);
          graphics.FillRectangle((Brush) solidBrush, rectangle);
          graphics.DrawImage(image, rectangle, rectangle, GraphicsUnit.Pixel);
        }
      }
      bmp.MakeTransparent(ImagesResizeHelper.IPSTransparentColor);
      return ImageHelper.BitmapToIcon(bmp);
    }
  }

  /// <summary>
  /// Изменить при необходимости размерность изображения и вернуть новый значок 16 x 16
  /// </summary>
  /// <param name="image">Исходное изображение</param>
  /// <returns>Значок с размерностью 16 x 16</returns>
  public static Icon GetIconFromImage(Image image)
  {
    if (image == null)
      return (Icon) null;
    using (Bitmap bmp = new Bitmap(image.Width, image.Height, image.PixelFormat))
    {
      using (Graphics graphics = Graphics.FromImage((Image) bmp))
      {
        Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
        graphics.DrawImage(image, rect);
      }
      return ImageHelper.BitmapToIcon(bmp);
    }
  }
}
