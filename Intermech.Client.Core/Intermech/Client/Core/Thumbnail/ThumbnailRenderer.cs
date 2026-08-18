
// Type: Intermech.Client.Core.Thumbnail.ThumbnailRenderer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls.Thumbnail;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace Intermech.Client.Core.Thumbnail;

/// <summary>Summary description for Renderer.</summary>
public class ThumbnailRenderer : BaseRenderer, IDisposable
{
  private List<ThumbnailItem> _items;
  private GetImageHandler _callback;
  private ICategoryTypeIconService _iconService;
  private Font _font;
  private StringFormat _imageStringFormat;

  public ThumbnailRenderer(Font font, GetImageHandler callback)
  {
    this._iconService = (ICategoryTypeIconService) ServicesManager.GetService(typeof (ICategoryTypeIconService));
    this._font = font;
    this._callback = callback;
    this._imageStringFormat = new StringFormat();
    this._imageStringFormat.LineAlignment = StringAlignment.Center;
    this._imageStringFormat.Alignment = StringAlignment.Center;
  }

  public new void Dispose()
  {
    base.Dispose();
    if (this._imageStringFormat == null)
      return;
    this._imageStringFormat.Dispose();
    this._imageStringFormat = (StringFormat) null;
  }

  public override void DrawPanel(
    int panelIndex,
    Graphics g,
    Rectangle bounds,
    bool selected,
    bool active)
  {
    using (SolidBrush solidBrush = new SolidBrush(selected ? (active ? this.SelectedColor : this.SelectedInactiveColor) : this.Color))
    {
      g.FillRectangle((Brush) solidBrush, bounds);
      bounds.Inflate(-2, -2);
      Rectangle rectangle = new Rectangle(bounds.Left, bounds.Top, bounds.Width, bounds.Height - 21);
      Rectangle layoutRectangle = new Rectangle(rectangle.Left, rectangle.Bottom + 1, bounds.Width, 20);
      g.FillRectangle(SystemBrushes.Control, rectangle);
      if (this._items == null)
        return;
      ThumbnailItem thumbnailItem = this._items[panelIndex];
      if (this._iconService != null)
      {
        int index = this._iconService.IndexOf(4, thumbnailItem.TypeId);
        if (index != -1)
          this._iconService.ImageList.Draw(g, layoutRectangle.Left + 2, layoutRectangle.Top + 2, index);
        int num = this._iconService.ImageList.ImageSize.Width + 4;
        layoutRectangle.X += num;
        layoutRectangle.Width -= num;
      }
      else
      {
        layoutRectangle.X += 4;
        layoutRectangle.Width -= 4;
      }
      g.DrawString(thumbnailItem.Name, this._font, selected ? SystemBrushes.ControlLightLight : SystemBrushes.ControlText, (RectangleF) layoutRectangle, this.TextFormat);
      if (panelIndex == 0)
        ControlPaint.DrawSizeGrip(g, SystemColors.Control, bounds.Right - 17, bounds.Bottom - 17, 16 /*0x10*/, 16 /*0x10*/);
      if (this._callback == null)
        return;
      object image = this._callback(panelIndex);
      if (image == null)
        return;
      ThumbnailRenderer.DrawImageObject(g, image, rectangle, this._font, this._imageStringFormat);
    }
  }

