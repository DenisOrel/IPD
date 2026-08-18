
// Type: SuperTooltips.ShadowPaintInfo
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace SuperTooltips
{
    public class ShadowPaintInfo
    {
      public Graphics Graphics;
      public Rectangle Bounds;
      public int Size;

      public ShadowPaintInfo()
      {
        this.Bounds = Rectangle.Empty;
        this.Size = 3;
      }
    }
}
