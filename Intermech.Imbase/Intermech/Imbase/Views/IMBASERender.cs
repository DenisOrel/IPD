// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Views.IMBASERender
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Controls.Thumbnail;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.Views;

internal class IMBASERender : IThumbnailRenderer, IDisposable
{
  private GetImageHandler _callback;
  private Size _MinSize;
  private Size _MaxSize;
  private Font _Font;
  private StringFormat _imageStringFormat;
  private Color _selectedColor;
  private Color _color;
  private ThumbnailsDictionary _thDict = new ThumbnailsDictionary();

  public event RedrawEventHandler RedrawRequired;

  protected void OnRedrawRequired(Rectangle bounds)
  {
    RedrawEventHandler redrawRequired = this.RedrawRequired;
    if (redrawRequired == null)
      return;
    redrawRequired((object) this, bounds.IsEmpty ? BoundsEventArgs.EmptyBounds : new BoundsEventArgs(bounds));
  }

  public IMBASERender(Font font, ThumbnailsDictionary thDict, GetImageHandler callback)
  {
    this._Font = font;
    this._thDict = thDict;
    this._callback = callback;
    this._MinSize = new Size(32 /*0x20*/, 32 /*0x20*/);
    this._MaxSize = new Size(400, 400);
    this._selectedColor = SystemColors.ControlLightLight;
    this._color = SystemColors.ControlDark;
    this._imageStringFormat = new StringFormat();
    this._imageStringFormat.LineAlignment = StringAlignment.Center;
    this._imageStringFormat.Alignment = StringAlignment.Center;
  }

  public void Dispose()
  {
    if (this._imageStringFormat == null)
      return;
    this._imageStringFormat.Dispose();
    this._imageStringFormat = (StringFormat) null;
  }

  public Size MinimumSize => this._MinSize;

  public Size MaximumSize => this._MaxSize;

  public ThumbnailsDictionary ThDictionary
  {
    get => this._thDict;
    set => this._thDict = value;
  }

  private void DrawImageObject(
    Graphics g,
    object image,
    Rectangle bounds,
    Font font,
    StringFormat imageStringFormat)
  {
    bounds.Inflate(-2, -2);
    switch (image)
    {
      case null:
        break;
      case Image _:
        Image image1 = image as Image;
        Rectangle rect = BaseRenderer.SmartStretchBounds(bounds, image1.Width, image1.Height);
        g.DrawImage(image1, rect);
        break;
      case Icon _:
        Icon icon = image as Icon;
        if (icon.Width > bounds.Width || icon.Height > bounds.Height)
        {
          Rectangle targetRect = BaseRenderer.SmartStretchBounds(bounds, icon.Width, icon.Height);
          g.DrawIcon(icon, targetRect);
          break;
        }
        int x = bounds.X + (bounds.Width - icon.Width) / 2;
        int y = bounds.Y + (bounds.Height - icon.Height) / 2;
        g.DrawIcon(icon, x, y);
        break;
      case IThumbImage _:
        IThumbImage thumbImage = image as IThumbImage;
        Rectangle stretchBounds = BaseRenderer.SmartStretchBounds(bounds, thumbImage.Width, thumbImage.Height);
        thumbImage.PaintTo(g, bounds, stretchBounds);
        break;
      default:
        string s = image.ToString();
        if (image is DBNull)
          s = LocalizationHolder.rm.GetString("Imbase.Client_9");
        g.DrawString(s, font, SystemBrushes.ControlText, (RectangleF) bounds, imageStringFormat);
        break;
    }
  }

  public void DrawPanel(int panelIndex, Graphics g, Rectangle bounds, bool selected, bool active)
  {
    using (SolidBrush solidBrush = new SolidBrush(selected ? this._selectedColor : this._color))
    {
      g.FillRectangle((Brush) solidBrush, bounds);
      bounds.Inflate(-1, -1);
      g.FillRectangle(SystemBrushes.Control, bounds);
      if (this._thDict.Count <= 0 || this._thDict.Count <= panelIndex)
        return;
      ThumbnailItem thumbnailItem = this._thDict[panelIndex];
      if (panelIndex == 0)
        ControlPaint.DrawSizeGrip(g, SystemColors.Control, bounds.Right - 17, bounds.Bottom - 17, 16 /*0x10*/, 16 /*0x10*/);
      if (this._callback == null)
        return;
      object image = this._callback(panelIndex);
      if (image == null)
        return;
      this.DrawImageObject(g, image, bounds, this._Font, this._imageStringFormat);
    }
  }
}