  public static void DrawImageObject(
    Graphics g,
    object image,
    Rectangle imageBounds,
    Font font,
    StringFormat imageStringFormat)
  {
    Rectangle imageBounds1 = imageBounds;
    try
    {
      imageBounds.Inflate(-2, -2);
      switch (image)
      {
        case null:
          break;
        case Image _:
          Image image1 = image as Image;
          Rectangle rect = BaseRenderer.SmartStretchBounds(imageBounds, image1.Width, image1.Height);
          g.DrawImage(image1, rect);
          break;
        case Icon _:
          Icon icon = image as Icon;
          if (icon.Width > imageBounds.Width || icon.Height > imageBounds.Height)
          {
            Rectangle targetRect = BaseRenderer.SmartStretchBounds(imageBounds, icon.Width, icon.Height);
            g.DrawIcon(icon, targetRect);
            break;
          }
          int x = imageBounds.X + (imageBounds.Width - icon.Width) / 2;
          int y = imageBounds.Y + (imageBounds.Height - icon.Height) / 2;
          g.DrawIcon(icon, x, y);
          break;
        case IThumbImage _:
          IThumbImage thumbImage = image as IThumbImage;
          Rectangle stretchBounds = BaseRenderer.SmartStretchBounds(imageBounds, thumbImage.Width, thumbImage.Height);
          thumbImage.PaintTo(g, imageBounds, stretchBounds);
          break;
        default:
          string noPicture = image.ToString();
          if (image is DBNull)
            noPicture = PicturesCache.NoPicture;
          g.DrawString(noPicture, font, SystemBrushes.ControlText, (RectangleF) imageBounds, imageStringFormat);
          break;
      }
    }
    catch (Exception ex)
    {
      ThumbnailRenderer.DrawImageObject(g, (object) ex.Message, imageBounds1, font, imageStringFormat);
    }
  }

  public static Size GetImageObjectSizeAdv(object image, Rectangle imageBounds)
  {
    Size imageObjectSizeAdv = Size.Empty;
    switch (image)
    {
      case null:
        return imageObjectSizeAdv;
      case Image _:
        Image image1 = image as Image;
        Rectangle rectangle1 = BaseRenderer.SmartStretchBoundsAdv(imageBounds, image1.Width, image1.Height);
        imageObjectSizeAdv = new Size(rectangle1.Width, rectangle1.Height);
        goto case null;
      case Icon _:
        Icon icon = image as Icon;
        if (icon.Width > imageBounds.Width || icon.Height > imageBounds.Height)
        {
          Rectangle rectangle2 = BaseRenderer.SmartStretchBoundsAdv(imageBounds, icon.Width, icon.Height);
          imageObjectSizeAdv = new Size(rectangle2.Width, rectangle2.Height);
          goto case null;
        }
        imageObjectSizeAdv = icon.Size;
        goto case null;
      case IThumbImage _:
        IThumbImage thumbImage = image as IThumbImage;
        Rectangle rectangle3 = BaseRenderer.SmartStretchBoundsAdv(imageBounds, thumbImage.Width, thumbImage.Height);
        imageObjectSizeAdv = new Size(rectangle3.Width, rectangle3.Height);
        goto case null;
      default:
        image.ToString();
        imageObjectSizeAdv = new Size(imageBounds.Width, imageBounds.Height);
        goto case null;
    }
  }

  public static void DrawImageObjectAdv(
    Graphics g,
    object image,
    Rectangle imageBounds,
    Font font,
    StringFormat imageStringFormat)
  {
    switch (image)
    {
      case null:
        break;
      case Image _:
        Image image1 = image as Image;
        Rectangle rect = BaseRenderer.SmartStretchBoundsAdv(imageBounds, image1.Width, image1.Height);
        g.DrawImage(image1, rect);
        break;
      case Icon _:
        Icon icon = image as Icon;
        if (icon.Width > imageBounds.Width || icon.Height > imageBounds.Height)
        {
          Rectangle targetRect = BaseRenderer.SmartStretchBoundsAdv(imageBounds, icon.Width, icon.Height);
          g.DrawIcon(icon, targetRect);
          break;
        }
        int x = imageBounds.X + (imageBounds.Width - icon.Width) / 2;
        int y = imageBounds.Y;
        g.DrawIcon(icon, x, y);
        break;
      case IThumbImage _:
        IThumbImage thumbImage = image as IThumbImage;
        Rectangle stretchBounds = BaseRenderer.SmartStretchBoundsAdv(imageBounds, thumbImage.Width, thumbImage.Height);
        thumbImage.PaintTo(g, imageBounds, stretchBounds);
        break;
      default:
        string noPicture = image.ToString();
        if (image is DBNull)
          noPicture = PicturesCache.NoPicture;
        g.DrawString(noPicture, font, SystemBrushes.ControlText, (RectangleF) imageBounds, imageStringFormat);
        break;
    }
  }

  public List<ThumbnailItem> Items
  {
    get => this._items;
    set => this._items = value;
  }
}
