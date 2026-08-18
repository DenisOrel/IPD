
// Type: Intermech.ButtonsPanel.ConvertHelper
// Assembly: Intermech.Bars, Version=4.0.14.0, Culture=neutral, PublicKeyToken=null
// MVID: E7FE806E-DF4F-43E8-8F59-6B4716E1A4DC
:\IPS\Client\Intermech.Bars.dll

using System.Drawing;


namespace Intermech.ButtonsPanel
{
    internal sealed class ConvertHelper
    {
      public static RectangleF ToRectangleF(Rectangle r)
      {
        return new RectangleF((float) r.X, (float) r.Y, (float) r.Width, (float) r.Height);
      }
    }
}
