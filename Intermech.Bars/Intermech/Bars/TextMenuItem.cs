
// Type: Intermech.Bars.TextMenuItem
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace Intermech.Bars
{
    public class TextMenuItem : MenuButtonItem
    {
      internal Rectangle _bounds;

      public void SetBounds(Graphics graphics, Rectangle bounds, bool vertical, bool rightToLeft)
      {
        this.ApplyLayout(bounds, graphics, vertical, rightToLeft);
        this._bounds = bounds;
      }

      public Rectangle TextBounds => this._bounds;
    }
}
