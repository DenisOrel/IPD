
// Type: Intermech.Bars.MenuMeasure
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Bars
{
    internal class MenuMeasure
    {
      public static int MaxImageWidth(ICollection items, ImageList imageList)
      {
        int num = 0;
        foreach (MenuButtonItem menuButtonItem in (IEnumerable) items)
        {
          if (menuButtonItem.Icon != null && menuButtonItem.IconSize.Width > num)
            num = menuButtonItem.IconSize.Width;
          else if (menuButtonItem.Image != null && menuButtonItem.Image.Width > num)
            num = menuButtonItem.Image.Width;
        }
        if (imageList != null && imageList.ImageSize.Width > num)
        {
          foreach (MenuButtonItem menuButtonItem in (IEnumerable) items)
          {
            if (menuButtonItem.ImageIndex >= 0 && menuButtonItem.ImageIndex < imageList.Images.Count)
            {
              num = imageList.ImageSize.Width;
              break;
            }
          }
        }
        if (num < 16 /*0x10*/)
          num = 16 /*0x10*/;
        return num + 16 /*0x10*/;
      }

      public static Size MenuItemSize(
        Graphics graphics,
        MenuButtonItem menuItem,
        ImageList imageList,
        IPopupMenuHost popupHost)
      {
        SizeF sizeF1 = (SizeF) Size.Empty;
        Size empty = Size.Empty;
        int num = 0;
        SizeF sizeF2 = graphics.MeasureString(menuItem.Text, menuItem.Font, 999, popupHost.Renderer.MenuTextStringFormat);
        if (menuItem.Shortcut != Shortcut.None)
          sizeF1 = graphics.MeasureString(menuItem.FriendlyShortcut, menuItem.Font, 999, popupHost.Renderer.MenuShortcutStringFormat);
        empty.Width = (int) Math.Ceiling((double) sizeF2.Width + (double) sizeF1.Width);
        if (menuItem.Shortcut != Shortcut.None)
          empty.Width += 6;
        empty.Width += 20;
        empty.Height = popupHost.Font.Height;
        if (empty.Height < 16 /*0x10*/)
          empty.Height = 16 /*0x10*/;
        if (menuItem.Icon != null)
          num = menuItem.IconSize.Height;
        else if (menuItem.Image != null)
          num = menuItem.Image.Height;
        else if (imageList != null)
          num = imageList.ImageSize.Height;
        if (num > empty.Height)
          empty.Height = num;
        empty.Height += 6;
        if (empty.Width < 32 /*0x20*/)
          empty.Width = 32 /*0x20*/;
        return empty;
      }
    }
}
