// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ImageListViewSubItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ImageListViewSubItem : OwnerdrawListViewSubitem
{
  private int _imageIndex = -1;
  private Image _image;

  public ImageListViewSubItem()
  {
  }

  public ImageListViewSubItem(string text) => this.Text = text;

  public ImageListViewSubItem(string text, int imageIndex)
  {
    this._imageIndex = imageIndex;
    this.Text = text;
  }

  public ImageListViewSubItem(string text, Image image)
  {
    this._image = image;
    this.Text = text;
  }

  public int ImageIndex
  {
    get => this._imageIndex;
    set => this._imageIndex = value;
  }

  public Image Image => this._image;

  public override void Draw(DrawInfo di, DrawListViewSubItemEventArgs e)
  {
    base.Draw(di, e);
    ImageList subImages = di.View.GetSubImages();
    if (subImages != null && this.ImageIndex != -1 || this.Image != null)
    {
      Rectangle bounds = e.Bounds;
      int y1 = bounds.Y;
      bounds = e.Bounds;
      int num1 = bounds.Height / 2;
      int y2 = y1 + num1 - e.SubItem.Font.Height / 2;
      bounds = e.Bounds;
      int x = bounds.X + 2;
      bounds = e.Bounds;
      int y3 = bounds.Y;
      bounds = e.Bounds;
      int num2 = bounds.Height / 2;
      int num3 = y3 + num2;
      if (subImages != null)
      {
        int y4 = num3 - subImages.ImageSize.Height / 2;
        try
        {
          subImages.Draw(e.Graphics, new Point(x, y4), this.ImageIndex);
        }
        catch
        {
          this.ImageIndex = -1;
        }
        x += subImages.ImageSize.Width + 2;
      }
      else if (this.Image != null)
      {
        int y5 = num3 - this.Image.Height / 2;
        e.Graphics.DrawImageUnscaled(this.Image, new Point(x, y5));
        x += this.Image.Width + 2;
      }
      using (Brush brush = (Brush) new SolidBrush(di.ForeColor))
        e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, (float) x, (float) y2);
    }
    else
      e.DrawDefault = true;
  }
}
