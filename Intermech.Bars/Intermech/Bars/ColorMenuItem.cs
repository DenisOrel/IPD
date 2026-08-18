
// Type: Intermech.Bars.ColorMenuItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace Intermech.Bars
{
    public class ColorMenuItem : MenuButtonItem
    {
      private Color color = Color.Black;
      internal Rectangle colorBounds;

      public Color Color
      {
        get => this.color;
        set
        {
          this.color = value;
          this.Invalidate();
        }
      }

      internal void SetBounds(Graphics graphics, Rectangle bounds, bool vertical, bool rightToLeft)
      {
        this.ApplyLayout(bounds, graphics, vertical, rightToLeft);
        this.colorBounds = bounds;
        this.colorBounds.Inflate(-3, -3);
      }
    }
}
