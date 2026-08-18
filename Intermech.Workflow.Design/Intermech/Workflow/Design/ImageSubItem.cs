// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ImageSubItem
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow.Design;

public class ImageSubItem : OwnerdrawListViewSubitem
{
  public Image Image;

  public override void Draw(DrawInfo di, DrawListViewSubItemEventArgs e)
  {
    base.Draw(di, e);
    if (this.Image != null)
    {
      Rectangle bounds = e.Bounds;
      int y1 = bounds.Y;
      bounds = e.Bounds;
      int num1 = bounds.Height / 2;
      int y2 = y1 + num1 - e.SubItem.Font.Height / 2;
      SizeF sizeF = e.Graphics.MeasureString(e.SubItem.Text, e.SubItem.Font);
      bounds = e.Bounds;
      int x1 = bounds.X;
      bounds = e.Bounds;
      int num2 = bounds.Width / 2;
      int x2 = (int) ((double) (x1 + num2) - (double) sizeF.Width / 2.0 - (double) (this.Image.Width / 2) - 1.0);
      bounds = e.Bounds;
      int y3 = bounds.Y;
      bounds = e.Bounds;
      int num3 = bounds.Height / 2;
      int y4 = y3 + num3 - this.Image.Height / 2;
      e.Graphics.DrawImage(this.Image, new Point(x2, y4));
      int x3 = x2 + (this.Image.Width + 2);
      using (Brush brush = (Brush) new SolidBrush(di.ForeColor))
        e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, brush, (float) x3, (float) y2);
    }
    else
      e.DrawDefault = true;
  }
}
